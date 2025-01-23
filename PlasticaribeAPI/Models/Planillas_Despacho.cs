using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Planillas_Despacho
    {
        [Key]
        public int Pla_Id { get; set; }


        public long Usua_Conductor { get; set; } 
        public Usuario? Conductor { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Pla_Placa { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Pla_Fecha { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Pla_Hora { get; set; }


        [Precision(18, 2)]
        public decimal Pla_ValorTotal { get; set; }

        [Precision(18, 2)]
        public decimal Pla_ValorContado { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Precision(18, 2)]
        public decimal Pla_PesoTotal { get; set; }

        public int? Estado_Id { get; set; }
        public Estado? Estado { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Pla_FechaRecepcion { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Pla_HoraRecepcion { get; set; }


        [Precision(18, 2)]
        public decimal Pla_ValorRecibido { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string? Pla_Observacion { get; set; }
    }
}
