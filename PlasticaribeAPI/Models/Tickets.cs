using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Estados (tabla Estados) que puede tener un ticket.
    /// </summary>
    public enum EstadoTicket
    {
        CANCELADO = 4,
        ABIERTO = 28,
        EN_REVISION = 29,
        RESUELTO = 30
    }

    public class Tickets
    {
        [Key]
        public long Ticket_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Ticket_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Ticket_Hora { get; set; }
        /// <summary>Reportante: la persona que tiene el problema (dueño del ticket).</summary>
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        /// <summary>Usuario de soporte que registró el ticket a nombre del reportante; null si lo creó el mismo reportante.</summary>
        public long? UsuaRegistra_Id { get; set; }
        public Usuario? UsuarioRegistra { get; set; }

        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Ticket_Asunto { get; set; } = string.Empty;

        public int TipoTicket_Id { get; set; }
        public Tickets_Tipos? TipoTicket { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Ticket_Descripcion { get; set; }

        /// <summary>Fecha y hora (servidor) en que se creó el ticket.</summary>
        [Column(TypeName = "datetime2")]
        public DateTime Ticket_FechaCreacion { get; set; }

        /// <summary>Fecha y hora en que el ticket pasó a RESUELTO; null si no está resuelto.</summary>
        [Column(TypeName = "datetime2")]
        public DateTime? Ticket_FechaResolucion { get; set; }

        /// <summary>Obsoleto: solo para tickets anteriores a Tickets_Adjuntos.</summary>
        [Column(TypeName = "varchar(max)")]
        public string Ticket_RutaImagen { get; set; } = string.Empty;

        /// <summary>Obsoleto: nombres separados por '|' de tickets anteriores a Tickets_Adjuntos.</summary>
        [Column(TypeName = "varchar(max)")]
        public string Ticket_NombreImagen { get; set; } = string.Empty;
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
