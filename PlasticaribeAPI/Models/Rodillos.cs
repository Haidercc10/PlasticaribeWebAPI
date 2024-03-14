using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Rodillos
    {
        [Key]
        public int Rodillo_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Rodillo_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Rodillo_Descripcion { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
