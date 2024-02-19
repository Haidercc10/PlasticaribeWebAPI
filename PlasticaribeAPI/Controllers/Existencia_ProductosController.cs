using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using StackExchange.Redis;

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
            var stock = from prod in _context.Set<Producto>()
                        join exi in _context.Set<Existencia_Productos>() on prod.Prod_Id equals exi.Prod_Id
                        where exi.ExProd_Cantidad >= 1
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
                                from est in _context.Set<Estados_ProcesosOT>()
                                join vende in _context.Set<Usuario>() on est.Usua_Id equals vende.Usua_Id
                                where prod.Prod_Id == est.Prod_Id
                                orderby est.Estado_Id descending
                                select new
                                {
                                    cli = new
                                    {
                                        Id_Client = 1,
                                        Client = est.EstProcOT_Cliente,
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
                                      !((from order in _context.Set<Detalles_OrdenFacturacion>()
                                         where order.Prod_Id == pp.Prod_Id && order.OrdenFacturacion.Estado_Id != 24
                                         select order.Numero_Rollo).ToList()).Contains(pp.NumeroRollo_BagPro)
                                select new
                                {
                                    Number_BagPro = pp.NumeroRollo_BagPro,
                                    Number = pp.Numero_Rollo,
                                    Quantity = pp.Cantidad,
                                    Weight = pp.Peso_Neto,
                                    Presentation = pp.Presentacion,
                                    Process = pp.Proceso.Proceso_Nombre,
                                    Date = pp.Fecha,
                                    Hour = pp.Hora,
                                    Price = pp.Precio,
                                    SellPrice = pp.PrecioVenta_Producto,
                                    Turn = pp.Turno,
                                    Information = (from dt in _context.Set<DetalleEntradaRollo_Producto>()
                                                   join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                                                   where (dt.Rollo_Id == pp.Numero_Rollo) &&
                                                          e.EntRolloProd_Id >= 28512
                                                   orderby e.EntRolloProd_Id descending
                                                   select e.EntRolloProd_Observacion).FirstOrDefault(),
                                    orderProduction = pp.OT,
                                }
                            ).ToList(),
                            Stock_MonthByMonth = (from mm in _context.Set<Inventario_Mensual_Productos>() where mm.Prod_Id == prod.Prod_Id && mm.UndMed_Id == exi.UndMed_Id select mm).ToList(),
                        };
            return Ok(stock);
        }

        //Consulta que devolverá el inventario de los rollos que se han pesado en empaque pero no se han entregado a despacho
        [HttpGet("getStockProducts_Process/{process}")]
        public ActionResult GetStockProducts_Process(string process)
        {
            var products = from pp in _context.Set<Produccion_Procesos>() where pp.Proceso_Id == process && pp.Envio_Zeus == false && pp.Estado_Rollo == 19 select pp.Prod_Id;

            var stockNotAvaible = from pp in _context.Set<Produccion_Procesos>()
                                  join p in _context.Set<Producto>() on pp.Prod_Id equals p.Prod_Id
                                  where pp.Envio_Zeus == false &&
                                        pp.Estado_Rollo == 19 &&
                                        pp.Proceso_Id == process &&
                                        pp.Fecha >= Convert.ToDateTime("2024-02-04")
                                  group new { pp, p } by new
                                  {
                                      Item = p.Prod_Id,
                                      Reference = p.Prod_Nombre,
                                      Presentation = pp.Presentacion,
                                  } into pp
                                  select new
                                  {
                                      Product = new
                                      {
                                          pp.Key.Item,
                                          pp.Key.Reference
                                      },
                                      Stock = new
                                      {
                                          Stock = pp.Key.Presentation == "Kg" ? pp.Sum(x => x.pp.Peso_Neto) : pp.Sum(x => x.pp.Cantidad),
                                          Price = (from exi in _context.Set<Existencia_Productos>() where exi.Prod_Id == pp.Key.Item && exi.UndMed_Id == pp.Key.Presentation select exi.ExProd_PrecioVenta).FirstOrDefault(),
                                          pp.Key.Presentation,
                                          StockPrice = (from exi in _context.Set<Existencia_Productos>() where exi.Prod_Id == pp.Key.Item && exi.UndMed_Id == pp.Key.Presentation select exi.ExProd_PrecioExistencia).FirstOrDefault(),
                                      },
                                      Client = (
                                        from est in _context.Set<Estados_ProcesosOT>()
                                        join vende in _context.Set<Usuario>() on est.Usua_Id equals vende.Usua_Id
                                        where pp.Key.Item == est.Prod_Id
                                        orderby est.Estado_Id descending
                                        select new
                                        {
                                            cli = new
                                            {
                                                Id_Client = 1,
                                                Client = est.EstProcOT_Cliente,
                                            },
                                            vende = new
                                            {
                                                Id_Vende = vende.Usua_Id,
                                                Name_Vende = vende.Usua_Nombre,
                                            }
                                        }
                                      ).ToList(),
                                      Avaible_Production = (
                                        from pp2 in _context.Set<Produccion_Procesos>()
                                        where pp2.Prod_Id == pp.Key.Item &&
                                              pp2.Estado_Rollo == 19 &&
                                              pp2.Envio_Zeus == false &&
                                              pp2.Fecha >= Convert.ToDateTime("2024-02-04") &&
                                              !((from order in _context.Set<Detalles_OrdenFacturacion>()
                                                 where order.Prod_Id == pp2.Prod_Id && order.OrdenFacturacion.Estado_Id != 24
                                                 select order.Numero_Rollo).ToList()).Contains(pp2.NumeroRollo_BagPro)
                                        select new
                                        {
                                            Number_BagPro = pp2.NumeroRollo_BagPro,
                                            Number = pp2.Numero_Rollo,
                                            Quantity = pp2.Cantidad,
                                            Weight = pp2.Peso_Neto,
                                            Presentation = pp2.Presentacion,
                                            Process = pp2.Proceso.Proceso_Nombre,
                                            Date = pp2.Fecha,
                                            Hour = pp2.Hora,
                                            Price = pp2.Precio,
                                            SellPrice = pp2.PrecioVenta_Producto,
                                            Turn = pp2.Turno,
                                            Information = pp2.Datos_Etiqueta,
                                            orderProduction = pp2.OT,
                                        }
                                      ).ToList(),
                                      Stock_MonthByMonth = (from mm in _context.Set<Inventario_Mensual_Productos>() where mm.Prod_Id == pp.Key.Item && mm.UndMed_Id == pp.Key.Presentation select mm).ToList(),
                                  };

            var stock = from prod in _context.Set<Producto>()
                        join exi in _context.Set<Existencia_Productos>() on prod.Prod_Id equals exi.Prod_Id
                        where exi.ExProd_Cantidad > 1 &&
                              products.Contains(prod.Prod_Id)
                        select new
                        {
                            Product = new
                            {
                                Item = prod.Prod_Id,
                                Reference = prod.Prod_Nombre,
                            },
                            Stock = new
                            {
                                Stock = exi.UndMed_Id == "Kg" ? (from pp in _context.Set<Produccion_Procesos>()
                                                                 where pp.Prod_Id == prod.Prod_Id &&
                                                                       pp.Estado_Rollo == 19 &&
                                                                       pp.Envio_Zeus == false &&
                                                                       pp.Proceso_Id == process &&
                                                                       pp.Fecha >= Convert.ToDateTime("2024-02-04")
                                                                 select pp.Peso_Neto).Sum() : (from pp in _context.Set<Produccion_Procesos>()
                                                                                               where pp.Prod_Id == prod.Prod_Id &&
                                                                                                     pp.Estado_Rollo == 19 &&
                                                                                                     pp.Envio_Zeus == false &&
                                                                                                     pp.Proceso_Id == process &&
                                                                                                     pp.Fecha >= Convert.ToDateTime("2024-02-04")
                                                                                               select pp.Cantidad).Sum(),
                                Price = exi.ExProd_PrecioVenta,
                                Presentation = exi.UndMed_Id,
                                StockPrice = exi.ExProd_PrecioExistencia,
                            },
                            Client = (
                                from est in _context.Set<Estados_ProcesosOT>()
                                join vende in _context.Set<Usuario>() on est.Usua_Id equals vende.Usua_Id
                                where prod.Prod_Id == est.Prod_Id
                                orderby est.Estado_Id descending
                                select new
                                {
                                    cli = new
                                    {
                                        Id_Client = 1,
                                        Client = est.EstProcOT_Cliente,
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
                                      pp.Envio_Zeus == false &&
                                      !((from order in _context.Set<Detalles_OrdenFacturacion>()
                                         where order.Prod_Id == pp.Prod_Id && order.OrdenFacturacion.Estado_Id != 24
                                         select order.Numero_Rollo).ToList()).Contains(pp.NumeroRollo_BagPro)
                                select new
                                {
                                    Number_BagPro = pp.NumeroRollo_BagPro,
                                    Number = pp.Numero_Rollo,
                                    Quantity = pp.Cantidad,
                                    Weight = pp.Peso_Neto,
                                    Presentation = pp.Presentacion,
                                    Process = pp.Proceso.Proceso_Nombre,
                                    Date = pp.Fecha,
                                    Hour = pp.Hora,
                                    Price = pp.Precio,
                                    SellPrice = pp.PrecioVenta_Producto,
                                    Turn = pp.Turno,
                                    Information = pp.Datos_Etiqueta,
                                    orderProduction = pp.OT,
                                }
                            ).ToList(),
                            Stock_MonthByMonth = (from mm in _context.Set<Inventario_Mensual_Productos>() where mm.Prod_Id == prod.Prod_Id && mm.UndMed_Id == exi.UndMed_Id select mm).ToList(),
                        };
            return Ok(stockNotAvaible);
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

        [HttpPut("ActualizacionCantMinima/{Prod_Id}/{cantMinima}")]
        public IActionResult PutCantMinima(int Prod_Id, decimal cantMinima)
        {
            try
            {
                var Actualizado = (from e in _context.Set<Existencia_Productos>() where e.Prod_Id == Prod_Id select e).FirstOrDefault();
                Actualizado.ExProd_CantMinima = cantMinima;
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
                existencia.ExProd_PrecioExistencia += precio * cantidad;
                _context.SaveChanges();
                return Ok("Existencia Actualizada");
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
