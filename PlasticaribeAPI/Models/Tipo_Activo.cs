using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipo_Activo
    {
        [Key]
        public int TpActv_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TpActv_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? TpActv_Descripcion { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
