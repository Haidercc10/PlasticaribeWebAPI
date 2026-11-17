using System.Data;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using PlasticaribeAPI.Service;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SolicitudesExtrusionController : ControllerBase
    {
        private readonly dataContext _context;
        private readonly ReservasInventarioService _reservas;
        private readonly SolicitudesMaterialService _solicitudes;
        private readonly IAuthorizationService _authorization;

        /// <summary>Proceso en el que se solicitan materias primas, que cuentan para el límite de kg de la OT.</summary>
        private const string ProcesoExtrusion = "EXT";

        /// <summary>
        /// Inicializa el controlador con el contexto de datos y los servicios de reservas y solicitudes.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        /// <param name="reservas">Servicio que calcula las reservas informativas.</param>
        /// <param name="solicitudes">Reglas de estado y cantidades de las solicitudes.</param>
        /// <param name="authorization">Evalúa la policy ExcederLimiteOT del usuario.</param>
        public SolicitudesExtrusionController(dataContext context, ReservasInventarioService reservas, SolicitudesMaterialService solicitudes, IAuthorizationService authorization)
        {
            _context = context;
            _reservas = reservas;
            _solicitudes = solicitudes;
            _authorization = authorization;
        }

        /// <summary>
        /// Consulta la existencia, la reserva informativa y la disponibilidad por subcategoría.
        /// </summary>
        /// <param name="subcategorias">Subcategorías separadas por coma; si se omite se consultan todas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Lista de existencia, reserva pendiente y disponible por subcategoría; 400 si el filtro no es válido.</returns>
        [HttpGet("reservas")]
        public async Task<ActionResult<List<ReservaInventarioDto>>> ObtenerReservas(
            [FromQuery] string? subcategorias,
            CancellationToken cancellationToken)
        {
            var ids = new List<int>();
            if (!string.IsNullOrWhiteSpace(subcategorias))
            {
                foreach (var texto in subcategorias.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    if (!int.TryParse(texto, NumberStyles.None, CultureInfo.InvariantCulture, out var id))
                        return BadRequest("El filtro de subcategorías no es válido.");
                    ids.Add(id);
                }
            }

            return Ok(await _reservas.ObtenerReservasAsync(ids, cancellationToken));
        }

        /// <summary>
        /// Lista las asignaciones hechas contra una solicitud, indicando si tienen materias primas y/o tintas
        /// para generar el PDF de cada tipo de movimiento.
        /// </summary>
        /// <param name="id">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Asignaciones de la solicitud ordenadas por número; 400 para la solicitud genérica.</returns>
        [HttpGet("{id:long}/asignaciones")]
        public async Task<IActionResult> ObtenerAsignaciones(long id, CancellationToken cancellationToken)
        {
            if (id == EstadosSolicitudMaterial.SolicitudGenericaId)
                return BadRequest("La solicitud genérica no tiene asignaciones propias.");

            var asignaciones = await _context.Asignaciones_MatPrima
                .AsNoTracking()
                .Where(a => a.SolMpExt_Id == id)
                .OrderBy(a => a.AsigMp_Id)
                .Select(a => new
                {
                    a.AsigMp_Id,
                    a.AsigMp_FechaEntrega,
                    TieneMateriaPrima = _context.DetallesAsignaciones_MateriasPrimas.Any(d => d.AsigMp_Id == a.AsigMp_Id),
                    TieneTintas = _context.DetalleAsignaciones_Tintas.Any(d => d.AsigMp_Id == a.AsigMp_Id)
                })
                .ToListAsync(cancellationToken);

            return Ok(asignaciones);
        }

        /// <summary>
        /// Registra en una sola transacción una solicitud de material de producción y sus detalles.
        /// El usuario se toma del token, el estado inicial es pendiente y la reserva es informativa:
        /// si lo solicitado supera la disponibilidad se exige confirmación explícita, sin descontar existencias.
        /// </summary>
        /// <param name="request">Encabezado y detalles de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>
        /// 201 con la solicitud creada; 400 si los datos no son válidos; 401 si el token no trae el usuario;
        /// 409 si supera el límite de kg de la OT (o falta confirmarlo) o con las advertencias de disponibilidad si falta confirmar.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CrearSolicitud(SolicitudExtrusionRequest request, CancellationToken cancellationToken)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();

            var errorFormato = ValidarFormatoDetalles(request.Detalles);
            if (errorFormato != null) return BadRequest(errorFormato);
            if (request.FechaEstimadaEntrega!.Value.Date < DateTime.Today)
                return BadRequest("La fecha estimada de entrega no puede ser anterior a hoy.");

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IActionResult>(async () =>
            {
                _context.ChangeTracker.Clear();
                // Serializable, igual que la edición, para que dos solicitudes simultáneas no superen juntas el límite de la OT.
                await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

                if (!await _context.Procesos.AnyAsync(p => p.Proceso_Id == request.ProcesoId, cancellationToken))
                    return BadRequest("El proceso indicado no existe.");

                if (!await MaquinaPerteneceAlProcesoAsync(request.Maquina, request.ProcesoId, cancellationToken))
                    return BadRequest($"La máquina {request.Maquina} no pertenece al proceso {request.ProcesoId}.");

                var (errorCatalogos, nombres) = await ValidarCatalogosAsync(request.Detalles, cancellationToken);
                if (errorCatalogos != null) return BadRequest(errorCatalogos);

                var errorTipo = await ValidarTipoSubcategoriasAsync(request.ProcesoId, request.Detalles.Select(d => d.SubCatMP_Id).ToList(), nombres!, cancellationToken);
                if (errorTipo != null) return BadRequest(errorTipo);

                if (request.ProcesoId == ProcesoExtrusion)
                {
                    var errorLimite = await ValidarLimiteOTAsync(request.OrdenTrabajo, request.LimiteKgOT, request.ConfirmarExcesoLimite,
                        request.Detalles, new Dictionary<int, CantidadSubcategoria>(), cancellationToken);
                    if (errorLimite != null) return errorLimite;
                }

                var advertencias = await CalcularAdvertenciasAsync(request.Detalles, nombres!, new Dictionary<int, decimal>(), cancellationToken);
                if (advertencias.Count > 0 && !request.ConfirmarSaldoNegativo)
                    return ConflictoDisponibilidad(advertencias);

                var ahora = DateTime.Now;
                var solicitud = new Solicitud_MatPrimaExtrusion
                {
                    SolMpExt_OT = request.OrdenTrabajo,
                    SolMpExt_Maquina = request.Maquina,
                    Proceso_Id = request.ProcesoId,
                    Estado_Id = EstadosSolicitudMaterial.Pendiente,
                    Usua_Id = usuarioId,
                    SolMpExt_Fecha = ahora.Date,
                    SolMpExt_Hora = ahora.ToString("H:mm:ss", CultureInfo.InvariantCulture),
                    SolMpExt_Observacion = request.Observacion,
                    SolMpExt_FechaEstimadaEntrega = request.FechaEstimadaEntrega?.Date
                };
                _context.Solicitud_MatPrimaExtrusion.Add(solicitud);
                await _context.SaveChangesAsync(cancellationToken);

                _context.DetSolicitud_MatPrimaExtrusion.AddRange(request.Detalles.Select(d => NuevoDetalle(solicitud.SolMpExt_Id, d, nombres![d.SubCatMP_Id])));
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return CreatedAtAction(
                    nameof(ObtenerReservas),
                    null,
                    new { solicitud.SolMpExt_Id, solicitud.Estado_Id, Advertencias = advertencias });
            });
        }

        /// <summary>
        /// Edita en una sola transacción el encabezado y los detalles de una solicitud activa.
        /// Las subcategorías que no vienen en la petición se eliminan. No se permite eliminar una subcategoría
        /// con entregas ni dejar una cantidad por debajo de lo ya entregado. Al final se recalcula el estado.
        /// </summary>
        /// <param name="id">Identificador de la solicitud.</param>
        /// <param name="request">Encabezado y detalles finales de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>
        /// 200 con el estado resultante; 400 si los datos no son válidos; 404 si no existe;
        /// 409 si la solicitud está cerrada, si se reduce por debajo de lo entregado, si supera el límite de kg de la OT
        /// o si falta confirmar la disponibilidad. Solo para la policy EditarSolicitudMaterial.
        /// </returns>
        [HttpPut("{id:long}")]
        [Authorize(Policy = "EditarSolicitudMaterial")]
        public async Task<IActionResult> EditarSolicitud(long id, EditarSolicitudExtrusionRequest request, CancellationToken cancellationToken)
        {
            if (id == EstadosSolicitudMaterial.SolicitudGenericaId)
                return Conflict("La solicitud genérica no se puede editar.");

            var errorFormato = ValidarFormatoDetalles(request.Detalles);
            if (errorFormato != null) return BadRequest(errorFormato);

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IActionResult>(async () =>
            {
                _context.ChangeTracker.Clear();
                await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

                var solicitud = await _context.Solicitud_MatPrimaExtrusion
                    .AsTracking()
                    .FirstOrDefaultAsync(s => s.SolMpExt_Id == id, cancellationToken);
                if (solicitud == null) return NotFound($"La solicitud N° {id} no existe.");
                if (EstadosSolicitudMaterial.EstaCerrada(solicitud.Estado_Id))
                    return Conflict($"La solicitud N° {id} está finalizada, verificada o cancelada y no se puede editar.");

                // Solo se exige fecha futura si se cambia, para poder editar solicitudes cuya fecha ya pasó sin moverla.
                var fechaEntrega = request.FechaEstimadaEntrega!.Value.Date;
                if (fechaEntrega != solicitud.SolMpExt_FechaEstimadaEntrega?.Date && fechaEntrega < DateTime.Today)
                    return BadRequest("La fecha estimada de entrega no puede ser anterior a hoy.");

                // La máquina solo se valida si cambia, para no bloquear solicitudes antiguas con máquinas que ya no están en el catálogo.
                if (request.Maquina != solicitud.SolMpExt_Maquina &&
                    !await MaquinaPerteneceAlProcesoAsync(request.Maquina, solicitud.Proceso_Id, cancellationToken))
                    return BadRequest($"La máquina {request.Maquina} no pertenece al proceso {solicitud.Proceso_Id}.");

                var (errorCatalogos, nombres) = await ValidarCatalogosAsync(request.Detalles, cancellationToken);
                if (errorCatalogos != null) return BadRequest(errorCatalogos);

                var cantidades = (await _solicitudes.ObtenerCantidadesAsync(id, cancellationToken)).ToDictionary(c => c.SubCatMP_Id);
                var nuevas = request.Detalles.ToDictionary(d => d.SubCatMP_Id);

                // Solo se valida el tipo de las subcategorías agregadas; las que ya tenía la solicitud se respetan.
                var agregadas = request.Detalles.Select(d => d.SubCatMP_Id).Where(s => !cantidades.ContainsKey(s)).ToList();
                var errorTipo = await ValidarTipoSubcategoriasAsync(solicitud.Proceso_Id, agregadas, nombres!, cancellationToken);
                if (errorTipo != null) return BadRequest(errorTipo);

                var eliminadaConEntrega = cantidades.Values.FirstOrDefault(c => !nuevas.ContainsKey(c.SubCatMP_Id) && c.Asignado > 0m);
                if (eliminadaConEntrega != null)
                {
                    var nombre = await _context.Subcategorias_MatPrima
                        .AsNoTracking()
                        .Where(s => s.SubCatMP_Id == eliminadaConEntrega.SubCatMP_Id)
                        .Select(s => s.SubCatMP_Nombre)
                        .FirstOrDefaultAsync(cancellationToken);
                    return Conflict($"No se puede quitar la subcategoría {nombre?.Trim() ?? eliminadaConEntrega.SubCatMP_Id.ToString(CultureInfo.InvariantCulture)} porque ya tiene {eliminadaConEntrega.Asignado.ToString("N2", CultureInfo.InvariantCulture)} entregados.");
                }

                var reducidaBajoEntrega = request.Detalles.FirstOrDefault(d => cantidades.TryGetValue(d.SubCatMP_Id, out var c) && d.Cantidad < c.Asignado);
                if (reducidaBajoEntrega != null)
                    return Conflict($"La cantidad de {nombres![reducidaBajoEntrega.SubCatMP_Id]} no puede ser menor a lo ya entregado ({cantidades[reducidaBajoEntrega.SubCatMP_Id].Asignado.ToString("N2", CultureInfo.InvariantCulture)}).");

                if (solicitud.Proceso_Id == ProcesoExtrusion)
                {
                    var errorLimite = await ValidarLimiteOTAsync(solicitud.SolMpExt_OT, request.LimiteKgOT, request.ConfirmarExcesoLimite,
                        request.Detalles, cantidades, cancellationToken);
                    if (errorLimite != null) return errorLimite;
                }

                var pendienteAnterior = cantidades.ToDictionary(c => c.Key, c => c.Value.Faltante);
                var advertencias = await CalcularAdvertenciasAsync(request.Detalles, nombres!, pendienteAnterior, cancellationToken, cantidades);
                if (advertencias.Count > 0 && !request.ConfirmarSaldoNegativo)
                    return ConflictoDisponibilidad(advertencias);

                solicitud.SolMpExt_Maquina = request.Maquina;
                solicitud.SolMpExt_Observacion = request.Observacion;
                solicitud.SolMpExt_FechaEstimadaEntrega = request.FechaEstimadaEntrega?.Date;

                var existentes = await _context.DetSolicitud_MatPrimaExtrusion
                    .AsTracking()
                    .Where(d => d.SolMpExt_Id == id)
                    .OrderBy(d => d.Codigo)
                    .ToListAsync(cancellationToken);

                foreach (var grupo in existentes.GroupBy(d => d.SubCatMP_Id))
                {
                    if (!nuevas.TryGetValue(grupo.Key, out var nueva))
                    {
                        _context.DetSolicitud_MatPrimaExtrusion.RemoveRange(grupo);
                        continue;
                    }

                    // Las filas duplicadas de una misma subcategoría (datos anteriores) se consolidan en la primera.
                    var principal = grupo.First();
                    principal.DtSolMpExt_Cantidad = nueva.Cantidad;
                    principal.UndMed_Id = nueva.UndMed_Id;
                    _context.DetSolicitud_MatPrimaExtrusion.RemoveRange(grupo.Skip(1));
                }

                var subcategoriasExistentes = existentes.Select(d => d.SubCatMP_Id).ToHashSet();
                _context.DetSolicitud_MatPrimaExtrusion.AddRange(request.Detalles
                    .Where(d => !subcategoriasExistentes.Contains(d.SubCatMP_Id))
                    .Select(d => NuevoDetalle(id, d, nombres![d.SubCatMP_Id])));

                await _context.SaveChangesAsync(cancellationToken);
                var estado = await _solicitudes.RecalcularEstadoAsync(id, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Ok(new { SolMpExt_Id = id, Estado_Id = estado, Advertencias = advertencias });
            });
        }

        /// <summary>Indica si la máquina (por su número) pertenece al proceso.</summary>
        private Task<bool> MaquinaPerteneceAlProcesoAsync(int maquina, string proceso, CancellationToken cancellationToken) =>
            _context.Maquinas.AsNoTracking().AnyAsync(m => m.Maq_Numero == maquina && m.Proceso_Id == proceso, cancellationToken);

        /// <summary>
        /// Comprueba que las subcategorías correspondan al proceso, con el mismo criterio de la búsqueda de subcategorías:
        /// en Extrusión deben agrupar materias primas y en los demás procesos deben agrupar tintas.
        /// </summary>
        /// <returns>Mensaje de error, o null si todas corresponden.</returns>
        private async Task<string?> ValidarTipoSubcategoriasAsync(
            string proceso,
            IReadOnlyCollection<int> subcategorias,
            IReadOnlyDictionary<int, string> nombres,
            CancellationToken cancellationToken)
        {
            if (subcategorias.Count == 0) return null;

            var ids = subcategorias.ToList();
            var esExtrusion = proceso == ProcesoExtrusion;
            var validas = esExtrusion
                ? await _context.Materias_Primas
                    .AsNoTracking()
                    .Where(m => m.MatPri_Id != SolicitudesMaterialService.MateriaPrimaGenericaId && m.SubCatMP_Id != null && ids.Contains(m.SubCatMP_Id.Value))
                    .Select(m => m.SubCatMP_Id!.Value)
                    .Distinct()
                    .ToListAsync(cancellationToken)
                : await _context.Tintas
                    .AsNoTracking()
                    .Where(t => t.Tinta_Id != SolicitudesMaterialService.TintaGenericaId && t.SubCatMP_Id != null && ids.Contains(t.SubCatMP_Id.Value))
                    .Select(t => t.SubCatMP_Id!.Value)
                    .Distinct()
                    .ToListAsync(cancellationToken);

            var invalida = ids.FirstOrDefault(s => !validas.Contains(s));
            if (invalida == 0) return null;
            var nombre = nombres.TryGetValue(invalida, out var n) ? n.Trim() : invalida.ToString(CultureInfo.InvariantCulture);
            return esExtrusion
                ? $"La subcategoría {nombre} no tiene materias primas; en Extrusión solo se solicitan materias primas."
                : $"La subcategoría {nombre} no tiene tintas; en el proceso {proceso} solo se solicitan tintas.";
        }

        /// <summary>
        /// Valida que la solicitud no haga superar el límite de kg de la OT: lo asignado más lo solicitado pendiente de la OT
        /// más el aumento de pendiente que genera esta operación (solo materias primas; las tintas no cuentan).
        /// Si lo supera, solo la policy ExcederLimiteOT puede continuar, y debe confirmarlo.
        /// </summary>
        /// <param name="ot">Orden de trabajo.</param>
        /// <param name="limite">Kg máximos de la OT enviados por el cliente (kg de la OT más el adicional).</param>
        /// <param name="confirmar">El usuario confirmó superar el límite.</param>
        /// <param name="detalles">Detalles finales de la solicitud.</param>
        /// <param name="cantidades">Cantidades que ya tenía la solicitud (vacío al crear).</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Respuesta de error, o null si puede continuar.</returns>
        private async Task<IActionResult?> ValidarLimiteOTAsync(
            long ot,
            decimal? limite,
            bool confirmar,
            IReadOnlyCollection<SolicitudExtrusionDetalleRequest> detalles,
            IReadOnlyDictionary<int, CantidadSubcategoria> cantidades,
            CancellationToken cancellationToken)
        {
            if (limite == null)
                return BadRequest("Debe indicar el límite de kg de la OT para solicitar materias primas de Extrusión.");

            var ids = detalles.Select(d => d.SubCatMP_Id).Concat(cantidades.Keys).Distinct().ToList();
            var tintas = await _solicitudes.ObtenerSubcategoriasDeTintaAsync(ids, cancellationToken);

            var pendienteNuevo = detalles
                .Where(d => !tintas.Contains(d.SubCatMP_Id))
                .Sum(d => Math.Max(0m, d.Cantidad - (cantidades.TryGetValue(d.SubCatMP_Id, out var c) ? c.Asignado : 0m)));
            var pendienteAnterior = cantidades.Values
                .Where(c => !tintas.Contains(c.SubCatMP_Id))
                .Sum(c => c.Faltante);
            var aumento = pendienteNuevo - pendienteAnterior;
            if (aumento <= 0m) return null;

            var consumo = await _solicitudes.CalcularConsumoOTAsync(ot, cancellationToken);
            var pendienteOT = await _solicitudes.ObtenerPendientePorAsignarOTAsync(ot, cancellationToken);
            var exceso = consumo + pendienteOT + aumento - limite.Value;
            if (exceso <= 0m) return null;

            var puedeExceder = (await _authorization.AuthorizeAsync(User, "ExcederLimiteOT")).Succeeded;
            if (puedeExceder && confirmar) return null;

            var kg = exceso.ToString("N2", CultureInfo.InvariantCulture);
            return Conflict(new
            {
                RequiereConfirmacionLimite = puedeExceder,
                Mensaje = puedeExceder
                    ? $"La solicitud supera en {kg} kg el límite de la OT {ot}."
                    : $"La solicitud supera en {kg} kg el límite de la OT {ot}. Debe solicitar permisos al administrador.",
                ExcesoKg = exceso
            });
        }

        /// <summary>
        /// Valida decimales y subcategorías repetidas sin consultar la base de datos.
        /// </summary>
        private static string? ValidarFormatoDetalles(IReadOnlyCollection<SolicitudExtrusionDetalleRequest> detalles)
        {
            if (detalles.Any(d => decimal.Round(d.Cantidad, 2) != d.Cantidad))
                return "Las cantidades deben tener máximo dos decimales.";
            if (detalles.Select(d => d.SubCatMP_Id).Distinct().Count() != detalles.Count)
                return "No se puede repetir una subcategoría en la misma solicitud.";
            return null;
        }

        /// <summary>
        /// Comprueba que existan las unidades de medida y las subcategorías, y devuelve el nombre de cada subcategoría.
        /// </summary>
        private async Task<(string? Error, Dictionary<int, string>? Nombres)> ValidarCatalogosAsync(
            IReadOnlyCollection<SolicitudExtrusionDetalleRequest> detalles,
            CancellationToken cancellationToken)
        {
            var unidades = detalles.Select(d => d.UndMed_Id).Distinct().ToList();
            var unidadesValidas = await _context.Unidades_Medidas
                .AsNoTracking()
                .CountAsync(u => unidades.Contains(u.UndMed_Id), cancellationToken);
            if (unidadesValidas != unidades.Count)
                return ("Una o más unidades de medida no existen.", null);

            var ids = detalles.Select(d => d.SubCatMP_Id).ToList();
            var nombres = await _context.Subcategorias_MatPrima
                .AsNoTracking()
                .Where(s => ids.Contains(s.SubCatMP_Id))
                .ToDictionaryAsync(s => s.SubCatMP_Id, s => s.SubCatMP_Nombre, cancellationToken);
            if (nombres.Count != ids.Count)
                return ("Una o más subcategorías no existen.", null);

            return (null, nombres);
        }

        /// <summary>
        /// Calcula las subcategorías cuyo aumento de reserva supera la disponibilidad informativa.
        /// La reserva de una subcategoría es lo solicitado menos lo entregado; la disponibilidad ya descuenta la reserva
        /// anterior de esta misma solicitud, por eso solo se compara el aumento.
        /// </summary>
        private async Task<List<AdvertenciaDisponibilidad>> CalcularAdvertenciasAsync(
            IReadOnlyCollection<SolicitudExtrusionDetalleRequest> detalles,
            IReadOnlyDictionary<int, string> nombres,
            IReadOnlyDictionary<int, decimal> pendienteAnterior,
            CancellationToken cancellationToken,
            IReadOnlyDictionary<int, CantidadSubcategoria>? cantidades = null)
        {
            var disponibilidad = (await _reservas.ObtenerReservasAsync(detalles.Select(d => d.SubCatMP_Id).ToList(), cancellationToken))
                .ToDictionary(r => r.SubCatMP_Id, r => r.Disponible);

            return detalles
                .Select(d =>
                {
                    var asignado = cantidades != null && cantidades.TryGetValue(d.SubCatMP_Id, out var c) ? c.Asignado : 0m;
                    var aumento = Math.Max(0m, d.Cantidad - asignado) - pendienteAnterior.GetValueOrDefault(d.SubCatMP_Id);
                    return new AdvertenciaDisponibilidad(d.SubCatMP_Id, nombres[d.SubCatMP_Id], d.Cantidad, aumento, disponibilidad.GetValueOrDefault(d.SubCatMP_Id));
                })
                .Where(a => a.Aumento > 0m && a.Aumento > a.Disponible)
                .ToList();
        }

        /// <summary>Construye la respuesta 409 que pide confirmar la disponibilidad.</summary>
        private ConflictObjectResult ConflictoDisponibilidad(List<AdvertenciaDisponibilidad> advertencias) => Conflict(new
        {
            RequiereConfirmacion = true,
            Mensaje = "La cantidad solicitada supera la disponibilidad informativa de una o más subcategorías.",
            Advertencias = advertencias
        });

        /// <summary>Crea un detalle nuevo sin entregas.</summary>
        private static DetSolicitud_MatPrimaExtrusion NuevoDetalle(long solicitudId, SolicitudExtrusionDetalleRequest detalle, string nombre) => new()
        {
            SolMpExt_Id = solicitudId,
            SubCatMP_Id = detalle.SubCatMP_Id,
            SubCatMP_Nombre = nombre,
            DtSolMpExt_Cantidad = detalle.Cantidad,
            DtSolMpExt_CantidadEntregada = 0,
            DtSolMpExt_CantidadFaltante = detalle.Cantidad,
            UndMed_Id = detalle.UndMed_Id
        };

        /// <summary>Subcategoría cuya reserva adicional supera la disponibilidad informativa.</summary>
        /// <param name="SubCatMP_Id">Subcategoría.</param>
        /// <param name="Nombre">Nombre de la subcategoría.</param>
        /// <param name="Solicitado">Cantidad total solicitada.</param>
        /// <param name="Aumento">Reserva adicional que genera la operación.</param>
        /// <param name="Disponible">Disponibilidad informativa antes de la operación.</param>
        private sealed record AdvertenciaDisponibilidad(int SubCatMP_Id, string Nombre, decimal Solicitado, decimal Aumento, decimal Disponible);
    }
}
