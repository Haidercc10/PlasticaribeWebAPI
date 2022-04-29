using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Area
    {

        [Key]

        public long Area_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Area_Nombre { get; set; }

        [Column(TypeName = "text")]
        public String? Area_Descripcion { get; set; }


    }
}
