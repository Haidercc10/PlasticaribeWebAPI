using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class LogEventos_Maquinas
    {
        [Key]
        public long Lem_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Maq_Id { get; set; }
        public Maquinas? Maquinas { get; set; }

        public long Evmq_Id { get; set; }
        public Eventos_Maquinas? EventosMaquinas { get; set; }

        [Column(TypeName = "date")]
        public DateTime Lem_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Lem_Hora { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? Lem_Observacion { get; set; }
    }
}
