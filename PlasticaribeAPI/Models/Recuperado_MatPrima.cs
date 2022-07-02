using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    //Esta clase estará relacionada con las entradas de 
    public class Recuperado_MatPrima
    {
        [Key]
        public long RecMp_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime RecMp_FechaIngreso { get; set; }
 
        public long Usua_Id { get; set; } //Llave primaria Usuario
        public Usuario? Usua { get; set; } //Propiedad de navegación Usuario 


        [Column(TypeName = "text")]
        public string? RecMp_Observacion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Proc_Id { get; set; } //Llave primaria Proceso
        public Proceso? Proceso { get; set; } //Propiedad de navegación Proceso 

        public IList<DetalleRecuperado_MateriaPrima>? DetRecMatPri { get; set; }

    }
}
