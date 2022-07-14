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
    public class Categoria_InsumoController : ControllerBase
    {
        private readonly dataContext _context;

        public Categoria_InsumoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Categoria_Insumo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria_Insumo>>> GetCategorias_Insumos()
        {
          if (_context.Categorias_Insumos == null)
          {
              return NotFound();
          }
            return await _context.Categorias_Insumos.ToListAsync();
        }

        // GET: api/Categoria_Insumo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria_Insumo>> GetCategoria_Insumo(int id)
        {
          if (_context.Categorias_Insumos == null)
          {
              return NotFound();
          }
            var categoria_Insumo = await _context.Categorias_Insumos.FindAsync(id);

            if (categoria_Insumo == null)
            {
                return NotFound();
            }

            return categoria_Insumo;
        }

        // PUT: api/Categoria_Insumo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria_Insumo(int id, Categoria_Insumo categoria_Insumo)
        {
            if (id != categoria_Insumo.CatInsu_Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria_Insumo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Categoria_InsumoExists(id))
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

        // POST: api/Categoria_Insumo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCategoria_Insumo(Categoria_Insumo categoria_Insumo, IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
            {
                return Problem("Debe seleccionar una imagen");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Release", Image.FileName);

            Console.WriteLine(path);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
                stream.Close();
            }

            categoria_Insumo.CatInsu_UrlImagen = Image.FileName;

            if (_context.Categorias_Insumos == null)
            {
                return Problem("Entity set 'dataContext.Categorias_Insumos'  is null.");
            }
            _context.Categorias_Insumos.Add(categoria_Insumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria_Insumo", new { id = categoria_Insumo.CatInsu_Id }, categoria_Insumo);
        }

        // DELETE: api/Categoria_Insumo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria_Insumo(int id)
        {
            if (_context.Categorias_Insumos == null)
            {
                return NotFound();
            }
            var categoria_Insumo = await _context.Categorias_Insumos.FindAsync(id);
            if (categoria_Insumo == null)
            {
                return NotFound();
            }

            _context.Categorias_Insumos.Remove(categoria_Insumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Categoria_InsumoExists(int id)
        {
            return (_context.Categorias_Insumos?.Any(e => e.CatInsu_Id == id)).GetValueOrDefault();
        }
    }
}
