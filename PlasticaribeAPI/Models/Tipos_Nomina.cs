using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Tipos_Nomina
    {
        [Key]
        public int TpNomina_Id { get; set; }
        public string TpNomina_Nombre { get; set; }
        public string TpNomina_Descripcion { get; set; }
    }
}
