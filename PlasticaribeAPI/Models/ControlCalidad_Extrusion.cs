using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ControlCalidad_Extrusion
    {
        [Key]
        public long CcExt_Id { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Turno_Id { get; set; }
        public Turno? Turnos { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usu { get; set; }

        public int CcExt_Maquina { get; set; }
        public int CcExt_Ronda { get; set; }
        public long CcExt_OT { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string CcExt_Cliente { get; set; }

        public int? Prod_Id { get; set; }
        public Producto? Producto { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string Referencia { get; set; }


        public long CcExt_Rollo { get; set; }

        public int Pigmento_Id { get; set; }
        public Pigmento? Pigmento { get; set; }


        [Precision(18, 2)]
        public decimal CcExt_AnchoTubular { get; set; }


        [Precision(18, 2)]
        public decimal CcExt_PesoMetro { get; set; }


        [Precision(18, 2)]
        public decimal CcExt_Ancho { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }


        [Precision(18, 2)]
        public decimal CcExt_CalibreMax { get; set; }  //Calibre Maximo


        [Precision(18, 2)]
        public decimal CcExt_CalibreMin { get; set; } //Calibre Minimo


        [Precision(18, 2)]
        public decimal CcExt_CalibreProm { get; set; } //Calibre Promed


        [Column(TypeName = "varchar(50)")]
        public string CcExt_Apariencia { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string CcExt_Tratado { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string CcExt_Rasgado { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string CcExt_TipoBobina { get; set; }


        [Precision(18, 2)]
        public decimal? CcExt_CalibreTB { get; set; } //Calibre Tipo Bobina


        [Column(TypeName = "date")]
        public DateTime CcExt_Fecha { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string CcExt_Hora { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? CcExt_Observacion { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
