using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Usuario
    {
        [Key]
        public int tpUsu_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String tpUsu_Nombre { get; set; }

        [Column(TypeName = "text")]
        public String tpUsu_Descripcion { get; set; }
    }
}
