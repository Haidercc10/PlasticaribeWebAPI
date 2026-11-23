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
    public void CalcularEstado_con_todas_cubiertas_es_completada_aunque_alguna_exceda()
    {
        var estado = SolicitudesMaterialService.CalcularEstado(
        [
            new CantidadSubcategoria(1, 100m, 100m),
            new CantidadSubcategoria(2, 50m, 60m)
        ]);

        Assert.Equal(EstadosSolicitudMaterial.Completada, estado);
    }

    [Fact]
    public void CalcularEstado_se_mide_por_el_total_de_kg_y_no_por_subcategoria()
    {
        // 150 solicitados y 150 entregados, aunque una subcategoría quedó corta.
        var estado = SolicitudesMaterialService.CalcularEstado(
        [
            new CantidadSubcategoria(1, 100m, 120m),
            new CantidadSubcategoria(2, 50m, 30m)
        ]);

        Assert.Equal(EstadosSolicitudMaterial.Completada, estado);
    }

    [Theory]
    // OT de 5000 kg: tope 5250. Llegar a 5000 no la completa sola; llegar a 5250 sí.
    [InlineData(EstadosSolicitudMaterial.Pendiente, 5000, 0, EstadosSolicitudMaterial.Pendiente)]
    [InlineData(EstadosSolicitudMaterial.Pendiente, 5000, 1000, EstadosSolicitudMaterial.Parcial)]
    [InlineData(EstadosSolicitudMaterial.Parcial, 5000, 5000, EstadosSolicitudMaterial.Parcial)]
    [InlineData(EstadosSolicitudMaterial.Parcial, 5000, 5250, EstadosSolicitudMaterial.Completada)]
    [InlineData(EstadosSolicitudMaterial.Parcial, 5000, 5300, EstadosSolicitudMaterial.Completada)]
    // Completada, finalizada y verificada no se deshacen aunque baje lo asignado (devoluciones).
    [InlineData(EstadosSolicitudMaterial.Completada, 5000, 4000, EstadosSolicitudMaterial.Completada)]
    [InlineData(EstadosSolicitudMaterial.Finalizada, 5000, 0, EstadosSolicitudMaterial.Finalizada)]
    [InlineData(EstadosSolicitudMaterial.Verificada, 5000, 0, EstadosSolicitudMaterial.Verificada)]
    public void CalcularEstadoOT_se_completa_sola_al_llegar_al_tope(int actual, double kgOT, double asignado, int esperado)
    {
        Assert.Equal(esperado, SolicitudesMaterialService.CalcularEstadoOT(actual, (decimal)kgOT, (decimal)asignado));
    }

    [Fact]
    public void CalcularEstadoOT_sin_kg_de_la_OT_no_se_completa_sola()
    {
        Assert.Equal(EstadosSolicitudMaterial.Parcial, SolicitudesMaterialService.CalcularEstadoOT(EstadosSolicitudMaterial.Parcial, null, 99999m));
    }

    [Theory]
    [InlineData(EstadosSolicitudMaterial.Parcial, 5000, 5000, true)]   // llegó al umbral
    [InlineData(EstadosSolicitudMaterial.Parcial, 5000, 4999.99, false)] // aún no llega
    [InlineData(EstadosSolicitudMaterial.Completada, 5000, 5250, false)] // ya está completada
    public void ValidarCompletarOT_exige_llegar_al_umbral(int estado, double kgOT, double asignado, bool sePuede)
    {
        Assert.Equal(sePuede, SolicitudesMaterialService.ValidarCompletarOT(estado, (decimal)kgOT, (decimal)asignado) == null);
    }

    [Fact]
    public void ValidarCompletarOT_sin_kg_de_la_OT_no_se_puede()
    {
        Assert.NotNull(SolicitudesMaterialService.ValidarCompletarOT(EstadosSolicitudMaterial.Parcial, null, 5000m));
    }

    [Fact]
    public void ValidarFinalizarOT_exige_OT_completada_y_ninguna_solicitud_activa()
    {
        const int completada = EstadosSolicitudMaterial.Completada;
        Assert.Null(SolicitudesMaterialService.ValidarFinalizarOT(completada, [completada, completada, EstadosSolicitudMaterial.Cancelada]));
        Assert.NotNull(SolicitudesMaterialService.ValidarFinalizarOT(completada, [completada, EstadosSolicitudMaterial.Parcial]));
        Assert.NotNull(SolicitudesMaterialService.ValidarFinalizarOT(EstadosSolicitudMaterial.Parcial, [completada]));
        Assert.NotNull(SolicitudesMaterialService.ValidarFinalizarOT(completada, [EstadosSolicitudMaterial.Cancelada]));
        Assert.NotNull(SolicitudesMaterialService.ValidarFinalizarOT(EstadosSolicitudMaterial.Finalizada, [EstadosSolicitudMaterial.Finalizada]));
    }

    [Fact]
    public void MotivoOTCerrada_completada_solo_bloquea_materia_prima()
    {
        const string ext = SolicitudesMaterialService.ProcesoMateriaPrima;
        Assert.NotNull(SolicitudesMaterialService.MotivoOTCerrada(EstadosSolicitudMaterial.Completada, ext, 1));
        Assert.Null(SolicitudesMaterialService.MotivoOTCerrada(EstadosSolicitudMaterial.Completada, "IMP", 1));
        Assert.NotNull(SolicitudesMaterialService.MotivoOTCerrada(EstadosSolicitudMaterial.Finalizada, "IMP", 1));
        Assert.Null(SolicitudesMaterialService.MotivoOTCerrada(EstadosSolicitudMaterial.Parcial, ext, 1));
        Assert.Null(SolicitudesMaterialService.MotivoOTCerrada(null, ext, 1));
    }

    [Theory]
    [InlineData(5000, 5250)]
    [InlineData(591, 620.55)]
    public void TopeKgOT_suma_el_cinco_por_ciento_y_KgOTDe_lo_deduce_del_tope(double kgOT, double tope)
    {
        Assert.Equal((decimal)tope, EstadosSolicitudMaterial.TopeKgOT((decimal)kgOT));
        Assert.Equal((decimal)kgOT, SolicitudesMaterialService.KgOTDe(null, (decimal)tope));
        Assert.Equal(10m, SolicitudesMaterialService.KgOTDe(10m, (decimal)tope));
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
    [InlineData(EstadosSolicitudMaterial.Completada, true)]
    [InlineData(EstadosSolicitudMaterial.Finalizada, true)]
    [InlineData(EstadosSolicitudMaterial.Verificada, true)]
    [InlineData(EstadosSolicitudMaterial.Pendiente, false)]
    [InlineData(EstadosSolicitudMaterial.Parcial, false)]
    public void EstaCerrada_para_todo_lo_que_no_esta_pendiente_ni_parcial(int estado, bool esperado)
    {
        Assert.Equal(esperado, EstadosSolicitudMaterial.EstaCerrada(estado));
    }
}
