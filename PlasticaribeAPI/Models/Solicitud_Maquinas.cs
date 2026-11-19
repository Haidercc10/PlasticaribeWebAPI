using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Máquina a la que va el material de una solicitud y el porcentaje que le corresponde. Es informativa
    /// (planeación y PDF); los porcentajes de una solicitud suman 100. La primera máquina también queda en
    /// Solicitud_MatPrimaExtrusion.SolMpExt_Maquina para los reportes anteriores.
    /// </summary>
    public class Solicitud_Maquinas
    {
        [Key]
        public long SolMaq_Id { get; set; }

        public long SolMpExt_Id { get; set; }
        public Solicitud_MatPrimaExtrusion? SolMatPriExt { get; set; }

        /// <summary>Número de la máquina (Maq_Numero).</summary>
        public int SolMaq_Maquina { get; set; }

        [Precision(5, 2)]
        public decimal SolMaq_Porcentaje { get; set; }
    }
}
