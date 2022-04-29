using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PlasticaribeAPI.Models
{
    public class EPS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
        public int eps_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long eps_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String eps_Nombre { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String eps_Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String eps_Telefono { get; set; }
        
        public long eps_CuentaBancaria { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String eps_Direccion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String eps_Ciudad { get; set; }
    }
}
