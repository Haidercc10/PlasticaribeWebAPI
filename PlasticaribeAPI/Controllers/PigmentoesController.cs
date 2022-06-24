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
    public class PigmentoesController : ControllerBase
    {
        private readonly dataContext _context;

        public PigmentoesController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Pigmentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pigmento>>> GetPigmentos()
        {
          if (_context.Pigmentos == null)
          {
              return NotFound();
          }
            return await _context.Pigmentos.ToListAsync();
        }

        // GET: api/Pigmentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pigmento>> GetPigmento(int id)
        {
          if (_context.Pigmentos == null)
          {
              return NotFound();
          }
            var pigmento = await _context.Pigmentos.FindAsync(id);

            if (pigmento == null)
            {
                return NotFound();
            }

            return pigmento;
        }

        // PUT: api/Pigmentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPigmento(int id, Pigmento pigmento)
        {
            if (id != pigmento.Pigmt_Id)
            {
                return BadRequest();
            }

            _context.Entry(pigmento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PigmentoExists(id))
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

        // POST: api/Pigmentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pigmento>> PostPigmento(Pigmento pigmento)
        {
          if (_context.Pigmentos == null)
          {
              return Problem("Entity set 'dataContext.Pigmentos'  is null.");
          }
            _context.Pigmentos.Add(pigmento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPigmento", new { id = pigmento.Pigmt_Id }, pigmento);
        }

        // DELETE: api/Pigmentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePigmento(int id)
        {
            if (_context.Pigmentos == null)
            {
                return NotFound();
            }
            var pigmento = await _context.Pigmentos.FindAsync(id);
            if (pigmento == null)
            {
                return NotFound();
            }

            _context.Pigmentos.Remove(pigmento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PigmentoExists(int id)
        {
            return (_context.Pigmentos?.Any(e => e.Pigmt_Id == id)).GetValueOrDefault();
        }
    }
}
