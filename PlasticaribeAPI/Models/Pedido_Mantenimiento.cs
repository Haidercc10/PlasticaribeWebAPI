using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Pedido_Mantenimiento
    {
        [Key]
        public long PedMtto_Id { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }


        [Column(TypeName = "date")]
        public DateTime? PedMtto_Fecha { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? PedMtto_Hora { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? PedMtto_Observacion { get; set; }

    }
}
