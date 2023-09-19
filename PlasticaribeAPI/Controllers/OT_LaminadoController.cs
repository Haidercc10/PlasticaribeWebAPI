using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class OT_LaminadoController : ControllerBase
    {
        private readonly dataContext _context;

        public OT_LaminadoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/OT_Laminado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OT_Laminado>>> GetOT_Laminado()
        {
            if (_context.OT_Laminado == null)
            {
                return NotFound();
            }
            return await _context.OT_Laminado.ToListAsync();
        }

        // GET: api/OT_Laminado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OT_Laminado>> GetOT_Laminado(int id)
        {
            if (_context.OT_Laminado == null)
            {
                return NotFound();
            }
            var oT_Laminado = await _context.OT_Laminado.FindAsync(id);

            if (oT_Laminado == null)
            {
                return NotFound();
            }

            return oT_Laminado;
        }

        // Funcion que consultará los datos en el proceso de laminado de una orden de trabajo
        [HttpGet("getOT_Laminado/{ot}")]
        public ActionResult getOt_Laminado(long ot)
        {
            var con = from lam in _context.Set<OT_Laminado>()
                      where lam.OT_Id == ot
                      select lam;
            return Ok(con);
        }

        // PUT: api/OT_Laminado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOT_Laminado(int id, OT_Laminado oT_Laminado)
        {
            if (id != oT_Laminado.LamCapa_Id)
            {
                return BadRequest();
            }

            _context.Entry(oT_Laminado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OT_LaminadoExists(id))
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

        // POST: api/OT_Laminado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OT_Laminado>> PostOT_Laminado(OT_Laminado oT_Laminado)
        {
            if (_context.OT_Laminado == null)
            {
                return Problem("Entity set 'dataContext.OT_Laminado'  is null.");
            }
            _context.OT_Laminado.Add(oT_Laminado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOT_Laminado", new { id = oT_Laminado.LamCapa_Id }, oT_Laminado);
        }

        // DELETE: api/OT_Laminado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOT_Laminado(int id)
        {
            if (_context.OT_Laminado == null)
            {
                return NotFound();
            }
            var oT_Laminado = await _context.OT_Laminado.FindAsync(id);
            if (oT_Laminado == null)
            {
                return NotFound();
            }

            _context.OT_Laminado.Remove(oT_Laminado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OT_LaminadoExists(int id)
        {
            return (_context.OT_Laminado?.Any(e => e.LamCapa_Id == id)).GetValueOrDefault();
        }
    }
}
