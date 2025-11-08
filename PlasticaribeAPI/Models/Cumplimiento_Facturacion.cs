using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Cumplimiento_Facturacion
    {
        [Key]
        public int Cufa_Id { get; set; }


        [Column(TypeName = "date")]
        public DateTime Cufa_Fecha { get; set; }


        [Precision(18, 2)]
        public decimal Cufa_FacturadoDia { get; set; }

        [Precision(18, 2)]
        public decimal Cufa_MetaDia { get; set; }


        [Precision(18, 2)]
        public decimal? Cufa_FacturadoMes { get; set; }

        [Precision(18, 2)]
        public decimal? Cufa_MetaMes { get; set; }


        [Precision(18, 2)]
        public decimal? Cufa_FacturadoAnual { get; set; }

        [Precision(18, 2)]
        public decimal? Cufa_MetaAnual { get; set; }


        [Precision(18, 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Cufa_PorcentajeDia { get; private set; }

        [Precision(18, 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Cufa_PorcentajeMes { get; private set; }

        [Precision(18, 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Cufa_PorcentajeAnual { get; private set; }

        [Column(TypeName = "date")]
        public DateTime Cufa_FechaRegistro { get; set; } = DateTime.Now;

        [Column(TypeName = "varchar(20)")]
        public string Cufa_HoraRegistro { get; set; }
    }
}
