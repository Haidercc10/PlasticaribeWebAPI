using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Mezcla
    {

        [Key]
        public long Mezcla_Id { get; set; } 


        [Column(TypeName = "varchar(MAX)")]
        public string Mezcla_Nombre { get; set; }


        public int Mezcla_NroCapas { get; set; }


        public int Material_Id { get; set; }  /** Llave foranea de Material_MatPrima*/
        public Material_MatPrima? MaterialMP { get; set; } /** Propiedad de navegación de Material_MatPrima*/


        /** Campos Mezcla Porcentaje 1 */

        [Precision(18, 2)]
        public int Mezcla_PorcentajeCapa1 { get; set; } /** Porcentaje Capa1 */


        public int MezMaterial_Id1xCapa1 { get; set; } /** Llave foranea de Mezcla_Material */
        public Mezcla_Material? MezMaterial_MP1C1 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18,2)]
        public decimal Mezcla_PorcentajeMaterial1_Capa1 { get; set; }



        public int MezMaterial_Id2xCapa1 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP2C1 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial2_Capa1 { get; set; }



        public int MezMaterial_Id3xCapa1 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP3C1 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial3_Capa1 { get; set; }



        public int MezMaterial_Id4xCapa1 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP4C1 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial4_Capa1 { get; set; }



        public int MezPigmto_Id1xCapa1 { get; set; }  /** Llave foranea de Mezcla_Pigmento */
        public Mezcla_Pigmento? MezPigmento1C1 { get; set; } /** Propiedad de navegación de Mezcla_Pigmento */


        [Precision(18, 2)]
        public decimal Mezcla_PorcentajePigmto1_Capa1 { get; set; } /** Llave foranea de Mezcla_Pigmento */



        public int MezPigmto_Id2xCapa1 { get; set; } /** Llave foranea de Mezcla_Pigmento */
        public Mezcla_Pigmento? MezPigmento2C1 { get; set; }  /** Propiedad de navegación de Mezcla_Pigmento */


        [Precision(18, 2)]
        public decimal Mezcla_PorcentajePigmto2_Capa1 { get; set; }


        /** Campos Mezcla Porcentaje 2 */

        public int Mezcla_PorcentajeCapa2 { get; set; }

        public int MezMaterial_Id1xCapa2 { get; set; } /** Llave foranea de Mezcla_Material */
        public Mezcla_Material? MezMaterial_MP1C2 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial1_Capa2 { get; set; }



        public int MezMaterial_Id2xCapa2 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP2C2 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial2_Capa2 { get; set; }



        public int MezMaterial_Id3xCapa2 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP3C2 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial3_Capa2 { get; set; }



        public int MezMaterial_Id4xCapa2 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP4C2 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial4_Capa2{ get; set; }



        public int MezPigmto_Id1xCapa2 { get; set; }  /** Llave foranea de Mezcla_Pigmento */
        public Mezcla_Pigmento? MezPigmento1C2 { get; set; } /** Propiedad de navegación de Mezcla_Pigmento */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajePigmto1_Capa2 { get; set; } /** Llave foranea de Mezcla_Pigmento */


        public int MezPigmto_Id2xCapa2 { get; set; } /** Llave foranea de Mezcla_Pigmento */
        public Mezcla_Pigmento? MezPigmento2C2 { get; set; }  /** Propiedad de navegación de Mezcla_Pigmento */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajePigmto2_Capa2 { get; set; }

        /** Mezcla Porcentaje 3 */

        public int Mezcla_PorcentajeCapa3 { get; set; }
      

        public int MezMaterial_Id1xCapa3 { get; set; } /** Llave foranea de Mezcla_Material */
        public Mezcla_Material? MezMaterial_MP1C3 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial1_Capa3 { get; set; }



        public int MezMaterial_Id2xCapa3 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP2C3 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial2_Capa3 { get; set; }



        public int MezMaterial_Id3xCapa3 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP3C3 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial3_Capa3 { get; set; }



        public int MezMaterial_Id4xCapa3 { get; set; } /** Llave foranea de Mezcla_Material*/
        public Mezcla_Material? MezMaterial_MP4C3 { get; set; } /** Propiedad de navegación de Mezcla_Material */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajeMaterial4_Capa3 { get; set; }



        public int MezPigmto_Id1xCapa3 { get; set; }  /** Llave foranea de Mezcla_Pigmento */
        public Mezcla_Pigmento? MezPigmento1C3 { get; set; } /** Propiedad de navegación de Mezcla_Pigmento */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajePigmto1_Capa3 { get; set; } /** Llave foranea de Mezcla_Pigmento */



        public int MezPigmto_Id2xCapa3 { get; set; } /** Llave foranea de Mezcla_Pigmento */
        public Mezcla_Pigmento? MezPigmento2C3 { get; set; }  /** Propiedad de navegación de Mezcla_Pigmento */

        [Precision(18, 2)]
        public decimal Mezcla_PorcentajePigmto2_Capa3 { get; set; }



        public long Usua_Id { get; set; } /** Llave foranea de Usuario */

        public Usuario? Usua { get; set; } /** Propiedad de navegación de Mezcla_Pigmento */


        [Column(TypeName = "date")]
        public DateTime Mezcla_FechaIngreso { get; set; }

    }
}
