using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class IngresoRollos_Extrusion
    {
        [Key]
        public long IngRollo_Id { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? IngRollo_Observacion { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }

        [Column(TypeName = "date")]
        public DateTime IngRollo_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string IngRollo_Hora { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
