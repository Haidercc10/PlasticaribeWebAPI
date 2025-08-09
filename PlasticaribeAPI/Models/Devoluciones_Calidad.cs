using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Devoluciones_Calidad
    {
        [Key]
        public long Dvc_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Dvc_Fecha { get; set; }

        public int Dvc_Ano { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Dvc_Mes { get; set; }

        public int Dvc_OT { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Cliente { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        public int Falla_Id { get; set; }
        public Falla_Tecnica? Fallas { get; set; }

        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }

        public int Req_Id { get; set; }
        public Requerimientos_Calidad? Requerimiento { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Dvc_TipoRechazo { get; set; }

        [Precision(18, 2)]
        public decimal Dvc_PesoBruto { get; set; }

        [Precision(18, 2)]
        public decimal Dvc_PesoNeto { get; set; }

        [Precision(18, 2)]
        public decimal Dvc_Precio { get; set; }

        [Precision(18, 2)]
        public decimal Dvc_Subtotal { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Dvc_FechaProduccion { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? Dvc_Observacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime Dvc_FechaRegistro { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Dvc_Hora { get; set; }
    }
}
