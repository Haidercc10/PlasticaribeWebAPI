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
    public class Inventario_AreasController : ControllerBase
    {
        private readonly dataContext _context;

        public Inventario_AreasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Inventario_Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario_Areas>>> GetInventarios_Areas()
        {
          if (_context.Inventarios_Areas == null)
          {
              return NotFound();
          }
            return await _context.Inventarios_Areas.ToListAsync();
        }

        // GET: api/Inventario_Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario_Areas>> GetInventario_Areas(long id)
        {
          if (_context.Inventarios_Areas == null)
          {
              return NotFound();
          }
            var inventario_Areas = await _context.Inventarios_Areas.FindAsync(id);

            if (inventario_Areas == null)
            {
                return NotFound();
            }

            return inventario_Areas;
        }

        // PUT: api/Inventario_Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario_Areas(long id, Inventario_Areas inventario_Areas)
        {
            if (id != inventario_Areas.InvCodigo)
            {
                return BadRequest();
            }

            _context.Entry(inventario_Areas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Inventario_AreasExists(id))
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

        // POST: api/Inventario_Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventario_Areas>> PostInventario_Areas(Inventario_Areas inventario_Areas)
        {
          if (_context.Inventarios_Areas == null)
          {
              return Problem("Entity set 'dataContext.Inventarios_Areas'  is null.");
          }
            _context.Inventarios_Areas.Add(inventario_Areas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario_Areas", new { id = inventario_Areas.InvCodigo }, inventario_Areas);
        }

        // DELETE: api/Inventario_Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario_Areas(long id)
        {
            if (_context.Inventarios_Areas == null)
            {
                return NotFound();
            }
            var inventario_Areas = await _context.Inventarios_Areas.FindAsync(id);
            if (inventario_Areas == null)
            {
                return NotFound();
            }

            _context.Inventarios_Areas.Remove(inventario_Areas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Inventario_AreasExists(long id)
        {
            return (_context.Inventarios_Areas?.Any(e => e.InvCodigo == id)).GetValueOrDefault();
        }
    }
}
