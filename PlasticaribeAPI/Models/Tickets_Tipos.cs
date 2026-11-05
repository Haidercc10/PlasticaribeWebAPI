using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Catálogo de tipos de ticket (Incidente, Solicitud, Requerimiento, Mejora, Otro).
    /// </summary>
    public class Tickets_Tipos
    {
        /// <summary>Id del tipo de ticket.</summary>
        [Key]
        public int TipoTicket_Id { get; set; }

        /// <summary>Nombre que se muestra en el formulario.</summary>
        [Column(TypeName = "varchar(50)")]
        public string TipoTicket_Nombre { get; set; } = string.Empty;

        /// <summary>Si es false no se ofrece al crear tickets.</summary>
        public bool TipoTicket_Activo { get; set; } = true;
    }
}
