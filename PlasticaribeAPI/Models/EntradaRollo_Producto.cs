using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class EntradaRollo_Producto
    {
        [Key]
        public long EntRolloProd_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime EntRolloProd_Fecha { get; set; }    


        [Column(TypeName = "text")]
        public string? EntRolloProd_Observacion { get; set; }


        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }

    }
}
