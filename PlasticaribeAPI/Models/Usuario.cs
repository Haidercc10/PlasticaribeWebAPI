using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Usu_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Usu_Id { get; set; }

        public TipoIdentificacion TipoIdentificacion { get; set; }


        [Column(TypeName = "varchar(50)")]
        public String Usu_Nombre { get; set; }

        public Area Area { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public Rol Roles { get; set; }

        public Empresa Empresa { get; set; }

        public Estado Estado { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Usu_Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Usu_Telefono { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Usu_Contrasena { get; set; }

        public cajaCompensacion cajas_Compensacion { get; set; }

        public EPS EPS { get; set; }

        public fondoPension fondosPension { get; set; }


    }
}