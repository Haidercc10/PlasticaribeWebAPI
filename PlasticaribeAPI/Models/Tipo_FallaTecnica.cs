using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_FallaTecnica
    {

        [Key]
        public int TipoFalla_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string TipoFalla_Nombre { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string TipoFalla_Descripcion { get; set; }
    }
}
