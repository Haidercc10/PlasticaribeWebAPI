using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Facturacion_Productos
    {
        [Key]
        public int FactPro_Codigo { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string FactPro_Pedido { get; set; }

        public int Of_Id { get; set; }
        public OrdenFacturacion? OrdenFacturacion { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        [Precision(18, 2)]
        public decimal FactPro_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }


        [Precision(18, 2)]
        public decimal FactPro_Unidades { get; set; }


        [Precision(18, 2)]
        public decimal Peso_Bruto { get; set; }

        [Precision(18, 2)]
        public decimal Peso_Neto { get; set; }

    }
}
