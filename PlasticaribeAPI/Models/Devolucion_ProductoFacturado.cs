using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Devolucion_ProductoFacturado
    {

        [Key]
        public long DevProdFact_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string FacturaVta_Id { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Cliente { get; set; }


        [Column(TypeName = "date")]
        public DateTime DevProdFact_Fecha { get; set; }


        [Column(TypeName = "text")]
        public string? DevProdFact_Observacion { get; set; }


        public int TipoDevProdFact_Id { get; set; }
        public TipoDevolucion_ProductoFacturado? TipoDevolucionPF { get; set; }

        public IList<DetalleDevolucion_ProductoFacturado>? DtDevProd_Fact { get; set; }

        public long Usua_Id {get; set;}
        public Usuario? Usua { get; set; }

        //[Column(TypeName = "varchar(10)")]
        //public string DevProd_Hora { get; set; }
    }
}
