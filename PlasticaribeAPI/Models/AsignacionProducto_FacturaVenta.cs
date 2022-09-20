using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class AsignacionProducto_FacturaVenta
    {

        [Key]
        public long AsigProdFV_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string? FacturaVta_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string? NotaCredito_Id { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }


        [Column(TypeName = "date")]
        public DateTime AsigProdFV_Fecha { get; set; }


        [Column(TypeName = "text")]
        public string? AsigProdFV_Observacion { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Cliente { get; set; }


        public long Usua_Conductor { get; set; }
        public Usuario? Usuario { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string AsigProdFV_PlacaCamion { get; set; }

        public IList<DetallesAsignacionProducto_FacturaVenta>? DtAsigProd_FVTA { get; set; }

    }
}
