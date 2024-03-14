using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class EventosCalendario
    {
        [Key]
        public long EventoCal_Id { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime EventoCal_FechaCreacion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string EventoCal_HoraCreacion { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string EventoCal_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string EventoCal_Descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime EventoCal_FechaInicial { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string EventoCal_HoraInicial { get; set; }

        [Column(TypeName = "date")]
        public DateTime EventoCal_FechaFinal { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string EventoCal_HoraFinal { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string EventoCal_Visibilidad { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
