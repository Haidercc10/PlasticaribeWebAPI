using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Bopp_Generico
    {
        [Key]
        public long BoppGen_Id { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string BoppGen_Nombre { get; set; }


        [Precision(18, 2)]
        public decimal BoppGen_Ancho { get; set; }


        [Precision(18, 2)]
        public long BoppGen_Micra { get; set; }
    }
}
