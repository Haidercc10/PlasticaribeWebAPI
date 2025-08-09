using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Requerimientos_Calidad
    {
        [Key]
        public int Req_Id { get; set; } /** ID Requerimiento */

        [Column(TypeName = "varchar(100)")]
        public string Req_Nombre { get; set; } /** Nombre del requerimiento */

        [Column(TypeName = "varchar(max)")]
        public string? Req_Descripcion { get; set; } /** Descripción del requerimiento */
        
        [Column(TypeName = "date")]
        public DateTime Req_FechaCreacion { get; set; } /** Fecha de creación del requerimiento */
        
        [Column(TypeName = "varchar(10)")]
        public string Req_HoraCreacion { get; set; } /** Fecha de última modificación del requerimiento */
    }
}
