using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PedidoExterno
    {
        [Key]
        public long PedExt_Id { get; set; }

        //Codigo Zeus
        public long PedExt_Codigo { get; set; }

        [Column(TypeName = "date")]
        public DateTime PedExt_FechaCreacion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? PedExt_HoraCreacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PedExt_FechaEntrega { get; set; }

        //key empresa
        public long Empresa_Id { get; set; }
        public Empresa? Empresa { get; set; }

        //key sede cliente
        public long? SedeCli_Id { get; set; }
        public SedesClientes? SedeCli { get; set; }

        //key usuario vendedor
        [Column(Order = 6)]
        public long? Usua_Id { get; set; }
        public Usuario? Usua { get; set; }

        //key estados
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string PedExt_Observacion { get; set; }

        [Precision(18, 2)]
        public decimal PedExt_PrecioTotal { get; set; }

        //key usuario creador
        public long? Creador_Id { get; set; }
        public Usuario? Creador { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string? PedExt_Oc { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string PedExt_DireccionEntrega { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
