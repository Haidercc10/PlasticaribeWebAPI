using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MovimientosAplicacion
    {
        [Key]
        public int MovApp_Id { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string MovApp_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string MovApp_Descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime MovApp_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string MovApp_Hora { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
