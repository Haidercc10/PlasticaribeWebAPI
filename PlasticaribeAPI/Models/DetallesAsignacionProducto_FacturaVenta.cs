using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetallesAsignacionProducto_FacturaVenta
    {

        [Key]
        public long DtAsigProdFV_Id { get; set; }

        public long AsigProdFV_Id { get; set; }
        public AsignacionProducto_FacturaVenta? AsigProducto_FV { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }

        public long Rollo_Id { get; set; }

        [Precision(14, 2)]
        public decimal DtAsigProdFV_Cantidad { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }

        [Precision(14, 2)]
        public decimal Prod_CantidadUnidades { get; set; }
    }
}
