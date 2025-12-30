using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PedidoProducto
    {
        
        [Key]
        public long Codigo { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Product { get; set; }

        public long PedExt_Id { get; set; }
        public PedidoExterno? PedidoExt { get; set; }

        //Cantidades del producto en el pedido externo
        [Precision(14, 2)]
        public decimal PedExtProd_Cantidad { get; set; }

        //Llave foranea unidad medida
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        [Precision(18, 2)]
        public decimal PedExtProd_PrecioUnitario { get; set; } 

        [Column(TypeName = "date")]
        public DateTime PedExtProd_FechaEntrega { get; set; }

        [Precision(18, 2)]
        public decimal PedExtProd_CantidadFacturada { get; set; }

        [Precision(18, 2)]
        public decimal PedExtProd_CantidadFaltante { get; set; }

        //Referencia del producto en el pedido externo
        [Column(TypeName = "varchar(200)")]
        public string? PedExtProd_Referencia { get; set; }

        public int Estado_Id { get; set; } 
        public Estado? Estado { get; set; }

        public int? PedExtProd_OT { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? PedExtProd_Observacion { get; set; }

        //Datos adicionales del pedido producto (Pt)
        public bool PedExtProd_Impresion { get; set; }

        public bool ImpresionDobleCara { get; set; }

        public int PedExtProd_NroEmbobinado { get; set; }

        //Datos adicionales del producto

        public int Material_Id { get; set; }
        public Material_MatPrima? MaterialMP { get; set; }

        public int Pigmt_Id { get; set; }
        public Pigmento? Pigmt { get; set; }

        [Precision(14, 2)]
        public decimal PedExtProd_Calibre { get; set; }

        public bool PedExtProd_Tratado { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
