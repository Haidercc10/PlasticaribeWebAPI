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
        public DbSet<Models.TiposClientes> Tipos_Clientes { get; set; }
        public DbSet<Models.Tipo_Bodega> Tipos_Bodegas { get; set; }
        public DbSet<Models.Clientes> Clientes { get; set; }

        public DbSet<Models.Unidad_Medida> Unidades_Medidas { get; set; }
        public DbSet<Models.SedesClientes> Sedes_Clientes { get; set; }
        public DbSet<Models.Tipo_Moneda> Tipos_Monedas { get; set; }

        public DbSet<Models.Producto> Productos { get; set; }

        public DbSet<Models.PedidoExterno> Pedidos_Externos { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasOne(prd => prd.UndMed1).WithMany().HasForeignKey(prd => prd.UndMedPeso).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(prd => prd.UndMed2).WithMany().HasForeignKey(prd => prd.UndMedACF).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.SedeCli).WithMany().HasForeignKey(Pext => Pext.SedeCli_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estado>().HasOne<Tipo_Estado>().WithMany().HasForeignKey(Est => Est.TpEstado_Id).OnDelete(DeleteBehavior.Restrict);
            //Sin propiedades de navegación, solo llave foranea. (Clave externa)
            modelBuilder.Entity<Insumo>().HasOne<Unidad_Medida>().WithMany().HasForeignKey(Ins => Ins.UndMed_Id);
            modelBuilder.Entity<Insumo>().HasOne<Categoria_Insumo>().WithMany().HasForeignKey(Ins2 => Ins2.CatInsu_Id);
            //Relaciones de usuarios
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.TipoIdentificacion).WithMany().HasForeignKey(Usu => Usu.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.Area).WithMany().HasForeignKey(Usu => Usu.Area_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.tpUsu).WithMany().HasForeignKey(Usu => Usu.tpUsu_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.RolUsu).WithMany().HasForeignKey(Usu => Usu.RolUsu_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.Empresa).WithMany().HasForeignKey(Usu => Usu.Empresa_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.Estado).WithMany().HasForeignKey(Usu => Usu.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.cajComp).WithMany().HasForeignKey(Usu => Usu.cajComp_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.EPS).WithMany().HasForeignKey(Usu => Usu.eps_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.fPen).WithMany().HasForeignKey(Usu => Usu.fPen_Id).OnDelete(DeleteBehavior.Restrict);
            //Relaciones de productos
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.TpProd).WithMany().HasForeignKey(Prd => Prd.TpProd_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.Estado).WithMany().HasForeignKey(Prd => Prd.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            //Relaciones pedido externo
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Empresa).WithMany().HasForeignKey(Pext => Pext.Empresa_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Estado).WithMany().HasForeignKey(Pext => Pext.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Usua).WithMany().HasForeignKey(Pext => Pext.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            //Relaciones fondo pension
            modelBuilder.Entity<fondoPension>().HasOne(fpe => fpe.TipoIdentificacion).WithMany().HasForeignKey(fpe => fpe.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            //Relaciones EPS
            modelBuilder.Entity<EPS>().HasOne(epss => epss.TipoIdentificacion).WithMany().HasForeignKey(epss => epss.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            //Relaciones Caja Compensación 
            modelBuilder.Entity<cajaCompensacion>().HasOne(ccom => ccom.TipoIdentificacion).WithMany().HasForeignKey(ccom => ccom.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            //Relaciones Extistencia Productos. 
            modelBuilder.Entity<Existencia_Productos>().HasOne(ep => ep.Prod).WithMany().HasForeignKey(ep => ep.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Existencia_Productos>().HasOne(exp => exp.TpBod).WithMany().HasForeignKey(exp => exp.TpBod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Existencia_Productos>().HasOne(expr => expr.UndMed).WithMany().HasForeignKey(expr => expr.UndMed_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Existencia_Productos>().HasOne(Exprd => Exprd.TpMoneda).WithMany().HasForeignKey(Exprd => Exprd.TpMoneda_Id).OnDelete(DeleteBehavior.Restrict);

            //Relación de empresa con tipo de identificación de la empresa
            modelBuilder.Entity<Empresa>().HasOne(emp => emp.TipoIdentificacion).WithMany().HasForeignKey(emp => emp.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
           
            //Relacion de clientes con Tipo de identificacón y Tipo de clientes.
            modelBuilder.Entity<Clientes>().HasOne(cli => cli.TipoIdentificacion).WithMany().HasForeignKey(cli => cli.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Clientes>().HasOne(cli => cli.TPCli).WithMany().HasForeignKey(cli => cli.TPCli_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Clientes>().HasOne(cli => cli.Usua).WithMany().HasForeignKey(cli => cli.usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Clientes>().HasOne(cli => cli.Estado).WithMany().HasForeignKey(cli => cli.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            //Relacipon de sedes de clientes con clientes
            modelBuilder.Entity<SedesClientes>().HasOne(Sedecli => Sedecli.Cli).WithMany().HasForeignKey(Sedecli => Sedecli.Cli_Id).OnDelete(DeleteBehavior.Restrict);
            //Relacion de Tipos de bodegas
            modelBuilder.Entity<Tipo_Bodega>().HasOne(tpBodg => tpBodg.Area).WithMany().HasForeignKey(tpBodg => tpBodg.Area_Id).OnDelete(DeleteBehavior.Restrict);

            //Relación de pedidos_productos
            modelBuilder.Entity<PedidoProducto>().HasKey(pep => new { pep.Prod_Id, pep.PedExt_Id }); //Llave compuesta
            
            modelBuilder.Entity<PedidoProducto>().HasOne<Producto>(ppp => ppp.Product).WithMany(pp => pp.PedExtProd).HasForeignKey(ppp => ppp.Prod_Id);
            modelBuilder.Entity<PedidoProducto>().HasOne<PedidoExterno>(ppp => ppp.PedidoExt).WithMany(pp => pp.PedExtProd).HasForeignKey(ppp => ppp.PedExt_Id);
            

            //modelBuilder.Entity<Pedido_Producto>().HasOne(pep => pep.Prod).WithMany().HasForeignKey(pep => pep.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Pedido_Producto>().HasOne(pep => pep.PedExt).WithMany().HasForeignKey(pep => pep.Prod_Id).OnDelete(DeleteBehavior.Restrict);
        }

        //Fluent API
        public DbSet<Models.Existencia_Productos> Existencias_Productos { get; set; }

        public DbSet<Models.Tipo_Estado> Tipos_Estados { get; set; }

        public DbSet<Models.Categoria_Insumo> Categorias_Insumos { get; set; }

        public DbSet<Models.Insumo> Insumos { get; set; }

        public DbSet<Models.PedidoProducto> PedidosExternos_Productos { get; set; }

    }

}
