//CLASE QUE DERIVA DE DBCONTEXT
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Data
{
    public class dataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public dataContext(DbContextOptions<dataContext> options, IConfiguration configuration) : base(options) { Configuration = configuration;  }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
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
        public DbSet<PlasticaribeAPI.Models.Remision_OrdenCompra> Remision_OrdenCompra { get; set; }
        public DbSet<PlasticaribeAPI.Models.VistasFavoritas> VistasFavoritas { get; set; }
        public DbSet<PlasticaribeAPI.Models.Tipo_Activo> Tipos_Activos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Tipo_Mantenimiento> Tipos_Mantenimientos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Activo> Activos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Mantenimiento> Mantenimientos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Detalle_Mantenimiento> Detalles_Mantenimientos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Pedido_Mantenimiento> Pedidos_Mantenimientos { get; set; }
        public DbSet<PlasticaribeAPI.Models.DetallePedido_Mantenimiento> DetallesPedidos_Mantenimientos { get; set; }
        public DbSet<PlasticaribeAPI.Models.MovimientosAplicacion> MovimientosAplicacion { get; set; }
        public DbSet<PlasticaribeAPI.Models.Desperdicio> Desperdicios { get; set; }
        public DbSet<PlasticaribeAPI.Models.Tipos_Sellados> Tipos_Sellados { get; set; }
        public DbSet<PlasticaribeAPI.Models.OT_Sellado_Corte> OT_Sellado_Corte { get; set; }
        public DbSet<PlasticaribeAPI.Models.Log_Transacciones> Log_Transacciones { get; set; }
        public DbSet<PlasticaribeAPI.Models.Log_Errores> Log_Errores { get; set; }
        public DbSet<PlasticaribeAPI.Models.Web_ContactoCorreo> Web_ContactoCorreo { get; set; }
        public DbSet<PlasticaribeAPI.Models.Inventario_Mensual_Productos> Inventario_Mensual_Productos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Terceros> Terceros { get; set; }
        public DbSet<PlasticaribeAPI.Models.Orden_Maquila> Orden_Maquila { get; set; }
        public DbSet<PlasticaribeAPI.Models.Detalle_OrdenMaquila> Detalle_OrdenMaquila { get; set; }
        public DbSet<PlasticaribeAPI.Models.Facturacion_OrdenMaquila> Facturacion_OrdenMaquila { get; set; }
        public DbSet<PlasticaribeAPI.Models.DetalleFacturacion_OrdenMaquila> DetalleFacturacion_OrdenMaquila { get; set; }
        public DbSet<PlasticaribeAPI.Models.OrdenMaquila_Facturacion> OrdenMaquila_Facturacion { get; set; }
        public DbSet<PlasticaribeAPI.Models.Tickets> Tickets { get; set; }
        public DbSet<PlasticaribeAPI.Models.Tickets_Revisados> Tickets_Revisados { get; set; }
        public DbSet<PlasticaribeAPI.Models.Detalles_SolicitudMateriaPrima> Detalles_SolicitudMateriaPrima { get; set; }
        public DbSet<PlasticaribeAPI.Models.Solicitud_MateriaPrima> Solicitud_MateriaPrima { get; set; }
        public DbSet<PlasticaribeAPI.Models.SolicitudesMP_OrdenesCompra> SolicitudesMP_OrdenesCompra { get; set; }
        public DbSet<PlasticaribeAPI.Models.EventosCalendario> EventosCalendario { get; set; }
        public DbSet<PlasticaribeAPI.Models.Solicitud_MatPrimaExtrusion> Solicitud_MatPrimaExtrusion { get; set; }
        public DbSet<PlasticaribeAPI.Models.DetSolicitud_MatPrimaExtrusion> DetSolicitud_MatPrimaExtrusion { get; set; }
        public DbSet<PlasticaribeAPI.Models.Tipo_Solicitud_Rollos_Areas> Tipo_Solicitud_Rollos_Areas { get; set; }
        public DbSet<PlasticaribeAPI.Models.Solicitud_Rollos_Areas> Solicitud_Rollos_Areas { get; set; }
        public DbSet<PlasticaribeAPI.Models.Detalles_SolicitudRollos> Detalles_SolicitudRollos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Bodegas_Rollos> Bodegas_Rollos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Detalles_BodegasRollos> Detalles_BodegasRollos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Vistas_Permisos> Vistas_Permisos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Formato_Documentos> Formato_Documentos { get; set; }
        public DbSet<PlasticaribeAPI.Models.Facturas_Invergoal_Inversuez> Facturas_Invergoal_Inversuez { get; set; }
        public DbSet<PlasticaribeAPI.Models.Costos_Empresas_Anios> Costos_Empresas_Anios { get; set; }
        public DbSet<PlasticaribeAPI.Models.Nomina_Plasticaribe> Nomina_Plasticaribe { get; set; }
        public DbSet<PlasticaribeAPI.Models.Tipos_Nomina> Tipos_Nomina { get; set; }
        public DbSet<PlasticaribeAPI.Models.Certificados_Calidad> Certificados_Calidad { get; set; }
        public DbSet<PlasticaribeAPI.Models.ControlCalidad_CorteDoblado> ControlCalidad_CorteDoblado { get; set; }
        public DbSet<PlasticaribeAPI.Models.ControlCalidad_Impresion> ControlCalidad_Impresion { get; set; }
        public DbSet<PlasticaribeAPI.Models.ControlCalidad_Extrusion> ControlCalidad_Extrusion { get; set; }
        public DbSet<PlasticaribeAPI.Models.ControlCalidad_Sellado> ControlCalidad_Sellado { get; set; }
        public DbSet<PlasticaribeAPI.Models.Movimientros_Entradas_MP> Movimientros_Entradas_MP { get; set; }
        public DbSet<PlasticaribeAPI.Models.Entradas_Salidas_MP> Entradas_Salidas_MP { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relaciones de productos
            //modelBuilder.Entity<Producto>().ToTable(tb => tb.HasTrigger("Auditoria_Productos"));
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.TpProd).WithMany().HasForeignKey(Prd => Prd.TpProd_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.Estado).WithMany().HasForeignKey(Prd => Prd.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.Pigmt).WithMany().HasForeignKey(Prd => Prd.Pigmt_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(prd => prd.UndMed1).WithMany().HasForeignKey(prd => prd.UndMedPeso).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(prd => prd.UndMed2).WithMany().HasForeignKey(prd => prd.UndMedACF).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(Prd => Prd.MaterialMP).WithMany().HasForeignKey(Prd => Prd.Material_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().HasOne(prd => prd.TiposSellados).WithMany().HasForeignKey(prd => prd.TpSellado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Producto>().Property(c => c.Prod_Cod).UseIdentityColumn().ValueGeneratedOnAddOrUpdate();

            //modelBuilder.Entity<Estado>().ToTable(tb => tb.HasTrigger("Auditoria_Estados"));
            modelBuilder.Entity<Estado>().HasOne<Tipo_Estado>().WithMany().HasForeignKey(Est => Est.TpEstado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Insumo>().HasOne<Unidad_Medida>().WithMany().HasForeignKey(Ins => Ins.UndMed_Id);
            modelBuilder.Entity<Insumo>().HasOne<Categoria_Insumo>().WithMany().HasForeignKey(Ins2 => Ins2.CatInsu_Id);

            //Relaciones de usuarios
            modelBuilder.Entity<Usuario>().ToTable(tb => tb.HasTrigger("Auditoria_Usuarios"));
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.TipoIdentificacion).WithMany().HasForeignKey(Usu => Usu.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.Area).WithMany().HasForeignKey(Usu => Usu.Area_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.tpUsu).WithMany().HasForeignKey(Usu => Usu.tpUsu_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.RolUsu).WithMany().HasForeignKey(Usu => Usu.RolUsu_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.Empresa).WithMany().HasForeignKey(Usu => Usu.Empresa_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.Estado).WithMany().HasForeignKey(Usu => Usu.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.cajComp).WithMany().HasForeignKey(Usu => Usu.cajComp_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.EPS).WithMany().HasForeignKey(Usu => Usu.eps_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().HasOne(Usu => Usu.fPen).WithMany().HasForeignKey(Usu => Usu.fPen_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>().Property(c => c.Usua_Codigo).UseIdentityColumn().ValueGeneratedOnAddOrUpdate();

            //Relaciones pedido externo
            //modelBuilder.Entity<PedidoExterno>().ToTable(tb => tb.HasTrigger("Auditoria_Pedidos_Externos"));
            modelBuilder.Entity<PedidoExterno>().ToTable(tb => tb.HasTrigger("CrearPedidos_Zeus"));
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Empresa).WithMany().HasForeignKey(Pext => Pext.Empresa_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Estado).WithMany().HasForeignKey(Pext => Pext.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Usua).WithMany().HasForeignKey(Pext => Pext.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.Creador).WithMany().HasForeignKey(Pext => Pext.Creador_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoExterno>().HasOne(Pext => Pext.SedeCli).WithMany().HasForeignKey(Pext => Pext.SedeCli_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones fondo pension
            //modelBuilder.Entity<fondoPension>().ToTable(tb => tb.HasTrigger("Auditoria_FondosPensiones"));
            modelBuilder.Entity<fondoPension>().HasOne(fpe => fpe.TipoIdentificacion).WithMany().HasForeignKey(fpe => fpe.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones EPS
            //modelBuilder.Entity<EPS>().ToTable(tb => tb.HasTrigger("Auditoria_EPS"));
            modelBuilder.Entity<EPS>().HasOne(epss => epss.TipoIdentificacion).WithMany().HasForeignKey(epss => epss.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Caja Compensación 
            //modelBuilder.Entity<cajaCompensacion>().ToTable(tb => tb.HasTrigger("Auditoria_Cajas_Compensaciones"));
            modelBuilder.Entity<cajaCompensacion>().HasOne(ccom => ccom.TipoIdentificacion).WithMany().HasForeignKey(ccom => ccom.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Extistencia Productos.
            //modelBuilder.Entity<Existencia_Productos>().ToTable(tb => tb.HasTrigger("Auditoria_Existencias_Productos"));
            modelBuilder.Entity<Existencia_Productos>().HasOne(ep => ep.Prod).WithMany().HasForeignKey(ep => ep.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Existencia_Productos>().HasOne(exp => exp.TpBod).WithMany().HasForeignKey(exp => exp.TpBod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Existencia_Productos>().HasOne(expr => expr.UndMed).WithMany().HasForeignKey(expr => expr.UndMed_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Existencia_Productos>().HasOne(Exprd => Exprd.TpMoneda).WithMany().HasForeignKey(Exprd => Exprd.TpMoneda_Id).OnDelete(DeleteBehavior.Restrict);

            //Relación de empresa con tipo de identificación de la empresa
            //modelBuilder.Entity<Empresa>().ToTable(tb => tb.HasTrigger("Auditoria_Empresas"));
            modelBuilder.Entity<Empresa>().HasOne(emp => emp.TipoIdentificacion).WithMany().HasForeignKey(emp => emp.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);

            //Relacion de clientes con Tipo de identificacón y Tipo de clientes.
            //modelBuilder.Entity<Clientes>().ToTable(tb => tb.HasTrigger("Auditoria_Clientes"));
            modelBuilder.Entity<Clientes>().HasOne(cli => cli.TipoIdentificacion).WithMany().HasForeignKey(cli => cli.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Clientes>().HasOne(cli => cli.TPCli).WithMany().HasForeignKey(cli => cli.TPCli_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Clientes>().HasOne(cli => cli.Usua).WithMany().HasForeignKey(cli => cli.usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Clientes>().HasOne(cli => cli.Estado).WithMany().HasForeignKey(cli => cli.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Clientes>().Property(cli => cli.Cli_Codigo).UseIdentityColumn().ValueGeneratedOnAddOrUpdate();

            //Relacipon de sedes de clientes con clientes
            //modelBuilder.Entity<SedesClientes>().ToTable(tb => tb.HasTrigger("Auditoria_Sedes_Clientes"));
            modelBuilder.Entity<SedesClientes>().HasOne(Sedecli => Sedecli.Cli).WithMany().HasForeignKey(Sedecli => Sedecli.Cli_Id).OnDelete(DeleteBehavior.Restrict);

            //Relacion de Tipos de bodegas
            //modelBuilder.Entity<Tipo_Bodega>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Bodegas"));
            modelBuilder.Entity<Tipo_Bodega>().HasOne(tpBodg => tpBodg.Area).WithMany().HasForeignKey(tpBodg => tpBodg.Area_Id).OnDelete(DeleteBehavior.Restrict);

            //Relación de pedidos_productos
            //modelBuilder.Entity<PedidoProducto>().ToTable(tb => tb.HasTrigger("Auditoria_PedidosExternos_Productos"));
            modelBuilder.Entity<PedidoProducto>().HasOne(pUnd => pUnd.PedidoExt).WithMany().HasForeignKey(pUnd => pUnd.PedExt_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoProducto>().HasOne(pUnd => pUnd.Product).WithMany().HasForeignKey(pUnd => pUnd.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoProducto>().HasOne(pUnd => pUnd.UndMed).WithMany().HasForeignKey(pUnd => pUnd.UndMed_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones clientes_productos
            modelBuilder.Entity<Cliente_Producto>().HasOne(clipro => clipro.Prod).WithMany().HasForeignKey(clipro => clipro.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cliente_Producto>().HasOne(clipro => clipro.Cli).WithMany().HasForeignKey(clipro => clipro.Cli_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Materias_Primas
            modelBuilder.Entity<Materia_Prima>().ToTable(tb => tb.HasTrigger("Auditoria_Materias_Primas"));
            modelBuilder.Entity<Materia_Prima>().ToTable(tb => tb.HasTrigger("TR_InventarioDiarioMateriasPrimas"));
            modelBuilder.Entity<Materia_Prima>().HasOne(mtp => mtp.CatMP).WithMany().HasForeignKey(mtp => mtp.CatMP_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Materia_Prima>().HasOne(mtp => mtp.UndMed).WithMany().HasForeignKey(mtp => mtp.UndMed_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Materia_Prima>().HasOne(mtp => mtp.TpBod).WithMany().HasForeignKey(mtp => mtp.TpBod_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Proveedores
            //modelBuilder.Entity<Proveedor>().ToTable(tb => tb.HasTrigger("Auditoria_Proveedores"));
            modelBuilder.Entity<Proveedor>().HasOne(prv => prv.TipoIdentificacion).WithMany().HasForeignKey(prv => prv.TipoIdentificacion_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Proveedor>().HasOne(prv => prv.TpProv).WithMany().HasForeignKey(prv => prv.TpProv_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Proveedor>().Property(p => p.Prov_Codigo).UseIdentityColumn().ValueGeneratedOnAddOrUpdate();

            //Relaciones Provedor_MateriaPrima
            //modelBuilder.Entity<Provedor_MateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Proveedores_MateriasPrimas"));
            modelBuilder.Entity<Provedor_MateriaPrima>().HasOne(fcco => fcco.Prov).WithMany().HasForeignKey(facco => facco.Prov_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de proveedor
            modelBuilder.Entity<Provedor_MateriaPrima>().HasOne(fcco => fcco.MatPri).WithMany().HasForeignKey(facco => facco.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de estado

            //Relaciones Facturas Compras
            //modelBuilder.Entity<Factura_Compra>().ToTable(tb => tb.HasTrigger("Auditoria_Facturas_Compras"));
            modelBuilder.Entity<Factura_Compra>().HasOne(fcco => fcco.Prov).WithMany().HasForeignKey(facco => facco.Prov_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de proveedor
            modelBuilder.Entity<Factura_Compra>().HasOne(fcco => fcco.Estado).WithMany().HasForeignKey(facco => facco.Estado_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de estado
            modelBuilder.Entity<Factura_Compra>().HasOne(fcco => fcco.Usua).WithMany().HasForeignKey(facco => facco.Usua_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de usuario
            modelBuilder.Entity<Factura_Compra>().HasOne(fcco => fcco.TpDoc).WithMany().HasForeignKey(facco => facco.TpDoc_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de tipo de documento

            //Relaciones FacturasCompras_MateriasPrimas
            //modelBuilder.Entity<FacturaCompra_MateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_FacturasCompras_MateriaPrimas"));
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.MatPri).WithMany().HasForeignKey(facco => facco.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.Facco).WithMany().HasForeignKey(facco => facco.Facco_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.Tinta).WithMany().HasForeignKey(facco => facco.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.UndMed).WithMany().HasForeignKey(facco => facco.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<FacturaCompra_MateriaPrima>().HasOne(fcco => fcco.Bopp_Generico).WithMany().HasForeignKey(facco => facco.Bopp_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Asignaciones_MatPrima
            //modelBuilder.Entity<Asignacion_MatPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Asignaciones_MatPrima"));
            modelBuilder.Entity<Asignacion_MatPrima>().ToTable(tb => tb.HasTrigger("TR_EstadosOTAsignado"));
            modelBuilder.Entity<Asignacion_MatPrima>().ToTable(tb => tb.HasTrigger("TR_IU_ActualizaEstados_EnEstadosProcesosOT"));
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.Estado).WithMany().HasForeignKey(asigmp => asigmp.Estado_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.Usua).WithMany().HasForeignKey(asgmpr => asgmpr.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.EstadoOT).WithMany().HasForeignKey(asigmp => asigmp.Estado_OrdenTrabajo).OnDelete(DeleteBehavior.Restrict); //foranea estado OrdenTrabajo
            modelBuilder.Entity<Asignacion_MatPrima>().HasOne(asgmp => asgmp.SolMatPrima_Extrusion).WithMany().HasForeignKey(asigmp => asigmp.SolMpExt_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado OrdenTrabajo

            //Relaciones DetallesAsignaciones_MateriasPrimas
            //modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesAsignaciones_MateriasPrimas"));
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().ToTable(tb => tb.HasTrigger("ActualizarMatPrima_AsignadaEPOT"));
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasOne(damp => damp.AsigMp).WithMany().HasForeignKey(damp => damp.AsigMp_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasOne(damp => damp.MatPri).WithMany().HasForeignKey(damp => damp.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso 
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasOne(damp => damp.UndMed).WithMany().HasForeignKey(damp => damp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleAsignacion_MateriaPrima>().HasOne(damp => damp.Proceso).WithMany().HasForeignKey(damp => damp.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso 

            //Relaciones Remisiones
            modelBuilder.Entity<Remision>().HasOne(rem => rem.Prov).WithMany().HasForeignKey(remi => remi.Prov_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de proveedor
            modelBuilder.Entity<Remision>().HasOne(rem => rem.Estado).WithMany().HasForeignKey(remi => remi.Estado_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de estado
            modelBuilder.Entity<Remision>().HasOne(rem => rem.Usua).WithMany().HasForeignKey(remi => remi.Usua_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de usuario
            modelBuilder.Entity<Remision>().HasOne(rem => rem.TpDoc).WithMany().HasForeignKey(remi => remi.TpDoc_Id).OnDelete(DeleteBehavior.Restrict); //Foranea de tipo de documento

            //Relaciones Remisiones - Materias Primas
            //modelBuilder.Entity<Remision_MateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Remisiones_MateriasPrimas"));
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(fcco => fcco.MatPri).WithMany().HasForeignKey(facco => facco.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(fcco => fcco.Rem).WithMany().HasForeignKey(facco => facco.Rem_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(fcco => fcco.Tinta).WithMany().HasForeignKey(facco => facco.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(rmp => rmp.UndMed).WithMany().HasForeignKey(rmp => rmp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Remision_MateriaPrima>().HasOne(rem => rem.Bopp).WithMany().HasForeignKey(rem => rem.Bopp_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Remisiones_FacturasCompras
            //modelBuilder.Entity<Remision_FacturaCompra>().ToTable(tb => tb.HasTrigger("Auditoria_Remisiones_FacturasCompras"));
            modelBuilder.Entity<Remision_FacturaCompra>().HasOne(rmp => rmp.Faccom).WithMany().HasForeignKey(rmp => rmp.Facco_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario
            modelBuilder.Entity<Remision_FacturaCompra>().HasOne(rmp => rmp.Remi).WithMany().HasForeignKey(rmp => rmp.Rem_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso

            //Relaciones Recuperado_MatPrima
            //modelBuilder.Entity<Recuperado_MatPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Recuperados_MatPrima"));
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.Usua).WithMany().HasForeignKey(rmp => rmp.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.Proceso).WithMany().HasForeignKey(rmp => rmp.Proc_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.TurnoRecMP).WithMany().HasForeignKey(rmp => rmp.Turno_Id).OnDelete(DeleteBehavior.Restrict); //foranea turno
            modelBuilder.Entity<Recuperado_MatPrima>().HasOne(rmp => rmp.UsuaOperador).WithMany().HasForeignKey(rmp => rmp.Usua_Operador).OnDelete(DeleteBehavior.Restrict); //foranea operador

            //Relaciones DetallesRecuperados_MateriasPrimas
            //modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesRecuperados_MateriasPrimas"));
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasOne(dremp => dremp.RecMp).WithMany().HasForeignKey(dremp => dremp.RecMp_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasOne(dremp => dremp.MatPri).WithMany().HasForeignKey(dremp => dremp.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasOne(dremp => dremp.UndMed).WithMany().HasForeignKey(dremp => dremp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleRecuperado_MateriaPrima>().HasOne(dremp => dremp.TpRecu).WithMany().HasForeignKey(dremp => dremp.TpRecu_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones InventarioInicialXDia_MatPrimas
            //modelBuilder.Entity<InventarioInicialXDia_MatPrima>().ToTable(tb => tb.HasTrigger("Auditoria_InventarioInicialXDias_MatPrima"));
            modelBuilder.Entity<InventarioInicialXDia_MatPrima>().HasOne(inv => inv.UndMed).WithMany().HasForeignKey(invini => invini.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones Devoluciones_MatPrima
            //modelBuilder.Entity<Devolucion_MatPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Devoluciones_MatPrima"));
            modelBuilder.Entity<Devolucion_MatPrima>().HasOne(dev => dev.Usua).WithMany().HasForeignKey(devmp => devmp.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones Devoluciones_MatPrima
            //modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesDevoluciones_MateriasPrimas"));
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().ToTable(tb => tb.HasTrigger("RestarCantMatPrimaAsignada_EPOTxDevolucion"));
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.DevMatPri).WithMany().HasForeignKey(damp => damp.DevMatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.MatPri).WithMany().HasForeignKey(damp => damp.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.Tinta).WithMany().HasForeignKey(damp => damp.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.UndMed).WithMany().HasForeignKey(damp => damp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(ddmp => ddmp.Proceso).WithMany().HasForeignKey(damp => damp.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso
            modelBuilder.Entity<DetalleDevolucion_MateriaPrima>().HasOne(erp => erp.Bopp).WithMany().HasForeignKey(erp => erp.BOPP_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones TINTAS
            modelBuilder.Entity<Tinta>().ToTable(tb => tb.HasTrigger("Auditoria_Tintas"));
            modelBuilder.Entity<Tinta>().ToTable(tb => tb.HasTrigger("CrearTintas_DesdePBDD"));
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.UndMed).WithMany().HasForeignKey(tint => tint.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.CatMP).WithMany().HasForeignKey(tint => tint.CatMP_Id).OnDelete(DeleteBehavior.Restrict); //foranea categorias matprima
            modelBuilder.Entity<Tinta>().HasOne(tin => tin.TpBod).WithMany().HasForeignKey(tint => tint.TpBod_Id).OnDelete(DeleteBehavior.Restrict); //foranea Tipo de bodega

            //Relaciones Tintas_MateriasPrimas
            //modelBuilder.Entity<Tinta_MateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Tintas_MateriasPrimas")); 
            modelBuilder.Entity<Tinta_MateriaPrima>().HasOne(tin => tin.Tinta).WithMany().HasForeignKey(tint => tint.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<Tinta_MateriaPrima>().HasOne(tin => tin.MatPri).WithMany().HasForeignKey(tint => tint.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida

            //Relaciones Asignacion_MatPrimaXTintas
            //modelBuilder.Entity<Asignacion_MatPrimaXTinta>().ToTable(tb => tb.HasTrigger("Auditoria_Asignaciones_MatPrimasXTintas"));
            modelBuilder.Entity<Asignacion_MatPrimaXTinta>().HasOne(asgmpx => asgmpx.Estado).WithMany().HasForeignKey(asigmpx => asigmpx.Estado_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<Asignacion_MatPrimaXTinta>().HasOne(asgmpx => asgmpx.Usua).WithMany().HasForeignKey(asgmprx => asgmprx.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario
            modelBuilder.Entity<Asignacion_MatPrimaXTinta>().HasOne(asgmpx => asgmpx.Tinta).WithMany().HasForeignKey(asgmprx => asgmprx.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea materia prima
            modelBuilder.Entity<Asignacion_MatPrimaXTinta>().HasOne(asgmpx => asgmpx.UndMed).WithMany().HasForeignKey(asgmprx => asgmprx.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida 

            //Relaciones DetalleAsignacion_MatPrimaXTintas
            //modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesAsignaciones_MatPrimasXTintas"));
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.AsigMPxTinta).WithMany().HasForeignKey(damp => damp.AsigMPxTinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea Materia Prima
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.MatPri).WithMany().HasForeignKey(damp => damp.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea Materia Prima
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.TintasDAMPxT).WithMany().HasForeignKey(damp => damp.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea Tinta
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.UndMed).WithMany().HasForeignKey(damp => damp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea unidad medida
            modelBuilder.Entity<DetalleAsignacion_MatPrimaXTinta>().HasOne(ddmp => ddmp.Proceso).WithMany().HasForeignKey(damp => damp.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso

            //Relaciones Detalles Asignacion Tintas Para una OT
            //modelBuilder.Entity<DetalleAsignacion_Tinta>().ToTable(tb => tb.HasTrigger("Auditoria_DetalleAsignaciones_Tintas"));
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasOne(dat => dat.AsigMp).WithMany().HasForeignKey(dat => dat.AsigMp_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasOne(dat => dat.Tinta).WithMany().HasForeignKey(dat => dat.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasOne(dat => dat.UndMed).WithMany().HasForeignKey(dat => dat.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<DetalleAsignacion_Tinta>().HasOne(dat => dat.Proceso).WithMany().HasForeignKey(dat => dat.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Proceso

            //Relaciones BOPP
            modelBuilder.Entity<BOPP>().ToTable(tb => tb.HasTrigger("Auditoria_BOPP"));
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.UndMed).WithMany().HasForeignKey(dat => dat.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.CatMP).WithMany().HasForeignKey(dat => dat.CatMP_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Categorias
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.TpBod).WithMany().HasForeignKey(dat => dat.TpBod_Id).OnDelete(DeleteBehavior.Restrict); //Foranea Tipos de bodega
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.UndMed2).WithMany().HasForeignKey(dat => dat.UndMed_Kg).OnDelete(DeleteBehavior.Restrict); //Foranea Unidad Medida
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.Usua).WithMany().HasForeignKey(dat => dat.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BOPP>().HasOne(datB => datB.boppGenerico).WithMany().HasForeignKey(dat => dat.BoppGen_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Asignacion_BOPP
            //modelBuilder.Entity<Asignacion_BOPP>().ToTable(tb => tb.HasTrigger("Auditoria_Asignaciones_BOPP"));
            modelBuilder.Entity<Asignacion_BOPP>().HasOne(asgb => asgb.Estado).WithMany().HasForeignKey(asig => asig.Estado_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<Asignacion_BOPP>().HasOne(asgb => asgb.Usua).WithMany().HasForeignKey(asig => asig.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario

            //Relaciones DetalleAsignacion_BOPP
            modelBuilder.Entity<DetalleAsignacion_BOPP>().ToTable(tb => tb.HasTrigger("SumarCantAsignada_EPOT"));
            modelBuilder.Entity<DetalleAsignacion_BOPP>().ToTable(tb => tb.HasTrigger("TR_EstadosOTDetAsigBOPP"));
            modelBuilder.Entity<DetalleAsignacion_BOPP>().ToTable(tb => tb.HasTrigger("TR_IU_ActualizaEstados_EnEstadosProcesosOT2"));
            //modelBuilder.Entity<DetalleAsignacion_BOPP>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesAsignaciones_BOPP"));
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
            modelBuilder.Entity<Orden_Trabajo>().ToTable(tb => tb.HasTrigger("TR_CrearOTs"));
            modelBuilder.Entity<Orden_Trabajo>().ToTable(tb => tb.HasTrigger("Auditoria_Orden_Trabajo"));
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.SedeCli).WithMany().HasForeignKey(ot => ot.SedeCli_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Prod).WithMany().HasForeignKey(ot => ot.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Unidad_Medida).WithMany().HasForeignKey(ot => ot.UndMed_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Estado).WithMany().HasForeignKey(ot => ot.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Usuario).WithMany().HasForeignKey(ot => ot.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.PedidoExterno).WithMany().HasForeignKey(ot => ot.PedExt_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Trabajo>().HasOne(ot => ot.Mezcla).WithMany().HasForeignKey(ot => ot.Mezcla_Id).OnDelete(DeleteBehavior.Restrict);

            //OT_Extrusion
            modelBuilder.Entity<OT_Extrusion>().ToTable(tb => tb.HasTrigger("TR_ActualizarCampos_OTExtrusion"));
            modelBuilder.Entity<OT_Extrusion>().ToTable(tb => tb.HasTrigger("Auditoria_OT_Extrusion"));
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Orden_Trabajo).WithMany().HasForeignKey(ot_ext => ot_ext.Ot_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Material_MatPrima).WithMany().HasForeignKey(ot_ext => ot_ext.Material_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Formato).WithMany().HasForeignKey(ot_ext => ot_ext.Formato_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Pigmento).WithMany().HasForeignKey(ot_ext => ot_ext.Pigmt_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Tratado).WithMany().HasForeignKey(ot_ext => ot_ext.Tratado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Extrusion>().HasOne(ot_ext => ot_ext.Unidad_Medida).WithMany().HasForeignKey(ot_ext => ot_ext.UndMed_Id).OnDelete(DeleteBehavior.Restrict);

            //OT_Impresión
            modelBuilder.Entity<OT_Impresion>().ToTable(tb => tb.HasTrigger("TR_ActualizarCampos_OTImpresion"));
            modelBuilder.Entity<OT_Impresion>().ToTable(tb => tb.HasTrigger("Auditoria_OT_Impresion"));
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Orden_Trabajo).WithMany().HasForeignKey(ot_imp => ot_imp.Ot_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tipos_Impresion).WithMany().HasForeignKey(ot_imp => ot_imp.TpImpresion_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta1).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta1_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta2).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta2_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta3).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta3_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta4).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta4_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta5).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta5_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta6).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta6_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta7).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta7_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Impresion>().HasOne(ot_imp => ot_imp.Tinta8).WithMany().HasForeignKey(ot_imp => ot_imp.Tinta8_Id).OnDelete(DeleteBehavior.Restrict);

            //OT_Laminado
            modelBuilder.Entity<OT_Laminado>().ToTable(tb => tb.HasTrigger("TR_ActualizarCampos_OTLaminado"));
            modelBuilder.Entity<OT_Laminado>().ToTable(tb => tb.HasTrigger("Auditoria_OT_Laminado"));
            modelBuilder.Entity<OT_Laminado>().HasOne(ot_lam => ot_lam.Orden_Trabajo).WithMany().HasForeignKey(ot_lam => ot_lam.OT_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Laminado>().HasOne(ot_lam => ot_lam.Laminado_Capa).WithMany().HasForeignKey(ot_lam => ot_lam.Capa_Id1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Laminado>().HasOne(ot_lam => ot_lam.Laminado_Capa2).WithMany().HasForeignKey(ot_lam => ot_lam.Capa_Id2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Laminado>().HasOne(ot_lam => ot_lam.Laminado_Capa3).WithMany().HasForeignKey(ot_lam => ot_lam.Capa_Id3).OnDelete(DeleteBehavior.Restrict);

            //OT_Sellado_Corte
            modelBuilder.Entity<OT_Sellado_Corte>().ToTable(tb => tb.HasTrigger("TR_ActualizarCampos_ProductoTerminado"));
            modelBuilder.Entity<OT_Sellado_Corte>().ToTable(tb => tb.HasTrigger("Auditoria_OT_Sellado_Corte"));
            modelBuilder.Entity<OT_Sellado_Corte>().HasOne(ot_SelCor => ot_SelCor.Orden_Trabajo).WithMany().HasForeignKey(ot_SelCor => ot_SelCor.Ot_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Sellado_Corte>().HasOne(ot_SelCor => ot_SelCor.Formato).WithMany().HasForeignKey(ot_SelCor => ot_SelCor.Formato_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OT_Sellado_Corte>().HasOne(ot_SelCor => ot_SelCor.TipoSellado).WithMany().HasForeignKey(ot_SelCor => ot_SelCor.TpSellado_Id).OnDelete(DeleteBehavior.Restrict);

            //Mezclas OT
            modelBuilder.Entity<Mezcla>().ToTable(tb => tb.HasTrigger("Crear_Mezclas"));
            modelBuilder.Entity<Mezcla>().ToTable(tb => tb.HasTrigger("Auditoria_Mezclas"));
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
            /** Triggers tablas Mezclas*/
            //modelBuilder.Entity<Mezcla_Pigmento>().ToTable(tb => tb.HasTrigger("Auditoria_Mezclas_Pigmentos"));
            //modelBuilder.Entity<Mezcla_Material>().ToTable(tb => tb.HasTrigger("Auditoria_Mezclas_Materiales"));
            modelBuilder.Entity<Mezcla_Material>().ToTable(tb => tb.HasTrigger("Crear_Mezclas_Materiales"));
            modelBuilder.Entity<Mezcla_Pigmento>().ToTable(tb => tb.HasTrigger("Crear_Mezcla_Pigmento"));

            //Relaciones EstadosProcesos_OT
            modelBuilder.Entity<Estados_ProcesosOT>().ToTable(tb => tb.HasTrigger("Auditoria_Estados_ProcesosOT"));
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.UnidadMedida).WithMany().HasForeignKey(eOT => eOT.UndMed_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.FallaTecnica).WithMany().HasForeignKey(eOT => eOT.Falla_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.Estado_OT).WithMany().HasForeignKey(eOT => eOT.Estado_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.Usuario).WithMany().HasForeignKey(eOT => eOT.Usua_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.Producto).WithMany().HasForeignKey(eOT => eOT.Prod_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Estados_ProcesosOT>().HasOne(eOT => eOT.Clientes).WithMany().HasForeignKey(eOT => eOT.Cli_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Fallas_Tecnicas
            //modelBuilder.Entity<Falla_Tecnica>().ToTable(tb => tb.HasTrigger("Auditoria_Fallas_Tecnicas"));
            modelBuilder.Entity<Falla_Tecnica>().HasOne(eOT => eOT.TipoFallaTecnica).WithMany().HasForeignKey(eOT => eOT.TipoFalla_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Entrada de Tintas
            //modelBuilder.Entity<Entrada_Tintas>().ToTable(tb => tb.HasTrigger("Auditoria_Entradas_Tintas"));
            modelBuilder.Entity<Entrada_Tintas>().HasOne(et => et.Usua).WithMany().HasForeignKey(et => et.Usua_Id).OnDelete(DeleteBehavior.Restrict);

            //Relaciones Detalles Entrada de Tintas
            //modelBuilder.Entity<Detalles_EntradaTintas>().ToTable(tb => tb.HasTrigger("Auditoria_Detalles_EntradaTintas"));
            modelBuilder.Entity<Detalles_EntradaTintas>().HasOne(dtET => dtET.Entrada_Tinta).WithMany().HasForeignKey(dtET => dtET.EntTinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_EntradaTintas>().HasOne(dtET => dtET.Tintas).WithMany().HasForeignKey(dtET => dtET.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_EntradaTintas>().HasOne(dtET => dtET.UndMed).WithMany().HasForeignKey(dtET => dtET.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Entrada Rollos Productos
            //modelBuilder.Entity<EntradaRollo_Producto>().ToTable(tb => tb.HasTrigger("Auditoria_EntradasRollos_Productos"));
            modelBuilder.Entity<EntradaRollo_Producto>().HasOne(erp => erp.Usua).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Detalles Entradas Rollos Productos
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().ToTable(tb => tb.HasTrigger("TR_ActualizarCantIngresada"));
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().ToTable(tb => tb.HasTrigger("TR_ActualizarCantIngresada_Facturada"));
            // modelBuilder.Entity<DetalleEntradaRollo_Producto>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesEntradasRollos_Productos"));
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.Estado).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.UndMedida_Rollo).WithMany().HasForeignKey(erp => erp.UndMed_Rollo).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.EntRollo_Producto).WithMany().HasForeignKey(erp => erp.EntRolloProd_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.Prod).WithMany().HasForeignKey(erp => erp.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.UndMedida_Prod).WithMany().HasForeignKey(erp => erp.UndMed_Prod).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleEntradaRollo_Producto>().HasOne(erp => erp.Proceso).WithMany().HasForeignKey(erp => erp.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Asignacion Productos a facturas ventas
            //modelBuilder.Entity<AsignacionProducto_FacturaVenta>().ToTable(tb => tb.HasTrigger("Auditoria_AsignacionesProductos_FacturasVentas"));
            modelBuilder.Entity<AsignacionProducto_FacturaVenta>().HasOne(erp => erp.Usua).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<AsignacionProducto_FacturaVenta>().HasOne(erp => erp.Cliente).WithMany().HasForeignKey(erp => erp.Cli_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<AsignacionProducto_FacturaVenta>().HasOne(erp => erp.Usuario).WithMany().HasForeignKey(erp => erp.Usua_Conductor).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Detalles Asignacion Productos a facturas ventas
            //modelBuilder.Entity<DetallesAsignacionProducto_FacturaVenta>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesAsignacionesProductos_FacturasVentas"));
            modelBuilder.Entity<DetallesAsignacionProducto_FacturaVenta>().HasOne<AsignacionProducto_FacturaVenta>(dapfv => dapfv.AsigProducto_FV).WithMany(remmp => remmp.DtAsigProd_FVTA).HasForeignKey(dapfv => dapfv.AsigProdFV_Id);
            modelBuilder.Entity<DetallesAsignacionProducto_FacturaVenta>().HasOne<Producto>(dapfv => dapfv.Prod).WithMany(remmp => remmp.DtAsigProd_FVTA).HasForeignKey(dapfv => dapfv.Prod_Id);
            modelBuilder.Entity<DetallesAsignacionProducto_FacturaVenta>().HasOne(erp => erp.UndMedida).WithMany().HasForeignKey(erp => erp.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Devolucion_ProductoFacturado
            //modelBuilder.Entity<Devolucion_ProductoFacturado>().ToTable(tb => tb.HasTrigger("Auditoria_Devolucion_ProductoFacturado"));
            modelBuilder.Entity<Devolucion_ProductoFacturado>().HasOne(erp => erp.Cliente).WithMany().HasForeignKey(erp => erp.Cli_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Devolucion_ProductoFacturado>().HasOne(erp => erp.TipoDevolucionPF).WithMany().HasForeignKey(erp => erp.TipoDevProdFact_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Devolucion_ProductoFacturado>().HasOne(x => x.Usua).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones DetalleDevolucion_ProductoFacturado
            //modelBuilder.Entity<DetalleDevolucion_ProductoFacturado>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesDevoluciones_ProductosFacturados"));
            modelBuilder.Entity<DetalleDevolucion_ProductoFacturado>().HasOne<Devolucion_ProductoFacturado>(dapfv => dapfv.DevolucionProdFact).WithMany(remmp => remmp.DtDevProd_Fact).HasForeignKey(dapfv => dapfv.DevProdFact_Id);
            modelBuilder.Entity<DetalleDevolucion_ProductoFacturado>().HasOne<Producto>(dapfv => dapfv.Prod).WithMany(remmp => remmp.DtDevProd_Fact).HasForeignKey(dapfv => dapfv.Prod_Id);
            modelBuilder.Entity<DetalleDevolucion_ProductoFacturado>().HasOne(erp => erp.UndMedida).WithMany().HasForeignKey(erp => erp.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones PreEntrega_RolloDespacho
            //modelBuilder.Entity<PreEntrega_RolloDespacho>().ToTable(tb => tb.HasTrigger("Auditoria_PreEntrega_RollosDespacho"));
            modelBuilder.Entity<PreEntrega_RolloDespacho>().HasOne(erp => erp.Usuario).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones PreEntrega_RolloDespacho
            //modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesPreEntrega_RollosDespacho"));
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.PreEntregaRollo).WithMany().HasForeignKey(erp => erp.PreEntRollo_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.Proceso).WithMany().HasForeignKey(erp => erp.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.UndMedidaRollo).WithMany().HasForeignKey(erp => erp.UndMed_Rollo).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.Cliente).WithMany().HasForeignKey(erp => erp.Cli_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.Prod).WithMany().HasForeignKey(erp => erp.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePreEntrega_RolloDespacho>().HasOne(erp => erp.UndMedidaProducto).WithMany().HasForeignKey(erp => erp.UndMed_Producto).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones IngresoRollos_Extrusion
            //modelBuilder.Entity<IngresoRollos_Extrusion>().ToTable(tb => tb.HasTrigger("Auditoria_IngresoRollos_Extrusion"));
            modelBuilder.Entity<IngresoRollos_Extrusion>().HasOne(x => x.Usua).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones DetallesIngRollos_Extrusion
            //modelBuilder.Entity<DetallesIngRollos_Extrusion>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesIngRollos_Extrusion"));
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.IngresoRollos_Extrusion).WithMany().HasForeignKey(x => x.IngRollo_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.Unidad_Medida).WithMany().HasForeignKey(x => x.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.Proceso).WithMany().HasForeignKey(x => x.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesIngRollos_Extrusion>().HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones AsignacionRollos_Extrusion
            //modelBuilder.Entity<AsignacionRollos_Extrusion>().ToTable(tb => tb.HasTrigger("Auditoria_AsignacionRollos_Extrusion"));
            modelBuilder.Entity<AsignacionRollos_Extrusion>().HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Reliaciones DetallesAsigRollos_Extrusion
            //modelBuilder.Entity<DetallesAsgRollos_Extrusion>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesAsgRollos_Extrusion"));
            modelBuilder.Entity<DetallesAsgRollos_Extrusion>().HasOne(x => x.AsignacionRollos).WithMany().HasForeignKey(x => x.AsgRollos_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesAsgRollos_Extrusion>().HasOne(x => x.Unidad_Medida).WithMany().HasForeignKey(x => x.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesAsgRollos_Extrusion>().HasOne(x => x.Proceso).WithMany().HasForeignKey(x => x.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallesAsgRollos_Extrusion>().HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relaciones Rollo_Desecho
            modelBuilder.Entity<Rollo_Desecho>().ToTable(tb => tb.HasTrigger("Auditoria_Rollos_Desechos"));
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Prod).WithMany().HasForeignKey(erp => erp.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Producto
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Proceso).WithMany().HasForeignKey(erp => erp.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.UndMedida).WithMany().HasForeignKey(erp => erp.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Unidad_Medida
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Turno).WithMany().HasForeignKey(erp => erp.Turno_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Turno
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Estado).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Estado
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Material).WithMany().HasForeignKey(erp => erp.Material_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Material 
            modelBuilder.Entity<Rollo_Desecho>().HasOne(erp => erp.Cono).WithMany().HasForeignKey(erp => erp.Cono_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Cono

            //Relaciones Orden_Compra
            modelBuilder.Entity<Orden_Compra>().ToTable(tb => tb.HasTrigger("Auditoria_Ordenes_Compras"));
            modelBuilder.Entity<Orden_Compra>().HasOne(erp => erp.Proveedor).WithMany().HasForeignKey(erp => erp.Prov_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Producto
            modelBuilder.Entity<Orden_Compra>().HasOne(erp => erp.TipoDoc).WithMany().HasForeignKey(erp => erp.TpDoc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Orden_Compra>().HasOne(erp => erp.Estado).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Orden_Compra>().HasOne(erp => erp.Usua).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso

            //Relaciones Detalle_OrdenCompra
            modelBuilder.Entity<Detalle_OrdenCompra>().ToTable(tb => tb.HasTrigger("Auditoria_Detalles_OrdenesCompras"));
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.Orden_Compra).WithMany().HasForeignKey(erp => erp.Oc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Producto
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.MatPrima).WithMany().HasForeignKey(erp => erp.MatPri_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.Tinta).WithMany().HasForeignKey(erp => erp.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.BOPP).WithMany().HasForeignKey(erp => erp.BOPP_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso
            modelBuilder.Entity<Detalle_OrdenCompra>().HasOne(erp => erp.UndMed).WithMany().HasForeignKey(erp => erp.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict); //Proceso

            //Ordenes Compras Facturas Compras
            modelBuilder.Entity<OrdenesCompras_FacturasCompras>().ToTable(tb => tb.HasTrigger("Auditoria_OrdenesCompras_FacturasCompras"));
            modelBuilder.Entity<OrdenesCompras_FacturasCompras>().HasOne(erp => erp.Orden_Compra).WithMany().HasForeignKey(erp => erp.Oc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<OrdenesCompras_FacturasCompras>().HasOne(erp => erp.Facco).WithMany().HasForeignKey(erp => erp.Facco_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Remision Ordenes de Compras
            modelBuilder.Entity<Remision_OrdenCompra>().ToTable(tb => tb.HasTrigger("Auditoria_Remision_OrdenCompra"));
            modelBuilder.Entity<Remision_OrdenCompra>().HasOne(erp => erp.Orden_Compra).WithMany().HasForeignKey(erp => erp.Oc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Remision_OrdenCompra>().HasOne(erp => erp.Remision).WithMany().HasForeignKey(erp => erp.Rem_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Vistas Favoritas
            modelBuilder.Entity<VistasFavoritas>().ToTable(tb => tb.HasTrigger("Auditoria_VistasFavoritas"));
            modelBuilder.Entity<VistasFavoritas>().HasOne(erp => erp.Usuario).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Activos
            //modelBuilder.Entity<Activo>().ToTable(tb => tb.HasTrigger("Auditoria_Activos"));
            modelBuilder.Entity<Activo>().HasOne(erp => erp.Estados).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Activo>().HasOne(erp => erp.Tp_Activo).WithMany().HasForeignKey(erp => erp.TpActv_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Activo>().HasOne(erp => erp.Area).WithMany().HasForeignKey(erp => erp.Area_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Mantenimientos
            modelBuilder.Entity<Mantenimiento>().ToTable(tb => tb.HasTrigger("ActualizarEstado_PedidoMtto"));
            //modelBuilder.Entity<Mantenimiento>().ToTable(tb => tb.HasTrigger("Auditoria_Mantenimientos"));
            modelBuilder.Entity<Mantenimiento>().HasOne(erp => erp.Estado).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Mantenimiento>().HasOne(erp => erp.Proveedor).WithMany().HasForeignKey(erp => erp.Prov_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Mantenimiento>().HasOne(erp => erp.Usu).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Mantenimiento>().HasOne(erp => erp.Pedido_Mtto).WithMany().HasForeignKey(erp => erp.PedMtto_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Detalles_Mtto
            modelBuilder.Entity<Detalle_Mantenimiento>().ToTable(tb => tb.HasTrigger("ActualizarMantenimiento_Pedido"));
            //modelBuilder.Entity<Detalle_Mantenimiento>().ToTable(tb => tb.HasTrigger("Auditoria_Detalles_Mantenimientos"));
            modelBuilder.Entity<Detalle_Mantenimiento>().HasOne(erp => erp.Mttos).WithMany().HasForeignKey(erp => erp.Mtto_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalle_Mantenimiento>().HasOne(erp => erp.Tipo_Mtto).WithMany().HasForeignKey(erp => erp.TpMtto_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalle_Mantenimiento>().HasOne(erp => erp.Act).WithMany().HasForeignKey(erp => erp.Actv_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalle_Mantenimiento>().HasOne(erp => erp.Estados).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Pedido_Mtto
            modelBuilder.Entity<Pedido_Mantenimiento>().ToTable(tb => tb.HasTrigger("Auditoria_Pedidos_Mantenimientos"));
            modelBuilder.Entity<Pedido_Mantenimiento>().HasOne(erp => erp.Usuario).WithMany().HasForeignKey(erp => erp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Pedido_Mantenimiento>().HasOne(erp => erp.Estado).WithMany().HasForeignKey(erp => erp.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //DetallePedido_Mtto
             modelBuilder.Entity<DetallePedido_Mantenimiento>().ToTable(tb => tb.HasTrigger("Auditoria_DetallesPedidos_Mantenimientos"));
            modelBuilder.Entity<DetallePedido_Mantenimiento>().HasOne(erp => erp.PedidoMtto).WithMany().HasForeignKey(erp => erp.PedMtto_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePedido_Mantenimiento>().HasOne(erp => erp.Tipo_Mtto).WithMany().HasForeignKey(erp => erp.TpMtto_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetallePedido_Mantenimiento>().HasOne(erp => erp.Act).WithMany().HasForeignKey(erp => erp.Actv_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Movimientos Aplicacion
            //modelBuilder.Entity<MovimientosAplicacion>().ToTable(tb => tb.HasTrigger("Auditoria_MovimientosAplicacion"));
            modelBuilder.Entity<MovimientosAplicacion>().HasOne(mov => mov.Usuario).WithMany().HasForeignKey(mov => mov.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Desperdicio
            modelBuilder.Entity<Desperdicio>().ToTable(tb => tb.HasTrigger("Auditoria_Desperdicios"));
            modelBuilder.Entity<Desperdicio>().HasOne(desp => desp.Producto).WithMany().HasForeignKey(desp => desp.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Desperdicio>().HasOne(desp => desp.Material).WithMany().HasForeignKey(desp => desp.Material_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Desperdicio>().HasOne(desp => desp.Usuario1).WithMany().HasForeignKey(desp => desp.Usua_Operario).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Desperdicio>().HasOne(desp => desp.Usuario2).WithMany().HasForeignKey(desp => desp.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Desperdicio>().HasOne(desp => desp.Proceso).WithMany().HasForeignKey(desp => desp.Proceso_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Desperdicio>().HasOne(desp => desp.Falla).WithMany().HasForeignKey(desp => desp.Falla_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Desperdicio>().HasOne(desp => desp.Activo).WithMany().HasForeignKey(desp => desp.Actv_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Log Transacciones
            modelBuilder.Entity<Log_Transacciones>().HasOne(trn => trn.Usuario).WithMany().HasForeignKey(trn => trn.Transac_Usuario).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Inventario mensual de productos
            modelBuilder.Entity<Inventario_Mensual_Productos>().ToTable(tb => tb.HasTrigger("Auditoria_Inventario_Mensual_Productos"));
            modelBuilder.Entity<Inventario_Mensual_Productos>().HasOne(x => x.Und).WithMany().HasForeignKey(x => x.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Terceros
            modelBuilder.Entity<Terceros>().ToTable(tb => tb.HasTrigger("Auditoria_Terceros"));
            modelBuilder.Entity<Terceros>().HasOne(x => x.TipoIdentificacion).WithMany().HasForeignKey(x => x.TipoIdentificacion_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Orden de Maquila
            modelBuilder.Entity<Orden_Maquila>().ToTable(tb => tb.HasTrigger("Auditoria_Orden_Maquila"));
            modelBuilder.Entity<Orden_Maquila>().HasOne(x => x.Tercero).WithMany().HasForeignKey(x => x.Tercero_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Maquila>().HasOne(x => x.TipoDoc).WithMany().HasForeignKey(x => x.TpDoc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Maquila>().HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Orden_Maquila>().HasOne(x => x.Usua).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            // Detalles de la Orden de Maquila
            modelBuilder.Entity<Detalle_OrdenMaquila>().ToTable(tb => tb.HasTrigger("Auditoria_Detalle_OrdenMaquila"));
            modelBuilder.Entity<Detalle_OrdenMaquila>().HasOne(x => x.Orden_Maquila).WithMany().HasForeignKey(x => x.OM_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalle_OrdenMaquila>().HasOne(x => x.MatPrima).WithMany().HasForeignKey(x => x.MatPri_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalle_OrdenMaquila>().HasOne(x => x.Tinta).WithMany().HasForeignKey(x => x.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalle_OrdenMaquila>().HasOne(x => x.BOPP).WithMany().HasForeignKey(x => x.BOPP_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalle_OrdenMaquila>().HasOne(x => x.UndMed).WithMany().HasForeignKey(x => x.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Facturacion Orden de Maquila
            modelBuilder.Entity<Facturacion_OrdenMaquila>().ToTable(tb => tb.HasTrigger("Auditoria_Facturacion_OrdenMaquila"));
            modelBuilder.Entity<Facturacion_OrdenMaquila>().HasOne(x => x.Tercero).WithMany().HasForeignKey(x => x.Tercero_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Facturacion_OrdenMaquila>().HasOne(x => x.TipoDoc).WithMany().HasForeignKey(x => x.TpDoc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Facturacion_OrdenMaquila>().HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Facturacion_OrdenMaquila>().HasOne(x => x.Usua).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Detalle de la Facturacion de Orden de Maquila
            modelBuilder.Entity<DetalleFacturacion_OrdenMaquila>().ToTable(tb => tb.HasTrigger("Auditoria_DetalleFacturacion_OrdenMaquila"));
            modelBuilder.Entity<DetalleFacturacion_OrdenMaquila>().HasOne(x => x.FacOM).WithMany().HasForeignKey(x => x.FacOM_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleFacturacion_OrdenMaquila>().HasOne(x => x.MatPrima).WithMany().HasForeignKey(x => x.MatPri_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleFacturacion_OrdenMaquila>().HasOne(x => x.Tinta).WithMany().HasForeignKey(x => x.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleFacturacion_OrdenMaquila>().HasOne(x => x.BOPP).WithMany().HasForeignKey(x => x.Bopp_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<DetalleFacturacion_OrdenMaquila>().HasOne(x => x.UndMed).WithMany().HasForeignKey(x => x.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Relación de Orden de Maquila con Facturación
            modelBuilder.Entity<OrdenMaquila_Facturacion>().ToTable(tb => tb.HasTrigger("Auditoria_OrdenMaquila_Facturacion"));
            modelBuilder.Entity<OrdenMaquila_Facturacion>().HasOne(x => x.Orden_Maquila).WithMany().HasForeignKey(x => x.OM_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<OrdenMaquila_Facturacion>().HasOne(x => x.FacOM).WithMany().HasForeignKey(x => x.FacOM_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            // Tickets
            modelBuilder.Entity<Tickets>().ToTable(tb => tb.HasTrigger("Auditoria_Tickets"));
            modelBuilder.Entity<Tickets>().HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Tickets>().HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            // Tickets Revisados
            modelBuilder.Entity<Tickets_Revisados>().ToTable(tb => tb.HasTrigger("Auditoria_Tickets_Revisados"));
            modelBuilder.Entity<Tickets_Revisados>().HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Tickets_Revisados>().HasOne(x => x.Tickets).WithMany().HasForeignKey(x => x.Ticket_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Solicitud de Materia Prima
            modelBuilder.Entity<Solicitud_MateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Solicitud_MateriaPrima"));
            modelBuilder.Entity<Solicitud_MateriaPrima>().HasOne(x => x.Usuario).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Solicitud_MateriaPrima>().HasOne(x => x.Estado).WithMany().HasForeignKey(y => y.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            // Detalles de solicitud de materia prima
            modelBuilder.Entity<Detalles_SolicitudMateriaPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Detalles_SolicitudMateriaPrima"));
            modelBuilder.Entity<Detalles_SolicitudMateriaPrima>().HasOne(x => x.Solicitud_MateriaPrima).WithMany().HasForeignKey(y => y.Solicitud_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudMateriaPrima>().HasOne(x => x.Materia_Prima).WithMany().HasForeignKey(y => y.MatPri_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudMateriaPrima>().HasOne(x => x.Tinta).WithMany().HasForeignKey(y => y.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudMateriaPrima>().HasOne(x => x.Bopp).WithMany().HasForeignKey(y => y.Bopp_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudMateriaPrima>().HasOne(x => x.UndMed).WithMany().HasForeignKey(y => y.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudMateriaPrima>().HasOne(x => x.Estado).WithMany().HasForeignKey(y => y.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Solicitudes de Materia Prima y Orden de Compra
            modelBuilder.Entity<SolicitudesMP_OrdenesCompra>().ToTable(tb => tb.HasTrigger("Auditoria_SolicitudesMP_OrdenesCompra"));
            modelBuilder.Entity<SolicitudesMP_OrdenesCompra>().HasOne(x => x.Solicitud_MateriaPrima).WithMany().HasForeignKey(y => y.Solicitud_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<SolicitudesMP_OrdenesCompra>().HasOne(x => x.Orden_Compra).WithMany().HasForeignKey(y => y.Oc_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Eventos de Calendario
            modelBuilder.Entity<EventosCalendario>().ToTable(tb => tb.HasTrigger("Auditoria_EventosCalendario"));
            modelBuilder.Entity<EventosCalendario>().HasOne(x => x.Usuario).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Solicitudes_MatPrimaExtrusion
            modelBuilder.Entity<Solicitud_MatPrimaExtrusion>().ToTable(tb => tb.HasTrigger("Auditoria_Solicitud_MatPrimaExtrusion"));
            modelBuilder.Entity<Solicitud_MatPrimaExtrusion>().HasOne(asgmp => asgmp.Estado).WithMany().HasForeignKey(asigmp => asigmp.Estado_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<Solicitud_MatPrimaExtrusion>().HasOne(asgmp => asgmp.Usua).WithMany().HasForeignKey(asgmpr => asgmpr.Usua_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario
            modelBuilder.Entity<Solicitud_MatPrimaExtrusion>().HasOne(asgmp => asgmp.Proceso).WithMany().HasForeignKey(asigmp => asigmp.Proceso_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso

            //DetSolicitudes_MatPrimaExtrusion
            modelBuilder.Entity<DetSolicitud_MatPrimaExtrusion>().ToTable(tb => tb.HasTrigger("Auditoria_DetSolicitud_MatPrimaExtrusion"));
            modelBuilder.Entity<DetSolicitud_MatPrimaExtrusion>().HasOne(asgmp => asgmp.MatPrima).WithMany().HasForeignKey(asigmp => asigmp.MatPri_Id).OnDelete(DeleteBehavior.Restrict); //foranea estado
            modelBuilder.Entity<DetSolicitud_MatPrimaExtrusion>().HasOne(asgmp => asgmp.Tinta).WithMany().HasForeignKey(asgmpr => asgmpr.Tinta_Id).OnDelete(DeleteBehavior.Restrict); //foranea usuario
            modelBuilder.Entity<DetSolicitud_MatPrimaExtrusion>().HasOne(asgmp => asgmp.UndMed).WithMany().HasForeignKey(asigmp => asigmp.UndMed_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso
            modelBuilder.Entity<DetSolicitud_MatPrimaExtrusion>().HasOne(asgmp => asgmp.SolMatPriExt).WithMany().HasForeignKey(asigmp => asigmp.SolMpExt_Id).OnDelete(DeleteBehavior.Restrict); //foranea proceso

            //Solicitud de Rollos a Areas
            modelBuilder.Entity<Solicitud_Rollos_Areas>().ToTable(tb => tb.HasTrigger("Auditoria_Solicitud_Rollos_Areas"));
            modelBuilder.Entity<Solicitud_Rollos_Areas>().HasOne(x => x.Usuario).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Solicitud_Rollos_Areas>().HasOne(x => x.UsuarioRespuesta).WithMany().HasForeignKey(y => y.Usua_Respuesta).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Solicitud_Rollos_Areas>().HasOne(x => x.Estado).WithMany().HasForeignKey(y => y.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Solicitud_Rollos_Areas>().HasOne(x => x.Tipo_solicitud).WithMany().HasForeignKey(y => y.TpSol_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Detalles Solicitud de Rollos a Areas
            modelBuilder.Entity<Detalles_SolicitudRollos>().ToTable(tb => tb.HasTrigger("Auditoria_Detalles_SolicitudRollos"));
            modelBuilder.Entity<Detalles_SolicitudRollos>().HasOne(x => x.SolicitudRollos).WithMany().HasForeignKey(y => y.SolRollo_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudRollos>().HasOne(x => x.Bodega_Solicitante).WithMany().HasForeignKey(y => y.DtSolRollo_BodegaSolicitante).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudRollos>().HasOne(x => x.Bodega_Solicitada).WithMany().HasForeignKey(y => y.DtSolRollo_BodegaSolicitada).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudRollos>().HasOne(x => x.Producto).WithMany().HasForeignKey(y => y.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_SolicitudRollos>().HasOne(x => x.Und).WithMany().HasForeignKey(y => y.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Bodegas Rollo
            modelBuilder.Entity<Bodegas_Rollos>().ToTable(tb => tb.HasTrigger("Auditoria_Bodegas_Rollos"));

            //Detalles Bodegas Rollo
            modelBuilder.Entity<Detalles_BodegasRollos>().ToTable(tb => tb.HasTrigger("Auditoria_Detalles_BodegasRollos"));
            modelBuilder.Entity<Detalles_BodegasRollos>().HasOne(x => x.Bodegas_Rollos).WithMany().HasForeignKey(y => y.BgRollo_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_BodegasRollos>().HasOne(x => x.Und).WithMany().HasForeignKey(y => y.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_BodegasRollos>().HasOne(x => x.Producto).WithMany().HasForeignKey(y => y.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Detalles_BodegasRollos>().HasOne(x => x.Bodega_Actual).WithMany().HasForeignKey(y => y.BgRollo_BodegaActual).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Vistas Permisos
            modelBuilder.Entity<Vistas_Permisos>().ToTable(x => x.HasTrigger("Auditoria_Vistas_Permisos"));

            //Formato Documentos
            modelBuilder.Entity<Formato_Documentos>().ToTable(x => x.HasTrigger("Auditoria_Formato_Documentos"));

            //Facturas Invergoal Inversuez
            modelBuilder.Entity<Facturas_Invergoal_Inversuez>().ToTable(x => x.HasTrigger("Auditoria_Facturas_Invergoal_Inversuez"));
            modelBuilder.Entity<Facturas_Invergoal_Inversuez>().HasOne(x => x.Usuario).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Facturas_Invergoal_Inversuez>().HasOne(x => x.Proveedor).WithMany().HasForeignKey(y => y.Nit_Proveedor).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Facturas_Invergoal_Inversuez>().HasOne(x => x.Estados).WithMany().HasForeignKey(y => y.Estado_Factura).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Costos Empresas Años
            modelBuilder.Entity<Costos_Empresas_Anios>().ToTable(x => x.HasTrigger("Auditoria_Costos_Empresas_Anios"));

            //Tipos de Nomina
            modelBuilder.Entity<Tipos_Nomina>().ToTable(x => x.HasTrigger("Auditoria_Tipos_Nomina"));

            //Nomina Plasticaribe
            modelBuilder.Entity<Nomina_Plasticaribe>().ToTable(x => x.HasTrigger("Auditoria_Nomina_Plasticaribe"));
            modelBuilder.Entity<Nomina_Plasticaribe>().ToTable(x => x.HasTrigger("Costos_Nomina_Plasticaribe"));
            modelBuilder.Entity<Nomina_Plasticaribe>().HasOne(x => x.Usuario).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Nomina_Plasticaribe>().HasOne(x => x.Tipos_Nomina).WithMany().HasForeignKey(y => y.TpNomina_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            // Certificados de Calidad
            modelBuilder.Entity<Certificados_Calidad>().ToTable(x => x.HasTrigger("Auditoria_Certificados_Calidad"));
            modelBuilder.Entity<Certificados_Calidad>().HasOne(x => x.Producto).WithMany().HasForeignKey(y => y.Item).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Certificados_Calidad>().HasOne(x => x.Und).WithMany().HasForeignKey(y => y.Presentacion_Producto).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Certificados_Calidad>().HasOne(x => x.Und_Calibre).WithMany().HasForeignKey(y => y.Unidad_Calibre).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Certificados_Calidad>().HasOne(x => x.Und_AnchoFrente).WithMany().HasForeignKey(y => y.Unidad_AnchoFrente).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Certificados_Calidad>().HasOne(x => x.Und_AnchoFuelle).WithMany().HasForeignKey(y => y.Unidad_AnchoFuelle).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Certificados_Calidad>().HasOne(x => x.Und_LargoRepeticion).WithMany().HasForeignKey(y => y.Unidad_LargoRepeticion).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Certificados_Calidad>().HasOne(x => x.Und_Cof).WithMany().HasForeignKey(y => y.Unidad_Cof).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Certificados_Calidad>().HasOne(x => x.Usuario).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);


            // Control de Calidad de Doblado y Corte
            modelBuilder.Entity<ControlCalidad_CorteDoblado>().ToTable(x => x.HasTrigger("Auditoria_ControlCalidad_CorteDoblado"));
            modelBuilder.Entity<ControlCalidad_CorteDoblado>().HasOne(x => x.Usuario).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior : DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_CorteDoblado>().HasOne(x => x.Turno).WithMany().HasForeignKey(y => y.Turno_Id).OnDelete(deleteBehavior : DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_CorteDoblado>().HasOne(x => x.Producto).WithMany().HasForeignKey(y => y.Prod_Id).OnDelete(deleteBehavior : DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_CorteDoblado>().HasOne(x => x.Und).WithMany().HasForeignKey(y => y.UndMed_Id).OnDelete(deleteBehavior : DeleteBehavior.Restrict);

            // Control de Calidad de Impresión
            modelBuilder.Entity<ControlCalidad_Impresion>().ToTable(x => x.HasTrigger("Auditoria_ControlCalidad_Impresion"));
            modelBuilder.Entity<ControlCalidad_Impresion>().HasOne(x => x.Usuario).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Impresion>().HasOne(x => x.Turno).WithMany().HasForeignKey(y => y.Turno_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Impresion>().HasOne(x => x.Producto).WithMany().HasForeignKey(y => y.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //ControlCalidad_Extrusion
            modelBuilder.Entity<ControlCalidad_Impresion>().ToTable(x => x.HasTrigger("Auditoria_ControlCalidad_Extrusion"));
            modelBuilder.Entity<ControlCalidad_Extrusion>().HasOne(x => x.Turnos).WithMany().HasForeignKey(y => y.Turno_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Extrusion>().HasOne(x => x.Usu).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Extrusion>().HasOne(x => x.Producto).WithMany().HasForeignKey(y => y.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Extrusion>().HasOne(x => x.Pigmento).WithMany().HasForeignKey(y => y.Pigmento_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Extrusion>().HasOne(x => x.UndMedida).WithMany().HasForeignKey(y => y.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //ControlCalidad_Sellado
            modelBuilder.Entity<ControlCalidad_Impresion>().ToTable(x => x.HasTrigger("Auditoria_ControlCalidad_Sellado"));
            modelBuilder.Entity<ControlCalidad_Sellado>().HasOne(x => x.Turnos).WithMany().HasForeignKey(y => y.Turno_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Sellado>().HasOne(x => x.Usu).WithMany().HasForeignKey(y => y.Usua_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Sellado>().HasOne(x => x.Producto).WithMany().HasForeignKey(y => y.Prod_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Sellado>().HasOne(x => x.UndMedida1).WithMany().HasForeignKey(y => y.UndMed_AL).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlCalidad_Sellado>().HasOne(x => x.UndMedida2).WithMany().HasForeignKey(y => y.UndMed_AF).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Movimientros_Entradas_MP
            modelBuilder.Entity<Movimientros_Entradas_MP>().ToTable(x => x.HasTrigger("Auditoria_Movimientros_Entradas_MP"));
            modelBuilder.Entity<Movimientros_Entradas_MP>().HasOne(x => x.Materia_Prima).WithMany().HasForeignKey(y => y.MatPri_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Movimientros_Entradas_MP>().HasOne(x => x.Tinta).WithMany().HasForeignKey(y => y.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Movimientros_Entradas_MP>().HasOne(x => x.Bopp).WithMany().HasForeignKey(y => y.Bopp_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Movimientros_Entradas_MP>().HasOne(x => x.Unidad_Medida).WithMany().HasForeignKey(y => y.UndMed_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Movimientros_Entradas_MP>().HasOne(x => x.Tipo_Documento).WithMany().HasForeignKey(y => y.Tipo_Entrada).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Movimientros_Entradas_MP>().HasOne(x => x.Estado).WithMany().HasForeignKey(y => y.Estado_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            //Entradas_Salidas_MP
            modelBuilder.Entity<Entradas_Salidas_MP>().ToTable(x => x.HasTrigger("Auditoria_Entradas_Salidas_MP"));
            modelBuilder.Entity<Entradas_Salidas_MP>().HasOne(x => x.Materia_Prima).WithMany().HasForeignKey(y => y.MatPri_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Entradas_Salidas_MP>().HasOne(x => x.Tinta).WithMany().HasForeignKey(y => y.Tinta_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Entradas_Salidas_MP>().HasOne(x => x.Bopp).WithMany().HasForeignKey(y => y.Bopp_Id).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Entradas_Salidas_MP>().HasOne(x => x.Movimientros).WithMany().HasForeignKey(y => y.Id_Entrada).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Entradas_Salidas_MP>().HasOne(x => x.Documento).WithMany().HasForeignKey(y => y.Tipo_Salida).OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            modelBuilder.Entity<Entradas_Salidas_MP>().HasOne(x => x.Tipo_Documento).WithMany().HasForeignKey(y => y.Tipo_Entrada).OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            modelBuilder.Entity<Area>().ToTable(tb => tb.HasTrigger("Auditoria_Areas"));
            modelBuilder.Entity<Rol_Usuario>().ToTable(tb => tb.HasTrigger("Auditoria_Roles_Usuarios"));
            modelBuilder.Entity<Tipo_Usuario>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Usuarios"));
            modelBuilder.Entity<Archivos>().ToTable(tb => tb.HasTrigger("Auditoria_Archivos"));
            modelBuilder.Entity<Tipo_Solicitud_Rollos_Areas>().ToTable(tb => tb.HasTrigger("Auditoria_Tipo_Solicitud_Rollos_Areas"));

            /*modelBuilder.Entity<Log_Errores>().ToTable(tb => tb.HasTrigger("Auditoria_Log_Errores"));
            modelBuilder.Entity<Tipos_Sellados>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Sellados"));
            modelBuilder.Entity<MovimientosAplicacion>().ToTable(tb => tb.HasTrigger("Auditoria_MovimientosAplicacion"));
            modelBuilder.Entity<Tipo_Moneda>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Monedas"));
            modelBuilder.Entity<Turno>().ToTable(tb => tb.HasTrigger("Auditoria_Turnos"));
            modelBuilder.Entity<Tipo_Estado>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Estados"));
            modelBuilder.Entity<Tipo_Proveedor>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Proveedores"));
            modelBuilder.Entity<Tipo_Recuperado>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Recuperados"));
            modelBuilder.Entity<Tipo_Producto>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Productos"));
            modelBuilder.Entity<TipoDevolucion_ProductoFacturado>().ToTable(tb => tb.HasTrigger("Auditoria_TipoDevoluciones_ProductosFacturados"));
            modelBuilder.Entity<Tipo_Mantenimiento>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Mantenimientos"));
            modelBuilder.Entity<Tipos_Impresion>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Impresion"));
            modelBuilder.Entity<Tipo_FallaTecnica>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_FallasTecnicas"));
            modelBuilder.Entity<TiposClientes>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Clientes"));
            modelBuilder.Entity<Tipo_Activo>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Activos"));
            modelBuilder.Entity<Tipo_Documento>().ToTable(tb => tb.HasTrigger("Auditoria_Tipos_Documentos"));
            modelBuilder.Entity<TipoIdentificacion>().ToTable(tb => tb.HasTrigger("Auditoria_TipoIdentificaciones"));
            modelBuilder.Entity<Rodillos>().ToTable(tb => tb.HasTrigger("Auditoria_Rodillos"));
            modelBuilder.Entity<Proceso>().ToTable(tb => tb.HasTrigger("Auditoria_Procesos"));
            modelBuilder.Entity<Pistas>().ToTable(tb => tb.HasTrigger("Auditoria_Pistas"));
            modelBuilder.Entity<Pigmento>().ToTable(tb => tb.HasTrigger("Auditoria_Pigmentos"));
            modelBuilder.Entity<Material_MatPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Materiales_MatPrima"));
            modelBuilder.Entity<Laminado_Capa>().ToTable(tb => tb.HasTrigger("Auditoria_Laminado_Capa"));
            modelBuilder.Entity<Insumo>().ToTable(tb => tb.HasTrigger("Auditoria_Insumos"));
            modelBuilder.Entity<Formato>().ToTable(tb => tb.HasTrigger("Auditoria_Formato"));
            modelBuilder.Entity<Categoria_Insumo>().ToTable(tb => tb.HasTrigger("Auditoria_Categorias_Insumos"));
            modelBuilder.Entity<Categoria_MatPrima>().ToTable(tb => tb.HasTrigger("Auditoria_Categorias_MatPrima"));
            modelBuilder.Entity<Categorias_Archivos>().ToTable(tb => tb.HasTrigger("Auditoria_Categorias_Archivos"));
            modelBuilder.Entity<Bopp_Generico>().ToTable(tb => tb.HasTrigger("Auditoria_Bopp_Generico"));
            modelBuilder.Entity<Cliente_Producto>().ToTable(tb => tb.HasTrigger("Auditoria_Clientes_Productos"));
            modelBuilder.Entity<Unidad_Medida>().ToTable(tb => tb.HasTrigger("Auditoria_Unidades_Medidas"));
            modelBuilder.Entity<Tratado>().ToTable(tb => tb.HasTrigger("Auditoria_Tratado"));
            modelBuilder.Entity<Cono>().ToTable(tb => tb.HasTrigger("Auditoria_Conos"));*/
        }
    }
}
