using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Detalles_AsignacionRollosOT
    {
        [Key]
        public long DtlAsgRll_Codigo { get; set; }

        public long AsgRll_Id { get; set; }
        public Asignacion_RollosOT? AsgRollosOT { get; set; }

        public long DtAsgRll_OT { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        public long Rollo_Id { get; set; }

        [Precision(18, 2)]
        public decimal? DtAsgRll_Cantidad { get; set; }

        public string UndMed_Id { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }

        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }
        
    }
}
