using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum EstadoAutorizacionExcesoOT
    {
        PENDIENTE = 11,
        ACEPTADO = 26,
        CANCELADO = 4,
        PROCESADO = 51
    }

    public class AutorizacionExcesoOT
    {
        [Key]
        public long AutEx_Id { get; set; }

        public long AutEx_OrdenTrabajo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AutEx_CantidadKg { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string AutEx_Motivo { get; set; } = string.Empty;

        public int Estado_Id { get; set; }
        public Estado? Estados { get; set; }

        public long UsuarioSolicitaId { get; set; }
        public Usuario? UsuarioSolicita { get; set; }

        [Column(TypeName = "date")]
        public DateTime AutEx_FechaSolicitudUtc { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string AutEx_HoraSolicitudUtc { get; set; }

        public long? UsuarioAutorizaId { get; set; }
        public Usuario? UsuarioAutoriza { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? AutEx_MotivoRespuesta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutEx_FechaRespuestaUtc { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? AutEx_HoraRespuestaUtc { get; set; } = string.Empty;

        public long? AsigMp_Id { get; set; }
        public Asignacion_MatPrima? AsignacionMatPrima { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutEx_FechaConsumoUtc { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? AutEx_HoraConsumoUtc { get; set; } = string.Empty;
    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
}
