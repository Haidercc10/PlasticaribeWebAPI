using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipos_Nomina
    {
        [Key]
        public int TpNomina_Id { get; set; }
        public string TpNomina_Nombre { get; set; }
        public string TpNomina_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
