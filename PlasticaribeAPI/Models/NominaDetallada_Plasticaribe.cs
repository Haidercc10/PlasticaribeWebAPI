using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NominaDetallada_Plasticaribe
    {
        [Column(Order = 1), Key, Required]
        public int Id { get; set; } /* Campo autoincrementable y llave primaria de la tabla */

        [Column(Order = 2), Required]
        public long Id_Trabajador { get; set; } /* Campo en el que se gusardará el número de identificación del trabajador al que se le crea la nomina, llave foranea */
        public Usuario? Trabajador { get; set; } /* Campo que hará referencia a la tabla de Usuarios */

        [Column(Order = 3), Required, Precision(18, 2)]
        public decimal SalarioBase { get; set; } /* Campo que almacenará el salario base del trabajador */

        [Column(Order = 4, TypeName = "date"), Required]
        public DateTime PeriodoInicio { get; set; } /* Campo que almacenará la fecha de inicio de la nomina */

        [Column(Order = 5, TypeName = "date"), Required]
        public DateTime PeriodoFin { get; set; } /* Campo que almacenará la fecha en que finaliza la nomina */

        [Column(Order = 6), Required]
        public int DiasAusente { get; set; } /* Cantidad de días ausente sin excusa */

        [Column(Order = 7), Required]
        public int DiasPagar { get; set; } /* Cantidad de días a pagar */

        [Column(Order = 8), Required]
        public int HorasPagar { get; set; } /* Cantidad de Horas a pagar. DiasPagar / 8 */

        [Column(Order = 9), Required, Precision(18, 2)]
        public decimal ValorDiasPagar { get; set; } /* Valor a pagar. DiasPagar * (SalarioBase / 30) */

        [Column(Order = 10), Required]
        public int DiasIncapEG { get; set; } /* Días de incapacidad por enfermedad general */

        [Column(Order = 11), Required, Precision(18, 2)]
        public decimal ValorIncapEG { get; set; } /* Valor a pagar por incapacidad de enfermedad general */

        [Column(Order = 12), Required]
        public int DiasIncapAT { get; set; } /* Días de incapacidad por accidente de trabajo */

        [Column(Order = 13), Required, Precision(18, 2)]
        public decimal ValorIncapAT { get; set; } /* Valor a pagar por incapacidad de accidente de trabajo */

        [Column(Order = 14), Required]
        public int DiasIncapPATMAT { get; set; } /* Días de incapacidad por paternidad o maternidad */

        [Column(Order = 15), Required, Precision(18, 2)]
        public decimal ValorIncapPATMAT { get; set; } /* Valor a pagar por incapacidad de paternidad o maternidad */

        [Column(Order = 16), Required]
        public int HorasADCDiurnas { get; set; } /* Horas adicionales diunas */

        [Column(Order = 17), Required, Precision(18, 2)]
        public decimal ValorADCDiurnas { get; set; } /* Valor horas adicionales diurnas */

        [Column(Order = 18), Required]
        public int HorasNoctDom { get; set; } /* Cantidad de horas nocturnas en domingos */

        [Column(Order = 19), Required, Precision(18, 2)]
        public decimal ValorNoctDom { get; set; } /* Valor de horas nocturnas en domingos */

        [Column(Order = 20), Required]
        public int HorasExtDiurnasDom { get; set; } /* Cantidad de horas extra diurnas en los domingos */

        [Column(Order = 21), Required, Precision(18, 2)]
        public decimal ValorExtDiurnasDom { get; set; } /* Valor de horas extra diurnas en lso domingos */

        [Column(Order = 22), Required]
        public int HorasRecargo035 { get; set; } /* Cantidad de horas por recargo al 35% */

        [Column(Order = 23), Required, Precision(18, 2)]
        public decimal ValorRecargo035 { get; set; } /* Valor de horas por recargo al 35% */

        [Column(Order = 24), Required]
        public int HorasExtNocturnasDom { get; set; } /* Cantidad de horas extras nocturnas en los domingos */

        [Column(Order = 25), Required, Precision(18, 2)]
        public decimal ValorExtNocturnasDom { get; set; } /* Valor de horas extra nocturas en los domingos */

        [Column(Order = 26), Required]
        public int HorasRecargo075 { get; set; } /* Cantidad de horas por recargo al 75% */

        [Column(Order = 27), Required, Precision(18, 2)]
        public decimal ValorRecargo075 { get; set; } /* Valor de horas por recargo al 75% */

        [Column(Order = 28), Required]
        public int HorasRecargo100 { get; set; } /* Cantidad de horas por recargo al 100% */

        [Column(Order = 29), Required, Precision(18, 2)]
        public decimal ValorRecargo100 { get; set; } /* Valor de horas por recargo al 100% */

        [Column(Order = 30), Required, Precision(18, 2)]
        public decimal TarifaADC { get; set; } /* Valor de tarifa adicional */

        [Column(Order = 31), Required, Precision(18, 2)]
        public decimal ValorTotalADCComp { get; set; } /* Valor total de costos adicionales, horas extra, incapacidades, etc */

        [Column(Order = 32), Required, Precision(18, 2)]
        public decimal AuxTransporte { get; set; } /* Valor auxilio de transporte */

        [Column(Order = 33), Required, Precision(18, 2)]
        public decimal ProductividadSella { get; set; } /* Valor de productividad de sellado */

        [Column(Order = 34), Required, Precision(18, 2)]
        public decimal ProductividadExt { get; set; } /* Valor de productividad de extrusión */

        [Column(Order = 35), Required, Precision(18, 2)]
        public decimal ProductividadMontaje { get; set; } /* Valor de productividad de empaque */

        [Column(Order = 36), Required, Precision(18, 2)]
        public decimal Devengado { get; set; } /* Total de costo devengado por el trabajador */

        [Column(Order = 37), Required, Precision(18, 2)]
        public decimal EPS { get; set; } /* Valor de EPS */

        [Column(Order = 38), Required, Precision(18, 2)]
        public decimal AFP { get; set; } /* Valor de AFP */

        [Column(Order = 39), Required, Precision(18, 2)]
        public decimal Ahorro { get; set; } /* Valor de ahorro */

        [Column(Order = 40), Required, Precision(18, 2)]
        public decimal Prestamo { get; set; } /* Valor del pago de la cuota del prestamo */

        [Column(Order = 41), Required, Precision(18, 2)]
        public decimal Anticipo { get; set; } /* Valor pago de anticipo */

        [Column(Order = 42), Required, Precision(18, 2)]
        public decimal TotalDcto { get; set; } /* Total de descuentos */

        [Column(Order = 43), Required, Precision(18, 2)]
        public decimal PagoPTESemanaAnt { get; set; } /*  */

        [Column(Order = 44), Required, Precision(18, 2)]
        public decimal Dctos { get; set; } /* Valor de descuentos */

        [Column(Order = 45), Required, Precision(18, 2)]
        public decimal Deducciones { get; set; } /* Valor total de deducciones */

        [Column(Order = 46), Required, Precision(18, 2)]
        public decimal TotalPagar { get; set; } /* Valor total a pagar al trabajdor */

        [Column(Order = 47, TypeName = "varchar(max)")]
        public string Novedades { get; set; } /* Observación acerca de la nomina del trabajdor */

        [Column(Order = 48), Required]
        public int TipoNomina { get; set; } /* Llave foranea del tipo de nomina */
        public Tipos_Nomina? TiposNomina { get; set; } /* Campo que referencia al modelo de tipos de nomina */

        [Column(Order = 49), Required]
        public int Estado_Nomina { get; set; } /* Llave foranea que indicará el estado de la nomina: 11 será para los anticipos y cambiará a 13 cuando se haya pagado el anticipo. 13 cuando sera nomina común */
        public Estado? Estado { get; set; } /* Campo que referencia al modelo de estados */

        [Column(Order = 50), Required]
        public long Creador_Id { get; set; } /* Llave foranea del usuario que creó el registro de la nomina */
        public Usuario? Creador { get; set; } /* Campo que referencia al modelo de usuarios para traer información del usuario que creó la nomina */

        [Column(Order = 51, TypeName = "date"), Required]
        public DateTime Fecha { get; set; } /* Fecha en que se creó la nomina */

        [Column(Order = 52, TypeName = "varchar(20)"), Required, Precision(18, 2)]
        public string Hora { get; set; } /* Hora en que se creó la nomina */
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
