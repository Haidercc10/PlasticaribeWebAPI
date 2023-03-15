using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Tinta_MateriaPrima
    {
        [Key]
        public long Codigo { get; set; }

        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPri { get; set; }
    }

}
