
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Servicios_Produccion
    {
        [Key]
        public int SvcProd_Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string SvcProd_Nombre { get; set;  }

        [Column(TypeName = "varchar(max)")]
        public string? SvcProd_Descripcion { get; set; }

        [Precision(18, 2)]
        public decimal SvcProd_ValorDia { get; set; }

        [Precision(18, 2)]
        public decimal SvcProd_ValorNoche { get; set; }

        [Precision(18, 2)]
        public decimal SvcProd_ValorDomFest { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Crea { get; set; }
        public Proceso? Proceso1 { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Solicita { get; set; }
        public Proceso? Proceso2 { get; set; }

        public int Material_Id { get; set; }
        public Material_MatPrima? Materiales { get; set; }

        public bool Impreso { get; set; }

    }
}
