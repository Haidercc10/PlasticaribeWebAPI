using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Datos para registrar en una sola operación una asignación de materia prima y tintas,
    /// con su consumo de inventario y la actualización de la solicitud asociada.
    /// </summary>
    public sealed class RegistrarAsignacionRequest
    {
        [Range(1, long.MaxValue)]
        public long OrdenTrabajo { get; set; }

        [Range(1, int.MaxValue)]
        public int Maquina { get; set; }

        [StringLength(200)]
        public string? Observacion { get; set; }

        [Required]
        public DateTime? FechaRealEntrega { get; set; }

        /// <summary>
        /// Solicitud de material que se atiende; si es nula se usa la solicitud genérica.
        /// </summary>
        public long? SolicitudId { get; set; }

        [Range(1, int.MaxValue)]
        public int ProductoId { get; set; }

        [Range(typeof(decimal), "0", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal CantidadPedidaOT { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string UnidadMedidaOT { get; set; } = string.Empty;

        /// <summary>
        /// Kg máximos de materia prima para la OT (kg de la OT más el adicional permitido), tomados de BagPro.
        /// </summary>
        [Range(typeof(decimal), "0", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal LimiteKgOT { get; set; }

        /// <summary>
        /// Indica que un usuario con permiso para exceder el límite vio la advertencia y decidió continuar.
        /// </summary>
        public bool ConfirmarExceso { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public List<RegistrarAsignacionMaterialRequest> Materiales { get; set; } = [];
    }

    /// <summary>
    /// Material y cantidad a asignar.
    /// </summary>
    public sealed class RegistrarAsignacionMaterialRequest
    {
        public TipoMaterialInventario TipoMaterial { get; set; }

        [Range(1, long.MaxValue)]
        public long MaterialId { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal Cantidad { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string ProcesoId { get; set; } = string.Empty;
    }

    /// <summary>
    /// Resultado del registro de una asignación.
    /// </summary>
    /// <param name="StatusCode">Código HTTP asociado al resultado.</param>
    /// <param name="Mensaje">Mensaje descriptivo.</param>
    /// <param name="AsignacionId">Asignación creada, si la operación fue exitosa.</param>
    /// <param name="EstadoSolicitud">Estado de la solicitud después de la asignación, si aplica.</param>
    /// <param name="RequiereAutorizacion">Indica que la asignación supera el límite de la OT y requiere autorización.</param>
    /// <param name="ExcesoKg">Kg que superan el límite de la OT, si aplica.</param>
    public sealed record RegistrarAsignacionResult(
        int StatusCode,
        string Mensaje,
        long? AsignacionId = null,
        int? EstadoSolicitud = null,
        bool RequiereAutorizacion = false,
        decimal? ExcesoKg = null)
    {
        /// <summary>Indica si la asignación quedó registrada.</summary>
        public bool Succeeded => StatusCode == 200;

        /// <summary>Crea un resultado por datos no válidos.</summary>
        /// <param name="mensaje">Motivo del rechazo.</param>
        /// <returns>Resultado con código 400.</returns>
        public static RegistrarAsignacionResult Invalid(string mensaje) => new(400, mensaje);

        /// <summary>Crea un resultado por recurso inexistente.</summary>
        /// <param name="mensaje">Descripción del recurso no encontrado.</param>
        /// <returns>Resultado con código 404.</returns>
        public static RegistrarAsignacionResult NotFound(string mensaje) => new(404, mensaje);

        /// <summary>Crea un resultado por conflicto con el estado actual de los datos.</summary>
        /// <param name="mensaje">Descripción del conflicto.</param>
        /// <returns>Resultado con código 409.</returns>
        public static RegistrarAsignacionResult Conflict(string mensaje) => new(409, mensaje);
    }
}
