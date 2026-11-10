using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Crea y edita de forma atómica una orden de trabajo con la información de extrusión, impresión, laminado y sellado/corte.
    /// <para>
    /// REGLA F2-15 (triggers de una sola fila): TR_CrearOTs, TR_ActualizarCampos_* y los triggers de BagPro leen
    /// <c>inserted</c> como si tuviera una sola fila. Sobre Orden_Trabajo y las tablas OT_* está PROHIBIDO usar
    /// ExecuteUpdate/ExecuteDelete, UPDATE o DELETE masivos, o AddRange de varias OT en un mismo SaveChanges.
    /// Cualquier cambio de estado o anulación se hace OT por OT.
    /// </para>
    /// <para>
    /// BagPro y Zeus se consultan por sinónimos de la BD (<c>dbo.bp_ClientesOT</c>, <c>dbo.bp_ClientesOT_Item</c>,
    /// <c>dbo.zeus_Articulo</c>), los mismos que usa TR_CrearOTs (scripts en PlasticaribeDB, Cambios/2026-11-07-ot-f2-creacion).
    /// </para>
    /// </summary>
    public sealed class OrdenTrabajoCompletaService
    {
        /// <summary>Recurso del bloqueo de aplicación que serializa la asignación de números de OT.</summary>
        public const string RecursoBloqueoNumero = "Plasticaribe_NumeroOT";

        /// <summary>Clave de configuración que activa la comprobación posterior de la réplica en BagPro (F2-14).</summary>
        public const string ClaveValidarReplicacion = "OrdenTrabajo:ValidarReplicacionBagPro";

        private const int TiempoEsperaBloqueoMs = 10000;

        private readonly dataContext _context;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa el servicio con el contexto de datos y la configuración.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        /// <param name="configuration">Configuración de la aplicación (interruptor de F2-14).</param>
        public OrdenTrabajoCompletaService(dataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Crea la OT y sus cuatro procesos en una sola transacción (F2-01, F2-02, F2-13, F2-14):
        /// valida los datos y los costos, comprueba que el item exista en BagPro y el artículo en Zeus, toma el bloqueo
        /// <see cref="RecursoBloqueoNumero"/>, calcula el número como MAX(BagPro, Plasticaribe) + 1, inserta y, si está activo
        /// <see cref="ClaveValidarReplicacion"/>, comprueba que el trigger haya creado la OT en BagPro. Si algo falla no queda nada guardado.
        /// Si se envía otro nombre de producto (referencia), lo cambia en el maestro antes de insertar la OT (el trigger lo lleva
        /// a BagPro y Zeus) y registra el cambio en Productos_HistorialNombres.
        /// </summary>
        /// <param name="request">Datos de la OT y de cada proceso.</param>
        /// <param name="usuarioId">Usuario del token, que queda como creador.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>201 con los identificadores; 400, 409, 422 o 502 con el motivo.</returns>
        public async Task<OrdenTrabajoCompletaResult> CrearAsync(CrearOrdenTrabajoCompletaDto request, long usuarioId, CancellationToken cancellationToken)
        {
            var datos = request.OrdenTrabajo;
            var ahora = DateTime.Now;

            var errores = OrdenTrabajoReglas.ValidarRangos(datos, ahora);
            if (errores.Count > 0)
                return OrdenTrabajoCompletaResult.Invalid(errores);

            if (!OrdenTrabajoReglas.PresentacionesPermitidasCrear.Contains(datos.UndMed_Id))
                return OrdenTrabajoCompletaResult.Unprocessable(
                    $"No se puede crear una OT con la presentación '{datos.UndMed_Id}'. Presentaciones permitidas: {string.Join(", ", OrdenTrabajoReglas.PresentacionesPermitidasCrear)}.");

            var faltantes = await ReferenciasInexistentesAsync(datos.SedeCli_Id, datos.Prod_Id, datos.UndMed_Id, datos.Mezcla_Id, datos.Id_Vendedor, cancellationToken);
            if (faltantes.Count > 0)
                return OrdenTrabajoCompletaResult.Unprocessable($"No existen: {string.Join(", ", faltantes)}.");

            var costos = await ValidarCostosAsync(datos, datos.UndMed_Id, request.SelladoCorte, cancellationToken);
            if (costos.Error != null)
                return costos.Error;
            // MTS: Valor Kg por definir; al crear se guarda 0, como en el front.
            var valorKg = costos.ValorKgCalculado ?? (datos.UndMed_Id == "MTS" ? 0m : datos.Ot_ValorKg);

            // F2-14 (comprobación previa, siempre activa): sin item en BagPro o sin artículo en Zeus el trigger no replica.
            var replicable = await ComprobarItemBagProYArticuloZeusAsync(datos.Prod_Id, datos.UndMed_Id, cancellationToken);
            if (replicable != null)
                return replicable;

            try
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                return await strategy.ExecuteAsync(async () =>
                {
                    _context.ChangeTracker.Clear();
                    // Los triggers de estas tablas (TR_CrearOTs, TR_ActualizarCampos_*) escriben por linked server y promueven la
                    // transacción a distribuida; SQL Server no lo permite si EF Core dejó un savepoint activo en SaveChanges.
                    _context.Database.AutoSavepointsEnabled = false;
                    await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    // F2-01: serializa las creaciones desde Plasticaribe hasta el commit.
                    if (!await TomarBloqueoNumeroAsync(cancellationToken))
                        return OrdenTrabajoCompletaResult.Conflict("Otro usuario está creando una OT, reintente.");

                    var numeroOT = await SiguienteNumeroOTAsync(cancellationToken);

                    // F2-13 (defensa 1): nunca insertar un número que ya existe en BagPro (el trigger sobrescribiría esa OT).
                    if (await ExisteEnBagProAsync(numeroOT, null, cancellationToken))
                        return OrdenTrabajoCompletaResult.Conflict($"El número de OT {numeroOT} ya existe en BagPro. Intente crear la OT de nuevo.");

                    // Nombre (referencia) del producto: se cambia antes de insertar la OT porque TR_CrearOTs y el SP de BagPro
                    // leen Productos.Prod_Nombre al crearla. Se lee dentro de la transacción por si la estrategia reintenta.
                    var nombreProductoActual = await _context.Set<Producto>().AsNoTracking().Where(p => p.Prod_Id == datos.Prod_Id)
                        .Select(p => p.Prod_Nombre).FirstOrDefaultAsync(cancellationToken);
                    var nombreProductoNuevo = OrdenTrabajoNombres.NombreProductoACambiar(datos.Prod_Nombre, nombreProductoActual);
                    if (nombreProductoNuevo != null)
                        await RenombrarProductoAsync(datos.Prod_Id, nombreProductoNuevo, cancellationToken);

                    var orden = OrdenTrabajoMapeo.NuevaOrden(datos, numeroOT, usuarioId, ahora, valorKg);
                    _context.Add(orden);
                    await _context.SaveChangesAsync(cancellationToken);

                    if (nombreProductoNuevo != null)
                        _context.Add(HistorialNombre(datos.Prod_Id, nombreProductoActual, nombreProductoNuevo, orden.Ot_Id, "Creación", usuarioId, ahora));

                    // Una fila por tabla (F2-15): cada INSERT dispara su trigger con una sola fila.
                    _context.Add(OrdenTrabajoMapeo.Extrusion(request.Extrusion, orden.Ot_Id));
                    _context.Add(OrdenTrabajoMapeo.Impresion(request.Impresion, orden.Ot_Id));
                    _context.Add(OrdenTrabajoMapeo.Laminado(request.Laminado, orden.Ot_Id));
                    _context.Add(OrdenTrabajoMapeo.SelladoCorte(request.SelladoCorte, orden.Ot_Id));
                    await _context.SaveChangesAsync(cancellationToken);

                    // F2-14 (comprobación posterior, configurable): en producción va en false hasta el corte (hallazgo H-A).
                    if (ValidarReplicacionBagPro && !await ExisteEnBagProAsync(numeroOT, datos.Prod_Id, cancellationToken))
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return OrdenTrabajoCompletaResult.BadGateway(
                            $"La OT no se pudo replicar en BagPro y no se creó. Revise que el item {datos.Prod_Id} en {datos.UndMed_Id}, " +
                            "la mezcla (mezclas predefinidas de BagPro), el cliente y el usuario existan en BagPro.");
                    }

                    await transaction.CommitAsync(cancellationToken);
                    return OrdenTrabajoCompletaResult.Success(201, "Orden de trabajo creada.", orden.Ot_Id, orden.Numero_OT);
                });
            }
            catch (Exception ex) when (ClasificarError(ex) is { } clasificado)
            {
                _context.ChangeTracker.Clear();
                return clasificado;
            }
        }

        /// <summary>
        /// Actualiza la OT y sus cuatro procesos en un único SaveChanges (atómico) aplicando solo la lista blanca de edición (F2-03).
        /// Conserva el número, el creador, la fecha y la hora de creación; producto, presentación y estado no se pueden cambiar (la sede del cliente sí).
        /// Registra al último editor y usa <c>Ot_RowVersion</c> para detectar ediciones concurrentes (409).
        /// Si a la OT le falta algún proceso, se crea. Si cambió el nombre del producto, lo actualiza en el maestro en la misma
        /// transacción; el nombre del cliente se guarda solo en la OT (<c>Ot_NombreCliente</c>, ver <see cref="OrdenTrabajoNombres"/>).
        /// </summary>
        /// <param name="otId">Identificador interno de la orden de trabajo.</param>
        /// <param name="request">Datos de la OT y de cada proceso, con la versión de la fila.</param>
        /// <param name="usuarioId">Usuario del token, que queda como último editor.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con los identificadores; 400, 404, 409, 422 o 502 con el motivo.</returns>
        public async Task<OrdenTrabajoCompletaResult> EditarAsync(long otId, EditarOrdenTrabajoCompletaDto request, long usuarioId, CancellationToken cancellationToken)
        {
            _context.ChangeTracker.Clear();

            byte[] rowVersion;
            try
            {
                rowVersion = Convert.FromBase64String(request.Ot_RowVersion);
            }
            catch (FormatException)
            {
                return OrdenTrabajoCompletaResult.Invalid(new Dictionary<string, string[]> { ["Ot_RowVersion"] = new[] { "La versión de la OT no es válida." } });
            }

            var orden = await _context.Set<Orden_Trabajo>().FirstOrDefaultAsync(ot => ot.Ot_Id == otId, cancellationToken);
            if (orden == null)
                return OrdenTrabajoCompletaResult.NotFound($"No existe la orden de trabajo con Id {otId}.");

            var datos = request.OrdenTrabajo;
            var errores = OrdenTrabajoReglas.ValidarRangos(datos, orden.Ot_FechaCreacion, orden.Ot_FechaEntrega);
            if (errores.Count > 0)
                return OrdenTrabajoCompletaResult.Invalid(errores);

            var noEditables = new List<string>();
            if (datos.Prod_Id.HasValue && datos.Prod_Id.Value != orden.Prod_Id) noEditables.Add("producto");
            if (datos.UndMed_Id != null && datos.UndMed_Id != orden.UndMed_Id) noEditables.Add("presentación");
            if (datos.Estado_Id.HasValue && datos.Estado_Id.Value != orden.Estado_Id) noEditables.Add("estado");
            if (noEditables.Count > 0)
                return OrdenTrabajoCompletaResult.Unprocessable($"Al editar una OT no se puede cambiar: {string.Join(", ", noEditables)}.");

            // El cliente (sede) sí se puede cambiar al editar: TR_CrearOTs lleva el nuevo cliente a ClientesOT de BagPro.
            long? sedeNueva = datos.SedeCli_Id.HasValue && datos.SedeCli_Id.Value != orden.SedeCli_Id ? datos.SedeCli_Id.Value : null;
            var faltantes = await ReferenciasInexistentesAsync(sedeNueva, null, null, datos.Mezcla_Id, datos.Id_Vendedor, cancellationToken);
            if (faltantes.Count > 0)
                return OrdenTrabajoCompletaResult.Unprocessable($"No existen: {string.Join(", ", faltantes)}.");

            var costos = await ValidarCostosAsync(datos, orden.UndMed_Id, request.SelladoCorte, cancellationToken);
            if (costos.Error != null)
                return costos.Error;
            // MTS (Valor Kg por definir) y presentaciones sin regla: se conserva el Valor Kg guardado.
            var valorKg = costos.ValorKgCalculado ?? orden.Ot_ValorKg;

            // Nombres: el del producto se cambia en el maestro; el del cliente solo en esta OT (Clientes no cambia).
            var nombreProductoActual = await _context.Set<Producto>().Where(p => p.Prod_Id == orden.Prod_Id)
                .Select(p => p.Prod_Nombre).FirstOrDefaultAsync(cancellationToken);
            if (sedeNueva.HasValue)
            {
                // Otro cliente: el nombre propio que tuviera la OT era del cliente anterior y se descarta.
                orden.SedeCli_Id = sedeNueva.Value;
                orden.Ot_NombreCliente = null;
            }
            var nombreClienteMaestro = await _context.Set<SedesClientes>().Where(s => s.SedeCli_Id == orden.SedeCli_Id)
                .Select(s => s.Cli!.Cli_Nombre).FirstOrDefaultAsync(cancellationToken);
            var nombreProductoNuevo = OrdenTrabajoNombres.NombreProductoACambiar(datos.Prod_Nombre, nombreProductoActual);

            var ahoraEdicion = DateTime.Now;
            OrdenTrabajoMapeo.AplicarEdicion(datos, orden, usuarioId, ahoraEdicion, valorKg);
            if (nombreProductoNuevo != null)
                _context.Add(HistorialNombre(orden.Prod_Id, nombreProductoActual, nombreProductoNuevo, otId, "Edición", usuarioId, ahoraEdicion));
            orden.Ot_NombreCliente = OrdenTrabajoNombres.NombreClienteOt(datos.Ot_NombreCliente, orden.Ot_NombreCliente, nombreClienteMaestro);
            _context.Entry(orden).Property(o => o.Ot_RowVersion).OriginalValue = rowVersion;

            await ActualizarOCrearProcesoAsync(_context.Set<OT_Extrusion>().Where(p => p.Ot_Id == otId), OrdenTrabajoMapeo.Extrusion(request.Extrusion, otId),
                (registro, valores) => valores.Extrusion_Id = registro.Extrusion_Id, cancellationToken);
            await ActualizarOCrearProcesoAsync(_context.Set<OT_Impresion>().Where(p => p.Ot_Id == otId), OrdenTrabajoMapeo.Impresion(request.Impresion, otId),
                (registro, valores) => valores.Impresion_Id = registro.Impresion_Id, cancellationToken);
            await ActualizarOCrearProcesoAsync(_context.Set<OT_Laminado>().Where(p => p.OT_Id == otId), OrdenTrabajoMapeo.Laminado(request.Laminado, otId),
                (registro, valores) => valores.LamCapa_Id = registro.LamCapa_Id, cancellationToken);
            await ActualizarOCrearProcesoAsync(_context.Set<OT_Sellado_Corte>().Where(p => p.Ot_Id == otId), OrdenTrabajoMapeo.SelladoCorte(request.SelladoCorte, otId),
                (registro, valores) => valores.SelladoCorte_Id = registro.SelladoCorte_Id, cancellationToken);

            try
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    // Sin savepoints, como al crear: los triggers que escriben por linked server promueven la transacción a distribuida.
                    _context.Database.AutoSavepointsEnabled = false;
                    await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    // El producto se renombra antes de guardar la OT porque TR_CrearOTs lee Productos.Prod_Nombre al
                    // actualizarla. Es un UPDATE idempotente de una sola fila, así que se puede repetir si la estrategia reintenta.
                    if (nombreProductoNuevo != null)
                        await RenombrarProductoAsync(orden.Prod_Id, nombreProductoNuevo, cancellationToken);

                    // Un solo SaveChanges para la OT y sus procesos. Los cambios se aceptan después del commit para que,
                    // si la estrategia reintenta, se vuelvan a enviar.
                    await _context.SaveChangesAsync(acceptAllChangesOnSuccess: false, cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                });
                _context.ChangeTracker.AcceptAllChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                _context.ChangeTracker.Clear();
                return OrdenTrabajoCompletaResult.Conflict("La OT fue modificada por otro usuario después de cargarla. Vuelva a cargarla y repita los cambios.");
            }
            catch (Exception ex) when (ClasificarError(ex) is { } clasificado)
            {
                _context.ChangeTracker.Clear();
                return clasificado;
            }

            return OrdenTrabajoCompletaResult.Success(200, "Orden de trabajo actualizada.", orden.Ot_Id, orden.Numero_OT);
        }

        /// <summary>
        /// Indica si está activa la comprobación posterior de la réplica en BagPro. Por defecto es false (B1): producción
        /// no la activa por omisión mientras plasticaribe2 lea PlasticaribeBDD_Test (hallazgo H-A); Development la activa
        /// explícitamente en appsettings.Development.json.
        /// </summary>
        private bool ValidarReplicacionBagPro => _configuration.GetValue(ClaveValidarReplicacion, false);

        /// <summary>
        /// Cambia el nombre del producto en el maestro con un UPDATE directo (idempotente, se puede repetir si la estrategia
        /// reintenta). Debe ir dentro de la transacción y antes de guardar la OT, porque TR_CrearOTs lee Productos.Prod_Nombre.
        /// </summary>
        /// <param name="prodId">Producto.</param>
        /// <param name="nombre">Nombre nuevo, ya validado y sin espacios extremos.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        private Task<int> RenombrarProductoAsync(int prodId, string nombre, CancellationToken cancellationToken) =>
            _context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE dbo.Productos SET Prod_Nombre = {nombre} WHERE Prod_Id = {prodId};", cancellationToken);

        /// <summary>
        /// Arma el registro del historial de nombres de un producto.
        /// </summary>
        /// <param name="prodId">Producto.</param>
        /// <param name="anterior">Nombre antes del cambio.</param>
        /// <param name="nuevo">Nombre después del cambio.</param>
        /// <param name="otId">OT en la que se cambió.</param>
        /// <param name="origen">"Creación" o "Edición".</param>
        /// <param name="usuarioId">Usuario del token.</param>
        /// <param name="fecha">Fecha y hora del cambio.</param>
        /// <returns>Registro listo para agregar al contexto.</returns>
        private static Producto_HistorialNombre HistorialNombre(int prodId, string? anterior, string nuevo, long otId, string origen, long usuarioId, DateTime fecha) => new()
        {
            Prod_Id = prodId,
            ProdHN_NombreAnterior = (anterior ?? "").Trim(),
            ProdHN_NombreNuevo = nuevo,
            Ot_Id = otId,
            ProdHN_Origen = origen,
            Usua_Id = usuarioId,
            ProdHN_Fecha = fecha,
        };

        /// <summary>
        /// Comprueba que existan las referencias principales de la OT. Los parámetros null no se comprueban.
        /// </summary>
        /// <param name="sedeId">Sede del cliente.</param>
        /// <param name="prodId">Producto.</param>
        /// <param name="presentacion">Presentación (Unidades_Medidas).</param>
        /// <param name="mezclaId">Mezcla.</param>
        /// <param name="vendedorId">Vendedor (Usuarios).</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Nombres de las referencias que no existen.</returns>
        private async Task<List<string>> ReferenciasInexistentesAsync(long? sedeId, int? prodId, string? presentacion, long mezclaId, long vendedorId, CancellationToken cancellationToken)
        {
            var faltantes = new List<string>();
            if (sedeId.HasValue && !await _context.Set<SedesClientes>().AnyAsync(s => s.SedeCli_Id == sedeId.Value, cancellationToken))
                faltantes.Add($"la sede del cliente {sedeId}");
            if (prodId.HasValue && !await _context.Set<Producto>().AnyAsync(p => p.Prod_Id == prodId.Value, cancellationToken))
                faltantes.Add($"el producto {prodId}");
            if (presentacion != null && !await _context.Set<Unidad_Medida>().AnyAsync(u => u.UndMed_Id == presentacion, cancellationToken))
                faltantes.Add($"la presentación {presentacion}");
            if (!await _context.Set<Mezcla>().AnyAsync(m => m.Mezcla_Id == mezclaId, cancellationToken))
                faltantes.Add($"la mezcla {mezclaId}");
            if (!await _context.Set<Usuario>().AnyAsync(u => u.Usua_Id == vendedorId, cancellationToken))
                faltantes.Add($"el vendedor {vendedorId}");
            return faltantes;
        }

        /// <summary>
        /// Valida que Valor OT, Valor Kg y Neto Kg coincidan con <see cref="OtCalculoCostos"/> (mismas reglas que el front).
        /// Usa el Peso Millar y las bolsas por paquete de sellado/corte (las que se guardan) y el nombre del formato para la regla D2.
        /// </summary>
        /// <param name="datos">Datos generales recibidos.</param>
        /// <param name="presentacion">Presentación de la OT (al editar, la guardada).</param>
        /// <param name="sellado">Datos de sellado/corte recibidos.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Error 422 si no coinciden; si coinciden, el Valor Kg calculado (null en MTS o sin regla).</returns>
        private async Task<(OrdenTrabajoCompletaResult? Error, decimal? ValorKgCalculado)> ValidarCostosAsync(
            OrdenTrabajoDatosBaseDto datos, string presentacion, OtSelladoCorteDto sellado, CancellationToken cancellationToken)
        {
            string? tipoProducto = null;
            if (OtCalculoCostos.PresentacionesD2.Contains(presentacion))
            {
                tipoProducto = await _context.Set<Tipo_Producto>().Where(t => t.TpProd_Id == sellado.Formato_Id)
                    .Select(t => t.TpProd_Nombre).FirstOrDefaultAsync(cancellationToken);
                if (tipoProducto == null)
                    return (OrdenTrabajoCompletaResult.Unprocessable($"No existe el formato {sellado.Formato_Id} de sellado/corte."), null);
            }

            var calculado = OtCalculoCostos.Calcular(new EntradaCostosOT(
                presentacion, datos.Ot_CantidadPedida, datos.Ot_ValorUnidad, datos.Ot_MargenAdicional,
                sellado.SelladoCorte_PesoMillar, tipoProducto, sellado.SelladoCorte_CantBolsasPaquete));
            if (calculado == null)
                return (null, null);

            var diferencias = OtCalculoCostos.Diferencias(calculado, datos.Ot_PesoNetoKg, datos.Ot_ValorKg, datos.Ot_ValorOT);
            if (diferencias.Count > 0)
                return (OrdenTrabajoCompletaResult.Unprocessable(
                    $"Los costos de la OT no coinciden con el cálculo del servidor ({string.Join("; ", diferencias)}). Recalcule la OT e intente de nuevo."), null);

            return (null, calculado.ValorKg.HasValue ? datos.Ot_ValorKg : null);
        }

        /// <summary>
        /// F2-14, comprobación previa: el item debe existir en BagPro (ClientesOT_Item, producto + presentación) y el artículo
        /// en Zeus (Articulo, código + presentación), con las mismas equivalencias que TR_CrearOTs. Se consulta fuera de la
        /// transacción para no retener el bloqueo del número mientras se consulta el linked server.
        /// </summary>
        /// <param name="prodId">Producto de la OT.</param>
        /// <param name="presentacion">Presentación de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>null si se puede replicar; 422 con el motivo si falta alguno.</returns>
        private async Task<OrdenTrabajoCompletaResult?> ComprobarItemBagProYArticuloZeusAsync(int prodId, string presentacion, CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.bp_ClientesOT_Item WHERE ClienteItems = @prod AND PtPresentacionNom = @presBagPro) THEN 1 ELSE 0 END AS int) AS ItemBagPro,
       CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.zeus_Articulo WHERE Codigo = @codigo AND Presentacion = @presZeus) THEN 1 ELSE 0 END AS int) AS ArticuloZeus;";

            int itemBagPro = 0, articuloZeus = 0;
            try
            {
                await EjecutarAsync(sql, async cmd =>
                {
                    await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
                    if (await reader.ReadAsync(cancellationToken))
                    {
                        itemBagPro = reader.GetInt32(0);
                        articuloZeus = reader.GetInt32(1);
                    }
                    return 0;
                }, cancellationToken,
                    new SqlParameter("@prod", SqlDbType.Int) { Value = prodId },
                    new SqlParameter("@presBagPro", SqlDbType.VarChar, 50) { Value = OrdenTrabajoReglas.PresentacionBagPro(presentacion) },
                    new SqlParameter("@codigo", SqlDbType.VarChar, 50) { Value = prodId.ToString() },
                    new SqlParameter("@presZeus", SqlDbType.VarChar, 50) { Value = OrdenTrabajoReglas.PresentacionZeus(presentacion) });
            }
            catch (Exception ex) when (ClasificarError(ex) is { } clasificado)
            {
                return clasificado;
            }

            var faltan = new List<string>();
            if (itemBagPro == 0) faltan.Add($"el item {prodId} en presentación {OrdenTrabajoReglas.PresentacionBagPro(presentacion)} en BagPro");
            if (articuloZeus == 0) faltan.Add($"el artículo {prodId} en presentación {OrdenTrabajoReglas.PresentacionZeus(presentacion)} en Zeus");
            return faltan.Count == 0
                ? null
                : OrdenTrabajoCompletaResult.Unprocessable($"No se puede crear la OT porque no existe {string.Join(" ni ", faltan)}.");
        }

        /// <summary>
        /// Toma el bloqueo de aplicación exclusivo <see cref="RecursoBloqueoNumero"/> ligado a la transacción actual.
        /// </summary>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>true si se obtuvo; false si otro usuario lo tiene después del tiempo de espera.</returns>
        private async Task<bool> TomarBloqueoNumeroAsync(CancellationToken cancellationToken)
        {
            const string sql = @"
DECLARE @r int;
EXEC @r = sp_getapplock @Resource = @recurso, @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = @espera;
SELECT @r;";
            var resultado = await EjecutarAsync(sql, async cmd => Convert.ToInt32(await cmd.ExecuteScalarAsync(cancellationToken)), cancellationToken,
                new SqlParameter("@recurso", SqlDbType.NVarChar, 255) { Value = RecursoBloqueoNumero },
                new SqlParameter("@espera", SqlDbType.Int) { Value = TiempoEsperaBloqueoMs });
            return resultado >= 0;
        }

        /// <summary>
        /// D5/F2-01: siguiente número de OT = MAX(MAX(BagPro ClientesOT.Item), MAX(Orden_Trabajo.Numero_OT)) + 1.
        /// BagPro se lee por el sinónimo bp_ClientesOT, la misma base a la que escribe TR_CrearOTs. Debe llamarse con el bloqueo tomado.
        /// </summary>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El número que se asignará a la nueva OT.</returns>
        private Task<long> SiguienteNumeroOTAsync(CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT CASE WHEN ISNULL(b.M, 0) > ISNULL(p.M, 0) THEN ISNULL(b.M, 0) ELSE ISNULL(p.M, 0) END + 1
FROM (SELECT MAX(CAST(Item AS bigint)) AS M FROM dbo.bp_ClientesOT) AS b
CROSS JOIN (SELECT MAX(Numero_OT) AS M FROM dbo.Orden_Trabajo) AS p;";
            return EjecutarAsync(sql, async cmd => Convert.ToInt64(await cmd.ExecuteScalarAsync(cancellationToken)), cancellationToken);
        }

        /// <summary>
        /// Indica si el número de OT existe en BagPro (ClientesOT) y, si se indica, con ese producto.
        /// </summary>
        /// <param name="numeroOT">Número de OT.</param>
        /// <param name="prodId">Producto que debe tener la OT en BagPro; null para no comprobarlo.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>true si existe.</returns>
        private async Task<bool> ExisteEnBagProAsync(long numeroOT, int? prodId, CancellationToken cancellationToken)
        {
            const string sql = @"
SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.bp_ClientesOT WHERE Item = @numero AND (@prod IS NULL OR ClienteItems = @prod)) THEN 1 ELSE 0 END;";
            var existe = await EjecutarAsync(sql, async cmd => Convert.ToInt32(await cmd.ExecuteScalarAsync(cancellationToken)), cancellationToken,
                new SqlParameter("@numero", SqlDbType.BigInt) { Value = numeroOT },
                new SqlParameter("@prod", SqlDbType.Int) { Value = (object?)prodId ?? DBNull.Value });
            return existe == 1;
        }

        /// <summary>
        /// Ejecuta un comando SQL con la conexión del contexto y, si hay una, dentro de la transacción actual.
        /// Abre y cierra la conexión solo si estaba cerrada.
        /// </summary>
        /// <typeparam name="T">Tipo del resultado.</typeparam>
        /// <param name="sql">Texto del comando.</param>
        /// <param name="ejecutar">Función que ejecuta el comando y obtiene el resultado.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <param name="parametros">Parámetros del comando.</param>
        /// <returns>El resultado de <paramref name="ejecutar"/>.</returns>
        private async Task<T> EjecutarAsync<T>(string sql, Func<DbCommand, Task<T>> ejecutar, CancellationToken cancellationToken, params SqlParameter[] parametros)
        {
            var conexion = _context.Database.GetDbConnection();
            var abrir = conexion.State != ConnectionState.Open;
            if (abrir)
                await conexion.OpenAsync(cancellationToken);
            try
            {
                await using var cmd = conexion.CreateCommand();
                cmd.CommandText = sql;
                cmd.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction();
                cmd.Parameters.AddRange(parametros);
                return await ejecutar(cmd);
            }
            finally
            {
                if (abrir)
                    await conexion.CloseAsync();
            }
        }

        /// <summary>
        /// Convierte los errores previstos de SQL Server (trigger, PK, FK, linked server/MSDTC) en un resultado legible (F2-12).
        /// </summary>
        /// <param name="ex">Excepción capturada.</param>
        /// <returns>El resultado, o null si el error no es de los previstos y debe propagarse.</returns>
        private static OrdenTrabajoCompletaResult? ClasificarError(Exception ex)
        {
            var sqlEx = ex as SqlException ?? ex.InnerException as SqlException ?? ex.GetBaseException() as SqlException;
            if (sqlEx == null)
                return null;
            var clasificado = OrdenTrabajoReglas.ClasificarErrorSql(sqlEx.Number);
            return clasificado == null ? null : new OrdenTrabajoCompletaResult(false, clasificado.Value.StatusCode, clasificado.Value.Mensaje, null, null);
        }

        /// <summary>
        /// Copia los valores recibidos sobre los registros existentes del proceso o, si la OT no tiene ninguno, lo agrega.
        /// </summary>
        /// <typeparam name="T">Entidad del proceso de la OT.</typeparam>
        /// <param name="registrosDeLaOrden">Consulta de los registros del proceso que pertenecen a la OT.</param>
        /// <param name="valores">Valores recibidos, con el Id de la OT ya asignado y la llave en cero.</param>
        /// <param name="copiarLlave">Copia la llave del registro existente a los valores para que SetValues no la modifique.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        private async Task ActualizarOCrearProcesoAsync<T>(
            IQueryable<T> registrosDeLaOrden,
            T valores,
            Action<T, T> copiarLlave,
            CancellationToken cancellationToken) where T : class
        {
            var registros = await registrosDeLaOrden.ToListAsync(cancellationToken);
            if (registros.Count == 0)
            {
                _context.Add(valores);
                return;
            }

            foreach (var registro in registros)
            {
                copiarLlave(registro, valores);
                _context.Entry(registro).CurrentValues.SetValues(valores);
            }
        }
    }

    /// <summary>
    /// Resultado de la creación o edición de una orden de trabajo.
    /// </summary>
    /// <param name="Succeeded">Indica si la operación se completó.</param>
    /// <param name="StatusCode">Código HTTP asociado al resultado.</param>
    /// <param name="Message">Mensaje descriptivo del resultado.</param>
    /// <param name="Ot_Id">Identificador interno de la OT, si la operación fue exitosa.</param>
    /// <param name="Numero_OT">Número visible de la OT, si la operación fue exitosa.</param>
    /// <param name="Errores">Errores por campo (solo en 400).</param>
    public sealed record OrdenTrabajoCompletaResult(bool Succeeded, int StatusCode, string Message, long? Ot_Id, long? Numero_OT, IDictionary<string, string[]>? Errores = null)
    {
        /// <summary>Crea un resultado exitoso.</summary>
        /// <param name="statusCode">Código HTTP de éxito (201 al crear, 200 al editar).</param>
        /// <param name="message">Mensaje descriptivo.</param>
        /// <param name="otId">Identificador interno de la OT.</param>
        /// <param name="numeroOT">Número visible de la OT.</param>
        /// <returns>Resultado exitoso.</returns>
        public static OrdenTrabajoCompletaResult Success(int statusCode, string message, long otId, long numeroOT) => new(true, statusCode, message, otId, numeroOT);

        /// <summary>Crea un resultado por datos fuera de rango.</summary>
        /// <param name="errores">Errores por campo.</param>
        /// <returns>Resultado con código 400.</returns>
        public static OrdenTrabajoCompletaResult Invalid(IDictionary<string, string[]> errores) => new(false, 400, "Los datos de la OT no son válidos.", null, null, errores);

        /// <summary>Crea un resultado por recurso inexistente.</summary>
        /// <param name="message">Descripción del recurso no encontrado.</param>
        /// <returns>Resultado con código 404.</returns>
        public static OrdenTrabajoCompletaResult NotFound(string message) => new(false, 404, message, null, null);

        /// <summary>Crea un resultado por conflicto con el estado actual de los datos.</summary>
        /// <param name="message">Descripción del conflicto.</param>
        /// <returns>Resultado con código 409.</returns>
        public static OrdenTrabajoCompletaResult Conflict(string message) => new(false, 409, message, null, null);

        /// <summary>Crea un resultado por una regla de negocio incumplida.</summary>
        /// <param name="message">Descripción de la regla incumplida.</param>
        /// <returns>Resultado con código 422.</returns>
        public static OrdenTrabajoCompletaResult Unprocessable(string message) => new(false, 422, message, null, null);

        /// <summary>Crea un resultado por un fallo de BagPro o Zeus.</summary>
        /// <param name="message">Descripción del fallo.</param>
        /// <returns>Resultado con código 502.</returns>
        public static OrdenTrabajoCompletaResult BadGateway(string message) => new(false, 502, message, null, null);
    }
}
