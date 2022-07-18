namespace PlasticaribeAPI.Models
{
    public class Tinta_MateriaPrima
    {
        public long Tinta_Id { get; set; }
        public Tinta? Tinta { get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPri { get; set; }
    }

}
