using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class NominaDetallada_PlasticaribeController : ControllerBase
    {
        private readonly dataContext _context;

        public NominaDetallada_PlasticaribeController(dataContext context)
        {
            _context = context;
        }

        // GET: api/NominaDetallada_Plasticaribe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NominaDetallada_Plasticaribe>>> GetNominaDetallada_Plasticaribe()
        {
            return await _context.NominaDetallada_Plasticaribe.ToListAsync();
        }

        // GET: api/NominaDetallada_Plasticaribe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NominaDetallada_Plasticaribe>> GetNominaDetallada_Plasticaribe(int id)
        {
            var nominaDetallada_Plasticaribe = await _context.NominaDetallada_Plasticaribe.FindAsync(id);

            if (nominaDetallada_Plasticaribe == null)
            {
                return NotFound();
            }

            return nominaDetallada_Plasticaribe;
        }

        // PUT: api/NominaDetallada_Plasticaribe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNominaDetallada_Plasticaribe(int id, NominaDetallada_Plasticaribe nominaDetallada_Plasticaribe)
        {
            if (id != nominaDetallada_Plasticaribe.Id)
            {
                return BadRequest();
            }

            _context.Entry(nominaDetallada_Plasticaribe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NominaDetallada_PlasticaribeExists(id))
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

        // POST: api/NominaDetallada_Plasticaribe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NominaDetallada_Plasticaribe>> PostNominaDetallada_Plasticaribe(NominaDetallada_Plasticaribe nominaDetallada_Plasticaribe)
        {
            _context.NominaDetallada_Plasticaribe.Add(nominaDetallada_Plasticaribe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNominaDetallada_Plasticaribe", new { id = nominaDetallada_Plasticaribe.Id }, nominaDetallada_Plasticaribe);
        }

        // DELETE: api/NominaDetallada_Plasticaribe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNominaDetallada_Plasticaribe(int id)
        {
            var nominaDetallada_Plasticaribe = await _context.NominaDetallada_Plasticaribe.FindAsync(id);
            if (nominaDetallada_Plasticaribe == null)
            {
                return NotFound();
            }

            _context.NominaDetallada_Plasticaribe.Remove(nominaDetallada_Plasticaribe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NominaDetallada_PlasticaribeExists(int id)
        {
            return _context.NominaDetallada_Plasticaribe.Any(e => e.Id == id);
        }
    }
}
