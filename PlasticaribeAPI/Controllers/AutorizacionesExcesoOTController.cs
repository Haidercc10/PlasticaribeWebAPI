using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutorizacionesExcesoOTController : ControllerBase
    {
        private readonly dataContext _context;

        /// <summary>
        /// Inicializa el controlador con el contexto de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        public AutorizacionesExcesoOTController(dataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra una solicitud de autorización de exceso de OT a nombre del usuario autenticado.
        /// </summary>
        /// <param name="solicitud">OT, cantidad excedente en kg y motivo de la solicitud.</param>
        /// <returns>201 con la solicitud creada; 400 si falta el motivo; 401 si el token no trae el usuario.</returns>
        [HttpPost]
        public async Task<ActionResult> SolicitarAutorizacion(SolicitarAutorizacionExcesoOT solicitud)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();
            if (string.IsNullOrWhiteSpace(solicitud.Motivo))
                return BadRequest("El motivo de la solicitud es obligatorio.");

            var ahoraUtc = DateTime.UtcNow;
            var autorizacion = new AutorizacionExcesoOT
            {
                AutEx_OrdenTrabajo = solicitud.OrdenTrabajo,
                AutEx_CantidadKg = solicitud.CantidadExcesoKg,
                AutEx_Motivo = solicitud.Motivo.Trim(),
                Estado_Id = (int)EstadoAutorizacionExcesoOT.PENDIENTE,
                UsuarioSolicitaId = usuarioId,
                AutEx_FechaSolicitudUtc = ahoraUtc,
                AutEx_HoraSolicitudUtc = ahoraUtc.ToString("HH:mm:ss")
            };

            _context.Set<AutorizacionExcesoOT>().Add(autorizacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(ObtenerMisAutorizaciones),
                new { ot = autorizacion.AutEx_OrdenTrabajo },
                new { autorizacion.AutEx_Id, autorizacion.AutEx_OrdenTrabajo, autorizacion.AutEx_CantidadKg, autorizacion.AutEx_Motivo, autorizacion.Estado_Id, autorizacion.AutEx_FechaSolicitudUtc });
        }

        /// <summary>
        /// Lista las solicitudes de exceso de OT pendientes de respuesta. Solo para roles con la policy ExcederLimiteOT.
        /// </summary>
        /// <returns>Solicitudes pendientes ordenadas por fecha de solicitud.</returns>
        [HttpGet("pendientes")]
        [Authorize(Policy = "ExcederLimiteOT")]
        public async Task<ActionResult> ObtenerPendientes()
        {
            var pendientes = await _context.Set<AutorizacionExcesoOT>()
                .AsNoTracking()
                .Where(a => a.Estado_Id == (int)EstadoAutorizacionExcesoOT.PENDIENTE)
                .OrderBy(a => a.AutEx_FechaSolicitudUtc)
                .Select(a => new
                {
                    a.AutEx_Id,
                    a.AutEx_OrdenTrabajo,
                    a.AutEx_CantidadKg,
                    a.AutEx_Motivo,
                    a.UsuarioSolicitaId,
                    UsuarioSolicita = a.UsuarioSolicita!.Usua_Nombre,
                    a.AutEx_FechaSolicitudUtc
                })
                .ToListAsync();

            return Ok(pendientes);
        }

        /// <summary>
        /// Consulta las solicitudes de exceso de una OT hechas por el usuario autenticado.
        /// </summary>
        /// <param name="ot">Número de la orden de trabajo.</param>
        /// <returns>Solicitudes del usuario para la OT, con su estado y datos de respuesta o consumo; 401 si el token no trae el usuario.</returns>
        [HttpGet("mis-solicitudes/{ot:long}")]
        public async Task<ActionResult> ObtenerMisAutorizaciones(long ot)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();

            var solicitudes = await _context.Set<AutorizacionExcesoOT>()
                .AsNoTracking()
                .Where(a => a.AutEx_OrdenTrabajo == ot && a.UsuarioSolicitaId == usuarioId)
                .OrderByDescending(a => a.AutEx_FechaSolicitudUtc)
                .Select(a => new
                {
                    a.AutEx_Id,
                    a.AutEx_OrdenTrabajo,
                    a.AutEx_CantidadKg,
                    a.AutEx_Motivo,
                    a.Estado_Id,
                    a.AutEx_FechaSolicitudUtc,
                    a.UsuarioAutorizaId,
                    UsuarioAutoriza = a.UsuarioAutoriza == null ? null : a.UsuarioAutoriza.Usua_Nombre,
                    a.AutEx_FechaRespuestaUtc,
                    a.AsigMp_Id,
                    a.AutEx_FechaConsumoUtc
                })
                .ToListAsync();

            return Ok(solicitudes);
        }

        /// <summary>
        /// Aprueba una solicitud pendiente registrando quién y cuándo la autorizó. Solo para la policy ExcederLimiteOT.
        /// </summary>
        /// <param name="id">Identificador de la solicitud.</param>
        /// <returns>204 si se aprobó; 409 si no existe o ya fue resuelta; 401 si el token no trae el usuario.</returns>
        [HttpPut("{id:long}/aprobar")]
        [Authorize(Policy = "ExcederLimiteOT")]
        public async Task<IActionResult> Aprobar(long id)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();

            var ahoraUtc = DateTime.UtcNow;
            var horaUtc = ahoraUtc.ToString("HH:mm:ss");
            var actualizadas = await _context.Set<AutorizacionExcesoOT>()
                .Where(a => a.AutEx_Id == id && a.Estado_Id == (int)EstadoAutorizacionExcesoOT.PENDIENTE)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(a => a.Estado_Id, (int)EstadoAutorizacionExcesoOT.ACEPTADO)
                    .SetProperty(a => a.UsuarioAutorizaId, usuarioId)
                    .SetProperty(a => a.AutEx_FechaRespuestaUtc, ahoraUtc)
                    .SetProperty(a => a.AutEx_HoraRespuestaUtc, horaUtc));

            if (actualizadas == 0)
                return Conflict("La solicitud no existe o ya fue resuelta.");

            return NoContent();
        }

        /// <summary>
        /// Rechaza una solicitud pendiente registrando quién, cuándo y el motivo. Solo para la policy ExcederLimiteOT.
        /// </summary>
        /// <param name="id">Identificador de la solicitud.</param>
        /// <param name="rechazo">Motivo obligatorio del rechazo.</param>
        /// <returns>204 si se rechazó; 400 sin motivo; 409 si no existe o ya fue resuelta; 401 si el token no trae el usuario.</returns>
        [HttpPut("{id:long}/rechazar")]
        [Authorize(Policy = "ExcederLimiteOT")]
        public async Task<IActionResult> Rechazar(long id, RechazarAutorizacionExcesoOT rechazo)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();
            if (string.IsNullOrWhiteSpace(rechazo.Motivo))
                return BadRequest("El motivo del rechazo es obligatorio.");

            var ahoraUtc = DateTime.UtcNow;
            var horaUtc = ahoraUtc.ToString("HH:mm:ss");
            var actualizadas = await _context.Set<AutorizacionExcesoOT>()
                .Where(a => a.AutEx_Id == id && a.Estado_Id == (int)EstadoAutorizacionExcesoOT.PENDIENTE)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(a => a.Estado_Id, (int)EstadoAutorizacionExcesoOT.CANCELADO)
                    .SetProperty(a => a.UsuarioAutorizaId, usuarioId)
                    .SetProperty(a => a.AutEx_FechaRespuestaUtc, ahoraUtc)
                    .SetProperty(a => a.AutEx_HoraRespuestaUtc, horaUtc)
                    .SetProperty(a => a.AutEx_MotivoRespuesta, rechazo.Motivo.Trim()));

            if (actualizadas == 0)
                return Conflict("La solicitud no existe o ya fue resuelta.");

            return NoContent();
        }
    }

    /// <summary>Datos para solicitar una autorización de exceso de OT.</summary>
    public sealed class SolicitarAutorizacionExcesoOT
    {
        [Range(1, long.MaxValue)]
        public long OrdenTrabajo { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999")]
        public decimal CantidadExcesoKg { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string Motivo { get; set; } = string.Empty;
    }

    /// <summary>Datos para rechazar una autorización de exceso de OT.</summary>
    public sealed class RechazarAutorizacionExcesoOT
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Motivo { get; set; } = string.Empty;
    }
}
