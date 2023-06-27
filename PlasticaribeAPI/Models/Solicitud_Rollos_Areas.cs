using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Solicitud_Rollos_Areas
    {
        [Key]
        public long SolRollo_Id { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime SolRollo_FechaSolicitud { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string SolRollo_HoraSolicitud { get; set; }
        public long Usua_Respuesta { get; set; }
        public Usuario? UsuarioRespuesta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SolRollo_FechaRespuesta { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? SolRollo_HoraRespuesta { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }        
        public int TpSol_Id { get; set; }
        public Tipo_Solicitud_Rollos_Areas? Tipo_solicitud { get; set; }
        public string SolRollo_Observacion { get; set; }
    }
}
