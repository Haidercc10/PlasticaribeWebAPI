using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Producto
    {

        /** Los comentarios de este tipo estan relacionados con la revisión de
         las tablas para inserción de datos, en este caso se verifican campos de
        la TABLA TIPOSARTICULOS EN BD INVENTARIO: ZEUS*/

        [Key]
        public int TpProd_Id { get; set; } /** CODIGO  */

        [Column(TypeName = "varchar(100)")]
        public String TpProd_Nombre { get; set; } /** NOMBRE */

        [Column(TypeName = "varchar(max)")]
        public String TpProd_Descripcion { get; set; } 
    }
}
