using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class CajaMenor_Plasticaribe
    {
        [Key]
        public int CajaMenor_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime CajaMenor_FechaRegistro { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string CajaMenor_HoraRegistro { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime CajaMenor_FechaSalida { get; set; }

        [Precision(18, 2)]
        public decimal CajaMenor_ValorSalida { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string CajaMenor_Observacion { get; set; }
        public int TpSal_Id { get; set; }
        public TipoSalidas_CajaMenor? TpSalida { get; set; }

        public long Area_Id { get; set; }
        public Area? Areas { get; set; }
    }
}
