using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Toma_Fisica
    {
        [Key]
        public int Toma_Id { get; set; }

        public int? InvSnap_Id { get; set; } 
        public Inventarios_Snapshot? Inventario_Snapshot { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Toma_Descripcion { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Tipo_Inventario { get; set; }

        public int TpBod_Id { get; set; }
        public Tipo_Bodega? Tipo_Bodega { get; set; }

        [Column(TypeName = "date")]
        public DateTime Toma_FechaCreacion { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Toma_HoraCreacion { get; set; } 

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        public int Estado_Id { get; set; }
        public Estado? Estado { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Toma_FechaCierre { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Toma_HoraCierre { get; set; }

        public long Usua_Cierre { get; set; }
        public Usuario? Usuario2 { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? Toma_Observacion { get; set; }

    }
}
