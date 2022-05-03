using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Existencia_Producto
    {

        [Key]
        public long ExProd_Id { get; set; }

        public Producto Prod { get; set; }

        public Tipo_Bodega TpBod { get; set; }

        [Precision(18, 2)]
        public decimal ExProd_Cantidad { get; set; }
        
        public Unidad_Medida UndMed { get; set; }

        [Precision(18,2)]
        public decimal ExProd_Precio { get; set; }

        [Precision(18, 2)]
        public decimal ExProd_PrecioExistencia { get; set; }
        
        [Precision(18, 2)]
        public decimal? ExProd_PrecioSinInflacion { get; set; }

        [Precision(18, 2)]
        public string? ExProd_PrecioTotalFinal { get; set; }
    }
}
