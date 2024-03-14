using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Material_MatPrima
    {
        [Key]
        public int Material_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Material_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Material_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
