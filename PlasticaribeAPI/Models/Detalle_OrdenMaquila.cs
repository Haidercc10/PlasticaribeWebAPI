using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Detalle_OrdenMaquila
    {
        [Key]
        public long DtOM_Codigo { get; set; }
        public long OM_Id { get; set; }
        public Orden_Maquila? Orden_Maquila { get; set; }
        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPrima { get; set; }
        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }
        public long BOPP_Id { get; set; }
        public BOPP? BOPP { get; set; }
        
        [Precision(18, 2)]
        public decimal DtOM_Cantidad { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        [Precision(18, 2)]
        public decimal DtOM_PrecioUnitario { get; set; }
    }
}
