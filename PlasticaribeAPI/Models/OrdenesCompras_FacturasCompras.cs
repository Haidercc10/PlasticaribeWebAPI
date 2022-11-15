using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class OrdenesCompras_FacturasCompras
    {
        public long Oc_Id { get; set; }
        public Orden_Compra? Orden_Compra { get; set; }

        public long Facco_Id { get; set; }
        public Factura_Compra? Facco { get; set; }
    }
}
