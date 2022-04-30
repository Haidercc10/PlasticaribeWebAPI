//CLASE QUE DERIVA DE DBCONTEXT
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Data
{
    public class dataContext : DbContext
    {

        public dataContext(DbContextOptions<dataContext>options) : base(options) { }
 
        public DbSet<PlasticaribeAPI.Models.TipoIdentificacion> TipoIdentificaciones { get; set; }        
        public DbSet<PlasticaribeAPI.Models.Empresa> Empresas { get; set; }
        public DbSet<Models.EPS> EPS { get; set; }
        public DbSet<Models.cajaCompensacion> Cajas_Compensaciones{ get; set; }
        public DbSet<Models.fondoPension> FondosPensiones { get; set; }
        public DbSet<Models.Area> Areas { get; set; }
        public DbSet<Models.Estado> Estados { get; set; }
       

        public DbSet<Models.Tipo_Usuario> Tipos_Usuarios { get; set; }

        public DbSet<Models.Usuario> Usuarios{ get; set; }

        public DbSet<Models.Rol_Usuario> Roles_Usuarios { get; set; }

        public DbSet<Models.Tipo_Producto> Tipos_Productos { get; set; }

    }

}
