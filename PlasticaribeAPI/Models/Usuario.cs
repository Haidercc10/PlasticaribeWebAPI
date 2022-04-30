using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Usua_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Usua_Id { get; set; }

        public TipoIdentificacion TipoIdentificacion { get; set; }


        [Column(TypeName = "varchar(50)")]
        public String Usua_Nombre { get; set; }

        public Area Area { get; set; }

        public Tipo_Usuario tpUsu { get; set; }

        public Rol_Usuario RolUsu { get; set; }

        public Empresa Empresa { get; set; }

        public Estado Estado { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Usua_Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Usua_Telefono { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Usua_Contrasena { get; set; }

        public cajaCompensacion cajComp { get; set; }

        public EPS EPS { get; set; }

        public fondoPension fPen { get; set; }

        
    }
}