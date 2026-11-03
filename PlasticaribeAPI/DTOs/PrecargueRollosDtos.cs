using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.DTOs
{
    /// <summary>
    /// Solicitud para agregar un rollo/bulto a un precargue de despacho en proceso.
    /// Si Pcd_Id es null se crea el precargue con este primer rollo.
    /// </summary>
    public sealed class AgregarRolloPrecargueRequest
    {
        /// <summary>Precargue en proceso al que se agrega el rollo; null para crearlo.</summary>
        public long? Pcd_Id { get; set; }

        /// <summary>Cliente real del precargue.</summary>
        [Range(1, long.MaxValue)]
        public long Cli_Id { get; set; }

        /// <summary>Indica que el rollo fue pesado a nombre de Cliente Stock y se debe buscar entre esos clientes.</summary>
        public bool ClienteStock { get; set; }

        /// <summary>Número de rollo/bulto de BagPro escaneado.</summary>
        [Range(1, long.MaxValue)]
        public long Rollo { get; set; }

        /// <summary>Usuario que realiza el precargue.</summary>
        [Range(1, long.MaxValue)]
        public long Usuario { get; set; }

        /// <summary>Vendedor del cliente; solo se usa al crear el precargue.</summary>
        public long? Vendedor { get; set; }

        /// <summary>Observación; solo se usa al crear el precargue.</summary>
        public string? Observacion { get; set; }
    }

    /// <summary>
    /// Solicitud para finalizar o anular un precargue de despacho en proceso.
    /// </summary>
    public sealed class CambiarEstadoPrecargueRequest
    {
        /// <summary>Usuario que realiza el cambio.</summary>
        [Range(1, long.MaxValue)]
        public long Usuario { get; set; }

        /// <summary>Observación del precargue (al finalizar) o motivo de la anulación (al anular).</summary>
        public string? Observacion { get; set; }
    }

    /// <summary>
    /// Rollo/bulto de un precargue de despacho.
    /// </summary>
    public sealed record RolloPrecargueDto(
        long Roll,
        int Item,
        string? Reference,
        long IdClient,
        string? Client,
        decimal Qty,
        decimal? Weight,
        decimal? NetWeight,
        string Unit,
        long? OT,
        string? ProcessId,
        string? Process,
        string? Ubication);

    /// <summary>
    /// Respuesta al agregar un rollo a un precargue.
    /// </summary>
    /// <param name="Pcd_Id">Id del precargue al que quedó asociado el rollo.</param>
    /// <param name="PrecargueCreado">True si el precargue se creó con este rollo.</param>
    /// <param name="RolloYaRegistrado">True si el rollo ya estaba en este precargue (reintento); no se modificó nada.</param>
    /// <param name="TotalRollos">Cantidad de rollos del precargue después de la operación.</param>
    /// <param name="Rollo">Información del rollo.</param>
    public sealed record AgregarRolloPrecargueResponse(long Pcd_Id, bool PrecargueCreado, bool RolloYaRegistrado, int TotalRollos, RolloPrecargueDto Rollo);

    /// <summary>
    /// Encabezado de un precargue de despacho con sus rollos.
    /// </summary>
    public sealed record PrecargueRollosDto(
        long Pcd_Id,
        long IdClient,
        string? Client,
        long? SalesId,
        int StatusId,
        string? Status,
        DateTime Date,
        string Hour,
        long UserId,
        string? Observation,
        int TotalRollos,
        IReadOnlyList<RolloPrecargueDto> Rolls);

    /// <summary>
    /// Resumen de un precargue de despacho en proceso.
    /// </summary>
    public sealed record PrecargueEnProcesoDto(long Pcd_Id, long IdClient, string? Client, DateTime Date, string Hour, int TotalRollos);
}
