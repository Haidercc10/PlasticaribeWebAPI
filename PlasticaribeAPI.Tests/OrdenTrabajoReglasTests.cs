using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;
using PlasticaribeAPI.Service;
using Xunit;

namespace PlasticaribeAPI.Tests;

/// <summary>Reglas puras de creación y edición de OT (F2-01, F2-02, F2-03, F2-12).</summary>
public class OrdenTrabajoReglasTests
{
    private static readonly DateTime Hoy = new(2026, 11, 11, 13, 5, 9);

    private static CrearOrdenTrabajoDatosDto DatosValidos() => new()
    {
        SedeCli_Id = 11,
        Prod_Id = 500,
        UndMed_Id = "Kg",
        PedExt_Id = 517,
        Ot_CantidadPedida = 100m,
        Ot_ValorUnidad = 5000m,
        Ot_MargenAdicional = 0m,
        Ot_PesoNetoKg = 100m,
        Ot_ValorKg = 5000m,
        Ot_ValorOT = 500000m,
        Mezcla_Id = 3,
        Id_Vendedor = 50,
        Ot_FechaEntrega = Hoy.Date.AddDays(5),
    };

    [Theory]
    [InlineData("Kg", true)]
    [InlineData("Paquete", true)]
    [InlineData("Und", true)]
    [InlineData("MTS", true)]
    [InlineData("Rollo", false)]
    public void Presentaciones_permitidas_al_crear(string presentacion, bool permitida)
    {
        Assert.Equal(permitida, OrdenTrabajoReglas.PresentacionesPermitidasCrear.Contains(presentacion));
    }

    [Theory]
    [InlineData("Und", "Unidad", "UND")]
    [InlineData("Kg", "Kilo", "KLS")]
    [InlineData("Paquete", "Paquete", "PAQ")]
    [InlineData("MTS", "MTS", "MTS")]
    [InlineData("Rollo", "", "")]
    public void Equivalencias_de_presentacion_iguales_a_TR_CrearOTs(string presentacion, string bagPro, string zeus)
    {
        Assert.Equal(bagPro, OrdenTrabajoReglas.PresentacionBagPro(presentacion));
        Assert.Equal(zeus, OrdenTrabajoReglas.PresentacionZeus(presentacion));
    }

    [Fact]
    public void ValidarRangos_datos_validos_no_da_errores()
    {
        Assert.Empty(OrdenTrabajoReglas.ValidarRangos(DatosValidos(), Hoy));
    }

    [Fact]
    public void ValidarRangos_rechaza_cantidad_cero_precio_y_margen_negativos()
    {
        var datos = DatosValidos();
        datos.Ot_CantidadPedida = 0m;
        datos.Ot_ValorUnidad = -1m;
        datos.Ot_MargenAdicional = -0.5m;

        var errores = OrdenTrabajoReglas.ValidarRangos(datos, Hoy);

        Assert.Contains("Ot_CantidadPedida", errores.Keys);
        Assert.Contains("Ot_ValorUnidad", errores.Keys);
        Assert.Contains("Ot_MargenAdicional", errores.Keys);
    }

    [Fact]
    public void ValidarRangos_exige_fecha_de_entrega()
    {
        var datos = DatosValidos();
        datos.Ot_FechaEntrega = null;

        Assert.Contains("Ot_FechaEntrega", OrdenTrabajoReglas.ValidarRangos(datos, Hoy).Keys);
    }

    [Fact]
    public void ValidarRangos_rechaza_fecha_de_entrega_anterior_a_la_creacion()
    {
        var datos = DatosValidos();
        datos.Ot_FechaEntrega = Hoy.Date.AddDays(-1);

        Assert.Contains("Ot_FechaEntrega", OrdenTrabajoReglas.ValidarRangos(datos, Hoy).Keys);
    }

    [Fact]
    public void ValidarRangos_acepta_entrega_el_mismo_dia_de_la_creacion()
    {
        var datos = DatosValidos();
        datos.Ot_FechaEntrega = Hoy.Date;

        Assert.Empty(OrdenTrabajoReglas.ValidarRangos(datos, Hoy));
    }

    [Fact]
    public void ValidarRangos_al_editar_no_bloquea_una_fecha_historica_que_no_cambio()
    {
        var datos = DatosValidos();
        var creacion = new DateTime(2025, 5, 10);
        datos.Ot_FechaEntrega = new DateTime(2025, 5, 1);

        Assert.Empty(OrdenTrabajoReglas.ValidarRangos(datos, creacion, new DateTime(2025, 5, 1)));
    }

