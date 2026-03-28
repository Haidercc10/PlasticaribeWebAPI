using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Producto_Terminado
    {
        
        [Key]
        public long Pt_Id { get; set; }

        public long PedExtProd_Id { get; set; }
        public PedidoProducto? PedidoProducto { get; set; }
      
        [Precision(5, 2)]
        public decimal Pt_Margen { get; set; }
        
        [Precision(14, 2)]
        public decimal Pt_PesoMillar { get; set; }

        [Precision(14, 2)]
        public decimal? Pt_PesoRollo { get; set; }

        [Precision(14, 2)]
        public decimal? Pt_PesoUnd { get; set; }
        
        public int Pt_CantBolsasBulto { get; set; }
        
        public int Pt_CantBolsasPaquete { get; set; }
        
        public int TpSellado_Id { get; set; }
        public Tipos_Sellados? TiposSellados { get; set; }

        public int TpImpresion_Id { get; set; }
        public Tipos_Impresion? TipoImpresion { get; set; }

        public int TpProd_Id { get; set; }
        public Tipo_Producto? TpProd { get; set; }

        //Nuevo
        public int Tratado_Id { get; set; } 
        public Tratado? Tratados { get; set; }

        [Precision(14, 2)]
        public decimal Pt_Ancho { get; set; }
        
        [Precision(14, 2)]
        public decimal Pt_Largo { get; set; }

        //Fuelles
        [Precision(14, 2)]
        public decimal? Pt_FuelleIzq { get; set; }

        [Precision(14, 2)]
        public decimal? Pt_FuelleDer { get; set; }

        [Precision(14, 2)]
        public decimal? Pt_FuelleFondo { get; set; }

        [Precision(14, 2)]
        public decimal? Pt_Solapa { get; set; }

        //Nuevo
        [Column(TypeName = "varchar(10)")]
        public string UndMed_ALF { get; set; }
        public Unidad_Medida? UnidadesALF { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        //Datos adicionales del pedido producto (Pt)

        [Column(TypeName = "varchar(50)")]
        public string Pt_ImpresionFD { get; set; }

        public bool Pt_Laminado { get; set; }

        public int Pt_NroEmbobinado { get; set; }

        //Datos adicionales del producto

        public int Material_Id { get; set; }
        public Material_MatPrima? MaterialMP { get; set; }

        public int Pigmt_Id { get; set; }
        public Pigmento? Pigmt { get; set; }

        [Precision(14, 2)]
        public decimal Pt_Calibre { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Calibre { get; set; }
        public Unidad_Medida? UnidadesCal { get; set; }
    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
}
