using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    //Esta clase guardará el inventario con el que se inicia cada día. 
    public class InventarioInicialXDia_MatPrima
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MatPri_Id { get; set; }

        [Precision(14, 2)]
        public decimal InvInicial_Stock { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; } //Llave foranea Unidad_Medida           
        public Unidad_Medida? UndMed { get; set; } //Propiedad de Navegación Unidad_Medida
    }
}
