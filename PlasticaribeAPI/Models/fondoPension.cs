using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class fondoPension
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fPen_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long fPen_Id { get; set; }

        //Llave foranea tipo identificacion agregada
        public String TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion? TipoIdentificacion { get; set; }


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

        [Column(TypeName = "Date")]
        public DateTime? fPen_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? fPen_Hora { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}

