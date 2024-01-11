using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    //Esta clase guardará el inventario con el que se inicia cada día. 
    public class InventarioInicialXDia_MatPrima
    {
        [Key]
        public long Codigo { get; set; }
        public long MatPri_Id { get; set; }
        public long Tinta_Id { get; set; }
        public long? Bopp_Id { get; set; } //Bopp Generico

        [Precision(14, 2)]
        public decimal InvInicial_Stock { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; } //Llave foranea Unidad_Medida           
        public Unidad_Medida? UndMed { get; set; } //Propiedad de Navegación Unidad_Medida

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
