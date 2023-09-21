using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetallePedido_Mantenimiento
    {
        [Key]
        public long DtPedMtto_Codigo { get; set; }


        public long PedMtto_Id { get; set; }
        public Pedido_Mantenimiento? PedidoMtto { get; set; }


        public long Actv_Id { get; set; }
        public Activo? Act { get; set; }


        public int TpMtto_Id { get; set; }
        public Tipo_Mantenimiento? Tipo_Mtto { get; set; }


        [Column(TypeName = "date")]
        public DateTime DtPedMtto_FechaFalla { get; set; }

    }
}
