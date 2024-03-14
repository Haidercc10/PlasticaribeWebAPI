using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Incapacidades
    {
        [Key, Column(Order = 1), Required]
        public int Id { get; set; }

        [Column(Order = 2), Required]
        public long Id_Trabajador { get; set; }
        public Usuario? Trabajador { get; set; }

        [Column(Order = 3, TypeName = "date"), Required]
        public DateTime FechaInicio { get; set; }

        [Column(Order = 4, TypeName = "date"), Required]
        public DateTime FechaFin { get; set; }

        [Column(Order = 5), Required]
        public int CantDias { get; set; }

        [Column(Order = 6), Required, Precision(18,2)]
        public decimal TotalPagar { get; set; }

        [Column(Order = 7), Required]
        public int Id_TipoIncapacidad { get; set; }
        public TipoIncapacidad? TipoIncapacidad { get; set; }

        [Column(Order = 8, TypeName = "varchar(max)")]
        public string Observacion { get; set; }

        [Column(Order = 9, TypeName = "date"), Required]
        public DateTime FechaRegistro { get; set; }

        [Column(Order = 10, TypeName = "varchar(50)"), Required]
        public string HoraRegistro { get; set; }

        [Column(Order = 11), Required]
        public long Creador_Id { get; set; }
        public Usuario? Creador { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
