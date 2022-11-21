using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Asignacion_MatPrimaXTinta
    {
        [Key]
        public long AsigMPxTinta_Id { get; set; }

        
        public long Tinta_Id { get; set; }         //Llave foranea de tinta  
        public Tinta? Tinta { get; set; }   //Propiedad de navegación de tinta


        [Precision(14, 2)]
        public decimal AsigMPxTinta_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }           //Llave foranea de unidad medida
        public Unidad_Medida? UndMed { get; set; }      //Llave foranea de unidad medida


        [Column(TypeName = "Date")]
        public DateTime AsigMPxTinta_FechaEntrega { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? AsigMPxTinta_Hora { get; set; }

        [Column(TypeName = "text")]
        public string? AsigMPxTinta_Observacion { get; set; }

        public long Usua_Id { get; set; } //Llave foranea de usuario que asigna materia prima
        public Usuario? Usua { get; set; } //Propiedad de navegación usuario que asigna materia prima 

    
        public int Estado_Id { get; set; } //Llave foranea de estado
        public Estado? Estado { get; set; } //Propiedad de navegación estado

        //Lista requerida para relación Asignacion_MatPrimaXTinta - materias primas
        //public IList<DetalleAsignacion_MatPrimaXTinta>? DetAsigMPxTinta { get; set; }

    }
}
