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
    public class InventarioInicialXDia_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public InventarioInicialXDia_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/InventarioInicialXDia_MatPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioInicialXDia_MatPrima>>> GetInventarioInicialXDias_MatPrima()
        {
          if (_context.InventarioInicialXDias_MatPrima == null)
          {
              return NotFound();
          }
            return await _context.InventarioInicialXDias_MatPrima.ToListAsync();
        }

        // GET: api/InventarioInicialXDia_MatPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioInicialXDia_MatPrima>> GetInventarioInicialXDia_MatPrima(long id)
        {
          if (_context.InventarioInicialXDias_MatPrima == null)
          {
              return NotFound();
          }
            var inventarioInicialXDia_MatPrima = await _context.InventarioInicialXDias_MatPrima.FindAsync(id);

            if (inventarioInicialXDia_MatPrima == null)
            {
                return NotFound();
            }

            return inventarioInicialXDia_MatPrima;
        }

        // PUT: api/InventarioInicialXDia_MatPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventarioInicialXDia_MatPrima(long id, InventarioInicialXDia_MatPrima inventarioInicialXDia_MatPrima)
        {
            if (id != inventarioInicialXDia_MatPrima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(inventarioInicialXDia_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioInicialXDia_MatPrimaExists(id))
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

        // POST: api/InventarioInicialXDia_MatPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventarioInicialXDia_MatPrima>> PostInventarioInicialXDia_MatPrima(InventarioInicialXDia_MatPrima inventarioInicialXDia_MatPrima)
        {
          if (_context.InventarioInicialXDias_MatPrima == null)
          {
              return Problem("Entity set 'dataContext.InventarioInicialXDias_MatPrima'  is null.");
          }
            _context.InventarioInicialXDias_MatPrima.Add(inventarioInicialXDia_MatPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InventarioInicialXDia_MatPrimaExists(inventarioInicialXDia_MatPrima.MatPri_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInventarioInicialXDia_MatPrima", new { id = inventarioInicialXDia_MatPrima.MatPri_Id }, inventarioInicialXDia_MatPrima);
        }

        // DELETE: api/InventarioInicialXDia_MatPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventarioInicialXDia_MatPrima(long id)
        {
            if (_context.InventarioInicialXDias_MatPrima == null)
            {
                return NotFound();
            }
            var inventarioInicialXDia_MatPrima = await _context.InventarioInicialXDias_MatPrima.FindAsync(id);
            if (inventarioInicialXDia_MatPrima == null)
            {
                return NotFound();
            }

            _context.InventarioInicialXDias_MatPrima.Remove(inventarioInicialXDia_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioInicialXDia_MatPrimaExists(long id)
        {
            return (_context.InventarioInicialXDias_MatPrima?.Any(e => e.MatPri_Id == id)).GetValueOrDefault();
        }
    }
}
