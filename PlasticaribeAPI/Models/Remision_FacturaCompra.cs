using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Remision_FacturaCompra
    {
        [Key]
        public long Codigo { get; set; }

        public int Rem_Id { get; set; }
        public Remision? Remi { get; set; }

        public long Facco_Id { get; set; }
        public Factura_Compra? Faccom { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
