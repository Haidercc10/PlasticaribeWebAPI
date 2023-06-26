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
    public class Bodegas_RollosController : ControllerBase
    {
        private readonly dataContext _context;

        public Bodegas_RollosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Bodegas_Rollos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bodegas_Rollos>>> GetBodegas_Rollos()
        {
          if (_context.Bodegas_Rollos == null)
          {
              return NotFound();
          }
            return await _context.Bodegas_Rollos.ToListAsync();
        }

        // GET: api/Bodegas_Rollos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bodegas_Rollos>> GetBodegas_Rollos(long id)
        {
          if (_context.Bodegas_Rollos == null)
          {
              return NotFound();
          }
            var bodegas_Rollos = await _context.Bodegas_Rollos.FindAsync(id);

            if (bodegas_Rollos == null)
            {
                return NotFound();
            }

            return bodegas_Rollos;
        }

        // Consulta que validará que los rollos que le sean pasado en el array estén en la base de datos, retornará los rollos que estén en la base de datos.
        [HttpPost("getRollos")]
        public IActionResult GetRollos([FromBody] List<long> rollos)
        {
            return Ok(from e in _context.Set<Bodegas_Rollos>() 
                      where rollos.Contains(e.BgRollo_Rollo) 
                      select new { e.BgRollo_Rollo, e.BgRollo_BodegaActual });
        }

        // PUT: api/Bodegas_Rollos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodegas_Rollos(long id, Bodegas_Rollos bodegas_Rollos)
        {
            if (id != bodegas_Rollos.BgRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(bodegas_Rollos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Bodegas_RollosExists(id))
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

        // POST: api/Bodegas_Rollos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bodegas_Rollos>> PostBodegas_Rollos(Bodegas_Rollos bodegas_Rollos)
        {
          if (_context.Bodegas_Rollos == null)
          {
              return Problem("Entity set 'dataContext.Bodegas_Rollos'  is null.");
          }
            _context.Bodegas_Rollos.Add(bodegas_Rollos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBodegas_Rollos", new { id = bodegas_Rollos.BgRollo_Id }, bodegas_Rollos);
        }

        // DELETE: api/Bodegas_Rollos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodegas_Rollos(long id)
        {
            if (_context.Bodegas_Rollos == null)
            {
                return NotFound();
            }
            var bodegas_Rollos = await _context.Bodegas_Rollos.FindAsync(id);
            if (bodegas_Rollos == null)
            {
                return NotFound();
            }

            _context.Bodegas_Rollos.Remove(bodegas_Rollos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Bodegas_RollosExists(long id)
        {
            return (_context.Bodegas_Rollos?.Any(e => e.BgRollo_Id == id)).GetValueOrDefault();
        }
    }
}
