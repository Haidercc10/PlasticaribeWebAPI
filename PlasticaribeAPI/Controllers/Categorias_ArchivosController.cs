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
    public class Categorias_ArchivosController : ControllerBase
    {
        private readonly dataContext _context;

        public Categorias_ArchivosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Categorias_Archivos
        [HttpGet]
        public ActionResult get()
        {
            try
            {
                return Ok(_context.Categorias_Archivos.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Categorias_Archivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias_Archivos>> GetCategorias_Archivos(int id)
        {
          if (_context.Categorias_Archivos == null)
          {
              return NotFound();
          }
            var categorias_Archivos = await _context.Categorias_Archivos.FindAsync(id);

            if (categorias_Archivos == null)
            {
                return NotFound();
            }

            return categorias_Archivos;
        }

        // PUT: api/Categorias_Archivos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorias_Archivos(int id, Categorias_Archivos categorias_Archivos)
        {
            if (id != categorias_Archivos.Categoria_Id)
            {
                return BadRequest();
            }

            _context.Entry(categorias_Archivos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Categorias_ArchivosExists(id))
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

        // POST: api/Categorias_Archivos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categorias_Archivos>> PostCategorias_Archivos(Categorias_Archivos categorias_Archivos)
        {
          if (_context.Categorias_Archivos == null)
          {
              return Problem("Entity set 'dataContext.Categorias_Archivos'  is null.");
          }
            _context.Categorias_Archivos.Add(categorias_Archivos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategorias_Archivos", new { id = categorias_Archivos.Categoria_Id }, categorias_Archivos);
        }

        // DELETE: api/Categorias_Archivos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorias_Archivos(int id)
        {
            if (_context.Categorias_Archivos == null)
            {
                return NotFound();
            }
            var categorias_Archivos = await _context.Categorias_Archivos.FindAsync(id);
            if (categorias_Archivos == null)
            {
                return NotFound();
            }

            _context.Categorias_Archivos.Remove(categorias_Archivos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Categorias_ArchivosExists(int id)
        {
            return (_context.Categorias_Archivos?.Any(e => e.Categoria_Id == id)).GetValueOrDefault();
        }
    }
}
