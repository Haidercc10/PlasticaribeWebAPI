using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Tipos_ImpresionController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipos_ImpresionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipos_Impresion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipos_Impresion>>> GetTipos_Impresion()
        {
            if (_context.Tipos_Impresion == null)
            {
                return NotFound();
            }
            return await _context.Tipos_Impresion.ToListAsync();
        }

        // GET: api/Tipos_Impresion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipos_Impresion>> GetTipos_Impresion(int id)
        {
            if (_context.Tipos_Impresion == null)
            {
                return NotFound();
            }
            var tipos_Impresion = await _context.Tipos_Impresion.FindAsync(id);

            if (tipos_Impresion == null)
            {
                return NotFound();
            }

            return tipos_Impresion;
        }

        // PUT: api/Tipos_Impresion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipos_Impresion(int id, Tipos_Impresion tipos_Impresion)
        {
            if (id != tipos_Impresion.TpImpresion_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipos_Impresion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipos_ImpresionExists(id))
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

        // POST: api/Tipos_Impresion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipos_Impresion>> PostTipos_Impresion(Tipos_Impresion tipos_Impresion)
        {
            if (_context.Tipos_Impresion == null)
            {
                return Problem("Entity set 'dataContext.Tipos_Impresion'  is null.");
            }
            _context.Tipos_Impresion.Add(tipos_Impresion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipos_Impresion", new { id = tipos_Impresion.TpImpresion_Id }, tipos_Impresion);
        }

        // DELETE: api/Tipos_Impresion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipos_Impresion(int id)
        {
            if (_context.Tipos_Impresion == null)
            {
                return NotFound();
            }
            var tipos_Impresion = await _context.Tipos_Impresion.FindAsync(id);
            if (tipos_Impresion == null)
            {
                return NotFound();
            }

            _context.Tipos_Impresion.Remove(tipos_Impresion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipos_ImpresionExists(int id)
        {
            return (_context.Tipos_Impresion?.Any(e => e.TpImpresion_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
