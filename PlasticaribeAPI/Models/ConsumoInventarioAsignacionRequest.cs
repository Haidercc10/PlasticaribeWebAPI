using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Tipo de material que se descuenta del inventario.
    /// </summary>
    public enum TipoMaterialInventario
    {
        /// <summary>Materia prima.</summary>
        MateriaPrima = 1,
        /// <summary>Tinta.</summary>
        Tinta = 2
    }

    /// <summary>
    /// Solicitud para consumir el inventario de una asignación de materia prima o tintas.
    /// </summary>
    public sealed class ConsumoInventarioAsignacionRequest
    {
        [Range(1, long.MaxValue)]
        public long AsignacionId { get; set; }

        [Range(1, int.MaxValue)]
        public int ProductoId { get; set; }

        [Range(typeof(decimal), "0", "9999999999999999")]
        public decimal CantidadPedidaOT { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string UnidadMedidaOT { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public List<ConsumoInventarioLoteRequest> Lotes { get; set; } = [];
    }

    /// <summary>
    /// Lote y cantidad a consumir dentro de una asignación.
    /// </summary>
    public sealed class ConsumoInventarioLoteRequest
    {
        public TipoMaterialInventario TipoMaterial { get; set; }

        [Range(1, long.MaxValue)]
        public long MaterialId { get; set; }

        [Range(1, int.MaxValue)]
        public int LoteId { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999")]
        public decimal Cantidad { get; set; }
    }
}
