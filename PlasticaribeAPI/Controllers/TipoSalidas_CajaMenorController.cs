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
    public class TipoSalidas_CajaMenorController : ControllerBase
    {
        private readonly dataContext _context;

        public TipoSalidas_CajaMenorController(dataContext context)
        {
            _context = context;
        }

        // GET: api/TipoSalidas_CajaMenor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSalidas_CajaMenor>>> GetTipoSalidas_CajaMenor()
        {
          if (_context.TipoSalidas_CajaMenor == null)
          {
              return NotFound();
          }
            return await _context.TipoSalidas_CajaMenor.ToListAsync();
        }

        // GET: api/TipoSalidas_CajaMenor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoSalidas_CajaMenor>> GetTipoSalidas_CajaMenor(int id)
        {
          if (_context.TipoSalidas_CajaMenor == null)
          {
              return NotFound();
          }
            var tipoSalidas_CajaMenor = await _context.TipoSalidas_CajaMenor.FindAsync(id);

            if (tipoSalidas_CajaMenor == null)
            {
                return NotFound();
            }

            return tipoSalidas_CajaMenor;
        }

        // PUT: api/TipoSalidas_CajaMenor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoSalidas_CajaMenor(int id, TipoSalidas_CajaMenor tipoSalidas_CajaMenor)
        {
            if (id != tipoSalidas_CajaMenor.TpSal_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoSalidas_CajaMenor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoSalidas_CajaMenorExists(id))
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

        // POST: api/TipoSalidas_CajaMenor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoSalidas_CajaMenor>> PostTipoSalidas_CajaMenor(TipoSalidas_CajaMenor tipoSalidas_CajaMenor)
        {
          if (_context.TipoSalidas_CajaMenor == null)
          {
              return Problem("Entity set 'dataContext.TipoSalidas_CajaMenor'  is null.");
          }
            _context.TipoSalidas_CajaMenor.Add(tipoSalidas_CajaMenor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoSalidas_CajaMenor", new { id = tipoSalidas_CajaMenor.TpSal_Id }, tipoSalidas_CajaMenor);
        }

        // DELETE: api/TipoSalidas_CajaMenor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoSalidas_CajaMenor(int id)
        {
            if (_context.TipoSalidas_CajaMenor == null)
            {
                return NotFound();
            }
            var tipoSalidas_CajaMenor = await _context.TipoSalidas_CajaMenor.FindAsync(id);
            if (tipoSalidas_CajaMenor == null)
            {
                return NotFound();
            }

            _context.TipoSalidas_CajaMenor.Remove(tipoSalidas_CajaMenor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoSalidas_CajaMenorExists(int id)
        {
            return (_context.TipoSalidas_CajaMenor?.Any(e => e.TpSal_Id == id)).GetValueOrDefault();
        }
    }
}
