using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlasticaribeAPI.Models;
using PlasticaribeAPI.Service;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventarioTransaccionalController : ControllerBase
    {
        private readonly ConsumoInventarioAsignacionService _consumoService;
        private readonly AsignacionMateriaPrimaService _asignacionService;
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Inicializa el controlador con los servicios de inventario transaccional.
        /// </summary>
        /// <param name="consumoService">Servicio que ejecuta el consumo transaccional.</param>
        /// <param name="asignacionService">Servicio que registra asignaciones completas.</param>
        /// <param name="authorizationService">Servicio para evaluar la policy de exceso de límite de OT.</param>
        public InventarioTransaccionalController(
            ConsumoInventarioAsignacionService consumoService,
            AsignacionMateriaPrimaService asignacionService,
            IAuthorizationService authorizationService)
        {
            _consumoService = consumoService;
            _asignacionService = asignacionService;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Consume el inventario de una asignación (stock, lotes y salidas) en una sola transacción.
        /// </summary>
        /// <param name="request">Asignación, datos de la OT y lotes a consumir.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con los movimientos creados; 400, 404 o 409 según la validación fallida.</returns>
        [HttpPost("consumir-asignacion")]
        public async Task<IActionResult> ConsumirAsignacion(
            ConsumoInventarioAsignacionRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _consumoService.ConsumirAsync(request, cancellationToken);
            if (!result.Succeeded)
                return StatusCode(result.StatusCode, result.Message);

            return Ok(new { result.Message, result.MovementsCreated });
        }

        /// <summary>
        /// Registra en una sola transacción la asignación, sus detalles, el consumo de inventario por lotes (FIFO),
        /// el estado de la solicitud atendida y la autorización de exceso usada, si aplica.
        /// </summary>
        /// <param name="request">Encabezado y materiales de la asignación.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>
        /// 200 con la asignación creada; 400 o 404 si los datos no son válidos; 401 si el token no trae el usuario;
        /// 409 si hay un conflicto de inventario o de solicitud, o si el exceso de la OT requiere autorización
        /// (<c>requiereAutorizacion = true</c>).
        /// </returns>
        [HttpPost("registrar-asignacion")]
        public async Task<IActionResult> RegistrarAsignacion(
            RegistrarAsignacionRequest request,
            CancellationToken cancellationToken)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();

            var puedeExceder = (await _authorizationService.AuthorizeAsync(User, "ExcederLimiteOT")).Succeeded;
            var result = await _asignacionService.RegistrarAsync(request, usuarioId, puedeExceder, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
