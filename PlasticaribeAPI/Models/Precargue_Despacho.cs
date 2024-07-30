using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Precargue_Despacho
    {
        [Key]
        public long Pcd_Id { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Cliente { get; set; }

        public int? OF_Id { get; set; }
        public OrdenFacturacion? OrdenFact { get; set; }


        [Column(TypeName = "date")]
        public DateTime Pcd_FechaCrea { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Pcd_HoraCrea { get; set; }


        public long Usua_Crea { get; set; }
        public Usuario? Usuario1 { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Pcd_Observacion { get; set; }

        public int Estado_Id { get; set; }
        public Estado? Estados { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Pcd_FechaModifica { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string? Pcd_HoraModifica { get; set; }


        public long Usua_Modifica { get; set; }
        public Usuario? Usuario2 { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Pcd_ObservacionModifica { get; set; }

    }
}
