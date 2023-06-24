using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Solicitud_Rollos_Areas
    {
        [Key]
        public int TpSol_Id { get; set; }
        public string TpSol_Nombre { get; set; }
        public string TpSol_Descricion { get; set; }
    }
}
