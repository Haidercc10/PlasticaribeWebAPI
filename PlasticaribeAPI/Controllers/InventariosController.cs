using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {

        private readonly dataContext _context;

        public InventariosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventarios>>> GetInventarios()
        {
            return await _context.Inventarios.ToListAsync();
        }

        // GET: api/Inventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventarios>> GetInventarios(string id)
        {
            var Inventarios = await _context.Inventarios.FindAsync(id);

            if (Inventarios == null)
            {
                return NotFound();
            }

            return Inventarios;
        }

        
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Funcion para obtener el inventario fisico
        [HttpGet("getInventorySnapshot")]
        public ActionResult getInventorySnapshot()
        {

            var tomaFisicaAgrupada = from tf in _context.Set<Toma_Fisica_Inventario>()
                                     group tf by tf.Prod_Id into g
                                     select new
                                     {
                                        Prod_Id = g.Key,
                                        PhysicalCount = (decimal?)g.Sum(x => x.Tfi_CantidadReal),
                                        PhysicalRollos = (int?)g.Count()
                                     };


            var snapshot =
                from i in _context.Set<Inventarios>()
                join p in _context.Set<Producto>()
                    on i.Prod_Id equals p.Prod_Id

                
                join tf in tomaFisicaAgrupada
                    on i.Prod_Id equals tf.Prod_Id into tfJoin
                from tf in tfJoin.DefaultIfEmpty()

                group new { i, tf } by new
                {
                    i.Prod_Id,
                    p.Prod_Nombre,
                    i.Presentacion,
                    i.Inv_PrecioVenta,
                    i.Inv_Existencias,
                    tf.PhysicalCount,
                    tf.PhysicalRollos
                } into g
                select new
                {
                    Item = g.Key.Prod_Id,
                    Reference = g.Key.Prod_Nombre,

                    
                    Stock = g.Key.Inv_Existencias,
                    Quantity = g.Sum(x => x.i.Inv_Cantidad),
                    GrossWeight = g.Sum(x => x.i.Inv_PesoBruto),
                    Price = g.Key.Inv_PrecioVenta,
                    Unit = g.Key.Presentacion,
                    Count = g.Count(),

                    
                    PhysicalQty = g.Key.PhysicalCount ?? 0,
                    PhysicalRollos = g.Key.PhysicalRollos ?? 0,

                    Diference = g.Key.Inv_Existencias - (g.Key.PhysicalCount ?? 0), 
                    Diference2 = g.Sum(x => x.i.Inv_Cantidad) - (g.Key.PhysicalCount ?? 0), 
                    Diference3 = g.Key.Inv_Existencias - g.Sum(x => x.i.Inv_Cantidad), 
                    Subtotal = (g.Key.Inv_Existencias * g.Key.Inv_PrecioVenta), 
                    DiferenceUnits = g.Count() - (g.Key.PhysicalRollos ?? 0),
                    SubtotalDetailed = (g.Sum(x => x.i.Inv_Cantidad) * g.Key.Inv_PrecioVenta),
                    SubTotalPhysical = ((g.Key.PhysicalCount ?? 0) * g.Key.Inv_PrecioVenta)
                };

            return Ok(snapshot);
        }

        
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Funcion para obtener el inventario fisico detallado por item
        [HttpGet("getInventorySnapshotForItem/{item}/{unit}")]
        public ActionResult getInventorySnapshotDetailed(long item, string unit)
        {
            var snapshot =
                from i in _context.Set<Inventarios>()
                join p in _context.Set<Producto>() on i.Prod_Id equals p.Prod_Id
                join c in _context.Set<Clientes>() on i.Cli_Id equals c.Cli_Id
                where p.Prod_Id == item
                && i.Presentacion == unit
                select new
                {
                    Item = p.Prod_Id,
                    Reference = p.Prod_Nombre,
                    Label = i.Inv_Etiqueta,
                    Ot = i.Inv_OT,
                    Client = c.Cli_Nombre,
                    Warehouse = i.TpBod_Id,
                    Quantity = i.Inv_Cantidad,
                    GrossWeight = i.Inv_PesoBruto,
                    Price = i.Inv_PrecioVenta,
                    Unit = i.Presentacion,
                    Process = i.Proceso_Id,
                    Location = i.Inv_Ubicacion,
                    Date = i.Inv_Fecha,
                    SubTotal = i.Inv_Cantidad * i.Inv_PrecioVenta
                };

            return Ok(snapshot);
        }

        // PUT: api/Inventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventarios(long id, Inventarios Inventarios)
        {
            if (id != Inventarios.Inv_Id)
            {
                return BadRequest();
            }

            _context.Entry(Inventarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventariosExists(id))
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

        // POST: api/Inventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventarios>> PostInventarios(Inventarios Inventarios)
        {
            _context.Inventarios.Add(Inventarios);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InventariosExists(Inventarios.Inv_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInventarios", new { id = Inventarios.Inv_Id }, Inventarios);
        }

        // DELETE: api/Inventarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventarios(string id)
        {
            var Inventarios = await _context.Inventarios.FindAsync(id);
            if (Inventarios == null)
            {
                return NotFound();
            }

            _context.Inventarios.Remove(Inventarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventariosExists(long id)
        {
            return _context.Inventarios.Any(e => e.Inv_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
