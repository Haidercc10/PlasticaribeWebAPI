using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Inventario_Mensual_ProductosController : ControllerBase
    {
        private readonly dataContext _context;

        public Inventario_Mensual_ProductosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Inventario_Mensual_Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario_Mensual_Productos>>> GetInventario_Mensual_Productos()
        {
            if (_context.Inventario_Mensual_Productos == null)
            {
                return NotFound();
            }
            return await _context.Inventario_Mensual_Productos.ToListAsync();
        }

        // GET: api/Inventario_Mensual_Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario_Mensual_Productos>> GetInventario_Mensual_Productos(long id)
        {
            if (_context.Inventario_Mensual_Productos == null)
            {
                return NotFound();
            }
            var inventario_Mensual_Productos = await _context.Inventario_Mensual_Productos.FindAsync(id);

            if (inventario_Mensual_Productos == null)
            {
                return NotFound();
            }

            return inventario_Mensual_Productos;
        }

        [HttpGet("get_Cantidad_Productos_Meses")]
        public ActionResult Get_Cantidad_Productos_Meses()
        {
            var enero = (from inv in _context.Set<Inventario_Mensual_Productos>()
                         join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                         select inv.Enero * pr.ExProd_PrecioVenta).Sum();

            var febrero = (from inv in _context.Set<Inventario_Mensual_Productos>()
                           join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                           select inv.Febrero * pr.ExProd_PrecioVenta).Sum();

            var marzo = (from inv in _context.Set<Inventario_Mensual_Productos>()
                         join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                         select inv.Marzo * pr.ExProd_PrecioVenta).Sum();

            var abril = (from inv in _context.Set<Inventario_Mensual_Productos>()
                         join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                         select inv.Abril * pr.ExProd_PrecioVenta).Sum();

            var mayo = (from inv in _context.Set<Inventario_Mensual_Productos>()
                        join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                        select inv.Mayo * pr.ExProd_PrecioVenta).Sum();

            var junio = (from inv in _context.Set<Inventario_Mensual_Productos>()
                         join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                         select inv.Junio * pr.ExProd_PrecioVenta).Sum();

            var julio = (from inv in _context.Set<Inventario_Mensual_Productos>()
                         join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                         select inv.Julio * pr.ExProd_PrecioVenta).Sum();

            var agosto = (from inv in _context.Set<Inventario_Mensual_Productos>()
                          join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                          select inv.Agosto * pr.ExProd_PrecioVenta).Sum();

            var septiembre = (from inv in _context.Set<Inventario_Mensual_Productos>()
                              join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                              select inv.Septiembre * pr.ExProd_PrecioVenta).Sum();

            var octubre = (from inv in _context.Set<Inventario_Mensual_Productos>()
                           join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                           select inv.Octubre * pr.ExProd_PrecioVenta).Sum();

            var novimebre = (from inv in _context.Set<Inventario_Mensual_Productos>()
                             join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                             select inv.Noviembre * pr.ExProd_PrecioVenta).Sum();

            var diciembre = (from inv in _context.Set<Inventario_Mensual_Productos>()
                             join pr in _context.Set<Existencia_Productos>() on inv.Prod_Id equals pr.Prod_Id
                             select inv.Diciembre * pr.ExProd_PrecioVenta).Sum();

            var result = new List<object>();
            result.Add($"'Enero': '{enero}'," +
                      $"'Febrero': '{febrero}'," +
                      $"'Marzo': '{marzo}'," +
                      $"'Abril': '{abril}'," +
                      $"'Mayo': '{mayo}'," +
                      $"'Junio': '{junio}'," +
                      $"'Julio': '{julio}'," +
                      $"'Agosto': '{agosto}'," +
                      $"'Septiembre': '{septiembre}'," +
                      $"'Octubre': '{octubre}'," +
                      $"'Noviembre': '{novimebre}'," +
                      $"'Diciembre': '{diciembre}'");

            return Ok(result);
        }

        // Consulta que traerá la cantidad de cada producto en cada mes
        [HttpGet("getCantidadMes_Producto/{prod}/{und}")]
        public ActionResult GetCantidadMes_Producto(long prod, string und)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from inv in _context.Set<Inventario_Mensual_Productos>()
                      from exi in _context.Set<Existencia_Productos>()
                      where inv.Prod_Id == prod
                            && inv.UndMed_Id == und
                            && inv.Prod_Id == exi.Prod_Id
                            && inv.UndMed_Id == exi.UndMed_Id
                      select new
                      {
                          Id = exi.Prod_Id,
                          Nombre = exi.Prod.Prod_Nombre,
                          Stock = exi.ExProd_Cantidad,
                          Und = exi.UndMed_Id,
                          Cant_Minima = exi.ExProd_CantMinima,
                          Enero = inv.Enero,
                          Febrero = inv.Febrero,
                          Marzo = inv.Marzo,
                          Abril = inv.Abril,
                          Mayo = inv.Mayo,
                          Junio = inv.Junio,
                          Julio = inv.Julio,
                          Agosto = inv.Agosto,
                          Septiembre = inv.Septiembre,
                          Octubre = inv.Octubre,
                          Noviembre = inv.Noviembre,
                          Diciembre = inv.Diciembre,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // Consulta que traerá la cantidad de cada producto en cada mes
        [HttpGet("getInventarioProductoInicioMes/{fechaFin}")]
        public ActionResult GetInventarioProductoInicioMes(DateTime fechaFin)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from inv in _context.Set<Inventario_Mensual_Productos>()
                      join exi in _context.Set<Existencia_Productos>()
                      on inv.Prod_Id equals exi.Prod_Id
                      where inv.Prod_Id != 1
                      && fechaFin.Month == 1 ? inv.Enero >= 1 :
                         fechaFin.Month == 2 ? inv.Febrero >= 1 :
                         fechaFin.Month == 3 ? inv.Marzo >= 1 :
                         fechaFin.Month == 4 ? inv.Abril >= 1 :
                         fechaFin.Month == 5 ? inv.Mayo >= 1 :
                         fechaFin.Month == 6 ? inv.Junio >= 1 :
                         fechaFin.Month == 7 ? inv.Julio >= 1 :
                         fechaFin.Month == 8 ? inv.Agosto >= 1 :
                         fechaFin.Month == 9 ? inv.Septiembre >= 1 :
                         fechaFin.Month == 10 ? inv.Octubre >= 1 :
                         fechaFin.Month == 11 ? inv.Noviembre >= 1 :
                         fechaFin.Month == 13 ? inv.Diciembre >= 1 : inv.Enero >= 1
                      select new {
                          Fecha_Inventario = new DateTime(fechaFin.Year, fechaFin.Month, 1),
                          Ot = "",
                          Item = inv.Prod_Id,
                          Referencia = exi.Prod.Prod_Nombre,
                          Stock = fechaFin.Month == 1 ? inv.Enero :
                                  fechaFin.Month == 2 ? inv.Febrero :
                                  fechaFin.Month == 3 ? inv.Marzo :
                                  fechaFin.Month == 4 ? inv.Abril :
                                  fechaFin.Month == 5 ? inv.Mayo :
                                  fechaFin.Month == 6 ? inv.Junio :
                                  fechaFin.Month == 7 ? inv.Julio :
                                  fechaFin.Month == 8 ? inv.Agosto :
                                  fechaFin.Month == 9 ? inv.Septiembre :
                                  fechaFin.Month == 10 ? inv.Octubre :
                                  fechaFin.Month == 11 ? inv.Noviembre :
                                  fechaFin.Month == 13 ? inv.Diciembre : inv.Enero,
                          Precio = exi.ExProd_PrecioVenta,
                          Subtotal = (fechaFin.Month == 1 ? inv.Enero :
                                      fechaFin.Month == 2 ? inv.Febrero :
                                      fechaFin.Month == 3 ? inv.Marzo :
                                      fechaFin.Month == 4 ? inv.Abril :
                                      fechaFin.Month == 5 ? inv.Mayo :
                                      fechaFin.Month == 6 ? inv.Junio :
                                      fechaFin.Month == 7 ? inv.Julio :
                                      fechaFin.Month == 8 ? inv.Agosto :
                                      fechaFin.Month == 9 ? inv.Septiembre :
                                      fechaFin.Month == 10 ? inv.Octubre :
                                      fechaFin.Month == 11 ? inv.Noviembre :
                                      fechaFin.Month == 13 ? inv.Diciembre : inv.Enero) * exi.ExProd_PrecioVenta,
                      };
            if (con == null) return BadRequest("No se encontraron Productos en la fecha consultada");
            else return Ok(con);
        }    

        // PUT: api/Inventario_Mensual_Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario_Mensual_Productos(long id, Inventario_Mensual_Productos inventario_Mensual_Productos)
        {
            if (id != inventario_Mensual_Productos.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(inventario_Mensual_Productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Inventario_Mensual_ProductosExists(id))
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

        // POST: api/Inventario_Mensual_Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventario_Mensual_Productos>> PostInventario_Mensual_Productos(Inventario_Mensual_Productos inventario_Mensual_Productos)
        {
            if (_context.Inventario_Mensual_Productos == null)
            {
                return Problem("Entity set 'dataContext.Inventario_Mensual_Productos'  is null.");
            }
            _context.Inventario_Mensual_Productos.Add(inventario_Mensual_Productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario_Mensual_Productos", new { id = inventario_Mensual_Productos.Codigo }, inventario_Mensual_Productos);
        }

        // DELETE: api/Inventario_Mensual_Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario_Mensual_Productos(long id)
        {
            if (_context.Inventario_Mensual_Productos == null)
            {
                return NotFound();
            }
            var inventario_Mensual_Productos = await _context.Inventario_Mensual_Productos.FindAsync(id);
            if (inventario_Mensual_Productos == null)
            {
                return NotFound();
            }

            _context.Inventario_Mensual_Productos.Remove(inventario_Mensual_Productos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Inventario_Mensual_ProductosExists(long id)
        {
            return (_context.Inventario_Mensual_Productos?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
