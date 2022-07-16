using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetalleDevolucion_MateriaPrima
    {

        public long DevMatPri_Id { get; set; }
        public Devolucion_MatPrima? DevMatPri { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPri { get; set; }

        [Precision(14, 2)]
        public decimal DtDevMatPri_CantidadDevuelta { get; set; }
        
        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; } 
        public Proceso? Proceso { get; set; }

    }
}
