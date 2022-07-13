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
    public class DetalleRecuperado_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleRecuperado_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleRecuperado_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleRecuperado_MateriaPrima>>> GetDetallesRecuperados_MateriasPrimas()
        {
          if (_context.DetallesRecuperados_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.DetallesRecuperados_MateriasPrimas.ToListAsync();
        }

        // GET: api/DetalleRecuperado_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleRecuperado_MateriaPrima>> GetDetalleRecuperado_MateriaPrima(long RecMp_Id, long MatPri_Id)
        {
          if (_context.DetallesRecuperados_MateriasPrimas == null)
          {
              return NotFound();
          }
            var detalleRecuperado_MateriaPrima = await _context.DetallesRecuperados_MateriasPrimas.FindAsync(RecMp_Id, MatPri_Id);

            if (detalleRecuperado_MateriaPrima == null)
            {
                return NotFound();
            }

            return detalleRecuperado_MateriaPrima;
        }

        [HttpGet("materiaPrima/{MatPri_Id}")]
        public ActionResult<DetalleRecuperado_MateriaPrima> GetIdMateriaPrima(long MatPri_Id)
        {
            var detalleRecuperado_MateriaPrima = _context.DetallesRecuperados_MateriasPrimas.Where(dtR => dtR.MatPri_Id == MatPri_Id).ToList();

            if (detalleRecuperado_MateriaPrima == null)
            {
                return  BadRequest();
            }
            else
            {
                return Ok(detalleRecuperado_MateriaPrima);
            }

        }

        [HttpGet("recuperado/{RecMp_Id}")]
        public ActionResult<DetalleRecuperado_MateriaPrima> GetRecuperado(long RecMp_Id)
        {
            var detalleRecuperado_MateriaPrima = _context.DetallesRecuperados_MateriasPrimas.Where(dtR => dtR.RecMp_Id == RecMp_Id).ToList();

            if (detalleRecuperado_MateriaPrima == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(detalleRecuperado_MateriaPrima);
            }

        }

        // PUT: api/DetalleRecuperado_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleRecuperado_MateriaPrima(long id, DetalleRecuperado_MateriaPrima detalleRecuperado_MateriaPrima)
        {
            if (id != detalleRecuperado_MateriaPrima.RecMp_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleRecuperado_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleRecuperado_MateriaPrimaExists(id))
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

        // POST: api/DetalleRecuperado_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleRecuperado_MateriaPrima>> PostDetalleRecuperado_MateriaPrima(DetalleRecuperado_MateriaPrima detalleRecuperado_MateriaPrima)
        {
          if (_context.DetallesRecuperados_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.DetallesRecuperados_MateriasPrimas'  is null.");
          }
            _context.DetallesRecuperados_MateriasPrimas.Add(detalleRecuperado_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleRecuperado_MateriaPrimaExists(detalleRecuperado_MateriaPrima.RecMp_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleRecuperado_MateriaPrima", new { id = detalleRecuperado_MateriaPrima.RecMp_Id }, detalleRecuperado_MateriaPrima);
        }

        // DELETE: api/DetalleRecuperado_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleRecuperado_MateriaPrima(long id)
        {
            if (_context.DetallesRecuperados_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleRecuperado_MateriaPrima = await _context.DetallesRecuperados_MateriasPrimas.FindAsync(id);
            if (detalleRecuperado_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.DetallesRecuperados_MateriasPrimas.Remove(detalleRecuperado_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleRecuperado_MateriaPrimaExists(long id)
        {
            return (_context.DetallesRecuperados_MateriasPrimas?.Any(e => e.RecMp_Id == id)).GetValueOrDefault();
        }
    }
}
