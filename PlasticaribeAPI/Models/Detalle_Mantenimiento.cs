using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Detalle_Mantenimiento
    {
        [Key]
        public long DtMtto_Codigo { get; set; }


        public long Mtto_Id { get; set; }
        public Mantenimiento? Mttos { get; set; }


        public long Actv_Id { get; set; }
        public Activo? Act { get; set; }


        public int TpMtto_Id { get; set; }
        public Tipo_Mantenimiento? Tipo_Mtto { get; set; }


        [Precision(18, 2)]
        public decimal? DtMtto_Precio { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? DtMtto_Descripcion { get; set; }

        public int Estado_Id { get; set; }
        public Estado? Estados { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
