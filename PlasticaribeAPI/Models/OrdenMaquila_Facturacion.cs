using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class OrdenMaquila_Facturacion
    {
        [Key]
        public long Codigo { get; set; }
        public long OM_Id { get; set; }
        public Orden_Maquila? Orden_Maquila { get; set; }

        public long FacOM_Id { get; set; }
        public Facturacion_OrdenMaquila? FacOM { get; set; }
    }
}
