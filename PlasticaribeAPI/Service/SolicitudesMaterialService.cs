using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Estados y valores fijos que usan las solicitudes de material de producción.
    /// </summary>
    /// <remarks>
    /// Los estados son del tipo de estado <see cref="TipoEstado"/> (solicitud y asignación de materia prima) y los usan
    /// tanto cada solicitud como la "solicitud de la OT" (<see cref="SolicitudMaterial_OT"/>).
    /// </remarks>
    public static class EstadosSolicitudMaterial
    {
        /// <summary>Tipo de estado "Solicitud y asignación de materia prima".</summary>
        public const int TipoEstado = (int)TipoEstadoEnum.SolicitudAsignacionMp;

        /// <summary>Sin material asignado.</summary>
        public const int Pendiente = (int)EstadoEnum.SolicitudMpPendiente;

        /// <summary>Con material asignado que no cubre todo lo solicitado (o, en la OT, que no llega al tope).</summary>
        public const int Parcial = (int)EstadoEnum.SolicitudMpParcial;

        /// <summary>
        /// Solicitud: recibió todo lo que pidió, o su OT se completó. OT: llegó al tope con el adicional, o alguien la
        /// completó a mano después de llegar al umbral. No admite más asignaciones ni ediciones y no se deshace.
        /// </summary>
        public const int Completada = (int)EstadoEnum.SolicitudMpCompletada;

        /// <summary>Se cerró la asignación de materiales de la OT (todas sus solicitudes estaban completadas).</summary>
        public const int Finalizada = (int)EstadoEnum.SolicitudMpFinalizada;

        /// <summary>El solicitante comprobó físicamente lo recibido para la OT.</summary>
        public const int Verificada = (int)EstadoEnum.SolicitudMpVerificada;

        /// <summary>Solicitud cancelada.</summary>
        public const int Cancelada = (int)EstadoEnum.SolicitudMpCancelada;

        /// <summary>Solicitud genérica que se asocia a las asignaciones hechas sin solicitud.</summary>
        public const long SolicitudGenericaId = 1;

        /// <summary>Adicional (margen o tolerancia) sobre los kg de la OT que se permite solicitar y asignar.</summary>
        public const decimal AdicionalKgOT = 0.05m;

        /// <summary>Indica si la solicitud está activa: pendiente o parcial.</summary>
        /// <param name="estado">Estado de la solicitud.</param>
        /// <returns>True si aún puede recibir asignaciones y ediciones.</returns>
        public static bool EstaActiva(int estado) => estado is Pendiente or Parcial;

        /// <summary>Indica si el estado ya no admite asignaciones ni ediciones.</summary>
        /// <param name="estado">Estado de la solicitud.</param>
        /// <returns>True si la solicitud está completada, finalizada, verificada o cancelada.</returns>
        public static bool EstaCerrada(int estado) => !EstaActiva(estado);

        /// <summary>Kg máximos que se pueden solicitar y asignar a una OT: sus kg más el adicional.</summary>
        /// <param name="kgOT">Kg de la OT.</param>
        /// <returns>Tope de la OT redondeado a dos decimales.</returns>
        public static decimal TopeKgOT(decimal kgOT) => Math.Round(kgOT * (1m + AdicionalKgOT), 2);

        /// <summary>Kg de la OT a partir del tope enviado por el cliente (kg de la OT más el adicional).</summary>
        /// <param name="tope">Tope de la OT.</param>
        /// <returns>Kg de la OT redondeados a dos decimales.</returns>
        public static decimal KgOTDesdeTope(decimal tope) => Math.Round(tope / (1m + AdicionalKgOT), 2);
    }

    /// <summary>
    /// Cantidad solicitada y asignada de una subcategoría dentro de una solicitud.
    /// </summary>
    /// <param name="SubCatMP_Id">Subcategoría.</param>
    /// <param name="Solicitado">Cantidad solicitada.</param>
    /// <param name="Asignado">Cantidad asignada (materias primas y tintas).</param>
    public sealed record CantidadSubcategoria(int SubCatMP_Id, decimal Solicitado, decimal Asignado)
    {
        /// <summary>Cantidad que falta por asignar; nunca es negativa.</summary>
        public decimal Faltante => Math.Max(0m, Solicitado - Asignado);
    }

    /// <summary>
    /// Material asignado a una solicitud, con lo entregado en total.
    /// </summary>
    /// <param name="Tipo">Materia prima o tinta.</param>
    /// <param name="MaterialId">Identificador del material.</param>
    /// <param name="Nombre">Nombre del material.</param>
    /// <param name="UndMed_Id">Unidad de medida del material.</param>
    /// <param name="SubCatMP_Id">Subcategoría del material.</param>
    /// <param name="Stock">Stock actual del material.</param>
    /// <param name="Entregado">Cantidad asignada a la solicitud.</param>
    public sealed record MaterialEntregadoSolicitud(
        TipoMaterialInventario Tipo,
        long MaterialId,
        string Nombre,
        string UndMed_Id,
        int SubCatMP_Id,
        decimal Stock,
        decimal Entregado);

    /// <summary>
    /// Asignación de una solicitud, para listar sus PDF.
    /// </summary>
    /// <param name="Id">Número de la asignación.</param>
    /// <param name="Fecha">Fecha real de entrega (o la de registro).</param>
    /// <param name="Hora">Hora de registro.</param>
    /// <param name="Maquina">Máquina que recibió.</param>
    /// <param name="Cantidad">Total asignado (materias primas y tintas).</param>
    /// <param name="ConMateriasPrimas">Tiene materias primas (PDF ASIGMP).</param>
    /// <param name="ConTintas">Tiene tintas (PDF ASIGTINTAS).</param>
    public sealed record AsignacionDeSolicitud(
        long Id,
        DateTime Fecha,
        string? Hora,
        int Maquina,
        decimal Cantidad,
        bool ConMateriasPrimas,
        bool ConTintas);

    /// <summary>
    /// Reglas compartidas entre la solicitud de material de producción y la asignación de materia prima:
    /// cantidades asignadas por subcategoría, estado de la solicitud y consumo de la OT.
    /// </summary>
    public sealed partial class SolicitudesMaterialService
    {
        /// <summary>Categoría de materia prima que no cuenta para el límite de kg de la OT.</summary>
        public const int CategoriaExcluidaLimiteOT = 18;

        /// <summary>Materia prima genérica que se guarda cuando el material asignado o devuelto es tinta o BOPP.</summary>
        public const long MateriaPrimaGenericaId = 84;

        /// <summary>Tinta genérica que se guarda cuando el material asignado o devuelto no es tinta.</summary>
        public const long TintaGenericaId = 2001;

        /// <summary>BOPP genérico que se guarda cuando el material asignado o devuelto no es BOPP.</summary>
        public const long BoppGenericoId = 449;

        private readonly dataContext _context;

        /// <summary>
        /// Inicializa el servicio con el contexto de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        public SolicitudesMaterialService(dataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Suma lo asignado (materias primas y tintas) por solicitud y subcategoría.
        /// </summary>
        /// <param name="solicitudes">Solicitudes a consultar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Cantidad asignada por solicitud y subcategoría.</returns>
        public async Task<Dictionary<(long SolMpExt_Id, int SubCatMP_Id), decimal>> ObtenerAsignadoAsync(
            IReadOnlyCollection<long> solicitudes,
            CancellationToken cancellationToken)
        {
            if (solicitudes.Count == 0) return [];

            var ids = solicitudes.ToList();
            var asignadoMp = await _context.DetallesAsignaciones_MateriasPrimas
                .AsNoTracking()
                .Where(d => d.AsigMp!.SolMpExt_Id != null && ids.Contains(d.AsigMp.SolMpExt_Id.Value) && d.MatPri!.SubCatMP_Id != null)
                .GroupBy(d => new { SolMpExt_Id = d.AsigMp!.SolMpExt_Id!.Value, SubCatMP_Id = d.MatPri!.SubCatMP_Id!.Value })
                .Select(g => new { g.Key.SolMpExt_Id, g.Key.SubCatMP_Id, Asignado = g.Sum(x => x.DtAsigMp_Cantidad) })
                .ToListAsync(cancellationToken);

            var asignadoTinta = await _context.DetalleAsignaciones_Tintas
                .AsNoTracking()
                .Where(d => d.AsigMp!.SolMpExt_Id != null && ids.Contains(d.AsigMp.SolMpExt_Id.Value) && d.Tinta!.SubCatMP_Id != null)
                .GroupBy(d => new { SolMpExt_Id = d.AsigMp!.SolMpExt_Id!.Value, SubCatMP_Id = d.Tinta!.SubCatMP_Id!.Value })
                .Select(g => new { g.Key.SolMpExt_Id, g.Key.SubCatMP_Id, Asignado = g.Sum(x => x.DtAsigTinta_Cantidad) })
                .ToListAsync(cancellationToken);

            return asignadoMp.Concat(asignadoTinta)
                .GroupBy(a => (a.SolMpExt_Id, a.SubCatMP_Id))
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Asignado));
        }

        /// <summary>
        /// Suma lo asignado a una solicitud por material (materias primas y tintas), con la subcategoría de cada material.
        /// Incluye los materiales que se entregaron en lugar de los solicitados (sustitutos de la misma subcategoría).
        /// </summary>
        /// <param name="solicitudId">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Una fila por material asignado.</returns>
        public async Task<List<MaterialEntregadoSolicitud>> ObtenerAsignadoPorMaterialAsync(long solicitudId, CancellationToken cancellationToken)
        {
            var materiasPrimas = await _context.DetallesAsignaciones_MateriasPrimas
                .AsNoTracking()
                .Where(d => d.AsigMp!.SolMpExt_Id == solicitudId && d.MatPri_Id != MateriaPrimaGenericaId && d.MatPri!.SubCatMP_Id != null)
                .GroupBy(d => new { d.MatPri_Id, d.MatPri!.MatPri_Nombre, d.MatPri.UndMed_Id, SubCatMP_Id = d.MatPri.SubCatMP_Id!.Value, d.MatPri.MatPri_Stock })
                .Select(g => new MaterialEntregadoSolicitud(
                    TipoMaterialInventario.MateriaPrima, g.Key.MatPri_Id, g.Key.MatPri_Nombre, g.Key.UndMed_Id, g.Key.SubCatMP_Id, g.Key.MatPri_Stock, g.Sum(x => x.DtAsigMp_Cantidad)))
                .ToListAsync(cancellationToken);

            var tintas = await _context.DetalleAsignaciones_Tintas
                .AsNoTracking()
                .Where(d => d.AsigMp!.SolMpExt_Id == solicitudId && d.Tinta_Id != TintaGenericaId && d.Tinta!.SubCatMP_Id != null)
                .GroupBy(d => new { d.Tinta_Id, d.Tinta!.Tinta_Nombre, d.Tinta.UndMed_Id, SubCatMP_Id = d.Tinta.SubCatMP_Id!.Value, d.Tinta.Tinta_Stock })
                .Select(g => new MaterialEntregadoSolicitud(
                    TipoMaterialInventario.Tinta, g.Key.Tinta_Id, g.Key.Tinta_Nombre, g.Key.UndMed_Id, g.Key.SubCatMP_Id, g.Key.Tinta_Stock, g.Sum(x => x.DtAsigTinta_Cantidad)))
                .ToListAsync(cancellationToken);

            return [.. materiasPrimas, .. tintas];
        }

        /// <summary>
        /// Lista las asignaciones de una solicitud (para ver sus PDF): número, fecha, máquina, cantidad total
        /// y si llevan materias primas y/o tintas (cada tipo se imprime en un PDF distinto).
        /// </summary>
        /// <param name="solicitudId">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Las asignaciones de la solicitud, de la más reciente a la más antigua.</returns>
        public async Task<List<AsignacionDeSolicitud>> ObtenerAsignacionesAsync(long solicitudId, CancellationToken cancellationToken)
        {
            var asignaciones = await _context.Asignaciones_MatPrima
                .AsNoTracking()
                .Where(a => a.SolMpExt_Id == solicitudId)
                .Select(a => new { a.AsigMp_Id, Fecha = a.AsigMp_FechaRealEntrega ?? a.AsigMp_FechaEntrega, a.AsigMp_Hora, a.AsigMp_Maquina })
                .ToListAsync(cancellationToken);
            if (asignaciones.Count == 0) return [];

            var ids = asignaciones.Select(a => a.AsigMp_Id).ToList();
            var materiasPrimas = await _context.DetallesAsignaciones_MateriasPrimas
                .AsNoTracking()
                .Where(d => ids.Contains(d.AsigMp_Id))
                .GroupBy(d => d.AsigMp_Id)
                .Select(g => new { AsigMp_Id = g.Key, Cantidad = g.Sum(x => x.DtAsigMp_Cantidad) })
                .ToDictionaryAsync(x => x.AsigMp_Id, x => x.Cantidad, cancellationToken);
            var tintas = await _context.DetalleAsignaciones_Tintas
                .AsNoTracking()
                .Where(d => ids.Contains(d.AsigMp_Id))
                .GroupBy(d => d.AsigMp_Id)
                .Select(g => new { AsigMp_Id = g.Key, Cantidad = g.Sum(x => x.DtAsigTinta_Cantidad) })
                .ToDictionaryAsync(x => x.AsigMp_Id, x => x.Cantidad, cancellationToken);

            return asignaciones
                .OrderByDescending(a => a.AsigMp_Id)
                .Select(a => new AsignacionDeSolicitud(
                    a.AsigMp_Id, a.Fecha, a.AsigMp_Hora, a.AsigMp_Maquina,
                    materiasPrimas.GetValueOrDefault(a.AsigMp_Id) + tintas.GetValueOrDefault(a.AsigMp_Id),
                    materiasPrimas.ContainsKey(a.AsigMp_Id), tintas.ContainsKey(a.AsigMp_Id)))
                .ToList();
        }

        /// <summary>
        /// Suma lo entregado a una solicitud por máquina (materias primas y tintas con subcategoría, sin los materiales genéricos),
        /// con la misma regla que lo entregado por subcategoría. Cada asignación va a una sola máquina.
        /// </summary>
        /// <param name="solicitudId">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Lo entregado por número de máquina.</returns>
        public async Task<Dictionary<int, decimal>> ObtenerAsignadoPorMaquinaAsync(long solicitudId, CancellationToken cancellationToken)
        {
            var materiasPrimas = await _context.DetallesAsignaciones_MateriasPrimas
                .AsNoTracking()
                .Where(d => d.AsigMp!.SolMpExt_Id == solicitudId && d.MatPri_Id != MateriaPrimaGenericaId && d.MatPri!.SubCatMP_Id != null)
                .GroupBy(d => d.AsigMp!.AsigMp_Maquina)
                .Select(g => new { Maquina = g.Key, Entregado = g.Sum(x => x.DtAsigMp_Cantidad) })
                .ToListAsync(cancellationToken);

            var tintas = await _context.DetalleAsignaciones_Tintas
                .AsNoTracking()
                .Where(d => d.AsigMp!.SolMpExt_Id == solicitudId && d.Tinta_Id != TintaGenericaId && d.Tinta!.SubCatMP_Id != null)
                .GroupBy(d => d.AsigMp!.AsigMp_Maquina)
                .Select(g => new { Maquina = g.Key, Entregado = g.Sum(x => x.DtAsigTinta_Cantidad) })
                .ToListAsync(cancellationToken);

            return materiasPrimas.Concat(tintas)
                .GroupBy(a => a.Maquina)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Entregado));
        }

        /// <summary>
        /// Obtiene lo solicitado y lo asignado por subcategoría de una solicitud.
        /// </summary>
        /// <param name="solicitudId">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Una fila por subcategoría solicitada.</returns>
        public async Task<List<CantidadSubcategoria>> ObtenerCantidadesAsync(long solicitudId, CancellationToken cancellationToken)
        {
            var solicitado = await _context.DetSolicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(d => d.SolMpExt_Id == solicitudId)
                .GroupBy(d => d.SubCatMP_Id)
                .Select(g => new { SubCatMP_Id = g.Key, Cantidad = g.Sum(x => x.DtSolMpExt_Cantidad) })
                .ToListAsync(cancellationToken);

            var asignado = await ObtenerAsignadoAsync([solicitudId], cancellationToken);
            return solicitado
                .Select(s => new CantidadSubcategoria(s.SubCatMP_Id, s.Cantidad, asignado.GetValueOrDefault((solicitudId, s.SubCatMP_Id))))
                .ToList();
        }

        /// <summary>
        /// Calcula el estado de una solicitud por su total de kg (no por subcategoría): pendiente sin asignaciones,
        /// completada si lo asignado cubre el total solicitado y parcial en cualquier otro caso.
        /// </summary>
        /// <param name="cantidades">Solicitado y asignado por subcategoría.</param>
        /// <returns>Estado calculado.</returns>
        public static int CalcularEstado(IReadOnlyCollection<CantidadSubcategoria> cantidades)
        {
            if (cantidades.Count == 0 || cantidades.All(c => c.Asignado <= 0m))
                return EstadosSolicitudMaterial.Pendiente;

            return cantidades.Sum(c => c.Asignado) >= cantidades.Sum(c => c.Solicitado)
                ? EstadosSolicitudMaterial.Completada
                : EstadosSolicitudMaterial.Parcial;
        }

        /// <summary>
        /// Reparte lo asignado de una subcategoría entre sus filas de detalle, en orden, sin superar la cantidad de cada fila.
        /// El excedente, si existe, queda en la última fila.
        /// </summary>
        /// <param name="filas">Código y cantidad solicitada de cada fila, en el orden de reparto.</param>
        /// <param name="asignado">Cantidad asignada a la subcategoría.</param>
        /// <returns>Cantidad entregada por código de fila.</returns>
        public static Dictionary<long, decimal> DistribuirEntregado(IReadOnlyList<(long Codigo, decimal Cantidad)> filas, decimal asignado)
        {
            var resultado = new Dictionary<long, decimal>(filas.Count);
            var restante = Math.Max(0m, asignado);
            for (var i = 0; i < filas.Count; i++)
            {
                var entregado = i == filas.Count - 1 ? restante : Math.Min(restante, filas[i].Cantidad);
                resultado[filas[i].Codigo] = entregado;
                restante -= entregado;
            }
            return resultado;
        }

        /// <summary>
        /// Recalcula el estado de la solicitud y las cantidades entregadas y faltantes de sus detalles, guarda los cambios
        /// y recalcula la solicitud de su OT. Las solicitudes que ya no están activas (completadas, finalizadas,
        /// verificadas o canceladas) y la solicitud genérica no se modifican.
        /// </summary>
        /// <param name="solicitudId">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El estado resultante, o null si la solicitud no existe.</returns>
        public async Task<int?> RecalcularEstadoAsync(long solicitudId, CancellationToken cancellationToken)
        {
            var solicitud = await _context.Solicitud_MatPrimaExtrusion
                .AsTracking()
                .FirstOrDefaultAsync(s => s.SolMpExt_Id == solicitudId, cancellationToken);
            if (solicitud == null) return null;
            if (solicitudId == EstadosSolicitudMaterial.SolicitudGenericaId || EstadosSolicitudMaterial.EstaCerrada(solicitud.Estado_Id))
                return solicitud.Estado_Id;

            var detalles = await _context.DetSolicitud_MatPrimaExtrusion
                .AsTracking()
                .Where(d => d.SolMpExt_Id == solicitudId)
                .OrderBy(d => d.Codigo)
                .ToListAsync(cancellationToken);
            var asignado = await ObtenerAsignadoAsync([solicitudId], cancellationToken);

            foreach (var grupo in detalles.GroupBy(d => d.SubCatMP_Id))
            {
                var filas = grupo.Select(d => (d.Codigo, d.DtSolMpExt_Cantidad)).ToList();
                var entregado = DistribuirEntregado(filas, asignado.GetValueOrDefault((solicitudId, grupo.Key)));
                foreach (var detalle in grupo)
                {
                    detalle.DtSolMpExt_CantidadEntregada = entregado[detalle.Codigo];
                    detalle.DtSolMpExt_CantidadFaltante = Math.Max(0m, detalle.DtSolMpExt_Cantidad - entregado[detalle.Codigo]);
                }
            }

            var cantidades = detalles
                .GroupBy(d => d.SubCatMP_Id)
                .Select(g => new CantidadSubcategoria(g.Key, g.Sum(d => d.DtSolMpExt_Cantidad), asignado.GetValueOrDefault((solicitudId, g.Key))))
                .ToList();
            solicitud.Estado_Id = CalcularEstado(cantidades);

            await _context.SaveChangesAsync(cancellationToken);

            // La OT puede completarse con esta asignación (y completar sus solicitudes activas de materia prima).
            await RecalcularOTAsync(solicitud.SolMpExt_OT, cancellationToken);
            return await _context.Solicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(s => s.SolMpExt_Id == solicitudId)
                .Select(s => s.Estado_Id)
                .FirstAsync(cancellationToken);
        }

        /// <summary>
        /// Calcula el consumo neto de una OT: materia prima (sin la categoría excluida) más BOPP, menos devoluciones.
        /// Es la regla única para el límite de kg de la OT.
        /// </summary>
        /// <param name="ot">Número de la orden de trabajo.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Kg netos consumidos por la OT.</returns>
        public async Task<decimal> CalcularConsumoOTAsync(long ot, CancellationToken cancellationToken)
        {
            var materiaPrima = await _context.DetallesAsignaciones_MateriasPrimas
                .AsNoTracking()
                .Where(d => d.AsigMp!.AsigMP_OrdenTrabajo == ot && d.MatPri!.CatMP_Id != CategoriaExcluidaLimiteOT)
                .SumAsync(d => d.DtAsigMp_Cantidad, cancellationToken);

            var bopp = await _context.Set<DetalleAsignacion_BOPP>()
                .AsNoTracking()
                .Where(d => d.DtAsigBOPP_OrdenTrabajo == ot)
                .SumAsync(d => d.DtAsigBOPP_Cantidad, cancellationToken);

            var devoluciones = await _context.Set<DetalleDevolucion_MateriaPrima>()
                .AsNoTracking()
                .Where(d => d.DevMatPri!.DevMatPri_OrdenTrabajo == ot && d.Tinta_Id == 2001 && d.MatPri!.CatMP_Id != CategoriaExcluidaLimiteOT)
                .SumAsync(d => d.DtDevMatPri_CantidadDevuelta, cancellationToken);

            return materiaPrima + bopp - devoluciones;
        }

        /// <summary>
        /// Calcula los kg solicitados que aún no se han asignado en las solicitudes activas de una OT.
        /// Solo cuentan las subcategorías de materia prima, porque las tintas no consumen el límite de kg de la OT.
        /// </summary>
        /// <param name="ot">Número de la orden de trabajo.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Kg pendientes por asignar de las solicitudes activas.</returns>
        public async Task<decimal> ObtenerPendientePorAsignarOTAsync(long ot, CancellationToken cancellationToken)
        {
            var detalles = await _context.DetSolicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(d => d.SolMatPriExt!.SolMpExt_OT == ot &&
                    d.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId &&
                    (d.SolMatPriExt.Estado_Id == EstadosSolicitudMaterial.Pendiente || d.SolMatPriExt.Estado_Id == EstadosSolicitudMaterial.Parcial))
                .GroupBy(d => new { d.SolMpExt_Id, d.SubCatMP_Id })
                .Select(g => new { g.Key.SolMpExt_Id, g.Key.SubCatMP_Id, Solicitado = g.Sum(x => x.DtSolMpExt_Cantidad) })
                .ToListAsync(cancellationToken);
            if (detalles.Count == 0) return 0m;

            var tintas = await ObtenerSubcategoriasDeTintaAsync(detalles.Select(d => d.SubCatMP_Id).Distinct().ToList(), cancellationToken);
            var asignado = await ObtenerAsignadoAsync(detalles.Select(d => d.SolMpExt_Id).Distinct().ToList(), cancellationToken);

            return detalles
                .Where(d => !tintas.Contains(d.SubCatMP_Id))
                .Sum(d => Math.Max(0m, d.Solicitado - asignado.GetValueOrDefault((d.SolMpExt_Id, d.SubCatMP_Id))));
        }

        /// <summary>
        /// Identifica las subcategorías que solo agrupan tintas (tienen tintas y ninguna materia prima).
        /// </summary>
        /// <param name="subcategorias">Subcategorías a evaluar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Subcategorías de tinta.</returns>
        public async Task<HashSet<int>> ObtenerSubcategoriasDeTintaAsync(IReadOnlyCollection<int> subcategorias, CancellationToken cancellationToken)
        {
            var ids = subcategorias.ToList();
            var conTintas = await _context.Tintas
                .AsNoTracking()
                .Where(t => t.SubCatMP_Id != null && ids.Contains(t.SubCatMP_Id.Value))
                .Select(t => t.SubCatMP_Id!.Value)
                .Distinct()
                .ToListAsync(cancellationToken);
            var conMateriaPrima = await _context.Materias_Primas
                .AsNoTracking()
                .Where(m => m.SubCatMP_Id != null && ids.Contains(m.SubCatMP_Id.Value))
                .Select(m => m.SubCatMP_Id!.Value)
                .Distinct()
                .ToListAsync(cancellationToken);

            return conTintas.Except(conMateriaPrima).ToHashSet();
        }

        /// <summary>
        /// Consulta el detalle de los materiales (materias primas, tintas y BOPP) asignados a una OT y sus devoluciones,
        /// agrupados en el servidor. Reemplaza las consultas por cada devolución que hacía el reporte de costos.
        /// </summary>
        /// <param name="ot">Número de la orden de trabajo.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Materiales asignados y devueltos, ordenados por proceso y nombre.</returns>
        public async Task<List<MaterialAsignadoOTDto>> ObtenerMaterialesAsignadosOTAsync(long ot, CancellationToken cancellationToken)
        {
            var materiasPrimas = await _context.DetallesAsignaciones_MateriasPrimas
                .AsNoTracking()
                .Where(d => d.AsigMp!.AsigMP_OrdenTrabajo == ot)
                .GroupBy(d => new { d.MatPri_Id, d.MatPri!.MatPri_Nombre, d.UndMed_Id, d.DtAsigMp_Precio, d.Proceso_Id, d.Proceso!.Proceso_Nombre })
                .Select(g => new
                {
                    g.Key.MatPri_Id,
                    g.Key.MatPri_Nombre,
                    g.Key.UndMed_Id,
                    Precio = g.Key.DtAsigMp_Precio ?? 0m,
                    g.Key.Proceso_Id,
                    g.Key.Proceso_Nombre,
                    Cantidad = g.Sum(x => x.DtAsigMp_Cantidad),
                })
                .ToListAsync(cancellationToken);

            var tintas = await _context.Set<DetalleAsignacion_Tinta>()
                .AsNoTracking()
                .Where(d => d.AsigMp!.AsigMP_OrdenTrabajo == ot)
                .GroupBy(d => new { d.Tinta_Id, d.Tinta!.Tinta_Nombre, d.UndMed_Id, d.DtAsigTinta_Precio, d.Proceso_Id, d.Proceso!.Proceso_Nombre })
                .Select(g => new
                {
                    g.Key.Tinta_Id,
                    g.Key.Tinta_Nombre,
                    g.Key.UndMed_Id,
                    Precio = g.Key.DtAsigTinta_Precio ?? 0m,
                    g.Key.Proceso_Id,
                    g.Key.Proceso_Nombre,
                    Cantidad = g.Sum(x => x.DtAsigTinta_Cantidad),
                })
                .ToListAsync(cancellationToken);

            var bopp = await _context.Set<DetalleAsignacion_BOPP>()
                .AsNoTracking()
                .Where(d => d.DtAsigBOPP_OrdenTrabajo == ot)
                .GroupBy(d => new { d.BOPP_Id, d.BOPP!.BOPP_Nombre, d.UndMed_Id, d.BOPP.BOPP_Precio, d.Proceso_Id, d.Proceso!.Proceso_Nombre })
                .Select(g => new
                {
                    g.Key.BOPP_Id,
                    g.Key.BOPP_Nombre,
                    g.Key.UndMed_Id,
                    g.Key.BOPP_Precio,
                    g.Key.Proceso_Id,
                    g.Key.Proceso_Nombre,
                    Cantidad = g.Sum(x => x.DtAsigBOPP_Cantidad),
                })
                .ToListAsync(cancellationToken);

            var devoluciones = await _context.Set<DetalleDevolucion_MateriaPrima>()
                .AsNoTracking()
                .Where(d => d.DevMatPri!.DevMatPri_OrdenTrabajo == ot)
                .GroupBy(d => new
                {
                    d.MatPri_Id,
                    d.MatPri!.MatPri_Nombre,
                    d.MatPri.MatPri_Precio,
                    d.Tinta_Id,
                    TintaNombre = d.Tinta!.Tinta_Nombre,
                    TintaPrecio = (decimal?)d.Tinta.Tinta_Precio,
                    d.BOPP_Id,
                    BoppNombre = d.Bopp!.BOPP_Nombre,
                    BoppPrecio = (decimal?)d.Bopp.BOPP_Precio,
                    d.UndMed_Id,
                })
                .Select(g => new { g.Key, Cantidad = g.Sum(x => x.DtDevMatPri_CantidadDevuelta) })
                .ToListAsync(cancellationToken);

            var resultado = new List<MaterialAsignadoOTDto>(materiasPrimas.Count + tintas.Count + bopp.Count + devoluciones.Count);
            resultado.AddRange(materiasPrimas.Select(m => new MaterialAsignadoOTDto(
                "MP", m.MatPri_Id, m.MatPri_Nombre, m.Cantidad, m.UndMed_Id, m.Precio, m.Cantidad * m.Precio, m.Proceso_Id, m.Proceso_Nombre, false)));
            resultado.AddRange(tintas.Select(t => new MaterialAsignadoOTDto(
                "TINTA", t.Tinta_Id, t.Tinta_Nombre, t.Cantidad, t.UndMed_Id, t.Precio, t.Cantidad * t.Precio, t.Proceso_Id, t.Proceso_Nombre, false)));
            resultado.AddRange(bopp.Select(b => new MaterialAsignadoOTDto(
                "BOPP", b.BOPP_Id, b.BOPP_Nombre, b.Cantidad, b.UndMed_Id, b.BOPP_Precio, b.Cantidad * b.BOPP_Precio, b.Proceso_Id, b.Proceso_Nombre, false)));

            // Cada devolución guarda en un solo registro el material real y los genéricos de los otros dos tipos.
            foreach (var d in devoluciones)
            {
                var k = d.Key;
                var esMp = k.MatPri_Id != MateriaPrimaGenericaId && k.Tinta_Id == TintaGenericaId && k.BOPP_Id == BoppGenericoId;
                var esTinta = k.MatPri_Id == MateriaPrimaGenericaId && k.Tinta_Id != TintaGenericaId && k.BOPP_Id == BoppGenericoId;
                var esBopp = k.MatPri_Id == MateriaPrimaGenericaId && k.Tinta_Id == TintaGenericaId && k.BOPP_Id != BoppGenericoId;

                if (esMp) resultado.Add(Devolucion("MP", k.MatPri_Id, k.MatPri_Nombre, d.Cantidad, k.UndMed_Id, k.MatPri_Precio));
                else if (esTinta) resultado.Add(Devolucion("TINTA", k.Tinta_Id!.Value, k.TintaNombre, d.Cantidad, k.UndMed_Id, k.TintaPrecio ?? 0m));
                else if (esBopp) resultado.Add(Devolucion("BOPP", k.BOPP_Id!.Value, k.BoppNombre, d.Cantidad, k.UndMed_Id, k.BoppPrecio ?? 0m));
            }

            return resultado
                .OrderBy(m => m.EsDevolucion)
                .ThenBy(m => m.Proceso)
                .ThenBy(m => m.Nombre)
                .ToList();

            static MaterialAsignadoOTDto Devolucion(string tipo, long id, string nombre, decimal cantidad, string und, decimal precio) =>
                new(tipo, id, nombre, cantidad, und, precio, cantidad * precio, "DEV", "Devolución", true);
        }
    }
}
