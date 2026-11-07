using PlasticaribeAPI.Service;
using Xunit;

namespace PlasticaribeAPI.Tests;

/// <summary>
/// Casos de referencia del cálculo de costos de OT. ESTA TABLA DEBE SER IDÉNTICA a la del front
/// (WorkspaceAngular, OtCalculoCostos.service.spec.ts): si cambia una regla, se cambian los dos archivos y las dos tablas.
/// </summary>
public class OtCalculoCostosTests
{
    /// <summary>
    /// Presentación, cantidad, precio, margen, peso millar, tipo, bolsas por paquete → neto Kg, valor Kg (null = no se calcula), valor OT.
    /// </summary>
    public static IEnumerable<object?[]> CasosDeReferencia() => new List<object?[]>
    {
        // Kg: el precio es el valor del kilo; el neto incluye el margen.
        new object?[] { "Kg", 100m, 5000m, 10m, 0m, null, 0m, 110m, 5000m, 500000m },
        // Paquete sin regla D2: peso paquete = 20 × 100 / 1000 = 2.
        new object?[] { "Paquete", 10m, 20000m, 0m, 20m, "BOLSA", 100m, 20m, 10000m, 200000m },
        // Paquete LAMINADO: D2 divide el Peso Millar entre 2 (peso paquete = 1).
        new object?[] { "Paquete", 10m, 20000m, 0m, 20m, "LAMINADO", 100m, 10m, 20000m, 200000m },
        // Paquete HOJA con margen 5%.
        new object?[] { "Paquete", 10m, 20000m, 5m, 20m, "HOJA", 100m, 10.5m, 20000m, 200000m },
        // Paquete sin Peso Millar: Valor Kg 0 (nunca división por cero).
        new object?[] { "Paquete", 10m, 20000m, 0m, 0m, "BOLSA", 100m, 0m, 0m, 200000m },
        // Paquete sin bolsas por paquete: Valor Kg 0.
        new object?[] { "Paquete", 10m, 20000m, 0m, 20m, "BOLSA", 0m, 0m, 0m, 200000m },
        // Und (D1): Valor OT = cantidad × precio; D2 no aplica fuera de Paquete aunque sea LAMINADO.
        new object?[] { "Und", 1000m, 50m, 0m, 8m, "LAMINADO", 0m, 8m, 6250m, 50000m },
        // Und sin Peso Millar: Valor Kg 0, Valor OT igual.
        new object?[] { "Und", 1000m, 50m, 0m, 0m, "BOLSA", 0m, 0m, 0m, 50000m },
        // MTS: Valor Kg por definir (null), neto 0.
        new object?[] { "MTS", 500m, 300m, 0m, 8m, null, 0m, 0m, null, 150000m },
    };

    [Theory]
    [MemberData(nameof(CasosDeReferencia))]
    public void Calcular_coincide_con_los_casos_de_referencia(
        string presentacion, decimal cantidad, decimal precio, decimal margen, decimal pesoMillar, string? tipo, decimal cantidadPaquete,
        decimal netoEsperado, decimal? valorKgEsperado, decimal valorOtEsperado)
    {
        var resultado = OtCalculoCostos.Calcular(new EntradaCostosOT(presentacion, cantidad, precio, margen, pesoMillar, tipo, cantidadPaquete));

        Assert.NotNull(resultado);
        Assert.Equal(netoEsperado, resultado!.NetoKg);
        Assert.Equal(valorKgEsperado, resultado.ValorKg);
        Assert.Equal(valorOtEsperado, resultado.ValorOt);
    }

    [Fact]
    public void Calcular_presentacion_sin_regla_devuelve_null()
    {
        Assert.Null(OtCalculoCostos.Calcular(new EntradaCostosOT("Rollo", 10m, 100m, 0m, 20m, null, 0m)));
    }

    [Fact]
    public void Calcular_repetido_da_siempre_el_mismo_resultado()
    {
        var entrada = new EntradaCostosOT("Paquete", 10m, 20000m, 0m, 20m, "LAMINADO", 100m);

        var primero = OtCalculoCostos.Calcular(entrada);
        var segundo = OtCalculoCostos.Calcular(entrada);

        Assert.Equal(primero, segundo);
        Assert.Equal(20m, entrada.PesoMillar);
    }

    [Theory]
    [InlineData(100.004, 100.0, true)]
    [InlineData(100.01, 100.0, true)]
    [InlineData(100.02, 100.0, false)]
    [InlineData(6250.333333, 6250.33, true)]
    public void Coincide_tolera_hasta_un_centavo(double enviado, double calculado, bool esperado)
    {
        Assert.Equal(esperado, OtCalculoCostos.Coincide((decimal)enviado, (decimal)calculado));
    }

    [Fact]
    public void Diferencias_no_valida_valor_kg_cuando_no_se_calcula()
    {
        var calculado = new ResultadoCostosOT(0m, null, 150000m);

        Assert.Empty(OtCalculoCostos.Diferencias(calculado, 0m, 1234m, 150000m));
    }

    [Fact]
    public void Diferencias_reporta_cada_valor_que_no_coincide()
    {
        var calculado = new ResultadoCostosOT(20m, 10000m, 200000m);

        var diferencias = OtCalculoCostos.Diferencias(calculado, 21m, 9000m, 199000m);

        Assert.Equal(3, diferencias.Count);
    }
}
