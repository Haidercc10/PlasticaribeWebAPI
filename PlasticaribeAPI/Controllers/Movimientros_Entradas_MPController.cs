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
    public class Movimientros_Entradas_MPController : ControllerBase
    {
        private readonly dataContext _context;

        public Movimientros_Entradas_MPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Movimientros_Entradas_MP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimientros_Entradas_MP>>> GetMovimientros_Entradas_MP()
        {
          if (_context.Movimientros_Entradas_MP == null)
          {
              return NotFound();
          }
            return await _context.Movimientros_Entradas_MP.ToListAsync();
        }

        // GET: api/Movimientros_Entradas_MP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimientros_Entradas_MP>> GetMovimientros_Entradas_MP(int id)
        {
          if (_context.Movimientros_Entradas_MP == null)
          {
              return NotFound();
          }
            var movimientros_Entradas_MP = await _context.Movimientros_Entradas_MP.FindAsync(id);

            if (movimientros_Entradas_MP == null)
            {
                return NotFound();
            }

            return movimientros_Entradas_MP;
        }

        // PUT: api/Movimientros_Entradas_MP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientros_Entradas_MP(int id, Movimientros_Entradas_MP movimientros_Entradas_MP)
        {
            if (id != movimientros_Entradas_MP.Id)
            {
                return BadRequest();
            }

            _context.Entry(movimientros_Entradas_MP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Movimientros_Entradas_MPExists(id))
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

        // POST: api/Movimientros_Entradas_MP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimientros_Entradas_MP>> PostMovimientros_Entradas_MP(Movimientros_Entradas_MP movimientros_Entradas_MP)
        {
          if (_context.Movimientros_Entradas_MP == null)
          {
              return Problem("Entity set 'dataContext.Movimientros_Entradas_MP'  is null.");
          }
            _context.Movimientros_Entradas_MP.Add(movimientros_Entradas_MP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimientros_Entradas_MP", new { id = movimientros_Entradas_MP.Id }, movimientros_Entradas_MP);
        }

        // DELETE: api/Movimientros_Entradas_MP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientros_Entradas_MP(int id)
        {
            if (_context.Movimientros_Entradas_MP == null)
            {
                return NotFound();
            }
            var movimientros_Entradas_MP = await _context.Movimientros_Entradas_MP.FindAsync(id);
            if (movimientros_Entradas_MP == null)
            {
                return NotFound();
            }

            _context.Movimientros_Entradas_MP.Remove(movimientros_Entradas_MP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Movimientros_Entradas_MPExists(int id)
        {
            return (_context.Movimientros_Entradas_MP?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
