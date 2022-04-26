//CLASE QUE DERIVA DE DBCONTEXT
using Microsoft.EntityFrameworkCore;

namespace PlasticaribeAPI.Data
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions<dataContext> options) : base(options)
        { }
            public DbSet<Models.Area> Areas { get; set; }
             
        
    }
}
