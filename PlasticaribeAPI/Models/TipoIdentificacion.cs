using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TipoIdentificacion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoIdentificacion_Codigo { get; set; }

        [Key]
        [Column(TypeName = "varchar(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public String TipoIdentificacion_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String TipoIdentificacion_Nombre { get; set; }

        [Column(TypeName = "varchar(200)")]
        public String? TipoIdentificacion_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
