using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Falla_Tecnica
    {

        [Key]
        public int Falla_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Falla_Nombre { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string Falla_Descripcion { get; set; }


        public int TipoFalla_Id { get; set; }
        public Tipo_FallaTecnica? TipoFallaTecnica { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Fecha_Creacion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Hora_Creacion { get; set; }

    }
}
