using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Solicitud_MatPrimaExtrusion
    {
        [Key]
        public long SolMpExt_Id { get; set; }

        public long SolMpExt_OT { get; set; }

        public int SolMpExt_Maquina { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }


        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }


        [Column(TypeName = "Date")]
        public DateTime SolMpExt_Fecha { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? SolMpExt_Hora { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? SolMpExt_Observacion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
