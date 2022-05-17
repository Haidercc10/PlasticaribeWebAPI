using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipo_Bodega
    {

        [Key]
        public int TpBod_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String TpBod_Nombre { get; set; }

        [Column(TypeName = "text")]
        public String TpBod_Descripcion { get; set; }

        //Llave foranea areas agregada.
        public long Area_Id { get; set; }
        public Area? Area { get; set; } //Foranea areas.
}
}
