using System.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Lote de entrada y cantidad que se descuenta de él para un material de una asignación.
    /// </summary>
    /// <param name="TipoMaterial">Tipo del material.</param>
    /// <param name="MaterialId">Materia prima o tinta.</param>
    /// <param name="Lote">Lote de entrada leído antes del descuento.</param>
    /// <param name="Cantidad">Cantidad a descontar del lote.</param>
    public sealed record LoteAsignado(TipoMaterialInventario TipoMaterial, long MaterialId, Movimientros_Entradas_MP Lote, decimal Cantidad);

    /// <summary>
    /// Datos de la OT que se registran en cada salida de inventario.
    /// </summary>
    /// <param name="ProductoId">Producto (ítem) de la OT.</param>
    /// <param name="CantidadPedidaOT">Cantidad pedida en la OT.</param>
    /// <param name="UnidadMedidaOT">Unidad de la cantidad pedida.</param>
    public sealed record DatosSalidaOT(int ProductoId, decimal CantidadPedidaOT, string UnidadMedidaOT);

    /// <summary>
    /// Registra las salidas de inventario (materia prima y tintas) de una asignación: descuenta stock y lotes
    /// y crea los movimientos de salida.
    /// </summary>
    public sealed class ConsumoInventarioAsignacionService
    {
        /// <summary>Estado de lote con saldo disponible.</summary>
        public const int EstadoLoteDisponible = 19;

        /// <summary>Estado de lote agotado.</summary>
        public const int EstadoLoteAgotado = 5;

        private readonly dataContext _context;

        /// <summary>
        /// Inicializa el servicio con el contexto de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        public ConsumoInventarioAsignacionService(dataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Descuenta el stock y los lotes indicados de una asignación ya guardada y registra sus salidas,
        /// en una transacción serializable. Rechaza asignaciones inexistentes, ya consumidas, cantidades que no
        /// coincidan con los detalles guardados, lotes no disponibles o sin saldo suficiente.
        /// </summary>
        /// <param name="request">Asignación, datos de la OT y lotes a consumir.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Resultado con el código HTTP, el mensaje y la cantidad de movimientos creados.</returns>
        public async Task<ConsumoInventarioAsignacionResult> ConsumirAsync(
            ConsumoInventarioAsignacionRequest request,
            CancellationToken cancellationToken)
        {
            if (request.Lotes.Any(l => !Enum.IsDefined(l.TipoMaterial)))
                return ConsumoInventarioAsignacionResult.Invalid("Tipo de material no válido.");

            if (request.Lotes.Any(l => decimal.Round(l.Cantidad, 2) != l.Cantidad))
                return ConsumoInventarioAsignacionResult.Invalid("Las cantidades deben tener máximo dos decimales.");

            if (request.Lotes.Select(l => l.LoteId).Distinct().Count() != request.Lotes.Count)
                return ConsumoInventarioAsignacionResult.Invalid("No se puede repetir un lote en la misma operación.");

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                _context.ChangeTracker.Clear();
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    IsolationLevel.Serializable, cancellationToken);

                var assignment = await _context.Asignaciones_MatPrima
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.AsigMp_Id == request.AsignacionId, cancellationToken);
                if (assignment == null)
                    return ConsumoInventarioAsignacionResult.NotFound("No existe la asignación indicada.");

                var existingMovements = await _context.Entradas_Salidas_MP
                    .AnyAsync(m => m.Codigo_Salida == (int)assignment.AsigMp_Id &&
                        (m.Tipo_Salida == "ASIGMP" || m.Tipo_Salida == "ASIGTINTAS"), cancellationToken);
                if (existingMovements)
                    return ConsumoInventarioAsignacionResult.Conflict("La asignación ya tiene salidas de inventario registradas.");

                var lotIds = request.Lotes.Select(l => l.LoteId).ToList();
                var lots = await _context.Movimientros_Entradas_MP
                    .AsNoTracking()
                    .Where(l => lotIds.Contains(l.Id))
                    .ToDictionaryAsync(l => l.Id, cancellationToken);

                if (lots.Count != request.Lotes.Count)
                    return ConsumoInventarioAsignacionResult.NotFound("Uno o más lotes no existen.");

                var requestedByMaterial = request.Lotes
                    .GroupBy(l => (l.TipoMaterial, l.MaterialId))
                    .ToDictionary(g => g.Key, g => g.Sum(l => l.Cantidad));

                if (!await CoincideConDetallesAsignados(request.AsignacionId, requestedByMaterial, cancellationToken))
                    return ConsumoInventarioAsignacionResult.Conflict("Las cantidades por material no coinciden con los detalles de la asignación.");

                var allocations = request.Lotes
                    .Select(l => new LoteAsignado(l.TipoMaterial, l.MaterialId, lots[l.LoteId], l.Cantidad))
                    .ToList();

                var error = await AplicarConsumoAsync(
                    assignment,
                    allocations,
                    new DatosSalidaOT(request.ProductoId, request.CantidadPedidaOT, request.UnidadMedidaOT),
                    cancellationToken);
                if (error != null) return error;

                await transaction.CommitAsync(cancellationToken);
                return ConsumoInventarioAsignacionResult.Success(allocations.Count);
            });
        }

        /// <summary>
        /// Selecciona, en orden de entrada (FIFO), los lotes disponibles que cubren la cantidad de cada material.
        /// </summary>
        /// <param name="materiales">Cantidad total por tipo y material.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Los lotes asignados, o el mensaje de error si algún material no tiene lotes suficientes.</returns>
        public async Task<(List<LoteAsignado>? Lotes, string? Error)> AsignarLotesFifoAsync(
            IReadOnlyDictionary<(TipoMaterialInventario TipoMaterial, long MaterialId), decimal> materiales,
            CancellationToken cancellationToken)
        {
            var mpIds = materiales.Keys.Where(k => k.TipoMaterial == TipoMaterialInventario.MateriaPrima).Select(k => k.MaterialId).ToList();
            var tintaIds = materiales.Keys.Where(k => k.TipoMaterial == TipoMaterialInventario.Tinta).Select(k => k.MaterialId).ToList();

            var lotes = await _context.Movimientros_Entradas_MP
                .AsNoTracking()
                .Where(l => l.Estado_Id == EstadoLoteDisponible && l.Cantidad_Disponible > 0 &&
                    (mpIds.Contains(l.MatPri_Id) || tintaIds.Contains(l.Tinta_Id)))
                .OrderBy(l => l.Id)
                .ToListAsync(cancellationToken);

            var resultado = new List<LoteAsignado>();
            foreach (var ((tipo, materialId), cantidad) in materiales)
            {
                var candidatos = lotes
                    .Where(l => tipo == TipoMaterialInventario.MateriaPrima ? l.MatPri_Id == materialId : l.Tinta_Id == materialId)
                    .ToList();
                var reparto = RepartirFifo(candidatos.Select(l => (l.Id, l.Cantidad_Disponible)).ToList(), cantidad);
                if (reparto == null)
                    return (null, $"Los lotes disponibles de {await ObtenerNombreMaterialAsync(tipo, materialId, cancellationToken)} no alcanzan para cubrir {cantidad.ToString("N2", CultureInfo.InvariantCulture)}.");

                var porId = candidatos.ToDictionary(l => l.Id);
                resultado.AddRange(reparto.Select(r => new LoteAsignado(tipo, materialId, porId[r.LoteId], r.Cantidad)));
            }

            return (resultado, null);
        }

        /// <summary>
        /// Reparte una cantidad entre lotes en el orden recibido, tomando de cada uno hasta su disponible.
        /// </summary>
        /// <param name="lotes">Identificador y disponible de cada lote, en orden FIFO.</param>
        /// <param name="cantidad">Cantidad a cubrir.</param>
        /// <returns>Cantidad tomada de cada lote, o null si la suma de los disponibles no alcanza.</returns>
        public static List<(int LoteId, decimal Cantidad)>? RepartirFifo(IReadOnlyList<(int LoteId, decimal Disponible)> lotes, decimal cantidad)
        {
            var resultado = new List<(int LoteId, decimal Cantidad)>();
            var restante = cantidad;
            foreach (var (loteId, disponible) in lotes)
            {
                if (restante <= 0m) break;
                if (disponible <= 0m) continue;

                var tomado = Math.Min(disponible, restante);
                resultado.Add((loteId, tomado));
                restante -= tomado;
            }

            return restante > 0m ? null : resultado;
        }

        /// <summary>
        /// Descuenta el stock de cada material, el saldo de cada lote y crea las salidas de inventario de la asignación.
        /// No abre transacción: debe ejecutarse dentro de la transacción del llamador, que hace rollback si devuelve un error.
        /// </summary>
        /// <param name="assignment">Asignación ya guardada.</param>
        /// <param name="allocations">Lotes y cantidades a descontar.</param>
        /// <param name="datosOT">Datos de la OT para las salidas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Null si todo se aplicó; de lo contrario, el resultado con el error.</returns>
        public async Task<ConsumoInventarioAsignacionResult?> AplicarConsumoAsync(
            Asignacion_MatPrima assignment,
            IReadOnlyList<LoteAsignado> allocations,
            DatosSalidaOT datosOT,
            CancellationToken cancellationToken)
        {
            if (assignment.AsigMp_Id > int.MaxValue)
                return ConsumoInventarioAsignacionResult.Invalid("El identificador de asignación excede el rango admitido por los movimientos de salida.");

            foreach (var allocation in allocations)
            {
                var lot = allocation.Lote;
                var matchesMaterial = allocation.TipoMaterial switch
                {
                    TipoMaterialInventario.MateriaPrima => lot.MatPri_Id == allocation.MaterialId,
                    TipoMaterialInventario.Tinta => lot.Tinta_Id == allocation.MaterialId,
                    _ => false
                };

                if (!matchesMaterial || lot.Estado_Id != EstadoLoteDisponible || lot.Cantidad_Disponible < allocation.Cantidad)
                    return ConsumoInventarioAsignacionResult.Conflict($"El lote {lot.Id} no tiene existencia disponible suficiente de {await ObtenerNombreMaterialAsync(allocation.TipoMaterial, allocation.MaterialId, cancellationToken)}.");
            }

            var porMaterial = allocations
                .GroupBy(a => (a.TipoMaterial, a.MaterialId))
                .OrderBy(g => g.Key.TipoMaterial).ThenBy(g => g.Key.MaterialId);
            foreach (var material in porMaterial)
            {
                var cantidad = material.Sum(a => a.Cantidad);
                var updated = material.Key.TipoMaterial switch
                {
                    TipoMaterialInventario.MateriaPrima => await _context.Materias_Primas
                        .Where(m => m.MatPri_Id == material.Key.MaterialId && m.MatPri_Stock >= cantidad)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(m => m.MatPri_Stock, m => m.MatPri_Stock - cantidad), cancellationToken),
                    TipoMaterialInventario.Tinta => await _context.Tintas
                        .Where(t => t.Tinta_Id == material.Key.MaterialId && t.Tinta_Stock >= cantidad)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(t => t.Tinta_Stock, t => t.Tinta_Stock - cantidad), cancellationToken),
                    _ => 0
                };

                // El filtro es por llave primaria: 0 filas significa que la condición de stock no se cumplió.
                // No se compara con 1 porque SQL Server suma al conteo las filas que afectan los triggers de la tabla.
                if (updated == 0)
                    return ConsumoInventarioAsignacionResult.Conflict($"La existencia física de {await ObtenerNombreMaterialAsync(material.Key.TipoMaterial, material.Key.MaterialId, cancellationToken)} no alcanza para completar la asignación.");
            }

            var now = DateTime.Now;
            var movements = new List<Entradas_Salidas_MP>(allocations.Count);
            foreach (var allocation in allocations.OrderBy(a => a.Lote.Id))
            {
                var lot = allocation.Lote;
                var cantidad = allocation.Cantidad;
                var availableAfter = lot.Cantidad_Disponible - cantidad;
                var updated = await _context.Movimientros_Entradas_MP
                    .Where(l => l.Id == lot.Id && l.Estado_Id == EstadoLoteDisponible && l.Cantidad_Disponible >= cantidad)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(l => l.Cantidad_Asignada, l => l.Cantidad_Asignada + cantidad)
                        .SetProperty(l => l.Cantidad_Disponible, l => l.Cantidad_Disponible - cantidad)
                        .SetProperty(l => l.Estado_Id, availableAfter == 0m ? EstadoLoteAgotado : EstadoLoteDisponible), cancellationToken);

                if (updated == 0)
                    return ConsumoInventarioAsignacionResult.Conflict($"El lote {lot.Id} de {await ObtenerNombreMaterialAsync(allocation.TipoMaterial, allocation.MaterialId, cancellationToken)} fue modificado por otra operación o ya no tiene saldo suficiente. Intente de nuevo.");

                movements.Add(new Entradas_Salidas_MP
                {
                    Id_Entrada = lot.Id,
                    Tipo_Entrada = lot.Tipo_Entrada,
                    Codigo_Entrada = lot.Codigo_Entrada,
                    Tipo_Salida = allocation.TipoMaterial == TipoMaterialInventario.MateriaPrima ? "ASIGMP" : "ASIGTINTAS",
                    Codigo_Salida = (int)assignment.AsigMp_Id,
                    MatPri_Id = lot.MatPri_Id,
                    Tinta_Id = lot.Tinta_Id,
                    Bopp_Id = lot.Bopp_Id,
                    Cantidad_Salida = cantidad,
                    Fecha_Registro = now.Date,
                    Hora_Registro = now.ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                    Orden_Trabajo = assignment.AsigMP_OrdenTrabajo,
                    Prod_Id = datosOT.ProductoId,
                    Cant_PedidaOT = datosOT.CantidadPedidaOT,
                    UndMed_Id = datosOT.UnidadMedidaOT
                });
            }

            _context.Entradas_Salidas_MP.AddRange(movements);
            await _context.SaveChangesAsync(cancellationToken);
            return null;
        }

        /// <summary>
        /// Obtiene el nombre de una materia prima o tinta para los mensajes al usuario.
        /// </summary>
        /// <param name="tipo">Tipo del material.</param>
        /// <param name="materialId">Identificador del material.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El nombre del material, o su identificador si no existe.</returns>
        private async Task<string> ObtenerNombreMaterialAsync(TipoMaterialInventario tipo, long materialId, CancellationToken cancellationToken)
        {
            var nombre = tipo == TipoMaterialInventario.Tinta
                ? await _context.Tintas.AsNoTracking().Where(t => t.Tinta_Id == materialId).Select(t => t.Tinta_Nombre).FirstOrDefaultAsync(cancellationToken)
                : await _context.Materias_Primas.AsNoTracking().Where(m => m.MatPri_Id == materialId).Select(m => m.MatPri_Nombre).FirstOrDefaultAsync(cancellationToken);
            return string.IsNullOrWhiteSpace(nombre) ? $"el material {materialId}" : nombre.Trim();
        }

        /// <summary>
        /// Comprueba que las cantidades por material solicitadas coincidan exactamente con los detalles de la asignación
        /// (materias primas y tintas).
        /// </summary>
        /// <param name="assignmentId">Identificador de la asignación.</param>
        /// <param name="requested">Cantidad total solicitada por tipo y material.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>True si las cantidades coinciden con los detalles guardados; de lo contrario, false.</returns>
        private async Task<bool> CoincideConDetallesAsignados(
            long assignmentId,
            IReadOnlyDictionary<(TipoMaterialInventario TipoMaterial, long MaterialId), decimal> requested,
            CancellationToken cancellationToken)
        {
            var materiasPrimas = await _context.DetallesAsignaciones_MateriasPrimas
                .AsNoTracking()
                .Where(d => d.AsigMp_Id == assignmentId)
                .GroupBy(d => d.MatPri_Id)
                .Select(g => new { MaterialId = g.Key, Cantidad = g.Sum(d => d.DtAsigMp_Cantidad) })
                .ToListAsync(cancellationToken);

            var tintas = await _context.DetalleAsignaciones_Tintas
                .AsNoTracking()
                .Where(d => d.AsigMp_Id == assignmentId)
                .GroupBy(d => d.Tinta_Id)
                .Select(g => new { MaterialId = g.Key, Cantidad = g.Sum(d => d.DtAsigTinta_Cantidad) })
                .ToListAsync(cancellationToken);

            var assigned = materiasPrimas
                .Select(d => (TipoMaterialInventario.MateriaPrima, d.MaterialId, d.Cantidad))
                .Concat(tintas.Select(d => (TipoMaterialInventario.Tinta, d.MaterialId, d.Cantidad)))
                .ToDictionary(d => (d.Item1, d.MaterialId), d => d.Cantidad);

            return assigned.Count == requested.Count &&
                assigned.All(item => requested.TryGetValue(item.Key, out var amount) && amount == item.Value);
        }
    }

    /// <summary>
    /// Resultado del consumo de inventario de una asignación.
    /// </summary>
    /// <param name="Succeeded">Indica si la operación se completó.</param>
    /// <param name="StatusCode">Código HTTP asociado al resultado.</param>
    /// <param name="Message">Mensaje descriptivo del resultado.</param>
    /// <param name="MovementsCreated">Cantidad de salidas registradas, si la operación fue exitosa.</param>
    public sealed record ConsumoInventarioAsignacionResult(bool Succeeded, int StatusCode, string Message, int? MovementsCreated)
    {
        /// <summary>Crea un resultado exitoso.</summary>
        /// <param name="count">Cantidad de movimientos creados.</param>
        /// <returns>Resultado con código 200.</returns>
        public static ConsumoInventarioAsignacionResult Success(int count) => new(true, 200, "Inventario actualizado.", count);

        /// <summary>Crea un resultado por solicitud no válida.</summary>
        /// <param name="message">Motivo del rechazo.</param>
        /// <returns>Resultado con código 400.</returns>
        public static ConsumoInventarioAsignacionResult Invalid(string message) => new(false, 400, message, null);

        /// <summary>Crea un resultado por recurso inexistente.</summary>
        /// <param name="message">Descripción del recurso no encontrado.</param>
        /// <returns>Resultado con código 404.</returns>
        public static ConsumoInventarioAsignacionResult NotFound(string message) => new(false, 404, message, null);

        /// <summary>Crea un resultado por conflicto con el estado actual de los datos.</summary>
        /// <param name="message">Descripción del conflicto.</param>
        /// <returns>Resultado con código 409.</returns>
        public static ConsumoInventarioAsignacionResult Conflict(string message) => new(false, 409, message, null);
    }
}
