using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Nomina_Plasticaribe
    {
        [Key]
        public int Nomina_Id { get; set; }
        public DateTime Nomina_FechaRegistro { get; set; }
        public string Nomina_HoraRegistro { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime Nomina_FechaIncial { get; set; }
        public DateTime Nomina_FechaFinal { get; set; }

        [Precision(14, 2)]
        public decimal Nomina_Costo { get; set; }
    }
}
