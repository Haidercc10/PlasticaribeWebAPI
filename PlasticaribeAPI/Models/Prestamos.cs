using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Prestamos
    {
        [Column(Order = 1), Key, Required]
        public long Ptm_Id { get; set; }

        [Column(Order = 2), Required]
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(Order = 3), Required, Precision(18, 2)]
        public decimal Ptm_Valor { get; set; }

        [Column(Order = 4), Required, Precision(18, 2)]
        public decimal Ptm_ValorDeuda { get; set; }

        [Column(Order = 5), Required, Precision(18, 2)]
        public decimal Ptm_ValorCancelado { get; set; }

        [Column(Order = 6), Required, Precision(18, 2)]
        public decimal Ptm_ValorCuota { get; set; }

        [Column(Order = 7), Required, Precision(18, 2)]
        public decimal Ptm_PctjeCuota { get; set; }

        [Column(Order = 8), Required]
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        [Column(Order = 9, TypeName = "date"), Required]
        public DateTime Ptm_FechaPlazo { get; set; }

        [Column(Order = 10, TypeName = "date")]
        public DateTime? Ptm_FechaUltCuota { get; set; }

        [Column(Order = 11, TypeName = "varchar(max)")]
        public string? Ptm_Observacion { get; set; }

        [Column(Order = 12), Required]
        public long Creador_Id { get; set; }
        public Usuario? Creador { get; set; }

        [Column(Order = 13, TypeName = "date"), Required]
        public DateTime Ptm_Fecha { get; set; }

        [Column(Order = 14, TypeName = "varchar(10)"), Required]
        public string Ptm_HoraRegistro { get; set; }

        [Column(Order = 15, TypeName = "date"), Required]
        public DateTime Ptm_FechaRegistro { get; set; }

        [Column(Order = 16), Required]
        public int Ptm_NroCuotas { get; set; }

        [Column(Order = 17), Required]
        public string Ptm_LapsoCuotas { get; set; }
    }
}
