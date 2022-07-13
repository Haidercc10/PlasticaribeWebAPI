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
    public class DetalleDevolucion_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleDevolucion_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleDevolucion_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleDevolucion_MateriaPrima>>> GetDetallesDevoluciones_MateriasPrimas()
        {
          if (_context.DetallesDevoluciones_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.DetallesDevoluciones_MateriasPrimas.ToListAsync();
        }

        // GET: api/DetalleDevolucion_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleDevolucion_MateriaPrima>> GetDetalleDevolucion_MateriaPrima(long DevMatPri_Id, long MatPri_Id)
        {
          if (_context.DetallesDevoluciones_MateriasPrimas == null)
          {
              return NotFound();
          }
            var detalleDevolucion_MateriaPrima = await _context.DetallesDevoluciones_MateriasPrimas.FindAsync(DevMatPri_Id, MatPri_Id);

            if (detalleDevolucion_MateriaPrima == null)
            {
                return NotFound();
            }

            return detalleDevolucion_MateriaPrima;
        }

        // PUT: api/DetalleDevolucion_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleDevolucion_MateriaPrima(long id, DetalleDevolucion_MateriaPrima detalleDevolucion_MateriaPrima)
        {
            if (id != detalleDevolucion_MateriaPrima.DevMatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleDevolucion_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleDevolucion_MateriaPrimaExists(id))
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

        // POST: api/DetalleDevolucion_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleDevolucion_MateriaPrima>> PostDetalleDevolucion_MateriaPrima(DetalleDevolucion_MateriaPrima detalleDevolucion_MateriaPrima)
        {
          if (_context.DetallesDevoluciones_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.DetallesDevoluciones_MateriasPrimas'  is null.");
          }
            _context.DetallesDevoluciones_MateriasPrimas.Add(detalleDevolucion_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleDevolucion_MateriaPrimaExists(detalleDevolucion_MateriaPrima.DevMatPri_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleDevolucion_MateriaPrima", new { id = detalleDevolucion_MateriaPrima.DevMatPri_Id }, detalleDevolucion_MateriaPrima);
        }

        // DELETE: api/DetalleDevolucion_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleDevolucion_MateriaPrima(long id)
        {
            if (_context.DetallesDevoluciones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleDevolucion_MateriaPrima = await _context.DetallesDevoluciones_MateriasPrimas.FindAsync(id);
            if (detalleDevolucion_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.DetallesDevoluciones_MateriasPrimas.Remove(detalleDevolucion_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleDevolucion_MateriaPrimaExists(long id)
        {
            return (_context.DetallesDevoluciones_MateriasPrimas?.Any(e => e.DevMatPri_Id == id)).GetValueOrDefault();
        }
    }
}
