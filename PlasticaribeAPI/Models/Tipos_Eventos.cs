using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipos_Eventos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TpEvento_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar(20)")]
        public string TpEvento_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TpEvento_Nombre { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string TpEvento_Descripcion { get; set; }
        
    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
}
