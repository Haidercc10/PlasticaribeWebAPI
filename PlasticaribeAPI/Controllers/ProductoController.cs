using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using ServiceReference1;
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

        [HttpGet("PostArticuloZeus/{item}")]
        public async Task<ActionResult> PostArticuloZeus(string item)
        {
            var article = await (from p in _context.Set<Producto>()
                                join exis in _context.Set<Existencia_Productos>() on p.Prod_Id equals exis.Prod_Id
                                where p.Prod_Id == Convert.ToInt32(item)
                                select new
                                {
                                    Item = p.Prod_Id,
                                    Referencia = p.Prod_Nombre,
                                    Descripcion = p.Prod_Descripcion,
                                    Costo = exis.ExProd_PrecioVenta,
                                    PrecioVenta = exis.ExProd_PrecioVenta,
                                }).FirstOrDefaultAsync();

            if (article == null) {
                return NotFound();
            }

            try
            {
                string tipo = Convert.ToString("PRODUCTO TERMINADO");
                string valorizacion = "PROMEDIO";
                string grupo = "00301";
                string precio = article.PrecioVenta.HasValue ? Convert.ToString(article.PrecioVenta.Value) : Convert.ToString(0m);

                SoapRequestAction request = new SoapRequestAction();
                request.User = "wsZeusInvProd";
                request.Password = "wsZeusInvProd";

                request.Body = "<Articulo>" +
                                    "<Op>I</Op>" +
                                        "<Codigo>" + Convert.ToString(article.Item) + "</Codigo>" +
                                        "<Nombre>" + Convert.ToString(article.Referencia) + "</Nombre>" +
                                        "<Descripcion>" + Convert.ToString(article.Descripcion) + "</Descripcion>" +
                                        "<Grupo> " + grupo + " </Grupo>" +
                                        "<GrupoAuxiliar> " + grupo + "</GrupoAuxiliar>" +
                                        "<Presentacion>" + Convert.ToString(article.Descripcion) + " </Presentacion>" +
                                        "<CostoPromedio>" + article.Costo + "</CostoPromedio>" +
                                        "<Tipo>" + tipo + "</Tipo>" +
                                        "<Valorizacion>" + valorizacion + "</Valorizacion>" +
                                        "<Categoria>Gravado</Categoria>" +
                                        "<PorcentajeIva>19.000000</PorcentajeIva>" +
                                        "<CuentaIVA>24081005</CuentaIVA>" +
                                        "<PrecioVenta>" + precio + "</PrecioVenta>" +
                                        "<DescripcionOtroIdioma>" + Convert.ToString(article.Descripcion) + "</DescripcionOtroIdioma>" +
                                        "<ComplementoCosto>2</ComplementoCosto>" +
                                        "<DesHabilitado>0</DesHabilitado>" +
                                        "<DiasGarantia>0</DiasGarantia>" +
                                        "<ComplementoVenta>2</ComplementoVenta>" +
                                        "<ConfiguracionPrecioVenta>4</ConfiguracionPrecioVenta>" +
                                        "<ComplementoInventario>4</ComplementoInventario>" +
                                        "<ComplementoDevolucionVentas1>2</ComplementoDevolucionVentas1>" +
                                        "<CuentaIVAVentas>24080505</CuentaIVAVentas>" +
                                        "<CuentaIVAVentas>24080505</CuentaIVAVentas>" +
                                        "<CuentaIVADevolucionVentas>24081020</CuentaIVADevolucionVentas>" +
                                        "<ComplementoInventarioRemisionado>4</ComplementoInventarioRemisionado>" +
                                        "<ComplementoCentroCosto>4</ComplementoCentroCosto>" +
                                        "<ComplementoVenta1>4</ComplementoVenta1>" +
                                        "<TipoArticulo>G</TipoArticulo>" +
                                        "<UnidadesContenidaEmpaque>1.0000</UnidadesContenidaEmpaque>" +
                                        "<ArticuloBolsaAgropecuaria>N</ArticuloBolsaAgropecuaria>" +
                                        "<MaximoIVADescontable>19.0000</MaximoIVADescontable>" +
                                        "<ModificarCantidadAlistamientoPorVerificacion>0</ModificarCantidadAlistamientoPorVerificacion>		 " +
                               "</Articulo>";

                request.DynamicProperty = "7";
                request.Action = "Inventario"; //modulo
                request.TypeSQL = "true"; //1

                var binding = new BasicHttpBinding()
                {
                    Name = "BasicHttpBinding_IFakeService",
                    MaxBufferSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647
                };

                var endpoint = new EndpointAddress("http://192.168.0.85/wsGenericoZeus/ServiceWS.asmx");
                WebservicesGenericoZeusSoapClient client = new WebservicesGenericoZeusSoapClient(binding, endpoint);
                SoapResponse response = await client.ExecuteActionSOAPAsync(request);
                if (Convert.ToString(response.Status) == "SUCCESS") Console.Write("Ok");
                return Convert.ToString(response.Status) == "SUCCESS" ? Ok(response) : BadRequest(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
