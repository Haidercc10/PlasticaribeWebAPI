using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Rol
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Rol_Codigo { get; set; }

        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int Rol_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Rol_Nombre { get; set; }

        [Column(TypeName = "varchar(200)")]
        public String? Rol_Descripcion { get; set; }

    }
}
