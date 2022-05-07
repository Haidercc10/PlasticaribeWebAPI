#nullable disable
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
    public class EPSController : ControllerBase
    {
        private readonly dataContext _context;

        public EPSController(dataContext context)
        {
            _context = context;
        }

        // GET: api/EPS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EPS>>> GetEPS()
        {
            return await _context.EPS.ToListAsync();
        }

        // GET: api/EPS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EPS>> GetEPS(long id)
        {
            var ePS = await _context.EPS.FindAsync(id);

            if (ePS == null)
            {
                return NotFound();
            }

            return ePS;
        }

        // PUT: api/EPS/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEPS(long id, EPS ePS)
        {
            if (id != ePS.eps_Id)
            {
                return BadRequest();
            }

            _context.Entry(ePS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EPSExists(id))
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

        // POST: api/EPS
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EPS>> PostEPS(EPS ePS)
        {
            _context.EPS.Add(ePS);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EPSExists(ePS.eps_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEPS", new { id = ePS.eps_Id }, ePS);
        }

        // DELETE: api/EPS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEPS(long id)
        {
            var ePS = await _context.EPS.FindAsync(id);
            if (ePS == null)
            {
                return NotFound();
            }

            _context.EPS.Remove(ePS);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EPSExists(long id)
        {
            return _context.EPS.Any(e => e.eps_Id == id);
        }
    }
}
