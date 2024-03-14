using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipo_Moneda
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TpMoneda_Codigo { get; set; }

        [Key]
        [Column(TypeName = "varchar(50)")]
        public String TpMoneda_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String TpMoneda_Nombre { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
