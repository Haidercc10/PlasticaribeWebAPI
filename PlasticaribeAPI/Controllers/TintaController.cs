using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TintaController : ControllerBase
    {
        private readonly dataContext _context;

        public TintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tinta>>> GetTintas()
        {
          if (_context.Tintas == null)
          {
              return NotFound();
          }
            return await _context.Tintas.ToListAsync();
        }

        // GET: api/Tinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tinta>> GetTinta(long id)
        {
          if (_context.Tintas == null)
          {
              return NotFound();
          }
            var tinta = await _context.Tintas.FindAsync(id);

            if (tinta == null)
            {
                return NotFound();
            }

            return tinta;
        }

        // PUT: api/Tinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTinta(long id, Tinta tinta)
        {
            if (id != tinta.Tinta_Id)
            {
                return BadRequest();
            }

            _context.Entry(tinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TintaExists(id))
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

        // POST: api/Tinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tinta>> PostTinta(Tinta tinta)
        {
          if (_context.Tintas == null)
          {
              return Problem("Entity set 'dataContext.Tintas'  is null.");
          }
            _context.Tintas.Add(tinta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTinta", new { id = tinta.Tinta_Id }, tinta);
        }

        // DELETE: api/Tinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTinta(long id)
        {
            if (_context.Tintas == null)
            {
                return NotFound();
            }
            var tinta = await _context.Tintas.FindAsync(id);
            if (tinta == null)
            {
                return NotFound();
            }

            _context.Tintas.Remove(tinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TintaExists(long id)
        {
            return (_context.Tintas?.Any(e => e.Tinta_Id == id)).GetValueOrDefault();
        }
    }
}
