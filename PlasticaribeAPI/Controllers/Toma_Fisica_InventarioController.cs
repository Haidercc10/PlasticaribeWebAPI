using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Toma_Fisica_InventarioController : ControllerBase
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
       
            private readonly dataContext _context;

            public Toma_Fisica_InventarioController(dataContext context)
            {
                _context = context;
            }

            // GET: api/Toma_Fisica_Inventario
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Toma_Fisica_Inventario>>> GetToma_Fisica_Inventario()
            {
                return await _context.Toma_Fisica_Inventario.ToListAsync();
            }

            // GET: api/Toma_Fisica_Inventario/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Toma_Fisica_Inventario>> GetToma_Fisica_Inventario(string id)
            {
                var Toma_Fisica_Inventario = await _context.Toma_Fisica_Inventario.FindAsync(id);

                if (Toma_Fisica_Inventario == null)
                {
                    return NotFound();
                }

                return Toma_Fisica_Inventario;
            }

            // GET: api/Toma_Fisica_Inventario/5/SELLA
            [HttpGet("getPhysicalInventory/{roll}/{process}")]
            public async Task<ActionResult<Toma_Fisica_Inventario>> getPhysicalInventory(long roll, string process)
            {
                var physicalInv = await _context.Set<Toma_Fisica_Inventario>()
                    .FirstOrDefaultAsync(tfi => tfi.Tfi_Etiqueta == roll && tfi.Proceso_Id == process);

                return Ok(physicalInv);
            }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Funcion para obtener el inventario fisico detallado por item
        [HttpGet("getPhysicalCountForItem/{item}")]
        public ActionResult getPhysicalCountForItem(long item)
        {
            var snapshot =
                from i in _context.Set<Toma_Fisica_Inventario>()
                join p in _context.Set<Producto>() on i.Prod_Id equals p.Prod_Id
                join c in _context.Set<Clientes>() on i.Cli_Id equals c.Cli_Id
                where p.Prod_Id == item
                select new
                {
                    Item = p.Prod_Id,
                    Reference = p.Prod_Nombre,
                    Label = i.Tfi_Etiqueta,
                    Ot = i.Tfi_OT,
                    Client = c.Cli_Nombre,
                    Warehouse = i.TpBod_Id,
                    Quantity = i.Tfi_CantidadReal,
                    GrossWeight = i.Tfi_PesoBruto,
                    Price = i.Tfi_PrecioVenta,
                    Unit = i.Presentacion,
                    Process = i.Proceso_Id,
                    Location = i.Tfi_Ubicacion,
                    Date = i.Tfi_Fecha,
                };

            return Ok(snapshot);
        }

        // PUT: api/Toma_Fisica_Inventario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
            public async Task<IActionResult> PutToma_Fisica_Inventario(long id, Toma_Fisica_Inventario Toma_Fisica_Inventario)
            {
                if (id != Toma_Fisica_Inventario.Tfi_Id)
                {
                    return BadRequest();
                }

                _context.Entry(Toma_Fisica_Inventario).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Toma_Fisica_InventarioExists(id))
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

            // POST: api/Toma_Fisica_Inventario
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<Toma_Fisica_Inventario>> PostToma_Fisica_inventario(Toma_Fisica_Inventario Toma_Fisica_inventario)
            {
                _context.Toma_Fisica_Inventario.Add(Toma_Fisica_inventario);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (Toma_Fisica_InventarioExists(Toma_Fisica_inventario.Tfi_Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetToma_Fisica_Inventario", new { id = Toma_Fisica_inventario.Tfi_Id }, Toma_Fisica_inventario);
            }

            // DELETE: api/Toma_Fisica_Inventario/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteToma_Fisica_Inventario(string id)
            {
                var Toma_Fisica_Inventario = await _context.Toma_Fisica_Inventario.FindAsync(id);
                if (Toma_Fisica_Inventario == null)
                {
                    return NotFound();
                }

                _context.Toma_Fisica_Inventario.Remove(Toma_Fisica_Inventario);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool Toma_Fisica_InventarioExists(long id)
            {
                return _context.Toma_Fisica_Inventario.Any(e => e.Tfi_Id == id);
            }
        }
    
}
