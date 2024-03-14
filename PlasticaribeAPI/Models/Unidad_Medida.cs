using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Unidad_Medida
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UndMed_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar(10)")]
        public String UndMed_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String UndMed_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public String UndMed_Descripcion { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
