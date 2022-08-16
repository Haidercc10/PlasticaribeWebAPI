using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Categorias_Archivos
    {
        [Key]
        public int Categoria_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Categoria_Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Categoria_Descricion { get; set; }
    }
}
