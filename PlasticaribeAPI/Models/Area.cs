using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Area
    {

        [Key]
        public long Area_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Area_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public String? Area_Descripcion { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
