using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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

        [Column(TypeName = "varchar(10)")]
        public string? DevProdFact_Hora { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? DevProdFact_Observacion { get; set; }


        public int TipoDevProdFact_Id { get; set; }
        public TipoDevolucion_ProductoFacturado? TipoDevolucionPF { get; set; }

        public IList<DetalleDevolucion_ProductoFacturado>? DtDevProd_Fact { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }

        public int? Id_OrdenFact { get; set; }
        public OrdenFacturacion? Orden_Fact { get; set; }

        public int? Estado_Id { get; set; }
        public Estado? Estados { get; set; }

        public bool? DevProdFact_Reposicion { get; set; }


        [Column(TypeName = "date")]
        public DateTime? DevProdFact_FechaModificado { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? DevProdFact_HoraModificado { get; set; }


        [Column(TypeName = "date")]
        public DateTime? DevProdFact_FechaFinalizado { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? DevProdFact_HoraFinalizado { get; set; }

        public long UsuaModifica_Id { get; set; }
        public Usuario? UsuaModificaDv { get; set; }

        //[Column(TypeName = "varchar(10)")]
        //public string DevProd_Hora { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
