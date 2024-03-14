using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Orden_Maquila
    {
        [Key]
        public long OM_Id { get; set; }
        public string Tercero_Id { get; set; }
        public Terceros? Tercero { get; set; }

        [Precision(18, 2)]
        public decimal OM_ValorTotal { get; set; }

        [Precision(18, 2)]
        public decimal OM_PesoTotal { get; set; }
        public string? OM_Observacion { get; set; }
        public string TpDoc_Id { get; set; }
        public Tipo_Documento? TipoDoc { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }

        [Column(TypeName = "date")]
        public DateTime OM_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string OM_Hora { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
