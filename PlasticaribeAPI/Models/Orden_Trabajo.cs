using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Orden_Trabajo
    {
        [Key]
        [Column(Order = 0)]
        public long Ot_Id { get; set; }

        [Column(Order = 1)]
        public long Numero_OT { get; set; }

        [Column(Order = 2)]
        public long SedeCli_Id { get; set; }
        public SedesClientes? SedeCli { get; set; }

        [Column(Order = 3)]
        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }

        [Column(Order = 4)]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }

        [Column(Order = 5)]
        [Precision(14, 2)]
        public decimal Ot_CantidadPedida { get; set; }

        [Column(Order = 6)]
        [Precision(14, 2)]
        public decimal Ot_ValorKg { get; set; }

        [Column(Order = 7)]
        [Precision(14, 2)]
        public decimal Ot_ValorUnidad { get; set; }

        [Column(Order = 8)]
        [Precision(14, 2)]
        public decimal Ot_PesoNetoKg { get; set; }

        [Column(Order = 9)]
        [Precision(14, 2)]
        public decimal Ot_MargenAdicional { get; set; }

        [Column(Order = 10)]
        [Precision(14, 2)]
        public decimal Ot_ValorOT { get; set; }

        [Column(Order = 11)]
        public bool Extrusion { get; set; }

        [Column(Order = 12)]
        public bool Impresion { get; set; }

        [Column(Order = 13)]
        public bool Rotograbado { get; set; }

        [Column(Order = 14)]
        public bool Laminado { get; set; }

        [Column(Order = 15)]
        public bool Ot_Corte { get; set; }

        [Column(Order = 16)]
        public bool Sellado { get; set; }

        [Column(Order = 17)]
        public bool Ot_Cyrel { get; set; }

        [Column(Order = 18)]
        public long Mezcla_Id { get; set; }
        public Mezcla? Mezcla { get; set; }

        [Column(Order = 19)]
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "Date", Order = 20)]
        public DateTime Ot_FechaCreacion { get; set; }

        [Column(TypeName = "varchar(10)", Order = 21)]
        public string? Ot_Hora { get; set; }

        [Column(Order = 22)]
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        [Column(Order = 23)]
        public long Id_Vendedor { get; set; }
        public Usuario? Vendedor { get; set; }

        [Column(Order = 24)]
        public long PedExt_Id { get; set; }
        public PedidoExterno? PedidoExterno { get; set; }

        [Column(TypeName = "varchar(max)", Order = 25)]
        public string Ot_Observacion { get; set; }

        [Column(Order = 26)]
        public bool MotrarEmpresaEtiquetas { get; set; }
    }
}