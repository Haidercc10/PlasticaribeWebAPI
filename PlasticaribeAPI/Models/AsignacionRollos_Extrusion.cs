using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class AsignacionRollos_Extrusion
    {
        [Key]
        public int AsgRollos_Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime AsgRollos_Fecha { get; set; }
        public string AsgRollos_Hora { get; set; }
        public string AsgRollos_Observacion { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
