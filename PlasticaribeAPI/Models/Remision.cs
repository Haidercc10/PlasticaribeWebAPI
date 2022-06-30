using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Remision
    {
        [Key]
        public int Rem_Id { get; set; } //ID Interno

        [Column(TypeName="varchar(100)")]
        public string Rem_Codigo { get; set; } //Codigo Externo

        [Column(TypeName="Date")]
        public DateTime Rem_Fecha { get; set; }

        [Precision(18,2)]
        public decimal? Rem_PrecioEstimado { get; set; }

        public long Prov_Id { get; set; } //Llave foranea de proveedores. 
        public Proveedor? Prov { get; set; } //Propiedad de navegación de proveedores.

        public int Estado_Id { get; set; } //Llave foranea de Estado. 
        public Estado? Estado { get; set; } //Propiedad de navegación de Estado.

        public long Usua_Id { get; set; } //Llave foranea de Usuario. 
        public Usuario? Usua { get; set; } //Propiedad de navegación de Usuario.

        [Column(TypeName = "varchar(10)")]
        public string TpDoc_Id { get; set; }  //Llave foranea de Tipo_Documento. 
        public Tipo_Documento? TpDoc { get; set; } //Propiedad de navegación de Tipo_Documento.

        [Column(TypeName = "text")] 
        public string? Rem_Observacion { get; set; }

        public IList<Remision_MateriaPrima>? RemiMatPri { get; set; }

    }
}
