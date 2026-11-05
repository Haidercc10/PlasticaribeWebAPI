using System.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Registra asignaciones de materia prima y tintas en una sola transacción: encabezado, detalles,
    /// consumo de inventario por lotes (FIFO), estado de la solicitud atendida y autorización de exceso usada.
    /// </summary>
    public sealed class AsignacionMateriaPrimaService
    {
        /// <summary>Materia prima "sin asignar" que no puede asignarse.</summary>
        public const long MateriaPrimaSinAsignar = 84;

        /// <summary>Tinta "sin asignar" que no puede asignarse.</summary>
        public const long TintaSinAsignar = 2001;

        private const int EstadoAsignacion = 13;
        private const int EstadoOrdenTrabajoAsignada = 14;

        private readonly dataContext _context;
        private readonly ConsumoInventarioAsignacionService _consumo;
        private readonly SolicitudesMaterialService _solicitudes;

        /// <summary>
        /// Inicializa el servicio con sus dependencias.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        /// <param name="consumo">Servicio que descuenta el inventario.</param>
        /// <param name="solicitudes">Reglas de solicitudes de material y consumo de la OT.</param>
        public AsignacionMateriaPrimaService(
            dataContext context,
            ConsumoInventarioAsignacionService consumo,
            SolicitudesMaterialService solicitudes)
        {
            _context = context;
            _consumo = consumo;
            _solicitudes = solicitudes;
        }

        /// <summary>
        /// Calcula los kg que superan el límite de la OT. Si la asignación no suma kg al límite
        /// (solo tintas o materiales excluidos) no hay exceso, aunque la OT ya esté por encima del límite.
        /// </summary>
        /// <param name="consumoActual">Kg netos ya consumidos por la OT.</param>
        /// <param name="kgNuevos">Kg de la asignación que cuentan para el límite.</param>
        /// <param name="limite">Kg máximos de la OT.</param>
        /// <returns>Kg en exceso; cero si no supera el límite.</returns>
        public static decimal CalcularExcesoKg(decimal consumoActual, decimal kgNuevos, decimal limite) =>
            kgNuevos <= 0m ? 0m : Math.Max(0m, consumoActual + kgNuevos - limite);

        /// <summary>
        /// Registra la asignación completa o no registra nada.
        /// </summary>
        /// <param name="request">Encabezado y materiales de la asignación.</param>
        /// <param name="usuarioId">Usuario que asigna, tomado del token.</param>
        /// <param name="puedeExcederLimite">Indica si el usuario tiene permiso para superar el límite de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Resultado con el código HTTP, el mensaje y, si aplica, la asignación creada o el exceso a autorizar.</returns>
        public async Task<RegistrarAsignacionResult> RegistrarAsync(
            RegistrarAsignacionRequest request,
            long usuarioId,
            bool puedeExcederLimite,
            CancellationToken cancellationToken)
        {
            var errorValidacion = ValidarMateriales(request.Materiales);
            if (errorValidacion != null) return RegistrarAsignacionResult.Invalid(errorValidacion);

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                _context.ChangeTracker.Clear();
                await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

                var procesos = request.Materiales.Select(m => m.ProcesoId).Distinct().ToList();
                var procesosValidos = await _context.Procesos.AsNoTracking().CountAsync(p => procesos.Contains(p.Proceso_Id), cancellationToken);
                if (procesosValidos != procesos.Count)
                    return RegistrarAsignacionResult.Invalid("Uno o más procesos no existen.");

                var materiales = await CargarMaterialesAsync(request.Materiales, cancellationToken);
                if (materiales.Count != request.Materiales.Count)
                    return RegistrarAsignacionResult.NotFound("Uno o más materiales no existen.");

                var solicitudId = request.SolicitudId ?? EstadosSolicitudMaterial.SolicitudGenericaId;
                var esSolicitudReal = solicitudId != EstadosSolicitudMaterial.SolicitudGenericaId;
                if (esSolicitudReal)
                {
                    var errorSolicitud = await ValidarContraSolicitudAsync(solicitudId, request, materiales, cancellationToken);
                    if (errorSolicitud != null) return errorSolicitud;
                }

                var kgNuevos = request.Materiales
                    .Where(m => m.TipoMaterial == TipoMaterialInventario.MateriaPrima &&
                        materiales[(m.TipoMaterial, m.MaterialId)].CatMP_Id != SolicitudesMaterialService.CategoriaExcluidaLimiteOT)
                    .Sum(m => m.Cantidad);
                var consumoActual = await _solicitudes.CalcularConsumoOTAsync(request.OrdenTrabajo, cancellationToken);
                var exceso = CalcularExcesoKg(consumoActual, kgNuevos, request.LimiteKgOT);

                long? autorizacionId = null;
                if (exceso > 0m && !(puedeExcederLimite && request.ConfirmarExceso))
                {
                    autorizacionId = await _context.AutorizacionesExcesoOT
                        .AsNoTracking()
                        .Where(a => a.AutEx_OrdenTrabajo == request.OrdenTrabajo &&
                            a.Estado_Id == (int)EstadoAutorizacionExcesoOT.ACEPTADO &&
                            a.AsigMp_Id == null &&
                            a.AutEx_CantidadKg >= exceso)
                        .OrderBy(a => a.AutEx_CantidadKg).ThenBy(a => a.AutEx_Id)
                        .Select(a => (long?)a.AutEx_Id)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (autorizacionId == null)
                        return new RegistrarAsignacionResult(
                            409,
                            puedeExcederLimite
                                ? $"La asignación supera en {exceso.ToString("N2", CultureInfo.InvariantCulture)} kg el límite de la OT {request.OrdenTrabajo}. Confirme para continuar."
                                : $"La asignación supera en {exceso.ToString("N2", CultureInfo.InvariantCulture)} kg el límite de la OT {request.OrdenTrabajo} y no hay una autorización aprobada que lo cubra.",
                            RequiereAutorizacion: true,
                            ExcesoKg: exceso);
                }

                var ahora = DateTime.Now;
                var asignacion = new Asignacion_MatPrima
                {
                    AsigMP_OrdenTrabajo = request.OrdenTrabajo,
                    AsigMp_FechaEntrega = ahora.Date,
                    AsigMp_Hora = ahora.ToString("H:mm:ss", CultureInfo.InvariantCulture),
                    AsigMp_Observacion = request.Observacion,
                    Usua_Id = usuarioId,
                    Estado_Id = EstadoAsignacion,
                    AsigMp_Maquina = request.Maquina,
                    Estado_OrdenTrabajo = EstadoOrdenTrabajoAsignada,
                    AsigMp_FechaRealEntrega = request.FechaRealEntrega!.Value.Date,
                    SolMpExt_Id = solicitudId
                };
                _context.Asignaciones_MatPrima.Add(asignacion);
                await _context.SaveChangesAsync(cancellationToken);

                AgregarDetalles(asignacion.AsigMp_Id, request.Materiales, materiales);
                await _context.SaveChangesAsync(cancellationToken);

                var porMaterial = request.Materiales.ToDictionary(m => (m.TipoMaterial, m.MaterialId), m => m.Cantidad);
                var (lotes, errorLotes) = await _consumo.AsignarLotesFifoAsync(porMaterial, cancellationToken);
                if (lotes == null) return RegistrarAsignacionResult.Conflict(errorLotes!);

                var errorConsumo = await _consumo.AplicarConsumoAsync(
                    asignacion,
                    lotes,
                    new DatosSalidaOT(request.ProductoId, request.CantidadPedidaOT, request.UnidadMedidaOT),
                    cancellationToken);
                if (errorConsumo != null) return new RegistrarAsignacionResult(errorConsumo.StatusCode, errorConsumo.Message);

                int? estadoSolicitud = esSolicitudReal
                    ? await _solicitudes.RecalcularEstadoAsync(solicitudId, cancellationToken)
                    : null;

                if (autorizacionId != null && !await MarcarAutorizacionProcesadaAsync(autorizacionId.Value, asignacion.AsigMp_Id, cancellationToken))
                    return RegistrarAsignacionResult.Conflict("La autorización de exceso fue usada por otra operación. Intente de nuevo.");

                await transaction.CommitAsync(cancellationToken);
                return new RegistrarAsignacionResult(200, "Asignación registrada.", asignacion.AsigMp_Id, estadoSolicitud);
            });
        }

        /// <summary>
        /// Valida tipos, decimales, duplicados y materiales comodín.
        /// </summary>
        /// <param name="materiales">Materiales de la petición.</param>
        /// <returns>El mensaje de error, o null si son válidos.</returns>
        private static string? ValidarMateriales(IReadOnlyCollection<RegistrarAsignacionMaterialRequest> materiales)
        {
            if (materiales.Any(m => !Enum.IsDefined(m.TipoMaterial)))
                return "Tipo de material no válido.";
            if (materiales.Any(m => decimal.Round(m.Cantidad, 2) != m.Cantidad))
                return "Las cantidades deben tener máximo dos decimales.";
            if (materiales.Select(m => (m.TipoMaterial, m.MaterialId)).Distinct().Count() != materiales.Count)
                return "No se puede repetir un material en la misma asignación.";
            if (materiales.Any(m =>
                    (m.TipoMaterial == TipoMaterialInventario.MateriaPrima && m.MaterialId == MateriaPrimaSinAsignar) ||
                    (m.TipoMaterial == TipoMaterialInventario.Tinta && m.MaterialId == TintaSinAsignar)))
                return "No se puede asignar el material genérico sin asignar.";
            return null;
        }

        /// <summary>
        /// Comprueba que la solicitud esté activa, sea de la misma OT y que cada material pertenezca a una subcategoría
        /// solicitada sin superar lo que falta por entregar.
        /// </summary>
        private async Task<RegistrarAsignacionResult?> ValidarContraSolicitudAsync(
            long solicitudId,
            RegistrarAsignacionRequest request,
            IReadOnlyDictionary<(TipoMaterialInventario, long), MaterialAsignable> materiales,
            CancellationToken cancellationToken)
        {
            var solicitud = await _context.Solicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(s => s.SolMpExt_Id == solicitudId)
                .Select(s => new { s.SolMpExt_OT, s.Estado_Id })
                .FirstOrDefaultAsync(cancellationToken);
            if (solicitud == null)
                return RegistrarAsignacionResult.NotFound($"La solicitud N° {solicitudId} no existe.");
            if (EstadosSolicitudMaterial.EstaCerrada(solicitud.Estado_Id))
                return RegistrarAsignacionResult.Conflict($"La solicitud N° {solicitudId} está finalizada o cancelada.");
            if (solicitud.SolMpExt_OT != request.OrdenTrabajo)
                return RegistrarAsignacionResult.Invalid($"La solicitud N° {solicitudId} pertenece a la OT {solicitud.SolMpExt_OT}.");

            var faltantes = (await _solicitudes.ObtenerCantidadesAsync(solicitudId, cancellationToken))
                .ToDictionary(c => c.SubCatMP_Id, c => c.Faltante);

            foreach (var grupo in request.Materiales.GroupBy(m => materiales[(m.TipoMaterial, m.MaterialId)].SubCatMP_Id))
            {
                if (grupo.Key == null || !faltantes.TryGetValue(grupo.Key.Value, out var faltante))
                {
                    var nombre = materiales[(grupo.First().TipoMaterial, grupo.First().MaterialId)].Nombre;
                    return RegistrarAsignacionResult.Conflict($"El material {nombre} no pertenece a una subcategoría de la solicitud N° {solicitudId}.");
                }

                var cantidad = grupo.Sum(m => m.Cantidad);
                if (cantidad > faltante)
                {
                    var subcategoria = await _context.Subcategorias_MatPrima
                        .AsNoTracking()
                        .Where(s => s.SubCatMP_Id == grupo.Key.Value)
                        .Select(s => s.SubCatMP_Nombre)
                        .FirstOrDefaultAsync(cancellationToken);
                    return RegistrarAsignacionResult.Conflict(
                        $"La cantidad asignada a la subcategoría {subcategoria?.Trim() ?? grupo.Key.ToString()} ({cantidad.ToString("N2", CultureInfo.InvariantCulture)}) supera lo que falta por entregar ({faltante.ToString("N2", CultureInfo.InvariantCulture)}).");
                }
            }

            return null;
        }

        /// <summary>
        /// Lee de la base de datos los datos de cada material (nombre, precio, unidad, categoría y subcategoría).
        /// </summary>
        private async Task<Dictionary<(TipoMaterialInventario, long), MaterialAsignable>> CargarMaterialesAsync(
            IReadOnlyCollection<RegistrarAsignacionMaterialRequest> solicitados,
            CancellationToken cancellationToken)
        {
            var mpIds = solicitados.Where(m => m.TipoMaterial == TipoMaterialInventario.MateriaPrima).Select(m => m.MaterialId).ToList();
            var tintaIds = solicitados.Where(m => m.TipoMaterial == TipoMaterialInventario.Tinta).Select(m => m.MaterialId).ToList();

            var materiasPrimas = await _context.Materias_Primas
                .AsNoTracking()
                .Where(m => mpIds.Contains(m.MatPri_Id))
                .Select(m => new MaterialAsignable(TipoMaterialInventario.MateriaPrima, m.MatPri_Id, m.MatPri_Nombre, m.MatPri_Precio, m.UndMed_Id, m.CatMP_Id, m.SubCatMP_Id))
                .ToListAsync(cancellationToken);
            var tintas = await _context.Tintas
                .AsNoTracking()
                .Where(t => tintaIds.Contains(t.Tinta_Id))
                .Select(t => new MaterialAsignable(TipoMaterialInventario.Tinta, t.Tinta_Id, t.Tinta_Nombre, t.Tinta_Precio, t.UndMed_Id, t.CatMP_Id, t.SubCatMP_Id))
                .ToListAsync(cancellationToken);

            return materiasPrimas.Concat(tintas).ToDictionary(m => (m.Tipo, m.Id));
        }

        /// <summary>
        /// Agrega los detalles de materia prima y tintas con el precio vigente de cada material.
        /// </summary>
        private void AgregarDetalles(
            long asignacionId,
            IEnumerable<RegistrarAsignacionMaterialRequest> solicitados,
            IReadOnlyDictionary<(TipoMaterialInventario, long), MaterialAsignable> materiales)
        {
            foreach (var item in solicitados)
            {
                var material = materiales[(item.TipoMaterial, item.MaterialId)];
                if (item.TipoMaterial == TipoMaterialInventario.MateriaPrima)
                {
                    _context.DetallesAsignaciones_MateriasPrimas.Add(new DetalleAsignacion_MateriaPrima
                    {
                        AsigMp_Id = asignacionId,
                        MatPri_Id = material.Id,
                        DtAsigMp_Cantidad = item.Cantidad,
                        UndMed_Id = material.UndMed_Id,
                        Proceso_Id = item.ProcesoId,
                        DtAsigMp_Precio = material.Precio,
                        DtAsigMp_Subtotal = material.Precio * item.Cantidad
                    });
                }
                else
                {
                    _context.DetalleAsignaciones_Tintas.Add(new DetalleAsignacion_Tinta
                    {
                        AsigMp_Id = asignacionId,
                        Tinta_Id = material.Id,
                        DtAsigTinta_Cantidad = item.Cantidad,
                        UndMed_Id = material.UndMed_Id,
                        Proceso_Id = item.ProcesoId,
                        DtAsigTinta_Precio = material.Precio,
                        DtAsigTinta_Subtotal = material.Precio * item.Cantidad
                    });
                }
            }
        }

        /// <summary>
        /// Marca la autorización como procesada y la vincula a la asignación, solo si sigue aceptada y sin usar.
        /// </summary>
        /// <returns>True si se marcó; false si otra operación la usó primero.</returns>
        private async Task<bool> MarcarAutorizacionProcesadaAsync(long autorizacionId, long asignacionId, CancellationToken cancellationToken)
        {
            var ahoraUtc = DateTime.UtcNow;
            var horaUtc = ahoraUtc.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            var actualizadas = await _context.AutorizacionesExcesoOT
                .Where(a => a.AutEx_Id == autorizacionId &&
                    a.Estado_Id == (int)EstadoAutorizacionExcesoOT.ACEPTADO &&
                    a.AsigMp_Id == null)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(a => a.Estado_Id, (int)EstadoAutorizacionExcesoOT.PROCESADO)
                    .SetProperty(a => a.AsigMp_Id, asignacionId)
                    .SetProperty(a => a.AutEx_FechaConsumoUtc, ahoraUtc)
                    .SetProperty(a => a.AutEx_HoraConsumoUtc, horaUtc), cancellationToken);
            return actualizadas > 0;
        }

        /// <summary>Datos de un material necesarios para asignarlo.</summary>
        private sealed record MaterialAsignable(
            TipoMaterialInventario Tipo,
            long Id,
            string Nombre,
            decimal Precio,
            string UndMed_Id,
            int CatMP_Id,
            int? SubCatMP_Id);
    }
}
