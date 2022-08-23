using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Laminado_Capa
    {
        [Key]
        public int LamCapa_Id { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string LamCapa_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string LamCapa_Descripcion { get; set; }
    }
}
