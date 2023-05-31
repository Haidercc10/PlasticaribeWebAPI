using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Orden_Compra
    {
        [Key]
        public long Oc_Id { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }


        [Column(TypeName = "date")]
        public DateTime Oc_Fecha { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Oc_Hora { get; set; }


        public long Prov_Id { get; set; }
        public Proveedor? Proveedor { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }


        [Precision(18, 2)]
        public decimal Oc_ValorTotal { get; set; }

        [Precision(18, 2)]
        public decimal Oc_PesoTotal { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string TpDoc_Id { get; set; }
        public Tipo_Documento? TipoDoc { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Oc_Observacion { get; set; }

        public int IVA { get; set; }
        //public IList<OrdenesCompras_FacturasCompras>? OrdenFactura { get; set; }
        //public IList<Remision_OrdenCompra>? OrdenRemision { get; set; }
    }
}
