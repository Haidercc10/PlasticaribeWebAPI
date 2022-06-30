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
    public class RemisionController : ControllerBase
    {
        private readonly dataContext _context;

        public RemisionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Remision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remision>>> GetRemisiones()
        {
          if (_context.Remisiones == null)
          {
              return NotFound();
          }
            return await _context.Remisiones.ToListAsync();
        }

        // GET: api/Remision/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remision>> GetRemision(int id)
        {
          if (_context.Remisiones == null)
          {
              return NotFound();
          }
            var remision = await _context.Remisiones.FindAsync(id);

            if (remision == null)
            {
                return NotFound();
            }

            return remision;
        }

        // PUT: api/Remision/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemision(int id, Remision remision)
        {
            if (id != remision.Rem_Id)
            {
                return BadRequest();
            }

            _context.Entry(remision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemisionExists(id))
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

        // POST: api/Remision
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Remision>> PostRemision(Remision remision)
        {
          if (_context.Remisiones == null)
          {
              return Problem("Entity set 'dataContext.Remisiones'  is null.");
          }
            _context.Remisiones.Add(remision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRemision", new { id = remision.Rem_Id }, remision);
        }

        // DELETE: api/Remision/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemision(int id)
        {
            if (_context.Remisiones == null)
            {
                return NotFound();
            }
            var remision = await _context.Remisiones.FindAsync(id);
            if (remision == null)
            {
                return NotFound();
            }

            _context.Remisiones.Remove(remision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RemisionExists(int id)
        {
            return (_context.Remisiones?.Any(e => e.Rem_Id == id)).GetValueOrDefault();
        }
    }
}
