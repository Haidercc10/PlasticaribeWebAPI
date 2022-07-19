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
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasKey(fcmp => new { fcmp.Facco_Id, fcmp.MatPri_Id }); //Llave Compuesta FacturaCompra_MateriaPrima 
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne<Factura_Compra>(fcomp => fcomp.Facco).WithMany(fccomp => fccomp.FaccoMatPri).HasForeignKey(fcomp => fcomp.Facco_Id); //Foranea factura compra
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne<Materia_Prima>(fcomp => fcomp.MatPri).WithMany(fccomp => fccomp.FaccoMatPri).HasForeignKey(fcomp => fcomp.MatPri_Id); //Foranea materiaprima
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.UndMed).WithMany().HasForeignKey(facco => facco.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones Asignaciones_MatPrima
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.Estado).WithMany().HasForeignKey(asigmp => asigmp.Estado_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.Usua).WithMany().HasForeignKey(asgmpr => asgmpr.Usua_Id).OnDelete(DeleteBehavior.Restrict);

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
            modelBuilder.Entity<Remision_MateriaPrima>().HasKey(rmp => new { rmp.Rem_Id, rmp.MatPri_Id }); //Llave Compuesta Remision_MateriaPrima 
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne<Remision>(remi => remi.Rem).WithMany(remmp => remmp.RemiMatPri).HasForeignKey(remi => remi.Rem_Id); //Foranea remision
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne<Materia_Prima>(fcomp => fcomp.MatPri).WithMany(remmp => remmp.RemiMatPri).HasForeignKey(remi => remi.MatPri_Id); //Foranea materiaprima
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(rmp => rmp.UndMed).WithMany().HasForeignKey(rmp => rmp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones Remisiones_FacturasCompras
            modelBuilder.Entity<Remision_FacturaCompra>().HasKey(remfc => new { remfc.Facco_Id, remfc.Rem_Id}); //Llave Compuesta de facturas compras y remisiones 
            modelBuilder.Entity<Remision_FacturaCompra>().HasOne<Factura_Compra>(refc => refc.Faccom).WithMany(refco => refco.RemiFacco).HasForeignKey(refc => refc.Facco_Id); //Foranea Facturas compras
            modelBuilder.Entity<Remision_FacturaCompra>().HasOne<Remision>(refc => refc.Remi).WithMany(refco => refco.RemiFacco).HasForeignKey(refc => refc.Rem_Id); //Foranea Remisiones

            //Relaciones Recuperado_MatPrima
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.Usua).WithMany().HasForeignKey(rmp => rmp.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.Proceso).WithMany().HasForeignKey(rmp => rmp.Proc_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso


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
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasKey(ddmp => new { ddmp.DevMatPri_Id, ddmp.MatPri_Id }); //Llave Compuesta DetalleAsignacion_MateriaPrima 
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne<Devolucion_MatPrima>(ddmp => ddmp.DevMatPri).WithMany(ddmp => ddmp.DetDevMatPri).HasForeignKey(ddmp => ddmp.DevMatPri_Id); //Foranea Asignacion_matpri
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne<Materia_Prima>(ddmp => ddmp.MatPri).WithMany(ddmp => ddmp.DetDevMatPri).HasForeignKey(ddmp => ddmp.MatPri_Id); //Foranea materiaprima
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.UndMed).WithMany().HasForeignKey(damp => damp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.Proceso).WithMany().HasForeignKey(damp => damp.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso

            //Relaciones TINTAS
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.UndMed).WithMany().HasForeignKey(tint => tint.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.CatMP).WithMany().HasForeignKey(tint => tint.CatMP_Id).OnDelete(DeleteBehavior.Restrict); //foranea categorias matprima
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.TpBod).WithMany().HasForeignKey(tint => tint.TpBod_Id).OnDelete(DeleteBehavior.Restrict); //foranea Tipo de bodega

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
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasKey(dampxt => new { dampxt.AsigMPxTinta_Id, dampxt.MatPri_Id }); //Llave Compuesta DetalleAsignacion_MateriaPrimaXTinta 
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne<Asignacion_MatPrimaXTinta>(dampxt => dampxt.AsigMPxTinta).WithMany(dampxti => dampxti.DetAsigMPxTinta).HasForeignKey(dampxt => dampxt.AsigMPxTinta_Id); //Foranea Asignacion_matpri
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne<Materia_Prima>(ddmp => ddmp.MatPri).WithMany(ddmp => ddmp.DetAsigMPxTinta).HasForeignKey(ddmp => ddmp.MatPri_Id); //Foranea materiaprima
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
    }

}
