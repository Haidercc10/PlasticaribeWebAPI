using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Detalle_OrdenCompra
    {
        [Key]
        public long Doc_Codigo { get; set; }

        public long Oc_Id { get; set; }
        public Orden_Compra? Orden_Compra { get; set; }


        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPrima { get; set; }


        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }


        public long BOPP_Id { get; set; }
        public Bopp_Generico? BOPP { get; set; }


        [Precision(18, 2)]
        public decimal Doc_CantidadPedida { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }


        [Precision(18, 2)]
        public decimal Doc_PrecioUnitario { get; set; }
    }
}
