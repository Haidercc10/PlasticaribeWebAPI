using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Rollo_Desecho
    {

        [Key]
        public long Rollo_Codigo { get; set; }

        public long Rollo_OT { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string Rollo_Cliente { get; set; }


        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }


        [Precision(18, 2)]
        public decimal Rollo_TotalPedidoOT { get; set; }

        public long Rollo_Id { get; set; }


        [Precision(18, 2)]
        public decimal Rollo_Ancho { get; set; }


        [Precision(18, 2)]
        public decimal Rollo_Largo { get; set; }


        [Precision(18, 2)]
        public decimal Rollo_Fuelle { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }


        [Precision(18, 2)]
        public decimal Rollo_PesoBruto { get; set; }


        [Precision(18, 2)]
        public decimal Rollo_PesoNeto { get; set; }


        [Precision(18, 2)]
        public decimal Rollo_Tara { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Cono_Id { get; set; }
        public Cono? Cono { get; set; }


        public int Material_Id { get; set; }
        public Material_MatPrima? Material { get; set; }


        [Precision(18, 2)]
        public decimal Rollo_Calibre { get; set; }


        [Column(TypeName = "date")]
        public DateTime Rollo_FechaIngreso { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Rollo_Hora { get; set; }


        public int Rollo_Maquina { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string Rollo_Operario { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Turno_Id { get; set; }
        public Turno? Turno { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Rollo_FechaEliminacion { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? Rollo_HoraEliminacion { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Rollo_Observacion { get; set; }

        public int? Falla_Id { get; set; }
        public Falla_Tecnica? Falla { get; set; } 
    }
}