    [Theory]
    [InlineData(50001, 409)]
    [InlineData(2627, 409)]
    [InlineData(2601, 409)]
    [InlineData(547, 422)]
    [InlineData(7399, 502)]
    [InlineData(7202, 502)]
    [InlineData(8501, 502)]
    public void ClasificarErrorSql_errores_previstos(int numero, int codigoEsperado)
    {
        var clasificado = OrdenTrabajoReglas.ClasificarErrorSql(numero);

        Assert.NotNull(clasificado);
        Assert.Equal(codigoEsperado, clasificado!.Value.StatusCode);
    }

    [Theory]
    [InlineData(1205)]
    [InlineData(-2)]
    [InlineData(208)]
    public void ClasificarErrorSql_otros_errores_no_se_clasifican(int numero)
    {
        Assert.Null(OrdenTrabajoReglas.ClasificarErrorSql(numero));
    }

    [Fact]
    public void NuevaOrden_fija_numero_creador_fecha_hora_y_estado_del_servidor()
    {
        var orden = OrdenTrabajoMapeo.NuevaOrden(DatosValidos(), 130001, 77, Hoy, 5000m);

        Assert.Equal(130001, orden.Numero_OT);
        Assert.Equal(77, orden.Usua_Id);
        Assert.Equal(Hoy.Date, orden.Ot_FechaCreacion);
        Assert.Equal("13:05:09", orden.Ot_Hora);
        Assert.Equal(OrdenTrabajoReglas.EstadoInicial, orden.Estado_Id);
        Assert.Equal(0, orden.Ot_Id);
        Assert.Null(orden.Ot_UsuarioModifica);
        Assert.Equal(string.Empty, orden.Ot_Observacion);
    }

    [Fact]
    public void AplicarEdicion_conserva_creador_numero_y_campos_no_editables_y_registra_al_editor()
    {
        var orden = new Orden_Trabajo
        {
            Ot_Id = 9, Numero_OT = 120000, Usua_Id = 1, Ot_FechaCreacion = new DateTime(2026, 1, 2), Ot_Hora = "8:00:00",
            Prod_Id = 500, UndMed_Id = "Kg", SedeCli_Id = 11, Estado_Id = 15, PedExt_Id = 517, Ot_Observacion = "",
        };
        var datos = new EditarOrdenTrabajoDatosDto
        {
            Prod_Id = 999, UndMed_Id = "Und", SedeCli_Id = 99, Estado_Id = 3,
            Ot_CantidadPedida = 200m, Ot_ValorUnidad = 10m, Mezcla_Id = 4, Id_Vendedor = 51, Ot_Observacion = "NUEVA",
            Ot_FechaEntrega = new DateTime(2026, 2, 1),
        };

        OrdenTrabajoMapeo.AplicarEdicion(datos, orden, 98, Hoy, 7m);

        Assert.Equal(120000, orden.Numero_OT);
        Assert.Equal(1, orden.Usua_Id);
        Assert.Equal(new DateTime(2026, 1, 2), orden.Ot_FechaCreacion);
        Assert.Equal("8:00:00", orden.Ot_Hora);
        Assert.Equal(500, orden.Prod_Id);
        Assert.Equal("Kg", orden.UndMed_Id);
        Assert.Equal(11, orden.SedeCli_Id);
        Assert.Equal(15, orden.Estado_Id);
        Assert.Equal(517, orden.PedExt_Id);
        Assert.Equal(200m, orden.Ot_CantidadPedida);
        Assert.Equal(7m, orden.Ot_ValorKg);
        Assert.Equal(4, orden.Mezcla_Id);
        Assert.Equal(51, orden.Id_Vendedor);
        Assert.Equal("NUEVA", orden.Ot_Observacion);
        Assert.Equal(98, orden.Ot_UsuarioModifica);
        Assert.Equal(Hoy, orden.Ot_FechaModifica);
    }

    [Fact]
    public void Procesos_se_mapean_con_la_llave_en_cero_y_el_id_de_la_ot()
    {
        var extrusion = OrdenTrabajoMapeo.Extrusion(new OtExtrusionDto { Material_Id = 3, UndMed_Id = "Cms" }, 9);
        var laminado = OrdenTrabajoMapeo.Laminado(new OtLaminadoDto { Capa_Id1 = 1 }, 9);
        var sellado = OrdenTrabajoMapeo.SelladoCorte(new OtSelladoCorteDto { SelladoCorte_CantBolsasPaquete = 100m }, 9);

        Assert.Equal(0, extrusion.Extrusion_Id);
        Assert.Equal(9, extrusion.Ot_Id);
        Assert.Equal(9, laminado.OT_Id);
        Assert.Equal(0, sellado.SelladoCorte_Id);
        Assert.Equal(100m, sellado.SelladoCorte_CantBolsasPaquete);
        Assert.Null(extrusion.Orden_Trabajo);
    }
}
