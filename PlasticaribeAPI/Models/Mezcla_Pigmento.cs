using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Mezcla_Pigmento
    {

        [Key]
        public int MezPigmto_Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string MezPigmto_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? MezPigmto_Descripcion { get; set; }
    }
}
