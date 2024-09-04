
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Servicios_Produccion
    {
        [Key]
        public int SvcProd_Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string SvcProd_Nombre { get; set;  }

        [Column(TypeName = "varchar(max)")]
        public string? SvcProd_Descripcion { get; set; }

        [Precision(18,2)]
        public decimal SvcProd_Valor { get; set; }

        public long Area_Realiza { get; set; }
        public Area? Areas_Solicitadas { get; set; }

        public long Area_Solicita { get; set; }
        public Area? Areas_Solicitantes { get; set; }

    }
}
