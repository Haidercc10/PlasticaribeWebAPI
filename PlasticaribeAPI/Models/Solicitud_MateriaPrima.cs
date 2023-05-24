using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Solicitud_MateriaPrima
    {
        [Key]
        public long Solicitud_Id { get; set; }
        public Usuario? Usuario { get; set; }
        public long Usua_Id { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Solicitud_Observacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime Solicitud_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Solicitud_Hora { get; set; }
        public Estado? Estado { get; set; } 
        public int Estado_Id { get; set; }
    }
}
