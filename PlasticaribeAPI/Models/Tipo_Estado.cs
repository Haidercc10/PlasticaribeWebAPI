using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipo_Estado
    {
        [Key]
        public int TpEstado_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string TpEstado_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? TpEstado_Descripcion { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
