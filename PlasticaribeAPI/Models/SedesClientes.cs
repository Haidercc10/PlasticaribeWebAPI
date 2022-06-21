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
        public long SedeCli_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String SedeCliente_Ciudad { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public String SedeCliente_Direccion { get; set; }

        [Column(TypeName = "bigint")]
        public long? SedeCli_CodPostal { get; set; } //Colocar NUllable.
        public long Cli_Id { get; set; }
        public Clientes? Cli { get; set; }
    }
}
