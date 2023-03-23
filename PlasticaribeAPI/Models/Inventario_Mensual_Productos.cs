using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Inventario_Mensual_Productos
    {
        [Key]
        public long Codigo { get; set; }
        public long Prod_Id { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }

        [Precision(14, 2)]
        public decimal Enero { get; set; }

        [Precision(14, 2)]
        public decimal Febrero { get; set; }

        [Precision(14, 2)]
        public decimal Marzo { get; set; }

        [Precision(14, 2)]
        public decimal Abril { get; set; }

        [Precision(14, 2)]
        public decimal Mayo { get; set; }

        [Precision(14, 2)]
        public decimal Junio { get; set; }

        [Precision(14, 2)]
        public decimal Julio { get; set; }

        [Precision(14, 2)]
        public decimal Agosto { get; set; }

        [Precision(14, 2)]
        public decimal Septiembre { get; set; }

        [Precision(14, 2)]
        public decimal Octubre { get; set; }

        [Precision(14, 2)]
        public decimal Noviembre { get; set; }

        [Precision(14, 2)]
        public decimal Diciembre { get; set; }
    }
}
