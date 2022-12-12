using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Mantenimiento
    {
        [Key]
        public int TpMtto_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TpMtto_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string? TpMtto_Descripcion { get; set; }

    }
}
