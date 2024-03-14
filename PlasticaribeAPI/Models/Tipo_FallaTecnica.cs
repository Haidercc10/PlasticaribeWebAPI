using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipo_FallaTecnica
    {

        [Key]
        public int TipoFalla_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string TipoFalla_Nombre { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string TipoFalla_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
