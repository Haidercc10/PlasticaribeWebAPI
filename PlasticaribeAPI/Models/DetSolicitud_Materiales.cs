using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Material que el solicitante eligió dentro de una subcategoría de la solicitud. La cantidad de la subcategoría
    /// (DetSolicitud_MatPrimaExtrusion) es la suma de sus materiales; lo entregado se sigue controlando por subcategoría.
    /// Según el tipo, se llena MatPri_Id (materia prima) o Tinta_Id (tinta).
    /// </summary>
    public class DetSolicitud_Materiales
    {
        [Key]
        public long DtSolMat_Id { get; set; }

        /// <summary>Detalle de la subcategoría al que pertenece el material.</summary>
        public long Codigo { get; set; }
        public DetSolicitud_MatPrimaExtrusion? DetSolicitud { get; set; }

        public long SolMpExt_Id { get; set; }
        public Solicitud_MatPrimaExtrusion? SolMatPriExt { get; set; }

        /// <summary>Tipo de material: 1 materia prima, 2 tinta.</summary>
        public TipoMaterialInventario DtSolMat_TipoMaterial { get; set; }

        public long? MatPri_Id { get; set; }
        public Materia_Prima? MatPri { get; set; }

        public long? Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }

        [Precision(14, 2)]
        public decimal DtSolMat_Cantidad { get; set; }
    }
}
