using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TipoDevolucion_ProductoFacturadoController : ControllerBase
    {
        private readonly dataContext _context;

        public TipoDevolucion_ProductoFacturadoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/TipoDevolucion_ProductoFacturado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDevolucion_ProductoFacturado>>> GetTiposDevoluciones_ProductosFacturados()
        {
            return await _context.TiposDevoluciones_ProductosFacturados.ToListAsync();
        }

        // GET: api/TipoDevolucion_ProductoFacturado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDevolucion_ProductoFacturado>> GetTipoDevolucion_ProductoFacturado(int id)
        {
            var tipoDevolucion_ProductoFacturado = await _context.TiposDevoluciones_ProductosFacturados.FindAsync(id);

            if (tipoDevolucion_ProductoFacturado == null)
            {
                return NotFound();
            }

            return tipoDevolucion_ProductoFacturado;
        }

        // PUT: api/TipoDevolucion_ProductoFacturado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoDevolucion_ProductoFacturado(int id, TipoDevolucion_ProductoFacturado tipoDevolucion_ProductoFacturado)
        {
            if (id != tipoDevolucion_ProductoFacturado.TipoDevProdFact_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoDevolucion_ProductoFacturado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDevolucion_ProductoFacturadoExists(id))
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

        // POST: api/TipoDevolucion_ProductoFacturado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoDevolucion_ProductoFacturado>> PostTipoDevolucion_ProductoFacturado(TipoDevolucion_ProductoFacturado tipoDevolucion_ProductoFacturado)
        {
            _context.TiposDevoluciones_ProductosFacturados.Add(tipoDevolucion_ProductoFacturado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoDevolucion_ProductoFacturado", new { id = tipoDevolucion_ProductoFacturado.TipoDevProdFact_Id }, tipoDevolucion_ProductoFacturado);
        }

        // DELETE: api/TipoDevolucion_ProductoFacturado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoDevolucion_ProductoFacturado(int id)
        {
            var tipoDevolucion_ProductoFacturado = await _context.TiposDevoluciones_ProductosFacturados.FindAsync(id);
            if (tipoDevolucion_ProductoFacturado == null)
            {
                return NotFound();
            }

            _context.TiposDevoluciones_ProductosFacturados.Remove(tipoDevolucion_ProductoFacturado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoDevolucion_ProductoFacturadoExists(int id)
        {
            return _context.TiposDevoluciones_ProductosFacturados.Any(e => e.TipoDevProdFact_Id == id);
        }
    }
}
