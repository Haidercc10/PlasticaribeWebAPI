using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DetallesAsgRollos_Extrusion
    {
        [Key]
        public int DtAsgRollos_Id { get; set; }
        public int AsgRollos_Id { get; set; }
        public AsignacionRollos_Extrusion? AsignacionRollos { get; set; }
        public long DtAsgRollos_OT { get; set; }
        public long Rollo_Id { get; set; }

        [Precision(18, 2)]
        public decimal? DtAsgRollos_Cantidad { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
