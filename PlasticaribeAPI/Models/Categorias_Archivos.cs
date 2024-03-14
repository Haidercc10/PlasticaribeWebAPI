using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Categorias_Archivos
    {
        [Key]
        public int Categoria_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Categoria_Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Categoria_Descricion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
