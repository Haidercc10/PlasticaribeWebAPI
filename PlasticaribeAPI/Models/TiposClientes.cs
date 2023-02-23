using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class TiposClientes

    {
        /** Los comentarios de este tipo estan relacionados con la revisión de
        las tablas para inserción de datos, en este caso se verifican campos de
        la TABLA TIPOCLIENTES EN BD INVENTARIO: ZEUS */

        [Key]
        public int TPCli_Id { get; set; } /** CODIGO */

        [Column(TypeName = "varchar(50)")]
        public String TPCli_Nombre { get; set; } /** NOMBRE */

        [Column(TypeName = "varchar(max)")]
        public String? TPCli_Descripcion { get; set; }

    }
}