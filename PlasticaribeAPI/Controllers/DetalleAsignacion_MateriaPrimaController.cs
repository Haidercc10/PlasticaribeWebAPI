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
    public class DetalleAsignacion_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_MateriaPrima>>> GetDetallesAsignaciones_MateriasPrimas()
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.DetallesAsignaciones_MateriasPrimas.ToListAsync();
        }

        // GET: api/DetalleAsignacion_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_MateriaPrima>> GetDetalleAsignacion_MateriaPrima(long AsigMp_Id, long MatPri_Id)
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return NotFound();
          }
            var detalleAsignacion_MateriaPrima = await _context.DetallesAsignaciones_MateriasPrimas.FindAsync(AsigMp_Id, MatPri_Id);

            if (detalleAsignacion_MateriaPrima == null)
            {
                return NotFound();
            }

            return detalleAsignacion_MateriaPrima;
        }

        // PUT: api/DetalleAsignacion_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_MateriaPrima(long id, DetalleAsignacion_MateriaPrima detalleAsignacion_MateriaPrima)
        {
            if (id != detalleAsignacion_MateriaPrima.AsigMp_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_MateriaPrimaExists(id))
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

        // POST: api/DetalleAsignacion_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_MateriaPrima>> PostDetalleAsignacion_MateriaPrima(DetalleAsignacion_MateriaPrima detalleAsignacion_MateriaPrima)
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.DetallesAsignaciones_MateriasPrimas'  is null.");
          }
            _context.DetallesAsignaciones_MateriasPrimas.Add(detalleAsignacion_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_MateriaPrimaExists(detalleAsignacion_MateriaPrima.AsigMp_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_MateriaPrima", new { id = detalleAsignacion_MateriaPrima.AsigMp_Id }, detalleAsignacion_MateriaPrima);
        }

        // DELETE: api/DetalleAsignacion_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_MateriaPrima(long id)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_MateriaPrima = await _context.DetallesAsignaciones_MateriasPrimas.FindAsync(id);
            if (detalleAsignacion_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_MateriasPrimas.Remove(detalleAsignacion_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_MateriaPrimaExists(long id)
        {
            return (_context.DetallesAsignaciones_MateriasPrimas?.Any(e => e.AsigMp_Id == id)).GetValueOrDefault();
        }
    }
}
