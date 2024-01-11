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
        [Column(Order = 0)]
        public int Prod_Cod { get; set; } /** CODIGO |  */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public int Prod_Id { get; set; } /** IDARTICULO | ClienteItems */


        [Column(TypeName = "varchar(100)", Order = 2)]

        public string Prod_Nombre { get; set; } /** NOMBRE | ClienteItemsNom */

        [Column(TypeName = "varchar(max)")]
        public string? Prod_Descripcion { get; set; } /** DESCRIPCION |   */

        //Llave tipos productos agregada. 
        [Column(Order = 4)]
        public int TpProd_Id { get; set; } /** POSIBLE: TIPO ó CATEGORIA | PtFormatoPt */
        public Tipo_Producto? TpProd { get; set; } //Foranea tipos productos

        [Column(Order = 5)]
        [Precision(14, 2)]
        public decimal Prod_Peso_Millar { get; set; } /** PESOCONEMPAQUE | PtPesopt  */

        [Column(Order = 6)]
        [Precision(14, 2)]
        public decimal Prod_Peso { get; set; } /** PESO | PtPesopt */

        [Column(Order = 7)]
        public string UndMedPeso { get; set; } /** Unidad de medida del peso */
        public Unidad_Medida? UndMed1 { get; set; } //Foranea unidades medidas

        [Column(Order = 8)]
        [Precision(14, 2)]
        public decimal? Prod_Fuelle { get; set; } /**  | PtFuelle */

        [Column(Order = 9)]
        [Precision(14, 2)]
        public decimal? Prod_Ancho { get; set; } /**  | PtAnchopt */

        [Column(Order = 10)]
        [Precision(14, 2)]
        public decimal? Prod_Largo { get; set; } /**  | PtLargopt */

        [Column(Order = 11)]
        [Precision(14, 2)]
        public decimal? Prod_Calibre { get; set; } /**  | ExtCalibre */

        [Column(Order = 12)]
        public string UndMedACF { get; set; } /**  | ExtUnidadesNom  */

        [Column(Order = 13)]
        public Unidad_Medida? UndMed2 { get; set; } //Foranea unidades medidas


        [Column(Order = 14)]
        public int? Estado_Id { get; set; } /** POSIBLE: DESHABILITADO | */   //Foranea de estados en producto.


        public Estado? Estado { get; set; }

        [Column(Order = 15)]
        public int? Pigmt_Id { get; set; } //Llave Foranea Pigmentos
        public Pigmento? Pigmt { get; set; }


        [Column(Order = 16)]
        public int? Material_Id { get; set; } //Llave foranea Materiales_MatPrima
        public Material_MatPrima? MaterialMP { get; set; }


        //public IList<PedidoProducto>? PedExtProd { get; set; }

        //Lista requerida para relación clientes-productos
        //public IList<Cliente_Producto>? CliProd { get; set; }

        public int? TpSellado_Id { get; set; }
        public Tipos_Sellados? TiposSellados { get; set; }

        [Precision(14, 2)]
        public int? Prod_CantBolsasPaquete { get; set; }
        [Precision(14, 2)]
        public int? Prod_CantBolsasBulto { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Prod_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Prod_Hora { get; set; }

        [Precision(14, 2)]
        public decimal Prod_PrecioDia_Sellado { get; set; }
        [Precision(14, 2)]
        public decimal Prod_PrecioNoche_Sellado { get; set; }
        [Precision(14, 2)]
        public decimal Prod_Peso_Paquete { get; set; }
        [Precision(14, 2)]
        public decimal Prod_Peso_Bulto { get; set; }

        public IList<DetallesAsignacionProducto_FacturaVenta>? DtAsigProd_FVTA { get; set; }

        public IList<DetalleDevolucion_ProductoFacturado>? DtDevProd_Fact { get; set; }
    }
}