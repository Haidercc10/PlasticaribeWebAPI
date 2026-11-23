using PlasticaribeAPI.Models;
using PlasticaribeAPI.Service;
using Xunit;

namespace PlasticaribeAPI.Tests;

public class ReglasSolicitudMaterialTests
{
    private static SolicitudMaquinaRequest Maquina(int numero, decimal porcentaje) => new() { Maquina = numero, Porcentaje = porcentaje };

    private static SolicitudMaterialRequest Material(long id, decimal cantidad, TipoMaterialInventario tipo = TipoMaterialInventario.MateriaPrima) =>
        new() { TipoMaterial = tipo, MaterialId = id, Cantidad = cantidad };

    private static SolicitudExtrusionDetalleRequest Subcategoria(int id, decimal cantidad, params SolicitudMaterialRequest[] materiales) =>
        new() { SubCatMP_Id = id, Cantidad = cantidad, UndMed_Id = "Kg", Materiales = materiales.Length == 0 ? null : [.. materiales] };

    [Fact]
    public void ValidarMaquinas_sin_lista_usa_la_maquina_anterior_al_100()
    {
        var (maquinas, error) = ReglasSolicitudMaterial.ValidarMaquinas(null, 7);

        Assert.Null(error);
        Assert.Equal([new MaquinaSolicitud(7, 100m)], maquinas);
    }

    [Fact]
    public void ValidarMaquinas_sin_lista_ni_maquina_anterior_es_error()
    {
        var (maquinas, error) = ReglasSolicitudMaterial.ValidarMaquinas([], 0);

        Assert.Null(maquinas);
        Assert.NotNull(error);
    }

    [Fact]
    public void ValidarMaquinas_acepta_porcentajes_que_suman_100_en_el_orden_recibido()
    {
        var (maquinas, error) = ReglasSolicitudMaterial.ValidarMaquinas([Maquina(3, 33.33m), Maquina(1, 33.33m), Maquina(2, 33.34m)], 0);

        Assert.Null(error);
        Assert.Equal([3, 1, 2], maquinas!.Select(m => m.Maquina));
    }

    [Theory]
    [InlineData(60, 30)]   // suman 90
    [InlineData(60, 50)]   // suman 110
    public void ValidarMaquinas_rechaza_porcentajes_que_no_suman_100(int primero, int segundo)
    {
        var (_, error) = ReglasSolicitudMaterial.ValidarMaquinas([Maquina(1, primero), Maquina(2, segundo)], 0);

        Assert.NotNull(error);
    }

    [Fact]
    public void ValidarMaquinas_rechaza_maquinas_repetidas()
    {
        var (_, error) = ReglasSolicitudMaterial.ValidarMaquinas([Maquina(1, 50m), Maquina(1, 50m)], 0);

        Assert.Contains("repetir", error);
    }

    [Fact]
    public void ValidarMaquinas_rechaza_mas_de_dos_decimales()
    {
        var (_, error) = ReglasSolicitudMaterial.ValidarMaquinas([Maquina(1, 33.333m), Maquina(2, 66.667m)], 0);

        Assert.NotNull(error);
    }

    [Fact]
    public void ValidarMateriales_acepta_subcategorias_cuya_cantidad_es_la_suma_de_sus_materiales()
    {
        var error = ReglasSolicitudMaterial.ValidarMateriales([Subcategoria(10, 1500m, Material(1, 1000m), Material(2, 500m))]);

        Assert.Null(error);
    }

    [Fact]
    public void ValidarMateriales_acepta_subcategorias_sin_materiales_del_formato_anterior()
    {
        var error = ReglasSolicitudMaterial.ValidarMateriales([Subcategoria(10, 1500m)]);

        Assert.Null(error);
    }

    [Fact]
    public void ValidarMateriales_rechaza_cantidad_que_no_es_la_suma()
    {
        var error = ReglasSolicitudMaterial.ValidarMateriales([Subcategoria(10, 1400m, Material(1, 1000m), Material(2, 500m))]);

        Assert.Contains("no es la suma", error);
    }

    [Fact]
    public void ValidarMateriales_rechaza_el_mismo_material_dos_veces_en_la_solicitud()
    {
        var error = ReglasSolicitudMaterial.ValidarMateriales(
        [
            Subcategoria(10, 1000m, Material(1, 1000m)),
            Subcategoria(11, 200m, Material(1, 200m))
        ]);

        Assert.Contains("repetir", error);
    }

    [Fact]
    public void ValidarMateriales_permite_el_mismo_id_si_es_de_otro_tipo()
    {
        var error = ReglasSolicitudMaterial.ValidarMateriales(
        [
            Subcategoria(10, 100m, Material(5, 100m)),
            Subcategoria(11, 50m, Material(5, 50m, TipoMaterialInventario.Tinta))
        ]);

        Assert.Null(error);
    }

    [Fact]
    public void ValidarMateriales_rechaza_materiales_genericos()
    {
        var error = ReglasSolicitudMaterial.ValidarMateriales([Subcategoria(10, 100m, Material(SolicitudesMaterialService.MateriaPrimaGenericaId, 100m))]);

        Assert.NotNull(error);
    }

    [Theory]
    // Quien gestiona: cancela pendientes y parciales.
    [InlineData(EstadosSolicitudMaterial.Cancelada, EstadosSolicitudMaterial.Pendiente, true, false, null)]
    [InlineData(EstadosSolicitudMaterial.Cancelada, EstadosSolicitudMaterial.Parcial, true, false, null)]
    [InlineData(EstadosSolicitudMaterial.Cancelada, EstadosSolicitudMaterial.Completada, true, false, 409)]
    [InlineData(EstadosSolicitudMaterial.Cancelada, EstadosSolicitudMaterial.Finalizada, true, false, 409)]
    // Solicitante: solo cancela la suya mientras esté pendiente.
    [InlineData(EstadosSolicitudMaterial.Cancelada, EstadosSolicitudMaterial.Pendiente, false, true, null)]
    [InlineData(EstadosSolicitudMaterial.Cancelada, EstadosSolicitudMaterial.Parcial, false, true, 409)]
    [InlineData(EstadosSolicitudMaterial.Cancelada, EstadosSolicitudMaterial.Pendiente, false, false, 403)]
    // Ya no se finaliza una solicitud sola (se finaliza con su OT): solo se puede cancelar.
    [InlineData(EstadosSolicitudMaterial.Finalizada, EstadosSolicitudMaterial.Pendiente, true, false, 400)]
    [InlineData(EstadosSolicitudMaterial.Finalizada, EstadosSolicitudMaterial.Pendiente, false, true, 400)]
    [InlineData(EstadosSolicitudMaterial.Verificada, EstadosSolicitudMaterial.Finalizada, true, false, 400)]
    public void ValidarCambioEstado_aplica_las_reglas_por_rol_y_estado(int nuevo, int actual, bool esGestor, bool esDueno, int? esperado)
    {
        var resultado = ReglasSolicitudMaterial.ValidarCambioEstado(nuevo, actual, esGestor, esDueno);

        Assert.Equal(esperado, resultado?.StatusCode);
    }
}
