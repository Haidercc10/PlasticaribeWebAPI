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
    public class Productos_MateriasPrimasController : ControllerBase
    {
        private readonly dataContext _context;

        public Productos_MateriasPrimasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Productos_MateriasPrimas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos_MateriasPrimas>>> GetProductos_MateriasPrimas()
        {
          if (_context.Productos_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.Productos_MateriasPrimas.ToListAsync();
        }

        // GET: api/Productos_MateriasPrimas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos_MateriasPrimas>> GetProductos_MateriasPrimas(int id)
        {
          if (_context.Productos_MateriasPrimas == null)
          {
              return NotFound();
          }
            var productos_MateriasPrimas = await _context.Productos_MateriasPrimas.FindAsync(id);

            if (productos_MateriasPrimas == null)
            {
                return NotFound();
            }

            return productos_MateriasPrimas;
        }

        // PUT: api/Productos_MateriasPrimas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductos_MateriasPrimas(int id, Productos_MateriasPrimas productos_MateriasPrimas)
        {
            if (id != productos_MateriasPrimas.Id)
            {
                return BadRequest();
            }

            _context.Entry(productos_MateriasPrimas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Productos_MateriasPrimasExists(id))
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

        // POST: api/Productos_MateriasPrimas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Productos_MateriasPrimas>> PostProductos_MateriasPrimas(Productos_MateriasPrimas productos_MateriasPrimas)
        {
          if (_context.Productos_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.Productos_MateriasPrimas'  is null.");
          }
            _context.Productos_MateriasPrimas.Add(productos_MateriasPrimas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductos_MateriasPrimas", new { id = productos_MateriasPrimas.Id }, productos_MateriasPrimas);
        }

        // DELETE: api/Productos_MateriasPrimas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductos_MateriasPrimas(int id)
        {
            if (_context.Productos_MateriasPrimas == null)
            {
                return NotFound();
            }
            var productos_MateriasPrimas = await _context.Productos_MateriasPrimas.FindAsync(id);
            if (productos_MateriasPrimas == null)
            {
                return NotFound();
            }

            _context.Productos_MateriasPrimas.Remove(productos_MateriasPrimas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Productos_MateriasPrimasExists(int id)
        {
            return (_context.Productos_MateriasPrimas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
