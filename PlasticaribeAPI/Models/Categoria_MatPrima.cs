using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Categoria_MatPrima
    {
        [Key]
        public int CatMP_Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string CatMP_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string CatMP_Descripcion { get; set; }
    }
}
