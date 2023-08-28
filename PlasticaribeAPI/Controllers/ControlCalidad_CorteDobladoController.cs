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
    public class ControlCalidad_CorteDobladoController : ControllerBase
    {
        private readonly dataContext _context;

        public ControlCalidad_CorteDobladoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/ControlCalidad_CorteDoblado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlCalidad_CorteDoblado>>> GetControlCalidad_CorteDoblado()
        {
          if (_context.ControlCalidad_CorteDoblado == null)
          {
              return NotFound();
          }
            return await _context.ControlCalidad_CorteDoblado.ToListAsync();
        }

        // GET: api/ControlCalidad_CorteDoblado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlCalidad_CorteDoblado>> GetControlCalidad_CorteDoblado(int id)
        {
          if (_context.ControlCalidad_CorteDoblado == null)
          {
              return NotFound();
          }
            var controlCalidad_CorteDoblado = await _context.ControlCalidad_CorteDoblado.FindAsync(id);

            if (controlCalidad_CorteDoblado == null)
            {
                return NotFound();
            }

            return controlCalidad_CorteDoblado;
        }

        // Consulta que devolverá los registros que se han realizado del día actual
        [HttpGet("getRegistrosHoy")]
        public ActionResult GetRegistrosHoy()
        {
            var con = from cc in _context.Set<ControlCalidad_CorteDoblado>()
                      where cc.Fecha_Registro == DateTime.Today
                      select cc;
            return con.Any() ? Ok(con) : Problem("No se encontraron registros del día de hoy");
        }

        // Consulta que devolverá el ultimo registro que se realizó a un producto
        [HttpGet("getUltRegistroItem/{item}")]
        public ActionResult GetUltRegistroItem(long item)
        {
            var con = (from cc in _context.Set<ControlCalidad_CorteDoblado>()
                       where cc.Prod_Id == item
                       orderby cc.Id descending
                       select cc).FirstOrDefault();
            return con != null ? Ok(con) : Problem("No hay información del item consultado");
        }

        // PUT: api/ControlCalidad_CorteDoblado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlCalidad_CorteDoblado(int id, ControlCalidad_CorteDoblado controlCalidad_CorteDoblado)
        {
            if (id != controlCalidad_CorteDoblado.Id)
            {
                return BadRequest();
            }

            _context.Entry(controlCalidad_CorteDoblado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlCalidad_CorteDobladoExists(id))
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

        // POST: api/ControlCalidad_CorteDoblado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ControlCalidad_CorteDoblado>> PostControlCalidad_CorteDoblado(ControlCalidad_CorteDoblado controlCalidad_CorteDoblado)
        {
          if (_context.ControlCalidad_CorteDoblado == null)
          {
              return Problem("Entity set 'dataContext.ControlCalidad_CorteDoblado'  is null.");
          }
            _context.ControlCalidad_CorteDoblado.Add(controlCalidad_CorteDoblado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlCalidad_CorteDoblado", new { id = controlCalidad_CorteDoblado.Id }, controlCalidad_CorteDoblado);
        }

        // DELETE: api/ControlCalidad_CorteDoblado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControlCalidad_CorteDoblado(int id)
        {
            if (_context.ControlCalidad_CorteDoblado == null)
            {
                return NotFound();
            }
            var controlCalidad_CorteDoblado = await _context.ControlCalidad_CorteDoblado.FindAsync(id);
            if (controlCalidad_CorteDoblado == null)
            {
                return NotFound();
            }

            _context.ControlCalidad_CorteDoblado.Remove(controlCalidad_CorteDoblado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ControlCalidad_CorteDobladoExists(int id)
        {
            return (_context.ControlCalidad_CorteDoblado?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
