using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class BodegasDespacho
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string IdBodega { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string IdUbicacion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string NombreUbicacion { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string IdSubUbicacion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string NombreSubUbicacion { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string IdCubo { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
