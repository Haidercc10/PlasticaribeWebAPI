using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Insumo
    {
        [Key]
        public int Insu_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Insu_Nombre { get; set; }

        [Column(TypeName = "varchar(100)")]

        [Precision(18, 2)]
        public decimal Insu_Stock { get; set; }

        public string UndMed_Id { get; set; }

        //public Unidad_Medida undMed { get; set; }

        public int CatInsu_Id { get; set; }

        //public Categoria_Insumo CatInsu { get; set; }



    }
}
