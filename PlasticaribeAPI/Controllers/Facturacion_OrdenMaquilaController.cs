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
    public class Facturacion_OrdenMaquilaController : ControllerBase
    {
        private readonly dataContext _context;

        public Facturacion_OrdenMaquilaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Facturacion_OrdenMaquila
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facturacion_OrdenMaquila>>> GetFacturacion_OrdenMaquila()
        {
          if (_context.Facturacion_OrdenMaquila == null)
          {
              return NotFound();
          }
            return await _context.Facturacion_OrdenMaquila.ToListAsync();
        }

        // GET: api/Facturacion_OrdenMaquila/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facturacion_OrdenMaquila>> GetFacturacion_OrdenMaquila(long id)
        {
          if (_context.Facturacion_OrdenMaquila == null)
          {
              return NotFound();
          }
            var facturacion_OrdenMaquila = await _context.Facturacion_OrdenMaquila.FindAsync(id);

            if (facturacion_OrdenMaquila == null)
            {
                return NotFound();
            }

            return facturacion_OrdenMaquila;
        }

        // PUT: api/Facturacion_OrdenMaquila/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturacion_OrdenMaquila(long id, Facturacion_OrdenMaquila facturacion_OrdenMaquila)
        {
            if (id != facturacion_OrdenMaquila.FacOM_Id)
            {
                return BadRequest();
            }

            _context.Entry(facturacion_OrdenMaquila).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Facturacion_OrdenMaquilaExists(id))
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

        // POST: api/Facturacion_OrdenMaquila
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facturacion_OrdenMaquila>> PostFacturacion_OrdenMaquila(Facturacion_OrdenMaquila facturacion_OrdenMaquila)
        {
          if (_context.Facturacion_OrdenMaquila == null)
          {
              return Problem("Entity set 'dataContext.Facturacion_OrdenMaquila'  is null.");
          }
            _context.Facturacion_OrdenMaquila.Add(facturacion_OrdenMaquila);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacturacion_OrdenMaquila", new { id = facturacion_OrdenMaquila.FacOM_Id }, facturacion_OrdenMaquila);
        }

        // DELETE: api/Facturacion_OrdenMaquila/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturacion_OrdenMaquila(long id)
        {
            if (_context.Facturacion_OrdenMaquila == null)
            {
                return NotFound();
            }
            var facturacion_OrdenMaquila = await _context.Facturacion_OrdenMaquila.FindAsync(id);
            if (facturacion_OrdenMaquila == null)
            {
                return NotFound();
            }

            _context.Facturacion_OrdenMaquila.Remove(facturacion_OrdenMaquila);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Facturacion_OrdenMaquilaExists(long id)
        {
            return (_context.Facturacion_OrdenMaquila?.Any(e => e.FacOM_Id == id)).GetValueOrDefault();
        }
    }
}
