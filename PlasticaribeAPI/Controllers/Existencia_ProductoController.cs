#nullable disable
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
    public class Existencia_ProductoController : ControllerBase
    {
        private readonly dataContext _context;

        public Existencia_ProductoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Existencia_Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Existencia_Producto>>> GetExistencia_Producto()
        {
            return await _context.Existencia_Producto.ToListAsync();
        }

        // GET: api/Existencia_Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Existencia_Producto>> GetExistencia_Producto(long id)
        {
            var existencia_Producto = await _context.Existencia_Producto.FindAsync(id);

            if (existencia_Producto == null)
            {
                return NotFound();
            }

            return existencia_Producto;
        }

        // PUT: api/Existencia_Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExistencia_Producto(long id, Existencia_Producto existencia_Producto)
        {
            if (id != existencia_Producto.ExProd_Id)
            {
                return BadRequest();
            }

            _context.Entry(existencia_Producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductoExists(id))
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

        // POST: api/Existencia_Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Existencia_Producto>> PostExistencia_Producto(Existencia_Producto existencia_Producto)
        {
            _context.Existencia_Producto.Add(existencia_Producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExistencia_Producto", new { id = existencia_Producto.ExProd_Id }, existencia_Producto);
        }

        // DELETE: api/Existencia_Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExistencia_Producto(long id)
        {
            var existencia_Producto = await _context.Existencia_Producto.FindAsync(id);
            if (existencia_Producto == null)
            {
                return NotFound();
            }

            _context.Existencia_Producto.Remove(existencia_Producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Existencia_ProductoExists(long id)
        {
            return _context.Existencia_Producto.Any(e => e.ExProd_Id == id);
        }
    }
}
