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
    public class ControlCalidad_SelladoController : ControllerBase
    {
        private readonly dataContext _context;

        public ControlCalidad_SelladoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/ControlCalidad_Sellado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlCalidad_Sellado>>> GetControlCalidad_Sellado()
        {
          if (_context.ControlCalidad_Sellado == null)
          {
              return NotFound();
          }
            return await _context.ControlCalidad_Sellado.ToListAsync();
        }

        // GET: api/ControlCalidad_Sellado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlCalidad_Sellado>> GetControlCalidad_Sellado(long id)
        {
          if (_context.ControlCalidad_Sellado == null)
          {
              return NotFound();
          }
            var controlCalidad_Sellado = await _context.ControlCalidad_Sellado.FindAsync(id);

            if (controlCalidad_Sellado == null)
            {
                return NotFound();
            }

            return controlCalidad_Sellado;
        }

        // PUT: api/ControlCalidad_Sellado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlCalidad_Sellado(long id, ControlCalidad_Sellado controlCalidad_Sellado)
        {
            if (id != controlCalidad_Sellado.CcSel_Id)
            {
                return BadRequest();
            }

            _context.Entry(controlCalidad_Sellado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlCalidad_SelladoExists(id))
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

        // POST: api/ControlCalidad_Sellado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ControlCalidad_Sellado>> PostControlCalidad_Sellado(ControlCalidad_Sellado controlCalidad_Sellado)
        {
          if (_context.ControlCalidad_Sellado == null)
          {
              return Problem("Entity set 'dataContext.ControlCalidad_Sellado'  is null.");
          }
            _context.ControlCalidad_Sellado.Add(controlCalidad_Sellado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlCalidad_Sellado", new { id = controlCalidad_Sellado.CcSel_Id }, controlCalidad_Sellado);
        }

        // DELETE: api/ControlCalidad_Sellado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControlCalidad_Sellado(long id)
        {
            if (_context.ControlCalidad_Sellado == null)
            {
                return NotFound();
            }
            var controlCalidad_Sellado = await _context.ControlCalidad_Sellado.FindAsync(id);
            if (controlCalidad_Sellado == null)
            {
                return NotFound();
            }

            _context.ControlCalidad_Sellado.Remove(controlCalidad_Sellado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ControlCalidad_SelladoExists(long id)
        {
            return (_context.ControlCalidad_Sellado?.Any(e => e.CcSel_Id == id)).GetValueOrDefault();
        }
    }
}
