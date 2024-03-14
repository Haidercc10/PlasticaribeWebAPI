using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Remision_OrdenCompra
    {
        [Key]
        public long Codigo { get; set; }

        public long Oc_Id { get; set; }
        public Orden_Compra? Orden_Compra { get; set; }

        public int Rem_Id { get; set; }
        public Remision? Remision { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
