using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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

        [Column(TypeName = "Date")]
        public DateTime? SedeCli_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? SedeCli_Hora { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? SedeCli_CodBagPro { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
