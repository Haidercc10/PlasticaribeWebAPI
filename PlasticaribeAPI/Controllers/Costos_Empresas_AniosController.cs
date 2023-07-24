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
    public class Costos_Empresas_AniosController : ControllerBase
    {
        private readonly dataContext _context;

        public Costos_Empresas_AniosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Costos_Empresas_Anios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Costos_Empresas_Anios>>> GetCostos_Empresas_Anios()
        {
          if (_context.Costos_Empresas_Anios == null)
          {
              return NotFound();
          }
            return await _context.Costos_Empresas_Anios.ToListAsync();
        }

        // GET: api/Costos_Empresas_Anios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Costos_Empresas_Anios>> GetCostos_Empresas_Anios(int id)
        {
          if (_context.Costos_Empresas_Anios == null)
          {
              return NotFound();
          }
            var costos_Empresas_Anios = await _context.Costos_Empresas_Anios.FindAsync(id);

            if (costos_Empresas_Anios == null)
            {
                return NotFound();
            }

            return costos_Empresas_Anios;
        }

        // PUT: api/Costos_Empresas_Anios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCostos_Empresas_Anios(int id, Costos_Empresas_Anios costos_Empresas_Anios)
        {
            if (id != costos_Empresas_Anios.CostosEmp_Id)
            {
                return BadRequest();
            }

            _context.Entry(costos_Empresas_Anios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Costos_Empresas_AniosExists(id))
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

        // POST: api/Costos_Empresas_Anios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Costos_Empresas_Anios>> PostCostos_Empresas_Anios(Costos_Empresas_Anios costos_Empresas_Anios)
        {
          if (_context.Costos_Empresas_Anios == null)
          {
              return Problem("Entity set 'dataContext.Costos_Empresas_Anios'  is null.");
          }
            _context.Costos_Empresas_Anios.Add(costos_Empresas_Anios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCostos_Empresas_Anios", new { id = costos_Empresas_Anios.CostosEmp_Id }, costos_Empresas_Anios);
        }

        // DELETE: api/Costos_Empresas_Anios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCostos_Empresas_Anios(int id)
        {
            if (_context.Costos_Empresas_Anios == null)
            {
                return NotFound();
            }
            var costos_Empresas_Anios = await _context.Costos_Empresas_Anios.FindAsync(id);
            if (costos_Empresas_Anios == null)
            {
                return NotFound();
            }

            _context.Costos_Empresas_Anios.Remove(costos_Empresas_Anios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Costos_Empresas_AniosExists(int id)
        {
            return (_context.Costos_Empresas_Anios?.Any(e => e.CostosEmp_Id == id)).GetValueOrDefault();
        }
    }
}
