using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Bodega
    {
        /** Los comentarios de este tipo estan relacionados con la revisión de
         las tablas para inserción de datos, en este caso se verifican campos de
        la TABLA BODEGAS EN BD INVENTARIO: ZEUS*/

        [Key]
        public int TpBod_Id { get; set; } /** CODIGO */

        [Column(TypeName = "varchar(50)")]
        public String TpBod_Nombre { get; set; } /** NOMBRE */

        [Column(TypeName = "varchar(max)")]
        public String TpBod_Descripcion { get; set; }

        //Llave foranea areas agregada.
        public long Area_Id { get; set; }
        public Area? Area { get; set; }
    }
}
