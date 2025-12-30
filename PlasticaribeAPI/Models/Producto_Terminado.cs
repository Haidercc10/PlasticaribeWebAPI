using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
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
        
        [Precision(14, 2)]
        public decimal Pt_Ancho { get; set; }
        
        [Precision(14, 2)]
        public decimal Pt_Largo { get; set; }

        [Precision(14, 2)]
        public decimal? Pt_Fuelle { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }
    }
}
