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

        /// <summary>
        /// Inicializa el controlador con el contexto de datos y los servicios de reservas y solicitudes.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        /// <param name="reservas">Servicio que calcula las reservas informativas.</param>
        /// <param name="solicitudes">Reglas de estado y cantidades de las solicitudes.</param>
        public SolicitudesExtrusionController(dataContext context, ReservasInventarioService reservas, SolicitudesMaterialService solicitudes)
        {
            _context = context;
            _reservas = reservas;
            _solicitudes = solicitudes;
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
        /// 409 con las advertencias de disponibilidad si falta confirmar.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CrearSolicitud(SolicitudExtrusionRequest request, CancellationToken cancellationToken)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();

            var errorFormato = ValidarFormatoDetalles(request.Detalles);
            if (errorFormato != null) return BadRequest(errorFormato);

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IActionResult>(async () =>
            {
                _context.ChangeTracker.Clear();
                await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

                if (!await _context.Procesos.AnyAsync(p => p.Proceso_Id == request.ProcesoId, cancellationToken))
                    return BadRequest("El proceso indicado no existe.");

                var (errorCatalogos, nombres) = await ValidarCatalogosAsync(request.Detalles, cancellationToken);
                if (errorCatalogos != null) return BadRequest(errorCatalogos);

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
        /// 409 si la solicitud está cerrada, si se reduce por debajo de lo entregado o si falta confirmar la disponibilidad.
        /// </returns>
        [HttpPut("{id:long}")]
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
                    return Conflict($"La solicitud N° {id} está finalizada o cancelada y no se puede editar.");

                var (errorCatalogos, nombres) = await ValidarCatalogosAsync(request.Detalles, cancellationToken);
                if (errorCatalogos != null) return BadRequest(errorCatalogos);

                var cantidades = (await _solicitudes.ObtenerCantidadesAsync(id, cancellationToken)).ToDictionary(c => c.SubCatMP_Id);
                var nuevas = request.Detalles.ToDictionary(d => d.SubCatMP_Id);

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
