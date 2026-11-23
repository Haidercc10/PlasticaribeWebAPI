using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Estado de la "solicitud de la OT": agrupa todas las solicitudes de material de una orden de trabajo.
    /// Una OT puede tener varias solicitudes (se pide por partes); aquí se lleva su avance contra los kg de la OT
    /// (umbral) y el tope con el adicional, y quién la completó, finalizó y verificó.
    /// </summary>
    public class SolicitudMaterial_OT
    {
        /// <summary>Número de la orden de trabajo (una fila por OT).</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SolOT_OrdenTrabajo { get; set; }

        /// <summary>Kg de la OT en BagPro (umbral, sin el adicional). Null mientras no se conozcan (OT migradas).</summary>
        [Precision(14, 2)]
        public decimal? SolOT_KgOT { get; set; }

        /// <summary>Estado de la solicitud de la OT (tipo de estado de solicitud y asignación de materia prima).</summary>
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        /// <summary>Fecha en que se registró la OT en este flujo.</summary>
        public DateTime SolOT_FechaRegistro { get; set; }

        /// <summary>Fecha en que la OT pasó a completada.</summary>
        public DateTime? SolOT_FechaCompletada { get; set; }

        /// <summary>Usuario que completó la OT manualmente; null si se completó sola al llegar al tope.</summary>
        public long? Usua_IdCompleta { get; set; }
        public Usuario? UsuaCompleta { get; set; }

        /// <summary>Fecha en que se finalizó la OT (se cerró la asignación de materiales).</summary>
        public DateTime? SolOT_FechaFinalizada { get; set; }

        /// <summary>Usuario que finalizó la OT.</summary>
        public long? Usua_IdFinaliza { get; set; }
        public Usuario? UsuaFinaliza { get; set; }

        /// <summary>Fecha en que el solicitante verificó físicamente lo recibido.</summary>
        public DateTime? SolOT_FechaVerificada { get; set; }

        /// <summary>Usuario que verificó la OT.</summary>
        public long? Usua_IdVerifica { get; set; }
        public Usuario? UsuaVerifica { get; set; }
    }
}
