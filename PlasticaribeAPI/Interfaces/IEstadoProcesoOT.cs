using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Interfaces
{
    public interface IEstadoProcesoOT
    {
        Task<Estados_ProcesosOT?> getEstadoProcesoOT(long OT);
        Task<IReadOnlyDictionary<long, Estados_ProcesosOT>> getEstadosProcesoOT(
            IEnumerable<long> ordenesTrabajo,
            CancellationToken cancellationToken);
        Task ValidarYCambiarEstado_EstadoProcesoOT(
            IReadOnlyDictionary<long, (Estados_ProcesosOT EstadoOT, HashSet<string> Procesos)> estadosPorOT,
            CancellationToken cancellationToken);
    }
}
