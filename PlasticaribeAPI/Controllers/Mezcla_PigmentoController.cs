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
    public class Mezcla_PigmentoController : ControllerBase
    {
        private readonly dataContext _context;

        public Mezcla_PigmentoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Mezcla_Pigmento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mezcla_Pigmento>>> GetMezclas_Pigmentos()
        {
          if (_context.Mezclas_Pigmentos == null)
          {
              return NotFound();
          }
            return await _context.Mezclas_Pigmentos.ToListAsync();
        }

        // GET: api/Mezcla_Pigmento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mezcla_Pigmento>> GetMezcla_Pigmento(int id)
        {
          if (_context.Mezclas_Pigmentos == null)
          {
              return NotFound();
          }
            var mezcla_Pigmento = await _context.Mezclas_Pigmentos.FindAsync(id);

            if (mezcla_Pigmento == null)
            {
                return NotFound();
            }

            return mezcla_Pigmento;
        }

        // PUT: api/Mezcla_Pigmento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMezcla_Pigmento(int id, Mezcla_Pigmento mezcla_Pigmento)
        {
            if (id != mezcla_Pigmento.MezPigmto_Id)
            {
                return BadRequest();
            }

            _context.Entry(mezcla_Pigmento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Mezcla_PigmentoExists(id))
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

        // POST: api/Mezcla_Pigmento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mezcla_Pigmento>> PostMezcla_Pigmento(Mezcla_Pigmento mezcla_Pigmento)
        {
          if (_context.Mezclas_Pigmentos == null)
          {
              return Problem("Entity set 'dataContext.Mezclas_Pigmentos'  is null.");
          }
            _context.Mezclas_Pigmentos.Add(mezcla_Pigmento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMezcla_Pigmento", new { id = mezcla_Pigmento.MezPigmto_Id }, mezcla_Pigmento);
        }

        // DELETE: api/Mezcla_Pigmento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMezcla_Pigmento(int id)
        {
            if (_context.Mezclas_Pigmentos == null)
            {
                return NotFound();
            }
            var mezcla_Pigmento = await _context.Mezclas_Pigmentos.FindAsync(id);
            if (mezcla_Pigmento == null)
            {
                return NotFound();
            }

            _context.Mezclas_Pigmentos.Remove(mezcla_Pigmento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Mezcla_PigmentoExists(int id)
        {
            return (_context.Mezclas_Pigmentos?.Any(e => e.MezPigmto_Id == id)).GetValueOrDefault();
        }
    }
}
