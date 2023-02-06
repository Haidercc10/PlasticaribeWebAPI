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
    public class Tipo_DocumentoController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_DocumentoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Documento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Documento>>> GetTipos_Documentos()
        {
          if (_context.Tipos_Documentos == null)
          {
              return NotFound();
          }
            return await _context.Tipos_Documentos.ToListAsync();
        }

        // GET: api/Tipo_Documento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Documento>> GetTipo_Documento(string id)
        {
          if (_context.Tipos_Documentos == null)
          {
              return NotFound();
          }
            var tipo_Documento = await _context.Tipos_Documentos.FindAsync(id);

            if (tipo_Documento == null)
            {
                return NotFound();
            }

            return tipo_Documento;
        }

        // PUT: api/Tipo_Documento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Documento(string id, Tipo_Documento tipo_Documento)
        {
            if (id != tipo_Documento.TpDoc_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Documento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_DocumentoExists(id))
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

        // POST: api/Tipo_Documento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_Documento>> PostTipo_Documento(Tipo_Documento tipo_Documento)
        {
          if (_context.Tipos_Documentos == null)
          {
              return Problem("Entity set 'dataContext.Tipos_Documentos'  is null.");
          }
            _context.Tipos_Documentos.Add(tipo_Documento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Tipo_DocumentoExists(tipo_Documento.TpDoc_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTipo_Documento", new { id = tipo_Documento.TpDoc_Id }, tipo_Documento);
        }

        // DELETE: api/Tipo_Documento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Documento(string id)
        {
            if (_context.Tipos_Documentos == null)
            {
                return NotFound();
            }
            var tipo_Documento = await _context.Tipos_Documentos.FindAsync(id);
            if (tipo_Documento == null)
            {
                return NotFound();
            }

            _context.Tipos_Documentos.Remove(tipo_Documento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_DocumentoExists(string id)
        {
            return (_context.Tipos_Documentos?.Any(e => e.TpDoc_Id == id)).GetValueOrDefault();
        }
    }
}
