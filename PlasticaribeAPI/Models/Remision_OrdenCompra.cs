namespace PlasticaribeAPI.Models
{
    public class Remision_OrdenCompra
    {
        public long Oc_Id { get; set; }
        public Orden_Compra? Orden_Compra { get; set; }

        public int Rem_Id { get; set; }
        public Remision? Remision { get; set; }
    }
}
