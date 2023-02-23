using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Recuperado
    {

        [Key]
        [Column(TypeName = "varchar(10)")]
        public string TpRecu_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TpRecu_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? TpRecu_Descripcion { get; set; }

    }
}
