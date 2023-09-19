using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PistasController : ControllerBase
    {
        private readonly dataContext _context;

        public PistasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Pistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pistas>>> GetPistas()
        {
            if (_context.Pistas == null)
            {
                return NotFound();
            }
            return await _context.Pistas.ToListAsync();
        }

        // GET: api/Pistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pistas>> GetPistas(int id)
        {
            if (_context.Pistas == null)
            {
                return NotFound();
            }
            var pistas = await _context.Pistas.FindAsync(id);

            if (pistas == null)
            {
                return NotFound();
            }

            return pistas;
        }

        // PUT: api/Pistas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPistas(int id, Pistas pistas)
        {
            if (id != pistas.Pista_Id)
            {
                return BadRequest();
            }

            _context.Entry(pistas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PistasExists(id))
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

        // POST: api/Pistas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pistas>> PostPistas(Pistas pistas)
        {
            if (_context.Pistas == null)
            {
                return Problem("Entity set 'dataContext.Pistas'  is null.");
            }
            _context.Pistas.Add(pistas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPistas", new { id = pistas.Pista_Id }, pistas);
        }

        // DELETE: api/Pistas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePistas(int id)
        {
            if (_context.Pistas == null)
            {
                return NotFound();
            }
            var pistas = await _context.Pistas.FindAsync(id);
            if (pistas == null)
            {
                return NotFound();
            }

            _context.Pistas.Remove(pistas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PistasExists(int id)
        {
            return (_context.Pistas?.Any(e => e.Pista_Id == id)).GetValueOrDefault();
        }
    }
}
