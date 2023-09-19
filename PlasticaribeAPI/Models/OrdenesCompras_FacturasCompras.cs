using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class OrdenesCompras_FacturasCompras
    {

        [Key]
        public long Codigo { get; set; }

        public long Oc_Id { get; set; }
        public Orden_Compra? Orden_Compra { get; set; }

        public long Facco_Id { get; set; }
        public Factura_Compra? Facco { get; set; }
    }
}
