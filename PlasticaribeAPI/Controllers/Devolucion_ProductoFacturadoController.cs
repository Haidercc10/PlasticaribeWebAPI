using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Devolucion_ProductoFacturadoController : ControllerBase
    {
        private readonly dataContext _context;

        public Devolucion_ProductoFacturadoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Devolucion_ProductoFacturado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Devolucion_ProductoFacturado>>> GetDevoluciones_ProductosFacturados()
        {
            return await _context.Devoluciones_ProductosFacturados.ToListAsync();
        }

        // GET: api/Devolucion_ProductoFacturado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Devolucion_ProductoFacturado>> GetDevolucion_ProductoFacturado(long id)
        {
            var devolucion_ProductoFacturado = await _context.Devoluciones_ProductosFacturados.FindAsync(id);

            if (devolucion_ProductoFacturado == null)
            {
                return NotFound();
            }

            return devolucion_ProductoFacturado;
        }

        // PUT: api/Devolucion_ProductoFacturado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevolucion_ProductoFacturado(long id, Devolucion_ProductoFacturado devolucion_ProductoFacturado)
        {
            if (id != devolucion_ProductoFacturado.DevProdFact_Id)
            {
                return BadRequest();
            }

            _context.Entry(devolucion_ProductoFacturado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Devolucion_ProductoFacturadoExists(id))
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

        // POST: api/Devolucion_ProductoFacturado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Devolucion_ProductoFacturado>> PostDevolucion_ProductoFacturado(Devolucion_ProductoFacturado devolucion_ProductoFacturado)
        {
            _context.Devoluciones_ProductosFacturados.Add(devolucion_ProductoFacturado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevolucion_ProductoFacturado", new { id = devolucion_ProductoFacturado.DevProdFact_Id }, devolucion_ProductoFacturado);
        }

        // DELETE: api/Devolucion_ProductoFacturado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevolucion_ProductoFacturado(long id)
        {
            var devolucion_ProductoFacturado = await _context.Devoluciones_ProductosFacturados.FindAsync(id);
            if (devolucion_ProductoFacturado == null)
            {
                return NotFound();
            }

            _context.Devoluciones_ProductosFacturados.Remove(devolucion_ProductoFacturado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Devolucion_ProductoFacturadoExists(long id)
        {
            return _context.Devoluciones_ProductosFacturados.Any(e => e.DevProdFact_Id == id);
        }
    }
}
