namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Datos para crear una materia prima (polietilenos, recuperados y demás materiales que no son tintas ni BOPP).
    /// El stock inicia en cero, la unidad es Kg y la bodega es la de materias primas.
    /// </summary>
    /// <param name="Nombre">Nombre de la materia prima (se guarda en mayúsculas y no se puede repetir).</param>
    /// <param name="Descripcion">Descripción; si se omite se usa el nombre.</param>
    /// <param name="CatMP_Id">Categoría (no puede ser una categoría de tintas).</param>
    /// <param name="SubCatMP_Id">Subcategoría; debe pertenecer a la categoría.</param>
    /// <param name="Precio">Precio unitario por Kg (mayor o igual a cero).</param>
    public sealed record CrearMateriaPrimaRequest(
        string? Nombre,
        string? Descripcion,
        int CatMP_Id,
        int? SubCatMP_Id,
        decimal Precio);

    /// <summary>
    /// Datos para crear una tinta, chip o solvente. El stock y el inventario inicial inician en cero y la bodega es la de tintas.
    /// </summary>
    /// <param name="Nombre">Nombre de la tinta (se guarda en mayúsculas y no se puede repetir).</param>
    /// <param name="Descripcion">Descripción; si se omite se usa el nombre.</param>
    /// <param name="CodigoHexadecimal">Color en hexadecimal (3, 6 u 8 dígitos, con o sin #); opcional.</param>
    /// <param name="UndMed_Id">Unidad de medida.</param>
    /// <param name="CatMP_Id">Categoría (tintas, chips o solventes).</param>
    /// <param name="SubCatMP_Id">Subcategoría; debe pertenecer a la categoría.</param>
    /// <param name="Precio">Precio unitario (mayor o igual a cero).</param>
    public sealed record CrearTintaRequest(
        string? Nombre,
        string? Descripcion,
        string? CodigoHexadecimal,
        string? UndMed_Id,
        int CatMP_Id,
        int? SubCatMP_Id,
        decimal Precio);
}
