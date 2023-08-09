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
    public class Formato_DocumentosController : ControllerBase
    {
        private readonly dataContext _context;

        public Formato_DocumentosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Formato_Documentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formato_Documentos>>> GetFormato_Documentos()
        {
          if (_context.Formato_Documentos == null)
          {
              return NotFound();
          }
            return await _context.Formato_Documentos.ToListAsync();
        }

        // GET: api/Formato_Documentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Formato_Documentos>> GetFormato_Documentos(int id)
        {
          if (_context.Formato_Documentos == null)
          {
              return NotFound();
          }
            var formato_Documentos = await _context.Formato_Documentos.FindAsync(id);

            if (formato_Documentos == null)
            {
                return NotFound();
            }

            return formato_Documentos;
        }

        //Consulta que devolverá la informacion del ultimo formato de un documento
        [HttpGet("getUltFormatoDoc/{nombre}")]
        public ActionResult GetUltFormadoDoc (string nombre)
        {
            var con = (from fmt in _context.Set<Formato_Documentos>()
                      where fmt.Nombre_Reporte == nombre
                      orderby fmt.Id descending
                      select fmt).FirstOrDefault();
            if (con != null) return Ok(con);
            else return NoContent();
        }

        // PUT: api/Formato_Documentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormato_Documentos(int id, Formato_Documentos formato_Documentos)
        {
            if (id != formato_Documentos.Id)
            {
                return BadRequest();
            }

            _context.Entry(formato_Documentos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Formato_DocumentosExists(id))
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

        // POST: api/Formato_Documentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Formato_Documentos>> PostFormato_Documentos(Formato_Documentos formato_Documentos)
        {
          if (_context.Formato_Documentos == null)
          {
              return Problem("Entity set 'dataContext.Formato_Documentos'  is null.");
          }
            _context.Formato_Documentos.Add(formato_Documentos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormato_Documentos", new { id = formato_Documentos.Id }, formato_Documentos);
        }

        // DELETE: api/Formato_Documentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormato_Documentos(int id)
        {
            if (_context.Formato_Documentos == null)
            {
                return NotFound();
            }
            var formato_Documentos = await _context.Formato_Documentos.FindAsync(id);
            if (formato_Documentos == null)
            {
                return NotFound();
            }

            _context.Formato_Documentos.Remove(formato_Documentos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Formato_DocumentosExists(int id)
        {
            return (_context.Formato_Documentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
