using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Orden_Trabajo
    {
        [Key]
        public long Ot_Id { get; set; }
        public long SedeCli_Id { get; set; }
        public SedesClientes? SedeCli { get; set; }
        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Ot_FechaCreacion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Ot_Hora { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
        public long PedExt_Id { get; set; }
        public PedidoExterno? PedidoExterno { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Ot_Observacion { get; set; }

        public bool Ot_Cyrel { get; set; }
        public bool Ot_Corte { get; set; }
        public long Mezcla_Id { get; set; }
        public Mezcla? Mezcla { get; set; }
        public bool Extrusion { get; set; }
        public bool Impresion { get; set; }
        public bool Rotograbado { get; set; }
        public bool Laminado { get; set; }
        public bool Sellado { get; set; }

        [Precision(14, 2)]
        public decimal Ot_MargenAdicional { get; set; }

        [Precision(14, 2)]
        public decimal Ot_CantidadPedida { get; set; }

        [Precision(14, 2)]
        public decimal Ot_ValorUnidad { get; set; }

        [Precision(14, 2)]
        public decimal Ot_PesoNetoKg { get; set; }

        [Precision(14, 2)]
        public decimal Ot_ValorKg { get; set; }

        [Precision(14, 2)]
        public decimal Ot_ValorOT { get; set; }

    }
}
