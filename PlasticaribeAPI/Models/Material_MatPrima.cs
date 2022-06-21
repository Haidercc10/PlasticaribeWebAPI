using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Material_MatPrima
    {
        [Key]
        public int Material_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Material_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Material_Descripcion { get; set; }
    }
}
