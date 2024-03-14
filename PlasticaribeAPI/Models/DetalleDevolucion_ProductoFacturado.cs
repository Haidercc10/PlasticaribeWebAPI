using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DetalleDevolucion_ProductoFacturado
    {
        [Key]
        public int DtDevProdFact_Id { get; set; }

        public long DevProdFact_Id { get; set; }
        public Devolucion_ProductoFacturado? DevolucionProdFact { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }


        public long Rollo_Id { get; set; }


        [Precision(14, 2)]
        public decimal DtDevProdFact_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }



    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
