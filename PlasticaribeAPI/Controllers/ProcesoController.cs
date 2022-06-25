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
    public class ProcesoController : ControllerBase
    {
        private readonly dataContext _context;

        public ProcesoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Proceso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proceso>>> GetProcesos()
        {
          if (_context.Procesos == null)
          {
              return NotFound();
          }
            return await _context.Procesos.ToListAsync();
        }

        // GET: api/Proceso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proceso>> GetProceso(string id)
        {
          if (_context.Procesos == null)
          {
              return NotFound();
          }
            var proceso = await _context.Procesos.FindAsync(id);

            if (proceso == null)
            {
                return NotFound();
            }

            return proceso;
        }

        // PUT: api/Proceso/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProceso(string id, Proceso proceso)
        {
            if (id != proceso.Proceso_Id)
            {
                return BadRequest();
            }

            _context.Entry(proceso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcesoExists(id))
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

        // POST: api/Proceso
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proceso>> PostProceso(Proceso proceso)
        {
          if (_context.Procesos == null)
          {
              return Problem("Entity set 'dataContext.Procesos'  is null.");
          }
            _context.Procesos.Add(proceso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProcesoExists(proceso.Proceso_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProceso", new { id = proceso.Proceso_Id }, proceso);
        }

        // DELETE: api/Proceso/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProceso(string id)
        {
            if (_context.Procesos == null)
            {
                return NotFound();
            }
            var proceso = await _context.Procesos.FindAsync(id);
            if (proceso == null)
            {
                return NotFound();
            }

            _context.Procesos.Remove(proceso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcesoExists(string id)
        {
            return (_context.Procesos?.Any(e => e.Proceso_Id == id)).GetValueOrDefault();
        }
    }
}
