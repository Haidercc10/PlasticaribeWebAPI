using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ubicaciones_BodegaRollosController : ControllerBase
    {
        private readonly dataContext _context;

        public Ubicaciones_BodegaRollosController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ubicaciones_BodegaRollos>>> GetUbicaciones_BodegaRollos()
        {
            return await _context.Ubicaciones_BodegaRollos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Ubicaciones_BodegaRollos>> GetUbicaciones_BodegaRollos(long id)
        {
            var ubr = await _context.Ubicaciones_BodegaRollos.FindAsync(id);

            if (ubr == null)
            {
                return NotFound();
            }
            return ubr;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUbicaciones_BodegaRollos(long id, Ubicaciones_BodegaRollos ubicaciones_BodegaRollos)
        {
            if (id != ubicaciones_BodegaRollos.UBR_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(ubicaciones_BodegaRollos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Ubicaciones_BodegaRollosExists(id))
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

        //
        [HttpPost]
        public async Task<ActionResult<Ubicaciones_BodegaRollos>> PostUbicaciones_BodegaRollos(Ubicaciones_BodegaRollos ubicaciones_BodegaRollos)
        {
            _context.Ubicaciones_BodegaRollos.Add(ubicaciones_BodegaRollos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUbicaciones_BodegaRollos", new { id = ubicaciones_BodegaRollos.UBR_Codigo }, ubicaciones_BodegaRollos);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUbicaciones_BodegaRollos(long id)
        {
            var ubr = await _context.Ubicaciones_BodegaRollos.FindAsync(id);
            if (ubr == null)
            {
                return NotFound();
            }

            _context.Ubicaciones_BodegaRollos.Remove(ubr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Ubicaciones_BodegaRollosExists(long id)
        {
            return _context.Ubicaciones_BodegaRollos.Any(e => e.UBR_Codigo == id);
        }

    }
}
