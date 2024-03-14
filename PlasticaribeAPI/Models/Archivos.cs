using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Archivos
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
        //[StringLength(255)]

        public int Categoria_Id { get; set; }
        public Categorias_Archivos? Categoria { get; set; }
        public string? Ubicacion { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
