using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Entradas_Salidas_MP
    {
        [Key]
        public int Id { get; set; }
        public int Id_Entrada { get; set; }
        public Movimientros_Entradas_MP? Movimientros { get; set; }
        public string Tipo_Salida { get; set; }
        public Tipo_Documento? Documento { get; set; }
        public int Codigo_Salida { get; set; }
    }
}
