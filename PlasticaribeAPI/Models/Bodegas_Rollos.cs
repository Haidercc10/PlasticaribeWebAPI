using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Bodegas_Rollos
    {
        [Key]
        public long BgRollo_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime BgRollo_FechaEntrada { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string BgRollo_HoraEntrada { get; set; }

        [Column(TypeName = "date")]
        public DateTime BgRollo_FechaModifica { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string BgRollo_HoraModifica { get; set; }
        public string BgRollo_Observacion { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
