using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class TiposClientes
    {
        [Key]
        public int TPCli_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String TPCli_Nombre { get; set; }

        [Column(TypeName = "text")]
        public String? TPCli_Descripcion { get; set; }

    }
}