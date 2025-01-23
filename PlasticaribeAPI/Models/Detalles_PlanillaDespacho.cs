using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Detalles_PlanillaDespacho
    {
        [Key]
        public int DtPla_Codigo { get; set; }

        public int Pla_Id { get; set; }
        public Planillas_Despacho? Planilla {  get; set; }


        public long Cli_Id { get; set; }
        public Clientes? Cli{ get; set; }


        [Column(TypeName = "varchar(50)")]
        public string DtPla_Factura { get; set; }


        [Precision(18, 2)]
        public decimal DtPla_ValorFactura { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string DtPla_FormaPago { get; set; }

        public int DtPla_UnidadesProducto { get; set; }

        [Precision(18, 2)]
        public decimal DtPla_PesoBruto { get; set; }
    }
}
