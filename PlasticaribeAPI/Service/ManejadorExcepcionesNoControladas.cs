using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Manejador global (F2-12) que SOLO da formato a los errores no controlados: responde 500 con ProblemDetails
    /// y un identificador de seguimiento, sin traza ni datos internos. No reclasifica excepciones (los 409/422/502
    /// de las OT los decide <see cref="OrdenTrabajoCompletaService"/>), así que los demás controladores conservan sus códigos.
    /// </summary>
    public sealed class ManejadorExcepcionesNoControladas : IExceptionHandler
    {
        private readonly ILogger<ManejadorExcepcionesNoControladas> _logger;

        /// <summary>
        /// Inicializa el manejador.
        /// </summary>
        /// <param name="logger">Registro donde queda el detalle del error.</param>
        public ManejadorExcepcionesNoControladas(ILogger<ManejadorExcepcionesNoControladas> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Registra la excepción y escribe una respuesta 500 en formato ProblemDetails.
        /// </summary>
        /// <param name="httpContext">Contexto de la petición.</param>
        /// <param name="exception">Excepción no controlada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>true: la respuesta quedó escrita.</returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Error no controlado en {Metodo} {Ruta} (traceId {TraceId}).",
                httpContext.Request.Method, httpContext.Request.Path, httpContext.TraceIdentifier);

            var problema = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error interno del servidor.",
                Detail = $"Ocurrió un error inesperado. Si persiste, informe este código a soporte: {httpContext.TraceIdentifier}.",
            };
            problema.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problema, (System.Text.Json.JsonSerializerOptions?)null, "application/problem+json", cancellationToken);
            return true;
        }
    }
}
