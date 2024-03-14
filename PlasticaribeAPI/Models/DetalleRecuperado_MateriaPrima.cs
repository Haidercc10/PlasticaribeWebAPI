using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DetalleRecuperado_MateriaPrima
    {
        [Key]
        public long Codigo { get; set; }

        public long RecMp_Id { get; set; }
        public Recuperado_MatPrima? RecMp { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPri { get; set; }

        [Precision(18, 2)]
        public decimal RecMatPri_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string TpRecu_Id { get; set; }
        public Tipo_Recuperado? TpRecu { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
