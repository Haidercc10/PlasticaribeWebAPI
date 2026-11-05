using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Calcula las reservas informativas de inventario a partir de las solicitudes de Extrusión activas.
    /// La reserva no se almacena: se deriva de lo solicitado menos lo ya asignado y no descuenta existencias físicas.
    /// </summary>
    public sealed class ReservasInventarioService
    {
        private readonly dataContext _context;
        private readonly SolicitudesMaterialService _solicitudes;

        /// <summary>
        /// Inicializa el servicio con el contexto de datos y las reglas de solicitudes.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        /// <param name="solicitudes">Servicio que calcula lo asignado por solicitud.</param>
        public ReservasInventarioService(dataContext context, SolicitudesMaterialService solicitudes)
        {
            _context = context;
            _solicitudes = solicitudes;
        }

        /// <summary>
        /// Calcula existencia, reserva pendiente y disponibilidad por subcategoría.
        /// Cada solicitud activa (pendiente o parcial) reserva lo solicitado menos lo asignado, sin valores negativos,
        /// y se excluye la solicitud genérica.
        /// </summary>
        /// <param name="subcategorias">Subcategorías a consultar; si es nulo o vacío se consultan todas las que tengan existencias o reservas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Una fila por subcategoría con su existencia, reserva y disponibilidad.</returns>
        public async Task<List<ReservaInventarioDto>> ObtenerReservasAsync(
            IReadOnlyCollection<int>? subcategorias,
            CancellationToken cancellationToken)
        {
            var filtrar = subcategorias is { Count: > 0 };
            var ids = subcategorias?.ToList() ?? [];

            var detalles = await _context.DetSolicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(d => d.SolMatPriExt!.SolMpExt_Id != EstadosSolicitudMaterial.SolicitudGenericaId &&
                    (d.SolMatPriExt.Estado_Id == EstadosSolicitudMaterial.Pendiente || d.SolMatPriExt.Estado_Id == EstadosSolicitudMaterial.Parcial) &&
                    (!filtrar || ids.Contains(d.SubCatMP_Id)))
                .GroupBy(d => new { d.SolMpExt_Id, d.SubCatMP_Id })
                .Select(g => new { g.Key.SolMpExt_Id, g.Key.SubCatMP_Id, Solicitado = g.Sum(x => x.DtSolMpExt_Cantidad) })
                .ToListAsync(cancellationToken);

            var asignado = await _solicitudes.ObtenerAsignadoAsync(
                detalles.Select(d => d.SolMpExt_Id).Distinct().ToList(), cancellationToken);

            var reservas = detalles
                .GroupBy(d => d.SubCatMP_Id)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(d => Math.Max(0m, d.Solicitado - asignado.GetValueOrDefault((d.SolMpExt_Id, d.SubCatMP_Id)))));

            var stockMp = await _context.Materias_Primas
                .AsNoTracking()
                .Where(m => m.SubCatMP_Id != null && (!filtrar || ids.Contains(m.SubCatMP_Id.Value)))
                .GroupBy(m => m.SubCatMP_Id!.Value)
                .Select(g => new { SubCatMP_Id = g.Key, Existencia = g.Sum(x => x.MatPri_Stock) })
                .ToListAsync(cancellationToken);

            var stockTinta = await _context.Tintas
                .AsNoTracking()
                .Where(t => t.SubCatMP_Id != null && (!filtrar || ids.Contains(t.SubCatMP_Id.Value)))
                .GroupBy(t => t.SubCatMP_Id!.Value)
                .Select(g => new { SubCatMP_Id = g.Key, Existencia = g.Sum(x => x.Tinta_Stock) })
                .ToListAsync(cancellationToken);

            var existencias = new Dictionary<int, (decimal Existencia, TipoMaterialInventario Tipo)>();
            foreach (var s in stockMp) existencias[s.SubCatMP_Id] = (s.Existencia, TipoMaterialInventario.MateriaPrima);
            foreach (var s in stockTinta)
            {
                var previa = existencias.TryGetValue(s.SubCatMP_Id, out var e) ? e.Existencia : 0m;
                existencias[s.SubCatMP_Id] = (previa + s.Existencia, TipoMaterialInventario.Tinta);
            }

            var subcategoriasResultado = existencias.Keys.Union(reservas.Keys).ToList();
            var nombres = await _context.Subcategorias_MatPrima
                .AsNoTracking()
                .Where(s => subcategoriasResultado.Contains(s.SubCatMP_Id))
                .ToDictionaryAsync(s => s.SubCatMP_Id, s => s.SubCatMP_Nombre, cancellationToken);

            return subcategoriasResultado
                .OrderBy(id => id)
                .Select(id =>
                {
                    var existencia = existencias.TryGetValue(id, out var e) ? e.Existencia : 0m;
                    var tipo = existencias.TryGetValue(id, out var t) ? t.Tipo : TipoMaterialInventario.MateriaPrima;
                    var reserva = reservas.GetValueOrDefault(id);
                    return new ReservaInventarioDto(id, nombres.GetValueOrDefault(id, string.Empty), tipo, existencia, reserva, existencia - reserva);
                })
                .ToList();
        }
    }
}
