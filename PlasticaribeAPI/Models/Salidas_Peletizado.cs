using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Salidas_Peletizado
    {
        [Key]
        public long SalPel_Id { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPrima { get; set; }

        [Precision(18, 2)]
        public decimal SalPel_Peso { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime SalPel_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string SalPel_Hora { get; set; }

        [Column(TypeName = "date")]
        public DateTime SalPel_FechaAprobado { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string SalPel_HoraAprobado { get; set; }

        public int Estado_Id { get; set; }
        public Estado? Estados { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? SalPel_Observacion { get; set; }

        public long Usua_Aprueba { get; set; }
        public Usuario? Usuario2 { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }

    }
}
