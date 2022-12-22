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
    public class DesperdiciosController : ControllerBase
    {
        private readonly dataContext _context;

        public DesperdiciosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Desperdicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Desperdicio>>> GetDesperdicios()
        {
            return await _context.Desperdicios.ToListAsync();
        }

      

        // GET: api/Desperdicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desperdicio>> GetDesperdicio(long id)
        {
            var Desperdicio = await _context.Desperdicios.FindAsync(id);

            if (Desperdicio == null)
            {
                return NotFound();
            }

            return Desperdicio;
        }

        // PUT: api/Desperdicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesperdicio(long id, Desperdicio Desperdicio)
        {
            if (id != Desperdicio.Desp_Id)
            {
                return BadRequest();
            }

            _context.Entry(Desperdicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesperdicioExists(id))
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

        // POST: api/Desperdicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Desperdicio>> PostDesperdicio(Desperdicio Desperdicio)
        {
            _context.Desperdicios.Add(Desperdicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDesperdicio", new { id = Desperdicio.Desp_Id }, Desperdicio);
        }

        // DELETE: api/Desperdicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesperdicio(long id)
        {
            var Desperdicio = await _context.Desperdicios.FindAsync(id);
            if (Desperdicio == null)
            {
                return NotFound();
            }

            _context.Desperdicios.Remove(Desperdicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DesperdicioExists(long id)
        {
            return _context.Desperdicios.Any(e => e.Desp_Id == id);
        }
    }
}
