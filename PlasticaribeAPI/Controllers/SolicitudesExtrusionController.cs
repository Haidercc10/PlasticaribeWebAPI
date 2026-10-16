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

        /// <summary>
        /// Inicializa el controlador con el contexto de datos y el servicio de reservas.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        /// <param name="reservas">Servicio que calcula las reservas informativas.</param>
        public SolicitudesExtrusionController(dataContext context, ReservasInventarioService reservas)
        {
            _context = context;
            _reservas = reservas;
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
        /// Registra en una sola transacción una solicitud de material de Extrusión y sus detalles.
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

            if (request.Detalles.Any(d => decimal.Round(d.Cantidad, 2) != d.Cantidad))
                return BadRequest("Las cantidades deben tener máximo dos decimales.");
            if (request.Detalles.Select(d => d.SubCatMP_Id).Distinct().Count() != request.Detalles.Count)
                return BadRequest("No se puede repetir una subcategoría en la misma solicitud.");

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IActionResult>(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

                if (!await _context.Procesos.AnyAsync(p => p.Proceso_Id == request.ProcesoId, cancellationToken))
                    return BadRequest("El proceso indicado no existe.");

                var unidades = request.Detalles.Select(d => d.UndMed_Id).Distinct().ToList();
                var unidadesValidas = await _context.Unidades_Medidas
                    .AsNoTracking()
                    .Where(u => unidades.Contains(u.UndMed_Id))
                    .CountAsync(cancellationToken);
                if (unidadesValidas != unidades.Count)
                    return BadRequest("Una o más unidades de medida no existen.");

                var ids = request.Detalles.Select(d => d.SubCatMP_Id).ToList();
                var nombres = await _context.Subcategorias_MatPrima
                    .AsNoTracking()
                    .Where(s => ids.Contains(s.SubCatMP_Id))
                    .ToDictionaryAsync(s => s.SubCatMP_Id, s => s.SubCatMP_Nombre, cancellationToken);
                if (nombres.Count != ids.Count)
                    return BadRequest("Una o más subcategorías no existen.");

                var disponibilidad = (await _reservas.ObtenerReservasAsync(ids, cancellationToken))
                    .ToDictionary(r => r.SubCatMP_Id);
                var advertencias = request.Detalles
                    .Select(d =>
                    {
                        var disponible = disponibilidad.TryGetValue(d.SubCatMP_Id, out var r) ? r.Disponible : 0m;
                        return new { d.SubCatMP_Id, Nombre = nombres[d.SubCatMP_Id], Solicitado = d.Cantidad, Disponible = disponible };
                    })
                    .Where(x => x.Solicitado > x.Disponible)
                    .ToList();

                if (advertencias.Count > 0 && !request.ConfirmarSaldoNegativo)
                    return Conflict(new
                    {
                        RequiereConfirmacion = true,
                        Mensaje = "La cantidad solicitada supera la disponibilidad informativa de una o más subcategorías.",
                        Advertencias = advertencias
                    });

                var ahora = DateTime.Now;
                var solicitud = new Solicitud_MatPrimaExtrusion
                {
                    SolMpExt_OT = request.OrdenTrabajo,
                    SolMpExt_Maquina = request.Maquina,
                    Proceso_Id = request.ProcesoId,
                    Estado_Id = ReservasInventarioService.EstadoSolicitudPendiente,
                    Usua_Id = usuarioId,
                    SolMpExt_Fecha = ahora.Date,
                    SolMpExt_Hora = ahora.ToString("H:mm:ss", CultureInfo.InvariantCulture),
                    SolMpExt_Observacion = request.Observacion,
                    SolMpExt_FechaEstimadaEntrega = request.FechaEstimadaEntrega
                };
                _context.Solicitud_MatPrimaExtrusion.Add(solicitud);
                await _context.SaveChangesAsync(cancellationToken);

                _context.DetSolicitud_MatPrimaExtrusion.AddRange(request.Detalles.Select(d => new DetSolicitud_MatPrimaExtrusion
                {
                    SolMpExt_Id = solicitud.SolMpExt_Id,
                    SubCatMP_Id = d.SubCatMP_Id,
                    SubCatMP_Nombre = nombres[d.SubCatMP_Id],
                    DtSolMpExt_Cantidad = d.Cantidad,
                    DtSolMpExt_CantidadEntregada = 0,
                    DtSolMpExt_CantidadFaltante = d.Cantidad,
                    UndMed_Id = d.UndMed_Id
                }));
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return CreatedAtAction(
                    nameof(ObtenerReservas),
                    null,
                    new { solicitud.SolMpExt_Id, solicitud.Estado_Id, Advertencias = advertencias });
            });
        }
    }
}
