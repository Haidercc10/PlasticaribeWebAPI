using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Clientes

    {
        /** Los comentarios de este tipo estan relacionados con la revisión de
         las tablas para inserción de datos en este caso se verifican campos de
        la TABLA CLIENTE EN BD CONTABILIDAD: ZEUS*/

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cli_Codigo { get; set; } /** POSIBLE: CODALTERNO */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Cli_Id { get; set; } /** OK IDCLIENTE */

        public string TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion? TipoIdentificacion { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public String Cli_Nombre { get; set; } /** OK RAZONCIAL */

        /*[Column(TypeName = "varchar(60)")]
        public String Cli_Direccion { get; set; }*/

        [Column(TypeName = "varchar(60)")]
        public String Cli_Telefono { get; set; } /** OK TELEFONO */


        [Column(TypeName = "varchar(MAX)")]
        public String Cli_Email { get; set; } /** OK EMAIL*/


        [Column(TypeName = "int")]
        public int TPCli_Id { get; set; } /** OK TIPOCLIENTE TABLA TIPOCLIENTES*/

        public TiposClientes? TPCli { get; set; }
        public long usua_Id { get; set; } /** OK IDVENDE TABLA 'MAEVENDE' */

        public Usuario? Usua { get; set; }

        //Llave foranea de estado en Clientes
        public int? Estado_Id { get; set; } /** POSIBLE: DESHABILITADO */
        public Estado? Estado { get; set; }

        //Lista requerida para relación clientes-productos
        //public IList<Cliente_Producto>? CliProd { get; set; } /**POSIBLE: CLIENTEOT_ITEMS */

        [Column(TypeName = "date")]
        public DateTime? Cli_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Cli_Hora { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}