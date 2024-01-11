using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Pigmento
    {
        [Key]
        public int Pigmt_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Pigmt_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Pigmt_Descripcion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Pigmt_Hexadecimal { get; set; }
    }
}
