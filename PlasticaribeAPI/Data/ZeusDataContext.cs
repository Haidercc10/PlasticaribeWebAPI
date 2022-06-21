using Microsoft.EntityFrameworkCore;

namespace PlasticaribeAPI.Data
{
    public class ZeusDataContext : DbContext
    {
        public ZeusDataContext(DbContextOptions<ZeusDataContext> options) : base(options) { }


    }
}
