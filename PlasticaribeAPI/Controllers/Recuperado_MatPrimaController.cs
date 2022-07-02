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
    public class Recuperado_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Recuperado_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Recuperado_MatPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recuperado_MatPrima>>> GetRecuperados_MatPrima()
        {
          if (_context.Recuperados_MatPrima == null)
          {
              return NotFound();
          }
            return await _context.Recuperados_MatPrima.ToListAsync();
        }

        // GET: api/Recuperado_MatPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recuperado_MatPrima>> GetRecuperado_MatPrima(long id)
        {
          if (_context.Recuperados_MatPrima == null)
          {
              return NotFound();
          }
            var recuperado_MatPrima = await _context.Recuperados_MatPrima.FindAsync(id);

            if (recuperado_MatPrima == null)
            {
                return NotFound();
            }

            return recuperado_MatPrima;
        }

        // PUT: api/Recuperado_MatPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecuperado_MatPrima(long id, Recuperado_MatPrima recuperado_MatPrima)
        {
            if (id != recuperado_MatPrima.RecMp_Id)
            {
                return BadRequest();
            }

            _context.Entry(recuperado_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Recuperado_MatPrimaExists(id))
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

        // POST: api/Recuperado_MatPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recuperado_MatPrima>> PostRecuperado_MatPrima(Recuperado_MatPrima recuperado_MatPrima)
        {
          if (_context.Recuperados_MatPrima == null)
          {
              return Problem("Entity set 'dataContext.Recuperados_MatPrima'  is null.");
          }
            _context.Recuperados_MatPrima.Add(recuperado_MatPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecuperado_MatPrima", new { id = recuperado_MatPrima.RecMp_Id }, recuperado_MatPrima);
        }

        // DELETE: api/Recuperado_MatPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecuperado_MatPrima(long id)
        {
            if (_context.Recuperados_MatPrima == null)
            {
                return NotFound();
            }
            var recuperado_MatPrima = await _context.Recuperados_MatPrima.FindAsync(id);
            if (recuperado_MatPrima == null)
            {
                return NotFound();
            }

            _context.Recuperados_MatPrima.Remove(recuperado_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Recuperado_MatPrimaExists(long id)
        {
            return (_context.Recuperados_MatPrima?.Any(e => e.RecMp_Id == id)).GetValueOrDefault();
        }
    }
}
