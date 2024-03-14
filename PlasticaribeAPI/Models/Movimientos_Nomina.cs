using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Movimientos_Nomina
    {
        [Key, Column(Order = 1)]
        public int Id { get; set; }

        [Column(Order = 2), Required]
        public long Trabajador_Id { get; set; }
        public Usuario? Trabajador { get; set; }

        [Column(Order = 3), Required]
        public int CodigoMovimento { get; set; }

        [Column(Order = 4, TypeName = "varchar(100)"), Required]
        public string NombreMovimento { get; set; }

        [Column(Order = 5), Precision(18,2), Required]
        public decimal ValorTotal { get; set; }

        [Column(Order = 6), Precision(18,2), Required]
        public decimal ValorDeuda { get; set; }

        [Column(Order = 7), Precision(18, 2), Required]
        public decimal ValorPagado { get; set; }
        
        [Column(Order = 8), Precision(18, 2), Required]
        public decimal ValorAbonado { get; set; }

        [Column(Order = 9), Precision(18, 2), Required]
        public decimal ValorFinalDeuda { get; set; }

        [Column(Order = 10, TypeName = "date"), Required]
        public DateTime Fecha { get; set; }

        [Column(Order = 11, TypeName = "varchar(50)"), Required]
        public string Hora { get; set; }

        [Column(Order = 12, TypeName = "varchar(max)")]
        public string Observacaion { get; set; }

        [Column(Order = 13), Required]
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        [Column(Order = 14), Required]
        public long Creador_Id { get; set; }
        public Usuario? Creador { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
