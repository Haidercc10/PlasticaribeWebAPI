using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipos_Impresion
    {
        [Key]
        public int TpImpresion_Id { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string TpImpresion_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string Tp_Impresion_Descripcion { get; set; }
    }
}
