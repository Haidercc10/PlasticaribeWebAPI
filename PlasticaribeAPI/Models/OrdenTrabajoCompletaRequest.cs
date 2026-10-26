using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Solicitud para crear una orden de trabajo junto con la información de todos sus procesos.
    /// El identificador de la OT en cada proceso lo asigna el servidor.
    /// </summary>
    public sealed class OrdenTrabajoCompletaRequest
    {
        [Required]
        public Orden_Trabajo OrdenTrabajo { get; set; } = null!;

        [Required]
        public OT_Extrusion Extrusion { get; set; } = null!;

        [Required]
        public OT_Impresion Impresion { get; set; } = null!;

        [Required]
        public OT_Laminado Laminado { get; set; } = null!;

        [Required]
        public OT_Sellado_Corte SelladoCorte { get; set; } = null!;
    }
}
