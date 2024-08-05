using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Ubicaciones_BodegaRollos
    {
        [Key]
        public int UBR_Codigo { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string UBR_Bodega { get; set; }

        public int UBR_Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string UBR_Nombre { get; set; }

        public int UBR_SubId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string UBR_SubNombre { get; set; }

        public int UBR_Zona { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string UBR_Nomenclatura { get; set; }


        [Column(TypeName="varchar(10)")]
        public string Proceso_Id { get; set; } 
        public Proceso? Procesos { get; set; }
    }
}
