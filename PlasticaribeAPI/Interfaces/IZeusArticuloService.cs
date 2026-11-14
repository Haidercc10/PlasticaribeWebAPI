using PlasticaribeAPI.DTOs;
using ServiceReference1;

namespace PlasticaribeAPI.Interfaces
{
    /// <summary>
    /// Creación y consulta de artículos de inventario en Zeus para productos existentes en Plasticaribe.
    /// </summary>
    public interface IZeusArticuloService
    {
        /// <summary>
        /// Consulta los artículos de Zeus (una fila por presentación) con el código indicado.
        /// </summary>
        /// <param name="codigo">Código del artículo (Prod_Id).</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Artículos existentes con nombre, descripción, grupos, precio y costo.</returns>
        Task<List<ArticuloZeusResumen>> ObtenerArticulosAsync(string codigo, CancellationToken cancellationToken = default);

        /// <summary>
        /// Indica si el artículo ya existe en Zeus con la presentación indicada.
        /// </summary>
        /// <param name="codigo">Código del artículo (Prod_Id).</param>
        /// <param name="presentacionZeus">Presentación Zeus (KLS, UND, PAQ, MTS).</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns><c>true</c> si ya existe.</returns>
        Task<bool> ExisteArticuloAsync(string codigo, string presentacionZeus, CancellationToken cancellationToken = default);

        /// <summary>
        /// Envía al servicio SOAP de Zeus la inserción del artículo.
        /// </summary>
        /// <param name="datos">Datos del artículo.</param>
        /// <returns>Respuesta del servicio SOAP de Zeus.</returns>
        Task<SoapResponse> EnviarArticuloAsync(ArticuloZeusDatos datos);
    }
}
