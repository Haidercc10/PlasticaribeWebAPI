using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tinta_MateriaPrima
    {
        [Key]
        public long Codigo { get; set; }

        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPri { get; set; }
    }
    #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
