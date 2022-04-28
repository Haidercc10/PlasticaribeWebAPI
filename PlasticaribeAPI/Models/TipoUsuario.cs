using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class TipoUsuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoUsuario_Codigo { get; set; }

        [Key]
        [Column(TypeName = "varchar(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public String TipoUsuario_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String TipoUsuario_Nombre { get; set; }

        [Column(TypeName = "varchar(200)")]
        public String? TipoUsuario_Descripcion { get; set; }
    }
}
