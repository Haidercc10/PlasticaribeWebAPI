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

        public long Rollo_Id { get; set; }


        [Precision(18,2)]
        public decimal DtlPreEntRollo_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }
    }
}
