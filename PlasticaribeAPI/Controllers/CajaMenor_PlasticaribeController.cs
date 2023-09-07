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
    public class CajaMenor_PlasticaribeController : ControllerBase
    {
        private readonly dataContext _context;

        public CajaMenor_PlasticaribeController(dataContext context)
        {
            _context = context;
        }

        // GET: api/CajaMenor_Plasticaribe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CajaMenor_Plasticaribe>>> GetCajaMenor_Plasticaribe()
        {
          if (_context.CajaMenor_Plasticaribe == null)
          {
              return NotFound();
          }
            return await _context.CajaMenor_Plasticaribe.ToListAsync();
        }

        // GET: api/CajaMenor_Plasticaribe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CajaMenor_Plasticaribe>> GetCajaMenor_Plasticaribe(int id)
        {
          if (_context.CajaMenor_Plasticaribe == null)
          {
              return NotFound();
          }
            var cajaMenor_Plasticaribe = await _context.CajaMenor_Plasticaribe.FindAsync(id);

            if (cajaMenor_Plasticaribe == null)
            {
                return NotFound();
            }

            return cajaMenor_Plasticaribe;
        }

        // PUT: api/CajaMenor_Plasticaribe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCajaMenor_Plasticaribe(int id, CajaMenor_Plasticaribe cajaMenor_Plasticaribe)
        {
            if (id != cajaMenor_Plasticaribe.CajaMenor_Id)
            {
                return BadRequest();
            }

            _context.Entry(cajaMenor_Plasticaribe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CajaMenor_PlasticaribeExists(id))
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

        // POST: api/CajaMenor_Plasticaribe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CajaMenor_Plasticaribe>> PostCajaMenor_Plasticaribe(CajaMenor_Plasticaribe cajaMenor_Plasticaribe)
        {
          if (_context.CajaMenor_Plasticaribe == null)
          {
              return Problem("Entity set 'dataContext.CajaMenor_Plasticaribe'  is null.");
          }
            _context.CajaMenor_Plasticaribe.Add(cajaMenor_Plasticaribe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCajaMenor_Plasticaribe", new { id = cajaMenor_Plasticaribe.CajaMenor_Id }, cajaMenor_Plasticaribe);
        }

        // DELETE: api/CajaMenor_Plasticaribe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCajaMenor_Plasticaribe(int id)
        {
            if (_context.CajaMenor_Plasticaribe == null)
            {
                return NotFound();
            }
            var cajaMenor_Plasticaribe = await _context.CajaMenor_Plasticaribe.FindAsync(id);
            if (cajaMenor_Plasticaribe == null)
            {
                return NotFound();
            }

            _context.CajaMenor_Plasticaribe.Remove(cajaMenor_Plasticaribe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CajaMenor_PlasticaribeExists(int id)
        {
            return (_context.CajaMenor_Plasticaribe?.Any(e => e.CajaMenor_Id == id)).GetValueOrDefault();
        }
    }
}
