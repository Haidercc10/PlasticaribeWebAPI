using System.Data;
using System.Globalization;
using System.ServiceModel;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Interfaces;
using ServiceReference1;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Opciones del servicio SOAP genérico de Zeus (sección <c>ZeusSoap</c> de la configuración).
    /// La contraseña se toma de user-secrets o variables de entorno, nunca del código.
    /// </summary>
    public sealed class ZeusSoapOptions
    {
        /// <summary>Nombre de la sección de configuración.</summary>
        public const string Seccion = "ZeusSoap";

        /// <summary>URL del servicio ServiceWS.asmx.</summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>Usuario del servicio SOAP.</summary>
        public string Usuario { get; set; } = string.Empty;

        /// <summary>Contraseña del servicio SOAP.</summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>Módulo (Action) de Zeus al que se envía el artículo.</summary>
        public string Action { get; set; } = string.Empty;

        /// <summary>Propiedad dinámica del servicio SOAP.</summary>
        public string DynamicProperty { get; set; } = "7";
    }

    /// <summary>
    /// Crea artículos de inventario en Zeus mediante el servicio SOAP genérico y consulta su existencia
    /// a través del sinónimo <c>dbo.zeus_ArticuloItems</c> de PlasticaribeBDD.
    /// </summary>
    public sealed class ZeusArticuloService : IZeusArticuloService
    {
        private readonly dataContext _context;
        private readonly ZeusSoapOptions _opciones;

        /// <summary>
        /// Inicializa el servicio con el contexto de datos y las opciones del SOAP de Zeus.
        /// </summary>
        /// <param name="context">Contexto de base de datos de Plasticaribe.</param>
        /// <param name="opciones">Opciones de la sección <c>ZeusSoap</c>.</param>
        public ZeusArticuloService(dataContext context, IOptions<ZeusSoapOptions> opciones)
        {
            _context = context;
            _opciones = opciones.Value;
        }

        /// <summary>
        /// Convierte la presentación recibida (nombre de Plasticaribe, unidad de BagPro o código Zeus) a la presentación Zeus,
        /// con las mismas reglas de TR_CrearOTs: Kilo→KLS, Unidad→UND, Paquete→PAQ, Metro/MTS→MTS.
        /// </summary>
        /// <param name="presentacion">Presentación recibida (no distingue mayúsculas).</param>
        /// <returns>Presentación Zeus o <c>null</c> si no es reconocida.</returns>
        public static string? PresentacionZeus(string? presentacion)
        {
            switch (presentacion?.Trim().ToUpperInvariant())
            {
                case "KILO": case "KG": case "KLS": return "KLS";
                case "UNIDAD": case "UND": return "UND";
                case "PAQUETE": case "PAQ": return "PAQ";
                case "METRO": case "MTS": return "MTS";
                default: return null;
            }
        }

        /// <summary>
        /// Grupo de inventario Zeus según el tipo de producto (formato), igual que TR_CrearOTs:
        /// bolsa/camisilla 00301, tubular 00302, lámina 00303, precorte 00304, hoja 00305; cualquier otro 00301.
        /// </summary>
        /// <param name="tpProdId">Tipo de producto (TpProd_Id).</param>
        /// <returns>Código del grupo Zeus.</returns>
        public static string GrupoZeus(int tpProdId) => tpProdId switch
        {
            1 => "00303",
            2 or 3 or 7 => "00301",
            4 or 5 => "00302",
            6 => "00304",
            8 => "00305",
            _ => "00301",
        };

        /// <inheritdoc />
        public async Task<List<ArticuloZeusResumen>> ObtenerArticulosAsync(string codigo, CancellationToken cancellationToken = default)
        {
            // Columnas de Inventario_Prea21.dbo.Articulo (ver InsertarArticulo_Zeus); los textos se convierten a varchar por si son text/char.
            var articulos = await _context.Database
                .SqlQueryRaw<ArticuloZeusResumen>(
                    @"SELECT CAST(Codigo AS varchar(50)) AS Codigo,
                             CAST(Presentacion AS varchar(10)) AS Presentacion,
                             CAST(Nombre AS varchar(200)) AS Nombre,
                             CAST(Descripcion AS varchar(500)) AS Descripcion,
                             CAST(Grupo AS varchar(50)) AS Grupo,
                             CAST(GrupoFacturacion AS varchar(50)) AS GrupoFacturacion,
                             CAST(PrecioVenta AS decimal(18,4)) AS Precio,
                             CAST(CostoArticulo AS decimal(18,4)) AS Costo
                      FROM dbo.zeus_ArticuloItems
                      WHERE Codigo = @codigo",
                    ParametroVarchar("@codigo", codigo))
                .ToListAsync(cancellationToken);

            foreach (var a in articulos)
            {
                a.Codigo = a.Codigo.Trim();
                a.Presentacion = (a.Presentacion ?? string.Empty).Trim();
                a.Nombre = a.Nombre?.Trim();
                a.Descripcion = a.Descripcion?.Trim();
                a.Grupo = a.Grupo?.Trim();
                a.GrupoFacturacion = a.GrupoFacturacion?.Trim();
            }
            return articulos
                .Where(a => a.Presentacion != string.Empty)
                .OrderBy(a => a.Presentacion)
                .ToList();
        }

        /// <inheritdoc />
        public async Task<bool> ExisteArticuloAsync(string codigo, string presentacionZeus, CancellationToken cancellationToken = default)
        {
            var filas = await _context.Database
                .SqlQueryRaw<int>(
                    "SELECT TOP(1) 1 AS [Value] FROM dbo.zeus_ArticuloItems WHERE Codigo = @codigo AND Presentacion = @presentacion",
                    ParametroVarchar("@codigo", codigo),
                    ParametroVarchar("@presentacion", presentacionZeus))
                .ToListAsync(cancellationToken);

            return filas.Count > 0;
        }

        /// <inheritdoc />
        public async Task<SoapResponse> EnviarArticuloAsync(ArticuloZeusDatos datos)
        {
            if (string.IsNullOrWhiteSpace(_opciones.Url) || string.IsNullOrWhiteSpace(_opciones.Action))
                throw new InvalidOperationException("El servicio SOAP de Zeus no está configurado (ZeusSoap:Url / ZeusSoap:Action).");

            var request = new SoapRequestAction
            {
                User = _opciones.Usuario,
                Password = _opciones.Password,
                Body = ConstruirXml(datos),
                DynamicProperty = _opciones.DynamicProperty,
                Action = _opciones.Action,
                TypeSQL = "true",
            };

            var binding = new BasicHttpBinding
            {
                Name = "BasicHttpBinding_IFakeService",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            var client = new WebservicesGenericoZeusSoapClient(binding, new EndpointAddress(_opciones.Url));
            try
            {
                var response = await client.ExecuteActionSOAPAsync(request);
                await client.CloseAsync();
                return response;
            }
            catch
            {
                client.Abort();
                throw;
            }
        }

        /// <summary>
        /// Arma el XML <c>&lt;Articulo&gt;</c> con operación de inserción. Los valores se escapan al construirse con
        /// <see cref="XElement"/>; los valores fijos (cuentas, IVA, complementos) son los del método original.
        /// </summary>
        /// <param name="datos">Datos del artículo.</param>
        /// <returns>XML del artículo listo para enviarse en el cuerpo de la solicitud SOAP.</returns>
        private static string ConstruirXml(ArticuloZeusDatos datos)
        {
            var inv = CultureInfo.InvariantCulture;
            var articulo = new XElement("Articulo",
                new XElement("Op", "I"),
                new XElement("Codigo", datos.Codigo),
                new XElement("Nombre", datos.Nombre),
                new XElement("Descripcion", datos.Descripcion),
                new XElement("Grupo", datos.Grupo),
                new XElement("GrupoAuxiliar", datos.Grupo),
                new XElement("Presentacion", datos.Presentacion),
                datos.GrupoFacturacion is null ? null : new XElement("GrupoFacturacion", datos.GrupoFacturacion),
                new XElement("CostoPromedio", datos.Costo.ToString(inv)),
                new XElement("Tipo", "PRODUCTO TERMINADO"),
                new XElement("Valorizacion", "PROMEDIO"),
                new XElement("Categoria", "Gravado"),
                new XElement("PorcentajeIva", "19.000000"),
                new XElement("CuentaIVA", "24081005"),
                new XElement("PrecioVenta", datos.PrecioVenta.ToString(inv)),
                new XElement("DescripcionOtroIdioma", datos.Descripcion),
                new XElement("ComplementoCosto", "2"),
                new XElement("DesHabilitado", "0"),
                new XElement("DiasGarantia", "0"),
                new XElement("ComplementoVenta", "2"),
                new XElement("ConfiguracionPrecioVenta", "4"),
                new XElement("ComplementoInventario", "4"),
                new XElement("ComplementoDevolucionVentas1", "2"),
                new XElement("CuentaIVAVentas", "24080505"),
                new XElement("CuentaIVAVentas", "24080505"),
                new XElement("CuentaIVADevolucionVentas", "24081020"),
                new XElement("ComplementoInventarioRemisionado", "4"),
                new XElement("ComplementoCentroCosto", "4"),
                new XElement("ComplementoVenta1", "4"),
                new XElement("TipoArticulo", "G"),
                new XElement("UnidadesContenidaEmpaque", "1.0000"),
                new XElement("ArticuloBolsaAgropecuaria", "N"),
                new XElement("MaximoIVADescontable", "19.0000"),
                new XElement("ModificarCantidadAlistamientoPorVerificacion", "0"));

            return articulo.ToString(SaveOptions.DisableFormatting);
        }

        /// <summary>
        /// Crea un parámetro varchar para evitar conversiones implícitas a nvarchar en la consulta al servidor vinculado.
        /// </summary>
        /// <param name="nombre">Nombre del parámetro.</param>
        /// <param name="valor">Valor del parámetro.</param>
        /// <returns>Parámetro SQL varchar(50).</returns>
        private static SqlParameter ParametroVarchar(string nombre, string valor) =>
            new(nombre, SqlDbType.VarChar, 50) { Value = valor };
    }
}
