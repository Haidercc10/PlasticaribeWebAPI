using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class FacturaCompra_MateriaPrima
    {
        [Key]
        [Column(Order = 0)]
        public long Facco_Codigo { get; set; } //Llave primaria compuesta Factura_Compra


        public long Facco_Id { get; set; } //Llave primaria compuesta Factura_Compra
        public Factura_Compra? Facco { get; set; } //Propiedad de navegación Factura_Compra


        public long MatPri_Id { get; set; } //Llave primaria compuesta Materia_Prima
        public Materia_Prima? MatPri { get; set; } //Propiedad de navegación Materia_Prima


        public long Tinta_Id { get; set; } //Llave primaria compuesta Tinta
        public Tinta? Tinta { get; set; } //Propiedad de navegación Tinta


        [Precision(14, 2)]
        public decimal FaccoMatPri_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }


        [Precision(18, 2)]
        public decimal FaccoMatPri_ValorUnitario { get; set; }

    }
}
