using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tickets_Revisados
    {
        [Key]
        public long TicketRev_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime TicketRev_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string TicketRev_Hora { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string TicketRev_Descripcion { get; set; }
        public long Ticket_Id { get; set; }
        public Tickets? Tickets { get; set; }
    }
}
