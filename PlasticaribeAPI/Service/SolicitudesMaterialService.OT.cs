using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Resultado de una acción sobre la solicitud de la OT (completar, finalizar o verificar).
    /// </summary>
    /// <param name="StatusCode">Código HTTP: 200 si se aplicó; 404 o 409 según la regla que no se cumple.</param>
    /// <param name="Mensaje">Mensaje para el usuario.</param>
    public sealed record ResultadoAccionOT(int StatusCode, string Mensaje);

    /// <summary>
    /// Avance y estado de la solicitud de la OT, para el panel de la OT en Movimientos y en la solicitud.
    /// </summary>
    public sealed record ResumenSolicitudOT
    {
        /// <summary>Número de la OT.</summary>
        public long OT { get; init; }
        /// <summary>Kg de la OT (umbral); null si aún no se conocen.</summary>
        public decimal? KgOT { get; init; }
        /// <summary>Kg de la OT más el adicional; null si aún no se conocen los kg de la OT.</summary>
        public decimal? TopeKgOT { get; init; }
        /// <summary>Estado de la solicitud de la OT.</summary>
        public int Estado_Id { get; init; }
        /// <summary>Nombre del estado.</summary>
        public string Estado { get; init; } = string.Empty;
        /// <summary>Kg netos asignados a la OT (materia prima sin la categoría excluida, más BOPP, menos devoluciones).</summary>
        public decimal KgAsignados { get; init; }
        /// <summary>Kg de materia prima solicitados en las solicitudes no canceladas de la OT.</summary>
        public decimal KgSolicitados { get; init; }
        /// <summary>Kg solicitados que aún no se asignan en las solicitudes activas.</summary>
        public decimal KgPendientesPorAsignar { get; init; }
        /// <summary>Lo asignado llegó a los kg de la OT.</summary>
        public bool UmbralAlcanzado { get; init; }
        /// <summary>Lo asignado llegó al tope (kg de la OT más el adicional).</summary>
        public bool TopeAlcanzado { get; init; }
        /// <summary>Cantidad de solicitudes de la OT (sin la genérica) por estado.</summary>
        public IReadOnlyList<ConteoEstadoOT> Solicitudes { get; init; } = [];
        /// <summary>Se puede completar a mano: está activa y llegó al umbral.</summary>
        public bool PuedeCompletar { get; init; }
        /// <summary>Se puede finalizar: está completada y no tiene solicitudes activas.</summary>
        public bool PuedeFinalizar { get; init; }
        /// <summary>Se puede verificar: está finalizada.</summary>
        public bool PuedeVerificar { get; init; }
        /// <summary>Fecha en que se completó.</summary>
        public DateTime? FechaCompletada { get; init; }
        /// <summary>Usuario que la completó a mano; null si se completó sola o no se ha completado.</summary>
        public string? UsuarioCompleta { get; init; }
        /// <summary>Fecha en que se finalizó.</summary>
        public DateTime? FechaFinalizada { get; init; }
        /// <summary>Usuario que la finalizó.</summary>
        public string? UsuarioFinaliza { get; init; }
        /// <summary>Fecha en que se verificó.</summary>
        public DateTime? FechaVerificada { get; init; }
        /// <summary>Usuario que la verificó.</summary>
        public string? UsuarioVerifica { get; init; }
    }

    /// <summary>Cantidad de solicitudes de una OT en un estado.</summary>
    /// <param name="Estado_Id">Estado.</param>
    /// <param name="Estado">Nombre del estado.</param>
    /// <param name="Cantidad">Cantidad de solicitudes.</param>
    public sealed record ConteoEstadoOT(int Estado_Id, string Estado, int Cantidad);

    /// <summary>
    /// Solicitud de la OT: una OT puede tener varias solicitudes de material y su estado se mide contra los kg de la OT.
    /// Pendiente sin material asignado; parcial con material; completada al llegar al tope (kg más el adicional) o cuando
    /// alguien la completa después de llegar al umbral (kg de la OT); finalizada cuando se cierra la asignación; verificada
    /// cuando el solicitante comprueba lo recibido. Al completarse la OT se completan sus solicitudes activas de materia prima.
    /// </summary>
    public sealed partial class SolicitudesMaterialService
    {
        /// <summary>Proceso cuyas solicitudes son de materia prima y cuentan para los kg de la OT.</summary>
        public const string ProcesoMateriaPrima = "EXT";

        /// <summary>Estado de la OT en Estados_ProcesosOT antes de producir (ABIERTA).</summary>
        private const int EstadoProcesoOTAbierta = (int)EstadoEnum.Abierta;

        /// <summary>Estado de la OT en Estados_ProcesosOT cuando se le asignó el material antes de producir (ASIGNADA).</summary>
        private const int EstadoProcesoOTAsignada = (int)EstadoEnum.Asignada;

        /// <summary>
        /// Calcula el estado de la solicitud de la OT. Completada, finalizada y verificada no se deshacen.
        /// </summary>
        /// <param name="estadoActual">Estado guardado de la OT.</param>
        /// <param name="kgOT">Kg de la OT; null si no se conocen (entonces no se completa sola).</param>
        /// <param name="kgAsignados">Kg netos asignados a la OT.</param>
        /// <returns>Estado calculado.</returns>
        public static int CalcularEstadoOT(int estadoActual, decimal? kgOT, decimal kgAsignados)
        {
            if (!EstadosSolicitudMaterial.EstaActiva(estadoActual)) return estadoActual;
            if (kgOT is > 0m && kgAsignados >= EstadosSolicitudMaterial.TopeKgOT(kgOT.Value))
                return EstadosSolicitudMaterial.Completada;
            return kgAsignados > 0m ? EstadosSolicitudMaterial.Parcial : EstadosSolicitudMaterial.Pendiente;
        }

        /// <summary>
        /// Valida si se puede completar a mano la solicitud de la OT: debe estar activa y lo asignado debe llegar a sus kg.
        /// </summary>
        /// <param name="estado">Estado de la OT.</param>
        /// <param name="kgOT">Kg de la OT.</param>
        /// <param name="kgAsignados">Kg netos asignados.</param>
        /// <returns>Null si se puede; si no, el mensaje.</returns>
        public static string? ValidarCompletarOT(int estado, decimal? kgOT, decimal kgAsignados)
        {
            if (!EstadosSolicitudMaterial.EstaActiva(estado))
                return "La solicitud de la OT ya está completada, finalizada o verificada.";
            if (kgOT is not > 0m)
                return "Aún no se conocen los kg de la OT. Consulte la OT para actualizarlos.";
            if (kgAsignados < kgOT.Value)
                return "Lo asignado aún no llega a los kg de la OT.";
            return null;
        }

        /// <summary>
        /// Valida si se puede finalizar la solicitud de la OT: debe estar completada, tener al menos una solicitud
        /// y ninguna activa (pendiente o parcial).
        /// </summary>
        /// <param name="estado">Estado de la OT.</param>
        /// <param name="estadosSolicitudes">Estados de las solicitudes de la OT.</param>
        /// <returns>Null si se puede; si no, el mensaje.</returns>
        public static string? ValidarFinalizarOT(int estado, IReadOnlyCollection<int> estadosSolicitudes)
        {
            if (estado != EstadosSolicitudMaterial.Completada)
                return estado is EstadosSolicitudMaterial.Finalizada or EstadosSolicitudMaterial.Verificada
                    ? "La solicitud de la OT ya está finalizada."
                    : "La solicitud de la OT debe estar completada para finalizarla.";
            if (!estadosSolicitudes.Any(e => e != EstadosSolicitudMaterial.Cancelada))
                return "La OT no tiene solicitudes por finalizar.";
            var activas = estadosSolicitudes.Count(EstadosSolicitudMaterial.EstaActiva);
            return activas > 0
                ? $"La OT tiene {activas} solicitud(es) pendiente(s) o parcial(es); deben completarse o cancelarse antes de finalizar."
                : null;
        }

        /// <summary>
        /// Crea la solicitud de la OT si no existe y guarda sus kg si se conocen. No guarda los cambios.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="kgOT">Kg de la OT en BagPro; null o cero para no cambiarlos.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>La solicitud de la OT, con seguimiento de cambios.</returns>
        private async Task<SolicitudMaterial_OT> ObtenerOCrearOTAsync(long ot, decimal? kgOT, CancellationToken cancellationToken)
        {
            var registro = await _context.SolicitudMaterial_OT
                .AsTracking()
                .FirstOrDefaultAsync(o => o.SolOT_OrdenTrabajo == ot, cancellationToken);
            if (registro == null)
            {
                registro = new SolicitudMaterial_OT
                {
                    SolOT_OrdenTrabajo = ot,
                    Estado_Id = EstadosSolicitudMaterial.Pendiente,
                    SolOT_FechaRegistro = DateTime.Now
                };
                _context.SolicitudMaterial_OT.Add(registro);
            }
            if (kgOT is > 0m) registro.SolOT_KgOT = Math.Round(kgOT.Value, 2);
            return registro;
        }

        /// <summary>
        /// Guarda los kg de la OT (creando su registro si no existe) y recalcula su estado.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="kgOT">Kg de la OT en BagPro; null o cero para no cambiarlos.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Estado resultante de la OT.</returns>
        public async Task<int> RegistrarKgOTAsync(long ot, decimal? kgOT, CancellationToken cancellationToken)
        {
            await GuardarKgOTAsync(ot, kgOT, cancellationToken);
            return await RecalcularOTAsync(ot, cancellationToken);
        }

        /// <summary>
        /// Guarda los kg de la OT (creando su registro si no existe) sin recalcular su estado. Se usa antes de
        /// <see cref="RecalcularEstadoAsync"/>, que recalcula la solicitud y luego su OT.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="kgOT">Kg de la OT en BagPro; null o cero para no cambiarlos.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        public async Task GuardarKgOTAsync(long ot, decimal? kgOT, CancellationToken cancellationToken)
        {
            await ObtenerOCrearOTAsync(ot, kgOT, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Kg de la OT de una petición: los enviados o, si no vienen, los que se deducen del tope (kg más el adicional).
        /// </summary>
        /// <param name="kgOT">Kg de la OT enviados.</param>
        /// <param name="tope">Tope enviado (kg de la OT más el adicional).</param>
        /// <returns>Kg de la OT, o null si no se pueden saber.</returns>
        public static decimal? KgOTDe(decimal? kgOT, decimal? tope) =>
            kgOT is > 0m ? kgOT : tope is > 0m ? EstadosSolicitudMaterial.KgOTDesdeTope(tope.Value) : null;

        /// <summary>
        /// Recalcula el estado de la solicitud de la OT con lo asignado. Si queda completada (por llegar al tope),
        /// completa sus solicitudes activas de materia prima. Guarda los cambios.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Estado resultante de la OT.</returns>
        public async Task<int> RecalcularOTAsync(long ot, CancellationToken cancellationToken)
        {
            var registro = await ObtenerOCrearOTAsync(ot, null, cancellationToken);
            var kgAsignados = await CalcularConsumoOTAsync(ot, cancellationToken);
            var estado = CalcularEstadoOT(registro.Estado_Id, registro.SolOT_KgOT, kgAsignados);

            if (estado == EstadosSolicitudMaterial.Completada && registro.Estado_Id != estado)
            {
                registro.SolOT_FechaCompletada = DateTime.Now;
                registro.Usua_IdCompleta = null; // se completó sola al llegar al tope
            }
            registro.Estado_Id = estado;
            await _context.SaveChangesAsync(cancellationToken);

            if (estado == EstadosSolicitudMaterial.Completada)
                await CompletarSolicitudesMateriaPrimaAsync(ot, cancellationToken);
            return estado;
        }

        /// <summary>
        /// Pasa a completadas las solicitudes activas de materia prima de la OT. Las de tintas siguen su propio total.
        /// </summary>
        private Task<int> CompletarSolicitudesMateriaPrimaAsync(long ot, CancellationToken cancellationToken) =>
            _context.Solicitud_MatPrimaExtrusion
                .Where(s => s.SolMpExt_OT == ot &&
                    s.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId &&
                    s.Proceso_Id == ProcesoMateriaPrima &&
                    (s.Estado_Id == EstadosSolicitudMaterial.Pendiente || s.Estado_Id == EstadosSolicitudMaterial.Parcial))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.Estado_Id, EstadosSolicitudMaterial.Completada), cancellationToken);

        /// <summary>
        /// Obtiene el estado guardado de la solicitud de la OT.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El estado, o null si la OT aún no tiene solicitudes.</returns>
        public Task<int?> ObtenerEstadoOTAsync(long ot, CancellationToken cancellationToken) =>
            _context.SolicitudMaterial_OT
                .AsNoTracking()
                .Where(o => o.SolOT_OrdenTrabajo == ot)
                .Select(o => (int?)o.Estado_Id)
                .FirstOrDefaultAsync(cancellationToken);

        /// <summary>
        /// Indica por qué una OT ya no admite solicitudes (crear o editar) del proceso indicado, o null si las admite.
        /// Una OT completada no admite más materia prima; finalizada o verificada no admite nada.
        /// </summary>
        /// <param name="estadoOT">Estado de la solicitud de la OT (null si no existe).</param>
        /// <param name="proceso">Proceso de la solicitud.</param>
        /// <param name="ot">Número de la OT, para el mensaje.</param>
        /// <returns>El mensaje, o null si se permite.</returns>
        public static string? MotivoOTCerrada(int? estadoOT, string proceso, long ot) => estadoOT switch
        {
            EstadosSolicitudMaterial.Finalizada or EstadosSolicitudMaterial.Verificada =>
                $"La asignación de materiales de la OT {ot} ya está finalizada; no admite más solicitudes ni asignaciones.",
            EstadosSolicitudMaterial.Completada when proceso == ProcesoMateriaPrima =>
                $"La OT {ot} ya está completada; no admite más solicitudes ni asignaciones de materia prima.",
            _ => null
        };

        /// <summary>
        /// Completa a mano la solicitud de la OT después de llegar al umbral (kg de la OT) y completa sus solicitudes
        /// activas de materia prima. No se puede deshacer.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="usuarioId">Usuario que completa.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Resultado de la acción.</returns>
        public async Task<ResultadoAccionOT> CompletarOTAsync(long ot, long usuarioId, CancellationToken cancellationToken)
        {
            var registro = await _context.SolicitudMaterial_OT
                .AsTracking()
                .FirstOrDefaultAsync(o => o.SolOT_OrdenTrabajo == ot, cancellationToken);
            if (registro == null) return new(404, $"La OT {ot} no tiene solicitudes de material.");

            var kgAsignados = await CalcularConsumoOTAsync(ot, cancellationToken);
            var error = ValidarCompletarOT(registro.Estado_Id, registro.SolOT_KgOT, kgAsignados);
            if (error != null) return new(409, error);

            registro.Estado_Id = EstadosSolicitudMaterial.Completada;
            registro.SolOT_FechaCompletada = DateTime.Now;
            registro.Usua_IdCompleta = usuarioId;
            await _context.SaveChangesAsync(cancellationToken);
            await CompletarSolicitudesMateriaPrimaAsync(ot, cancellationToken);
            return new(200, $"La solicitud de la OT {ot} quedó completada.");
        }

        /// <summary>
        /// Finaliza la solicitud de la OT: cierra la asignación de materiales, pasa sus solicitudes completadas a
        /// finalizadas y, si la OT aún no tiene producción (ABIERTA), la registra como ASIGNADA en Estados_ProcesosOT.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="usuarioId">Usuario que finaliza.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Resultado de la acción.</returns>
        public async Task<ResultadoAccionOT> FinalizarOTAsync(long ot, long usuarioId, CancellationToken cancellationToken)
        {
            var registro = await _context.SolicitudMaterial_OT
                .AsTracking()
                .FirstOrDefaultAsync(o => o.SolOT_OrdenTrabajo == ot, cancellationToken);
            if (registro == null) return new(404, $"La OT {ot} no tiene solicitudes de material.");

            var estados = await _context.Solicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(s => s.SolMpExt_OT == ot && s.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId)
                .Select(s => s.Estado_Id)
                .ToListAsync(cancellationToken);
            var error = ValidarFinalizarOT(registro.Estado_Id, estados);
            if (error != null) return new(409, error);

            registro.Estado_Id = EstadosSolicitudMaterial.Finalizada;
            registro.SolOT_FechaFinalizada = DateTime.Now;
            registro.Usua_IdFinaliza = usuarioId;
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Solicitud_MatPrimaExtrusion
                .Where(s => s.SolMpExt_OT == ot &&
                    s.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId &&
                    s.Estado_Id == EstadosSolicitudMaterial.Completada)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.Estado_Id, EstadosSolicitudMaterial.Finalizada), cancellationToken);

            // Trazabilidad: solo si la OT aún no empezó a producir; si ya está en proceso no se toca.
            var asignada = await _context.Estados_ProcesosOT
                .Where(e => e.EstProcOT_OrdenTrabajo == ot && e.Estado_Id == EstadoProcesoOTAbierta)
                .ExecuteUpdateAsync(e => e.SetProperty(x => x.Estado_Id, EstadoProcesoOTAsignada), cancellationToken);

            return new(200, asignada > 0
                ? $"La solicitud de la OT {ot} quedó finalizada y la OT pasó a ASIGNADA."
                : $"La solicitud de la OT {ot} quedó finalizada.");
        }

        /// <summary>
        /// Verifica la solicitud de la OT (el solicitante comprobó lo recibido) y pasa sus solicitudes finalizadas a verificadas.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="usuarioId">Usuario que verifica.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Resultado de la acción.</returns>
        public async Task<ResultadoAccionOT> VerificarOTAsync(long ot, long usuarioId, CancellationToken cancellationToken)
        {
            var registro = await _context.SolicitudMaterial_OT
                .AsTracking()
                .FirstOrDefaultAsync(o => o.SolOT_OrdenTrabajo == ot, cancellationToken);
            if (registro == null) return new(404, $"La OT {ot} no tiene solicitudes de material.");
            if (registro.Estado_Id != EstadosSolicitudMaterial.Finalizada)
                return new(409, registro.Estado_Id == EstadosSolicitudMaterial.Verificada
                    ? $"La solicitud de la OT {ot} ya está verificada."
                    : "Solo se puede verificar una solicitud de OT finalizada.");

            registro.Estado_Id = EstadosSolicitudMaterial.Verificada;
            registro.SolOT_FechaVerificada = DateTime.Now;
            registro.Usua_IdVerifica = usuarioId;
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Solicitud_MatPrimaExtrusion
                .Where(s => s.SolMpExt_OT == ot &&
                    s.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId &&
                    s.Estado_Id == EstadosSolicitudMaterial.Finalizada)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.Estado_Id, EstadosSolicitudMaterial.Verificada), cancellationToken);
            return new(200, $"La solicitud de la OT {ot} quedó verificada.");
        }

        /// <summary>
        /// Obtiene el avance y el estado de la solicitud de la OT.
        /// </summary>
        /// <param name="ot">Número de la OT.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El resumen, o null si la OT no tiene solicitudes.</returns>
        public async Task<ResumenSolicitudOT?> ObtenerResumenOTAsync(long ot, CancellationToken cancellationToken)
        {
            var registro = await _context.SolicitudMaterial_OT
                .AsNoTracking()
                .Where(o => o.SolOT_OrdenTrabajo == ot)
                .Select(o => new
                {
                    o.SolOT_KgOT,
                    o.Estado_Id,
                    Estado = o.Estado!.Estado_Nombre,
                    o.SolOT_FechaCompletada,
                    UsuarioCompleta = o.UsuaCompleta != null ? o.UsuaCompleta.Usua_Nombre : null,
                    o.SolOT_FechaFinalizada,
                    UsuarioFinaliza = o.UsuaFinaliza != null ? o.UsuaFinaliza.Usua_Nombre : null,
                    o.SolOT_FechaVerificada,
                    UsuarioVerifica = o.UsuaVerifica != null ? o.UsuaVerifica.Usua_Nombre : null
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (registro == null) return null;

            var solicitudes = await _context.Solicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(s => s.SolMpExt_OT == ot && s.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId)
                .GroupBy(s => new { s.Estado_Id, s.Estado!.Estado_Nombre })
                .Select(g => new ConteoEstadoOT(g.Key.Estado_Id, g.Key.Estado_Nombre, g.Count()))
                .ToListAsync(cancellationToken);

            var kgSolicitados = await _context.DetSolicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(d => d.SolMatPriExt!.SolMpExt_OT == ot &&
                    d.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId &&
                    d.SolMatPriExt.Proceso_Id == ProcesoMateriaPrima &&
                    d.SolMatPriExt.Estado_Id != EstadosSolicitudMaterial.Cancelada)
                .SumAsync(d => d.DtSolMpExt_Cantidad, cancellationToken);

            var kgAsignados = await CalcularConsumoOTAsync(ot, cancellationToken);
            var kgPendientes = await ObtenerPendientePorAsignarOTAsync(ot, cancellationToken);
            var kgOT = registro.SolOT_KgOT;

            return new ResumenSolicitudOT
            {
                OT = ot,
                KgOT = kgOT,
                TopeKgOT = kgOT is > 0m ? EstadosSolicitudMaterial.TopeKgOT(kgOT.Value) : null,
                Estado_Id = registro.Estado_Id,
                Estado = registro.Estado.Trim(),
                KgAsignados = kgAsignados,
                KgSolicitados = kgSolicitados,
                KgPendientesPorAsignar = kgPendientes,
                UmbralAlcanzado = kgOT is > 0m && kgAsignados >= kgOT.Value,
                TopeAlcanzado = kgOT is > 0m && kgAsignados >= EstadosSolicitudMaterial.TopeKgOT(kgOT.Value),
                Solicitudes = solicitudes,
                PuedeCompletar = ValidarCompletarOT(registro.Estado_Id, kgOT, kgAsignados) == null,
                PuedeFinalizar = ValidarFinalizarOT(registro.Estado_Id,
                    solicitudes.SelectMany(s => Enumerable.Repeat(s.Estado_Id, s.Cantidad)).ToList()) == null,
                PuedeVerificar = registro.Estado_Id == EstadosSolicitudMaterial.Finalizada,
                FechaCompletada = registro.SolOT_FechaCompletada,
                UsuarioCompleta = registro.UsuarioCompleta,
                FechaFinalizada = registro.SolOT_FechaFinalizada,
                UsuarioFinaliza = registro.UsuarioFinaliza,
                FechaVerificada = registro.SolOT_FechaVerificada,
                UsuarioVerifica = registro.UsuarioVerifica
            };
        }
    }
}
