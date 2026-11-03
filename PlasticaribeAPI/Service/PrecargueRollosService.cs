using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Gestiona el precargue de rollos de despacho rollo a rollo: cada rollo escaneado queda guardado de inmediato en un
    /// precargue en proceso, de modo que si la página se cierra el usuario puede retomarlo donde lo dejó.
    /// </summary>
    public sealed class PrecargueRollosService
    {
        /// <summary>Clientes a nombre de los cuales se pesan los rollos de Cliente Stock.</summary>
        private static readonly long[] ClientesStock = { 1061, 1035 };

        /// <summary>Procesos cuyos rollos pueden precargarse para despacho.</summary>
        private static readonly string[] ProcesosDespacho = { "SELLA", "EMP", "EXT" };

        /// <summary>Orden de facturación genérica con la que se crean los precargues.</summary>
        private const int OrdenFacturacionGenerica = 4472;

        /// <summary>Primera entrada de rollos cuya observación corresponde a la ubicación del rollo en bodega.</summary>
        private const long PrimeraEntradaConUbicacion = 28512;

        private readonly dataContext _context;

        /// <summary>
        /// Inicializa el servicio con el contexto de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        public PrecargueRollosService(dataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Agrega un rollo a un precargue en proceso, o crea el precargue si no se envía su Id, en una sola transacción:
        /// guarda el detalle y cambia el rollo de Disponible a Precargado solo si sigue disponible.
        /// Si el rollo ya pertenece a ese mismo precargue (reintento tras perder la respuesta) responde con él sin modificar nada.
        /// </summary>
        /// <param name="request">Precargue, cliente y rollo escaneado.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>201 si se creó el precargue, 200 si se agregó a uno existente; 404 o 409 si el rollo o el precargue no son válidos.</returns>
        public async Task<PrecargueRollosResult> AgregarRolloAsync(AgregarRolloPrecargueRequest request, CancellationToken cancellationToken)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                _context.ChangeTracker.Clear();
                // Los triggers de Produccion_Procesos (TR_EnviarRollosBagpro, TR_ActualizarProduccion) pueden promover la
                // transacción a distribuida; SQL Server no lo permite si EF Core dejó un savepoint activo en SaveChanges.
                _context.Database.AutoSavepointsEnabled = false;
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                var pcdId = request.Pcd_Id;
                if (pcdId != null)
                {
                    var bloqueo = await BloquearPrecargueEnProcesoAsync(pcdId.Value, cancellationToken);
                    if (bloqueo != null) return bloqueo;

                    var cliente = await _context.Set<Precargue_Despacho>().AsNoTracking()
                        .Where(p => p.Pcd_Id == pcdId).Select(p => p.Cli_Id).FirstAsync(cancellationToken);
                    if (cliente != request.Cli_Id)
                        return PrecargueRollosResult.Conflict($"El precargue N° {pcdId} pertenece a otro cliente.");

                    var registrado = (await ConsultarRollosAsync(pcdId.Value, request.Rollo, cancellationToken)).FirstOrDefault();
                    if (registrado != null)
                    {
                        var total = await ContarRollosAsync(pcdId.Value, cancellationToken);
                        return PrecargueRollosResult.Success(200, new AgregarRolloPrecargueResponse(pcdId.Value, false, true, total, registrado));
                    }
                }

                var clientes = request.ClienteStock ? ClientesStock : new[] { request.Cli_Id };
                var produccion = await (from pp in _context.Set<Produccion_Procesos>().AsNoTracking()
                                        from e in _context.Set<Existencia_Productos>()
                                        where pp.NumeroRollo_BagPro == request.Rollo &&
                                              pp.Estado_Rollo == (int)EstadoEnum.Disponible &&
                                              pp.Envio_Zeus &&
                                              ProcesosDespacho.Contains(pp.Proceso_Id) &&
                                              clientes.Contains(pp.Cli_Id) &&
                                              e.Prod_Id == pp.Prod_Id &&
                                              e.UndMed_Id == pp.Presentacion
                                        orderby pp.Id descending
                                        select new
                                        {
                                            pp.Id,
                                            pp.Numero_Rollo,
                                            pp.Prod_Id,
                                            Referencia = pp.Producto!.Prod_Nombre,
                                            pp.Cli_Id,
                                            Cliente = pp.Clientes!.Cli_Nombre,
                                            Cantidad = pp.Presentacion == "Kg" ? pp.Peso_Neto : pp.Cantidad,
                                            pp.Peso_Bruto,
                                            pp.Peso_Neto,
                                            pp.Presentacion,
                                            pp.OT,
                                            pp.Proceso_Id,
                                            Proceso = pp.Proceso!.Proceso_Nombre,
                                            Stock = e.ExProd_Cantidad,
                                        }).FirstOrDefaultAsync(cancellationToken);

                if (produccion == null)
                    return await DiagnosticarRolloNoDisponibleAsync(request.Rollo, cancellationToken);
                if (produccion.Stock <= 0)
                    return PrecargueRollosResult.Conflict($"El item N° {produccion.Prod_Id} no tiene stock disponible.");

                // La condición sobre el estado garantiza que, si otro proceso tomó el rollo después de la consulta, no se precargue dos veces.
                var rollosActualizados = await _context.Set<Produccion_Procesos>()
                    .Where(pp => pp.Id == produccion.Id && pp.Estado_Rollo == (int)EstadoEnum.Disponible)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(pp => pp.Estado_Rollo, (int)EstadoEnum.Precargado)
                        .SetProperty(pp => pp.Envio_Zeus, true), cancellationToken);
                if (rollosActualizados == 0)
                    return PrecargueRollosResult.Conflict($"El rollo/bulto N° {request.Rollo} acaba de ser tomado por otro proceso.");

                var ubicacion = await (from dt in _context.Set<DetalleEntradaRollo_Producto>().AsNoTracking()
                                       join en in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals en.EntRolloProd_Id
                                       where dt.Rollo_Id == produccion.Numero_Rollo && en.EntRolloProd_Id >= PrimeraEntradaConUbicacion
                                       orderby en.EntRolloProd_Id descending
                                       select en.EntRolloProd_Observacion).FirstOrDefaultAsync(cancellationToken);

                var detalle = new Detalles_PrecargueDespacho
                {
                    Prod_Id = produccion.Prod_Id,
                    DtlPcd_Rollo = request.Rollo,
                    DtlPcd_Cantidad = produccion.Cantidad,
                    UndMed_Id = produccion.Presentacion,
                    DtlPcd_OT = produccion.OT,
                    DtlPcd_PesoNeto = produccion.Peso_Neto,
                    DtlPcd_PesoBruto = produccion.Peso_Bruto,
                    DtlPcd_Referencia = Recortar(produccion.Referencia, 100),
                    DtlPcd_Ubicacion = Recortar(ubicacion, 50),
                };

                Precargue_Despacho? nuevo = null;
                if (pcdId == null)
                {
                    var (fecha, hora) = FechaHoraActual();
                    nuevo = new Precargue_Despacho
                    {
                        Cli_Id = request.Cli_Id,
                        OF_Id = OrdenFacturacionGenerica,
                        Pcd_FechaCrea = fecha,
                        Pcd_HoraCrea = hora,
                        Usua_Crea = request.Usuario,
                        Pcd_Observacion = request.Observacion,
                        Estado_Id = (int)EstadoEnum.EnProceso,
                        Pcd_FechaModifica = fecha,
                        Pcd_HoraModifica = hora,
                        Usua_Modifica = request.Usuario,
                        Pcd_ObservacionModifica = string.Empty,
                        Usua_Vendedor = request.Vendedor,
                    };
                    detalle.Precargue = nuevo;
                }
                else detalle.Pcd_Id = pcdId.Value;

                // Encabezado (si es nuevo) y detalle se insertan en un solo SaveChanges.
                _context.Add(detalle);
                await _context.SaveChangesAsync(cancellationToken);
                pcdId = detalle.Pcd_Id;

                var totalRollos = await ContarRollosAsync(pcdId.Value, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                var rollo = new RolloPrecargueDto(request.Rollo, produccion.Prod_Id, produccion.Referencia, produccion.Cli_Id, produccion.Cliente,
                    produccion.Cantidad, produccion.Peso_Bruto, produccion.Peso_Neto, produccion.Presentacion, produccion.OT,
                    produccion.Proceso_Id, produccion.Proceso, detalle.DtlPcd_Ubicacion);
                return PrecargueRollosResult.Success(nuevo != null ? 201 : 200,
                    new AgregarRolloPrecargueResponse(pcdId.Value, nuevo != null, false, totalRollos, rollo));
            });
        }

        /// <summary>
        /// Quita un rollo de un precargue en proceso y lo devuelve a Disponible, en una sola transacción.
        /// </summary>
        /// <param name="pcdId">Id del precargue.</param>
        /// <param name="rollo">Número de rollo/bulto de BagPro.</param>
        /// <param name="item">Item del rollo.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con la cantidad de rollos restantes; 404 si el rollo no está en el precargue; 409 si el precargue no está en proceso.</returns>
        public async Task<PrecargueRollosResult> QuitarRolloAsync(long pcdId, long rollo, int item, CancellationToken cancellationToken)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                _context.ChangeTracker.Clear();
                _context.Database.AutoSavepointsEnabled = false;
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                var bloqueo = await BloquearPrecargueEnProcesoAsync(pcdId, cancellationToken);
                if (bloqueo != null) return bloqueo;

                var eliminados = await _context.Set<Detalles_PrecargueDespacho>()
                    .Where(d => d.Pcd_Id == pcdId && d.DtlPcd_Rollo == rollo && d.Prod_Id == item)
                    .ExecuteDeleteAsync(cancellationToken);
                if (eliminados == 0)
                    return PrecargueRollosResult.NotFound($"El rollo/bulto N° {rollo} no está en el precargue N° {pcdId}.");

                await _context.Set<Produccion_Procesos>()
                    .Where(pp => pp.NumeroRollo_BagPro == rollo &&
                                 pp.Prod_Id == item &&
                                 pp.Estado_Rollo == (int)EstadoEnum.Precargado &&
                                 ProcesosDespacho.Contains(pp.Proceso_Id))
                    .ExecuteUpdateAsync(s => s.SetProperty(pp => pp.Estado_Rollo, (int)EstadoEnum.Disponible), cancellationToken);

                var totalRollos = await ContarRollosAsync(pcdId, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return PrecargueRollosResult.Success(200, new { pcd_Id = pcdId, totalRollos });
            });
        }

        /// <summary>
        /// Finaliza un precargue en proceso que tenga al menos un rollo: pasa a Pendiente y queda disponible para facturación.
        /// </summary>
        /// <param name="pcdId">Id del precargue.</param>
        /// <param name="request">Usuario y observación final; si la observación es null se conserva la actual.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 si se finalizó; 404 si no existe; 409 si no está en proceso o no tiene rollos.</returns>
        public async Task<PrecargueRollosResult> FinalizarAsync(long pcdId, CambiarEstadoPrecargueRequest request, CancellationToken cancellationToken)
        {
            var (fecha, hora) = FechaHoraActual();
            var actualizados = await _context.Set<Precargue_Despacho>()
                .Where(p => p.Pcd_Id == pcdId &&
                            p.Estado_Id == (int)EstadoEnum.EnProceso &&
                            _context.Set<Detalles_PrecargueDespacho>().Any(d => d.Pcd_Id == p.Pcd_Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Estado_Id, (int)EstadoEnum.Pendiente)
                    .SetProperty(p => p.Pcd_Observacion, p => request.Observacion ?? p.Pcd_Observacion)
                    .SetProperty(p => p.Pcd_FechaModifica, fecha)
                    .SetProperty(p => p.Pcd_HoraModifica, hora)
                    .SetProperty(p => p.Usua_Modifica, request.Usuario), cancellationToken);

            if (actualizados > 0)
                return PrecargueRollosResult.Success(200, new { pcd_Id = pcdId });

            var diagnostico = await DiagnosticarPrecargueAsync(pcdId, cancellationToken);
            return diagnostico ?? PrecargueRollosResult.Conflict($"El precargue N° {pcdId} no tiene rollos.");
        }

        /// <summary>
        /// Anula un precargue en proceso y devuelve a Disponible los rollos que seguían precargados, en una sola transacción.
        /// Los detalles se conservan como histórico.
        /// </summary>
        /// <param name="pcdId">Id del precargue.</param>
        /// <param name="request">Usuario y motivo de la anulación.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con la cantidad de rollos liberados; 404 si no existe; 409 si no está en proceso.</returns>
        public async Task<PrecargueRollosResult> AnularAsync(long pcdId, CambiarEstadoPrecargueRequest request, CancellationToken cancellationToken)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                _context.ChangeTracker.Clear();
                _context.Database.AutoSavepointsEnabled = false;
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                var (fecha, hora) = FechaHoraActual();
                var actualizados = await _context.Set<Precargue_Despacho>()
                    .Where(p => p.Pcd_Id == pcdId && p.Estado_Id == (int)EstadoEnum.EnProceso)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.Estado_Id, (int)EstadoEnum.Anulado)
                        .SetProperty(p => p.Pcd_ObservacionModifica, request.Observacion ?? string.Empty)
                        .SetProperty(p => p.Pcd_FechaModifica, fecha)
                        .SetProperty(p => p.Pcd_HoraModifica, hora)
                        .SetProperty(p => p.Usua_Modifica, request.Usuario), cancellationToken);
                if (actualizados == 0)
                    return await DiagnosticarPrecargueAsync(pcdId, cancellationToken)
                        ?? PrecargueRollosResult.Conflict($"No fue posible anular el precargue N° {pcdId}, intente de nuevo.");

                var rollosLiberados = await _context.Set<Produccion_Procesos>()
                    .Where(pp => pp.Estado_Rollo == (int)EstadoEnum.Precargado &&
                                 ProcesosDespacho.Contains(pp.Proceso_Id) &&
                                 _context.Set<Detalles_PrecargueDespacho>().Any(d => d.Pcd_Id == pcdId &&
                                                                                    d.DtlPcd_Rollo == pp.NumeroRollo_BagPro &&
                                                                                    d.Prod_Id == pp.Prod_Id))
                    .ExecuteUpdateAsync(s => s.SetProperty(pp => pp.Estado_Rollo, (int)EstadoEnum.Disponible), cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return PrecargueRollosResult.Success(200, new { pcd_Id = pcdId, rollosLiberados });
            });
        }

        /// <summary>
        /// Consulta un precargue con todos sus rollos, del más reciente al más antiguo.
        /// </summary>
        /// <param name="pcdId">Id del precargue.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>200 con el precargue; 404 si no existe.</returns>
        public async Task<PrecargueRollosResult> ObtenerAsync(long pcdId, CancellationToken cancellationToken)
        {
            var encabezado = await _context.Set<Precargue_Despacho>().AsNoTracking()
                .Where(p => p.Pcd_Id == pcdId)
                .Select(p => new
                {
                    p.Cli_Id,
                    Cliente = p.Cliente!.Cli_Nombre,
                    p.Usua_Vendedor,
                    p.Estado_Id,
                    Estado = p.Estados!.Estado_Nombre,
                    p.Pcd_FechaCrea,
                    p.Pcd_HoraCrea,
                    p.Usua_Crea,
                    p.Pcd_Observacion,
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (encabezado == null)
                return PrecargueRollosResult.NotFound($"No existe el precargue N° {pcdId}.");

            var rollos = await ConsultarRollosAsync(pcdId, null, cancellationToken);
            return PrecargueRollosResult.Success(200, new PrecargueRollosDto(pcdId, encabezado.Cli_Id, encabezado.Cliente, encabezado.Usua_Vendedor,
                encabezado.Estado_Id, encabezado.Estado, encabezado.Pcd_FechaCrea, encabezado.Pcd_HoraCrea, encabezado.Usua_Crea,
                encabezado.Pcd_Observacion, rollos.Count, rollos));
        }

        /// <summary>
        /// Lista los precargues en proceso creados por un usuario, para que pueda retomarlos.
        /// </summary>
        /// <param name="usuario">Usuario que creó los precargues.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Precargues en proceso del usuario, del más antiguo al más reciente.</returns>
        public async Task<List<PrecargueEnProcesoDto>> ListarEnProcesoAsync(long usuario, CancellationToken cancellationToken)
        {
            return await _context.Set<Precargue_Despacho>().AsNoTracking()
                .Where(p => p.Usua_Crea == usuario && p.Estado_Id == (int)EstadoEnum.EnProceso)
                .OrderBy(p => p.Pcd_Id)
                .Select(p => new PrecargueEnProcesoDto(p.Pcd_Id, p.Cli_Id, p.Cliente!.Cli_Nombre, p.Pcd_FechaCrea, p.Pcd_HoraCrea,
                    _context.Set<Detalles_PrecargueDespacho>().Count(d => d.Pcd_Id == p.Pcd_Id)))
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Bloquea la fila del precargue hasta el fin de la transacción, verificando que siga en proceso, para que no se
        /// finalice ni anule mientras se le agregan o quitan rollos.
        /// </summary>
        /// <returns>null si el precargue está en proceso; en otro caso, el resultado de error a devolver.</returns>
        private async Task<PrecargueRollosResult?> BloquearPrecargueEnProcesoAsync(long pcdId, CancellationToken cancellationToken)
        {
            var bloqueados = await _context.Set<Precargue_Despacho>()
                .Where(p => p.Pcd_Id == pcdId && p.Estado_Id == (int)EstadoEnum.EnProceso)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.Estado_Id, p => p.Estado_Id), cancellationToken);
            return bloqueados > 0 ? null : await DiagnosticarPrecargueAsync(pcdId, cancellationToken);
        }

        /// <summary>
        /// Explica por qué un precargue no admite cambios.
        /// </summary>
        /// <returns>404 si no existe, 409 si no está en proceso, o null si sí está en proceso.</returns>
        private async Task<PrecargueRollosResult?> DiagnosticarPrecargueAsync(long pcdId, CancellationToken cancellationToken)
        {
            var precargue = await _context.Set<Precargue_Despacho>().AsNoTracking()
                .Where(p => p.Pcd_Id == pcdId)
                .Select(p => new { p.Estado_Id, Estado = p.Estados!.Estado_Nombre })
                .FirstOrDefaultAsync(cancellationToken);

            if (precargue == null)
                return PrecargueRollosResult.NotFound($"No existe el precargue N° {pcdId}.");
            if (precargue.Estado_Id != (int)EstadoEnum.EnProceso)
                return PrecargueRollosResult.Conflict($"El precargue N° {pcdId} está en estado {precargue.Estado} y ya no se puede modificar.");
            return null;
        }

        /// <summary>
        /// Explica por qué un rollo no se puede precargar: si ya está en otro precargue activo lo indica; si no, lo reporta como no disponible.
        /// </summary>
        private async Task<PrecargueRollosResult> DiagnosticarRolloNoDisponibleAsync(long rollo, CancellationToken cancellationToken)
        {
            var precargueActivo = await (from d in _context.Set<Detalles_PrecargueDespacho>().AsNoTracking()
                                         join p in _context.Set<Precargue_Despacho>() on d.Pcd_Id equals p.Pcd_Id
                                         where d.DtlPcd_Rollo == rollo &&
                                               (p.Estado_Id == (int)EstadoEnum.EnProceso || p.Estado_Id == (int)EstadoEnum.Pendiente)
                                         orderby p.Pcd_Id descending
                                         select (long?)p.Pcd_Id).FirstOrDefaultAsync(cancellationToken);

            return precargueActivo != null
                ? PrecargueRollosResult.Conflict($"El rollo/bulto N° {rollo} ya está en el precargue N° {precargueActivo}.")
                : PrecargueRollosResult.NotFound($"El rollo/bulto N° {rollo} no se encuentra disponible para el cliente seleccionado.");
        }

        /// <summary>
        /// Consulta los rollos de un precargue (o uno solo, si se indica el número de rollo), del más reciente al más antiguo.
        /// Usa los datos guardados en el detalle y completa los que falten con la información de producción.
        /// </summary>
        private async Task<List<RolloPrecargueDto>> ConsultarRollosAsync(long pcdId, long? rollo, CancellationToken cancellationToken)
        {
            var detalles = await _context.Set<Detalles_PrecargueDespacho>().AsNoTracking()
                .Where(d => d.Pcd_Id == pcdId && (rollo == null || d.DtlPcd_Rollo == rollo))
                .OrderByDescending(d => d.DtlPcd_Codigo)
                .Select(d => new
                {
                    d.DtlPcd_Rollo,
                    d.Prod_Id,
                    Referencia = d.DtlPcd_Referencia ?? d.Producto!.Prod_Nombre,
                    d.DtlPcd_Cantidad,
                    d.UndMed_Id,
                    d.DtlPcd_OT,
                    d.DtlPcd_PesoBruto,
                    d.DtlPcd_PesoNeto,
                    d.DtlPcd_Ubicacion,
                })
                .ToListAsync(cancellationToken);
            if (detalles.Count == 0) return new();

            var rollos = detalles.Select(d => d.DtlPcd_Rollo).Distinct().ToList();
            var produccion = (await _context.Set<Produccion_Procesos>().AsNoTracking()
                .Where(pp => rollos.Contains(pp.NumeroRollo_BagPro) && ProcesosDespacho.Contains(pp.Proceso_Id))
                .OrderByDescending(pp => pp.Id)
                .Select(pp => new
                {
                    pp.NumeroRollo_BagPro,
                    pp.Prod_Id,
                    pp.Cli_Id,
                    Cliente = pp.Clientes!.Cli_Nombre,
                    pp.OT,
                    pp.Peso_Bruto,
                    pp.Peso_Neto,
                    pp.Proceso_Id,
                    Proceso = pp.Proceso!.Proceso_Nombre,
                })
                .ToListAsync(cancellationToken))
                .GroupBy(pp => (pp.NumeroRollo_BagPro, pp.Prod_Id))
                .ToDictionary(g => g.Key, g => g.First());

            return detalles.Select(d =>
            {
                produccion.TryGetValue((d.DtlPcd_Rollo, d.Prod_Id), out var pp);
                return new RolloPrecargueDto(d.DtlPcd_Rollo, d.Prod_Id, d.Referencia, pp?.Cli_Id ?? 0, pp?.Cliente, d.DtlPcd_Cantidad,
                    d.DtlPcd_PesoBruto ?? pp?.Peso_Bruto, d.DtlPcd_PesoNeto ?? pp?.Peso_Neto, d.UndMed_Id, d.DtlPcd_OT ?? pp?.OT,
                    pp?.Proceso_Id, pp?.Proceso, d.DtlPcd_Ubicacion);
            }).ToList();
        }

        private Task<int> ContarRollosAsync(long pcdId, CancellationToken cancellationToken) =>
            _context.Set<Detalles_PrecargueDespacho>().CountAsync(d => d.Pcd_Id == pcdId, cancellationToken);

        private static (DateTime Fecha, string Hora) FechaHoraActual()
        {
            var ahora = DateTime.Now;
            return (ahora.Date, ahora.ToString("HH:mm:ss"));
        }

        private static string? Recortar(string? valor, int longitud) =>
            valor == null || valor.Length <= longitud ? valor : valor[..longitud];
    }

    /// <summary>
    /// Resultado de una operación sobre un precargue de rollos.
    /// </summary>
    /// <param name="Succeeded">Indica si la operación se completó.</param>
    /// <param name="StatusCode">Código HTTP asociado al resultado.</param>
    /// <param name="Message">Mensaje descriptivo del error, si lo hubo.</param>
    /// <param name="Data">Datos a devolver si la operación fue exitosa.</param>
    public sealed record PrecargueRollosResult(bool Succeeded, int StatusCode, string Message, object? Data)
    {
        /// <summary>Crea un resultado exitoso.</summary>
        public static PrecargueRollosResult Success(int statusCode, object data) => new(true, statusCode, string.Empty, data);

        /// <summary>Crea un resultado por recurso inexistente (404).</summary>
        public static PrecargueRollosResult NotFound(string message) => new(false, 404, message, null);

        /// <summary>Crea un resultado por conflicto con el estado actual de los datos (409).</summary>
        public static PrecargueRollosResult Conflict(string message) => new(false, 409, message, null);
    }
}
