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
        
        //Llave tipo ID agregada
        public string TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion? TipoIdentificacion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Usua_Nombre { get; set; }

        //Llave area agregada
        public long Area_Id { get; set; }
        public Area? Area { get; set; }

        //Llave tipo usuario agregada
        public int tpUsu_Id { get; set; }
        public Tipo_Usuario? tpUsu { get; set; }

        //Llave rol usuario agregada
        public int RolUsu_Id { get; set; }
        public Rol_Usuario? RolUsu { get; set; }

        //Llave empresa agregada
        public long Empresa_Id { get; set; }
        public Empresa? Empresa { get; set; }

        //Llave empresa agregada
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Usua_Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Usua_Telefono { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Usua_Contrasena { get; set; }

        //Llave caja compensacion agregada
        public long cajComp_Id { get; set; }
        public cajaCompensacion? cajComp { get; set; }

        //Llave eps agregada
        public long eps_Id { get; set; }
        public EPS? EPS { get; set; }

        //Llave fondo pensiones agregada
        public long fPen_Id { get; set; }
        public fondoPension? fPen { get; set; }
    }
}