using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using Microsoft.Data.SqlClient;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Interfaces;
using PlasticaribeAPI.Models;
using PlasticaribeAPI.Service;
using ServiceReference1;
using System.Globalization;
using System.Security.Claims;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly dataContext _context;

        /// <summary>Roles autorizados para crear artículos en Zeus (también se usa en la ficha complementaria del item).</summary>
        internal static readonly HashSet<string> RolesCreanArticuloZeus = new() { "1", "98" };

        public ProductoController(dataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// PDF del arte del item, guardado por Aseguramiento de la Calidad en la carpeta <c>Artes:Carpeta</c> de la
        /// configuración con el nombre <c>{prodId}.pdf</c>. Se devuelve para mostrarlo (inline); el front puede descargarlo.
        /// </summary>
        /// <param name="prodId">Producto (item). Al ser entero, el nombre del archivo no puede salirse de la carpeta.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        /// <returns>200 con el PDF; 404 si el item no tiene arte; 503 si la carpeta no está configurada o no se puede leer.</returns>
        [HttpGet("arte/{prodId:int}")]
        public IActionResult GetArte(int prodId, [FromServices] IConfiguration configuration)
        {
            var carpeta = configuration["Artes:Carpeta"];
            if (string.IsNullOrWhiteSpace(carpeta))
                return Problem("La carpeta de artes no está configurada (Artes:Carpeta).", statusCode: StatusCodes.Status503ServiceUnavailable);

            var ruta = Path.Combine(carpeta.Trim(), $"{prodId}.pdf");
            try
            {
                if (!System.IO.File.Exists(ruta))
                {
                    // File.Exists también da false si la carpeta no es accesible: se distingue para avisar bien
                    if (!Directory.Exists(carpeta.Trim()))
                        return Problem($"No se puede acceder a la carpeta de artes ({carpeta}).", statusCode: StatusCodes.Status503ServiceUnavailable);
                    return Problem($"El item {prodId} no tiene arte cargado ({prodId}.pdf) en la carpeta de artes de Aseguramiento de la Calidad.",
                        statusCode: StatusCodes.Status404NotFound);
                }
                return PhysicalFile(ruta, "application/pdf");
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                return Problem($"No se pudo leer el arte del item {prodId}: {ex.Message}", statusCode: StatusCodes.Status503ServiceUnavailable);
            }
        }

        /// <summary>
        /// Historial de nombres (referencia) de un producto: cada cambio hecho al crear o editar una OT, del más reciente al más antiguo.
        /// </summary>
        /// <param name="prodId">Producto (item).</param>
        /// <param name="cancellationToken">Token para cancelar la consulta.</param>
        /// <returns>Lista (vacía si el nombre nunca cambió).</returns>
        [HttpGet("historialNombres/{prodId}")]
        public async Task<ActionResult> GetHistorialNombres(int prodId, CancellationToken cancellationToken)
        {
            var historial = await _context.Productos_HistorialNombres.AsNoTracking()
                .Where(h => h.Prod_Id == prodId)
                .OrderByDescending(h => h.ProdHN_Fecha).ThenByDescending(h => h.ProdHN_Id)
                .Select(h => new
                {
                    fecha = h.ProdHN_Fecha,
                    nombreAnterior = h.ProdHN_NombreAnterior,
                    nombreNuevo = h.ProdHN_NombreNuevo,
                    numero_OT = h.Ot!.Numero_OT,
                    origen = h.ProdHN_Origen,
                    usuario = h.Usua!.Usua_Nombre,
                })
                .ToListAsync(cancellationToken);
            return Ok(historial);
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            if (_context.Productos == null)
            {
                return NotFound();
            }
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            if (_context.Productos == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpGet("consultaNombreItem/{letras}")]
        public ActionResult GetItem(string letras)
        {
            var productos = _context.Productos.Where(p => p.Prod_Nombre.StartsWith(letras))
                                              .Select(p => new { p.Prod_Id, p.Prod_Nombre })
                                              .Take(30)
                                              .ToList();

            return Ok(productos);
        }

        [HttpGet("IdProducto/{Prod_Id}")]
        public ActionResult<Producto> GetNombreCliente(int Prod_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Productos.Where(p => p.Prod_Id == Prod_Id)
                .Select(p => new
                {
                    p.Prod_Id,
                    p.Prod_Nombre,
                    p.Prod_Ancho,
                    p.Prod_Fuelle,
                    p.Prod_Calibre,
                    p.Prod_Largo,
                    p.UndMedACF,
                    p.TpProd.TpProd_Nombre,
                    p.MaterialMP.Material_Nombre,
                    p.Pigmt.Pigmt_Nombre,
                    p.Prod_Descripcion,
                    p.Prod_Peso_Millar,
                    p.Prod_Peso,
                    p.UndMedPeso,

                })
                .ToList();

            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(producto);
            }
        }

        [HttpGet("consultaNombreProducto/{Id}")]
        public ActionResult GetNombreProducto(int Id)
        {
            var productos = from p in _context.Set<Producto>()
                            where p.Prod_Id == Id
                            select p.Prod_Nombre;

            return Ok(productos);
        }

        //Funcion que va a consultar el id del ultimo producto creado
        [HttpGet("getIdUltimoProducto")]
        public ActionResult GetIdUltimoProducto()
        {
            var con = (from prod in _context.Set<Producto>()
                       orderby prod.Prod_Id descending
                       select prod.Prod_Id).FirstOrDefault();
            return Ok(con);
        }

        // Funcion que obtendrá toda la información de un producto basamdose en su Id y Presentacion
        [HttpGet("getInfoProducto_Prod_Presentacion/{prod}/{pres}")]
        public ActionResult getInfoProducto_Prod_Presentacion(int prod, string pres)
        {
            var con = from exis in _context.Set<Existencia_Productos>()
                      from produ in _context.Set<Producto>()
                      where exis.Prod_Id == prod
                            && exis.UndMed_Id == pres
                            && exis.Prod_Id == produ.Prod_Id
                      select new
                      {
                          produ,
                          exis,
                          Tipo_Producto = produ.TpProd.TpProd_Nombre,
                          Tipo_Sellado = produ.TiposSellados.TpSellados_Nombre,
                          Material = produ.MaterialMP.Material_Nombre,
                          Pigmento = produ.Pigmt.Pigmt_Nombre,
                          PrecioUnidad = exis.ExProd_PrecioVenta
                      };

            return Ok(con);
        }

        // Funcion que realizará una consulta en la base de datos para obtener información de los productos cuando el nombre de estos tenga la información recibida como parametro
        [HttpGet("getProductsByName/{name}")]
        public ActionResult GetProductsByName(string name)
        {
            var products = from prod in _context.Set<Producto>()
                           join exis in _context.Set<Existencia_Productos>() on prod.Prod_Id equals exis.Prod_Id
                           where prod.Prod_Nombre.Contains(name)
                           select new
                           {
                               prod,
                               exis
                           };
            return products.Any() ? Ok(products) : NotFound();
        }

        // Funcion que realizará una consulta en la base de datos para obtener información de los productos cuando el nombre de estos tenga la información recibida como parametro
        [HttpGet("getProductsById/{id}")]
        public ActionResult GetProductsById(int id)
        {
            var products = from prod in _context.Set<Producto>()
                           join exis in _context.Set<Existencia_Productos>() on prod.Prod_Id equals exis.Prod_Id
                           where prod.Prod_Id == id
                           select new
                           {
                               prod,
                               exis
                           };
            return products.Any() ? Ok(products) : NotFound();
        }

        /// <summary>
        /// Endpoint heredado: crea en Zeus el artículo del item con los valores históricos (grupo 00301, presentación igual a la
        /// descripción, costo igual al precio de venta de Existencias_Productos). Mantiene el contrato anterior hacia el front.
        /// </summary>
        /// <param name="item">Código del item (Prod_Id).</param>
        /// <param name="zeus">Servicio de artículos Zeus.</param>
        /// <returns>200 con la respuesta SOAP si Zeus responde SUCCESS; 400 con la respuesta si falla; 404 si el item no existe;
        /// 500 si ocurre una excepción.</returns>
        [HttpGet("PostArticuloZeus/{item}")]
        public async Task<ActionResult> PostArticuloZeus(string item, [FromServices] IZeusArticuloService zeus)
        {
            if (!int.TryParse(item, out int prodId))
                return NotFound();

            var article = await (from p in _context.Set<Producto>()
                                join exis in _context.Set<Existencia_Productos>() on p.Prod_Id equals exis.Prod_Id
                                where p.Prod_Id == prodId
                                select new
                                {
                                    Item = p.Prod_Id,
                                    Referencia = p.Prod_Nombre,
                                    Descripcion = p.Prod_Descripcion,
                                    PrecioVenta = exis.ExProd_PrecioVenta,
                                }).FirstOrDefaultAsync();

            if (article == null) {
                return NotFound();
            }

            try
            {
                decimal precio = article.PrecioVenta ?? 0m;
                var response = await zeus.EnviarArticuloAsync(new ArticuloZeusDatos
                {
                    Codigo = article.Item.ToString(CultureInfo.InvariantCulture),
                    Nombre = article.Referencia ?? string.Empty,
                    Descripcion = article.Descripcion ?? string.Empty,
                    Grupo = "00301",
                    Presentacion = (article.Descripcion ?? string.Empty) + " ",
                    Costo = precio,
                    PrecioVenta = precio,
                });
                return response.Status == Status.SUCCESS ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Crea en Zeus el artículo de un producto existente en Plasticaribe para una presentación, si aún no existe.
        /// Grupo según el tipo de producto, GrupoFacturacion = grupo + presentación, Precio = precio recibido y
        /// Costo = precio × 0,75. Solo roles 1 y 98.
        /// </summary>
        /// <param name="codigo">Código del producto (Prod_Id).</param>
        /// <param name="presentacion">Presentación: Kilo/Kg/KLS, Unidad/Und/UND, Paquete/PAQ, Metro/MTS.</param>
        /// <param name="body">Precio unitario de venta (obligatorio, mayor que cero).</param>
        /// <param name="zeus">Servicio de artículos Zeus.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>200 con <c>{ creado, yaExistia }</c>; 400 si la presentación o el precio no son válidos; 403 si el rol no
        /// está autorizado; 404 si el producto no existe; 502 si falla la consulta a Zeus o el servicio SOAP rechaza el artículo.</returns>
        [HttpPost("{codigo:int}/articuloZeus/{presentacion}")]
        public async Task<ActionResult<CrearArticuloZeusResponse>> CrearArticuloZeus(int codigo, string presentacion,
            [FromBody] CrearArticuloZeusRequest body, [FromServices] IZeusArticuloService zeus, CancellationToken cancellationToken)
        {
            if (!RolesCreanArticuloZeus.Contains(User.FindFirstValue(ClaimTypes.Role) ?? string.Empty))
                return Forbid();

            string? presentacionZeus = ZeusArticuloService.PresentacionZeus(presentacion);
            if (presentacionZeus == null)
                return BadRequest($"La presentación '{presentacion}' no es válida. Use Kilo, Unidad, Paquete o Metro.");
            if (body?.Precio is not decimal precio || precio <= 0)
                return BadRequest("El precio es obligatorio y debe ser mayor que cero.");

            var producto = await _context.Set<Producto>()
                .Where(p => p.Prod_Id == codigo)
                .Select(p => new { p.Prod_Id, p.Prod_Nombre, p.Prod_Descripcion, p.TpProd_Id })
                .FirstOrDefaultAsync(cancellationToken);
            if (producto == null)
                return NotFound($"El producto {codigo} no existe en Plasticaribe.");

            string codigoZeus = codigo.ToString(CultureInfo.InvariantCulture);
            try
            {
                if (await zeus.ExisteArticuloAsync(codigoZeus, presentacionZeus, cancellationToken))
                    return Ok(new CrearArticuloZeusResponse { Creado = false, YaExistia = true });
            }
            catch (SqlException ex)
            {
                return Problem($"No se pudo consultar el artículo en Zeus: {ex.Message}", statusCode: StatusCodes.Status502BadGateway);
            }

            string grupo = ZeusArticuloService.GrupoZeus(producto.TpProd_Id);
            SoapResponse response;
            try
            {
                response = await zeus.EnviarArticuloAsync(new ArticuloZeusDatos
                {
                    Codigo = codigoZeus,
                    Nombre = producto.Prod_Nombre,
                    Descripcion = string.IsNullOrWhiteSpace(producto.Prod_Descripcion) ? producto.Prod_Nombre : producto.Prod_Descripcion,
                    Grupo = grupo,
                    Presentacion = presentacionZeus,
                    GrupoFacturacion = grupo + presentacionZeus,
                    PrecioVenta = precio,
                    Costo = Math.Round(precio * 0.75m, 4),
                });
            }
            catch (Exception ex) when (ex is CommunicationException or TimeoutException or InvalidOperationException)
            {
                return Problem($"No se pudo comunicar con el servicio de Zeus: {ex.Message}", statusCode: StatusCodes.Status502BadGateway);
            }

            if (response.Status != Status.SUCCESS)
                return Problem($"Zeus rechazó el artículo: {response.Message}", statusCode: StatusCodes.Status502BadGateway);

            return Ok(new CrearArticuloZeusResponse { Creado = true, YaExistia = false });
        }

        /// <summary>
        /// Artículos de Zeus del producto (uno por presentación: KLS, UND, PAQ, MTS) con nombre, descripción, grupo,
        /// grupo de facturación, precio y costo, para mostrar el estado de sincronización con Zeus.
        /// </summary>
        /// <param name="codigo">Código del producto (Prod_Id).</param>
        /// <param name="zeus">Servicio de artículos Zeus.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>200 con la lista de artículos (vacía si no existe en Zeus); 502 si falla la consulta a Zeus.</returns>
        [HttpGet("{codigo:int}/articulosZeus")]
        public async Task<ActionResult<List<ArticuloZeusResumen>>> GetArticulosZeus(int codigo, [FromServices] IZeusArticuloService zeus,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await zeus.ObtenerArticulosAsync(codigo.ToString(CultureInfo.InvariantCulture), cancellationToken));
            }
            catch (SqlException ex)
            {
                return Problem($"No se pudo consultar el artículo en Zeus: {ex.Message}", statusCode: StatusCodes.Status502BadGateway);
            }
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Prod_Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Funcion que va a actualizar el estado del producto
        [HttpPut("putEstadoProducto/{id}")]
        public ActionResult PutEstadoProducto(int id, Producto producto)
        {
            if (id != producto.Prod_Id)
            {
                return BadRequest();
            }
            try
            {
                var con = _context.Productos.Where(x => x.Prod_Id == id).First<Producto>();
                con.Estado_Id = producto.Estado_Id;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'dataContext.Productos'  is null.");
            }
            _context.Productos.Add(producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductoExists(producto.Prod_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProducto", new { id = producto.Prod_Id }, producto);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            if (_context.Productos == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.Prod_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
