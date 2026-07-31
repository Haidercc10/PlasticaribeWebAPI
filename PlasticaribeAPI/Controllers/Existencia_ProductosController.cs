using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using StackExchange.Redis;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
            var producto = from e in _context.Set<Existencia_Productos>()
                           join p in _context.Set<Producto>() on e.Prod_Id equals p.Prod_Id
                           where e.Prod_Id == Prod_Id
                           select new
                           {
                               exist = e,
                               prod = p
                           };

            if (producto == null) return NotFound();
            else return Ok(producto);
        }

        /* Compara ID Producto de Plasticaribe con Codigo Articulo de Zeus. */
        [HttpPost("getSearchArticleFromZeus")]
        public async Task<IActionResult> getSearchArticleFromZeus([FromBody] List<int> items)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = await _context.Existencias_Productos
                .AsNoTracking()
                .Where(e => items.Contains(e.Prod_Id))
                .Select(e => new
                {
                    Item = e.Prod_Id,
                    Presentation = e.UndMed_Id,
                    Client = e.ExProd_Cliente,
                    Stock = e.ExProd_Cantidad,
                    Asesor = e.ExProd_Asesor,
                    CodeAsesor = e.Usua_Asesor,
                })
                .ToListAsync();

                return Ok(producto);
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

        [HttpGet("getDataProduct/{item}/{unit}")]
        public ActionResult getDataProduct(int item, string unit)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = from e in _context.Set<Existencia_Productos>()
                           where e.Prod_Id == item &&
                           e.UndMed_Id == unit
                           select new {
                               e,
                               Unit_Packing = (from pp in _context.Set<Produccion_Procesos>()
                                               where pp.Prod_Id == item &&
                                               pp.Presentacion == unit &&
                                               pp.Fecha >= Convert.ToDateTime("2024-02-04") &&
                                               pp.Estado_Rollo == 19 &&
                                               pp.Envio_Zeus == true
                                               select pp.Presentacion == null ? Convert.ToDecimal(0m) : pp.Presentacion == "Kg" ? pp.Peso_Neto : pp.Cantidad).FirstOrDefault(),
                               Teoric_Weight = (from pp in _context.Set<Produccion_Procesos>()
                                               where pp.Prod_Id == item &&
                                               pp.Presentacion == unit &&
                                               pp.Fecha >= Convert.ToDateTime("2024-02-04") &&
                                               pp.Estado_Rollo == 19 &&
                                               pp.Envio_Zeus == true
                                               select pp.Presentacion == null ?  Convert.ToDecimal(0m) : pp.Peso_Neto).DefaultIfEmpty().Average(),
                               Teoric_GrossWeight = (from pp in _context.Set<Produccion_Procesos>()
                                                     where pp.Prod_Id == item &&
                                                     pp.Presentacion == unit &&
                                                     pp.Fecha >= Convert.ToDateTime("2024-02-04") &&
                                                     pp.Estado_Rollo == 19 &&
                                                     pp.Envio_Zeus == true
                                                     select pp.Presentacion == null ? Convert.ToDecimal(0m) : pp.Peso_Bruto).DefaultIfEmpty().Average()
                           };

                return Ok(producto);
            
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
        public async Task<ActionResult> GetStockProducts_AvaibleProduction(string? sales = "")
        {
            string UnidadKg = "Kg";

            var stock = await (from exi in _context.Set<Existencia_Productos>()
                        join prod in _context.Set<Producto>() on exi.Prod_Id equals prod.Prod_Id
                        where exi.ExProd_Cantidad >= 1
                        && (string.IsNullOrEmpty(sales) || exi.ExProd_Asesor.Contains(sales))
                               select new
                        {
                            Product = new
                            {
                                Item = exi.Prod_Id,
                                Reference = prod.Prod_Nombre, 
                            },
                            Stock = new
                            {
                                Stock = exi.ExProd_Cantidad,
                                Price = exi.ExProd_PrecioVenta,
                                Presentation = exi.UndMed_Id,
                                StockPrice = exi.ExProd_PrecioExistencia,

                            },
                            Client = exi.ExProd_Cliente, 
                            Code_Seller = exi.Usua_Asesor,
                            Seller = exi.ExProd_Asesor, 
                            QtyStandard = exi.ExProd_UndEmpaque,
                            //              _context.Set<Produccion_Procesos>()
                            //                       .Where(pp => pp.Prod_Id == prod.Prod_Id 
                            //                       && pp.Presentacion == exi.UndMed_Id)
                            //                       .Select(pp => exi.UndMed_Id == UnidadKg
                            //                               ? pp.Peso_Neto
                            //                               : pp.Cantidad
                            //              ).FirstOrDefault(), 
                            //
                            //Date = (from pp in _context.Set<Produccion_Procesos>()
                            //        where pp.Prod_Id == prod.Prod_Id &&
                            //        pp.Presentacion == exi.UndMed_Id && 
                            //        pp.Estado_Rollo == 19 &&
                            //        pp.Fecha >= Convert.ToDateTime("2024-02-04") &&
                            //        pp.Envio_Zeus == true
                            //        select (DateTime?)pp.Fecha).Min(),

                            Weight = exi.ExProd_PesoBruto, 
                            CityClient = (from s in _context.Set<SedesClientes>()
                                          where s.Cli_Id == (from est in _context.Set<Estados_ProcesosOT>()
                                                              where est.Prod_Id == exi.Prod_Id
                                                              orderby est.EstProcOT_Id descending
                                                              select est.Cli_Id == null ? 1 : est.Cli_Id).FirstOrDefault()
                                         select s.SedeCliente_Ciudad).FirstOrDefault(),
                            Count = exi.ExProd_Unidades,                       
                        }).ToListAsync();

            return Ok(stock);
        }

        //Consulta que devolverá el inventario de los rollos que se han pesado en empaque pero no se han entregado a despacho
        [HttpGet("getStockProducts_Process/{process}")]
        public ActionResult GetStockProducts_Process(string process)
        {
            int[] statuses = { 20, 24, 36, 44, 45 };

            var stockNotAvaible = from pp in _context.Set<Produccion_Procesos>()
                                  join p in _context.Set<Producto>() on pp.Prod_Id equals p.Prod_Id
                                  where pp.Envio_Zeus == false &&
                                        pp.Estado_Rollo == 19 &&
                                        pp.Proceso_Id == process &&
                                        pp.Proceso_Id != "WIKE" &&
                                        pp.Fecha >= Convert.ToDateTime("2024-02-04") &&
                                      !((from order in _context.Set<Detalles_OrdenFacturacion>()
                                         where order.Prod_Id == pp.Prod_Id && order.OrdenFacturacion.Estado_Id != 3 && statuses.Contains(order.Estado_Id)
                                         select order.Numero_Rollo).ToList()).Contains(pp.NumeroRollo_BagPro)
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
                                      Client = (from est in _context.Set<Estados_ProcesosOT>() where est.Prod_Id == pp.Key.Item orderby est.EstProcOT_Id descending select est.EstProcOT_Cliente).FirstOrDefault(),
                                      Seller = (from est in _context.Set<Estados_ProcesosOT>() where pp.Key.Item == est.Prod_Id orderby est.EstProcOT_Id descending select est.Usuario.Usua_Nombre).FirstOrDefault(),
                                      QtyStandard = (from p_p in _context.Set<Produccion_Procesos>() 
                                                     where p_p.Prod_Id == pp.Key.Item && 
                                                     p_p.Presentacion == pp.Key.Presentation 
                                                     select p_p.Presentacion == "Kg" ? p_p.Peso_Neto == null ? 0 : p_p.Peso_Neto : p_p.Cantidad == null ? 0 : p_p.Cantidad).FirstOrDefault(),
                                      Date = (from p_p in _context.Set<Produccion_Procesos>()
                                                     where p_p.Prod_Id == pp.Key.Item &&
                                                     p_p.Presentacion == pp.Key.Presentation &&
                                                     p_p.Estado_Rollo == 19 &&
                                                     p_p.Envio_Zeus == false
                                                     select p_p.Fecha).Min(),
                                      Weight = pp.Sum(x => x.pp.Peso_Bruto),
                                      CityClient = (from s in _context.Set<SedesClientes>()
                                                    where s.Cli_Id == (from est in _context.Set<Estados_ProcesosOT>()
                                                                       where est.Prod_Id == pp.Key.Item
                                                                       orderby est.EstProcOT_Id descending
                                                                       select est.Cli_Id == null ? 1 : est.Cli_Id).FirstOrDefault()
                                                    select s.SedeCliente_Ciudad).FirstOrDefault(),
                                      Count = pp.Count(),
                                                                       };
            return Ok(stockNotAvaible);
        }

        //Consulta que devolverá el inventario de los rollos que se han pesado en empaque pero no se han entregado a despacho
        [HttpGet("getStockDelivered_NoAvaible")]
        public ActionResult GetStockDelivered_NoAvaible()
        {
            var stockNotAvaible = from pp in _context.Set<Produccion_Procesos>()
                                  join p in _context.Set<Producto>() on pp.Prod_Id equals p.Prod_Id
                                  where pp.Envio_Zeus == false &&
                                        pp.Estado_Rollo == 31 &&
                                        pp.Proceso_Id != "WIKE" &&
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
                                      Client = (from est in _context.Set<Estados_ProcesosOT>() where est.Prod_Id == pp.Key.Item orderby est.EstProcOT_Id descending select est.EstProcOT_Cliente).FirstOrDefault(),
                                      Seller = (from est in _context.Set<Estados_ProcesosOT>() where pp.Key.Item == est.Prod_Id orderby est.EstProcOT_Id descending select est.Usuario.Usua_Nombre).FirstOrDefault(),
                                      Stock_MonthByMonth = (from mm in _context.Set<Inventario_Mensual_Productos>() where mm.Prod_Id == pp.Key.Item && mm.UndMed_Id == pp.Key.Presentation select mm).ToList(),
                                  };
            return Ok(stockNotAvaible);
        }

        [HttpPost("getInventoryProducts")]
        public ActionResult getInventoryProducts([FromBody] List<Article> items) 
        {
            int count = 0;
            List<Product> newStock = new List<Product>();

            foreach (var item in items)
            {
                //string product = item.Split("-")[0];
                //string presentation = item.Split("-")[1];
                string presentation = "";
                if (item.presentation == "KLS") presentation = "Kg";
                if (item.presentation == "UND") presentation = "Und";
                if (item.presentation == "PAQ") presentation = "Paquete";

                var stock = (from e in _context.Set<Existencia_Productos>()
                            join p in _context.Set<Producto>() on e.Prod_Id equals p.Prod_Id 
                            where e.ExProd_Cantidad != item.qty &&
                            e.Prod_Id == Convert.ToInt64(item.item) &&
                            e.UndMed_Id == presentation
                            select new Product { 
                              Code = count + 1,   
                              Item = e.Prod_Id,  
                              Reference = p.Prod_Nombre,
                              QtyPL = e.ExProd_Cantidad,
                              PresentationPL = e.UndMed_Id,
                              QtyZeus = item.qty,
                              PresentationZeus = item.presentation,
                              Difference = (item.qty - e.ExProd_Cantidad),
                              Price = e.ExProd_PrecioVenta.Value,
                              Subtotal = (e.ExProd_PrecioVenta.Value * (item.qty - e.ExProd_Cantidad)),
                              GenericQty = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == p.Prod_Id select pp.Presentacion == "Kg" ? pp.Peso_Teorico == null ? 0 : pp.Peso_Teorico : pp.Cantidad == null ? 0 : pp.Cantidad).FirstOrDefault()
                            }).FirstOrDefault();

                count++;
                newStock.Add(stock);
                if(count == items.Count()) return Ok(newStock);
            }
            return Ok(newStock);
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

        ///Restar al inventario de productos 
        [HttpPut("putConsolidateProductsOF/{orden}")]
        async public Task<IActionResult> putConsolidateProductsOF(int orden)
        {
            var products = from fp in _context.Set<Facturacion_Productos>()
                           where fp.Of_Id == orden && fp.OrdenFacturacion.Estado_Id == 19 //&& pp.Estado_Rollo != 20
                           select fp;

            int count = 0;
            foreach (var item in products)
            {
                var product = (from e in _context.Set<Existencia_Productos>() where e.Prod_Id == item.Prod_Id && e.UndMed_Id == item.UndMed_Id select e).FirstOrDefault();
                product.ExProd_PrecioExistencia = ((product.ExProd_Cantidad - item.FactPro_Cantidad) * Convert.ToDecimal(product.ExProd_PrecioVenta));
                product.ExProd_Cantidad = (product.ExProd_Cantidad - item.FactPro_Cantidad);
                //product.ExProd_PesoBruto = (product.ExProd_PesoBruto - item.Peso_Bruto);
                //product.ExProd_Unidades = (product.ExProd_Unidades - item.FactPro_Unidades);

                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == products.Count()) return NoContent();
            }
            return NoContent();
        }

        [HttpPut("putConsolidateProductsOF2/{orden}")]
        public async Task<IActionResult> PutConsolidateProductsOF2(int orden)
        {
            //await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var productos = await (
                    from fp in _context.Set<Facturacion_Productos>()
                    join ep in _context.Set<Existencia_Productos>()
                        on new { fp.Prod_Id, fp.UndMed_Id }
                        equals new { ep.Prod_Id, ep.UndMed_Id }
                    where fp.Of_Id == orden &&
                          fp.OrdenFacturacion.Estado_Id == 19
                    select new
                    {
                        Facturacion = fp,
                        Existencia = ep
                    }
                ).ToListAsync();

                if (!productos.Any())
                {
                    return NotFound(new
                    {
                        message = "No se encontraron productos para consolidar."
                    });
                }

                foreach (var item in productos)
                {
                    item.Existencia.ExProd_Cantidad -= item.Facturacion.FactPro_Cantidad;

                    item.Existencia.ExProd_PrecioExistencia =
                        item.Existencia.ExProd_Cantidad *
                        Convert.ToDecimal(item.Existencia.ExProd_PrecioVenta);

                    // Si en el futuro deseas actualizar estos campos:
                    // item.Existencia.ExProd_PesoBruto -= item.Facturacion.Peso_Bruto;
                    // item.Existencia.ExProd_Unidades -= item.Facturacion.FactPro_Unidades;
                }

                await _context.SaveChangesAsync();
                //await transaction.CommitAsync();

                return NoContent();
            }
            catch (Exception)
            {
                //await transaction.RollbackAsync();
                throw;
            }
        }

        ///Restar al inventario de productos 
        [HttpPut("putStockThenAnullation/{orden}")]
        async public Task<IActionResult> putStockThenAnullation(int orden)
        {
            var products = from fp in _context.Set<Facturacion_Productos>()
                           where fp.Of_Id == orden //&& pp.Estado_Rollo != 20
                           select fp;

            int count = 0;
            foreach (var item in products)
            {
                var product = (from e in _context.Set<Existencia_Productos>() where e.Prod_Id == item.Prod_Id && e.UndMed_Id == item.UndMed_Id select e).FirstOrDefault();
                product.ExProd_PrecioExistencia = ((product.ExProd_Cantidad + item.FactPro_Cantidad) * Convert.ToDecimal(product.ExProd_PrecioVenta));
                product.ExProd_Cantidad = (product.ExProd_Cantidad + item.FactPro_Cantidad);
                product.ExProd_PesoBruto = (product.ExProd_PesoBruto + item.Peso_Bruto);
                product.ExProd_Unidades = (product.ExProd_Unidades + item.FactPro_Unidades);

                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == products.Count()) return NoContent();
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

public class Product
{
    public int Code { get; set; }

    public int Item { get; set; }

    public string Reference { get; set; }

    [Precision(18,2)]
    public decimal QtyPL { get; set; }

    public string PresentationPL { get; set; }

    [Precision(18, 2)]
    public decimal QtyZeus { get; set; }

    public string PresentationZeus { get; set; }

    [Precision(18, 2)]
    public decimal Difference { get; set; }

    [Precision(18, 2)]
    public decimal Price { get; set; }

    [Precision(18, 2)]
    public decimal Subtotal { get; set; }

    [Precision(18, 2)]
    public decimal GenericQty { get; set; }
}

public class Article
{
    public string item { get; set; }
    public string reference { get; set; }

    [Precision(18, 2)]
    public decimal qty { get; set; }

    public string presentation { get; set; }

    [Precision(18, 2)]
    public decimal price { get; set; }

    [Precision(18, 2)]
    public decimal subtotal { get; set; }

}
