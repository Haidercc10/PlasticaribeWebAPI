using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Costos_Empresas_Anios
    {
        [Key]
        public int CostosEmp_Id { get; set; }
        public string CostosEmp_Descripcion { get; set; }
        public int Anio { get; set; }

        [Precision(14, 2)]
        public decimal Enero { get; set; }

        [Precision(14, 2)]
        public decimal Febrero { get; set; }

        [Precision(14, 2)]
        public decimal Marzo { get; set; }

        [Precision(14, 2)]
        public decimal Abril { get; set; }

        [Precision(14, 2)]
        public decimal Mayo { get; set; }

        [Precision(14, 2)]
        public decimal Junio { get; set; }

        [Precision(14, 2)]
        public decimal Julio { get; set; }

        [Precision(14, 2)]
        public decimal Agosto { get; set; }

        [Precision(14, 2)]
        public decimal Septiembre { get; set; }

        [Precision(14, 2)]
        public decimal Octubre { get; set; }

        [Precision(14, 2)]
        public decimal Noviembre { get; set; }

        [Precision(14, 2)]
        public decimal Diciembre { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
