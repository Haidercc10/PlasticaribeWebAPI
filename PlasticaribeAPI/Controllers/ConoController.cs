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
    public class ConoController : ControllerBase
    {
        private readonly dataContext _context;

        public ConoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Cono
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cono>>> GetConos()
        {
            return await _context.Conos.ToListAsync();
        }

        // GET: api/Cono/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cono>> GetCono(string id)
        {
            var cono = await _context.Conos.FindAsync(id);

            if (cono == null)
            {
                return NotFound();
            }

            return cono;
        }

        // PUT: api/Cono/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCono(string id, Cono cono)
        {
            if (id != cono.Cono_Id)
            {
                return BadRequest();
            }

            _context.Entry(cono).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConoExists(id))
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

        // POST: api/Cono
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cono>> PostCono(Cono cono)
        {
            _context.Conos.Add(cono);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConoExists(cono.Cono_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCono", new { id = cono.Cono_Id }, cono);
        }

        // DELETE: api/Cono/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCono(string id)
        {
            var cono = await _context.Conos.FindAsync(id);
            if (cono == null)
            {
                return NotFound();
            }

            _context.Conos.Remove(cono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConoExists(string id)
        {
            return _context.Conos.Any(e => e.Cono_Id == id);
        }
    }
}
