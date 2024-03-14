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
    public class PreEntrega_RolloDespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public PreEntrega_RolloDespachoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/PreEntrega_RolloDespacho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreEntrega_RolloDespacho>>> GetPreEntrega_RollosDespacho()
        {
            return await _context.PreEntrega_RollosDespacho.ToListAsync();
        }

        // GET: api/PreEntrega_RolloDespacho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreEntrega_RolloDespacho>> GetPreEntrega_RolloDespacho(long id)
        {
            var preEntrega_RolloDespacho = await _context.PreEntrega_RollosDespacho.FindAsync(id);

            if (preEntrega_RolloDespacho == null)
            {
                return NotFound();
            }

            return preEntrega_RolloDespacho;
        }

        // PUT: api/PreEntrega_RolloDespacho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreEntrega_RolloDespacho(long id, PreEntrega_RolloDespacho preEntrega_RolloDespacho)
        {
            if (id != preEntrega_RolloDespacho.PreEntRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(preEntrega_RolloDespacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreEntrega_RolloDespachoExists(id))
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

        // POST: api/PreEntrega_RolloDespacho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PreEntrega_RolloDespacho>> PostPreEntrega_RolloDespacho(PreEntrega_RolloDespacho preEntrega_RolloDespacho)
        {
            _context.PreEntrega_RollosDespacho.Add(preEntrega_RolloDespacho);
            preEntrega_RolloDespacho.PreEntRollo_Fecha = DateTime.Now;
            preEntrega_RolloDespacho.PreEntRollo_Hora = DateTime.Now.ToString("hh:mm:ss");
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreEntrega_RolloDespacho", new { id = preEntrega_RolloDespacho.PreEntRollo_Id }, preEntrega_RolloDespacho);
        }

        // DELETE: api/PreEntrega_RolloDespacho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreEntrega_RolloDespacho(long id)
        {
            var preEntrega_RolloDespacho = await _context.PreEntrega_RollosDespacho.FindAsync(id);
            if (preEntrega_RolloDespacho == null)
            {
                return NotFound();
            }

            _context.PreEntrega_RollosDespacho.Remove(preEntrega_RolloDespacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreEntrega_RolloDespachoExists(long id)
        {
            return _context.PreEntrega_RollosDespacho.Any(e => e.PreEntRollo_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
