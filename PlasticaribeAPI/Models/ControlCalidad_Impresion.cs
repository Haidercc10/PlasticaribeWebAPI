using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ControlCalidad_Impresion
    {
        [Key]
        public int Id { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Registro { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Hora_Registro { get; set; }
        public string Turno_Id { get; set; }
        public Turno? Turno { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Maquina { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Ronda { get; set; }
        public long Orden_Trabajo { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Cliente { get; set; }
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Nombre_Producto { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string LoteRollo_SinImpresion { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Prueba_Tratado { get; set; }

        [Precision(18, 2)]
        public decimal Ancho_Rollo { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Cian { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Magenta { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Amarillo { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Negro { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Base { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Pantone1 { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Pantone2 { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Pantone3 { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Secuencia_Pantone4 { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Tipo_Embobinado { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Codigo_Barras { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Texto { get; set; }
        public bool Fotocelda_Izquierda { get; set; }
        public bool Fotcelda_Derecha { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Registro_Colores { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Adherencia_Tinta { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Conformidad_Laminado { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string PasoGuia_Repetecion { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Observacion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}