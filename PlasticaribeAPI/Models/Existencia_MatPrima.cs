using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Existencia_MatPrima
    {
        [Key]
        public long ExMatPri_Id { get; set; } /** CODIGO */

        //llave Foranea de la tabla materias_primas
        public long MatPri_Id { get; set; } /** ARTICULO */
        public Materia_Prima? MatePrima { get; set; }

        [Precision(18, 2)]
        public decimal ExMatPri_Cantidad { get; set; } /** EXISTENCIAS */

        // Llave foranea de la tabla unidad_medida.
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMed { get; set; }

        //llave Foranea de la tabla tipos_bodegas
        public int TpBod_Id { get; set; } /** BODEGA */
        public Tipo_Bodega? TpBod { get; set; }

        [Precision(18, 2)]
        public decimal Ex_PrecioUnitario { get; set; } /** VALOR */

        /* Valor de cantidad de materia prima multiplicada por el precio unitario */
        [Precision(18, 2)]
        public decimal ExProd_PrecioExistencia { get; set; } /** VALOR 2  */

        //llave Foranea de la tabla tipos_moneda
        public string TpMoneda_Id { get; set; }
        public Tipo_Moneda? TpMoneda { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
