using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Provedor_MateriaPrima
    {
        [Key]
        public long Codigo { get; set; }

        public long Prov_Id { get; set; }
        public Proveedor? Prov { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPri { get; set; }
    }
}
