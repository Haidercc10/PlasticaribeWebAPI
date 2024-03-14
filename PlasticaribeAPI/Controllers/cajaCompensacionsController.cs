#nullable disable
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
    public class cajaCompensacionsController : ControllerBase
    {
        private readonly dataContext _context;

        public cajaCompensacionsController(dataContext context)
        {
            _context = context;
        }

        // GET: api/cajaCompensacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<cajaCompensacion>>> GetCajas_Compensaciones()
        {
            return await _context.Cajas_Compensaciones.ToListAsync();
        }

        // GET: api/cajaCompensacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<cajaCompensacion>> GetcajaCompensacion(long id)
        {
            var cajaCompensacion = await _context.Cajas_Compensaciones.FindAsync(id);

            if (cajaCompensacion == null)
            {
                return NotFound();
            }

            return cajaCompensacion;
        }

        // PUT: api/cajaCompensacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutcajaCompensacion(long id, cajaCompensacion cajaCompensacion)
        {
            if (id != cajaCompensacion.cajComp_Id)
            {
                return BadRequest();
            }

            _context.Entry(cajaCompensacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cajaCompensacionExists(id))
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

        // POST: api/cajaCompensacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<cajaCompensacion>> PostcajaCompensacion(cajaCompensacion cajaCompensacion)
        {
            _context.Cajas_Compensaciones.Add(cajaCompensacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (cajaCompensacionExists(cajaCompensacion.cajComp_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetcajaCompensacion", new { id = cajaCompensacion.cajComp_Id }, cajaCompensacion);
        }

        // DELETE: api/cajaCompensacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletecajaCompensacion(long id)
        {
            var cajaCompensacion = await _context.Cajas_Compensaciones.FindAsync(id);
            if (cajaCompensacion == null)
            {
                return NotFound();
            }

            _context.Cajas_Compensaciones.Remove(cajaCompensacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool cajaCompensacionExists(long id)
        {
            return _context.Cajas_Compensaciones.Any(e => e.cajComp_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
