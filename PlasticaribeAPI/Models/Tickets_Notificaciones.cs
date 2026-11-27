using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    /// <summary>Qué pasó en el ticket para generar la notificación.</summary>
    public enum TipoNotificacionTicket
    {
        /// <summary>Se creó el ticket (a soporte) o soporte lo registró a nombre del usuario (al reportante).</summary>
        NUEVO = 1,
        /// <summary>Cambió el estado del ticket (con o sin comentario).</summary>
        CAMBIO_ESTADO = 2,
        /// <summary>Se agregó un comentario sin cambiar el estado.</summary>
        COMENTARIO = 3
    }

    /// <summary>
    /// Novedad de un ticket para un usuario: una fila por destinatario y movimiento. Quien hace el movimiento no la recibe.
    /// El front la muestra como aviso en la esquina hasta que el usuario la marca como leída o abre el ticket.
    /// </summary>
    public class Tickets_Notificaciones
    {
        [Key]
        public long TicketNot_Id { get; set; }

        public long Ticket_Id { get; set; }
        public Tickets? Ticket { get; set; }

        /// <summary>Usuario que recibe la notificación.</summary>
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        /// <summary>Movimiento del historial que la generó; null en un ticket nuevo sin historial.</summary>
        public long? TicketRev_Id { get; set; }
        public Tickets_Revisados? Revision { get; set; }

        /// <summary>Usuario que hizo el movimiento (creó, cambió el estado o comentó).</summary>
        public long UsuaAutor_Id { get; set; }
        public Usuario? Autor { get; set; }

        public int TicketNot_Tipo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TicketNot_Fecha { get; set; }

        public bool TicketNot_Leida { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TicketNot_FechaLeida { get; set; }
    }
}
