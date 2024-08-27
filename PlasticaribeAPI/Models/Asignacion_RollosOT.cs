using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Asignacion_RollosOT
    {
        [Key]
        public long AsgRll_Id { get; set; }


        [Column(TypeName = "date")]
        public DateTime AsgRll_Fecha { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string AsgRll_Hora { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? AsgRll_Observacion { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
