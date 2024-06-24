using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class MatPrima_Material_Pigmento
    {
        [Key]
        public long Codigo { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPrima { get; set; }

        public int Material_Id { get; set; }
        public Material_MatPrima? Material { get; set; }

        public int Pigmt_Id { get; set; }
        public Pigmento? Pigmento { get; set; }
    }
}
