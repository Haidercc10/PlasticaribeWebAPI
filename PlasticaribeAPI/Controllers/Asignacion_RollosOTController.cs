using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Asignacion_RollosOTController : ControllerBase
    {
        private readonly dataContext _context;

        public Asignacion_RollosOTController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Asignacion_RollosOT
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion_RollosOT>>> GetAsignacion_RollosOT()
        {
            return await _context.Asignacion_RollosOT.ToListAsync();
        }

        // GET: api/Asignacion_RollosOT/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion_RollosOT>> GetAsignacion_RollosOT(long id)
        {
            var asg = await _context.Asignacion_RollosOT.FindAsync(id);

            if (asg == null)
            {
                return NotFound();
            }

            return asg;
        }

        // PUT: api/Asignacion_RollosOT/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion_RollosOT(long id, Asignacion_RollosOT Asignacion_RollosOT)
        {
            if (id != Asignacion_RollosOT.AsgRll_Id)
            {
                return BadRequest();
            }

            _context.Entry(Asignacion_RollosOT).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asignacion_RollosOTExists(id))
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

        // POST: api/Asignacion_RollosOT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignacion_RollosOT>> PostAsignacion_RollosOT(Asignacion_RollosOT Asignacion_RollosOT)
        {
            _context.Asignacion_RollosOT.Add(Asignacion_RollosOT);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion_RollosOT", new { id = Asignacion_RollosOT.AsgRll_Id }, Asignacion_RollosOT);
        }

        // DELETE: api/Asignacion_RollosOT/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion_RollosOT(long id)
        {
            var Asignacion_RollosOT = await _context.Asignacion_RollosOT.FindAsync(id);
            if (Asignacion_RollosOT == null)
            {
                return NotFound();
            }

            _context.Asignacion_RollosOT.Remove(Asignacion_RollosOT);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Asignacion_RollosOTExists(long id)
        {
            return _context.Asignacion_RollosOT.Any(e => e.AsgRll_Id == id);
        }
    }
}
