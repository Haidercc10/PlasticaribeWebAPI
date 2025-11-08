using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Usabilidad_Modulos
    {
        [Key]
        public long Usm_Id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Usm_Modulo { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime Usm_Fecha { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Usm_Hora { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Usm_Accion { get; set; }
    }
}
