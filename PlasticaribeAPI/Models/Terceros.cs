using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Terceros
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Tercero_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Tercero_Id { get; set; }
        public string TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion? TipoIdentificacion { get; set; }
        public string Tercero_Nombre { get; set; }
        public string Tercero_Ciudad { get; set; }
        public string Tercero_Telefono { get; set; }
        public string Tercero_Email { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Tercero_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Tercero_Hora { get; set; }
    }
}
