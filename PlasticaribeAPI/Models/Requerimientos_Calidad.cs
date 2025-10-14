using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Requerimientos_Calidad
    {
        [Key]
        public int Req_Id { get; set; } 

        [Column(TypeName = "varchar(100)")]
        public string Req_Nombre { get; set; } 

        [Column(TypeName = "varchar(max)")]
        public string? Req_Descripcion { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime Req_FechaCreacion { get; set; } 
        
        [Column(TypeName = "varchar(10)")]
        public string Req_HoraCreacion { get; set; } 
    }
}
