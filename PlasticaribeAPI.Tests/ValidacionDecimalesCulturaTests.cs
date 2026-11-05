using System.ComponentModel.DataAnnotations;
using System.Globalization;
using PlasticaribeAPI.Models;
using Xunit;

namespace PlasticaribeAPI.Tests;

/// <summary>
/// Los límites decimales de [Range] deben interpretarse igual sin importar la cultura del servidor (es-CO usa coma decimal).
/// </summary>
public class ValidacionDecimalesCulturaTests
{
    [Theory]
    [InlineData(0.5, true)]
    [InlineData(0.01, true)]
    [InlineData(0, false)]
    public void Cantidades_se_validan_con_cultura_colombiana(double cantidad, bool esValida)
    {
        var culturaOriginal = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("es-CO");
        try
        {
            Assert.Equal(esValida, EsValido(new SolicitudExtrusionDetalleRequest { SubCatMP_Id = 1, Cantidad = (decimal)cantidad, UndMed_Id = "Kg" }));
            Assert.Equal(esValida, EsValido(new RegistrarAsignacionMaterialRequest { TipoMaterial = TipoMaterialInventario.MateriaPrima, MaterialId = 1, Cantidad = (decimal)cantidad, ProcesoId = "EXT" }));
            Assert.Equal(esValida, EsValido(new ConsumoInventarioLoteRequest { TipoMaterial = TipoMaterialInventario.Tinta, MaterialId = 1, LoteId = 1, Cantidad = (decimal)cantidad }));
        }
        finally
        {
            CultureInfo.CurrentCulture = culturaOriginal;
        }
    }

    private static bool EsValido(object modelo) =>
        Validator.TryValidateObject(modelo, new ValidationContext(modelo), new List<ValidationResult>(), validateAllProperties: true);
}
