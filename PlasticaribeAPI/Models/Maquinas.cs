using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Maquinas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Maq_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar(50)")]
        public string Maq_Id { get; set; }

        public int Maq_Numero { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Maq_Nombre { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Procesos { get; set; }
    }
}
