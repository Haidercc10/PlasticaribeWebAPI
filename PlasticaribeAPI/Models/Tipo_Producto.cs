using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Producto
    {
        [Key]
        public int TpProd_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String TpProd_Nombre { get; set; }

        [Column(TypeName = "text")]
        public String TpProd_Descripcion { get; set; }
    }
}
