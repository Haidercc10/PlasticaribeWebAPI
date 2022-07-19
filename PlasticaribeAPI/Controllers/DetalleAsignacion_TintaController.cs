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
    public class DetalleAsignacion_TintaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_TintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_Tinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_Tinta>>> GetDetalleAsignaciones_Tintas()
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return NotFound();
          }
            return await _context.DetalleAsignaciones_Tintas.ToListAsync();
        }

        // GET: api/DetalleAsignacion_Tinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_Tinta>> GetDetalleAsignacion_Tinta(long id)
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return NotFound();
          }
            var detalleAsignacion_Tinta = await _context.DetalleAsignaciones_Tintas.FindAsync(id);

            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            }

            return detalleAsignacion_Tinta;
        }

        // GET: api/DetalleAsignacion_Tinta/5
        [HttpGet("asignacion/{AsigMp_Id}")]
        public ActionResult<DetalleAsignacion_Tinta> GetDetalleAsignacion(long AsigMp_Id)
        {
            var detalleAsignacion_Tinta = _context.DetalleAsignaciones_Tintas.Where(dtAsg => dtAsg.AsigMp_Id == AsigMp_Id).ToList();

            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(detalleAsignacion_Tinta);
            }

        }

        // PUT: api/DetalleAsignacion_Tinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_Tinta(long id, DetalleAsignacion_Tinta detalleAsignacion_Tinta)
        {
            if (id != detalleAsignacion_Tinta.AsigMp_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_Tinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_TintaExists(id))
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

        // POST: api/DetalleAsignacion_Tinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_Tinta>> PostDetalleAsignacion_Tinta(DetalleAsignacion_Tinta detalleAsignacion_Tinta)
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return Problem("Entity set 'dataContext.DetalleAsignaciones_Tintas'  is null.");
          }
            _context.DetalleAsignaciones_Tintas.Add(detalleAsignacion_Tinta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_TintaExists(detalleAsignacion_Tinta.AsigMp_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_Tinta", new { id = detalleAsignacion_Tinta.AsigMp_Id }, detalleAsignacion_Tinta);
        }

        // DELETE: api/DetalleAsignacion_Tinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_Tinta(long id)
        {
            if (_context.DetalleAsignaciones_Tintas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_Tinta = await _context.DetalleAsignaciones_Tintas.FindAsync(id);
            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            }

            _context.DetalleAsignaciones_Tintas.Remove(detalleAsignacion_Tinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_TintaExists(long id)
        {
            return (_context.DetalleAsignaciones_Tintas?.Any(e => e.AsigMp_Id == id)).GetValueOrDefault();
        }
    }
}
