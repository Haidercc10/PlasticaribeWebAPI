using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Activo
    {
        [Key]
        public long Actv_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Actv_Serial { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Actv_Nombre { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Actv_Marca { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string? Actv_Modelo { get; set; }

        public int TpActv_Id { get; set; }
        public Tipo_Activo? Tp_Activo { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estados { get; set; }


        public long Area_Id { get; set; }
        public Area? Area { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Actv_FechaUltimoMtto { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Actv_Observacion { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Actv_FechaCompra { get; set; }


        [Precision(18, 2)]
        public decimal? Actv_PrecioCompra { get; set; }


        [Precision(18, 2)]
        public decimal? Actv_Depreciacion { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Actv_FechaCreacion { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? Actv_HoraCreacion { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
