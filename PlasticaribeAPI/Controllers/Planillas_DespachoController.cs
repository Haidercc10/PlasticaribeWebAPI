using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Planillas_DespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public Planillas_DespachoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Planillas_Despacho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Planillas_Despacho>>> GetPlanillas_Despacho()
        {
            return await _context.Planillas_Despacho.ToListAsync();
        }

        // GET: api/Planillas_Despacho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Planillas_Despacho>> GetPlanillas_Despacho(int id)
        {
            var Planillas_Despacho = await _context.Planillas_Despacho.FindAsync(id);

            if (Planillas_Despacho == null)
            {
                return NotFound();
            }

            return Planillas_Despacho;
        }

        // PUT: api/Planillas_Despacho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanillas_Despacho(int id, Planillas_Despacho Planillas_Despacho)
        {
            if (id != Planillas_Despacho.Pla_Id)
            {
                return BadRequest();
            }

            _context.Entry(Planillas_Despacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Planillas_DespachoExists(id))
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

        // POST: api/Planillas_Despacho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Planillas_Despacho>> PostPlanillas_Despacho(Planillas_Despacho Planillas_Despacho)
        {
            _context.Planillas_Despacho.Add(Planillas_Despacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlanillas_Despacho", new { id = Planillas_Despacho.Pla_Id }, Planillas_Despacho);
        }

        // DELETE: api/Planillas_Despacho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanillas_Despacho(int id)
        {
            var Planillas_Despacho = await _context.Planillas_Despacho.FindAsync(id);
            if (Planillas_Despacho == null)
            {
                return NotFound();
            }

            _context.Planillas_Despacho.Remove(Planillas_Despacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Planillas_DespachoExists(int id)
        {
            return _context.Planillas_Despacho.Any(e => e.Pla_Id == id);
        }
    }
}
