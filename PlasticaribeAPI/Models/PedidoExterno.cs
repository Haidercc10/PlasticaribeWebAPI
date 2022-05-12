        using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class PedidoExterno
    {
        [Key]
        public long PedExt_Id { get; set; }

        public long PedExt_Codigo { get; set; }

        public DateTime PedExt_FechaCreacion { get; set; }

        public DateTime PedExt_FechaEntrega { get; set; }

        //Llave foranea empresa
        public long Empresa_Id { get; set; }
        public Empresa Empresa { get; set; }

        public long SedeCliente { get; set; }
        public SedesClientes SedeCli { get; set; }

        //Llave foranea estados
        public int Estado_Id { get; set; }
        public Estado Estado { get; set; }

        [Column(TypeName = "text")]
        public string PedExt_Observacion { get; set; }

        [Precision(18,2)]
        public decimal PedExt_PrecioTotal { get; set; }

        [Column(TypeName = "binary(MAX)")]
        public int PedExt_Archivo { get; set; }

    }
}
