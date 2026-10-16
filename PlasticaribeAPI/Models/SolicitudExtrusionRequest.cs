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

        [Range(0, int.MaxValue)]
        public int Maquina { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string ProcesoId { get; set; } = string.Empty;

        public string? Observacion { get; set; }

        public DateTime? FechaEstimadaEntrega { get; set; }

        /// <summary>
        /// Indica que el usuario vio la advertencia de disponibilidad informativa y decidió continuar.
        /// </summary>
        public bool ConfirmarSaldoNegativo { get; set; }

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

        [Range(typeof(decimal), "0.01", "9999999999999999")]
        public decimal Cantidad { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string UndMed_Id { get; set; } = string.Empty;
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
