using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Cliente_Producto
    {
        [Key]
        public long Codigo { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Cli { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
