using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Prestamos
    {
        [Key]
        public long Ptm_Id { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
        
        [Precision(18, 2)]
        public decimal Ptm_Valor { get; set; }

        [Precision(18, 2)]
        public decimal Ptm_ValorDeuda { get; set; }

        [Precision(18, 2)]
        public decimal Ptm_ValorCancelado { get; set; }

        [Column(TypeName = "date")]
        public DateTime Ptm_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Ptm_Hora { get; set; }

        [Precision(18, 2)]
        public decimal Ptm_ValorCuota { get; set; }

        [Precision(18, 2)]
        public decimal Ptm_PctjeCuota { get; set; }

        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }


        [Column(TypeName = "date")]
        public DateTime Ptm_FechaPlazo { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Ptm_FechaUltCuota { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Ptm_Observacion { get; set; }

        public long Usua_Creador { get; set; }
        public Usuario? Usuario2 { get; set; }
    }
}
