using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Clientes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cli_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Cli_Id { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Cli_Nombre { get; set; }

        [Column(TypeName = "varchar(60)")]
        public String Cli_Direccion { get; set; }

        [Column(TypeName = "bigint")]
        public long Cli_Telefono { get; set; }

        [Column(TypeName = "varchar(60)")]
        public String Cli_Email { get; set; }
        public TiposClientes TPCli { get; set; }
    }
}
