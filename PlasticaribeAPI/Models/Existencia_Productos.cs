using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Existencia_Productos
    {
        /** Los comentarios de este tipo estan relacionados con la revisión de
         las tablas para inserción de datos, en este caso se verifican campos de
        la TABLA EXISTENCIA EN BD INVENTARIO: ZEUS*/

        [Key]
        public long ExProd_Id { get; set; } /** CODIGO */

        // Llave foranea Producto agregada
        [Column(Order = 1)]
        public int Prod_Id { get; set; } /** ARTICULO */
        public Producto? Prod { get; set; }

        [Column(Order = 2)]
        [Precision(18, 2)]
        public decimal ExProd_Cantidad { get; set; } /** EXISTENCIAS */

        // Llave foranea unidad medida agregada.
        [Column(Order = 3)]
        public String UndMed_Id { get; set; } 
        public Unidad_Medida? UndMed { get; set; }

        // Llave foranea Tipo Bodega agregada.
        [Column(Order = 4)]
        public int TpBod_Id { get; set; } /** BODEGA */
        public Tipo_Bodega? TpBod { get; set; }

        [Precision(18, 2)]
        public decimal ExProd_Precio { get; set; } /** VALOR */

        [Precision(18, 2)]
        public decimal ExProd_PrecioExistencia { get; set; } /** VALOR 2  */ 

        [Precision(18, 2)]
        public decimal? ExProd_PrecioSinInflacion { get; set; } /** VALORSININFLACION */

       /* [Precision(18, 2)]
        public decimal? ExProd_PrecioTotalFinal { get; set; } */ /** VALOR 2 */

        // Llave foranea Tipo Moneda agregada.
        public String TpMoneda_Id { get; set; } 
        public Tipo_Moneda? TpMoneda { get; set; }
       
        [Precision(18,2)]
        public decimal? ExProd_PrecioVenta { get; set; } /** PrecioVenta Tabla Articulo */

        [Precision(18,2)]
        public decimal ExProd_CantMinima { get; set; }

        //[Precision(14, 2)]
        //public int ExProd_CantBolsasPaquete { get; set; }

        [Precision(14, 2)]
        public int ExProd_CantBolsasBulto { get; set; }

    }
}
