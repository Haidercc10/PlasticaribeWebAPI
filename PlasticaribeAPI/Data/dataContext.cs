//CLASE QUE DERIVA DE DBCONTEXT
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Data
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions<dataContext> options) : base(options)
        { }

        public DbSet<Models.Rol> Roles { get; set; }
        public DbSet<PlasticaribeAPI.Models.TipoIdentificacion> TipoIdentificaciones { get; set; }
        public DbSet<PlasticaribeAPI.Models.TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<PlasticaribeAPI.Models.Empresa> Empresas { get; set; }
        //public DbSet<Models.Area> Areas { get; set; }

    }
}
