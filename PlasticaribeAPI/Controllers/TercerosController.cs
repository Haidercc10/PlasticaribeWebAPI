using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TercerosController : ControllerBase
    {
        private readonly dataContext _context;

        public TercerosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Terceros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Terceros>>> GetTerceros()
        {
          if (_context.Terceros == null)
          {
              return NotFound();
          }
            return await _context.Terceros.ToListAsync();
        }

        // GET: api/Terceros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Terceros>> GetTerceros(string id)
        {
          if (_context.Terceros == null)
          {
              return NotFound();
          }
            var terceros = await _context.Terceros.FindAsync(id);

            if (terceros == null)
            {
                return NotFound();
            }

            return terceros;
        }

        // Consultará los terceros por el nombre
        [HttpGet("getTerceroLike/{nombre}")]
        public ActionResult getTerceroLike(string nombre)
        {
            var con = _context.Terceros
                .Where(x => x.Tercero_Nombre.StartsWith(nombre) || Convert.ToString(x.Tercero_Id).StartsWith(nombre))
                .Select(x => new
                {
                    x.Tercero_Id,
                    x.Tercero_Nombre,
                });
            return Ok(con);
        }

        // PUT: api/Terceros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerceros(string id, Terceros terceros)
        {
            if (id != terceros.Tercero_Id)
            {
                return BadRequest();
            }

            _context.Entry(terceros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TercerosExists(id))
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

        // POST: api/Terceros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Terceros>> PostTerceros(Terceros terceros)
        {
          if (_context.Terceros == null)
          {
              return Problem("Entity set 'dataContext.Terceros'  is null.");
          }
            _context.Terceros.Add(terceros);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TercerosExists(terceros.Tercero_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTerceros", new { id = terceros.Tercero_Id }, terceros);
        }

        // DELETE: api/Terceros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerceros(string id)
        {
            if (_context.Terceros == null)
            {
                return NotFound();
            }
            var terceros = await _context.Terceros.FindAsync(id);
            if (terceros == null)
            {
                return NotFound();
            }

            _context.Terceros.Remove(terceros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TercerosExists(string id)
        {
            return (_context.Terceros?.Any(e => e.Tercero_Id == id)).GetValueOrDefault();
        }
    }
}
