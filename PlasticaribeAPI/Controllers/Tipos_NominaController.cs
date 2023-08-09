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
    public class Tipos_NominaController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipos_NominaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipos_Nomina
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipos_Nomina>>> GetTipos_Nomina()
        {
          if (_context.Tipos_Nomina == null)
          {
              return NotFound();
          }
            return await _context.Tipos_Nomina.ToListAsync();
        }

        // GET: api/Tipos_Nomina/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipos_Nomina>> GetTipos_Nomina(int id)
        {
          if (_context.Tipos_Nomina == null)
          {
              return NotFound();
          }
            var tipos_Nomina = await _context.Tipos_Nomina.FindAsync(id);

            if (tipos_Nomina == null)
            {
                return NotFound();
            }

            return tipos_Nomina;
        }

        // PUT: api/Tipos_Nomina/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipos_Nomina(int id, Tipos_Nomina tipos_Nomina)
        {
            if (id != tipos_Nomina.TpNomina_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipos_Nomina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipos_NominaExists(id))
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

        // POST: api/Tipos_Nomina
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipos_Nomina>> PostTipos_Nomina(Tipos_Nomina tipos_Nomina)
        {
          if (_context.Tipos_Nomina == null)
          {
              return Problem("Entity set 'dataContext.Tipos_Nomina'  is null.");
          }
            _context.Tipos_Nomina.Add(tipos_Nomina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipos_Nomina", new { id = tipos_Nomina.TpNomina_Id }, tipos_Nomina);
        }

        // DELETE: api/Tipos_Nomina/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipos_Nomina(int id)
        {
            if (_context.Tipos_Nomina == null)
            {
                return NotFound();
            }
            var tipos_Nomina = await _context.Tipos_Nomina.FindAsync(id);
            if (tipos_Nomina == null)
            {
                return NotFound();
            }

            _context.Tipos_Nomina.Remove(tipos_Nomina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipos_NominaExists(int id)
        {
            return (_context.Tipos_Nomina?.Any(e => e.TpNomina_Id == id)).GetValueOrDefault();
        }
    }
}
