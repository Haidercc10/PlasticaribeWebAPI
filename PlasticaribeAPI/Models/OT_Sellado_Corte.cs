using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class OT_Sellado_Corte
    {
        [Key]
        public long SelladoCorte_Id { get; set; }
        public long Ot_Id { get; set; }
        public Orden_Trabajo? Orden_Trabajo { get; set; }
        public bool Corte { get; set; }
        public bool Sellado { get; set; }
        public int Formato_Id { get; set; }
        public Tipo_Producto? Formato { get; set; }

        [Precision(14, 2)]
        public decimal SelladoCorte_Ancho { get; set; }

        [Precision(14, 2)]
        public decimal SelladoCorte_Largo { get; set; }

        [Precision(14, 2)]
        public decimal? SelladoCorte_Fuelle { get; set; }

        [Precision(14, 2)]
        public decimal SelladoCorte_PesoMillar { get; set; }
        public int TpSellado_Id { get; set; }
        public Tipos_Sellados? TipoSellado { get; set; }

        [Precision(14, 2)]
        public decimal SelladoCorte_PrecioSelladoDia { get; set; }

        [Precision(14, 2)]
        public decimal SelladoCorte_PrecioSelladoNoche { get; set; }

        [Precision(14, 2)]
        public decimal SelladoCorte_CantBolsasPaquete { get; set; }
        [Precision(14, 2)]
        public decimal SelladoCorte_CantBolsasBulto { get; set; }

        [Precision(14, 2)]
        public decimal SelladoCorte_PesoPaquete { get; set; }
        [Precision(14, 2)]
        public decimal SelladoCorte_PesoBulto { get; set; }

        [Precision(14, 2)]
        public decimal SelladoCorte_PesoProducto { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string SelladoCorte_Etiqueta_Ancho { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string SelladoCorte_Etiqueta_Largo { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string SelladoCorte_Etiqueta_Fuelle { get; set; }
    }
}
