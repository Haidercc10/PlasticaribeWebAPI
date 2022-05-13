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
    public class Existencia_ProductosController : ControllerBase
    {
        private readonly dataContext _context;

        public Existencia_ProductosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Existencia_Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Existencia_Productos>>> GetExistencias_Productos()
        {
          if (_context.Existencias_Productos == null)
          {
              return NotFound();
          }
            return await _context.Existencias_Productos.ToListAsync();
        }

        // GET: api/Existencia_Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Existencia_Productos>> GetExistencia_Productos(long id)
        {
          if (_context.Existencias_Productos == null)
          {
              return NotFound();
          }
            var existencia_Productos = await _context.Existencias_Productos.FindAsync(id);

            if (existencia_Productos == null)
            {
                return NotFound();
            }

            return existencia_Productos;
        }

        // PUT: api/Existencia_Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExistencia_Productos(long id, Existencia_Productos existencia_Productos)
        {
            if (id != existencia_Productos.ExProd_Id)
            {
                return BadRequest();
            }

            _context.Entry(existencia_Productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(id))
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

        // POST: api/Existencia_Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Existencia_Productos>> PostExistencia_Productos(Existencia_Productos existencia_Productos)
        {
          if (_context.Existencias_Productos == null)
          {
              return Problem("Entity set 'dataContext.Existencias_Productos'  is null.");
          }
            _context.Existencias_Productos.Add(existencia_Productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExistencia_Productos", new { id = existencia_Productos.ExProd_Id }, existencia_Productos);
        }

        // DELETE: api/Existencia_Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExistencia_Productos(long id)
        {
            if (_context.Existencias_Productos == null)
            {
                return NotFound();
            }
            var existencia_Productos = await _context.Existencias_Productos.FindAsync(id);
            if (existencia_Productos == null)
            {
                return NotFound();
            }

            _context.Existencias_Productos.Remove(existencia_Productos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Existencia_ProductosExists(long id)
        {
            return (_context.Existencias_Productos?.Any(e => e.ExProd_Id == id)).GetValueOrDefault();
        }
    }
}
