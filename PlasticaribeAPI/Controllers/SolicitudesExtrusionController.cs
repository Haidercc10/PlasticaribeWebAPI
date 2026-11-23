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
            var (maquinas, errorMaquinas) = ReglasSolicitudMaterial.ValidarMaquinas(request.Maquinas, request.Maquina);
            if (errorMaquinas != null) return BadRequest(errorMaquinas);
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

                var maquinaInvalida = await MaquinaFueraDelProcesoAsync(maquinas!.Select(m => m.Maquina).ToList(), request.ProcesoId, cancellationToken);
                if (maquinaInvalida != null)
                    return BadRequest($"La máquina {maquinaInvalida} no pertenece al proceso {request.ProcesoId}.");

                var (errorCatalogos, nombres) = await ValidarCatalogosAsync(request.Detalles, cancellationToken);
                if (errorCatalogos != null) return BadRequest(errorCatalogos);

                var errorTipo = await ValidarTipoSubcategoriasAsync(request.ProcesoId, request.Detalles.Select(d => d.SubCatMP_Id).ToList(), nombres!, cancellationToken);
                if (errorTipo != null) return BadRequest(errorTipo);

                var errorMateriales = await ValidarMaterialesCatalogoAsync(request.ProcesoId, request.Detalles, nombres!, cancellationToken);
                if (errorMateriales != null) return BadRequest(errorMateriales);

                var otCerrada = SolicitudesMaterialService.MotivoOTCerrada(
                    await _solicitudes.ObtenerEstadoOTAsync(request.OrdenTrabajo, cancellationToken), request.ProcesoId, request.OrdenTrabajo);
                if (otCerrada != null) return Conflict(otCerrada);

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
                    SolMpExt_Maquina = maquinas![0].Maquina, // la primera máquina, para los reportes anteriores
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

                var detalles = request.Detalles.Select(d => (Request: d, Entidad: NuevoDetalle(solicitud.SolMpExt_Id, d, nombres![d.SubCatMP_Id]))).ToList();
                _context.DetSolicitud_MatPrimaExtrusion.AddRange(detalles.Select(d => d.Entidad));
                await _context.SaveChangesAsync(cancellationToken);

                _context.DetSolicitud_Materiales.AddRange(detalles.SelectMany(d => NuevosMateriales(solicitud.SolMpExt_Id, d.Entidad.Codigo, d.Request.Materiales)));
                _context.Solicitud_Maquinas.AddRange(maquinas!.Select(m => NuevaMaquina(solicitud.SolMpExt_Id, m)));
                await _context.SaveChangesAsync(cancellationToken);
                await _solicitudes.RegistrarKgOTAsync(request.OrdenTrabajo, SolicitudesMaterialService.KgOTDe(request.KgOT, request.LimiteKgOT), cancellationToken);
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
            var (maquinas, errorMaquinas) = ReglasSolicitudMaterial.ValidarMaquinas(request.Maquinas, request.Maquina);
            if (errorMaquinas != null) return BadRequest(errorMaquinas);

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
                    return Conflict($"La solicitud N° {id} ya no está activa (completada, finalizada, verificada o cancelada) y no se puede editar.");

                var otCerrada = SolicitudesMaterialService.MotivoOTCerrada(
                    await _solicitudes.ObtenerEstadoOTAsync(solicitud.SolMpExt_OT, cancellationToken), solicitud.Proceso_Id, solicitud.SolMpExt_OT);
                if (otCerrada != null) return Conflict(otCerrada);

                // Solo se exige fecha futura si se cambia, para poder editar solicitudes cuya fecha ya pasó sin moverla.
                var fechaEntrega = request.FechaEstimadaEntrega!.Value.Date;
                if (fechaEntrega != solicitud.SolMpExt_FechaEstimadaEntrega?.Date && fechaEntrega < DateTime.Today)
                    return BadRequest("La fecha estimada de entrega no puede ser anterior a hoy.");

                // Solo se validan las máquinas agregadas, para no bloquear solicitudes antiguas con máquinas que ya no están en el catálogo.
                var maquinasActuales = await _context.Solicitud_Maquinas
                    .AsNoTracking()
                    .Where(m => m.SolMpExt_Id == id)
                    .Select(m => m.SolMaq_Maquina)
                    .ToListAsync(cancellationToken);
                maquinasActuales.Add(solicitud.SolMpExt_Maquina);
                var maquinaInvalida = await MaquinaFueraDelProcesoAsync(
                    maquinas!.Select(m => m.Maquina).Where(m => !maquinasActuales.Contains(m)).ToList(), solicitud.Proceso_Id, cancellationToken);
                if (maquinaInvalida != null)
                    return BadRequest($"La máquina {maquinaInvalida} no pertenece al proceso {solicitud.Proceso_Id}.");

                var (errorCatalogos, nombres) = await ValidarCatalogosAsync(request.Detalles, cancellationToken);
                if (errorCatalogos != null) return BadRequest(errorCatalogos);

                var errorMateriales = await ValidarMaterialesCatalogoAsync(solicitud.Proceso_Id, request.Detalles, nombres!, cancellationToken);
                if (errorMateriales != null) return BadRequest(errorMateriales);

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

                solicitud.SolMpExt_Maquina = maquinas![0].Maquina; // la primera máquina, para los reportes anteriores
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
                var agregados = request.Detalles
                    .Where(d => !subcategoriasExistentes.Contains(d.SubCatMP_Id))
                    .Select(d => NuevoDetalle(id, d, nombres![d.SubCatMP_Id]))
                    .ToList();
                _context.DetSolicitud_MatPrimaExtrusion.AddRange(agregados);

                await _context.SaveChangesAsync(cancellationToken);
                await ReemplazarMaterialesAsync(id, request.Detalles, existentes.Concat(agregados), cancellationToken);
                await ReemplazarMaquinasAsync(id, maquinas!, cancellationToken);
                await _solicitudes.GuardarKgOTAsync(solicitud.SolMpExt_OT, SolicitudesMaterialService.KgOTDe(request.KgOT, request.LimiteKgOT), cancellationToken);
                var estado = await _solicitudes.RecalcularEstadoAsync(id, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Ok(new { SolMpExt_Id = id, Estado_Id = estado, Advertencias = advertencias });
            });
        }

        /// <summary>
        /// Consulta las solicitudes para el reporte de movimientos: una fila por solicitud, de la más nueva a la más vieja.
        /// Al buscar por número de solicitud no se aplican las fechas. Sin fechas no hay límite de tiempo.
        /// </summary>
        /// <param name="desde">Fecha inicial de creación (opcional).</param>
        /// <param name="hasta">Fecha final de creación (opcional).</param>
        /// <param name="id">Número exacto de la solicitud (opcional).</param>
        /// <param name="ot">Orden de trabajo exacta (opcional).</param>
        /// <param name="estados">Estados separados por coma, p. ej. "11,12" (opcional).</param>
        /// <param name="soloMias">Solo las solicitudes creadas por el usuario del token (página del solicitante).</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Solicitudes encontradas con sus máquinas; 400 si el filtro de estados no es válido.</returns>
        [HttpGet("consulta")]
        public async Task<IActionResult> Consultar(
            [FromQuery] DateTime? desde,
            [FromQuery] DateTime? hasta,
            [FromQuery] long? id,
            [FromQuery] long? ot,
            [FromQuery] string? estados,
            [FromQuery] bool soloMias,
            CancellationToken cancellationToken)
        {
            long usuarioId = 0;
            if (soloMias && !long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out usuarioId))
                return Unauthorized();

            var listaEstados = new List<int>();
            foreach (var texto in (estados ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                if (!int.TryParse(texto, NumberStyles.None, CultureInfo.InvariantCulture, out var estado))
                    return BadRequest("El filtro de estados no es válido.");
                listaEstados.Add(estado);
            }

            var consulta = _context.Solicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(s => s.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId);
            if (id != null)
            {
                consulta = consulta.Where(s => s.SolMpExt_Id == id);
            }
            else
            {
                if (desde != null) consulta = consulta.Where(s => s.SolMpExt_Fecha >= desde.Value.Date);
                if (hasta != null) consulta = consulta.Where(s => s.SolMpExt_Fecha <= hasta.Value.Date);
            }
            if (ot != null) consulta = consulta.Where(s => s.SolMpExt_OT == ot);
            if (soloMias) consulta = consulta.Where(s => s.Usua_Id == usuarioId);
            if (listaEstados.Count > 0) consulta = consulta.Where(s => listaEstados.Contains(s.Estado_Id));

            var solicitudes = await consulta
                .OrderByDescending(s => s.SolMpExt_Id)
                .Select(s => new
                {
                    Id = s.SolMpExt_Id,
                    OT = s.SolMpExt_OT,
                    s.Proceso_Id,
                    Proceso = s.Proceso!.Proceso_Nombre,
                    s.Estado_Id,
                    Estado = s.Estado!.Estado_Nombre,
                    // Estado de la solicitud de la OT (todas las solicitudes de la OT).
                    EstadoOT_Id = _context.SolicitudMaterial_OT
                        .Where(o => o.SolOT_OrdenTrabajo == s.SolMpExt_OT)
                        .Select(o => (int?)o.Estado_Id)
                        .FirstOrDefault(),
                    EstadoOT = _context.SolicitudMaterial_OT
                        .Where(o => o.SolOT_OrdenTrabajo == s.SolMpExt_OT)
                        .Select(o => o.Estado!.Estado_Nombre)
                        .FirstOrDefault(),
                    s.Usua_Id,
                    Usuario = s.Usua!.Usua_Nombre,
                    Fecha = s.SolMpExt_Fecha,
                    Hora = s.SolMpExt_Hora,
                    FechaEntrega = s.SolMpExt_FechaEstimadaEntrega,
                    Observacion = s.SolMpExt_Observacion,
                    MaquinaPrincipal = s.SolMpExt_Maquina,
                    // Total solicitado de la solicitud (suma de sus subcategorías).
                    TotalSolicitado = _context.DetSolicitud_MatPrimaExtrusion
                        .Where(d => d.SolMpExt_Id == s.SolMpExt_Id)
                        .Sum(d => (decimal?)d.DtSolMpExt_Cantidad) ?? 0m,
                    Maquinas = _context.Solicitud_Maquinas
                        .Where(m => m.SolMpExt_Id == s.SolMpExt_Id)
                        .OrderBy(m => m.SolMaq_Id)
                        .Select(m => new { Maquina = m.SolMaq_Maquina, Porcentaje = m.SolMaq_Porcentaje })
                        .ToList()
                })
                .ToListAsync(cancellationToken);

            return Ok(solicitudes);
        }

        /// <summary>
        /// Cuenta las solicitudes por estado (sin la solicitud genérica), con el mismo criterio de la consulta sin fechas.
        /// </summary>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Cantidad de solicitudes por estado.</returns>
        [HttpGet("conteo-estados")]
        public async Task<IActionResult> ContarPorEstado(CancellationToken cancellationToken)
        {
            var conteo = await _context.Solicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(s => s.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId)
                .GroupBy(s => new { s.Estado_Id, s.Estado!.Estado_Nombre })
                .Select(g => new { g.Key.Estado_Id, Estado = g.Key.Estado_Nombre, Cantidad = g.Count() })
                .ToListAsync(cancellationToken);

            return Ok(conteo);
        }

        /// <summary>
        /// Obtiene una solicitud con sus máquinas y sus subcategorías; cada subcategoría trae sus materiales con lo
        /// solicitado, lo entregado y el stock. Los materiales entregados que no se solicitaron (sustitutos de la misma
        /// subcategoría) aparecen con solicitado 0. Las solicitudes anteriores no tienen materiales.
        /// </summary>
        /// <param name="id">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>La solicitud; 404 si no existe; 400 para la solicitud genérica.</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> ObtenerDetalle(long id, CancellationToken cancellationToken)
        {
            if (id == EstadosSolicitudMaterial.SolicitudGenericaId)
                return BadRequest("La solicitud genérica no tiene detalle.");

            var encabezado = await _context.Solicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(s => s.SolMpExt_Id == id)
                .Select(s => new
                {
                    s.SolMpExt_OT,
                    s.Proceso_Id,
                    Proceso = s.Proceso!.Proceso_Nombre,
                    s.Estado_Id,
                    Estado = s.Estado!.Estado_Nombre,
                    s.Usua_Id,
                    Usuario = s.Usua!.Usua_Nombre,
                    s.SolMpExt_Fecha,
                    s.SolMpExt_Hora,
                    s.SolMpExt_FechaEstimadaEntrega,
                    s.SolMpExt_Observacion,
                    s.SolMpExt_Maquina,
                    s.SolMpExt_MotivoCancelacion,
                    s.SolMpExt_FechaCancelacion,
                    UsuarioCancela = s.UsuaCancela != null ? s.UsuaCancela.Usua_Nombre : null
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (encabezado == null) return NotFound($"La solicitud N° {id} no existe.");

            var maquinas = await _context.Solicitud_Maquinas
                .AsNoTracking()
                .Where(m => m.SolMpExt_Id == id)
                .OrderBy(m => m.SolMaq_Id)
                .Select(m => new MaquinaSolicitud(m.SolMaq_Maquina, m.SolMaq_Porcentaje))
                .ToListAsync(cancellationToken);
            if (maquinas.Count == 0) maquinas.Add(new MaquinaSolicitud(encabezado.SolMpExt_Maquina, 100m));

            var filas = await _context.DetSolicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(d => d.SolMpExt_Id == id)
                .OrderBy(d => d.Codigo)
                .Select(d => new { d.Codigo, d.SubCatMP_Id, d.SubCatMP_Nombre, d.UndMed_Id, d.DtSolMpExt_Cantidad })
                .ToListAsync(cancellationToken);

            var solicitados = await _context.DetSolicitud_Materiales
                .AsNoTracking()
                .Where(m => m.SolMpExt_Id == id)
                .OrderBy(m => m.DtSolMat_Id)
                .Select(m => new
                {
                    m.Codigo,
                    Tipo = m.DtSolMat_TipoMaterial,
                    MaterialId = m.MatPri_Id ?? m.Tinta_Id ?? 0,
                    Nombre = m.MatPri_Id != null ? m.MatPri!.MatPri_Nombre : m.Tinta!.Tinta_Nombre,
                    UndMed_Id = m.MatPri_Id != null ? m.MatPri!.UndMed_Id : m.Tinta!.UndMed_Id,
                    Stock = m.MatPri_Id != null ? m.MatPri!.MatPri_Stock : m.Tinta!.Tinta_Stock,
                    Cantidad = m.DtSolMat_Cantidad
                })
                .ToListAsync(cancellationToken);

            var asignado = await _solicitudes.ObtenerAsignadoAsync([id], cancellationToken);
            var entregados = await _solicitudes.ObtenerAsignadoPorMaterialAsync(id, cancellationToken);
            var entregadoPorMaquina = await _solicitudes.ObtenerAsignadoPorMaquinaAsync(id, cancellationToken);
            var asignaciones = await _solicitudes.ObtenerAsignacionesAsync(id, cancellationToken);
            var reservas = (await _reservas.ObtenerReservasAsync(filas.Select(f => f.SubCatMP_Id).Distinct().ToList(), cancellationToken))
                .ToDictionary(r => r.SubCatMP_Id);

            var subcategorias = filas
                .GroupBy(f => f.SubCatMP_Id)
                .Select(g =>
                {
                    var codigos = g.Select(f => f.Codigo).ToHashSet();
                    var pedidos = solicitados.Where(m => codigos.Contains(m.Codigo)).ToList();
                    var entregadosSub = entregados.Where(e => e.SubCatMP_Id == g.Key).ToList();

                    // Materiales solicitados (en su orden) y luego los entregados que no se pidieron.
                    var materiales = pedidos
                        .GroupBy(m => (m.Tipo, m.MaterialId))
                        .Select(m =>
                        {
                            var primero = m.First();
                            var entregado = entregadosSub.FirstOrDefault(e => e.Tipo == m.Key.Tipo && e.MaterialId == m.Key.MaterialId)?.Entregado ?? 0m;
                            return new MaterialDetalleSolicitud(m.Key.Tipo, m.Key.MaterialId, primero.Nombre.Trim(), primero.UndMed_Id, primero.Stock, m.Sum(x => x.Cantidad), entregado, false);
                        })
                        .ToList();
                    materiales.AddRange(entregadosSub
                        .Where(e => !materiales.Any(m => m.TipoMaterial == e.Tipo && m.MaterialId == e.MaterialId))
                        .Select(e => new MaterialDetalleSolicitud(e.Tipo, e.MaterialId, e.Nombre.Trim(), e.UndMed_Id, e.Stock, 0m, e.Entregado, pedidos.Count > 0)));

                    var solicitado = g.Sum(f => f.DtSolMpExt_Cantidad);
                    var entregadoSub = asignado.GetValueOrDefault((id, g.Key));
                    reservas.TryGetValue(g.Key, out var reserva);
                    return new
                    {
                        Codigo = g.Min(f => f.Codigo),
                        SubCatMP_Id = g.Key,
                        Nombre = g.First().SubCatMP_Nombre.Trim(),
                        g.First().UndMed_Id,
                        Solicitado = solicitado,
                        Entregado = entregadoSub,
                        Faltante = Math.Max(0m, solicitado - entregadoSub),
                        Stock = reserva?.Existencia ?? 0m,
                        Disponible = reserva?.Disponible ?? 0m,
                        ConMateriales = pedidos.Count > 0,
                        Materiales = materiales
                    };
                })
                .ToList();

            return Ok(new
            {
                Id = id,
                OT = encabezado.SolMpExt_OT,
                encabezado.Proceso_Id,
                encabezado.Proceso,
                encabezado.Estado_Id,
                encabezado.Estado,
                encabezado.Usua_Id,
                encabezado.Usuario,
                Fecha = encabezado.SolMpExt_Fecha,
                Hora = encabezado.SolMpExt_Hora,
                FechaEntrega = encabezado.SolMpExt_FechaEstimadaEntrega,
                Observacion = encabezado.SolMpExt_Observacion,
                // Cancelación (null si no está cancelada).
                MotivoCancelacion = encabezado.SolMpExt_MotivoCancelacion,
                FechaCancelacion = encabezado.SolMpExt_FechaCancelacion,
                encabezado.UsuarioCancela,
                // Por máquina: su porcentaje, lo que le toca del total solicitado (informativo) y lo ya entregado a ella.
                Maquinas = maquinas.Select(m => new
                {
                    m.Maquina,
                    m.Porcentaje,
                    Objetivo = Math.Round(subcategorias.Sum(s => s.Solicitado) * m.Porcentaje / 100m, 2),
                    Entregado = entregadoPorMaquina.GetValueOrDefault(m.Maquina)
                }),
                Subcategorias = subcategorias,
                // Asignaciones de la solicitud (para "Ver asignación PDF" en Movimientos).
                Asignaciones = asignaciones
            });
        }

        /// <summary>
        /// Cancela una solicitud. Quien gestiona solicitudes (policy GestionarSolicitudMaterial) cancela las pendientes y
        /// parciales; el solicitante solo puede cancelar la suya mientras esté pendiente. Ya no se finaliza una solicitud
        /// sola: se completa al recibir todo y se finaliza con su OT (ot/{ot}/finalizar).
        /// La actualización es condicional al estado leído, así no se cambia una solicitud que otra operación modificó.
        /// </summary>
        /// <param name="id">Identificador de la solicitud.</param>
        /// <param name="request">Estado nuevo.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con el estado nuevo; 400, 403, 404 o 409 según la regla que no se cumple.</returns>
        [HttpPut("{id:long}/estado")]
        public async Task<IActionResult> CambiarEstado(long id, CambiarEstadoSolicitudRequest request, CancellationToken cancellationToken)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();
            if (id == EstadosSolicitudMaterial.SolicitudGenericaId)
                return Conflict("La solicitud genérica no se puede finalizar ni cancelar.");

            var actual = await _context.Solicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(s => s.SolMpExt_Id == id)
                .Select(s => new { s.Estado_Id, s.Usua_Id })
                .FirstOrDefaultAsync(cancellationToken);
            if (actual == null) return NotFound($"La solicitud N° {id} no existe.");

            var esGestor = (await _authorization.AuthorizeAsync(User, "GestionarSolicitudMaterial")).Succeeded;
            var esDueno = actual.Usua_Id == usuarioId;
            var error = ReglasSolicitudMaterial.ValidarCambioEstado(request.Estado, actual.Estado_Id, esGestor, esDueno);
            if (error != null) return StatusCode(error.Value.StatusCode, error.Value.Mensaje);

            var motivo = request.Motivo?.Trim();
            if (string.IsNullOrEmpty(motivo))
                return BadRequest("Debe indicar el motivo de la cancelación.");

            // Se guarda quién canceló, cuándo y por qué.
            var estadoLeido = actual.Estado_Id;
            var ahora = DateTime.Now;
            var actualizadas = await _context.Solicitud_MatPrimaExtrusion
                .Where(s => s.SolMpExt_Id == id && s.Estado_Id == estadoLeido)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.Estado_Id, request.Estado)
                    .SetProperty(x => x.SolMpExt_MotivoCancelacion, motivo)
                    .SetProperty(x => x.SolMpExt_FechaCancelacion, ahora)
                    .SetProperty(x => x.Usua_IdCancela, usuarioId), cancellationToken);
            if (actualizadas == 0)
                return Conflict($"La solicitud N° {id} cambió de estado mientras tanto. Consúltela de nuevo.");

            return Ok(new { SolMpExt_Id = id, Estado_Id = request.Estado });
        }

        /// <summary>
        /// Obtiene el avance y el estado de la solicitud de la OT (todas sus solicitudes): kg de la OT, tope con el
        /// adicional, kg solicitados y asignados, solicitudes por estado y qué acciones permite.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El resumen; 404 si la OT no tiene solicitudes.</returns>
        [HttpGet("ot/{ot:long}")]
        public async Task<IActionResult> ObtenerResumenOT(long ot, CancellationToken cancellationToken)
        {
            var resumen = await _solicitudes.ObtenerResumenOTAsync(ot, cancellationToken);
            return resumen == null ? NotFound($"La OT {ot} no tiene solicitudes de material.") : Ok(resumen);
        }

        /// <summary>
        /// Guarda los kg de la OT tomados de BagPro y recalcula su estado (por ejemplo, para las OT anteriores a esta
        /// regla, que no tienen sus kg). Solo aplica a OT que ya tienen solicitudes.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="request">Kg de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El resumen actualizado; 404 si la OT no tiene solicitudes.</returns>
        [HttpPut("ot/{ot:long}/kg")]
        public Task<IActionResult> RegistrarKgOT(long ot, RegistrarKgOTRequest request, CancellationToken cancellationToken) =>
            EnTransaccionOTAsync(async () =>
            {
                if (await _solicitudes.ObtenerEstadoOTAsync(ot, cancellationToken) == null)
                    return NotFound($"La OT {ot} no tiene solicitudes de material.");
                await _solicitudes.RegistrarKgOTAsync(ot, request.KgOT, cancellationToken);
                return null;
            }, ot, cancellationToken);

        /// <summary>
        /// Completa a mano la solicitud de la OT después de que lo asignado llegó a los kg de la OT (umbral) y completa
        /// sus solicitudes activas de materia prima. No se puede deshacer. Para administrador, asignador y solicitante.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El resumen actualizado; 404 o 409 según la regla que no se cumple.</returns>
        [HttpPut("ot/{ot:long}/completar")]
        [Authorize(Policy = "GestionarSolicitudOT")]
        public Task<IActionResult> CompletarOT(long ot, CancellationToken cancellationToken) =>
            AccionOTAsync(ot, usuario => _solicitudes.CompletarOTAsync(ot, usuario, cancellationToken), cancellationToken);

        /// <summary>
        /// Finaliza la solicitud de la OT (cierra la asignación de materiales) cuando está completada y no tiene
        /// solicitudes activas. Si la OT aún no produce, queda ASIGNADA en Estados_ProcesosOT.
        /// Para administrador, asignador y solicitante.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El resumen actualizado; 404 o 409 según la regla que no se cumple.</returns>
        [HttpPut("ot/{ot:long}/finalizar")]
        [Authorize(Policy = "GestionarSolicitudOT")]
        public Task<IActionResult> FinalizarOT(long ot, CancellationToken cancellationToken) =>
            AccionOTAsync(ot, usuario => _solicitudes.FinalizarOTAsync(ot, usuario, cancellationToken), cancellationToken);

        /// <summary>
        /// Verifica la solicitud de la OT finalizada: el solicitante comprobó físicamente lo recibido.
        /// Para administrador y solicitante.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El resumen actualizado; 404 o 409 según la regla que no se cumple.</returns>
        [HttpPut("ot/{ot:long}/verificar")]
        [Authorize(Policy = "VerificarSolicitudMaterial")]
        public Task<IActionResult> VerificarOT(long ot, CancellationToken cancellationToken) =>
            AccionOTAsync(ot, usuario => _solicitudes.VerificarOTAsync(ot, usuario, cancellationToken), cancellationToken);

        /// <summary>
        /// Ejecuta una acción de la solicitud de la OT con el usuario del token y responde el resumen o el error.
        /// </summary>
        private async Task<IActionResult> AccionOTAsync(long ot, Func<long, Task<ResultadoAccionOT>> accion, CancellationToken cancellationToken)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();

            return await EnTransaccionOTAsync(async () =>
            {
                var resultado = await accion(usuarioId);
                return resultado.StatusCode == StatusCodes.Status200OK ? null : StatusCode(resultado.StatusCode, resultado.Mensaje);
            }, ot, cancellationToken);
        }

        /// <summary>
        /// Ejecuta una operación sobre la solicitud de la OT en una transacción serializable (para que no se cruce con
        /// una asignación) y, si no devolvió un error, responde el resumen actualizado de la OT.
        /// </summary>
        /// <param name="operacion">Operación; devuelve la respuesta de error o null si se aplicó.</param>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        private Task<IActionResult> EnTransaccionOTAsync(Func<Task<IActionResult?>> operacion, long ot, CancellationToken cancellationToken)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return strategy.ExecuteAsync<IActionResult>(async () =>
            {
                _context.ChangeTracker.Clear();
                await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
                var error = await operacion();
                if (error != null) return error;
                await transaction.CommitAsync(cancellationToken);
                return Ok(await _solicitudes.ObtenerResumenOTAsync(ot, cancellationToken));
            });
        }

        /// <summary>
        /// Busca la primera máquina (por su número) que no pertenece al proceso.
        /// </summary>
        /// <returns>El número de la máquina inválida, o null si todas pertenecen.</returns>
        private async Task<int?> MaquinaFueraDelProcesoAsync(IReadOnlyCollection<int> maquinas, string proceso, CancellationToken cancellationToken)
        {
            if (maquinas.Count == 0) return null;
            var numeros = maquinas.ToList();
            var validas = await _context.Maquinas
                .AsNoTracking()
                .Where(m => m.Proceso_Id == proceso && numeros.Contains(m.Maq_Numero))
                .Select(m => m.Maq_Numero)
                .Distinct()
                .ToListAsync(cancellationToken);
            var invalidas = numeros.Where(m => !validas.Contains(m)).ToList();
            return invalidas.Count == 0 ? null : invalidas[0];
        }

        /// <summary>
        /// Comprueba que los materiales existan, sean del tipo del proceso (materias primas en Extrusión y tintas en
        /// los demás) y pertenezcan a la subcategoría en la que se enviaron.
        /// </summary>
        /// <returns>Mensaje de error, o null si todos son válidos.</returns>
        private async Task<string?> ValidarMaterialesCatalogoAsync(
            string proceso,
            IReadOnlyCollection<SolicitudExtrusionDetalleRequest> detalles,
            IReadOnlyDictionary<int, string> nombres,
            CancellationToken cancellationToken)
        {
            var materiales = detalles.SelectMany(d => (d.Materiales ?? []).Select(m => (Subcategoria: d.SubCatMP_Id, Material: m))).ToList();
            if (materiales.Count == 0) return null;

            var tipo = ReglasSolicitudMaterial.TipoMaterialDelProceso(proceso);
            if (materiales.Any(x => x.Material.TipoMaterial != tipo))
                return tipo == TipoMaterialInventario.MateriaPrima
                    ? "En Extrusión solo se solicitan materias primas."
                    : $"En el proceso {proceso} solo se solicitan tintas.";

            var ids = materiales.Select(x => x.Material.MaterialId).Distinct().ToList();
            var catalogo = tipo == TipoMaterialInventario.MateriaPrima
                ? await _context.Materias_Primas
                    .AsNoTracking()
                    .Where(m => ids.Contains(m.MatPri_Id))
                    .Select(m => new { Id = m.MatPri_Id, Nombre = m.MatPri_Nombre, m.SubCatMP_Id })
                    .ToDictionaryAsync(m => m.Id, cancellationToken)
                : await _context.Tintas
                    .AsNoTracking()
                    .Where(t => ids.Contains(t.Tinta_Id))
                    .Select(t => new { Id = t.Tinta_Id, Nombre = t.Tinta_Nombre, t.SubCatMP_Id })
                    .ToDictionaryAsync(t => t.Id, cancellationToken);

            foreach (var (subcategoria, material) in materiales)
            {
                if (!catalogo.TryGetValue(material.MaterialId, out var encontrado))
                    return $"El material {material.MaterialId} no existe.";
                if (encontrado.SubCatMP_Id != subcategoria)
                    return $"El material {encontrado.Nombre.Trim()} no pertenece a la subcategoría {nombres[subcategoria].Trim()}.";
            }
            return null;
        }

        /// <summary>
        /// Guarda los materiales de cada subcategoría editada en su fila principal. Las subcategorías que no envían
        /// materiales (formato anterior) conservan los suyos solo si siguen sumando la cantidad de la subcategoría.
        /// Los materiales de las subcategorías quitadas ya se borraron en cascada con su fila.
        /// </summary>
        /// <param name="solicitudId">Solicitud editada.</param>
        /// <param name="detalles">Subcategorías finales de la petición.</param>
        /// <param name="filas">Filas de detalle guardadas (existentes y agregadas), ya con su código.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        private async Task ReemplazarMaterialesAsync(
            long solicitudId,
            IReadOnlyCollection<SolicitudExtrusionDetalleRequest> detalles,
            IEnumerable<DetSolicitud_MatPrimaExtrusion> filas,
            CancellationToken cancellationToken)
        {
            var principal = filas
                .GroupBy(f => f.SubCatMP_Id)
                .ToDictionary(g => g.Key, g => g.Min(f => f.Codigo));
            var sumaPorCodigo = await _context.DetSolicitud_Materiales
                .AsNoTracking()
                .Where(m => m.SolMpExt_Id == solicitudId)
                .GroupBy(m => m.Codigo)
                .Select(g => new { Codigo = g.Key, Suma = g.Sum(x => x.DtSolMat_Cantidad) })
                .ToDictionaryAsync(g => g.Codigo, g => g.Suma, cancellationToken);

            var reemplazar = new List<long>();
            var nuevos = new List<DetSolicitud_Materiales>();
            foreach (var detalle in detalles)
            {
                var codigo = principal[detalle.SubCatMP_Id];
                if (detalle.Materiales == null)
                {
                    if (sumaPorCodigo.TryGetValue(codigo, out var suma) && suma != detalle.Cantidad) reemplazar.Add(codigo);
                    continue;
                }
                reemplazar.Add(codigo);
                nuevos.AddRange(NuevosMateriales(solicitudId, codigo, detalle.Materiales));
            }

            if (reemplazar.Count > 0)
                await _context.DetSolicitud_Materiales
                    .Where(m => m.SolMpExt_Id == solicitudId && reemplazar.Contains(m.Codigo))
                    .ExecuteDeleteAsync(cancellationToken);
            _context.DetSolicitud_Materiales.AddRange(nuevos);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>Reemplaza las máquinas de la solicitud por las de la edición.</summary>
        private async Task ReemplazarMaquinasAsync(long solicitudId, IEnumerable<MaquinaSolicitud> maquinas, CancellationToken cancellationToken)
        {
            await _context.Solicitud_Maquinas.Where(m => m.SolMpExt_Id == solicitudId).ExecuteDeleteAsync(cancellationToken);
            _context.Solicitud_Maquinas.AddRange(maquinas.Select(m => NuevaMaquina(solicitudId, m)));
            await _context.SaveChangesAsync(cancellationToken);
        }

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
            return ReglasSolicitudMaterial.ValidarMateriales(detalles);
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

        /// <summary>Crea los materiales de una subcategoría de la solicitud.</summary>
        private static IEnumerable<DetSolicitud_Materiales> NuevosMateriales(long solicitudId, long codigo, IEnumerable<SolicitudMaterialRequest>? materiales) =>
            (materiales ?? []).Select(m => new DetSolicitud_Materiales
            {
                Codigo = codigo,
                SolMpExt_Id = solicitudId,
                DtSolMat_TipoMaterial = m.TipoMaterial,
                MatPri_Id = m.TipoMaterial == TipoMaterialInventario.MateriaPrima ? m.MaterialId : null,
                Tinta_Id = m.TipoMaterial == TipoMaterialInventario.Tinta ? m.MaterialId : null,
                DtSolMat_Cantidad = m.Cantidad
            });

        /// <summary>Crea una máquina de la solicitud con su porcentaje.</summary>
        private static Solicitud_Maquinas NuevaMaquina(long solicitudId, MaquinaSolicitud maquina) => new()
        {
            SolMpExt_Id = solicitudId,
            SolMaq_Maquina = maquina.Maquina,
            SolMaq_Porcentaje = maquina.Porcentaje
        };

        /// <summary>Material de una subcategoría en el detalle de la solicitud.</summary>
        /// <param name="TipoMaterial">Materia prima o tinta.</param>
        /// <param name="MaterialId">Identificador del material.</param>
        /// <param name="Nombre">Nombre del material.</param>
        /// <param name="UndMed_Id">Unidad de medida.</param>
        /// <param name="Stock">Stock actual del material.</param>
        /// <param name="Solicitado">Cantidad solicitada (0 si se entregó sin pedirse).</param>
        /// <param name="Entregado">Cantidad asignada a la solicitud.</param>
        /// <param name="EsSustituto">Se entregó en lugar de los materiales solicitados de la subcategoría.</param>
        private sealed record MaterialDetalleSolicitud(
            TipoMaterialInventario TipoMaterial,
            long MaterialId,
            string Nombre,
            string UndMed_Id,
            decimal Stock,
            decimal Solicitado,
            decimal Entregado,
            bool EsSustituto);

        /// <summary>Subcategoría cuya reserva adicional supera la disponibilidad informativa.</summary>
        /// <param name="SubCatMP_Id">Subcategoría.</param>
        /// <param name="Nombre">Nombre de la subcategoría.</param>
        /// <param name="Solicitado">Cantidad total solicitada.</param>
        /// <param name="Aumento">Reserva adicional que genera la operación.</param>
        /// <param name="Disponible">Disponibilidad informativa antes de la operación.</param>
        private sealed record AdvertenciaDisponibilidad(int SubCatMP_Id, string Nombre, decimal Solicitado, decimal Aumento, decimal Disponible);
    }
}
