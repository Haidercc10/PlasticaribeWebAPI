using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Pistas
    {
        [Key]
        public int Pista_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Pista_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Pista_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
