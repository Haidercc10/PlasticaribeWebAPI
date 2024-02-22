using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class SalariosTrabajadores
    {
        [Column(Order = 1), Key, Required]
        public int Id { get; set; }

        [Column(Order = 2), Required]
        public long Id_Trabajador { get; set; }
        public Usuario? Trabajador { get; set; }

        [Column(Order = 3), Required, Precision(18, 2)]
        public decimal SalarioBase { get; set; }

        [Column(Order = 4), Required, Precision(18, 2)]
        public decimal AuxilioTransp { get; set; }

        [Column(Order = 5), Required, Precision(18, 2)]
        public decimal EPSMensual { get; set; }

        [Column(Order = 6), Required, Precision(18, 2)]
        public decimal AFPMensual { get; set; }

        [Column(Order = 7), Required, Precision(18, 2)]
        public decimal AhorroTotal { get; set; }
    }
}
