using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Asignacion_MatPrima
    {
        [Key]
        public long AsigMp_Id { get; set; }

        public long AsigMP_OrdenTrabajo { get; set; }

        [Column(TypeName = "Date")]
        public DateTime AsigMp_FechaEntrega { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? AsigMp_Hora { get; set; }

        [Column(TypeName = "text")]
        public string? AsigMp_Observacion { get; set; }

        public long Usua_Id { get; set; } //Llave foranea de usuario que asigna materia prima
        public Usuario? Usua { get; set; } //Propiedad de navegación usuario que asigna materia prima 

        // public long Area_Id { get; set; } //Llave foranea de area que pide materia prima
        // public Area? Area { get; set; } //Propiedad de navegación area que pide materia prima        

        public int Estado_Id { get; set; } //Llave foranea de estado
        public Estado? Estado { get; set; } //Propiedad de navegación estado

        public int AsigMp_Maquina { get; set; } 

        public int? Estado_OrdenTrabajo { get; set; } //Llave foranea de estado de la OT segun condiciones.
        public Estado? EstadoOT { get; set; } //Propiedad de navegación estado de la OT segun condiciones.


        //Lista requerida para relación detalles asignacion - materias primas
        public IList<DetalleAsignacion_MateriaPrima>? DtAsigMatPri { get; set; }

        //Lista requerida para relación detalles asignacion_matpri - tintas
        public IList<DetalleAsignacion_Tinta>? DetAsigTinta { get; set; }

    }
}
