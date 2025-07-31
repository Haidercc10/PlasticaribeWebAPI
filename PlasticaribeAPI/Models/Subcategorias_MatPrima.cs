using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Subcategorias_MatPrima
    {
        [Key]
        public int SubCatMP_Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string SubCatMP_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string SubCatMP_Descripcion { get; set; }

        public int CatMP_Id { get; set; }
        public Categoria_MatPrima? Categoria_MP { get; set; }
    }
}
