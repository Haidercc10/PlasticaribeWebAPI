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
    public class Detalles_SolicitudMateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_SolicitudMateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_SolicitudMateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_SolicitudMateriaPrima>>> GetDetalles_SolicitudMateriaPrima()
        {
          if (_context.Detalles_SolicitudMateriaPrima == null)
          {
              return NotFound();
          }
            return await _context.Detalles_SolicitudMateriaPrima.ToListAsync();
        }

        // GET: api/Detalles_SolicitudMateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_SolicitudMateriaPrima>> GetDetalles_SolicitudMateriaPrima(long id)
        {
          if (_context.Detalles_SolicitudMateriaPrima == null)
          {
              return NotFound();
          }
            var detalles_SolicitudMateriaPrima = await _context.Detalles_SolicitudMateriaPrima.FindAsync(id);

            if (detalles_SolicitudMateriaPrima == null)
            {
                return NotFound();
            }

            return detalles_SolicitudMateriaPrima;
        }

        // PUT: api/Detalles_SolicitudMateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_SolicitudMateriaPrima(long id, Detalles_SolicitudMateriaPrima detalles_SolicitudMateriaPrima)
        {
            if (id != detalles_SolicitudMateriaPrima.DtSolicitud_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalles_SolicitudMateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_SolicitudMateriaPrimaExists(id))
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

        // POST: api/Detalles_SolicitudMateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_SolicitudMateriaPrima>> PostDetalles_SolicitudMateriaPrima(Detalles_SolicitudMateriaPrima detalles_SolicitudMateriaPrima)
        {
          if (_context.Detalles_SolicitudMateriaPrima == null)
          {
              return Problem("Entity set 'dataContext.Detalles_SolicitudMateriaPrima'  is null.");
          }
            _context.Detalles_SolicitudMateriaPrima.Add(detalles_SolicitudMateriaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_SolicitudMateriaPrima", new { id = detalles_SolicitudMateriaPrima.DtSolicitud_Id }, detalles_SolicitudMateriaPrima);
        }

        // DELETE: api/Detalles_SolicitudMateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_SolicitudMateriaPrima(long id)
        {
            if (_context.Detalles_SolicitudMateriaPrima == null)
            {
                return NotFound();
            }
            var detalles_SolicitudMateriaPrima = await _context.Detalles_SolicitudMateriaPrima.FindAsync(id);
            if (detalles_SolicitudMateriaPrima == null)
            {
                return NotFound();
            }

            _context.Detalles_SolicitudMateriaPrima.Remove(detalles_SolicitudMateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_SolicitudMateriaPrimaExists(long id)
        {
            return (_context.Detalles_SolicitudMateriaPrima?.Any(e => e.DtSolicitud_Id == id)).GetValueOrDefault();
        }
    }
}
