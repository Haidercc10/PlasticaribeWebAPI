using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Cliente_Producto
    {
        [Key]
        public long Codigo { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Cli { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; } 
    }
}
