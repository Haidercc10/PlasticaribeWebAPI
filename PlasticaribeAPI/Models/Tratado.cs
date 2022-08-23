using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tratado
    {
        [Key]
        public int Tratado_Id { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Tratado_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string Tratado_Descripcion { get; set; }
    }
}
