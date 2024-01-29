using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class ReImpresionEtiquetas
    {
        [Key]
        public int Id { get; set; }
        public long Orden_Trabajo { get; set; }
        public long NumeroRollo_BagPro { get; set; }
        public Proceso? Proceso { get; set; }
        public string Proceso_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Hora { get; set; }
        public Usuario? Usuario { get; set; }
        public long Usua_Id { get; set; }
    }
}
