using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Categoria_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Categoria_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Categoria_MatPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria_MatPrima>>> GetCategorias_MatPrima()
        {
          if (_context.Categorias_MatPrima == null)
          {
              return NotFound();
          }
            return await _context.Categorias_MatPrima.ToListAsync();
        }

        // GET: api/Categoria_MatPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria_MatPrima>> GetCategoria_MatPrima(int id)
        {
          if (_context.Categorias_MatPrima == null)
          {
              return NotFound();
          }
            var categoria_MatPrima = await _context.Categorias_MatPrima.FindAsync(id);

            if (categoria_MatPrima == null)
            {
                return NotFound();
            }

            return categoria_MatPrima;
        }

        // PUT: api/Categoria_MatPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria_MatPrima(int id, Categoria_MatPrima categoria_MatPrima)
        {
            if (id != categoria_MatPrima.CatMP_Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Categoria_MatPrimaExists(id))
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

        // POST: api/Categoria_MatPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categoria_MatPrima>> PostCategoria_MatPrima(Categoria_MatPrima categoria_MatPrima)
        {
          if (_context.Categorias_MatPrima == null)
          {
              return Problem("Entity set 'dataContext.Categorias_MatPrima'  is null.");
          }
            _context.Categorias_MatPrima.Add(categoria_MatPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria_MatPrima", new { id = categoria_MatPrima.CatMP_Id }, categoria_MatPrima);
        }

        // DELETE: api/Categoria_MatPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria_MatPrima(int id)
        {
            if (_context.Categorias_MatPrima == null)
            {
                return NotFound();
            }
            var categoria_MatPrima = await _context.Categorias_MatPrima.FindAsync(id);
            if (categoria_MatPrima == null)
            {
                return NotFound();
            }

            _context.Categorias_MatPrima.Remove(categoria_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Categoria_MatPrimaExists(int id)
        {
            return (_context.Categorias_MatPrima?.Any(e => e.CatMP_Id == id)).GetValueOrDefault();
        }
    }
}
