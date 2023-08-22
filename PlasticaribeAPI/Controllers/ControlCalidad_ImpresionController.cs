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
    public class ControlCalidad_ImpresionController : ControllerBase
    {
        private readonly dataContext _context;

        public ControlCalidad_ImpresionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/ControlCalidad_Impresion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlCalidad_Impresion>>> GetControlCalidad_Impresion()
        {
          if (_context.ControlCalidad_Impresion == null)
          {
              return NotFound();
          }
            return await _context.ControlCalidad_Impresion.ToListAsync();
        }

        // GET: api/ControlCalidad_Impresion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlCalidad_Impresion>> GetControlCalidad_Impresion(int id)
        {
          if (_context.ControlCalidad_Impresion == null)
          {
              return NotFound();
          }
            var controlCalidad_Impresion = await _context.ControlCalidad_Impresion.FindAsync(id);

            if (controlCalidad_Impresion == null)
            {
                return NotFound();
            }

            return controlCalidad_Impresion;
        }

        // PUT: api/ControlCalidad_Impresion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlCalidad_Impresion(int id, ControlCalidad_Impresion controlCalidad_Impresion)
        {
            if (id != controlCalidad_Impresion.Id)
            {
                return BadRequest();
            }

            _context.Entry(controlCalidad_Impresion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlCalidad_ImpresionExists(id))
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

        // POST: api/ControlCalidad_Impresion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ControlCalidad_Impresion>> PostControlCalidad_Impresion(ControlCalidad_Impresion controlCalidad_Impresion)
        {
          if (_context.ControlCalidad_Impresion == null)
          {
              return Problem("Entity set 'dataContext.ControlCalidad_Impresion'  is null.");
          }
            _context.ControlCalidad_Impresion.Add(controlCalidad_Impresion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlCalidad_Impresion", new { id = controlCalidad_Impresion.Id }, controlCalidad_Impresion);
        }

        // DELETE: api/ControlCalidad_Impresion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControlCalidad_Impresion(int id)
        {
            if (_context.ControlCalidad_Impresion == null)
            {
                return NotFound();
            }
            var controlCalidad_Impresion = await _context.ControlCalidad_Impresion.FindAsync(id);
            if (controlCalidad_Impresion == null)
            {
                return NotFound();
            }

            _context.ControlCalidad_Impresion.Remove(controlCalidad_Impresion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ControlCalidad_ImpresionExists(int id)
        {
            return (_context.ControlCalidad_Impresion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
