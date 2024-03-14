using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PreEntrega_RolloDespacho
    {
        [Key]
        public long PreEntRollo_Id { get; set; }


        [Column(TypeName = "date")]
        public DateTime PreEntRollo_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? PreEntRollo_Hora { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? PreEntRollo_Observacion { get; set; }


        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        //[Column(TypeName = "varchar(10)")]
        //public string PreEntRollo_Hora { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
