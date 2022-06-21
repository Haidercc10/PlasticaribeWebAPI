using Microsoft.EntityFrameworkCore;

namespace PlasticaribeAPI.Models
{
    public class PedidoProducto
    {
        public int Prod_Id { get; set; }       
        public Producto? Product { get; set; }

        //Llave foranea pedidos
        public long PedExt_Id { get; set; }
        public PedidoExterno? PedidoExt { get; set; }

        //Campos para la información detallada del pedido
        //Cantidad
        [Precision(14, 2)]
        public decimal PedExtProd_Cantidad { get; set; }
        
        //Llave foranea unidad medida
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        [Precision(18, 2)]
        public decimal? PedExtProd_PrecioUnitario { get; set; } /** Valor */

    }
}
