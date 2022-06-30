using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Remision_MateriaPrima
    {

        public int Rem_Id { get; set; } //Llave primaria compuesta Remision
        public Remision? Rem { get; set; } //Propiedad de navegación Remision

        public long MatPri_Id { get; set; } //Llave primaria compuesta Materia_Prima
        public Materia_Prima? MatPri { get; set; } //Propiedad de navegación Materia_Prima

        [Precision(14, 2)]
        public decimal RemiMatPri_Cantidad { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        [Precision(18, 2)]
        public decimal? RemiMatPri_ValorUnitario { get; set; }
    }
}
