using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Rol_Usuario
    {

        [Key]
        public int RolUsu_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String RolUsu_Nombre { get; set; }

        [Column(TypeName = "varchar(200)")]
        public String? RolUsu_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
