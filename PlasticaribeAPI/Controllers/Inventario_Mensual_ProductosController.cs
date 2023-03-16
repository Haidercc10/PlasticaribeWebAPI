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
    public class Inventario_Mensual_ProductosController : ControllerBase
    {
        private readonly dataContext _context;

        public Inventario_Mensual_ProductosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Inventario_Mensual_Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario_Mensual_Productos>>> GetInventario_Mensual_Productos()
        {
          if (_context.Inventario_Mensual_Productos == null)
          {
              return NotFound();
          }
            return await _context.Inventario_Mensual_Productos.ToListAsync();
        }

        // GET: api/Inventario_Mensual_Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario_Mensual_Productos>> GetInventario_Mensual_Productos(long id)
        {
          if (_context.Inventario_Mensual_Productos == null)
          {
              return NotFound();
          }
            var inventario_Mensual_Productos = await _context.Inventario_Mensual_Productos.FindAsync(id);

            if (inventario_Mensual_Productos == null)
            {
                return NotFound();
            }

            return inventario_Mensual_Productos;
        }

        // PUT: api/Inventario_Mensual_Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario_Mensual_Productos(long id, Inventario_Mensual_Productos inventario_Mensual_Productos)
        {
            if (id != inventario_Mensual_Productos.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(inventario_Mensual_Productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Inventario_Mensual_ProductosExists(id))
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

        // POST: api/Inventario_Mensual_Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventario_Mensual_Productos>> PostInventario_Mensual_Productos(Inventario_Mensual_Productos inventario_Mensual_Productos)
        {
          if (_context.Inventario_Mensual_Productos == null)
          {
              return Problem("Entity set 'dataContext.Inventario_Mensual_Productos'  is null.");
          }
            _context.Inventario_Mensual_Productos.Add(inventario_Mensual_Productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario_Mensual_Productos", new { id = inventario_Mensual_Productos.Codigo }, inventario_Mensual_Productos);
        }

        // DELETE: api/Inventario_Mensual_Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario_Mensual_Productos(long id)
        {
            if (_context.Inventario_Mensual_Productos == null)
            {
                return NotFound();
            }
            var inventario_Mensual_Productos = await _context.Inventario_Mensual_Productos.FindAsync(id);
            if (inventario_Mensual_Productos == null)
            {
                return NotFound();
            }

            _context.Inventario_Mensual_Productos.Remove(inventario_Mensual_Productos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Inventario_Mensual_ProductosExists(long id)
        {
            return (_context.Inventario_Mensual_Productos?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
