using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class SolicitudesMP_OrdenesCompra
    {
        [Key]
        public long Codigo { get; set; }
        public Orden_Compra? Orden_Compra { get; set; }
        public long Oc_Id { get; set; }
        public Solicitud_MateriaPrima? Solicitud_MateriaPrima { get; set; }
        public long Solicitud_Id { get; set; }
    }
}
