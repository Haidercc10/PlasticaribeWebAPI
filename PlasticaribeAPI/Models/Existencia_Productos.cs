using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Existencia_Productos
    {
        [Key]
        public long ExProd_Id { get; set; }

        // Llave foranea Producto agregada
        [Column(Order = 1)]
        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }

        [Column(Order = 2)]
        [Precision(18, 2)]
        public decimal ExProd_Cantidad { get; set; }

        // Llave foranea unidad medida agregada.
        [Column(Order = 3)]
        public String UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        // Llave foranea Tipo Bodega agregada.
        [Column(Order = 4)]
        public int TpBod_Id { get; set; }
        public Tipo_Bodega? TpBod { get; set; }

        [Precision(18, 2)]
        public decimal ExProd_Precio { get; set; }

        [Precision(18, 2)]
        public decimal ExProd_PrecioExistencia { get; set; }

        [Precision(18, 2)]
        public decimal? ExProd_PrecioSinInflacion { get; set; }

        [Precision(18, 2)]
        public string? ExProd_PrecioTotalFinal { get; set; }

        // Llave foranea Tipo Moneda agregada.
        public String TpMoneda_Id { get; set; }
        public Tipo_Moneda? TpMoneda { get; set; }
    }
}
