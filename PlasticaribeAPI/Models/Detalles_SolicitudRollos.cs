using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Detalles_SolicitudRollos
    {
        [Key]
        public long Codigo { get; set; }
        public long SolRollo_Id { get; set; }
        public Solicitud_Rollos_Areas? SolicitudRollos { get; set; }
        public long DtSolRollo_OrdenTrabajo { get; set; }
        public long DtSolRollo_Maquina { get; set; }
        public string DtSolRollo_BodegaSolicitante { get; set; }
        public Proceso? Bodega_Solicitante { get; set; }
        public string DtSolRollo_BodegaSolicitada { get; set; }
        public Proceso? Bodega_Solicitada { get; set; }
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }
        public long DtSolRollo_Rollo { get; set; }

        [Precision(14, 2)]
        public decimal DtSolRollo_Cantidad { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }

        public int? Falla_Id { get; set; }
        public Falla_Tecnica? Fallas { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
