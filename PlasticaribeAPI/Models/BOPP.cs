using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class BOPP
    {

        [Key]
        public long BOPP_Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]

        public string BOPP_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? BOPP_Descripcion { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string BOPP_Serial { get; set; }


        [Precision(18, 2)]
        public decimal BOPP_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; } //Llave foranea Unidad_Medida           
        public Unidad_Medida? UndMed { get; set; } //Propiedad de Navegación Unidad_Medida


        public int CatMP_Id { get; set; }  //Llave foranea Categorias_MatPrima.        
        public Categoria_MatPrima? CatMP { get; set; }  //Propiedad de Navegación Categorias_MatPrima


        [Precision(18, 2)]
        public decimal BOPP_Precio { get; set; }


        public int TpBod_Id { get; set; } //Llave foranea Tipo_Bodega
        public Tipo_Bodega? TpBod { get; set; } //Propiedad de Navegación Tipos_Bodegas
    }
}
