using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Movimientros_Entradas_MP
    {
        [Key]
        public int Id { get; set; }
        public long MatPri_Id { get; set; }
        public Materia_Prima? Materia_Prima { get; set; }
        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }
        public long Bopp_Id { get; set; }
        public Bopp_Generico? Bopp { get; set; }

        [Precision(18, 2)]
        public decimal Cantidad_Entrada { get; set; }
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }

        [Precision(18, 2)]
        public decimal Precio_RealUnitario { get; set; }

        [Precision(18, 2)]
        public decimal Precio_EstandarUnitario { get; set; }
        public string Tipo_Entrada { get; set; }
        public Tipo_Documento? Tipo_Documento { get; set; }
        public int Codigo_Entrada { get; set; }
        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }

        [Precision(18, 2)]
        public decimal Cantidad_Asignada { get; set; }

        [Precision(18, 2)]
        public decimal Cantidad_Disponible { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Observacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Entrada { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Hora_Entrada { get; set; }
    }
}
