using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DetalleDevolucion_MateriaPrima
    {
        [Key]
        public long DtDevMatPri_Codigo { get; set; }

        public long DevMatPri_Id { get; set; }
        public Devolucion_MatPrima? DevMatPri { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPri { get; set; }

        public long? Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }

        public long? BOPP_Id { get; set; }
        public BOPP? Bopp { get; set; }


        [Precision(14, 2)]
        public decimal DtDevMatPri_CantidadDevuelta { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
