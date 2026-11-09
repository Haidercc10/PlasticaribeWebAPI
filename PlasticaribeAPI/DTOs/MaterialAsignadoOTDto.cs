namespace PlasticaribeAPI.DTOs
{
    /// <summary>
    /// Material asignado o devuelto en una orden de trabajo, agrupado por material, unidad, precio y proceso.
    /// </summary>
    /// <param name="Tipo">Tipo de material: MP, TINTA o BOPP.</param>
    /// <param name="Id">Identificador del material.</param>
    /// <param name="Nombre">Nombre del material.</param>
    /// <param name="Cantidad">Cantidad asignada o devuelta (siempre positiva).</param>
    /// <param name="UndMedida">Unidad de medida.</param>
    /// <param name="Precio">Precio unitario.</param>
    /// <param name="SubTotal">Cantidad por precio.</param>
    /// <param name="ProcesoId">Proceso al que se asignó el material.</param>
    /// <param name="Proceso">Nombre del proceso, o "Devolución" si es una devolución.</param>
    /// <param name="EsDevolucion">Indica que la cantidad fue devuelta y resta de lo asignado.</param>
    public sealed record MaterialAsignadoOTDto(
        string Tipo,
        long Id,
        string Nombre,
        decimal Cantidad,
        string UndMedida,
        decimal Precio,
        decimal SubTotal,
        string ProcesoId,
        string Proceso,
        bool EsDevolucion);
}
