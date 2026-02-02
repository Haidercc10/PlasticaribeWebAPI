using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioSnapshotController : ControllerBase
    {
        private readonly dataContext _context;

        public InventarioSnapshotController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Inventarios_Snapshot
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Inventarios_Snapshot>>> GetInventarios_Snapshot()
        {
            return await _context.Inventarios_Snapshot.ToListAsync();
        }

        // GET: api/Inventarios_Snapshot/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Inventarios_Snapshot>> GetInventarios_Snapshot(int id)
        {
            var inv = await _context.Inventarios_Snapshot.FindAsync(id);

            if (inv == null)
            {
                return NotFound();
            }

            return inv;
        }

        //Función para obtener el último ID de Toma_Fisica
        [HttpGet("getInventoriesSnapshot")]
        public ActionResult getInventoriesSnapshot()
        {
            var toma = from i in _context.Set<Inventarios_Snapshot>()
                       select i;

            return Ok(toma);
        }

        // PUT: api/Inventarios_Snapshot/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventarios_Snapshot(int id, Inventarios_Snapshot Inventarios_Snapshot)
        {
            if (id != Inventarios_Snapshot.InvSnap_Id)
            {
                return BadRequest();
            }

            _context.Entry(Inventarios_Snapshot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Inventarios_SnapshotExists(id))
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

        // POST: api/Inventarios_Snapshot
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventarios_Snapshot>> PostInventarios_Snapshot(Inventarios_Snapshot Inventarios_Snapshot)
        {
            _context.Inventarios_Snapshot.Add(Inventarios_Snapshot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventarios_Snapshot", new { id = Inventarios_Snapshot.InvSnap_Id }, Inventarios_Snapshot);
        }

        // DELETE: api/Inventarios_Snapshot/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventarios_Snapshot(int id)
        {
            var Inventarios_Snapshot = await _context.Inventarios_Snapshot.FindAsync(id);
            if (Inventarios_Snapshot == null)
            {
                return NotFound();
            }

            _context.Inventarios_Snapshot.Remove(Inventarios_Snapshot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Inventarios_SnapshotExists(int id)
        {
            return _context.Inventarios_Snapshot.Any(e => e.InvSnap_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
