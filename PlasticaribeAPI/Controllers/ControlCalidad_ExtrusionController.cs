using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlCalidad_ExtrusionController : ControllerBase
    {
        private readonly dataContext _context;

        public ControlCalidad_ExtrusionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/ControlCalidad_Extrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ControlCalidad_Extrusion>>> GetControlCalidad_Extrusion()
        {
            if (_context.ControlCalidad_Extrusion == null)
            {
                return NotFound();
            }
            return await _context.ControlCalidad_Extrusion.ToListAsync();
        }

        // GET: api/ControlCalidad_Extrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlCalidad_Extrusion>> GetControlCalidad_Extrusion(long id)
        {
            if (_context.ControlCalidad_Extrusion == null)
            {
                return NotFound();
            }
            var controlCalidad_Extrusion = await _context.ControlCalidad_Extrusion.FindAsync(id);

            if (controlCalidad_Extrusion == null)
            {
                return NotFound();
            }

            return controlCalidad_Extrusion;
        }

        // GET Por Fechas
        [HttpGet("getControlCalidad_ExtrusionHoy/{fecha1}/{fecha2}")]
        public ActionResult GetControlCalidad_ExtrusionHoy(DateTime fecha1, DateTime fecha2)
        {
            var controlExtrusion = from cce in _context.Set<ControlCalidad_Extrusion>()
                                   where cce.CcExt_Fecha >= fecha1 &&
                                   cce.CcExt_Fecha <= fecha2
                                   select cce;

            if (controlExtrusion == null) return NotFound();
            else return Ok(controlExtrusion);
        }

        // GET ronda por OT
        [HttpGet("getRonda/{ot}")]
        public ActionResult GetRonda(long ot)
        {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            var controlExtrusion = (from cce in _context.Set<ControlCalidad_Extrusion>()
                                    where cce.CcExt_OT == ot &&
                                    cce.CcExt_Fecha == DateTime.Today
                                    orderby cce.CcExt_Id descending
                                    select cce.CcExt_Ronda).FirstOrDefault();

            return controlExtrusion == null ? Ok(0) : Ok(controlExtrusion);
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
        }

        // PUT: api/ControlCalidad_Extrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlCalidad_Extrusion(long id, ControlCalidad_Extrusion controlCalidad_Extrusion)
        {
            if (id != controlCalidad_Extrusion.CcExt_Id)
            {
                return BadRequest();
            }

            _context.Entry(controlCalidad_Extrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlCalidad_ExtrusionExists(id))
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

        // POST: api/ControlCalidad_Extrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ControlCalidad_Extrusion>> PostControlCalidad_Extrusion(ControlCalidad_Extrusion controlCalidad_Extrusion)
        {
            if (_context.ControlCalidad_Extrusion == null)
            {
                return Problem("Entity set 'dataContext.ControlCalidad_Extrusion'  is null.");
            }
            _context.ControlCalidad_Extrusion.Add(controlCalidad_Extrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlCalidad_Extrusion", new { id = controlCalidad_Extrusion.CcExt_Id }, controlCalidad_Extrusion);
        }

        // DELETE: api/ControlCalidad_Extrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControlCalidad_Extrusion(long id)
        {
            if (_context.ControlCalidad_Extrusion == null)
            {
                return NotFound();
            }
            var controlCalidad_Extrusion = await _context.ControlCalidad_Extrusion.FindAsync(id);
            if (controlCalidad_Extrusion == null)
            {
                return NotFound();
            }

            _context.ControlCalidad_Extrusion.Remove(controlCalidad_Extrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ControlCalidad_ExtrusionExists(long id)
        {
            return (_context.ControlCalidad_Extrusion?.Any(e => e.CcExt_Id == id)).GetValueOrDefault();
        }
    }
}
