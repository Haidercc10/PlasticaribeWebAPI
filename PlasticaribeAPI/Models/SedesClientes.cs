using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class SedesClientes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SedeCli_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SedeCli_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String SedeCli_Ciudad { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String SedeCli_CodPostal { get; set; }
        public Usuario Usu_Id { get; set; }
    }
}
