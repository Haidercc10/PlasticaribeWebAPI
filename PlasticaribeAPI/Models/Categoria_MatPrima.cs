using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Categoria_MatPrima
    {
        [Key]
        public int CatMP_Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string CatMP_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string CatMP_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
