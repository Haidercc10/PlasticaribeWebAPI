using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetSolicitud_MatPrimaExtrusion
    {
        [Key]
        public long Codigo { get; set; }

        public long SolMpExt_Id { get; set; }
        public Solicitud_MatPrimaExtrusion? SolMatPriExt { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPrima { get; set; }

        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }

        [Precision(14, 2)]
        public decimal DtSolMpExt_Cantidad { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }
    }
}
