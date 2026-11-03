using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Service;

namespace PlasticaribeAPI.Controllers
{
    /// <summary>
    /// Precargue de rollos de despacho guardado rollo a rollo, con estados En proceso → Pendiente (o Anulado).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PrecargueRollosController : ControllerBase
    {
        private readonly PrecargueRollosService _service;

        /// <summary>
        /// Inicializa el controlador con el servicio de precargue de rollos.
        /// </summary>
        public PrecargueRollosController(PrecargueRollosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Consulta un precargue con todos sus rollos.
        /// </summary>
        /// <param name="id">Id del precargue.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con el precargue; 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrecargue(long id, CancellationToken cancellationToken) =>
            ToActionResult(await _service.ObtenerAsync(id, cancellationToken));

        /// <summary>
        /// Lista los precargues en proceso creados por un usuario.
        /// </summary>
        /// <param name="usuario">Id del usuario.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con la lista, vacía si no tiene precargues en proceso.</returns>
        [HttpGet("enProceso/{usuario}")]
        public async Task<IActionResult> GetEnProceso(long usuario, CancellationToken cancellationToken) =>
            Ok(await _service.ListarEnProcesoAsync(usuario, cancellationToken));

        /// <summary>
        /// Agrega un rollo escaneado a un precargue en proceso; si no se envía Pcd_Id, crea el precargue con ese rollo.
        /// </summary>
        /// <param name="request">Precargue, cliente y rollo escaneado.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>201 si se creó el precargue; 200 si se agregó; 404 si el rollo no está disponible; 409 si hay conflicto.</returns>
        [HttpPost("agregarRollo")]
        public async Task<IActionResult> PostAgregarRollo(AgregarRolloPrecargueRequest request, CancellationToken cancellationToken) =>
            ToActionResult(await _service.AgregarRolloAsync(request, cancellationToken));

        /// <summary>
        /// Quita un rollo de un precargue en proceso y lo devuelve a disponible.
        /// </summary>
        /// <param name="id">Id del precargue.</param>
        /// <param name="rollo">Número de rollo/bulto de BagPro.</param>
        /// <param name="item">Item del rollo.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con los rollos restantes; 404 si el rollo no está en el precargue; 409 si el precargue no está en proceso.</returns>
        [HttpDelete("{id}/rollos/{rollo}/{item}")]
        public async Task<IActionResult> DeleteRollo(long id, long rollo, int item, CancellationToken cancellationToken) =>
            ToActionResult(await _service.QuitarRolloAsync(id, rollo, item, cancellationToken));

        /// <summary>
        /// Finaliza un precargue en proceso (pasa a pendiente).
        /// </summary>
        /// <param name="id">Id del precargue.</param>
        /// <param name="request">Usuario y observación final.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 si se finalizó; 404 si no existe; 409 si no está en proceso o no tiene rollos.</returns>
        [HttpPut("{id}/finalizar")]
        public async Task<IActionResult> PutFinalizar(long id, CambiarEstadoPrecargueRequest request, CancellationToken cancellationToken) =>
            ToActionResult(await _service.FinalizarAsync(id, request, cancellationToken));

        /// <summary>
        /// Anula un precargue en proceso y devuelve sus rollos a disponible.
        /// </summary>
        /// <param name="id">Id del precargue.</param>
        /// <param name="request">Usuario y motivo de la anulación.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con los rollos liberados; 404 si no existe; 409 si no está en proceso.</returns>
        [HttpPut("{id}/anular")]
        public async Task<IActionResult> PutAnular(long id, CambiarEstadoPrecargueRequest request, CancellationToken cancellationToken) =>
            ToActionResult(await _service.AnularAsync(id, request, cancellationToken));

        private IActionResult ToActionResult(PrecargueRollosResult result) =>
            result.Succeeded ? StatusCode(result.StatusCode, result.Data) : StatusCode(result.StatusCode, result.Message);
    }
}
