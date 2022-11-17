using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Materia_Prima
    {
       [Key]
       public long MatPri_Id { get; set; }
    
       [Column(TypeName = "varchar(MAX)")] 
       public string MatPri_Nombre { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string MatPri_Descripcion { get; set; }

       [Precision(18, 2)]
       public decimal MatPri_Stock { get; set; }
       
        [Column(TypeName = "varchar(10)")]        
        public string UndMed_Id { get; set; } //Llave foranea Unidad_Medida           
        public Unidad_Medida? UndMed { get; set; } //Propiedad de Navegación Unidad_Medida

        public int CatMP_Id { get; set; }  //Llave foranea Categorias_MatPrima.        
        public Categoria_MatPrima? CatMP { get; set; }  //Propiedad de Navegación Categorias_MatPrima


        [Precision(18, 2)]
        public decimal MatPri_Precio { get; set; }


        public int TpBod_Id { get; set; } //Llave foranea Tipo_Bodega
        public Tipo_Bodega? TpBod { get; set; } //Propiedad de Navegación Tipos_Bodegas

        
        //Lista requerida para relación proveedor-materiaprima
        public IList<Provedor_MateriaPrima>? ProvMatPri { get; set; }

        //Lista requerida para relación facturas compras - materias primas
        
        //public IList<FacturaCompra_MateriaPrima>? FaccoMatPri { get; set; }

        //Lista requerida para relación detalles asignacion - materias primas
        public IList<DetalleAsignacion_MateriaPrima>? DtAsigMatPri { get; set; }

        //public IList<Remision_MateriaPrima>? RemiMatPri { get; set; }

        public IList<DetalleRecuperado_MateriaPrima>? DetRecMatPri { get; set; }

        //public IList<DetalleDevolucion_MateriaPrima>? DetDevMatPri { get; set; }

        //Lista requerida para relación tintas - materias primas
        public IList<Tinta_MateriaPrima>? TintaMatPri { get; set; }

        //Lista requerida para relación Asignacion_MatPrimaXTinta - materias primas
        //public IList<DetalleAsignacion_MatPrimaXTinta>? DetAsigMPxTinta { get; set; }

    }
}
 