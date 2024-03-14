using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class DetalleAsignacion_Tinta
    {

        /**Este ID será igual al de la asignación de Materia Prima para crear
        la relación entre el ID de la asignación de Materia Prima y tintas en una OT**/
        [Key]
        public long Codigo { get; set; } //Primaria

        public long AsigMp_Id { get; set; } //Llave foranea de tinta
        public Asignacion_MatPrima? AsigMp { get; set; } //Propiedad de navegación de tinta

        public long Tinta_Id { get; set; }         //Llave foranea de tinta  
        public Tinta? Tinta { get; set; }   //Propiedad de navegación de tinta


        [Precision(14, 2)]
        public decimal DtAsigTinta_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }           //Llave foranea de unidad medida
        public Unidad_Medida? UndMed { get; set; }      //Llave foranea de unidad medida


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }  //Llave foranea de proceso
        public Proceso? Proceso { get; set; }   //Llave foranea de proceso

        /*[Column(TypeName = "bigint")]
        public long DtAsigTinta_OTImpresion { get; set; }*/

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
