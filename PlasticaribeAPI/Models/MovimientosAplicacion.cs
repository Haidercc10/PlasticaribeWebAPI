using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class MovimientosAplicacion
    {
        [Key]
        public int MovApp_Id { get; set; }
        public long Usua_Id { get; set; }
        public Usuario Usuario { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MovApp_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string MovApp_Descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime MovApp_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string MovApp_Hora { get; set; }
    }
}
