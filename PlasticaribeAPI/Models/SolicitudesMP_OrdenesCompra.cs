using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class SolicitudesMP_OrdenesCompra
    {
        [Key]
        public long Codigo { get; set; }
        public Orden_Compra? Orden_Compra { get; set; }
        public long Oc_Id { get; set; }
        public Solicitud_MateriaPrima? Solicitud_MateriaPrima { get; set; }
        public long Solicitud_Id { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
