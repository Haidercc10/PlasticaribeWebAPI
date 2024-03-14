using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipos_Conceptos
    {
        [Key]
        public int TpCcpto_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TpCcpto_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string TpCcpto_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
