using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Categoria_Insumo

    {
       [Key]
       public int CatInsu_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string CatInsu_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string CatInsu_Descripcion { get; set; }

        //[StringLength(255)]
        public string? CatInsu_UrlImagen { get; set; }

        //[NotMapped]
        //public IFormFile? CatInsu_RutaImagen { get; set; }
    }
}
