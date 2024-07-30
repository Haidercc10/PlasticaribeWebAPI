using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Detalles_PrecargueDespacho
    {
        [Key]
        public long DtlPcd_Codigo { get; set; }

        public long Pcd_Id { get; set; }
        public Precargue_Despacho? Precargue { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        public long DtlPcd_Rollo { get; set; }

        [Precision(18,2)]
        public decimal DtlPcd_Cantidad { get; set; }

        [Column(TypeName="varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }
    }
}
