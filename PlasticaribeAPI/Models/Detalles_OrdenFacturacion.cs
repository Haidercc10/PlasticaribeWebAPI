using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Detalles_OrdenFacturacion
    {
        [Key]
        public int Id { get; set; }
        public int Id_OrdenFacturacion { get; set; }
        public OrdenFacturacion? OrdenFacturacion { get; set; }
        public long Numero_Rollo { get; set; }
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        [Precision(18, 2)]
        public decimal Cantidad { get; set; }

        public string Presentacion { get; set; }
        public Unidad_Medida? Und {  get; set; }
    }
}
