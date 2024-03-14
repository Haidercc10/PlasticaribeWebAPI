using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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

        public int TpCcpto_Id { get; set; }
        public Tipos_Conceptos? TipoCpto { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
