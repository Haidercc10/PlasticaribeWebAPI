using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    //CLASE DE RELACIÓN ENTRE ASIGNACIONES DE MATERIAS PRIMAS Y MATERIAS PRIMAS
    public class DetalleAsignacion_MateriaPrima
    {
        public long AsigMp_Id { get; set; }
        public Asignacion_MatPrima? AsigMp { get; set; }

        public long MatPri_Id { get; set; } 
        public Materia_Prima? MatPri { get; set; }


        [Precision(14,2)]
        public decimal DtAsigMp_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }

    }
}
