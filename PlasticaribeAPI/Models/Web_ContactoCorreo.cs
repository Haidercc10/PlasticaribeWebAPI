using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Web_ContactoCorreo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Fecha_Envio { get; set; }
        public string Hora_Envio { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}