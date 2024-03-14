using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipo_Solicitud_Rollos_Areas
    {
        [Key]
        public int TpSol_Id { get; set; }
        public string TpSol_Nombre { get; set; }
        public string TpSol_Descricion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
