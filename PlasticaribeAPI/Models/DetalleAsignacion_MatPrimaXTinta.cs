using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetalleAsignacion_MatPrimaXTinta
    {
        public long AsigMPxTinta_Id { get; set; } //Llave foranea de Asignacion_MatPrimaXTinta
        public Asignacion_MatPrimaXTinta? AsigMPxTinta { get; set; } //Propiedad de navegación de Asignacion_MatPrimaXTinta

        public long MatPri_Id { get; set; }         //Llave foranea de materia prima      
        public Materia_Prima? MatPri { get; set; }   //Propiedad de navegación de materia prima


        [Precision(14, 2)]
        public decimal DetAsigMPxTinta_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }           //Llave foranea de unidad medida
        public Unidad_Medida? UndMed { get; set; }      //Llave foranea de unidad medida


        [Column(TypeName = "varchar(10)")]  
        public string Proceso_Id { get; set; }  //Llave foranea de proceso
        public Proceso? Proceso { get; set; }   //Llave foranea de proceso

    }
}
