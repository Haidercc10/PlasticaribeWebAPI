using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Turno
    {

        [Key]

        [Column(TypeName = "varchar(50)")]
        public string Turno_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Turno_Nombre { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string? Turno_Descripcion { get; set; }

    }
}
