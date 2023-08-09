using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Nomina_Plasticaribe
    {
        [Key]
        public int Nomina_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Nomina_FechaRegistro { get; set; }
        public string Nomina_HoraRegistro { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime Nomina_FechaIncial { get; set; }

        [Column(TypeName = "date")]
        public DateTime Nomina_FechaFinal { get; set; }

        [Precision(14, 2)]
        public decimal Nomina_Costo { get; set; }
        public string Nomina_Observacion { get; set; }
        public int TpNomina_Id { get; set; }
        public Tipos_Nomina? Tipos_Nomina { get; set; }
    }
}
