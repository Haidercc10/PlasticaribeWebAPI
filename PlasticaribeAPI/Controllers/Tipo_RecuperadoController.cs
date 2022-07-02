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
    public class Tipo_RecuperadoController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_RecuperadoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Recuperado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Recuperado>>> GetTipos_Recuperados()
        {
          if (_context.Tipos_Recuperados == null)
          {
              return NotFound();
          }
            return await _context.Tipos_Recuperados.ToListAsync();
        }

        // GET: api/Tipo_Recuperado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Recuperado>> GetTipo_Recuperado(string id)
        {
          if (_context.Tipos_Recuperados == null)
          {
              return NotFound();
          }
            var tipo_Recuperado = await _context.Tipos_Recuperados.FindAsync(id);

            if (tipo_Recuperado == null)
            {
                return NotFound();
            }

            return tipo_Recuperado;
        }

        // PUT: api/Tipo_Recuperado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Recuperado(string id, Tipo_Recuperado tipo_Recuperado)
        {
            if (id != tipo_Recuperado.TpRecu_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Recuperado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_RecuperadoExists(id))
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

        // POST: api/Tipo_Recuperado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_Recuperado>> PostTipo_Recuperado(Tipo_Recuperado tipo_Recuperado)
        {
          if (_context.Tipos_Recuperados == null)
          {
              return Problem("Entity set 'dataContext.Tipos_Recuperados'  is null.");
          }
            _context.Tipos_Recuperados.Add(tipo_Recuperado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Tipo_RecuperadoExists(tipo_Recuperado.TpRecu_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTipo_Recuperado", new { id = tipo_Recuperado.TpRecu_Id }, tipo_Recuperado);
        }

        // DELETE: api/Tipo_Recuperado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Recuperado(string id)
        {
            if (_context.Tipos_Recuperados == null)
            {
                return NotFound();
            }
            var tipo_Recuperado = await _context.Tipos_Recuperados.FindAsync(id);
            if (tipo_Recuperado == null)
            {
                return NotFound();
            }

            _context.Tipos_Recuperados.Remove(tipo_Recuperado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_RecuperadoExists(string id)
        {
            return (_context.Tipos_Recuperados?.Any(e => e.TpRecu_Id == id)).GetValueOrDefault();
        }
    }
}
