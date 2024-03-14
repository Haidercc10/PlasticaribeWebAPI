using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    //Esta clase estará relacionada con las entradas de 
    public class Recuperado_MatPrima
    {
        [Key]
        public long RecMp_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime RecMp_FechaIngreso { get; set; }

        public long Usua_Id { get; set; } //Llave primaria Usuario
        public Usuario? Usua { get; set; } //Propiedad de navegación Usuario 


        [Column(TypeName = "varchar(max)")]
        public string? RecMp_Observacion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Proc_Id { get; set; } //Llave primaria Proceso
        public Proceso? Proceso { get; set; } //Propiedad de navegación Proceso 

        /** Campos para control de recuperado MP */

        [Column(TypeName = "varchar(50)")]
        public string Turno_Id { get; set; }
        public Turno? TurnoRecMP { get; set; }

        public long Usua_Operador { get; set; } //Llave primaria Usuario
        public Usuario? UsuaOperador { get; set; } //Propiedad de navegación Usuario 

        public int RecMp_Maquina { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RecMp_FechaEntrega { get; set; }


        [Column(TypeName = "varchar(20)", Order = 2)]
        public string RecMp_HoraIngreso { get; set; }

        //public IList<DetalleRecuperado_MateriaPrima>? DetRecMatPri { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
