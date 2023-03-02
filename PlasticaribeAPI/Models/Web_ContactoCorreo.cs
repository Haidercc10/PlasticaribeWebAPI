using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Web_ContactoCorreo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
    }
}