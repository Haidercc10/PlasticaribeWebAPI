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
    public class Nomina_PlasticaribeController : ControllerBase
    {
        private readonly dataContext _context;

        public Nomina_PlasticaribeController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Nomina_Plasticaribe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nomina_Plasticaribe>>> GetNomina_Plasticaribe()
        {
          if (_context.Nomina_Plasticaribe == null)
          {
              return NotFound();
          }
            return await _context.Nomina_Plasticaribe.ToListAsync();
        }

        // GET: api/Nomina_Plasticaribe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nomina_Plasticaribe>> GetNomina_Plasticaribe(int id)
        {
          if (_context.Nomina_Plasticaribe == null)
          {
              return NotFound();
          }
            var nomina_Plasticaribe = await _context.Nomina_Plasticaribe.FindAsync(id);

            if (nomina_Plasticaribe == null)
            {
                return NotFound();
            }

            return nomina_Plasticaribe;
        }

        // PUT: api/Nomina_Plasticaribe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNomina_Plasticaribe(int id, Nomina_Plasticaribe nomina_Plasticaribe)
        {
            if (id != nomina_Plasticaribe.Nomina_Id)
            {
                return BadRequest();
            }

            _context.Entry(nomina_Plasticaribe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Nomina_PlasticaribeExists(id))
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

        // POST: api/Nomina_Plasticaribe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nomina_Plasticaribe>> PostNomina_Plasticaribe(Nomina_Plasticaribe nomina_Plasticaribe)
        {
          if (_context.Nomina_Plasticaribe == null)
          {
              return Problem("Entity set 'dataContext.Nomina_Plasticaribe'  is null.");
          }
            _context.Nomina_Plasticaribe.Add(nomina_Plasticaribe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNomina_Plasticaribe", new { id = nomina_Plasticaribe.Nomina_Id }, nomina_Plasticaribe);
        }

        // DELETE: api/Nomina_Plasticaribe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNomina_Plasticaribe(int id)
        {
            if (_context.Nomina_Plasticaribe == null)
            {
                return NotFound();
            }
            var nomina_Plasticaribe = await _context.Nomina_Plasticaribe.FindAsync(id);
            if (nomina_Plasticaribe == null)
            {
                return NotFound();
            }

            _context.Nomina_Plasticaribe.Remove(nomina_Plasticaribe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Nomina_PlasticaribeExists(int id)
        {
            return (_context.Nomina_Plasticaribe?.Any(e => e.Nomina_Id == id)).GetValueOrDefault();
        }
    }
}
