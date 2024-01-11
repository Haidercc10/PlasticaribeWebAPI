using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Vistas_Permisos
    {
        [Key]
        public int Vp_Id { get; set; }
        public string Vp_Nombre { get; set; }
        public string Vp_Icono_Menu { get; set; }
        public string Vp_Icono_Dock { get; set; }
        public string Vp_Ruta { get; set; }
        public string Vp_Categoria { get; set; }
        public string Vp_Id_Roles { get; set; }
        public int Vp_Estado { get; set; }
        public Estado? Estado { get; set;}
    }
}
