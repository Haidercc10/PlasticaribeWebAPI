using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Facturacion_OrdenMaquila
    {
        [Key]
        public long FacOM_Id { get; set; }
        public string? TpDoc_Id { get; set; }
        public Tipo_Documento? TipoDoc { get; set; }
        public string FacOM_Codigo { get; set; }
        public string Tercero_Id { get; set; }
        public Terceros? Tercero { get; set; }

        [Precision(18, 2)]
        public decimal FacOM_ValorTotal { get; set; }
        public string? FacOM_Observacion { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }

        [Column(TypeName = "Date")]
        public DateTime FacOM_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string FacOM_Hora { get; set; }

    }
}
