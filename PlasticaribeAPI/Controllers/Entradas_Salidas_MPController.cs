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
    public class Entradas_Salidas_MPController : ControllerBase
    {
        private readonly dataContext _context;

        public Entradas_Salidas_MPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Entradas_Salidas_MP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entradas_Salidas_MP>>> GetEntradas_Salidas_MP()
        {
          if (_context.Entradas_Salidas_MP == null)
          {
              return NotFound();
          }
            return await _context.Entradas_Salidas_MP.ToListAsync();
        }

        // GET: api/Entradas_Salidas_MP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entradas_Salidas_MP>> GetEntradas_Salidas_MP(int id)
        {
          if (_context.Entradas_Salidas_MP == null)
          {
              return NotFound();
          }
            var entradas_Salidas_MP = await _context.Entradas_Salidas_MP.FindAsync(id);

            if (entradas_Salidas_MP == null)
            {
                return NotFound();
            }

            return entradas_Salidas_MP;
        }

        // PUT: api/Entradas_Salidas_MP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntradas_Salidas_MP(int id, Entradas_Salidas_MP entradas_Salidas_MP)
        {
            if (id != entradas_Salidas_MP.Id)
            {
                return BadRequest();
            }

            _context.Entry(entradas_Salidas_MP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Entradas_Salidas_MPExists(id))
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

        // POST: api/Entradas_Salidas_MP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entradas_Salidas_MP>> PostEntradas_Salidas_MP(Entradas_Salidas_MP entradas_Salidas_MP)
        {
          if (_context.Entradas_Salidas_MP == null)
          {
              return Problem("Entity set 'dataContext.Entradas_Salidas_MP'  is null.");
          }
            _context.Entradas_Salidas_MP.Add(entradas_Salidas_MP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntradas_Salidas_MP", new { id = entradas_Salidas_MP.Id }, entradas_Salidas_MP);
        }

        // DELETE: api/Entradas_Salidas_MP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntradas_Salidas_MP(int id)
        {
            if (_context.Entradas_Salidas_MP == null)
            {
                return NotFound();
            }
            var entradas_Salidas_MP = await _context.Entradas_Salidas_MP.FindAsync(id);
            if (entradas_Salidas_MP == null)
            {
                return NotFound();
            }

            _context.Entradas_Salidas_MP.Remove(entradas_Salidas_MP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Entradas_Salidas_MPExists(int id)
        {
            return (_context.Entradas_Salidas_MP?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
