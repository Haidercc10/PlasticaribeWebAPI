using PlasticaribeAPI.Service;
using Xunit;

namespace PlasticaribeAPI.Tests;

public class SolicitudAsignacionReglasTests
{
    [Fact]
    public void CalcularEstado_sin_asignaciones_es_pendiente()
    {
        var estado = SolicitudesMaterialService.CalcularEstado(
        [
            new CantidadSubcategoria(1, 100m, 0m),
            new CantidadSubcategoria(2, 50m, 0m)
        ]);

        Assert.Equal(EstadosSolicitudMaterial.Pendiente, estado);
    }

    [Fact]
    public void CalcularEstado_con_una_subcategoria_completa_y_otra_sin_entregar_es_parcial()
    {
        var estado = SolicitudesMaterialService.CalcularEstado(
        [
            new CantidadSubcategoria(1, 100m, 100m),
            new CantidadSubcategoria(2, 50m, 0m)
        ]);

        Assert.Equal(EstadosSolicitudMaterial.Parcial, estado);
    }

    [Fact]
    public void CalcularEstado_con_todas_cubiertas_es_finalizada_aunque_alguna_exceda()
    {
        var estado = SolicitudesMaterialService.CalcularEstado(
        [
            new CantidadSubcategoria(1, 100m, 100m),
            new CantidadSubcategoria(2, 50m, 60m)
        ]);

        Assert.Equal(EstadosSolicitudMaterial.Finalizada, estado);
    }

    [Fact]
    public void CalcularEstado_sin_detalles_es_pendiente()
    {
        Assert.Equal(EstadosSolicitudMaterial.Pendiente, SolicitudesMaterialService.CalcularEstado([]));
    }

    [Fact]
    public void Faltante_nunca_es_negativo()
    {
        Assert.Equal(0m, new CantidadSubcategoria(1, 50m, 80m).Faltante);
        Assert.Equal(20m, new CantidadSubcategoria(1, 50m, 30m).Faltante);
    }

    [Fact]
    public void DistribuirEntregado_llena_las_filas_en_orden_y_deja_el_excedente_en_la_ultima()
    {
        var resultado = SolicitudesMaterialService.DistribuirEntregado([(10L, 30m), (11L, 20m)], 60m);

        Assert.Equal(30m, resultado[10]);
        Assert.Equal(30m, resultado[11]);
    }

    [Fact]
    public void DistribuirEntregado_con_una_sola_fila_asigna_todo_a_esa_fila()
    {
        var resultado = SolicitudesMaterialService.DistribuirEntregado([(10L, 100m)], 40m);

        Assert.Equal(40m, Assert.Single(resultado).Value);
    }

    [Fact]
    public void RepartirFifo_toma_de_los_lotes_mas_antiguos_primero()
    {
        var reparto = ConsumoInventarioAsignacionService.RepartirFifo([(1, 30m), (2, 50m), (3, 100m)], 60m);

        Assert.NotNull(reparto);
        Assert.Equal([(1, 30m), (2, 30m)], reparto);
    }

    [Fact]
    public void RepartirFifo_omite_lotes_sin_saldo()
    {
        var reparto = ConsumoInventarioAsignacionService.RepartirFifo([(1, 0m), (2, 25m)], 25m);

        Assert.Equal([(2, 25m)], reparto);
    }

    [Fact]
    public void RepartirFifo_devuelve_null_si_los_lotes_no_alcanzan()
    {
        Assert.Null(ConsumoInventarioAsignacionService.RepartirFifo([(1, 10m), (2, 5m)], 20m));
    }

    [Theory]
    [InlineData(900, 50, 1000, 0)]
    [InlineData(900, 150, 1000, 50)]
    [InlineData(1200, 0, 1000, 0)]
    [InlineData(1200, 10, 1000, 210)]
    public void CalcularExcesoKg_solo_hay_exceso_cuando_la_asignacion_suma_kg(
        double consumoActual, double kgNuevos, double limite, double esperado)
    {
        var exceso = AsignacionMateriaPrimaService.CalcularExcesoKg((decimal)consumoActual, (decimal)kgNuevos, (decimal)limite);

        Assert.Equal((decimal)esperado, exceso);
    }

    [Theory]
    [InlineData(EstadosSolicitudMaterial.Cancelada, true)]
    [InlineData(EstadosSolicitudMaterial.Finalizada, true)]
    [InlineData(EstadosSolicitudMaterial.Pendiente, false)]
    [InlineData(EstadosSolicitudMaterial.Parcial, false)]
    public void EstaCerrada_solo_para_canceladas_y_finalizadas(int estado, bool esperado)
    {
        Assert.Equal(esperado, EstadosSolicitudMaterial.EstaCerrada(estado));
    }
}
