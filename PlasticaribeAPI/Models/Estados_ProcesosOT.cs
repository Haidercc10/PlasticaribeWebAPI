using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Estados_ProcesosOT
    {

        [Key]
        public long EstProcOT_Id { get; set; }

        public long EstProcOT_OrdenTrabajo { get; set; }


        [Precision(18,2)]
        public decimal EstProcOT_ExtrusionKg { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_ImpresionKg { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_RotograbadoKg { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_LaminadoKg { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_CorteKg { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_DobladoKg { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_SelladoKg { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_SelladoUnd { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_WiketiadoKg { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_WiketiadoUnd { get; set; }


        [Precision(18, 2)]
        public decimal EstProcOT_CantidadPedida { get; set; }

        /** LLave foranea y prop. navegación Unidades Medidas */
        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UnidadMedida { get; set; }

        /** LLave foranea y prop. navegación Estados */
        public int Estado_Id { get; set; }
        public Estado? Estado_OT { get; set; }


        /** LLave foranea y prop. navegación Falla_Tecnica */
        public int Falla_Id { get; set; }        
        public Falla_Tecnica? FallaTecnica { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string EstProcOT_Observacion { get; set; }




    }
}
