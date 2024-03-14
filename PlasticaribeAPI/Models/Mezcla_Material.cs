using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Mezcla_Material
    {
        [Key]
        public int MezMaterial_Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string MezMaterial_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? MezMaterial_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
