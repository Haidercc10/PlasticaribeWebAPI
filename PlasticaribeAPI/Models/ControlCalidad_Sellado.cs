using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class ControlCalidad_Sellado
    {
        [Key]
        public long CcSel_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Turno_Id { get; set; }
        public Turno? Turnos { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usu { get; set; }

        public int CcSel_Maquina { get; set; }
        public int CcSel_Ronda { get; set; }
        public long CcSel_OT { get; set; }

        public int? Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Referencia { get; set; }

        [Precision(18, 2)]
        public decimal CcSel_Calibre { get; set; }

        [Precision(18, 2)]
        public decimal CcSel_Ancho { get; set; }

        [Precision(18, 2)]
        public decimal CcSel_Largo { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_AL { get; set; }
        public Unidad_Medida? UndMedida1 { get; set; }

        [Precision(18, 2)]
        public decimal AnchoFuelle_Izq { get; set; }

        [Precision(18, 2)]
        public decimal AnchoFuelle_Der { get; set; }

        [Precision(18, 2)]
        public decimal AnchoFuelle_Abajo { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_AF { get; set; }
        public Unidad_Medida? UndMedida2 { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CcSel_Rasgado { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CcSel_PruebaFiltrado { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CcSel_PruebaPresion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CcSel_Sellabilidad { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CcSel_Impresion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CcSel_Precorte { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CcSel_Perforacion { get; set; }

        [Precision(18, 2)]
        public decimal CcSel_CantBolsasxPaq { get; set; }

        [Column(TypeName = "date")]
        public DateTime CcSel_Fecha { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string CcSel_Hora { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string CcSel_Observacion { get; set; }
    }
}
