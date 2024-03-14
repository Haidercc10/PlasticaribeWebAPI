using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tickets
    {
        [Key]
        public long Ticket_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Ticket_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Ticket_Hora { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Ticket_Descripcion { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Ticket_RutaImagen { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Ticket_NombreImagen { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
