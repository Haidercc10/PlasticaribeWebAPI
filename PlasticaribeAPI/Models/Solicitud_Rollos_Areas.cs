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

        [Column(TypeName = "date")]
        public DateTime SolRollo_FechaRespues { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string SolRollo_HoraRespuesta { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }
        public long SolRollo_OrdenTrabajo { get; set; }
        public long SolRollo_Maquina { get; set; }
        public string SolRollo_BodegaSolicitante { get; set; }
        public Proceso? Bodega_Solicitante { get; set; }
        public string SolRollo_BodegaSolicitada { get; set; }
        public Proceso? Bodega_Solicitada { get; set; }
        public long SolRollo_Rollo { get; set; }

        [Precision(14, 2)]
        public decimal SolRollo_Cantidad { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }
        public int TpSol_Id { get; set; }
        public Tipo_Solicitud_Rollos_Areas? Tipo_solicitud { get; set; }
        public string SolRollo_Observacion { get; set; }
    }
}
