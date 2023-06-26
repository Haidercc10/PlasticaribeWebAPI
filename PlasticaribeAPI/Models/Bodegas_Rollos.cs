using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Bodegas_Rollos
    {
        [Key]
        public long BgRollo_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime BgRollo_FechaEntrada { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string BgRollo_HoraEntrada { get; set; }

        [Column(TypeName = "date")]
        public DateTime BgRollo_FechaModifica { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string BgRollo_HoraModifica { get; set; }
        public long BgRollo_Rollo { get; set; }

        [Precision(14, 2)]
        public decimal BgRollo_Cantidad { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }
        public string BgRollo_BodegaActual { get; set; }
        public Proceso? Bodega_Actual { get; set; }
        public bool BgRollo_Extrusion { get; set; }
        public bool BgRollo_ProdIntermedio { get; set; }
        public bool BgRollo_Impresion { get; set; }
        public bool BgRollo_Rotograbado { get; set; }
        public bool BgRollo_Sellado { get; set; }
        public bool BgRollo_Corte { get; set; }
        public bool BgRollo_Despacho { get; set; }
        public string BgRollo_Observacion { get; set; }
    }
}
