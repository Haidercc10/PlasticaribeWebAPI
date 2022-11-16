//CLASE QUE DERIVA DE DBCONTEXT
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Data
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions<dataContext> options) : base(options) { }

        public DbSet<PlasticaribeAPI.Models.TipoIdentificacion> TipoIdentificaciones { get; set; }
        public DbSet<PlasticaribeAPI.Models.Empresa> Empresas { get; set; }
        public DbSet<Models.EPS> EPS { get; set; }
        public DbSet<Models.cajaCompensacion> Cajas_Compensaciones { get; set; }
        public DbSet<Models.fondoPension> FondosPensiones { get; set; }
        public DbSet<Models.Area> Areas { get; set; }
        public DbSet<Models.Estado> Estados { get; set; }
        public DbSet<Models.Tipo_Usuario> Tipos_Usuarios { get; set; }
        public DbSet<Models.Usuario> Usuarios { get; set; }
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
            //Relaciones de productos
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.TpProd).WithMany().HasForeignKey(Prd => Prd.TpProd_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.Estado).WithMany().HasForeignKey(Prd => Prd.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.Pigmt).WithMany().HasForeignKey(Prd => Prd.Pigmt_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(prd => prd.UndMed1).WithMany().HasForeignKey(prd => prd.UndMedPeso).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(prd => prd.UndMed2).WithMany().HasForeignKey(prd => prd.UndMedACF).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.MaterialMP).WithMany().HasForeignKey(Prd => Prd.Material_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(prd => prd.TiposSellados).WithMany().HasForeignKey(prd => prd.TpSellado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().Property(c => c.Prod_Cod).UseIdentityColumn().ValueGeneratedOnAddOrUpdate();

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

            //Relaciones pedido externo
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Empresa).WithMany().HasForeignKey(Pext => Pext.Empresa_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Estado).WithMany().HasForeignKey(Pext => Pext.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Usua).WithMany().HasForeignKey(Pext => Pext.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.SedeCli).WithMany().HasForeignKey(Pext => Pext.SedeCli_Id).OnDelete(DeleteBehavior.Restrict);

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
            modelBuilder.Entity<Clientes>().Property(cli => cli.Cli_Codigo).UseIdentityColumn().ValueGeneratedOnAddOrUpdate();
            //Relacipon de sedes de clientes con clientes
            modelBuilder.Entity<SedesClientes>().HasOne(Sedecli => Sedecli.Cli).WithMany().HasForeignKey(Sedecli => Sedecli.Cli_Id).OnDelete(DeleteBehavior.Restrict);
            //Relacion de Tipos de bodegas
            modelBuilder.Entity<Tipo_Bodega>().HasOne(tpBodg => tpBodg.Area).WithMany().HasForeignKey(tpBodg => tpBodg.Area_Id).OnDelete(DeleteBehavior.Restrict);

            //Relación de pedidos_productos
            modelBuilder.Entity<PedidoProducto>().HasKey(pep => new { pep.Prod_Id, pep.PedExt_Id }); //Llave compuesta
            modelBuilder.Entity<PedidoProducto>().HasOne<Producto>(ppp => ppp.Product).WithMany(pp => pp.PedExtProd).HasForeignKey(ppp => ppp.Prod_Id); //Foranea 1
            modelBuilder.Entity<PedidoProducto>().HasOne<PedidoExterno>(ppp => ppp.PedidoExt).WithMany(pp => pp.PedExtProd).HasForeignKey(ppp => ppp.PedExt_Id); //Foranea 2
            //Llave foranea aparte unidad medida
            modelBuilder.Entity<PedidoProducto>().HasOne(pUnd => pUnd.UndMed).WithMany().HasForeignKey(pUnd => pUnd.UndMed_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones clientes_productos
            modelBuilder.Entity<Cliente_Producto>().HasKey(cpro => new { cpro.Prod_Id, cpro.Cli_Id }); //Llave Compuesta
            modelBuilder.Entity<Cliente_Producto>().HasOne<Clientes>(clipro => clipro.Cli).WithMany(clprod => clprod.CliProd).HasForeignKey(clipro => clipro.Cli_Id);
            modelBuilder.Entity<Cliente_Producto>().HasOne<Producto>(clipro => clipro.Prod).WithMany(clprod => clprod.CliProd).HasForeignKey(clipro => clipro.Prod_Id);

            //Relaciones Materias_Primas
            modelBuilder.Entity<Materia_Prima>().HasOne(mtp => mtp.CatMP).WithMany().HasForeignKey(mtp => mtp.CatMP_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Materia_Prima>().HasOne(mtp => mtp.UndMed).WithMany().HasForeignKey(mtp => mtp.UndMed_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Materia_Prima>().HasOne(mtp => mtp.TpBod).WithMany().HasForeignKey(mtp => mtp.TpBod_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Proveedores
            modelBuilder.Entity<Proveedor>().HasOne(prv => prv.TipoIdentificacion).WithMany().HasForeignKey(prv => prv.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Proveedor>().HasOne(prv => prv.TpProv).WithMany().HasForeignKey(prv => prv.TpProv_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Proveedor>().Property(p => p.Prov_Codigo).UseIdentityColumn().ValueGeneratedOnAddOrUpdate();

            //Relaciones Provedor_MateriaPrima
            modelBuilder.Entity<Provedor_MateriaPrima>().HasKey(provmp => new { provmp.Prov_Id, provmp.MatPri_Id }); //Llave Compuesta Provedor_MateriaPrima 
            modelBuilder.Entity<Provedor_MateriaPrima>().HasOne<Proveedor>(prvmp => prvmp.Prov).WithMany(prvMtp => prvMtp.ProvMatPri).HasForeignKey(prvmp => prvmp.Prov_Id); //Foranea proveedor
            modelBuilder.Entity<Provedor_MateriaPrima>().HasOne<Materia_Prima>(prvmp => prvmp.MatPri).WithMany(prvMtp => prvMtp.ProvMatPri).HasForeignKey(prvmp => prvmp.MatPri_Id); //Foranea materiaprima

            //Relaciones Facturas Compras
            modelBuilder.Entity<Factura_Compra>().HasOne(fcco => fcco.Prov).WithMany().HasForeignKey(facco => facco.Prov_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de proveedor
            modelBuilder.Entity<Factura_Compra>().HasOne(fcco => fcco.Estado).WithMany().HasForeignKey(facco => facco.Estado_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de estado
            modelBuilder.Entity<Factura_Compra>().HasOne(fcco => fcco.Usua).WithMany().HasForeignKey(facco => facco.Usua_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de usuario
            modelBuilder.Entity<Factura_Compra>().HasOne(fcco => fcco.TpDoc).WithMany().HasForeignKey(facco => facco.TpDoc_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de tipo de documento

            //Relaciones FacturasCompras_MateriasPrimas
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.MatPri).WithMany().HasForeignKey(facco => facco.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.Facco).WithMany().HasForeignKey(facco => facco.Facco_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.Tinta).WithMany().HasForeignKey(facco => facco.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.UndMed).WithMany().HasForeignKey(facco => facco.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.Bopp_Generico).WithMany().HasForeignKey(facco => facco.Bopp_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Asignaciones_MatPrima
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.Estado).WithMany().HasForeignKey(asigmp => asigmp.Estado_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.Usua).WithMany().HasForeignKey(asgmpr => asgmpr.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.EstadoOT).WithMany().HasForeignKey(asigmp => asigmp.Estado_OrdenTrabajo).OnDelete(DeleteBehavior.Restrict); //foranea estado OrdenTrabajo


            //Relaciones DetallesAsignaciones_MateriasPrimas
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasKey(damp => new { damp.AsigMp_Id, damp.MatPri_Id }); //Llave Compuesta DetalleAsignacion_MateriaPrima 
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasOne<Asignacion_MatPrima>(dasigmp => dasigmp.AsigMp).WithMany(dasimp => dasimp.DtAsigMatPri).HasForeignKey(dasigmp => dasigmp.AsigMp_Id); //Foranea Asignacion_matpri
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasOne<Materia_Prima>(dasigmp => dasigmp.MatPri).WithMany(dasimp => dasimp.DtAsigMatPri).HasForeignKey(dasigmp => dasigmp.MatPri_Id); //Foranea materiaprima
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasOne(damp => damp.UndMed).WithMany().HasForeignKey(damp => damp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasOne(damp => damp.Proceso).WithMany().HasForeignKey(damp => damp.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso 

            //Relaciones Existencias_MatPrima
            /*modelBuilder.Entity<Existencia_MatPrima>().HasOne(mp => mp.MatePrima).WithMany().HasForeignKey(mp => mp.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de Materia Prima
            modelBuilder.Entity<Existencia_MatPrima>().HasOne(mp => mp.TpBod).WithMany().HasForeignKey(mp => mp.TpBod_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de Tipo bodega
            modelBuilder.Entity<Existencia_MatPrima>().HasOne(mp => mp.UndMed).WithMany().HasForeignKey(mp => mp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de Unidad Medida
            modelBuilder.Entity<Existencia_MatPrima>().HasOne(mp => mp.TpMoneda).WithMany().HasForeignKey(mp => mp.TpMoneda_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de Tipo Moneda*/

            //Relaciones Remisiones
            modelBuilder.Entity<Remision>().HasOne(rem => rem.Prov).WithMany().HasForeignKey(remi => remi.Prov_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de proveedor
            modelBuilder.Entity<Remision>().HasOne(rem => rem.Estado).WithMany().HasForeignKey(remi => remi.Estado_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de estado
            modelBuilder.Entity<Remision>().HasOne(rem => rem.Usua).WithMany().HasForeignKey(remi => remi.Usua_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de usuario
            modelBuilder.Entity<Remision>().HasOne(rem => rem.TpDoc).WithMany().HasForeignKey(remi => remi.TpDoc_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de tipo de documento

            //Relaciones Remisiones - Materias Primas 
            //modelBuilder.Entity<Remision_MateriaPrima>().HasKey(rmp => new { rmp.Rem_Id, rmp.MatPri_Id }); //Llave Compuesta Remision_MateriaPrima 
            //modelBuilder.Entity<Remision_MateriaPrima>().HasOne<Remision>(remi => remi.Rem).WithMany(remmp => remmp.RemiMatPri).HasForeignKey(remi => remi.Rem_Id); //Foranea remision
            //modelBuilder.Entity<Remision_MateriaPrima>().HasOne<Materia_Prima>(fcomp => fcomp.MatPri).WithMany(remmp => remmp.RemiMatPri).HasForeignKey(remi => remi.MatPri_Id); //Foranea materiaprima
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(fcco => fcco.MatPri).WithMany().HasForeignKey(facco => facco.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(fcco => fcco.Rem).WithMany().HasForeignKey(facco => facco.Rem_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(fcco => fcco.Tinta).WithMany().HasForeignKey(facco => facco.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(rmp => rmp.UndMed).WithMany().HasForeignKey(rmp => rmp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(rem => rem.Bopp).WithMany().HasForeignKey(rem => rem.Bopp_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Remisiones_FacturasCompras
            modelBuilder.Entity<Remision_FacturaCompra>().HasKey(remfc => new { remfc.Facco_Id, remfc.Rem_Id}); //Llave Compuesta de facturas compras y remisiones 
            modelBuilder.Entity<Remision_FacturaCompra>().HasOne<Factura_Compra>(refc => refc.Faccom).WithMany(refco => refco.RemiFacco).HasForeignKey(refc => refc.Facco_Id); //Foranea Facturas compras
            modelBuilder.Entity<Remision_FacturaCompra>().HasOne<Remision>(refc => refc.Remi).WithMany(refco => refco.RemiFacco).HasForeignKey(refc => refc.Rem_Id); //Foranea Remisiones

            //Relaciones Recuperado_MatPrima
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.Usua).WithMany().HasForeignKey(rmp => rmp.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.Proceso).WithMany().HasForeignKey(rmp => rmp.Proc_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.TurnoRecMP).WithMany().HasForeignKey(rmp => rmp.Turno_Id).OnDelete(DeleteBehavior.Restrict); //foranea turno
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.UsuaOperador).WithMany().HasForeignKey(rmp => rmp.Usua_Operador).OnDelete(DeleteBehavior.Restrict); //foranea operador


            //Relaciones DetallesRecuperados_MateriasPrimas
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasKey(dremp => new { dremp.RecMp_Id, dremp.MatPri_Id }); //Llave Compuesta de recuperadosmatpri y materias primas 
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasOne<Recuperado_MatPrima>(remp => remp.RecMp).WithMany(rempr => rempr.DetRecMatPri).HasForeignKey(remp => remp.RecMp_Id); //Foranea Recuperado_MatPrima
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasOne<Materia_Prima>(remp => remp.MatPri).WithMany(rempr => rempr.DetRecMatPri).HasForeignKey(remp => remp.MatPri_Id); //Foranea Materia_Prima
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasOne(dremp => dremp.UndMed).WithMany().HasForeignKey(dremp => dremp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasOne(dremp => dremp.TpRecu).WithMany().HasForeignKey(dremp => dremp.TpRecu_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones InventarioInicialXDia_MatPrimas
            modelBuilder.Entity<InventarioInicialXDia_MatPrima>().HasOne(inv => inv.UndMed).WithMany().HasForeignKey(invini => invini.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones Devoluciones_MatPrima
            modelBuilder.Entity<Devolucion_MatPrima>().HasOne(dev => dev.Usua).WithMany().HasForeignKey(devmp => devmp.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones Devoluciones_MatPrima
            //modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasKey(ddmp => new { ddmp.DevMatPri_Id, ddmp.MatPri_Id }); //Llave Compuesta DetalleAsignacion_MateriaPrima 
            //modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne<Devolucion_MatPrima>(ddmp => ddmp.DevMatPri).WithMany(ddmp => ddmp.DetDevMatPri).HasForeignKey(ddmp => ddmp.DevMatPri_Id); //Foranea Asignacion_matpri
            //modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne<Materia_Prima>(ddmp => ddmp.MatPri).WithMany(ddmp => ddmp.DetDevMatPri).HasForeignKey(ddmp => ddmp.MatPri_Id); //Foranea materiaprima
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.DevMatPri).WithMany().HasForeignKey(damp => damp.DevMatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.MatPri).WithMany().HasForeignKey(damp => damp.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.Tinta).WithMany().HasForeignKey(damp => damp.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.UndMed).WithMany().HasForeignKey(damp => damp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.Proceso).WithMany().HasForeignKey(damp => damp.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso

            //Relaciones TINTAS
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.UndMed).WithMany().HasForeignKey(tint => tint.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.CatMP).WithMany().HasForeignKey(tint => tint.CatMP_Id).OnDelete(DeleteBehavior.Restrict); //foranea categorias matprima
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.TpBod).WithMany().HasForeignKey(tint => tint.TpBod_Id).OnDelete(DeleteBehavior.Restrict); //foranea Tipo de bodega
            //modelBuilder.Entity<Tinta>().Property(prop => prop.Tinta_Id).UseIdentityColumn(2000, 1);


            //Relaciones Tintas_MateriasPrimas
            modelBuilder.Entity<Tinta_MateriaPrima>().HasKey(tmp => new { tmp.Tinta_Id, tmp.MatPri_Id }); //Llave Compuesta Tinta_MateriaPrima 
            modelBuilder.Entity<Tinta_MateriaPrima>().HasOne<Tinta>(tinmp => tinmp.Tinta).WithMany(ttmp => ttmp.TintaMatPri).HasForeignKey(ddmp => ddmp.Tinta_Id); //Foranea tintas
            modelBuilder.Entity<Tinta_MateriaPrima>().HasOne<Materia_Prima>(tinmp => tinmp.MatPri).WithMany(ttmp => ttmp.TintaMatPri).HasForeignKey(ddmp => ddmp.MatPri_Id); //Foranea materiaprima 


            //Relaciones Asignacion_MatPrimaXTintas
            modelBuilder.Entity<Asignacion_MatPrimaXTinta>().HasOne(asgmpx => asgmpx.Estado).WithMany().HasForeignKey(asigmpx => asigmpx.Estado_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<Asignacion_MatPrimaXTinta>().HasOne(asgmpx => asgmpx.Usua).WithMany().HasForeignKey(asgmprx => asgmprx.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario
            modelBuilder.Entity<Asignacion_MatPrimaXTinta>().HasOne(asgmpx => asgmpx.Tinta).WithMany().HasForeignKey(asgmprx => asgmprx.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea materia prima
            modelBuilder.Entity<Asignacion_MatPrimaXTinta>().HasOne(asgmpx => asgmpx.UndMed).WithMany().HasForeignKey(asgmprx => asgmprx.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida 

            //Relaciones DetalleAsignacion_MatPrimaXTintas
            //modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasKey(dampxt => new { dampxt.AsigMPxTinta_Id, dampxt.MatPri_Id }); //Llave Compuesta DetalleAsignacion_MateriaPrimaXTinta 
            //modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne<Asignacion_MatPrimaXTinta>(dampxt => dampxt.AsigMPxTinta).WithMany(dampxti => dampxti.DetAsigMPxTinta).HasForeignKey(dampxt => dampxt.AsigMPxTinta_Id); //Foranea Asignacion_matpri
            //modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne<Materia_Prima>(ddmp => ddmp.MatPri).WithMany(ddmp => ddmp.DetAsigMPxTinta).HasForeignKey(ddmp => ddmp.MatPri_Id); //Foranea materiaprima
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.AsigMPxTinta).WithMany().HasForeignKey(damp => damp.AsigMPxTinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea Materia Prima
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.MatPri).WithMany().HasForeignKey(damp => damp.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea Materia Prima
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.TintasDAMPxT).WithMany().HasForeignKey(damp => damp.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea Tinta
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.UndMed).WithMany().HasForeignKey(damp => damp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.Proceso).WithMany().HasForeignKey(damp => damp.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso

            //Relaciones Detalles Asignacion Tintas Para una OT
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasKey(dat => new { dat.AsigMp_Id, dat.Tinta_Id }); //Llave Compuesta DetalleAsignacion_Tinta
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasOne<Asignacion_MatPrima>(dasigmp => dasigmp.AsigMp).WithMany(dastin => dastin.DetAsigTinta).HasForeignKey(dasigmp => dasigmp.AsigMp_Id); //Foranea Asignacion_matpri
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasOne<Tinta>(dasigmp => dasigmp.Tinta).WithMany(dastin => dastin.DetAsigTinta).HasForeignKey(dasigmp => dasigmp.Tinta_Id); //Foranea tintas
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasOne(dat => dat.UndMed).WithMany().HasForeignKey(dat => dat.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasOne(dat => dat.Proceso).WithMany().HasForeignKey(dat => dat.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Proceso

            //Relaciones BOPP
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.UndMed).WithMany().HasForeignKey(dat => dat.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.CatMP).WithMany().HasForeignKey(dat => dat.CatMP_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Categorias
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.TpBod).WithMany().HasForeignKey(dat => dat.TpBod_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Tipos de bodega
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.UndMed2).WithMany().HasForeignKey(dat => dat.UndMed_Kg).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.Usua).WithMany().HasForeignKey(dat => dat.Usua_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Asignacion_BOPP
            modelBuilder.Entity<Asignacion_BOPP>().HasOne(asgb => asgb.Estado).WithMany().HasForeignKey(asig => asig.Estado_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<Asignacion_BOPP>().HasOne(asgb => asgb.Usua).WithMany().HasForeignKey(asig => asig.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario

            //Relaciones DetalleAsignacion_BOPP
            //modelBuilder.Entity<DetalleAsignacion_BOPP>().HasKey(dat => new { dat.AsigBOPP_Id, dat.BOPP_Id }); //Llave Compuesta DetalleAsignacion_BOPP
            modelBuilder.Entity<DetalleAsignacion_BOPP>().HasOne<Asignacion_BOPP>(dasig => dasig.AsigBOPP).WithMany(das => das.DetAsigBOPP).HasForeignKey(dasigmp => dasigmp.AsigBOPP_Id); //Foranea Asignacion_BOPP
            modelBuilder.Entity<DetalleAsignacion_BOPP>().HasOne<BOPP>(dasig => dasig.BOPP).WithMany(das => das.DetAsigBOPP).HasForeignKey(dasigmp => dasigmp.BOPP_Id); //Foranea BOPP
            modelBuilder.Entity<DetalleAsignacion_BOPP>().HasOne(dat => dat.UndMed).WithMany().HasForeignKey(dat => dat.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<DetalleAsignacion_BOPP>().HasOne(dat => dat.Proceso).WithMany().HasForeignKey(dat => dat.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Proceso
            modelBuilder.Entity<DetalleAsignacion_BOPP>().HasOne(asgmp => asgmp.EstadoOT).WithMany().HasForeignKey(asigmp => asigmp.Estado_OrdenTrabajo).OnDelete(DeleteBehavior.Restrict); //foranea estado OrdenTrabajo
            modelBuilder.Entity<DetalleAsignacion_BOPP>().HasOne(asgb => asgb.Tipo_Documento).WithMany().HasForeignKey(asig => asig.TpDoc_Id).OnDelete(DeleteBehavior.Restrict);

            //Archivos
            modelBuilder.Entity<Archivos>().HasOne(Arc => Arc.Categoria).WithMany().HasForeignKey(Arc => Arc.Categoria_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Archivos>().HasOne(Arc => Arc.Usuario).WithMany().HasForeignKey(Arc => Arc.Usua_Id).OnDelete(DeleteBehavior.Restrict);

            //Orden de Trabajo
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.SedeCli).WithMany().HasForeignKey(ot => ot.SedeCli_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Prod).WithMany().HasForeignKey(ot => ot.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Unidad_Medida).WithMany().HasForeignKey(ot => ot.UndMed_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Estado).WithMany().HasForeignKey(ot => ot.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Usuario).WithMany().HasForeignKey(ot => ot.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.PedidoExterno).WithMany().HasForeignKey(ot => ot.PedExt_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Mezcla).WithMany().HasForeignKey(ot => ot.Mezcla_Id).OnDelete(DeleteBehavior.Restrict);

            //OT_Extrusion
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Orden_Trabajo).WithMany().HasForeignKey(ot_ext => ot_ext.Ot_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Material_MatPrima).WithMany().HasForeignKey(ot_ext => ot_ext.Material_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Formato).WithMany().HasForeignKey(ot_ext => ot_ext.Formato_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Pigmento).WithMany().HasForeignKey(ot_ext => ot_ext.Pigmt_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Tratado).WithMany().HasForeignKey(ot_ext => ot_ext.Tratado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Unidad_Medida).WithMany().HasForeignKey(ot_ext => ot_ext.UndMed_Id).OnDelete(DeleteBehavior.Restrict);

            //OT_Impresión
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Orden_Trabajo).WithMany().HasForeignKey(ot_imp => ot_imp.Ot_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tipos_Impresion).WithMany().HasForeignKey(ot_imp => ot_imp.TpImpresion_Id).OnDelete(DeleteBehavior.Restrict);
            /*modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Rodillos).WithMany().HasForeignKey(ot_imp => ot_imp.Rodillo_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Pistas).WithMany().HasForeignKey(ot_imp => ot_imp.Pista_Id).OnDelete(DeleteBehavior.Restrict);*/
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta1).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta1_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta2).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta2_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta3).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta3_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta4).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta4_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta5).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta5_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta6).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta6_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta7).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta7_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta8).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta8_Id).OnDelete(DeleteBehavior.Restrict);

            //OT_Laminado
            modelBuilder.Entity<OT_Laminado>().HasOne(ot_lam => ot_lam.Orden_Trabajo).WithMany().HasForeignKey(ot_lam => ot_lam.OT_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Laminado>().HasOne(ot_lam => ot_lam.Laminado_Capa).WithMany().HasForeignKey(ot_lam => ot_lam.Capa_Id1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Laminado>().HasOne(ot_lam => ot_lam.Laminado_Capa2).WithMany().HasForeignKey(ot_lam => ot_lam.Capa_Id2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Laminado>().HasOne(ot_lam => ot_lam.Laminado_Capa3).WithMany().HasForeignKey(ot_lam => ot_lam.Capa_Id3).OnDelete(DeleteBehavior.Restrict);

            //Mezclas OT
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MaterialMP).WithMany().HasForeignKey(mez => mez.Material_Id).OnDelete(DeleteBehavior.Restrict); /** Llave Primaria Material_MatPrima*/
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP1C1).WithMany().HasForeignKey(mez => mez.MezMaterial_Id1xCapa1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP1C2).WithMany().HasForeignKey(mez => mez.MezMaterial_Id1xCapa2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP1C3).WithMany().HasForeignKey(mez => mez.MezMaterial_Id1xCapa3).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP2C1).WithMany().HasForeignKey(mez => mez.MezMaterial_Id2xCapa1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP2C2).WithMany().HasForeignKey(mez => mez.MezMaterial_Id2xCapa2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP2C3).WithMany().HasForeignKey(mez => mez.MezMaterial_Id2xCapa3).OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP3C1).WithMany().HasForeignKey(mez => mez.MezMaterial_Id3xCapa1).OnDelete(DeleteBehavior.Restrict); /** Llave Primaria Mezcla_Material*/
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP3C2).WithMany().HasForeignKey(mez => mez.MezMaterial_Id3xCapa2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP3C3).WithMany().HasForeignKey(mez => mez.MezMaterial_Id3xCapa3).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP4C1).WithMany().HasForeignKey(mez => mez.MezMaterial_Id4xCapa1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP4C2).WithMany().HasForeignKey(mez => mez.MezMaterial_Id4xCapa2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezMaterial_MP4C3).WithMany().HasForeignKey(mez => mez.MezMaterial_Id4xCapa3).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezPigmento1C1).WithMany().HasForeignKey(mez => mez.MezPigmto_Id1xCapa1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezPigmento1C2).WithMany().HasForeignKey(mez => mez.MezPigmto_Id1xCapa2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezPigmento1C3).WithMany().HasForeignKey(mez => mez.MezPigmto_Id1xCapa3).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezPigmento2C1).WithMany().HasForeignKey(mez => mez.MezPigmto_Id2xCapa1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezPigmento2C2).WithMany().HasForeignKey(mez => mez.MezPigmto_Id2xCapa2).OnDelete(DeleteBehavior.Restrict); /** Llave Primaria Mezcla_Pigmento*/
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.MezPigmento2C3).WithMany().HasForeignKey(mez => mez.MezPigmto_Id2xCapa3).OnDelete(DeleteBehavior.Restrict);            
            modelBuilder.Entity<Mezcla>().HasOne(mez => mez.Usua).WithMany().HasForeignKey(mez => mez.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario 

            //Relaciones EstadosProcesos_OT
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.UnidadMedida).WithMany().HasForeignKey(eOT => eOT.UndMed_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.FallaTecnica).WithMany().HasForeignKey(eOT => eOT.Falla_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.Estado_OT).WithMany().HasForeignKey(eOT => eOT.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.Usuario).WithMany().HasForeignKey(eOT => eOT.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.Producto).WithMany().HasForeignKey(eOT => eOT.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.Clientes).WithMany().HasForeignKey(eOT => eOT.Cli_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Fallas_Tecnicas
            modelBuilder.Entity<Falla_Tecnica>().HasOne(eOT => eOT.TipoFallaTecnica).WithMany().HasForeignKey(eOT => eOT.TipoFalla_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Entrada de Tintas
            modelBuilder.Entity<Entrada_Tintas>().HasOne(et => et.Usua).WithMany().HasForeignKey(et => et.Usua_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Detalles Entrada de Tintas
            modelBuilder.Entity<Detalles_EntradaTintas>().HasKey(dtET => new { dtET.EntTinta_Id, dtET.Tinta_Id });
            modelBuilder.Entity<Detalles_EntradaTintas>().HasOne(dtET => dtET.Entrada_Tinta).WithMany().HasForeignKey(dtET => dtET.EntTinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_EntradaTintas>().HasOne(dtET => dtET.Tintas).WithMany().HasForeignKey(dtET => dtET.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_EntradaTintas>().HasOne(dtET => dtET.UndMed).WithMany().HasForeignKey(dtET => dtET.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Entrada Rollos Productos
            modelBuilder.Entity<EntradaRollo_Producto>().HasOne(erp => erp.Usua).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);


            //Relaciones Detalles Entradas Rollos Productos
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.Estado).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.UndMedida_Rollo).WithMany().HasForeignKey(erp => erp.UndMed_Rollo).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.EntRollo_Producto).WithMany().HasForeignKey(erp => erp.EntRolloProd_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.Prod).WithMany().HasForeignKey(erp => erp.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.UndMedida_Prod).WithMany().HasForeignKey(erp => erp.UndMed_Prod).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.Proceso).WithMany().HasForeignKey(erp => erp.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Asignacion Productos a facturas ventas
            modelBuilder.Entity<AsignacionProducto_FacturaVenta>().HasOne(erp => erp.Usua).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<AsignacionProducto_FacturaVenta>().HasOne(erp => erp.Cliente).WithMany().HasForeignKey(erp => erp.Cli_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<AsignacionProducto_FacturaVenta>().HasOne(erp => erp.Usuario).WithMany().HasForeignKey(erp => erp.Usua_Conductor).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Detalles Asignacion Productos a facturas ventas
            //modelBuilder.Entity<DetallesAsignacionProducto_FacturaVenta>().HasKey(erp => new { erp.AsigProdFV_Id, erp.Prod_Id });
            modelBuilder.Entity<DetallesAsignacionProducto_FacturaVenta>().HasOne<AsignacionProducto_FacturaVenta>(dapfv => dapfv.AsigProducto_FV).WithMany(remmp => remmp.DtAsigProd_FVTA).HasForeignKey(dapfv => dapfv.AsigProdFV_Id); 
            modelBuilder.Entity<DetallesAsignacionProducto_FacturaVenta>().HasOne<Producto>(dapfv => dapfv.Prod).WithMany(remmp => remmp.DtAsigProd_FVTA).HasForeignKey(dapfv => dapfv.Prod_Id); 
            modelBuilder.Entity<DetallesAsignacionProducto_FacturaVenta>().HasOne(erp => erp.UndMedida).WithMany().HasForeignKey(erp => erp.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Devolucion_ProductoFacturado
            modelBuilder.Entity<Devolucion_ProductoFacturado>().HasOne(erp => erp.Cliente).WithMany().HasForeignKey(erp => erp.Cli_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Devolucion_ProductoFacturado>().HasOne(erp => erp.TipoDevolucionPF).WithMany().HasForeignKey(erp => erp.TipoDevProdFact_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Devolucion_ProductoFacturado>().HasOne(x => x.Usua).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones DetalleDevolucion_ProductoFacturado
            //modelBuilder.Entity<DetalleDevolucion_ProductoFacturado>().HasKey(erp => new { erp.DevProdFact_Id, erp.Prod_Id });
            modelBuilder.Entity<DetalleDevolucion_ProductoFacturado>().HasOne<Devolucion_ProductoFacturado>(dapfv => dapfv.DevolucionProdFact).WithMany(remmp => remmp.DtDevProd_Fact).HasForeignKey(dapfv => dapfv.DevProdFact_Id);
            modelBuilder.Entity<DetalleDevolucion_ProductoFacturado>().HasOne<Producto>(dapfv => dapfv.Prod).WithMany(remmp => remmp.DtDevProd_Fact).HasForeignKey(dapfv => dapfv.Prod_Id);
            modelBuilder.Entity<DetalleDevolucion_ProductoFacturado>().HasOne(erp => erp.UndMedida).WithMany().HasForeignKey(erp => erp.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones PreEntrega_RolloDespacho
            modelBuilder.Entity<PreEntrega_RolloDespacho>().HasOne(erp => erp.Usuario).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones PreEntrega_RolloDespacho
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.PreEntregaRollo).WithMany().HasForeignKey(erp => erp.PreEntRollo_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.Proceso).WithMany().HasForeignKey(erp => erp.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.UndMedidaRollo).WithMany().HasForeignKey(erp => erp.UndMed_Rollo).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.Cliente).WithMany().HasForeignKey(erp => erp.Cli_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.Prod).WithMany().HasForeignKey(erp => erp.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.UndMedidaProducto).WithMany().HasForeignKey(erp => erp.UndMed_Producto).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones IngresoRollos_Extrusion
            modelBuilder.Entity<IngresoRollos_Extrusion>().HasOne(x => x.Usua).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            
            //Relaciones DetallesIngRollos_Extrusion
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.IngresoRollos_Extrusion).WithMany().HasForeignKey(x => x.IngRollo_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.Unidad_Medida).WithMany().HasForeignKey(x => x.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.Proceso).WithMany().HasForeignKey(x => x.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones AsignacionRollos_Extrusion
            modelBuilder.Entity<AsignacionRollos_Extrusion>().HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Reliaciones DetallesAsigRollos_Extrusion
            modelBuilder.Entity<DetallesAsgRollos_Extrusion>().HasOne(x => x.AsignacionRollos).WithMany().HasForeignKey(x => x.AsgRollos_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesAsgRollos_Extrusion>().HasOne(x => x.Unidad_Medida).WithMany().HasForeignKey(x => x.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesAsgRollos_Extrusion>().HasOne(x => x.Proceso).WithMany().HasForeignKey(x => x.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesAsgRollos_Extrusion>().HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Rollo_Desecho
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Prod).WithMany().HasForeignKey(erp => erp.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Producto
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Proceso).WithMany().HasForeignKey(erp => erp.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.UndMedida).WithMany().HasForeignKey(erp => erp.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Unidad_Medida
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Turno).WithMany().HasForeignKey(erp => erp.Turno_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Turno
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Estado).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Estado
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Material).WithMany().HasForeignKey(erp => erp.Material_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Material 
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Cono).WithMany().HasForeignKey(erp => erp.Cono_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Cono

            //Relaciones Orden_Compra
            modelBuilder.Entity<Orden_Compra>().HasOne(erp => erp.Proveedor).WithMany().HasForeignKey(erp => erp.Prov_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Producto
            modelBuilder.Entity<Orden_Compra>().HasOne(erp => erp.TipoDoc).WithMany().HasForeignKey(erp => erp.TpDoc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Orden_Compra>().HasOne(erp => erp.Estado).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Orden_Compra>().HasOne(erp => erp.Usua).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso

            //Relaciones Detalle_OrdenCompra
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.Orden_Compra).WithMany().HasForeignKey(erp => erp.Oc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Producto
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.MatPrima).WithMany().HasForeignKey(erp => erp.MatPri_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.Tinta).WithMany().HasForeignKey(erp => erp.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.BOPP).WithMany().HasForeignKey(erp => erp.BOPP_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.UndMed).WithMany().HasForeignKey(erp => erp.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso

            //Ordenes Compras Facturas Compras
            modelBuilder.Entity<OrdenesCompras_FacturasCompras>().HasKey(x => new { x.Oc_Id, x.Facco_Id }); //Llave Compuesta Provedor_MateriaPrima 
            modelBuilder.Entity<OrdenesCompras_FacturasCompras>().HasOne<Orden_Compra>(x => x.Orden_Compra).WithMany(x => x.OrdenFactura).HasForeignKey(x => x.Oc_Id);
            modelBuilder.Entity<OrdenesCompras_FacturasCompras>().HasOne<Factura_Compra>(x => x.Facco).WithMany(x => x.OrdenFactura).HasForeignKey(x => x.Facco_Id);

        }


        //Fluent API
        public DbSet<Models.Existencia_Productos> Existencias_Productos { get; set; }

        public DbSet<Models.Tipo_Estado> Tipos_Estados { get; set; }

        public DbSet<Models.Categoria_Insumo> Categorias_Insumos { get; set; }

        public DbSet<Models.Insumo> Insumos { get; set; }

        public DbSet<Models.PedidoProducto> PedidosExternos_Productos { get; set; }

        public DbSet<Models.Cliente_Producto> Clientes_Productos { get; set; }

        public DbSet<Models.Pigmento> Pigmentos { get; set; }

        public DbSet<Models.Material_MatPrima> Materiales_MatPrima { get; set; }

        public DbSet<Models.Materia_Prima> Materias_Primas { get; set; }

        public DbSet<Models.Categoria_MatPrima> Categorias_MatPrima { get; set; }

        public DbSet<Models.Tipo_Proveedor> Tipos_Proveedores { get; set; }

        public DbSet<Models.Proveedor> Proveedores { get; set; }

        public DbSet<Models.Provedor_MateriaPrima> Proveedores_MateriasPrimas { get; set; }

        public DbSet<Models.Factura_Compra> Facturas_Compras { get; set; }

        public DbSet<Models.FacturaCompra_MateriaPrima> FacturasCompras_MateriaPrimas { get; set; }

        public DbSet<Models.Proceso> Procesos { get; set; }

        public DbSet<Models.Asignacion_MatPrima> Asignaciones_MatPrima { get; set; }

        public DbSet<Models.DetalleAsignacion_MateriaPrima> DetallesAsignaciones_MateriasPrimas { get; set; }

        /*public DbSet<Models.Existencia_MatPrima> Existencias_MatPrima { get; set; }*/

        public DbSet<Models.Tipo_Documento> Tipos_Documentos{ get; set; }

        public DbSet<Models.Remision> Remisiones { get; set; }

        public DbSet<Models.Remision_MateriaPrima> Remisiones_MateriasPrimas { get; set; }

        public DbSet<Models.Remision_FacturaCompra> Remisiones_FacturasCompras { get; set; }

        public DbSet<Models.Tipo_Recuperado> Tipos_Recuperados { get; set; }

        public DbSet<Models.Recuperado_MatPrima> Recuperados_MatPrima { get; set; }

        public DbSet<Models.DetalleRecuperado_MateriaPrima> DetallesRecuperados_MateriasPrimas { get; set; }

        public DbSet<Models.InventarioInicialXDia_MatPrima> InventarioInicialXDias_MatPrima { get; set; }

        public DbSet<Models.Devolucion_MatPrima> Devoluciones_MatPrima { get; set; }

        public DbSet<Models.DetalleDevolucion_MateriaPrima> DetallesDevoluciones_MateriasPrimas { get; set; }

        public DbSet<Models.Tinta> Tintas { get; set; }

        public DbSet<Models.Tinta_MateriaPrima> Tintas_MateriasPrimas { get; set; } 

        public DbSet<Models.Asignacion_MatPrimaXTinta> Asignaciones_MatPrimasXTintas { get; set; }

        public DbSet<Models.DetalleAsignacion_MatPrimaXTinta> DetallesAsignaciones_MatPrimasXTintas { get; set; }

        public DbSet<Models.DetalleAsignacion_Tinta> DetalleAsignaciones_Tintas { get; set; }

        public DbSet<Models.BOPP> BOPP { get; set; }

        public DbSet<Models.Asignacion_BOPP> Asignaciones_BOPP { get; set; }

        public DbSet<Models.DetalleAsignacion_BOPP> DetallesAsignaciones_BOPP { get; set; }

        public DbSet<Models.Archivos> Archivos { get; set; }

        public DbSet<Models.Categorias_Archivos> Categorias_Archivos { get; set; }

        public DbSet<PlasticaribeAPI.Models.Orden_Trabajo>? Orden_Trabajo { get; set; }

        public DbSet<PlasticaribeAPI.Models.Tratado>? Tratado { get; set; }

        public DbSet<PlasticaribeAPI.Models.Formato>? Formato { get; set; }

        public DbSet<PlasticaribeAPI.Models.Tipos_Impresion>? Tipos_Impresion { get; set; }

        public DbSet<PlasticaribeAPI.Models.Pistas>? Pistas { get; set; }

        public DbSet<PlasticaribeAPI.Models.Rodillos>? Rodillos { get; set; }

        public DbSet<PlasticaribeAPI.Models.Laminado_Capa>? Laminado_Capa { get; set; }

        public DbSet<PlasticaribeAPI.Models.Mezcla_Material>? Mezclas_Materiales { get; set; }

        public DbSet<PlasticaribeAPI.Models.Mezcla_Pigmento>? Mezclas_Pigmentos { get; set; }

        public DbSet<PlasticaribeAPI.Models.Mezcla> Mezclas { get; set; }

        public DbSet<PlasticaribeAPI.Models.OT_Extrusion>? OT_Extrusion { get; set; }

        public DbSet<PlasticaribeAPI.Models.OT_Impresion> OT_Impresion { get; set; }

        public DbSet<PlasticaribeAPI.Models.OT_Laminado> OT_Laminado { get; set; }
      
        public DbSet<PlasticaribeAPI.Models.Estados_ProcesosOT> Estados_ProcesosOT { get; set; }

        public DbSet<PlasticaribeAPI.Models.Falla_Tecnica> Fallas_Tecnicas { get; set; }

        public DbSet<PlasticaribeAPI.Models.Tipo_FallaTecnica> Tipos_FallasTecnicas { get; set; }

        public DbSet<PlasticaribeAPI.Models.Entrada_Tintas> Entradas_Tintas { get; set; }

        public DbSet<PlasticaribeAPI.Models.Detalles_EntradaTintas> Detalles_EntradaTintas { get; set; }

        public DbSet<PlasticaribeAPI.Models.EntradaRollo_Producto> EntradasRollos_Productos { get; set; }

        public DbSet<PlasticaribeAPI.Models.DetalleEntradaRollo_Producto> DetallesEntradasRollos_Productos { get; set; }

        public DbSet<PlasticaribeAPI.Models.AsignacionProducto_FacturaVenta> AsignacionesProductos_FacturasVentas { get; set; }

        public DbSet<PlasticaribeAPI.Models.DetallesAsignacionProducto_FacturaVenta> DetallesAsignacionesProductos_FacturasVentas { get; set; }

        public DbSet<PlasticaribeAPI.Models.Devolucion_ProductoFacturado> Devoluciones_ProductosFacturados { get; set; }

        public DbSet<PlasticaribeAPI.Models.DetalleDevolucion_ProductoFacturado> DetallesDevoluciones_ProductosFacturados { get; set; }

        public DbSet<PlasticaribeAPI.Models.TipoDevolucion_ProductoFacturado> TiposDevoluciones_ProductosFacturados { get; set; }

        public DbSet<PlasticaribeAPI.Models.PreEntrega_RolloDespacho> PreEntrega_RollosDespacho { get; set; }

        public DbSet<PlasticaribeAPI.Models.DetallePreEntrega_RolloDespacho> DetallesPreEntrega_RollosDespacho { get; set; }

        public DbSet<PlasticaribeAPI.Models.Turno> Turnos { get; set; }

        public DbSet<PlasticaribeAPI.Models.IngresoRollos_Extrusion> IngresoRollos_Extrusion { get; set; }
        public DbSet<PlasticaribeAPI.Models.DetallesIngRollos_Extrusion> DetallesIngRollos_Extrusion { get; set; }
        public DbSet<PlasticaribeAPI.Models.AsignacionRollos_Extrusion> AsignacionRollos_Extrusion { get; set; }
        public DbSet<PlasticaribeAPI.Models.DetallesAsgRollos_Extrusion> DetallesAsgRollos_Extrusion { get; set; }

        public DbSet<PlasticaribeAPI.Models.Cono> Conos { get; set; }

        public DbSet<PlasticaribeAPI.Models.Rollo_Desecho> Rollos_Desechos { get; set; }

        public DbSet<PlasticaribeAPI.Models.Orden_Compra> Ordenes_Compras { get; set; }
        public DbSet<PlasticaribeAPI.Models.Detalle_OrdenCompra> Detalles_OrdenesCompras { get; set; }
        public DbSet<PlasticaribeAPI.Models.Bopp_Generico> Bopp_Generico { get; set; }
        public DbSet<PlasticaribeAPI.Models.OrdenesCompras_FacturasCompras> OrdenesCompras_FacturasCompras { get; set; }


    }

}
