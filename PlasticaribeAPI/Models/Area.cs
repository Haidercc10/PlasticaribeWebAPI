using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Area
    {
        
        [Key]              
        public int area_Id { get; set; }
               
        public int area_Codigo { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public String area_Nombre { get; set; }

        [Column(TypeName = "varchar(200)")]
        public String? area_Descripcion { get; set; }
    }
}
