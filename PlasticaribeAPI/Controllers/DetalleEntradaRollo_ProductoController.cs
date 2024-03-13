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
    public class DetalleEntradaRollo_ProductoController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleEntradaRollo_ProductoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleEntradaRollo_Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleEntradaRollo_Producto>>> GetDetallesEntradasRollos_Productos()
        {
            return await _context.DetallesEntradasRollos_Productos.ToListAsync();
        }

        // GET: api/DetalleEntradaRollo_Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleEntradaRollo_Producto>> GetDetalleEntradaRollo_Producto(long id)
        {
            var detalleEntradaRollo_Producto = await _context.DetallesEntradasRollos_Productos.FindAsync(id);

            if (detalleEntradaRollo_Producto == null)
            {
                return NotFound();
            }

            return detalleEntradaRollo_Producto;
        }

        //Consulta que va a traer la informacion de un Rollo
        [HttpGet("VerificarRollo/{id}")]
        public ActionResult Get(long id)
        {
            var con = _context.DetallesEntradasRollos_Productos.Where(x => x.Rollo_Id == id).ToList();
            return Ok(con);
        }

        //Consulta que va a traer la informacion de los rollos que esten asociados a un producto, el cual será consultado
        [HttpGet("consultarProducto/{id}")]
        public ActionResult GetConsultarProd(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetallesEntradasRollos_Productos
                .Where(x => x.Prod_Id == id)
                .Select(x => new
                {
                    x.EntRolloProd_Id,
                    x.Prod_Id,
                    x.Prod.Prod_Nombre,
                    x.Estado_Id,
                    x.Rollo_Id,
                    x.DtEntRolloProd_Cantidad,
                    x.UndMed_Rollo,
                    x.Prod_CantBolsasPaquete,
                    x.Prod_CantBolsasRestates,
                    x.Prod_CantPaquetesRestantes
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta que nos permitirá crear un archivo pdf a base de dala informacion pedida
        [HttpGet("CrearPdf/{ot}")]
        public ActionResult GetCrearPdf(long ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      from emp in _context.Set<Empresa>()
                      where rollo.EntRolloProd_Id == ot
                            && emp.Empresa_Id == 800188732
                      select new
                      {
                          rollo.EntRollo_Producto.EntRolloProd_Id,
                          rollo.Prod_Id,
                          rollo.Prod.Prod_Nombre,
                          rollo.Rollo_Id,
                          rollo.UndMed_Rollo,
                          rollo.DtEntRolloProd_Cantidad,
                          rollo.EntRollo_Producto.EntRolloProd_Fecha,
                          Creador = rollo.EntRollo_Producto.Usua_Id,
                          NombreCreador = rollo.EntRollo_Producto.Usua.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("CrearPdf2/{ot}")]
        public ActionResult GetCrearPdf2(long ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetallesEntradasRollos_Productos.Where(x => x.EntRolloProd_Id == ot)
                    .GroupBy(x => new
                    {
                        x.Prod_Id,
                        x.Prod.Prod_Nombre,
                        x.UndMed_Prod
                    }).Select(x => new
                    {
                        x.Key.Prod_Id,
                        x.Key.Prod_Nombre,
                        suma = x.Sum(x => x.DtEntRolloProd_Cantidad),
                        x.Key.UndMed_Prod,
                        cantRollos = x.Count()
                    }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("CrearPDFUltimoID/{id}")]
        public ActionResult GetCrearPDF(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      from emp in _context.Set<Empresa>()
                      where rollo.EntRollo_Producto.EntRolloProd_Id == id
                            && emp.Empresa_Id == 800188732
                      orderby rollo.EntRollo_Producto.EntRolloProd_Id
                      select new
                      {
                          rollo.DtEntRolloProd_OT,
                          rollo.EntRollo_Producto.EntRolloProd_Id,
                          rollo.Prod_Id,
                          rollo.Prod.Prod_Nombre,
                          rollo.Rollo_Id,
                          rollo.UndMed_Rollo,
                          rollo.DtEntRolloProd_Cantidad,
                          rollo.EntRollo_Producto.EntRolloProd_Fecha,
                          Creador = rollo.EntRollo_Producto.Usua_Id,
                          NombreCreador = rollo.EntRollo_Producto.Usua.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpPost("getRollos")]
        public IActionResult getRollos([FromBody] List<long> rollos)
        {
            return Ok(from e in _context.Set<DetalleEntradaRollo_Producto>() where rollos.Contains(e.Rollo_Id) && e.EntRolloProd_Id >= 28686 select new { e.Rollo_Id, e.Proceso_Id });
        }

        [HttpGet("getDataProductionIncome/{startDate}/{endDate}")]
        public ActionResult GetDataProductionIncome(DateTime startDate, DateTime endDate, string? production = "", string? orderProduction = "", string? item = "")
        {
            var con = from dt in _context.Set<DetalleEntradaRollo_Producto>()
                      join ent in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals ent.EntRolloProd_Id
                      join reel in _context.Set<Produccion_Procesos>() on dt.Prod_Id equals reel.Prod_Id
                      where ent.EntRolloProd_Fecha >= startDate &&
                            ent.EntRolloProd_Fecha <= endDate &&
                            (dt.Rollo_Id == reel.Numero_Rollo || dt.Rollo_Id == reel.NumeroRollo_BagPro) &&
                            (production != "" ? reel.NumeroRollo_BagPro == Convert.ToInt64(production) : true) &&
                            (orderProduction != "" ? dt.DtEntRolloProd_OT == Convert.ToInt64(orderProduction) : true) &&
                            (item != "" ? dt.Prod_Id == Convert.ToInt64(item) : true) &&
                            ent.EntRolloProd_Id >= 28686 &&
                            reel.Envio_Zeus == true
                      select new
                      {
                          ent = new
                          {
                              ent.EntRolloProd_Fecha,
                              ent.EntRolloProd_Hora,
                              ent.EntRolloProd_Observacion,
                          },
                          Details = new
                          {
                                dt.DtEntRolloProd_OT,
                                dt.DtEntRolloProd_Cantidad,
                                dt.UndMed_Rollo,
                          },
                          Product = new
                          {
                              dt.Prod.Prod_Id,
                              dt.Prod.Prod_Nombre,
                          },
                          Process = dt.Proceso,
                          User = new
                          {
                              ent.Usua.Usua_Nombre,
                          },
                          DetailsProduction = new
                          {
                              reel.NumeroRollo_BagPro,
                              reel.Numero_Rollo,
                              reel.PrecioVenta_Producto,
                          },
                          State = reel.Estado, 
                          StateEntry = dt.Estado
                      };

            return con.Any() ? Ok(con) : NotFound();
        }

        [HttpGet("getStartedProductionByItem/{item}")]
        public ActionResult GetStartedProductionByItem(long item)
        {
            var notAvaibleProduction = (from exits in _context.Set<DetallesAsignacionProducto_FacturaVenta>() where exits.Prod_Id == item select exits.Rollo_Id);

            var startedProduction = from p in _context.Set<DetalleEntradaRollo_Producto>()
                                    join prod in _context.Set<Producto>() on p.Prod_Id equals prod.Prod_Id
                                    join ent in _context.Set<EntradaRollo_Producto>() on p.EntRolloProd_Id equals ent.EntRolloProd_Id
                                    where p.Prod_Id == item  &&
                                          ent.EntRolloProd_Id >= 28686 &&
                                          !notAvaibleProduction.Contains(p.Rollo_Id)
                                    group p by new
                                    {
                                        p.Prod_Id,
                                        prod.Prod_Nombre,
                                        ent.EntRolloProd_Observacion
                                    } into p
                                    select new
                                    {
                                        Item = p.Key.Prod_Id,
                                        Referencia = p.Key.Prod_Nombre,
                                        Ubicacion_Bodega = p.Key.EntRolloProd_Observacion,
                                        Cantidad_Rollos = p.Count(),
                                        Cantidad_Total = p.Sum(x => x.DtEntRolloProd_Cantidad)
                                    };

            return Ok(startedProduction);
        }

        // PUT: api/DetalleEntradaRollo_Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleEntradaRollo_Producto(long id, DetalleEntradaRollo_Producto detalleEntradaRollo_Producto)
        {
            if (id != detalleEntradaRollo_Producto.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalleEntradaRollo_Producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleEntradaRollo_ProductoExists(id))
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

        //Consulta que seleccionará y cambiará el/los numero(s) de rollo(s) que se le pase(n) en el front end para cambiar su ubicacion.
        [HttpPost("putChangeUbicationRoll/{ubication}")]
        public async Task<IActionResult> putChangeUbicationRoll([FromBody] List<long> rolls, string ubication)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            int count = 0;
            foreach (var roll in rolls)
            {
                var dataEntry = (from ent in _context.Set<EntradaRollo_Producto>()
                                 from det in _context.Set<DetalleEntradaRollo_Producto>()
                                 where ent.EntRolloProd_Id == det.EntRolloProd_Id &&
                                 det.Rollo_Id == roll
                                 select ent).FirstOrDefault();

                dataEntry.EntRolloProd_Observacion = ubication;
                _context.Entry(dataEntry).State = EntityState.Modified;
                _context.SaveChanges();

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RollExists(roll)) return NotFound();
                    else throw;
                }
                count++;
                if (count == rolls.Count()) return NoContent();
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Consulta que seleccionará y cambiará el/los numero(s) de rollo(s) que se le pase(n) en el front end para cambiar su estado a devuelto.
        [HttpPost("putStateReturnedRoll")]
        public async Task<IActionResult> putStateReturnedRoll([FromBody] List<long> rolls)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            int count = 0;
            foreach (var roll in rolls)
            {
                var dataEntry = (from ent in _context.Set<EntradaRollo_Producto>()
                                 from det in _context.Set<DetalleEntradaRollo_Producto>()
                                 where ent.EntRolloProd_Id == det.EntRolloProd_Id &&
                                 det.Rollo_Id == roll
                                 select det).FirstOrDefault();

                if (dataEntry != null) {

                    dataEntry.Estado_Id = 24;
                    _context.Entry(dataEntry).State = EntityState.Modified;
                    _context.SaveChanges();

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RollExists(roll)) return NotFound();
                        else throw;
                    }
                }
                count++;
                if (count == rolls.Count()) return NoContent();
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // POST: api/DetalleEntradaRollo_Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleEntradaRollo_Producto>> PostDetalleEntradaRollo_Producto(DetalleEntradaRollo_Producto detalleEntradaRollo_Producto)
        {
            _context.DetallesEntradasRollos_Productos.Add(detalleEntradaRollo_Producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleEntradaRollo_Producto", new { id = detalleEntradaRollo_Producto.Codigo }, detalleEntradaRollo_Producto);
        }

        // DELETE: api/DetalleEntradaRollo_Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleEntradaRollo_Producto(long id)
        {
            var detalleEntradaRollo_Producto = await _context.DetallesEntradasRollos_Productos.FindAsync(id);
            if (detalleEntradaRollo_Producto == null)
            {
                return NotFound();
            }

            _context.DetallesEntradasRollos_Productos.Remove(detalleEntradaRollo_Producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Eliminar Rollo de la tabla
        [HttpDelete("EliminarRollo/{rollo}")]
        public ActionResult EliminarRollo(long rollo)
        {
            var x = (from y in _context.Set<DetalleEntradaRollo_Producto>()
                     where y.Rollo_Id == rollo
                     select y).FirstOrDefault();

            if (x == null)
            {
                return NoContent();
            }
            _context.DetallesEntradasRollos_Productos.Remove(x);
            _context.SaveChanges();

            return NoContent();

        }

        private bool DetalleEntradaRollo_ProductoExists(long id)
        {
            return _context.DetallesEntradasRollos_Productos.Any(e => e.Codigo == id);
        }

        private bool RollExists(long roll)
        {
            return _context.DetallesEntradasRollos_Productos.Any(e => e.Rollo_Id == roll);
        }
    }
}
