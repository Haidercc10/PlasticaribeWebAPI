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
    public class AsignacionRollos_ExtrusionController : ControllerBase
    {
        private readonly dataContext _context;

        public AsignacionRollos_ExtrusionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/AsignacionRollos_Extrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsignacionRollos_Extrusion>>> GetAsignacionRollos_Extrusion()
        {
            return await _context.AsignacionRollos_Extrusion.ToListAsync();
        }

        // GET: api/AsignacionRollos_Extrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AsignacionRollos_Extrusion>> GetAsignacionRollos_Extrusion(int id)
        {
            var asignacionRollos_Extrusion = await _context.AsignacionRollos_Extrusion.FindAsync(id);

            if (asignacionRollos_Extrusion == null)
            {
                return NotFound();
            }

            return asignacionRollos_Extrusion;
        }

        // GET: Obtener ultimo ID
        [HttpGet("getObtenerUltimoID")]
        public ActionResult getObtenerUltimoID(long id)
        {
            var con = _context.AsignacionRollos_Extrusion.OrderByDescending(x => x.AsgRollos_Id).First();
            return Ok(con);
        }

        // PUT: api/AsignacionRollos_Extrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacionRollos_Extrusion(int id, AsignacionRollos_Extrusion asignacionRollos_Extrusion)
        {
            if (id != asignacionRollos_Extrusion.AsgRollos_Id)
            {
                return BadRequest();
            }

            _context.Entry(asignacionRollos_Extrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionRollos_ExtrusionExists(id))
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

        // POST: api/AsignacionRollos_Extrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AsignacionRollos_Extrusion>> PostAsignacionRollos_Extrusion(AsignacionRollos_Extrusion asignacionRollos_Extrusion)
        {
            _context.AsignacionRollos_Extrusion.Add(asignacionRollos_Extrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacionRollos_Extrusion", new { id = asignacionRollos_Extrusion.AsgRollos_Id }, asignacionRollos_Extrusion);
        }

        // DELETE: api/AsignacionRollos_Extrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacionRollos_Extrusion(int id)
        {
            var asignacionRollos_Extrusion = await _context.AsignacionRollos_Extrusion.FindAsync(id);
            if (asignacionRollos_Extrusion == null)
            {
                return NotFound();
            }

            _context.AsignacionRollos_Extrusion.Remove(asignacionRollos_Extrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignacionRollos_ExtrusionExists(int id)
        {
            return _context.AsignacionRollos_Extrusion.Any(e => e.AsgRollos_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
