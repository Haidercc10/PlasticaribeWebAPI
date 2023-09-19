using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class IngresoRollos_ExtrusionController : ControllerBase
    {
        private readonly dataContext _context;

        public IngresoRollos_ExtrusionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/IngresoRollos_Extrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngresoRollos_Extrusion>>> GetIngresoRollos_Extrusion()
        {
            return await _context.IngresoRollos_Extrusion.ToListAsync();
        }

        // GET: api/IngresoRollos_Extrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngresoRollos_Extrusion>> GetIngresoRollos_Extrusion(long id)
        {
            var ingresoRollos_Extrusion = await _context.IngresoRollos_Extrusion.FindAsync(id);

            if (ingresoRollos_Extrusion == null)
            {
                return NotFound();
            }

            return ingresoRollos_Extrusion;
        }

        // GET: Obtener ultimo ID
        [HttpGet("getObtenerUltimoID")]
        public ActionResult getObtenerUltimoID(long id)
        {
            var con = _context.IngresoRollos_Extrusion.OrderByDescending(x => x.IngRollo_Id).First();
            return Ok(con);
        }

        // PUT: api/IngresoRollos_Extrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngresoRollos_Extrusion(long id, IngresoRollos_Extrusion ingresoRollos_Extrusion)
        {
            if (id != ingresoRollos_Extrusion.IngRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(ingresoRollos_Extrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngresoRollos_ExtrusionExists(id))
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

        // POST: api/IngresoRollos_Extrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngresoRollos_Extrusion>> PostIngresoRollos_Extrusion(IngresoRollos_Extrusion ingresoRollos_Extrusion)
        {
            _context.IngresoRollos_Extrusion.Add(ingresoRollos_Extrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngresoRollos_Extrusion", new { id = ingresoRollos_Extrusion.IngRollo_Id }, ingresoRollos_Extrusion);
        }

        // DELETE: api/IngresoRollos_Extrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngresoRollos_Extrusion(long id)
        {
            var ingresoRollos_Extrusion = await _context.IngresoRollos_Extrusion.FindAsync(id);
            if (ingresoRollos_Extrusion == null)
            {
                return NotFound();
            }

            _context.IngresoRollos_Extrusion.Remove(ingresoRollos_Extrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngresoRollos_ExtrusionExists(long id)
        {
            return _context.IngresoRollos_Extrusion.Any(e => e.IngRollo_Id == id);
        }
    }
}
