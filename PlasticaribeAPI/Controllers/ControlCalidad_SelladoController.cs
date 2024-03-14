using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
            if (_context.ControlCalidad_Sellado == null) return NotFound();
            return await _context.ControlCalidad_Sellado.ToListAsync();
        }

        // GET: api/ControlCalidad_Sellado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlCalidad_Sellado>> GetControlCalidad_Sellado(long id)
        {
            if (_context.ControlCalidad_Sellado == null) return NotFound();
            var controlCalidad_Sellado = await _context.ControlCalidad_Sellado.FindAsync(id);

            if (controlCalidad_Sellado == null) return NotFound();

            return controlCalidad_Sellado;
        }

        // GET Por fecha actual
        [HttpGet("getControlCalidad_SelladoHoy/{fecha1}/{fecha2}")]
        public ActionResult GetControlCalidad_SelladoHoy(DateTime fecha1, DateTime fecha2)
        {
            var sellado = from cce in _context.Set<ControlCalidad_Sellado>()
                          where cce.CcSel_Fecha >= fecha1 &&
                          cce.CcSel_Fecha <= fecha2
                          select cce;

            if (sellado == null) return NotFound();
            else return Ok(sellado);
        }

        // GET ronda por OT
        [HttpGet("getRonda/{ot}")]
        public ActionResult GetRonda(long ot)
        {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            var rondaMayor = (from cce in _context.Set<ControlCalidad_Sellado>()
                              where cce.CcSel_OT == ot &&
                              cce.CcSel_Fecha == DateTime.Today
                              orderby cce.CcSel_Id descending
                              select cce.CcSel_Ronda).FirstOrDefault();

            return rondaMayor == null ? Ok(0) : Ok(rondaMayor);
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
