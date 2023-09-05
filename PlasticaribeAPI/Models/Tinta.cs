using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tinta
    {
        [Key]
        public long Tinta_Id { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string Tinta_Nombre { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string? Tinta_Descripcion { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string? Tinta_CodigoHexadecimal { get; set; }


        [Precision(18, 2)]
        public decimal Tinta_Stock { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }


        [Precision(18, 2)]
        public decimal Tinta_Precio { get; set; }

        public int CatMP_Id { get; set; }       
        public Categoria_MatPrima? CatMP { get; set; }

        public int TpBod_Id { get; set; }
        public Tipo_Bodega? TpBod { get; set; }

        [Precision(14,2)]
        public decimal Tinta_InvInicial { get; set; }


        [Column(TypeName = "Date")]
        public DateTime? Tinta_FechaIngreso { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Tinta_Hora { get; set; }

        [Precision(18, 2)]
        public decimal Tinta_PrecioEstandar { get; set; }
    }
}
