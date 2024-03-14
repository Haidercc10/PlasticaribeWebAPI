using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TipoDevolucion_ProductoFacturado
    {


        [Key]
        public int TipoDevProdFact_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public int TipoDevProdFact_Nombre { get; set; }


        [Column(TypeName = "varchar(max)")]
        public int TipoDevProdFact_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
