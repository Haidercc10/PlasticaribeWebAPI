using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TipoIncapacidad
    {
        [Key, Column(Order = 1), Required]
        public int Id { get; set; }

        [Column(Order = 2, TypeName = "varchar(100)"), Required]
        public string Nombre { get; set; }

        [Column(Order = 3, TypeName = "varchar(max)"), Required]
        public string Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
