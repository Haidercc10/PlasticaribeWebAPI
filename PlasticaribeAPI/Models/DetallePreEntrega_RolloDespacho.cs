using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class DetallePreEntrega_RolloDespacho
    {

        [Key]
        public long DtlPreEntRollo_Id { get; set; }

        public long PreEntRollo_Id { get; set; }
        public PreEntrega_RolloDespacho? PreEntregaRollo { get; set; }


        [Column(Order = 2)]
        public long DtlPreEntRollo_OT { get; set; }


        [Column(Order = 3)]
        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }

        
        [Column(TypeName = "varchar(10)", Order = 4)]
        public string UndMed_Producto { get; set; }
        public Unidad_Medida? UndMedidaProducto { get; set; }


        [Column(Order = 5)]
        public long Cli_Id { get; set; }
        public Clientes? Cliente { get; set; }


        public long Rollo_Id { get; set; }


        [Precision(14,2)]
        public decimal DtlPreEntRollo_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Rollo { get; set; }
        public Unidad_Medida? UndMedidaRollo { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }
    }
}
