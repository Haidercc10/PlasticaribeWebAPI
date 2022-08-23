using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Rodillos
    {
        [Key]
        public int Rodillo_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Rodillo_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string Rodillo_Descripcion { get; set; }

    }
}
