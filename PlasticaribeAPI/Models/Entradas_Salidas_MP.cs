using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Entradas_Salidas_MP
    {
        [Key]
        public int Id { get; set; }
        public int Id_Entrada { get; set; }
        public string Tipo_Entrada { get; set; }
        public Tipo_Documento? Tipo_Documento { get; set; }
        public int Codigo_Entrada { get; set; }
        public Movimientros_Entradas_MP? Movimientros { get; set; }
        public string Tipo_Salida { get; set; }
        public Tipo_Documento? Documento { get; set; }
        public int Codigo_Salida { get; set; }
        public long MatPri_Id { get; set; }
        public Materia_Prima? Materia_Prima { get; set; }
        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }
        public long Bopp_Id { get; set; }
        public Bopp_Generico? Bopp { get; set; }

        [Precision(18, 2)]
        public decimal Cantidad_Salida { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Registro { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Hora_Registro { get; set; }

        public long Orden_Trabajo { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

    }
}
