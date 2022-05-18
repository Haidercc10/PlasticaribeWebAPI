using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Pedido_Producto
    {   
        
        public int Prod_Id { get; set; }
        
        public Producto? Prod { get; set; }

        //Llave foranea pedidos
        public long PedExt_Id { get; set; }

        public PedidoExterno? PedExt { get; set; }
    }
}
