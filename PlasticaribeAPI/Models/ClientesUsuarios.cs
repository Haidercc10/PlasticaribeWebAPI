using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PlasticaribeAPI.Models
{
    public class ClientesUsuarios
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CliUsu_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CliUsu_Id { get; set; }
        public Clientes Cli { get; set; }
        public Usuario Usua { get; set; }
    }
}
