using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Producto
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Prod_Cod { get; set; }
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int Prod_Id { get; set; }

        
        [Column(TypeName = "varchar(100)")]
        public string Prod_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string? Prod_Descripcion { get; set; }

        
        public decimal Prod_Peso_Bruto { get; set; }

        
        public decimal Prod_Peso_Neto { get; set; }

        
        public decimal? Prod_Fuelle { get; set; }
        
        
        public decimal? Prod_Ancho { get; set; }
        
        
        public decimal? Prod_Calibre { get; set; }

        public Unidad_Medida UndMed { get; set; } //Foranea unidades medidas
        
        public Tipo_Producto TpProd { get; set; } //Foranea tipos productos

        public Tipo_Bodega TpBod { get; set; } //Foranea tipos bodegas
    }
}
