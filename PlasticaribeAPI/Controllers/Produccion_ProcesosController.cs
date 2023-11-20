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
    public class Produccion_ProcesosController : ControllerBase
    {
        private readonly dataContext _context;

        public Produccion_ProcesosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Produccion_Procesos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produccion_Procesos>>> GetProduccion_Procesos()
        {
            return await _context.Produccion_Procesos.ToListAsync();
        }

        // GET: api/Produccion_Procesos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produccion_Procesos>> GetProduccion_Procesos(int id)
        {
            var produccion_Procesos = await _context.Produccion_Procesos.FindAsync(id);

            if (produccion_Procesos == null)
            {
                return NotFound();
            }

            return produccion_Procesos;
        }

        // PUT: api/Produccion_Procesos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduccion_Procesos(int id, Produccion_Procesos produccion_Procesos)
        {
            if (id != produccion_Procesos.Id)
            {
                return BadRequest();
            }

            _context.Entry(produccion_Procesos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Produccion_ProcesosExists(id))
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

        // POST: api/Produccion_Procesos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produccion_Procesos>> PostProduccion_Procesos(Produccion_Procesos produccion_Procesos)
        {
            var numeroUltimoRollo = (from prod in _context.Set<Produccion_Procesos>()
                                     orderby prod.Numero_Rollo descending
                                     select prod.Numero_Rollo).FirstOrDefault();

            produccion_Procesos.Numero_Rollo = numeroUltimoRollo + 1;
            _context.Produccion_Procesos.Add(produccion_Procesos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduccion_Procesos", new { id = produccion_Procesos.Id }, produccion_Procesos);
        }

        // DELETE: api/Produccion_Procesos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccion_Procesos(int id)
        {
            var produccion_Procesos = await _context.Produccion_Procesos.FindAsync(id);
            if (produccion_Procesos == null)
            {
                return NotFound();
            }

            _context.Produccion_Procesos.Remove(produccion_Procesos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Produccion_ProcesosExists(int id)
        {
            return _context.Produccion_Procesos.Any(e => e.Id == id);
        }
    }
}
