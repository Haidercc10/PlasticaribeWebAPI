using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Detalles_SalidasPeletizado
    {
        [Key]
        public long DtSalPel_Codigo { get; set; }

        public long SalPel_Id { get; set; }
        public Salidas_Peletizado? Salidas_Pele { get; set; }

        public long IngPel_Id { get; set; }
        public Ingreso_Peletizado? Ing_Pele { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string TpRecu_Id { get; set; }
        public Tipo_Recuperado? Tipo_Recuperado { get; set; } 

        [Precision(18, 2)]
        public decimal DtSalPel_Peso { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }
    }
}
