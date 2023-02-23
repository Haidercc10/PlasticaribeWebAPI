using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Mantenimiento
    {
        [Key]
        public long Mtto_Id { get; set; }


        public long PedMtto_Id { get; set; }       
        public Pedido_Mantenimiento? Pedido_Mtto { get; set;  }


        public long Prov_Id { get; set; }
        public Proveedor? Proveedor { get; set; }


        [Column(TypeName = "date")]
        public DateTime Mtto_FechaInicio { get; set; }


        [Column(TypeName = "date")]
        public DateTime Mtto_FechaFin { get; set; }


        [Precision(18, 2)]
        public decimal? Mtto_PrecioTotal { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Mtto_Observacion { get; set; }


        public int? Mtto_CantDias { get; set; }


        public long Usua_Id { get; set; }
        public Usuario? Usu { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Mtto_FechaRegistro { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? Mtto_HoraRegistro { get; set; }
    }
}
