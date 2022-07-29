using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetalleAsignacion_BOPP
    {
        [Key]
        public long DtAsigBOPP_Id { get; set; }

        public long AsigBOPP_Id { get; set; }
        public Asignacion_BOPP? AsigBOPP { get; set; }


        public long BOPP_Id { get; set; }
        public BOPP? BOPP { get; set; }


        [Precision(14, 2)]
        public decimal DtAsigBOPP_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }

        public long DtAsigBOPP_OrdenTrabajo { get; set; }

        public int? Estado_OrdenTrabajo { get; set; } //Llave foranea de estado de la OT segun condiciones.
        public Estado? EstadoOT { get; set; } //Propiedad de navegación estado de la OT segun condiciones.

    }
}
