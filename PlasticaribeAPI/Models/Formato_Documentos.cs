using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Formato_Documentos
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Codigo { get; set; }
        public string Version { get; set; }
        public string Vigencia { get; set; }
        public string Nombre_Reporte { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
