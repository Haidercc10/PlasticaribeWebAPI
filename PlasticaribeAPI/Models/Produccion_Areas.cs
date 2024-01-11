using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Produccion_Areas
    {
        [Key]
        public int Id { get; set; }
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }

        public int Anio_Produccion {  get; set; }

        [Precision(18, 2)]
        public decimal Meta_Enero {  get; set; }
        [Precision(18, 2)]
        public decimal Producido_Enero { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Febrero { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Febrero { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Marzo { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Marzo { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Abril { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Abril { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Mayo { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Mayo { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Junio { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Junio { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Julio { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Julio { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Agosto { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Agosto { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Septiembre { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Septiembre { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Octubre { get; set; }
        [Precision(18, 2)]
        public decimal Producido_OCtubre { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Noviembre { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Noviembre { get; set; }

        [Precision(18, 2)]
        public decimal Meta_Diciembre { get; set; }
        [Precision(18, 2)]
        public decimal Producido_Diciembre { get; set; }
    }
}
