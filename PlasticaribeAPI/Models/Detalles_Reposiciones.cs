using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Detalles_Reposiciones
    {
        [Key]
        public long DtlRep_Codigo { get; set; }

        public long Rep_Id { get; set; }
        public Reposiciones? Repo { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        public long DtlRep_Rollo { get; set; }


        [Precision(18, 2)]
        public decimal DtlRep_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? Und { get; set; }
    }
}
