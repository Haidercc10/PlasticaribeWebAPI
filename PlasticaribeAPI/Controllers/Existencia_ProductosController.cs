using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Migrations;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Existencia_ProductosController : ControllerBase
    {
        private readonly dataContext _context;

        public Existencia_ProductosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Existencia_Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Existencia_Productos>>> GetExistencias_Productos()
        {
            if (_context.Existencias_Productos == null)
            {
                return NotFound();
            }
            return await _context.Existencias_Productos.ToListAsync();
        }

        // GET: api/Existencia_Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Existencia_Productos>> GetExistencia_Productos(long id)
        {
            if (_context.Existencias_Productos == null)
            {
                return NotFound();
            }
            var existencia_Productos = await _context.Existencias_Productos.FindAsync(id);

            if (existencia_Productos == null)
            {
                return NotFound();
            }

            return existencia_Productos;
        }

        [HttpGet("IdProducto/{Prod_Id}")]
        public ActionResult<Existencia_Productos> GetNombreCliente(int Prod_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == Prod_Id)
                .Select(p => new
                {
                    p.ExProd_Id,
                    p.Prod_Id,
                    p.Prod.Prod_Nombre,
                    p.Prod.Prod_Ancho,
                    p.Prod.Prod_Fuelle,
                    p.Prod.Prod_Calibre,
                    p.Prod.Prod_Largo,
                    p.Prod.UndMedACF,
                    p.Prod.TpProd.TpProd_Nombre,
                    p.Prod.MaterialMP.Material_Nombre,
                    p.Prod.Pigmt.Pigmt_Nombre,
                    p.UndMed_Id,
                    p.ExProd_PrecioVenta,
                    p.ExProd_Cantidad,
                    p.Prod.Prod_Descripcion,
                    p.TpMoneda_Id,
                    p.Prod.Prod_Peso_Millar,
                    p.Prod.Prod_Peso,
                    p.Prod.UndMedPeso,
                    p.TpBod_Id,
                    p.Prod.TiposSellados.TpSellados_Nombre,
                    p.Prod.Prod_CantBolsasPaquete,
                    p.Prod.Prod_CantBolsasBulto,
                    p.ExProd_CantMinima

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

        /** Compara ID Producto de PBDD con Codigo Articulo de Zeus. */
        [HttpGet("IdProductoPBDDXCodigoArticuloZeus/{Prod_Id}")]
        public ActionResult<Existencia_Productos> GetCompararProductos(int Prod_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == Prod_Id)
                .Select(p => new
                {
                    p.Prod_Id,
                    p.Prod.Prod_Nombre,
                    p.ExProd_Cantidad,
                    p.UndMed_Id,
                    p.ExProd_Id,
                    p.ExProd_Precio,
                    p.ExProd_PrecioExistencia,
                    p.ExProd_PrecioSinInflacion,
                    p.TpMoneda_Id,
                    p.ExProd_PrecioVenta,
                    p.ExProd_CantMinima,
                    p.ExProd_Fecha,
                    p.ExProd_Hora
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

        [HttpGet("IdProductoPresentacion/{Prod_Id}/{UndMed_Id}")]
        public ActionResult<Existencia_Productos> GetProductoPresentacion(int Prod_Id, string UndMed_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == Prod_Id && p.UndMed_Id == UndMed_Id)
                .Select(p => new
                {
                    p.ExProd_Id,
                    p.Prod_Id,
                    p.Prod.Prod_Nombre,
                    p.Prod.Prod_Ancho,
                    p.Prod.Prod_Fuelle,
                    p.Prod.Prod_Calibre,
                    p.Prod.Prod_Largo,
                    p.Prod.UndMedACF,
                    p.Prod.TpProd.TpProd_Nombre,
                    p.Prod.MaterialMP.Material_Nombre,
                    p.Prod.Pigmt.Pigmt_Nombre,
                    p.UndMed_Id,
                    p.ExProd_PrecioVenta,
                    p.ExProd_Cantidad,
                    p.Prod.Prod_Descripcion,
                    p.TpMoneda_Id,
                    p.Prod.Prod_Peso_Millar,
                    p.Prod.Prod_Peso,
                    p.Prod.UndMedPeso,
                    p.TpBod_Id,

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

        [HttpGet("InventarioProductos")]
        public ActionResult<Existencia_Productos> GetInventarioProductos()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == p.Prod.Prod_Id && p.ExProd_Cantidad >= 1)
                .Include(p => p.Prod)
                .Select(p => new
                {
                    p.Prod_Id,
                    p.Prod.Prod_Nombre,
                    p.ExProd_PrecioVenta,
                    p.ExProd_Cantidad,
                    p.UndMed_Id,
                    p.ExProd_CantMinima
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

        [HttpGet("IdProductoPresentacionInventario/{Prod_Id}/{UndMed_Id}")]
        public ActionResult<Existencia_Productos> IdProductoPresentacionInventario(int Prod_Id, string UndMed_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == Prod_Id && p.UndMed_Id == UndMed_Id)
                .Select(p => p)
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

        // Consulta que devolverá la información de un producto
        [HttpGet("getInfoProducto/{producto}")]
        public ActionResult GetInfoProducto(string producto)
        {
            var con = (from e in _context.Set<Existencia_Productos>()
                       where e.Prod_Id.ToString().Contains(producto) ||
                             e.Prod.Prod_Nombre.Contains(producto)
                       select new
                       {
                           Id = e.Prod_Id,
                           Nombre = e.Prod.Prod_Nombre,
                           Presentacion = e.UndMed_Id,
                           Id_Existencia = e.ExProd_Id,
                       });
            return Ok(con);
        }

        // Consulta que devolverá el inventario de los productos con cada uno de los rollos que tiene disponibles
        [HttpGet("getStockProducts_AvaibleProduction")]
        public ActionResult GetStockProducts_AvaibleProduction()
        {
            var notAvaibleProduccion = from order in _context.Set<Detalles_OrdenFacturacion>()
                                       select order.Numero_Rollo;

            var stock = from prod in _context.Set<Producto>()
                        join exi in _context.Set<Existencia_Productos>() on prod.Prod_Id equals exi.Prod_Id
                        where exi.ExProd_Cantidad > 2
                        select new
                        {
                            Product = new
                            {
                                Item = prod.Prod_Id,
                                Reference = prod.Prod_Nombre,
                            },
                            Stock = new
                            {
                                Stock = exi.ExProd_Cantidad,
                                Price = exi.ExProd_PrecioVenta,
                                Presentation = exi.UndMed_Id,
                                StockPrice = exi.ExProd_PrecioExistencia,
                            },
                            Client = (
                                from cp in _context.Set<Cliente_Producto>()
                                join cli in _context.Set<Clientes>() on cp.Cli_Id equals cli.Cli_Id
                                join vende in _context.Set<Usuario>() on cli.usua_Id equals vende.Usua_Id
                                where prod.Prod_Id == cp.Prod_Id
                                select new
                                {
                                    cli = new {
                                        Id_Client = cli.Cli_Id,
                                        Client = cli.Cli_Nombre,
                                    },
                                    vende = new
                                    {
                                        Id_Vende = vende.Usua_Id,
                                        Name_Vende = vende.Usua_Nombre,
                                    }
                                }
                            ).ToList(),
                            Avaible_Production = (
                                from pp in _context.Set<Produccion_Procesos>()
                                where pp.Prod_Id == prod.Prod_Id &&
                                      pp.Estado_Rollo == 19 &&
                                      pp.Envio_Zeus == true &&
                                      !notAvaibleProduccion.Contains(pp.Numero_Rollo)
                                select new
                                {
                                    Number = pp.Numero_Rollo,
                                    Quantity = pp.Cantidad,
                                    Weight = pp.Peso_Neto,
                                    Presentation = pp.Presentacion,
                                    Process = pp.Proceso.Proceso_Nombre,
                                    Date = pp.Fecha,
                                    Hour = pp.Hora,
                                    Price = pp.Precio,
                                    Turn = pp.Turno,
                                    Information = pp.Datos_Etiqueta,
                                    orderProduction = pp.OT,
                                }
                            ).ToList(),
                            Stock_MonthByMonth = (
                                from mm in _context.Set<Inventario_Mensual_Productos>()
                                where mm.Prod_Id == prod.Prod_Id &&
                                      mm.UndMed_Id == exi.UndMed_Id
                                select mm
                            ).ToList(),
                        };
            return Ok(stock);
        }

        // PUT: api/Existencia_Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExistencia_Productos(long id, Existencia_Productos existencia_Productos)
        {
            if (id != existencia_Productos.ExProd_Id)
            {
                return BadRequest();
            }

            _context.Entry(existencia_Productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(id))
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

        [HttpPut("ActualizacionProducto/{Prod_Id}/{UndMed_Id}")]
        public IActionResult PutEstadoClientesOt(int Prod_Id, string UndMed_Id, Existencia_Productos existencia_Productos)
        {

            if (Prod_Id != existencia_Productos.Prod_Id && UndMed_Id != existencia_Productos.UndMed_Id)
            {
                return BadRequest();
            }

            try
            {
                var Actualizado = _context.Existencias_Productos.Where(x => x.Prod_Id == Prod_Id && x.UndMed_Id == UndMed_Id).First<Existencia_Productos>();
                Actualizado.ExProd_PrecioVenta = existencia_Productos.ExProd_PrecioVenta;
                Actualizado.ExProd_Precio = existencia_Productos.ExProd_Precio;
                Actualizado.TpMoneda_Id = existencia_Productos.TpMoneda_Id;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(Prod_Id, UndMed_Id))
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

        [HttpPut("ActualizacionExistencia/{exProd_Id}")]
        public IActionResult Put(int ExProd_Id, Existencia_Productos existencia_Productos)
        {

            if (ExProd_Id != existencia_Productos.ExProd_Id)
            {
                return BadRequest();
            }

            try
            {
                var Actualizado = _context.Existencias_Productos.Where(x => x.ExProd_Id == ExProd_Id).First<Existencia_Productos>();
                Actualizado.ExProd_PrecioVenta = existencia_Productos.ExProd_PrecioVenta;
                Actualizado.ExProd_Cantidad = existencia_Productos.ExProd_Cantidad;
                Actualizado.ExProd_PrecioExistencia = existencia_Productos.ExProd_PrecioExistencia;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(ExProd_Id))
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

        [HttpPut("ActualizacionCantMinima/{Prod_Id}")]
        public IActionResult PutCantMinima(int Prod_Id, Existencia_Productos existencia_Productos)
        {
            if (Prod_Id != existencia_Productos.Prod_Id)
            {
                return BadRequest();
            }

            try
            {
                var Actualizado = _context.Existencias_Productos.Where(x => x.Prod_Id == Prod_Id).First<Existencia_Productos>();
                Actualizado.ExProd_CantMinima = existencia_Productos.ExProd_CantMinima;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(Prod_Id))
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

        [HttpPut("putPrecioProducto/{producto}/{presentacion}/{precio}")]
        public IActionResult PutPrecioProducto(int producto, string presentacion, decimal precio)
        {
            try
            {
                var existencia = (from exis in _context.Set<Existencia_Productos>() where exis.Prod_Id == producto && exis.UndMed_Id == presentacion select exis).FirstOrDefault();
                existencia.ExProd_PrecioVenta = precio;
                _context.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(producto))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPut("putExistencia/{producto}/{presentacion}/{precio}/{cantidad}")]
        public IActionResult PutExistencia(int producto, string presentacion, decimal precio, decimal cantidad)
        {
            try
            {
                var existencia = (from exis in _context.Set<Existencia_Productos>() where exis.Prod_Id == producto && exis.UndMed_Id == presentacion select exis).FirstOrDefault();
                existencia.ExProd_PrecioVenta = precio;
                existencia.ExProd_Cantidad += cantidad;
                _context.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(producto))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Existencia_Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Existencia_Productos>> PostExistencia_Productos(Existencia_Productos existencia_Productos)
        {
            if (_context.Existencias_Productos == null)
            {
                return Problem("Entity set 'dataContext.Existencias_Productos'  is null.");
            }
            _context.Existencias_Productos.Add(existencia_Productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExistencia_Productos", new { id = existencia_Productos.ExProd_Id }, existencia_Productos);
        }

        // DELETE: api/Existencia_Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExistencia_Productos(long id)
        {
            if (_context.Existencias_Productos == null)
            {
                return NotFound();
            }
            var existencia_Productos = await _context.Existencias_Productos.FindAsync(id);
            if (existencia_Productos == null)
            {
                return NotFound();
            }

            _context.Existencias_Productos.Remove(existencia_Productos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Existencia_ProductosExists(long id)
        {
            return (_context.Existencias_Productos?.Any(e => e.ExProd_Id == id)).GetValueOrDefault();
        }

        //Actualizacion de existencia segun el id del producto y la presentacion de este mismo
        private bool Existencia_ProductosExists(int prod_Id, string undMed_Id)
        {
            return (_context.Existencias_Productos?.Any(e => e.Prod_Id == prod_Id && e.UndMed_Id == undMed_Id)).GetValueOrDefault();
        }
    }
}
