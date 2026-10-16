using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Interfaces;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    public class EstadoProcesoOTService : IEstadoProcesoOT
    {
        private readonly dataContext _context;

        public EstadoProcesoOTService(dataContext context)
        {
            _context = context;
        }

        public async Task<Estados_ProcesosOT?> getEstadoProcesoOT(long OT)
        {
            return await _context.Set<Estados_ProcesosOT>()
                .FirstOrDefaultAsync(est => est.EstProcOT_OrdenTrabajo == OT);
        }

        public async Task<IReadOnlyDictionary<long, Estados_ProcesosOT>> getEstadosProcesoOT(
            IEnumerable<long> ordenesTrabajo,
            CancellationToken cancellationToken)
        {
            var ordenes = ordenesTrabajo.Distinct().ToArray();
            if (ordenes.Length == 0)
                return new Dictionary<long, Estados_ProcesosOT>();

            var estados = await _context.Set<Estados_ProcesosOT>()
                .Where(est => ordenes.Contains(est.EstProcOT_OrdenTrabajo))
                .ToListAsync(cancellationToken);

            return estados
                .GroupBy(est => est.EstProcOT_OrdenTrabajo)
                .ToDictionary(grupo => grupo.Key, grupo => grupo.First());
        }

        public async Task ValidarYCambiarEstado_EstadoProcesoOT(
            IReadOnlyDictionary<long, (Estados_ProcesosOT EstadoOT, HashSet<string> Procesos)> estadosPorOT,
            CancellationToken cancellationToken)
        {
            var cantidadesProducidasPorOT = estadosPorOT.ToDictionary(
                par => par.Key,
                par => par.Value.Procesos
                    .Select(proceso => ObtenerCantidadProducida(par.Value.EstadoOT, proceso))
                    .Where(cantidad => cantidad.HasValue)
                    .Select(cantidad => cantidad!.Value)
                    .ToList());

            var ordenesSinProduccion = cantidadesProducidasPorOT
                .Where(par => par.Value.Count > 0 && par.Value.All(cantidad => cantidad <= 0m))
                .Select(par => par.Key)
                .ToArray();

            var ordenesConAsignacion = ordenesSinProduccion.Length == 0
                ? new HashSet<long>()
                : (await _context.Set<Asignacion_MatPrima>()
                    .Where(asignacion => ordenesSinProduccion.Contains(asignacion.AsigMP_OrdenTrabajo))
                    .Select(asignacion => asignacion.AsigMP_OrdenTrabajo)
                    .Distinct()
                    .ToListAsync(cancellationToken))
                .ToHashSet();

            foreach (var (_, datosOT) in estadosPorOT)
            {
                var estadoOT = datosOT.EstadoOT;
                var cantidadesProducidas = cantidadesProducidasPorOT[estadoOT.EstProcOT_OrdenTrabajo];

                if (cantidadesProducidas.Count == 0)
                    continue;

                if (cantidadesProducidas.All(cantidad => cantidad <= 0m))
                {
                    estadoOT.Estado_Id = ordenesConAsignacion.Contains(estadoOT.EstProcOT_OrdenTrabajo) ? 14 : 15;
                }
                else if (estadoOT.EstProcOT_CantidadPedidaUnd.HasValue &&
                         cantidadesProducidas.Any(cantidad => cantidad >= estadoOT.EstProcOT_CantidadPedidaUnd.Value))
                {
                    estadoOT.Estado_Id = 17;
                }
                else
                {
                    estadoOT.Estado_Id = 16;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private static decimal? ObtenerCantidadProducida(Estados_ProcesosOT estadoOT, string proceso)
        {
            return proceso switch
            {
                "EXT" => estadoOT.EstProcOT_ExtrusionKg,
                "IMP" => estadoOT.EstProcOT_ImpresionKg,
                "LAM" => estadoOT.EstProcOT_LaminadoKg,
                "PERF" => estadoOT.EstProcOT_PerforadoKg ?? 0m,
                "DBLD" => estadoOT.EstProcOT_DobladoKg,
                "EMP" => estadoOT.EstProcOT_EmpaqueKg,
                "SELLA" => estadoOT.EstProcOT_SelladoUnd,
                _ => null
            };
        }
    }
}
