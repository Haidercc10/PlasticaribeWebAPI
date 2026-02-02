using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Inventarios_Snapshot
    {
        [Key]
        public int InvSnap_Id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string InvSnap_Descripcion { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Tipo_Inventario { get; set; }

        public int TpBod_Id { get; set; }
        public Tipo_Bodega? Tipo_Bodega { get; set; }

        [Column(TypeName = "date")]
        public DateTime InvSnap_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string InvSnap_Hora { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? InvSnap_Observacion { get; set; }
    }
}
