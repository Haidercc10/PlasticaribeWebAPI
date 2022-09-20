using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetalleEntradaRollo_Producto
    {
        [Key]
        public long DtEntRolloProd_Codigo { get; set; }

        public long EntRolloProd_Id { get; set; }
        public EntradaRollo_Producto? EntRollo_Producto { get; set; }

        public long Rollo_Id { get; set; }


        [Precision(14,2)]
        public decimal DtEntRolloProd_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }
        

        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }
    }
}
