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

        [Precision(14, 2)]
        public decimal Ot_CantidadKilos { get; set; }

        [Precision(14, 2)]
        public decimal Ot_CantidadUnidades { get; set; }

        [Precision(14, 2)]
        public decimal Ot_MargenAdicional { get; set; }

        [Precision(14, 2)]
        public decimal Ot_CantidadKilos_Margen { get; set; }

        [Precision(14, 2)]
        public decimal Ot_CantidadUnidades_Margen { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Ot_FechaCreacion { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
        public long PedExt_Id { get; set; }
        public PedidoExterno? PedidoExterno { get; set; }

        [Column(TypeName = "text")]
        public string Ot_Observacion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Ot_Cyrel { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Ot_Corte { get; set; }

        public long Mezcla_Id { get; set; }
        public Mezcla? Mezcla { get; set; }

    }
}
