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
        /*public String TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }*/

        //Llave foranea tipo identificacion agregada. 
        public String TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion? TipoIdentificacion{ get; set; }

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

        [Column(TypeName = "Date")]
        public DateTime? eps_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? eps_Hora { get; set; }
    }
}
