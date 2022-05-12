using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class fondoPension
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fPen_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long fPen_Id { get; set; }

        //Llave foranea tipo identificacion agregada
        public String TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }


        [Column(TypeName = "varchar(50)")]
        public String fPen_Nombre { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String fPen_Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String fPen_Telefono { get; set; }

        public long fPen_CuentaBancaria { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String fPen_Direccion { get; set; }


        [Column(TypeName = "varchar(50)")]
        public String fPen_Ciudad { get; set; }
    }

}

