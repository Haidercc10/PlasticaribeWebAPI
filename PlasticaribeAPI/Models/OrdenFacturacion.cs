using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class OrdenFacturacion
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Factura { get; set; }
        public long Cli_Id { get; set; }
        public Clientes? Clientes { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Hora { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Observacion { get; set; }
    }
}
