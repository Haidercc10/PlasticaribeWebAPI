using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class ControlCalidad_CorteDoblado
    {
        [Key]
        public int Id { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Registro { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Hora_Resgitros { get; set; }
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

        [Precision(18, 2)]
        public decimal Ancho { get; set; }
        public string UndMed_Id { get; set; } = "Cms";
        public Unidad_Medida? Und { get; set; }

        [Precision(18, 2)]
        public decimal Calibre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Codigo_Barras { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Tipo_Embobinado { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string PasoEntre_Guia { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Observacion { get; set; }
    }
}