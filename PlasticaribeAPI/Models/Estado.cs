using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Estado
    {
        [Key]
        public int Estado_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Estado_Nombre { get; set; }

        [Column(TypeName = "text")]
        public String? Estado_Descripcion { get; set; }
        
        public int TpEstado_Id { get; set; }
        //public Tipo_Estado TpEstado { get; set; }

    }
}
