using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Eventos_Maquinas
    {
        [Key]
        public long Evmq_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Evmq_Codigo { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Evmq_Descripcion { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string TpEvento_Id { get; set; }
        public Tipos_Eventos? TiposEventos { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Maq_Id { get; set; }
        public Maquinas? Maquinas { get; set; }

        public bool Evmq_Activo { get; set; }


    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
}


