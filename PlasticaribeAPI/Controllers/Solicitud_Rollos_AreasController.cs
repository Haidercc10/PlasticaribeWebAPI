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
    public class Solicitud_Rollos_AreasController : ControllerBase
    {
        private readonly dataContext _context;

        public Solicitud_Rollos_AreasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Solicitud_Rollos_Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud_Rollos_Areas>>> GetSolicitud_Rollos_Areas()
        {
            if (_context.Solicitud_Rollos_Areas == null)
            {
                return NotFound();
            }
            return await _context.Solicitud_Rollos_Areas.ToListAsync();
        }

        // GET: api/Solicitud_Rollos_Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud_Rollos_Areas>> GetSolicitud_Rollos_Areas(long id)
        {
            if (_context.Solicitud_Rollos_Areas == null)
            {
                return NotFound();
            }
            var solicitud_Rollos_Areas = await _context.Solicitud_Rollos_Areas.FindAsync(id);

            if (solicitud_Rollos_Areas == null)
            {
                return NotFound();
            }

            return solicitud_Rollos_Areas;
        }

        // PUT: api/Solicitud_Rollos_Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitud_Rollos_Areas(long id, Solicitud_Rollos_Areas solicitud_Rollos_Areas)
        {
            if (id != solicitud_Rollos_Areas.SolRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitud_Rollos_Areas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Solicitud_Rollos_AreasExists(id))
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

        // POST: api/Solicitud_Rollos_Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitud_Rollos_Areas>> PostSolicitud_Rollos_Areas(Solicitud_Rollos_Areas solicitud_Rollos_Areas)
        {
            if (_context.Solicitud_Rollos_Areas == null)
            {
                return Problem("Entity set 'dataContext.Solicitud_Rollos_Areas'  is null.");
            }
            _context.Solicitud_Rollos_Areas.Add(solicitud_Rollos_Areas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitud_Rollos_Areas", new { id = solicitud_Rollos_Areas.SolRollo_Id }, solicitud_Rollos_Areas);
        }

        // DELETE: api/Solicitud_Rollos_Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud_Rollos_Areas(long id)
        {
            if (_context.Solicitud_Rollos_Areas == null)
            {
                return NotFound();
            }
            var solicitud_Rollos_Areas = await _context.Solicitud_Rollos_Areas.FindAsync(id);
            if (solicitud_Rollos_Areas == null)
            {
                return NotFound();
            }

            _context.Solicitud_Rollos_Areas.Remove(solicitud_Rollos_Areas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Solicitud_Rollos_AreasExists(long id)
        {
            return (_context.Solicitud_Rollos_Areas?.Any(e => e.SolRollo_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
