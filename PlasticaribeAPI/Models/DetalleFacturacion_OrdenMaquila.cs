using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DetalleFacturacion_OrdenMaquila
    {
        [Key]
        public long DtFacOM_Codigo { get; set; }
        public long FacOM_Id { get; set; }
        public Facturacion_OrdenMaquila? FacOM { get; set; }
        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPrima { get; set; }
        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }
        public long Bopp_Id { get; set; }
        public BOPP? BOPP { get; set; }

        [Precision(14, 2)]
        public decimal DtFacOM_Cantidad { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        [Precision(18, 2)]
        public decimal DtFacOM_ValorUnitario { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
