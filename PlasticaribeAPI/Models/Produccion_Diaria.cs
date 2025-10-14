using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Produccion_Diaria
    {
        [Key]
        public int Prd_Id { get; set; }

        public int Prd_Ano { get; set; }

        [Column(TypeName = "date")]
        public DateTime Prd_Fecha { get; set; }

        public int Prd_Maquina { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Procesos { get; set; }

        [Precision(18,2)]
        public decimal Prd_Peso { get; set; }

        [Precision(18, 2)]
        public decimal Prd_Cantidad { get; set; }

        [Precision(18, 2)]
        public decimal Prd_Meta { get; set; }

        [Precision(18, 2)]
        public decimal Prd_Porcentaje { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Turno_Id { get; set; }
        public Turno? Turnos { get; set; }


        [Column(TypeName = "date")]
        public DateTime Prd_FechaRegistro { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Prd_HoraRegistro { get; set; }
    }
}
