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

        /// <summary>
        /// Inicializa el controlador con el servicio de consumo de inventario.
        /// </summary>
        /// <param name="consumoService">Servicio que ejecuta el consumo transaccional.</param>
        public InventarioTransaccionalController(ConsumoInventarioAsignacionService consumoService)
        {
            _consumoService = consumoService;
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
    }
}
