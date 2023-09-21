using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Log_Transacciones
    {
        [Key]
        public int Transac_Codigo { get; set; }
        public string? Transac_Id { get; set; }
        public string? Transac_Tipo { get; set; }
        public string? Transac_Nombre { get; set; }
        public string? Transac_Tabla { get; set; }
        public string? Transac_LlavePK { get; set; }
        public string? Transac_Campo { get; set; }
        public string? Transac_ValorOriginal { get; set; }
        public string? Transac_valorNuevo { get; set; }

        [Column(TypeName = "date")]
        public DateTime Transac_Fecha { get; set; }
        public string Transac_Hora { get; set; }
        public long Transac_Usuario { get; set; }
        public Usuario? Usuario { get; set; }
        public string? Transac_BaseDatos { get; set; }
    }
}
