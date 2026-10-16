using System.Data;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Registra de forma transaccional las salidas de inventario (materia prima y tintas) de una asignación.
    /// </summary>
    public sealed class ConsumoInventarioAsignacionService
    {
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
        /// Descuenta el stock y los lotes de una asignación y registra sus salidas, todo en una transacción serializable.
        /// Rechaza asignaciones inexistentes, ya consumidas, cantidades que no coincidan con los detalles guardados,
        /// lotes no disponibles o sin saldo suficiente.
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
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    IsolationLevel.Serializable, cancellationToken);

                var assignment = await _context.Asignaciones_MatPrima
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.AsigMp_Id == request.AsignacionId, cancellationToken);
                if (assignment == null)
                    return ConsumoInventarioAsignacionResult.NotFound("No existe la asignación indicada.");

                if (assignment.AsigMp_Id > int.MaxValue)
                    return ConsumoInventarioAsignacionResult.Invalid("El identificador de asignación excede el rango admitido por los movimientos de salida.");

                var existingMovements = await _context.Entradas_Salidas_MP
                    .AnyAsync(m => m.Codigo_Salida == (int)assignment.AsigMp_Id &&
                        (m.Tipo_Salida == "ASIGMP" || m.Tipo_Salida == "ASIGTINTAS"), cancellationToken);
                if (existingMovements)
                    return ConsumoInventarioAsignacionResult.Conflict("La asignación ya tiene salidas de inventario registradas.");

                var lotIds = request.Lotes.Select(l => l.LoteId).ToList();
                var lots = await _context.Movimientros_Entradas_MP
                    .AsNoTracking()
                    .Where(l => lotIds.Contains(l.Id))
                    .OrderBy(l => l.Id)
                    .ToListAsync(cancellationToken);

                if (lots.Count != request.Lotes.Count)
                    return ConsumoInventarioAsignacionResult.NotFound("Uno o más lotes no existen.");

                var requestedByMaterial = request.Lotes
                    .GroupBy(l => new { l.TipoMaterial, l.MaterialId })
                    .ToDictionary(g => (g.Key.TipoMaterial, g.Key.MaterialId), g => g.Sum(l => l.Cantidad));

                if (!await CoincideConDetallesAsignados(request.AsignacionId, requestedByMaterial, cancellationToken))
                    return ConsumoInventarioAsignacionResult.Conflict("Las cantidades por SKU no coinciden con los detalles de la asignación.");

                var requestByLotId = request.Lotes.ToDictionary(l => l.LoteId);
                foreach (var lot in lots)
                {
                    var allocation = requestByLotId[lot.Id];
                    var matchesMaterial = allocation.TipoMaterial switch
                    {
                        TipoMaterialInventario.MateriaPrima => lot.MatPri_Id == allocation.MaterialId,
                        TipoMaterialInventario.Tinta => lot.Tinta_Id == allocation.MaterialId,
                        _ => false
                    };

                    if (!matchesMaterial || lot.Estado_Id != 19 || lot.Cantidad_Disponible < allocation.Cantidad)
                        return ConsumoInventarioAsignacionResult.Conflict($"El lote {lot.Id} no tiene existencia disponible suficiente para el material solicitado.");
                }

                foreach (var material in requestedByMaterial.OrderBy(x => x.Key.TipoMaterial).ThenBy(x => x.Key.MaterialId))
                {
                    var updated = material.Key.TipoMaterial switch
                    {
                        TipoMaterialInventario.MateriaPrima => await _context.Materias_Primas
                            .Where(m => m.MatPri_Id == material.Key.MaterialId && m.MatPri_Stock >= material.Value)
                            .ExecuteUpdateAsync(setters => setters.SetProperty(m => m.MatPri_Stock, m => m.MatPri_Stock - material.Value), cancellationToken),
                        TipoMaterialInventario.Tinta => await _context.Tintas
                            .Where(t => t.Tinta_Id == material.Key.MaterialId && t.Tinta_Stock >= material.Value)
                            .ExecuteUpdateAsync(setters => setters.SetProperty(t => t.Tinta_Stock, t => t.Tinta_Stock - material.Value), cancellationToken),
                        _ => 0
                    };

                    if (updated != 1)
                        return ConsumoInventarioAsignacionResult.Conflict($"La existencia física del SKU {material.Key.MaterialId} no alcanza para completar la asignación.");
                }

                var movements = new List<Entradas_Salidas_MP>(lots.Count);
                var now = DateTime.UtcNow;
                foreach (var lot in lots)
                {
                    var allocation = requestByLotId[lot.Id];
                    var availableAfter = lot.Cantidad_Disponible - allocation.Cantidad;
                    var updated = await _context.Movimientros_Entradas_MP
                        .Where(l => l.Id == lot.Id && l.Estado_Id == 19 && l.Cantidad_Disponible >= allocation.Cantidad)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(l => l.Cantidad_Asignada, l => l.Cantidad_Asignada + allocation.Cantidad)
                            .SetProperty(l => l.Cantidad_Disponible, l => l.Cantidad_Disponible - allocation.Cantidad)
                            .SetProperty(l => l.Estado_Id, availableAfter == 0m ? 5 : 19), cancellationToken);

                    if (updated != 1)
                        return ConsumoInventarioAsignacionResult.Conflict($"El lote {lot.Id} fue modificado por otra operación o ya no tiene saldo suficiente.");

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
                        Cantidad_Salida = allocation.Cantidad,
                        Fecha_Registro = now.Date,
                        Hora_Registro = now.ToString("HH:mm:ss"),
                        Orden_Trabajo = assignment.AsigMP_OrdenTrabajo,
                        Prod_Id = request.ProductoId,
                        Cant_PedidaOT = request.CantidadPedidaOT,
                        UndMed_Id = request.UnidadMedidaOT
                    });
                }

                _context.Entradas_Salidas_MP.AddRange(movements);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return ConsumoInventarioAsignacionResult.Success(movements.Count);
            });
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
