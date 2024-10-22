using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Reposiciones
    {
        [Key]
        public long Rep_Id { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Cliente { get; set; }


        [Column(TypeName = "date")]
        public DateTime Rep_FechaCrea { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Rep_HoraCrea { get; set; }


        public long Usua_Crea { get; set; }
        public Usuario? Usuario1 { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Rep_Observacion { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estados { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Rep_FechaSalida { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? Rep_HoraSalida { get; set; }


        public long Usua_Salida { get; set; }
        public Usuario? Usuario2 { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Rep_ObservacionSalida { get; set; }
    }
}
