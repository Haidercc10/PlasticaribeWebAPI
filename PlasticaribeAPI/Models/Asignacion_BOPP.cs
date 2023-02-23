using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Asignacion_BOPP
    {
        [Key]
        public long AsigBOPP_Id { get; set; }

        //public long AsigBOPP_OrdenTrabajo { get; set; }

        [Column(TypeName = "Date")]
        public DateTime AsigBOPP_FechaEntrega { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? AsigBOPP_Hora { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? AsigBOPP_Observacion { get; set; }

        public long Usua_Id { get; set; } //Llave foranea de usuario que asigna BOPP
        public Usuario? Usua { get; set; } //Propiedad de navegación usuario que asigna BOPP        

        public int Estado_Id { get; set; } //Llave foranea de estado
        public Estado? Estado { get; set; } //Propiedad de navegación estado

        //Lista requerida para relacion con BOPP en detalles asignaciones bopp
        public IList<DetalleAsignacion_BOPP>? DetAsigBOPP { get; set; }

    }
}
