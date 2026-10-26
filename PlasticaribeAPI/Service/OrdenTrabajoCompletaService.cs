using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Crea y edita de forma atómica una orden de trabajo con la información de extrusión, impresión, laminado y sellado/corte.
    /// </summary>
    public sealed class OrdenTrabajoCompletaService
    {
        private readonly dataContext _context;

        /// <summary>
        /// Inicializa el servicio con el contexto de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        public OrdenTrabajoCompletaService(dataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Guarda la OT y sus cuatro procesos en una sola transacción; si algún registro falla no queda nada guardado.
        /// Rechaza la solicitud si el número de OT ya existe.
        /// </summary>
        /// <param name="request">Orden de trabajo y la información de cada proceso.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Resultado con el código HTTP, el mensaje y los identificadores de la OT creada.</returns>
        public async Task<OrdenTrabajoCompletaResult> CrearAsync(OrdenTrabajoCompletaRequest request, CancellationToken cancellationToken)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                _context.ChangeTracker.Clear();
                // Los triggers de estas tablas (TR_CrearOTs, TR_ActualizarCampos_*) escriben por linked server y promueven la
                // transacción a distribuida; SQL Server no lo permite si EF Core dejó un savepoint activo en SaveChanges.
                _context.Database.AutoSavepointsEnabled = false;
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                var orden = request.OrdenTrabajo;
                if (await _context.Set<Orden_Trabajo>().AnyAsync(ot => ot.Numero_OT == orden.Numero_OT, cancellationToken))
                    return OrdenTrabajoCompletaResult.Conflict($"Ya existe una orden de trabajo con el número {orden.Numero_OT}.");

                orden.Ot_Id = 0;
                _context.Add(orden);
                await _context.SaveChangesAsync(cancellationToken);

                request.Extrusion.Extrusion_Id = 0;
                request.Extrusion.Ot_Id = orden.Ot_Id;
                request.Impresion.Impresion_Id = 0;
                request.Impresion.Ot_Id = orden.Ot_Id;
                request.Laminado.LamCapa_Id = 0;
                request.Laminado.OT_Id = orden.Ot_Id;
                request.SelladoCorte.SelladoCorte_Id = 0;
                request.SelladoCorte.Ot_Id = orden.Ot_Id;

                _context.AddRange(request.Extrusion, request.Impresion, request.Laminado, request.SelladoCorte);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return OrdenTrabajoCompletaResult.Success(201, "Orden de trabajo creada.", orden.Ot_Id, orden.Numero_OT);
            });
        }

        /// <summary>
        /// Actualiza la OT y sus cuatro procesos en un único SaveChanges (atómico); si algún registro falla no se modifica nada.
        /// El número, la fecha y la hora de creación de la OT se conservan. Si a la OT le falta algún proceso, se crea.
        /// </summary>
        /// <param name="otId">Identificador interno de la orden de trabajo.</param>
        /// <param name="request">Orden de trabajo y la información de cada proceso.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Resultado con el código HTTP, el mensaje y los identificadores de la OT editada.</returns>
        public async Task<OrdenTrabajoCompletaResult> EditarAsync(long otId, OrdenTrabajoCompletaRequest request, CancellationToken cancellationToken)
        {
            _context.ChangeTracker.Clear();

            var orden = await _context.Set<Orden_Trabajo>().FirstOrDefaultAsync(ot => ot.Ot_Id == otId, cancellationToken);
            if (orden == null)
                return OrdenTrabajoCompletaResult.NotFound($"No existe la orden de trabajo con Id {otId}.");

            request.OrdenTrabajo.Ot_Id = otId;
            request.OrdenTrabajo.Numero_OT = orden.Numero_OT;
            request.OrdenTrabajo.Ot_FechaCreacion = orden.Ot_FechaCreacion;
            request.OrdenTrabajo.Ot_Hora = orden.Ot_Hora;
            _context.Entry(orden).CurrentValues.SetValues(request.OrdenTrabajo);

            request.Extrusion.Extrusion_Id = 0;
            request.Extrusion.Ot_Id = otId;
            await ActualizarOCrearProcesoAsync(_context.Set<OT_Extrusion>().Where(p => p.Ot_Id == otId), request.Extrusion,
                (registro, valores) => valores.Extrusion_Id = registro.Extrusion_Id, cancellationToken);

            request.Impresion.Impresion_Id = 0;
            request.Impresion.Ot_Id = otId;
            await ActualizarOCrearProcesoAsync(_context.Set<OT_Impresion>().Where(p => p.Ot_Id == otId), request.Impresion,
                (registro, valores) => valores.Impresion_Id = registro.Impresion_Id, cancellationToken);

            request.Laminado.LamCapa_Id = 0;
            request.Laminado.OT_Id = otId;
            await ActualizarOCrearProcesoAsync(_context.Set<OT_Laminado>().Where(p => p.OT_Id == otId), request.Laminado,
                (registro, valores) => valores.LamCapa_Id = registro.LamCapa_Id, cancellationToken);

            request.SelladoCorte.SelladoCorte_Id = 0;
            request.SelladoCorte.Ot_Id = otId;
            await ActualizarOCrearProcesoAsync(_context.Set<OT_Sellado_Corte>().Where(p => p.Ot_Id == otId), request.SelladoCorte,
                (registro, valores) => valores.SelladoCorte_Id = registro.SelladoCorte_Id, cancellationToken);

            // Un solo SaveChanges: EF Core lo envuelve en su propia transacción, sin savepoints, por lo que los triggers
            // que escriben por linked server pueden promoverla a distribuida.
            await _context.SaveChangesAsync(cancellationToken);

            return OrdenTrabajoCompletaResult.Success(200, "Orden de trabajo actualizada.", orden.Ot_Id, orden.Numero_OT);
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
    public sealed record OrdenTrabajoCompletaResult(bool Succeeded, int StatusCode, string Message, long? Ot_Id, long? Numero_OT)
    {
        /// <summary>Crea un resultado exitoso.</summary>
        /// <param name="statusCode">Código HTTP de éxito (201 al crear, 200 al editar).</param>
        /// <param name="message">Mensaje descriptivo.</param>
        /// <param name="otId">Identificador interno de la OT.</param>
        /// <param name="numeroOT">Número visible de la OT.</param>
        /// <returns>Resultado exitoso.</returns>
        public static OrdenTrabajoCompletaResult Success(int statusCode, string message, long otId, long numeroOT) => new(true, statusCode, message, otId, numeroOT);

        /// <summary>Crea un resultado por recurso inexistente.</summary>
        /// <param name="message">Descripción del recurso no encontrado.</param>
        /// <returns>Resultado con código 404.</returns>
        public static OrdenTrabajoCompletaResult NotFound(string message) => new(false, 404, message, null, null);

        /// <summary>Crea un resultado por conflicto con el estado actual de los datos.</summary>
        /// <param name="message">Descripción del conflicto.</param>
        /// <returns>Resultado con código 409.</returns>
        public static OrdenTrabajoCompletaResult Conflict(string message) => new(false, 409, message, null, null);
    }
}
