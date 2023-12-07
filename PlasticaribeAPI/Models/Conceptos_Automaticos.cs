using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Conceptos_Automaticos
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Concepto { get; set; }

        [Precision(14, 2)]
        public decimal Base { get; set; }

        [Precision(14, 2)]
        public decimal Porcentaje { get; set; }
    }
}
