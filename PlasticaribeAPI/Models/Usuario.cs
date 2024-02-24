using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Usuario
    {
        /**TEMPORAL: PARA USUARIOS VENDEDORES. 
         Los comentarios de este tipo estan relacionados con la revisión de
         las tablas para inserción de datos, en este caso se verifican campos de
         la TABLA MAEVENDE EN BD CONTABILIDAD: ZEUS */

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Usua_Codigo { get; set; } /** IDVENDE */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Usua_Id { get; set; } /** IDENTIFICACION */

        //Llave tipo ID agregada
        public string TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion? TipoIdentificacion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Usua_Nombre { get; set; } /** NOMBVENDE */

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
        public int Estado_Id { get; set; } /** DESHABILITADO */
        public Estado? Estado { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Usua_Email { get; set; } /** EMAIL */

        [Column(TypeName = "varchar(50)")]
        public String Usua_Telefono { get; set; } /** TELEFONO */

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

        [Column(TypeName = "Date")]
        public DateTime? Usua_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Usua_Hora { get; set; }

        public long? Usua_Cedula { get; set; } 
    }
}