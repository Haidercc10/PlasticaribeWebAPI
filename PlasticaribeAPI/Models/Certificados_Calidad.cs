using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Certificados_Calidad
    {
        [Key]
        public long Consecutivo { get; set; }
        public long Orden_Trabajo { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Cliente { get; set; }
        public int Item { get; set; }
        public Producto? Producto { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Referencia { get; set; }

        [Precision(18, 2)]
        public decimal Cantidad_Producir { get; set; }
        public Unidad_Medida? Und { get; set; }
        public string Presentacion_Producto { get; set; }

        [Column(TypeName = ("date"))]
        public DateTime Fecha_Orden { get; set; }
        public Unidad_Medida? Und_Calibre { get; set; }
        public string Unidad_Calibre { get; set; }

        [Precision(18, 2)]
        public decimal Nominal_Calibre { get; set; }

        [Precision (18, 2)]
        public decimal Tolerancia_Calibre { get; set; }

        [Precision(18, 2)]
        public decimal Minimo_Calibre { get; set; }

        [Precision(18, 2)]
        public decimal Maximo_Calibre { get; set; }
        public Unidad_Medida? Und_AnchoFrente { get; set; }
        public string Unidad_AnchoFrente { get; set; }

        [Precision(18, 2)]
        public decimal Nominal_AnchoFrente { get; set; }

        [Precision(18, 2)]
        public decimal Tolerancia_AnchoFrente { get; set; }

        [Precision(18, 2)]
        public decimal Minimo_AnchoFrente { get; set; }

        [Precision(18, 2)]
        public decimal Maximo_AnchoFrente { get; set; }
        public Unidad_Medida? Und_AnchoFuelle { get; set; }
        public string Unidad_AnchoFuelle { get; set; }

        [Precision(18, 2)]
        public decimal Nominal_AnchoFuelle { get; set; }

        [Precision(18, 2)]
        public decimal Tolerancia_AnchoFuelle { get; set; }

        [Precision(18, 2)]
        public decimal Minimo_AnchoFuelle { get; set; }

        [Precision(18, 2)]
        public decimal Maximo_AnchoFuelle { get; set; }
        public Unidad_Medida? Und_LargoRepeticion { get; set; }
        public string Unidad_LargoRepeticion { get; set; }

        [Precision(18, 2)]
        public decimal Nominal_LargoRepeticion { get; set; }

        [Precision(18, 2)]
        public decimal Tolerancia_LargoRepeticion { get; set; }

        [Precision(18, 2)]
        public decimal Minimo_LargoRepeticion { get; set; }

        [Precision(18, 2)]
        public decimal Maximo_LargoRepeticion { get; set; }
        public Unidad_Medida? Und_Cof { get; set; }
        public string Unidad_Cof { get; set; }

        [Precision(18, 2)]
        public decimal Nominal_Cof { get; set; }

        [Precision(18, 2)]
        public decimal Tolerancia_Cof { get; set; }

        [Precision(18, 2)]
        public decimal Minimo_Cof { get; set; }

        [Precision(18, 2)]
        public decimal Maximo_Cof { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Material { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Resistencia { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Sellabilidad { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Transparencia { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Tratado { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Impresion { get; set; }

        [Column(TypeName = ("varchar(max)"))]
        public string Observacion { get; set; }

        [Column(TypeName = ("date"))]
        public DateTime Fecha_Registro { get; set; }

        [Column(TypeName = ("varchar(10)"))]
        public string Hora_Registro { get; set; }
        public Usuario? Usuario { get; set; }
        public long Usua_Id { get; set; }
    }
}
