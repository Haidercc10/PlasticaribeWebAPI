using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController]
    public class Toma_FisicaController : ControllerBase
    {
        private readonly dataContext _context;

        public Toma_FisicaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Toma_Fisica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Toma_Fisica>>> GetToma_Fisica()
        {
            return await _context.Toma_Fisica.ToListAsync();
        }

        // GET: api/Toma_Fisica/GetLastTomaFisicaId
        //Función para obtener el último ID de Toma_Fisica
        [HttpGet("getTomasFisicas")]
        public ActionResult getTomasFisicas()
        {
            var toma = from t in _context.Set<Toma_Fisica>()
                       where t.Estado_Id == 1
                       select t;

            return Ok(toma);
        }

        // GET: api/Toma_Fisica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Toma_Fisica>> GetToma_Fisica(int id)
        {
            var Toma_Fisica = await _context.Toma_Fisica.FindAsync(id);

            if (Toma_Fisica == null)
            {
                return NotFound();
            }

            return Toma_Fisica;
        }

        // PUT: api/Toma_Fisica/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToma_Fisica(int id, Toma_Fisica Toma_Fisica)
        {
            if (id != Toma_Fisica.Toma_Id)
            {
                return BadRequest();
            }

            _context.Entry(Toma_Fisica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Toma_FisicaExists(id))
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

        // POST: api/Toma_Fisica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Toma_Fisica>> PostToma_Fisica(Toma_Fisica Toma_Fisica)
        {
            _context.Toma_Fisica.Add(Toma_Fisica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToma_Fisica", new { id = Toma_Fisica.Toma_Id }, Toma_Fisica);
        }

        // DELETE: api/Toma_Fisica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToma_Fisica(int id)
        {
            var Toma_Fisica = await _context.Toma_Fisica.FindAsync(id);
            if (Toma_Fisica == null)
            {
                return NotFound();
            }

            _context.Toma_Fisica.Remove(Toma_Fisica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Toma_FisicaExists(int id)
        {
            return _context.Toma_Fisica.Any(e => e.Toma_Id == id);
        }
    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
}
