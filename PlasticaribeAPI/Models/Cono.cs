using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Cono
    {

        [Key]

        [Column(TypeName = "varchar(10)")]
        public string Cono_Id { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Cono_Nombre { get; set; }


        [Precision(18, 4)]
        public decimal Cono_KgXCmsAncho { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Cono_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
