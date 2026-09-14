using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DetSolicitud_MatPrimaExtrusion
    {
        [Key]
        public long Codigo { get; set; }

        public long SolMpExt_Id { get; set; }
        public Solicitud_MatPrimaExtrusion? SolMatPriExt { get; set; }

        public int SubCatMP_Id { get; set; }
        public Subcategorias_MatPrima? SubCatMP { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string SubCatMP_Nombre { get; set; }

        [Precision(14, 2)]
        public decimal DtSolMpExt_Cantidad { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
