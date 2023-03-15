using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Remision_FacturaCompra
    {
        [Key]
        public long Codigo { get; set; }

        public int Rem_Id { get; set; }
        public Remision? Remi { get; set; }

        public long Facco_Id { get; set; }
        public Factura_Compra? Faccom { get; set; } 
    }
}
