using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Activo
    {
        [Key]
        public int TpActv_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TpActv_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string? TpActv_Descripcion { get; set; }

    }
}
