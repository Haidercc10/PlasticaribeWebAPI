using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Producto
    {
        /** Los comentarios de este tipo estan relacionados con la revisión de
         las tablas para inserción de datos, en este caso se verifican campos de
        la TABLA ARTICULO EN BD INVENTARIO: ZEUS y TABLA CLIENTESOT_ITEM 
        EN BD PLASTICARIBE: BAGPRO*/

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Prod_Cod { get; set; } /** CODIGO |  */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Prod_Id { get; set; } /** IDARTICULO | ClienteItems */


        [Column(TypeName = "varchar(100)")]
        public string Prod_Nombre { get; set; } /** NOMBRE | ClienteItemsNom */

        [Column(TypeName = "text")]
        public string? Prod_Descripcion { get; set; } /** DESCRIPCION |   */

        //Llave tipos productos agregada. 
        public int TpProd_Id { get; set; } /** POSIBLE: TIPO ó CATEGORIA | PtFormatoPt */
        public Tipo_Producto? TpProd { get; set; } //Foranea tipos productos
        
        [Precision(14, 2)]
        public decimal Prod_Peso_Bruto { get; set; } /** PESOCONEMPAQUE | PtPesopt  */

        [Precision(14, 2)]
        public decimal Prod_Peso_Neto { get; set; } /** PESO | PtPesopt */

        public string UndMedPeso { get; set; } /** PRESENTACIÓN | PrPresentacionNom */
        public Unidad_Medida? UndMed1 { get; set; } //Foranea unidades medidas

        [Precision(14, 2)]
        public decimal? Prod_Fuelle { get; set; } /**  | PtFuelle */

        [Precision(14, 2)]
        public decimal? Prod_Ancho { get; set; } /**  | PtAnchopt */

        [Precision(14, 2)]
        public decimal? Prod_Calibre { get; set; } /**  | ExtCalibre */

        public string UndMedACF { get; set; } /**  |   */
        
        public Unidad_Medida? UndMed2 { get; set; } //Foranea unidades medidas

        public int? Estado_Id { get; set; } /** POSIBLE: DESHABILITADO | */
        
        //Foranea de estados en producto.    
        public Estado? Estado { get; set; } 

        public IList<PedidoProducto>? PedExtProd { get; set; }

        //Lista requerida para relación clientes-productos
        public IList<Cliente_Producto>? CliProd { get; set; } 
    }
}