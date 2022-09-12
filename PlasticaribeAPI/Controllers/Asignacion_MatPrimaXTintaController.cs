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
    public class Asignacion_MatPrimaXTintaController : ControllerBase
    {
        private readonly dataContext _context;

        public Asignacion_MatPrimaXTintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Asignacion_MatPrimaXTinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion_MatPrimaXTinta>>> GetAsignaciones_MatPrimasXTintas()
        {
          if (_context.Asignaciones_MatPrimasXTintas == null)
          {
              return NotFound();
          }
            return await _context.Asignaciones_MatPrimasXTintas.ToListAsync();
        }

        // GET: api/Asignacion_MatPrimaXTinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion_MatPrimaXTinta>> GetAsignacion_MatPrimaXTinta(long id)
        {
          if (_context.Asignaciones_MatPrimasXTintas == null)
          {
              return NotFound();
          }
            var asignacion_MatPrimaXTinta = await _context.Asignaciones_MatPrimasXTintas.FindAsync(id);

            if (asignacion_MatPrimaXTinta == null)
            {
                return NotFound();
            }

            return asignacion_MatPrimaXTinta;
        }


        [HttpGet("ultimoId/")]
        public ActionResult<Asignacion_MatPrimaXTinta> GetUltimoId()
        {
            var asignacion = _context.Asignaciones_MatPrimasXTintas.OrderBy(asg => asg.AsigMPxTinta_Id).Last();

            return Ok(asignacion);
        }

        // PUT: api/Asignacion_MatPrimaXTinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion_MatPrimaXTinta(long id, Asignacion_MatPrimaXTinta asignacion_MatPrimaXTinta)
        {
            if (id != asignacion_MatPrimaXTinta.AsigMPxTinta_Id)
            {
                return BadRequest();
            }

            _context.Entry(asignacion_MatPrimaXTinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asignacion_MatPrimaXTintaExists(id))
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

        // POST: api/Asignacion_MatPrimaXTinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignacion_MatPrimaXTinta>> PostAsignacion_MatPrimaXTinta(Asignacion_MatPrimaXTinta asignacion_MatPrimaXTinta)
        {
          if (_context.Asignaciones_MatPrimasXTintas == null)
          {
              return Problem("Entity set 'dataContext.Asignaciones_MatPrimasXTintas'  is null.");
          }
            _context.Asignaciones_MatPrimasXTintas.Add(asignacion_MatPrimaXTinta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion_MatPrimaXTinta", new { id = asignacion_MatPrimaXTinta.AsigMPxTinta_Id }, asignacion_MatPrimaXTinta);
        }

        // DELETE: api/Asignacion_MatPrimaXTinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion_MatPrimaXTinta(long id)
        {
            if (_context.Asignaciones_MatPrimasXTintas == null)
            {
                return NotFound();
            }
            var asignacion_MatPrimaXTinta = await _context.Asignaciones_MatPrimasXTintas.FindAsync(id);
            if (asignacion_MatPrimaXTinta == null)
            {
                return NotFound();
            }

            _context.Asignaciones_MatPrimasXTintas.Remove(asignacion_MatPrimaXTinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Asignacion_MatPrimaXTintaExists(long id)
        {
            return (_context.Asignaciones_MatPrimasXTintas?.Any(e => e.AsigMPxTinta_Id == id)).GetValueOrDefault();
        }
    }
}
