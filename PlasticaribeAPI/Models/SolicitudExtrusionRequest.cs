using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Datos para registrar una solicitud de material de Extrusión con sus detalles.
    /// </summary>
    public sealed class SolicitudExtrusionRequest
    {
        [Range(1, long.MaxValue)]
        public long OrdenTrabajo { get; set; }

        /// <summary>
        /// Máquina única del formato anterior. Se usa solo si no se envía <see cref="Maquinas"/> (queda al 100%).
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Maquina { get; set; }

        /// <summary>
        /// Máquinas a las que va el material y el porcentaje de cada una; los porcentajes deben sumar 100.
        /// </summary>
        [MaxLength(20)]
        public List<SolicitudMaquinaRequest>? Maquinas { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string ProcesoId { get; set; } = string.Empty;

        public string? Observacion { get; set; }

        /// <summary>
        /// Fecha en que se necesita el material. Obligatoria.
        /// </summary>
        [Required(ErrorMessage = "Debe indicar la fecha estimada de entrega.")]
        public DateTime? FechaEstimadaEntrega { get; set; }

        /// <summary>
        /// Indica que el usuario vio la advertencia de disponibilidad informativa y decidió continuar.
        /// </summary>
        public bool ConfirmarSaldoNegativo { get; set; }

        /// <summary>
        /// Kg máximos que se pueden solicitar para la OT (kg de la OT más el adicional permitido).
        /// Obligatorio en Extrusión, donde las materias primas cuentan para el límite de la OT.
        /// </summary>
        [Range(typeof(decimal), "0", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal? LimiteKgOT { get; set; }

        /// <summary>
        /// Kg de la OT en BagPro, sin el adicional (umbral de la solicitud de la OT). Si no se envía se calcula desde
        /// <see cref="LimiteKgOT"/>.
        /// </summary>
        [Range(typeof(decimal), "0", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal? KgOT { get; set; }

        /// <summary>
        /// Indica que un usuario con la policy ExcederLimiteOT confirmó solicitar por encima del límite de la OT.
        /// </summary>
        public bool ConfirmarExcesoLimite { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public List<SolicitudExtrusionDetalleRequest> Detalles { get; set; } = [];
    }

    /// <summary>
    /// Datos para editar una solicitud de material activa. La OT y el proceso no cambian;
    /// los detalles enviados reemplazan a los guardados.
    /// </summary>
    public sealed class EditarSolicitudExtrusionRequest
    {
        /// <summary>
        /// Máquina única del formato anterior. Se usa solo si no se envía <see cref="Maquinas"/> (queda al 100%).
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Maquina { get; set; }

        /// <summary>
        /// Máquinas a las que va el material y el porcentaje de cada una; los porcentajes deben sumar 100.
        /// </summary>
        [MaxLength(20)]
        public List<SolicitudMaquinaRequest>? Maquinas { get; set; }

        public string? Observacion { get; set; }

        /// <summary>
        /// Fecha en que se necesita el material. Obligatoria.
        /// </summary>
        [Required(ErrorMessage = "Debe indicar la fecha estimada de entrega.")]
        public DateTime? FechaEstimadaEntrega { get; set; }

        /// <summary>
        /// Indica que el usuario vio la advertencia de disponibilidad informativa y decidió continuar.
        /// </summary>
        public bool ConfirmarSaldoNegativo { get; set; }

        /// <summary>
        /// Kg máximos que se pueden solicitar para la OT (kg de la OT más el adicional permitido).
        /// Obligatorio en Extrusión, donde las materias primas cuentan para el límite de la OT.
        /// </summary>
        [Range(typeof(decimal), "0", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal? LimiteKgOT { get; set; }

        /// <summary>
        /// Kg de la OT en BagPro, sin el adicional (umbral de la solicitud de la OT). Si no se envía se calcula desde
        /// <see cref="LimiteKgOT"/>.
        /// </summary>
        [Range(typeof(decimal), "0", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal? KgOT { get; set; }

        /// <summary>
        /// Indica que un usuario con la policy ExcederLimiteOT confirmó solicitar por encima del límite de la OT.
        /// </summary>
        public bool ConfirmarExcesoLimite { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public List<SolicitudExtrusionDetalleRequest> Detalles { get; set; } = [];
    }

    /// <summary>
    /// Subcategoría y cantidad solicitada dentro de una solicitud de Extrusión.
    /// </summary>
    public sealed class SolicitudExtrusionDetalleRequest
    {
        [Range(1, int.MaxValue)]
        public int SubCatMP_Id { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal Cantidad { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string UndMed_Id { get; set; } = string.Empty;

        /// <summary>
        /// Materiales de la subcategoría elegidos por el solicitante. Si se envían, <see cref="Cantidad"/> debe ser su suma.
        /// </summary>
        [MaxLength(100)]
        public List<SolicitudMaterialRequest>? Materiales { get; set; }
    }

    /// <summary>
    /// Material elegido dentro de una subcategoría de la solicitud.
    /// </summary>
    public sealed class SolicitudMaterialRequest
    {
        public TipoMaterialInventario TipoMaterial { get; set; }

        [Range(1, long.MaxValue)]
        public long MaterialId { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal Cantidad { get; set; }
    }

    /// <summary>
    /// Máquina de la solicitud y el porcentaje del material que le corresponde.
    /// </summary>
    public sealed class SolicitudMaquinaRequest
    {
        [Range(1, int.MaxValue)]
        public int Maquina { get; set; }

        [Range(typeof(decimal), "0.01", "100", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal Porcentaje { get; set; }
    }

    /// <summary>
    /// Cambio de estado manual de una solicitud: solo se admite cancelarla, con su motivo.
    /// </summary>
    public sealed class CambiarEstadoSolicitudRequest
    {
        [Range(1, int.MaxValue)]
        public int Estado { get; set; }

        /// <summary>Motivo de la cancelación (obligatorio, máximo 1000 caracteres).</summary>
        [MaxLength(1000)]
        public string? Motivo { get; set; }
    }

    /// <summary>
    /// Kg de una OT tomados de BagPro, para su solicitud de la OT.
    /// </summary>
    public sealed class RegistrarKgOTRequest
    {
        /// <summary>Kg de la OT, sin el adicional.</summary>
        [Range(typeof(decimal), "0.01", "9999999999999999", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal KgOT { get; set; }
    }

    /// <summary>
    /// Existencia, reserva informativa y disponibilidad de una subcategoría de material.
    /// </summary>
    /// <param name="SubCatMP_Id">Identificador de la subcategoría.</param>
    /// <param name="SubCatMP_Nombre">Nombre de la subcategoría.</param>
    /// <param name="Tipo">Indica si la subcategoría corresponde a materia prima o a tinta.</param>
    /// <param name="Existencia">Existencia física total de la subcategoría.</param>
    /// <param name="ReservaPendiente">Cantidad solicitada aún sin asignar en solicitudes activas.</param>
    /// <param name="Disponible">Existencia menos reserva pendiente; puede ser negativa.</param>
    public sealed record ReservaInventarioDto(
        int SubCatMP_Id,
        string SubCatMP_Nombre,
        TipoMaterialInventario Tipo,
        decimal Existencia,
        decimal ReservaPendiente,
        decimal Disponible);
}
