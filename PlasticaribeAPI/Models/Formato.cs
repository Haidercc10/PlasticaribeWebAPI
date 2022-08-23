using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Formato
    {
        [Key]
        public long Formato_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Formato_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string Formato_Descripcion { get; set; }
    }
}
