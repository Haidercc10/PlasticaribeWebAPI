using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Factura_Compra
    {              
        [Key]        
        public long Facco_Id { get; set; } //Id interno (Consecutivo dado por la empresa) de la factura

        [Column(TypeName = "varchar(100)")]
        public string Facco_Codigo { get; set; } //Codigo externo (Consecutivo del proveedor) de la factura 

        [Column(TypeName = "Date")]
        public DateTime Facco_FechaFactura { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Facco_FechaVencimiento { get; set; }

        public long Prov_Id { get; set; } //Llave foranea de proveedores
        public Proveedor? Prov { get; set; } //Propiedad de navegación proveedores 

        [Precision(18, 2)]
        public decimal Facco_ValorTotal { get; set; }
       
        [Column(TypeName = "text")]
        public string? Facco_Observacion { get; set; }

        public int Estado_Id { get; set; } //Llave foranea de proveedores
        public Estado? Estado { get; set; } //Propiedad de navegación proveedores 

        public long Usua_Id { get; set; } //Llave foranea de usuario que registra la factura de compra
        public Usuario? Usua { get; set; } //Propiedad de navegación usuario que registra la factura de compra 

        
        [Column(TypeName = "varchar(10)")]
        public string? TpDoc_Id { get; set; } //Llave foranea de Tipo_Documento
        public Tipo_Documento? TpDoc { get; set; } //Propiedad de navegación de Tipo_Documento


        //Lista requerida para relación facturas compras - materias primas
        public IList<FacturaCompra_MateriaPrima>? FaccoMatPri { get; set; }

    }
}
