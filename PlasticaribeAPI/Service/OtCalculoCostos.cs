namespace PlasticaribeAPI.Service
{
    /// <summary>Datos de entrada para calcular los costos de una OT.</summary>
    /// <param name="Presentacion">Presentación de la OT (Kg, Paquete, Und, MTS).</param>
    /// <param name="Cantidad">Cantidad pedida en la presentación.</param>
    /// <param name="Precio">Precio por unidad de la presentación.</param>
    /// <param name="MargenAdicional">Margen adicional en porcentaje.</param>
    /// <param name="PesoMillar">Peso Millar base del producto, sin la regla D2.</param>
    /// <param name="TipoProducto">Nombre del tipo o formato del producto (LAMINADO, HOJA, ...).</param>
    /// <param name="CantidadPaquete">Bolsas por paquete (la que se guarda en sellado/corte).</param>
    public sealed record EntradaCostosOT(
        string Presentacion,
        decimal Cantidad,
        decimal Precio,
        decimal MargenAdicional,
        decimal PesoMillar,
        string? TipoProducto,
        decimal CantidadPaquete);

    /// <summary>Costos calculados de una OT.</summary>
    /// <param name="NetoKg">Peso neto en kilos, con margen.</param>
    /// <param name="ValorKg">Valor del kilo; null en MTS (pendiente de definir: lo decide quien llama).</param>
    /// <param name="ValorOt">Valor total de la OT.</param>
    public sealed record ResultadoCostosOT(decimal NetoKg, decimal? ValorKg, decimal ValorOt);

    /// <summary>
    /// Traducción a C# de <c>OtCalculoCostosService.ts</c> (WorkspaceAngular, PR2) para que el servidor valide los costos
    /// que envía el front (F2-03). Funciones puras. Cualquier cambio de regla debe hacerse en los dos archivos y en las dos
    /// tablas de casos (<c>OtCalculoCostosTests.cs</c> y <c>OtCalculoCostos.service.spec.ts</c>).
    /// </summary>
    public static class OtCalculoCostos
    {
        /// <summary>Tipos de producto a los que se aplica la regla D2 (opción B).</summary>
        public static readonly IReadOnlyList<string> TiposD2 = new[] { "LAMINADO", "HOJA" };

        /// <summary>Presentaciones en las que se aplica la regla D2 (opción B: solo Paquete).</summary>
        public static readonly IReadOnlyList<string> PresentacionesD2 = new[] { "Paquete" };

        /// <summary>Divisor del Peso Millar en la regla D2.</summary>
        public const decimal DivisorD2 = 2m;

        /// <summary>Diferencia máxima aceptada entre el valor enviado y el calculado, ambos redondeados a 2 decimales.</summary>
        public const decimal Tolerancia = 0.01m;

        /// <summary>
        /// Calcula los costos según la presentación.
        /// </summary>
        /// <param name="e">Datos de la OT.</param>
        /// <returns>Los costos, o null si la presentación no tiene regla de cálculo (p. ej. Rollo).</returns>
        public static ResultadoCostosOT? Calcular(EntradaCostosOT e)
        {
            switch (e.Presentacion)
            {
                case "Kg":
                    return new ResultadoCostosOT(
                        e.Cantidad + ((e.Cantidad * e.MargenAdicional) / 100m),
                        e.Precio,
                        e.Cantidad * e.Precio);

                case "Paquete":
                    {
                        var pesoMillar = PesoMillarEfectivo(e.PesoMillar, e.TipoProducto, e.Presentacion);
                        var pesoPaquete = pesoMillar > 0 && e.CantidadPaquete > 0 ? pesoMillar * (e.CantidadPaquete / 1000m) : 0m;
                        return new ResultadoCostosOT(
                            (1 + (e.MargenAdicional / 100m)) * ((pesoMillar / 1000m) * (e.Cantidad * e.CantidadPaquete)),
                            pesoPaquete > 0 ? e.Precio / pesoPaquete : 0m,
                            e.Cantidad * e.Precio);
                    }

                case "Und":
                    {
                        // D1: Valor OT = cantidad × precio siempre; el peso solo interviene en el Valor Kg.
                        var pesoMillar = PesoMillarEfectivo(e.PesoMillar, e.TipoProducto, e.Presentacion);
                        var valorOt = e.Cantidad * e.Precio;
                        var kilos = (e.Cantidad * pesoMillar) / 1000m;
                        return new ResultadoCostosOT(
                            (1 + (e.MargenAdicional / 100m)) * kilos,
                            kilos > 0 ? valorOt / kilos : 0m,
                            valorOt);
                    }

                case "MTS":
                    // Valor Kg MTS por definir (responsable): no se calcula. Neto Kg queda en 0, como en el front.
                    return new ResultadoCostosOT(0m, null, e.Cantidad * e.Precio);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Aplica la regla D2 al Peso Millar base sin modificarlo: ÷2 solo en Paquete con LAMINADO u HOJA (comparación exacta).
        /// </summary>
        /// <param name="pesoMillar">Peso Millar base.</param>
        /// <param name="tipoProducto">Nombre del tipo o formato del producto.</param>
        /// <param name="presentacion">Presentación de la OT.</param>
        /// <returns>El Peso Millar que se usa en los cálculos.</returns>
        public static decimal PesoMillarEfectivo(decimal pesoMillar, string? tipoProducto, string presentacion)
        {
            var aplica = TiposD2.Contains(tipoProducto ?? string.Empty) && PresentacionesD2.Contains(presentacion);
            return aplica ? pesoMillar / DivisorD2 : pesoMillar;
        }

        /// <summary>
        /// Indica si dos valores coinciden dentro de la tolerancia, después de redondearlos a 2 decimales (precisión de la BD).
        /// </summary>
        /// <param name="enviado">Valor recibido del front.</param>
        /// <param name="calculado">Valor calculado por el servidor.</param>
        /// <returns>true si la diferencia no supera <see cref="Tolerancia"/>.</returns>
        public static bool Coincide(decimal enviado, decimal calculado) =>
            Math.Abs(Math.Round(enviado, 2, MidpointRounding.AwayFromZero) - Math.Round(calculado, 2, MidpointRounding.AwayFromZero)) <= Tolerancia;

        /// <summary>
        /// Compara los costos enviados con los calculados. No valida el Valor Kg cuando el cálculo no lo define (MTS).
        /// </summary>
        /// <param name="calculado">Costos calculados por el servidor.</param>
        /// <param name="netoKg">Peso neto enviado.</param>
        /// <param name="valorKg">Valor del kilo enviado.</param>
        /// <param name="valorOt">Valor de la OT enviado.</param>
        /// <returns>Descripción de cada diferencia (vacío si todo coincide).</returns>
        public static IReadOnlyList<string> Diferencias(ResultadoCostosOT calculado, decimal netoKg, decimal valorKg, decimal valorOt)
        {
            var diferencias = new List<string>();
            if (!Coincide(valorOt, calculado.ValorOt))
                diferencias.Add($"Valor OT: enviado {valorOt:0.##}, esperado {calculado.ValorOt:0.##}");
            if (calculado.ValorKg.HasValue && !Coincide(valorKg, calculado.ValorKg.Value))
                diferencias.Add($"Valor Kg: enviado {valorKg:0.##}, esperado {calculado.ValorKg.Value:0.##}");
            if (!Coincide(netoKg, calculado.NetoKg))
                diferencias.Add($"Neto Kg: enviado {netoKg:0.##}, esperado {calculado.NetoKg:0.##}");
            return diferencias;
        }
    }
}
