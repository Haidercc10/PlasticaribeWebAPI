using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class cajaCompensacion
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cajComp_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long cajComp_Id { get; set; }
        /*public String TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }*/

        //llave foranea tipo ID agregada.
        public String TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion? TipoIdentificacion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String cajComp_Nombre { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String cajComp_Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String cajComp_Telefono { get; set; }

        public long cajComp_CuentaBancaria { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String cajComp_Direccion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String cajComp_Ciudad { get; set; }

    }
}

