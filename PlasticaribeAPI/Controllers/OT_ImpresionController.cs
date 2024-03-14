using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or members
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class OT_ImpresionController : ControllerBase
    {
        private readonly dataContext _context;

        public OT_ImpresionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/OT_Impresion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OT_Impresion>>> GetOT_Impresion()
        {
            if (_context.OT_Impresion == null)
            {
                return NotFound();
            }
            return await _context.OT_Impresion.ToListAsync();
        }

        // GET: api/OT_Impresion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OT_Impresion>> GetOT_Impresion(int id)
        {
            if (_context.OT_Impresion == null)
            {
                return NotFound();
            }
            var oT_Impresion = await _context.OT_Impresion.FindAsync(id);

            if (oT_Impresion == null)
            {
                return NotFound();
            }

            return oT_Impresion;
        }

        // Funcion que consultará los datos en el proceso de impresion de una orden de trabajo
        [HttpGet("getOT_Impresion/{ot}")]
        public ActionResult getOt_Impresion(long ot)
        {
            var con = from imp in _context.Set<OT_Impresion>()
                      where imp.Ot_Id == ot
                      select imp;
            return Ok(con);
        }

        // PUT: api/OT_Impresion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOT_Impresion(int id, OT_Impresion oT_Impresion)
        {
            if (id != oT_Impresion.Impresion_Id)
            {
                return BadRequest();
            }

            _context.Entry(oT_Impresion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OT_ImpresionExists(id))
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

        // POST: api/OT_Impresion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OT_Impresion>> PostOT_Impresion(OT_Impresion oT_Impresion)
        {
            if (_context.OT_Impresion == null)
            {
                return Problem("Entity set 'dataContext.OT_Impresion'  is null.");
            }
            _context.OT_Impresion.Add(oT_Impresion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOT_Impresion", new { id = oT_Impresion.Impresion_Id }, oT_Impresion);
        }

        // DELETE: api/OT_Impresion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOT_Impresion(int id)
        {
            if (_context.OT_Impresion == null)
            {
                return NotFound();
            }
            var oT_Impresion = await _context.OT_Impresion.FindAsync(id);
            if (oT_Impresion == null)
            {
                return NotFound();
            }

            _context.OT_Impresion.Remove(oT_Impresion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OT_ImpresionExists(int id)
        {
            return (_context.OT_Impresion?.Any(e => e.Impresion_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or members
}
