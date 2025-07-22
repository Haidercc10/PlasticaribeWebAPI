using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Devolucion_ProductoFacturado
    {

        [Key]
        public long DevProdFact_Id { get; set; }

        public int? Id_OrdenFact { get; set; }
        public OrdenFacturacion? Orden_Fact { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FacturaVta_Id { get; set; }

        public bool? DevProdFact_Reposicion { get; set; }

        public bool? DevProdFact_NotaCredito { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Cliente { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? DevProdFact_Responsable { get; set; }

        public long? Asesor_Id { get; set; }
        public Usuario? Asesor_ComercialDv { get; set; }

        public int? Estado_Id { get; set; }
        public Estado? Estados { get; set; }
        

        //Usuario que crea la devolución
        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }

        [Column(TypeName = "date")]
        public DateTime DevProdFact_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? DevProdFact_Hora { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? DevProdFact_Observacion { get; set; }

        //Usuario que modifica la devolución
        public long UsuaModifica_Id { get; set; }
        public Usuario? UsuaModificaDv { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DevProdFact_FechaModificado { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? DevProdFact_HoraModificado { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? DevProdFact_ObservacionModificado { get; set; }

        //Usuario que gestiona la devolución
        public long? UsuaGestiona_Id { get; set; }
        public Usuario? Usua_Gestion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DevProdFact_FechaGestion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? DevProdFact_HoraGestion { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? DevProdFact_ObservacionGestion { get; set; }

        //Usuario que finaliza la devolución
        public long? UsuaFinaliza_Id { get; set; }
        public Usuario? UsuaFinalizaDv { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DevProdFact_FechaFinalizado { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? DevProdFact_HoraFinalizado { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? DevProdFact_ObservacionFinal { get; set; }
        public long? Reposicion_Id { get; set; }
        public Reposiciones? Reposicion { get; set; }

        //Llave foránea de tipo devolución de producto facturado
        public int TipoDevProdFact_Id { get; set; }
        public TipoDevolucion_ProductoFacturado? TipoDevolucionPF { get; set; }
        public IList<DetalleDevolucion_ProductoFacturado>? DtDevProd_Fact { get; set; }

        //[Column(TypeName = "varchar(10)")]
        //public string DevProd_Hora { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
