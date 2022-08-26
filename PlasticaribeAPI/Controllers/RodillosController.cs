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
    public class RodillosController : ControllerBase
    {
        private readonly dataContext _context;

        public RodillosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Rodillos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rodillos>>> GetRodillos()
        {
          if (_context.Rodillos == null)
          {
              return NotFound();
          }
            return await _context.Rodillos.ToListAsync();
        }

        // GET: api/Rodillos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rodillos>> GetRodillos(int id)
        {
          if (_context.Rodillos == null)
          {
              return NotFound();
          }
            var rodillos = await _context.Rodillos.FindAsync(id);

            if (rodillos == null)
            {
                return NotFound();
            }

            return rodillos;
        }

        // PUT: api/Rodillos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRodillos(int id, Rodillos rodillos)
        {
            if (id != rodillos.Rodillo_Id)
            {
                return BadRequest();
            }

            _context.Entry(rodillos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RodillosExists(id))
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

        // POST: api/Rodillos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rodillos>> PostRodillos(Rodillos rodillos)
        {
          if (_context.Rodillos == null)
          {
              return Problem("Entity set 'dataContext.Rodillos'  is null.");
          }
            _context.Rodillos.Add(rodillos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRodillos", new { id = rodillos.Rodillo_Id }, rodillos);
        }

        // DELETE: api/Rodillos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRodillos(int id)
        {
            if (_context.Rodillos == null)
            {
                return NotFound();
            }
            var rodillos = await _context.Rodillos.FindAsync(id);
            if (rodillos == null)
            {
                return NotFound();
            }

            _context.Rodillos.Remove(rodillos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RodillosExists(int id)
        {
            return (_context.Rodillos?.Any(e => e.Rodillo_Id == id)).GetValueOrDefault();
        }
    }
}
