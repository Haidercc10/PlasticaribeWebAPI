using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PrestamosController : ControllerBase
    {
        private readonly dataContext _context;

        public PrestamosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Prestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamos>>> GetPrestamos()
        {
            return await _context.Prestamos.ToListAsync();
        }

        // GET: api/Prestamos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamos>> GetPrestamos(long id)
        {
            var prestamos = await _context.Prestamos.FindAsync(id);

            if (prestamos == null)
            {
                return NotFound();
            }

            return prestamos;
        }

        [HttpGet("getLoansForCardId/{id}")]
        public ActionResult LoansForCardId(long id) 
        {

            var Loans = from p in _context.Set<Prestamos>()
                        where p.Usua_Id == id &&
                        p.Estado_Id == 11
                        select new
                        {
                            loans = p,
                            User = p.Usuario,
                            Status = p.Estado,
                        };

            return Ok(Loans);
        }

        // PUT: api/Prestamos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamos(long id, Prestamos prestamos)
        {
            if (id != prestamos.Ptm_Id)
            {
                return BadRequest();
            }

            _context.Entry(prestamos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestamosExists(id))
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

        // POST: api/Prestamos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prestamos>> PostPrestamos(Prestamos prestamos)
        {
            _context.Prestamos.Add(prestamos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrestamos", new { id = prestamos.Ptm_Id }, prestamos);
        }

        // DELETE: api/Prestamos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamos(long id)
        {
            var prestamos = await _context.Prestamos.FindAsync(id);
            if (prestamos == null)
            {
                return NotFound();
            }

            _context.Prestamos.Remove(prestamos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrestamosExists(long id)
        {
            return _context.Prestamos.Any(e => e.Ptm_Id == id);
        }
    }
}
