using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.DTOs
{
    /// <summary>
    /// Datos que envía el trabajador al crear un ticket (multipart/form-data).
    /// El usuario creador se toma del token, nunca del formulario.
    /// </summary>
    public class CrearTicketDto
    {
        /// <summary>Asunto o título corto.</summary>
        [Required, MaxLength(150)]
        public string Asunto { get; set; } = string.Empty;

        /// <summary>Id de Tickets_Tipos.</summary>
        [Required]
        public int TipoTicketId { get; set; }

        /// <summary>Descripción del problema.</summary>
        [Required]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>Imágenes .png / .jpg opcionales.</summary>
        public List<IFormFile>? Imagenes { get; set; }

        /// <summary>
        /// Solo soporte: usuario a cuyo nombre se crea el ticket. Si es null el ticket queda a nombre de quien lo crea.
        /// </summary>
        public long? ReportanteId { get; set; }
    }

    /// <summary>
    /// Cambio de estado de un ticket con su comentario.
    /// </summary>
    public class CambiarEstadoTicketDto
    {
        /// <summary>Estado destino (ver EstadoTicket).</summary>
        [Required]
        public int EstadoId { get; set; }

        /// <summary>Comentario que queda en el historial.</summary>
        public string? Comentario { get; set; }
    }

    /// <summary>
    /// Comentario que se agrega al historial sin cambiar el estado.
    /// </summary>
    public class ComentarTicketDto
    {
        /// <summary>Texto del comentario.</summary>
        [Required]
        public string Comentario { get; set; } = string.Empty;
    }
}
