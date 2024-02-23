using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class NominaDetallada_Plasticaribe
    {
        [Column(Order = 1), Key, Required]
        public int Id { get; set; }

        [Column(Order = 2), Required]
        public long Id_Trabajador { get; set; }
        public Usuario? Trabajador { get; set; }

        [Column(Order = 3), Required, Precision(18, 2)]
        public decimal SalarioBase { get; set; }

        [Column(Order = 4, TypeName = "date"), Required]
        public DateTime PeriodoInicio { get; set; }

        [Column(Order = 5, TypeName = "date"), Required]
        public DateTime PeriodoFin { get; set; }

        [Column(Order = 6), Required]
        public int DiasAusente { get; set; }

        [Column(Order = 7), Required]
        public int DiasPagar { get; set; }

        [Column(Order = 8), Required]
        public int HorasPagar { get; set; }

        [Column(Order = 9), Required, Precision(18, 2)]
        public decimal ValorDiasPagar { get; set; }

        [Column(Order = 10), Required]
        public int DiasIncapEG { get; set; }

        [Column(Order = 11), Required, Precision(18, 2)]
        public decimal ValorIncapEG { get; set; }

        [Column(Order = 12), Required]
        public int DiasIncapAT { get; set; }

        [Column(Order = 13), Required, Precision(18, 2)]
        public decimal ValorIncapAT { get; set; }

        [Column(Order = 14), Required]
        public int DiasIncapPATMAT { get; set; }

        [Column(Order = 15), Required, Precision(18, 2)]
        public decimal ValorIncapPATMAT { get; set; }

        [Column(Order = 16), Required]
        public int HorasADCDiurnas { get; set; }

        [Column(Order = 17), Required, Precision(18, 2)]
        public decimal ValorADCDiurnas { get; set; }

        [Column(Order = 18), Required]
        public int HorasNoctDom { get; set; }

        [Column(Order = 19), Required, Precision(18, 2)]
        public decimal ValorNoctDom { get; set; }

        [Column(Order = 20), Required]
        public int HorasExtDiurnasDom { get; set; }

        [Column(Order = 21), Required, Precision(18, 2)]
        public decimal ValorExtDiurnasDom { get; set; }

        [Column(Order = 22), Required]
        public int HorasRecargo035 { get; set; }

        [Column(Order = 23), Required, Precision(18, 2)]
        public decimal ValorRecargo035 { get; set; }

        [Column(Order = 24), Required]
        public int HorasExtNocturnasDom { get; set; }

        [Column(Order = 25), Required, Precision(18, 2)]
        public decimal ValorExtNocturnasDom { get; set; }

        [Column(Order = 26), Required]
        public int HorasRecargo075 { get; set; }

        [Column(Order = 27), Required, Precision(18, 2)]
        public decimal ValorRecargo075 { get; set; }

        [Column(Order = 28), Required]
        public int HorasRecargo100 { get; set; }

        [Column(Order = 29), Required, Precision(18, 2)]
        public decimal ValorRecargo100 { get; set; }

        [Column(Order = 30), Required, Precision(18, 2)]
        public decimal TarifaADC { get; set; }

        [Column(Order = 31), Required, Precision(18, 2)]
        public decimal ValorTotalADCComp { get; set; }

        [Column(Order = 32), Required, Precision(18, 2)]
        public decimal AuxTransporte { get; set; }

        [Column(Order = 33), Required, Precision(18, 2)]
        public decimal ProductividadSella { get; set; }

        [Column(Order = 34), Required, Precision(18, 2)]
        public decimal ProductividadExt { get; set; }

        [Column(Order = 35), Required, Precision(18, 2)]
        public decimal ProductividadMontaje { get; set; }

        [Column(Order = 36), Required, Precision(18, 2)]
        public decimal Devengado { get; set; }

        [Column(Order = 37), Required, Precision(18, 2)]
        public decimal EPS { get; set; }

        [Column(Order = 38), Required, Precision(18, 2)]
        public decimal AFP { get; set; }

        [Column(Order = 39), Required, Precision(18, 2)]
        public decimal Ahorro { get; set; }

        [Column(Order = 40), Required, Precision(18, 2)]
        public decimal Prestamo { get; set; }

        [Column(Order = 41), Required, Precision(18, 2)]
        public decimal Anticipo { get; set; }

        [Column(Order = 42), Required, Precision(18, 2)]
        public decimal TotalDcto { get; set; }

        [Column(Order = 43), Required, Precision(18, 2)]
        public decimal PagoPTESemanaAnt { get; set; }

        [Column(Order = 44), Required, Precision(18, 2)]
        public decimal Dctos { get; set; }

        [Column(Order = 45), Required, Precision(18, 2)]
        public decimal Deducciones { get; set; }

        [Column(Order = 46), Required, Precision(18, 2)]
        public decimal TotalPagar { get; set; }

        [Column(Order = 47, TypeName = "varchar(max)"), Required]
        public string Novedades { get; set; }

        [Column(Order = 48), Required]
        public int TipoNomina { get; set; }
        public Tipos_Nomina? TiposNomina { get; set; }

        [Column(Order = 49), Required]
        public int Estado_Nomina { get; set; }
        public Estado? Estado { get; set; }

        [Column(Order = 50), Required]
        public long Creador_Id { get; set; }
        public Usuario? Creador { get; set; }

        [Column(Order = 51, TypeName = "date"), Required]
        public DateTime Fecha { get; set; }

        [Column(Order = 52, TypeName = "varchar(20)"), Required, Precision(18, 2)]
        public string Hora { get; set; }
    }
}
