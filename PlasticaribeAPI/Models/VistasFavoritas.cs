using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class VistasFavoritas
    {
        [Key]
        public int VistasFav_Id { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
        public int VistaFav_Num1 { get; set; }
        public int VistaFav_Num2 { get; set; }
        public int VistaFav_Num3 { get; set; }
        public int VistaFav_Num4 { get; set; }
        public int VistaFav_Num5 { get; set; }
        public DateTime VistaFav_Fecha { get; set; }
        public string VistaFav_Hora { get; set; }

    }
}
