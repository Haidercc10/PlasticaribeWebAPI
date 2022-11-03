using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetallesIngRollos_Extrusion
    {
        [Key]
        public int DtIngRollo_Id { get; set; }
        public long IngRollo_Id { get; set; }
        public IngresoRollos_Extrusion? IngresoRollos_Extrusion { get; set; }
        public int Rollo_Id { get; set; }

        [Precision(18, 2)]
        public decimal DtIngRollo_Cantidad { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }
        public int DtIngRollo_OT { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }
    }
}
