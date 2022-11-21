using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tinta
    {
        [Key]
        public long Tinta_Id { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string Tinta_Nombre { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        public string? Tinta_Descripcion { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string? Tinta_CodigoHexadecimal { get; set; }


        [Precision(18, 2)]
        public decimal Tinta_Stock { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; } //Llave foranea Unidad_Medida           
        public Unidad_Medida? UndMed { get; set; } //Propiedad de Navegación Unidad_Medida


        [Precision(18, 2)]
        public decimal Tinta_Precio { get; set; }

        public int CatMP_Id { get; set; }  //Llave foranea Categorias_MatPrima.        
        public Categoria_MatPrima? CatMP { get; set; }  //Propiedad de Navegación Categorias_MatPrima

        public int TpBod_Id { get; set; } //Llave foranea Tipo_Bodega
        public Tipo_Bodega? TpBod { get; set; } //Propiedad de Navegación Tipos_Bodegas

        [Precision(14,2)]
        public decimal Tinta_InvInicial { get; set; }


        [Column(TypeName = "Date")]
        public DateTime? Tinta_FechaIngreso { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Tinta_Hora { get; set; }

        public IList<Tinta_MateriaPrima>? TintaMatPri { get; set; }

        //Lista requerida para relación detalles asignacion_matpri - tintas
        public IList<DetalleAsignacion_Tinta>? DetAsigTinta { get; set; }
    }
}
