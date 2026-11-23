using System.Globalization;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Máquina de una solicitud con su porcentaje, ya validada.
    /// </summary>
    /// <param name="Maquina">Número de la máquina.</param>
    /// <param name="Porcentaje">Porcentaje del material que le corresponde.</param>
    public sealed record MaquinaSolicitud(int Maquina, decimal Porcentaje);

    /// <summary>
    /// Reglas de formato de las solicitudes de material que no dependen de la base de datos:
    /// distribución por máquinas y materiales de cada subcategoría.
    /// </summary>
    public static class ReglasSolicitudMaterial
    {
        /// <summary>
        /// Valida las máquinas de la solicitud. Si no se envían, usa la máquina única del formato anterior al 100%.
        /// </summary>
        /// <param name="maquinas">Máquinas y porcentajes enviados.</param>
        /// <param name="maquinaAnterior">Máquina única del formato anterior.</param>
        /// <returns>Las máquinas validadas en el orden recibido, o el mensaje de error.</returns>
        public static (List<MaquinaSolicitud>? Maquinas, string? Error) ValidarMaquinas(
            IReadOnlyCollection<SolicitudMaquinaRequest>? maquinas,
            int maquinaAnterior)
        {
            if (maquinas == null || maquinas.Count == 0)
            {
                return maquinaAnterior > 0
                    ? ([new MaquinaSolicitud(maquinaAnterior, 100m)], null)
                    : (null, "Debe indicar al menos una máquina.");
            }

            if (maquinas.Select(m => m.Maquina).Distinct().Count() != maquinas.Count)
                return (null, "No se puede repetir una máquina en la misma solicitud.");
            if (maquinas.Any(m => m.Maquina <= 0))
                return (null, "El número de máquina no es válido.");
            if (maquinas.Any(m => m.Porcentaje <= 0m || decimal.Round(m.Porcentaje, 2) != m.Porcentaje))
                return (null, "El porcentaje de cada máquina debe ser mayor a cero y tener máximo dos decimales.");

            var suma = maquinas.Sum(m => m.Porcentaje);
            if (suma != 100m)
                return (null, $"Los porcentajes de las máquinas deben sumar 100 (suman {suma.ToString("0.##", CultureInfo.InvariantCulture)}).");

            return (maquinas.Select(m => new MaquinaSolicitud(m.Maquina, m.Porcentaje)).ToList(), null);
        }

        /// <summary>
        /// Valida los materiales de cada subcategoría: tipo válido, dos decimales, sin repetir un material
        /// en toda la solicitud, sin materiales genéricos y que la cantidad de la subcategoría sea su suma.
        /// Las subcategorías sin materiales (formato anterior) no se validan aquí.
        /// </summary>
        /// <param name="detalles">Subcategorías de la solicitud.</param>
        /// <returns>El mensaje de error, o null si son válidos.</returns>
        public static string? ValidarMateriales(IReadOnlyCollection<SolicitudExtrusionDetalleRequest> detalles)
        {
            var materiales = detalles.SelectMany(d => d.Materiales ?? []).ToList();
            if (materiales.Any(m => !Enum.IsDefined(m.TipoMaterial)))
                return "Tipo de material no válido.";
            if (materiales.Any(m => decimal.Round(m.Cantidad, 2) != m.Cantidad))
                return "Las cantidades de los materiales deben tener máximo dos decimales.";
            if (materiales.Any(m =>
                    (m.TipoMaterial == TipoMaterialInventario.MateriaPrima && m.MaterialId == SolicitudesMaterialService.MateriaPrimaGenericaId) ||
                    (m.TipoMaterial == TipoMaterialInventario.Tinta && m.MaterialId == SolicitudesMaterialService.TintaGenericaId)))
                return "No se puede solicitar el material genérico sin asignar.";
            if (materiales.Select(m => (m.TipoMaterial, m.MaterialId)).Distinct().Count() != materiales.Count)
                return "No se puede repetir un material en la misma solicitud; sume sus cantidades.";

            var descuadrada = detalles.FirstOrDefault(d => d.Materiales is { Count: > 0 } && d.Materiales.Sum(m => m.Cantidad) != d.Cantidad);
            if (descuadrada != null)
                return $"La cantidad de la subcategoría {descuadrada.SubCatMP_Id} ({descuadrada.Cantidad.ToString("N2", CultureInfo.InvariantCulture)}) no es la suma de sus materiales ({descuadrada.Materiales!.Sum(m => m.Cantidad).ToString("N2", CultureInfo.InvariantCulture)}).";

            return null;
        }

        /// <summary>
        /// Tipo de material que se solicita en un proceso: materias primas en Extrusión y tintas en los demás.
        /// </summary>
        /// <param name="proceso">Proceso de la solicitud.</param>
        /// <returns>Tipo de material del proceso.</returns>
        public static TipoMaterialInventario TipoMaterialDelProceso(string proceso) =>
            proceso == "EXT" ? TipoMaterialInventario.MateriaPrima : TipoMaterialInventario.Tinta;

        /// <summary>
        /// Indica si un usuario puede cancelar la solicitud. Quien gestiona solicitudes cancela las pendientes y
        /// parciales; el solicitante solo puede cancelar su propia solicitud mientras esté pendiente.
        /// </summary>
        /// <param name="estadoNuevo">Estado pedido (solo se admite cancelada).</param>
        /// <param name="estadoActual">Estado actual de la solicitud.</param>
        /// <param name="esGestor">El usuario tiene la policy GestionarSolicitudMaterial.</param>
        /// <param name="esDueno">El usuario es quien creó la solicitud.</param>
        /// <returns>Null si se permite; si no, el código HTTP (403 o 409) y el mensaje.</returns>
        public static (int StatusCode, string Mensaje)? ValidarCambioEstado(int estadoNuevo, int estadoActual, bool esGestor, bool esDueno)
        {
            // Las solicitudes ya no se finalizan una por una: se completan solas y se finalizan con su OT.
            if (estadoNuevo != EstadosSolicitudMaterial.Cancelada)
                return (400, "Solo se puede cancelar una solicitud; se finaliza junto con su OT.");

            if (esGestor)
                return EstadosSolicitudMaterial.EstaActiva(estadoActual)
                    ? null
                    : (409, "Solo se pueden cancelar solicitudes pendientes o parciales.");

            if (!esDueno)
                return (403, "No tiene permiso para cambiar el estado de esta solicitud.");

            return estadoActual == EstadosSolicitudMaterial.Pendiente
                ? null
                : (409, "Solo puede cancelar su solicitud mientras esté pendiente (sin material entregado).");
        }
    }
}
