using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Detalles_BodegasRollos
    {
        [Key]
        public long Codigo { get; set; }
        public long BgRollo_Id { get; set; }
        public Bodegas_Rollos? Bodegas_Rollos { get; set; }
        public long BgRollo_OrdenTrabajo { get; set; }
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }
        public long DtBgRollo_Rollo { get; set; }

        [Precision(14, 2)]
        public decimal DtBgRollo_Cantidad { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }
        public string BgRollo_BodegaActual { get; set; }
        public Proceso? Bodega_Actual { get; set; }
        public bool DtBgRollo_Extrusion { get; set; }
        public bool DtBgRollo_ProdIntermedio { get; set; }
        public bool DtBgRollo_Impresion { get; set; }
        public bool DtBgRollo_Rotograbado { get; set; }
        public bool DtBgRollo_Sellado { get; set; }
        public bool DtBgRollo_Corte { get; set; }
        public bool DtBgRollo_Despacho { get; set; }
        public int? Estado_Id { get; set; }
        public Estado? Estados { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? DtBgRollo_Ubicacion { get; set; }

        public string? BgRollo_BodegaInicial { get; set; }
        public Proceso? Bodega_Inicial { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
