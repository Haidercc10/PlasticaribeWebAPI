using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ControlCalidad_ImpresionController : ControllerBase
    {
        private readonly dataContext _context;

        public ControlCalidad_ImpresionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/ControlCalidad_Impresion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlCalidad_Impresion>>> GetControlCalidad_Impresion()
        {
            if (_context.ControlCalidad_Impresion == null)
            {
                return NotFound();
            }
            return await _context.ControlCalidad_Impresion.ToListAsync();
        }

        // GET: api/ControlCalidad_Impresion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlCalidad_Impresion>> GetControlCalidad_Impresion(int id)
        {
            if (_context.ControlCalidad_Impresion == null)
            {
                return NotFound();
            }
            var controlCalidad_Impresion = await _context.ControlCalidad_Impresion.FindAsync(id);

            if (controlCalidad_Impresion == null)
            {
                return NotFound();
            }

            return controlCalidad_Impresion;
        }

        // Consulta que devolverá los registros que se han realizado del día actual
        [HttpGet("getRegistros/{inicio}/{fin}")]
        public ActionResult GetRegistros(DateTime inicio, DateTime fin)
        {
            var con = from cc in _context.Set<ControlCalidad_Impresion>()
                      where cc.Fecha_Registro >= inicio &&
                            cc.Fecha_Registro <= fin
                      select cc;
            return con.Any() ? Ok(con) : Problem("No se encontraron registros del día de hoy");
        }

        // Consulta que devolverá el ultimo registro que se realizó a un producto
        [HttpGet("getUltRegistroItem/{item}")]
        public ActionResult GetUltRegistroItem(long item)
        {
            var con = (from cc in _context.Set<ControlCalidad_Impresion>()
                       where cc.Prod_Id == item
                       orderby cc.Id descending
                       select cc).FirstOrDefault();
            return con != null ? Ok(con) : Problem("No hay información del item consultado");
        }

        // PUT: api/ControlCalidad_Impresion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlCalidad_Impresion(int id, ControlCalidad_Impresion controlCalidad_Impresion)
        {
            if (id != controlCalidad_Impresion.Id)
            {
                return BadRequest();
            }

            _context.Entry(controlCalidad_Impresion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlCalidad_ImpresionExists(id))
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

        // POST: api/ControlCalidad_Impresion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ControlCalidad_Impresion>> PostControlCalidad_Impresion(ControlCalidad_Impresion controlCalidad_Impresion)
        {
            if (_context.ControlCalidad_Impresion == null)
            {
                return Problem("Entity set 'dataContext.ControlCalidad_Impresion'  is null.");
            }
            _context.ControlCalidad_Impresion.Add(controlCalidad_Impresion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlCalidad_Impresion", new { id = controlCalidad_Impresion.Id }, controlCalidad_Impresion);
        }

        // DELETE: api/ControlCalidad_Impresion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControlCalidad_Impresion(int id)
        {
            if (_context.ControlCalidad_Impresion == null)
            {
                return NotFound();
            }
            var controlCalidad_Impresion = await _context.ControlCalidad_Impresion.FindAsync(id);
            if (controlCalidad_Impresion == null)
            {
                return NotFound();
            }

            _context.ControlCalidad_Impresion.Remove(controlCalidad_Impresion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ControlCalidad_ImpresionExists(int id)
        {
            return (_context.ControlCalidad_Impresion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
