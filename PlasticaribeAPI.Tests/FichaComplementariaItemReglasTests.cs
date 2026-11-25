using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlasticaribeAPI.Controllers;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;
using PlasticaribeAPI.Service;
using Xunit;

namespace PlasticaribeAPI.Tests;

/// <summary>Reglas de la ficha complementaria del item (normalización, validación y upsert de campos).</summary>
public class FichaComplementariaItemReglasTests
{
    private const int ProdId = 123456;
    private const long Usuario = 900;

    [Fact]
    public void Datos_validos_no_tienen_errores()
    {
        var dto = new FichaComplementariaItemDto
        {
            Prod_Id = ProdId, Usua_Id = Usuario, Embobinado_Id = 16, CodigoBarras = "0012345678905",
            Repeticiones = "4 x 2 A", PasoGuias = "25 cm", UbicacionGuia = "Lado derecho", LargoContinuo = true,
        };
        FichaComplementariaItemReglas.Normalizar(dto);
        Assert.Empty(FichaComplementariaItemReglas.Validar(dto, ProdId, Usuario));
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(8, true)]
    [InlineData(9, true)]
    [InlineData(16, true)]
    [InlineData(17, false)]
    public void Embobinado_debe_estar_entre_1_y_16(int embobinado, bool valido)
    {
        var dto = new FichaComplementariaItemDto { Embobinado_Id = embobinado };
        var errores = FichaComplementariaItemReglas.Validar(dto, ProdId, Usuario);
        Assert.Equal(!valido, errores.ContainsKey("Embobinado_Id"));
    }

    [Theory]
    [InlineData("0001234", true)]
    [InlineData("12345678901234567890", true)]
    [InlineData("123456789012345678901", false)]
    [InlineData("12A45", false)]
    [InlineData("12 45", false)]
    [InlineData("-123", false)]
    public void Codigo_barras_solo_digitos_y_maximo_20(string codigo, bool valido)
    {
        var dto = new FichaComplementariaItemDto { CodigoBarras = codigo };
        FichaComplementariaItemReglas.Normalizar(dto);
        var errores = FichaComplementariaItemReglas.Validar(dto, ProdId, Usuario);
        Assert.Equal(!valido, errores.ContainsKey("CodigoBarras"));
    }

    [Fact]
    public void Normalizar_conserva_ceros_y_convierte_vacios_en_null()
    {
        var dto = new FichaComplementariaItemDto { CodigoBarras = "  007  ", Repeticiones = "   ", PasoGuias = "", UbicacionGuia = null };
        FichaComplementariaItemReglas.Normalizar(dto);
        Assert.Equal("007", dto.CodigoBarras);
        Assert.Null(dto.Repeticiones);
        Assert.Null(dto.PasoGuias);
        Assert.Null(dto.UbicacionGuia);
    }

    [Fact]
    public void Textos_largos_se_rechazan()
    {
        var dto = new FichaComplementariaItemDto
        {
            Repeticiones = new string('R', 51), PasoGuias = new string('P', 51), UbicacionGuia = new string('U', 101),
        };
        var errores = FichaComplementariaItemReglas.Validar(dto, ProdId, Usuario);
        Assert.Contains("Repeticiones", errores.Keys);
        Assert.Contains("PasoGuias", errores.Keys);
        Assert.Contains("UbicacionGuia", errores.Keys);
    }

    [Fact]
    public void Prod_Id_y_Usua_Id_del_cuerpo_deben_coincidir_con_ruta_y_token()
    {
        var dto = new FichaComplementariaItemDto { Prod_Id = ProdId + 1, Usua_Id = Usuario + 1 };
        var errores = FichaComplementariaItemReglas.Validar(dto, ProdId, Usuario);
        Assert.Contains("Prod_Id", errores.Keys);
        Assert.Contains("Usua_Id", errores.Keys);
    }

    [Fact]
    public void Aplicar_actualiza_datos_sin_tocar_auditoria()
    {
        var creada = new DateTime(2026, 11, 21, 8, 0, 0);
        var ficha = new Ficha_Complementaria_Item { Prod_Id = ProdId, Usua_Id = 5, Fecha_Creacion = creada, CodigoBarras = "1" };
        var dto = new FichaComplementariaItemDto { Embobinado_Id = 3, CodigoBarras = "0099", LargoContinuo = true, MezclaAjustada = true, Usua_Id = 77, Prod_Id = 1 };

        FichaComplementariaItemReglas.Aplicar(dto, ficha);

        Assert.True(ficha.MezclaAjustada);
        Assert.True(FichaComplementariaItemReglas.ADto(ficha).MezclaAjustada);
        FichaComplementariaItemReglas.Aplicar(new FichaComplementariaItemDto(), ficha);
        Assert.False(ficha.MezclaAjustada);
        FichaComplementariaItemReglas.Aplicar(dto, ficha);

        Assert.Equal(ProdId, ficha.Prod_Id);
        Assert.Equal(5, ficha.Usua_Id);
        Assert.Equal(creada, ficha.Fecha_Creacion);
        Assert.Equal(3, ficha.Embobinado_Id);
        Assert.Equal("0099", ficha.CodigoBarras);
        Assert.True(ficha.LargoContinuo);
        Assert.Null(ficha.Repeticiones);
    }

    [Theory]
    [InlineData("2")]
    [InlineData("")]
    public async Task Put_con_rol_no_autorizado_devuelve_403(string rol)
    {
        var controller = Controlador(rol);
        var resultado = await controller.PutFicha(ProdId, new FichaComplementariaItemDto(), CancellationToken.None);
        Assert.IsType<ForbidResult>(resultado.Result);
    }

    [Fact]
    public async Task Put_sin_usuario_en_token_devuelve_401()
    {
        var controller = Controlador("1", conUsuario: false);
        var resultado = await controller.PutFicha(ProdId, new FichaComplementariaItemDto(), CancellationToken.None);
        Assert.IsType<UnauthorizedResult>(resultado.Result);
    }

    /// <summary>Controlador con un usuario autenticado del rol indicado (las pruebas no llegan a la base de datos).</summary>
    private static Ficha_Complementaria_ItemController Controlador(string rol, bool conUsuario = true)
    {
        var context = new dataContext(new DbContextOptionsBuilder<dataContext>().Options, new ConfigurationBuilder().Build());
        var claims = new List<Claim> { new(ClaimTypes.Role, rol) };
        if (conUsuario)
            claims.Add(new Claim(ClaimTypes.NameIdentifier, Usuario.ToString()));
        return new Ficha_Complementaria_ItemController(context)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test")) },
            },
        };
    }
}
