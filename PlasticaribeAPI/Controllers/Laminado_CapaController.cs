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
    public class Laminado_CapaController : ControllerBase
    {
        private readonly dataContext _context;

        public Laminado_CapaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Laminado_Capa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laminado_Capa>>> GetLaminado_Capa()
        {
            if (_context.Laminado_Capa == null)
            {
                return NotFound();
            }
            return await _context.Laminado_Capa.ToListAsync();
        }

        // GET: api/Laminado_Capa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laminado_Capa>> GetLaminado_Capa(int id)
        {
            if (_context.Laminado_Capa == null)
            {
                return NotFound();
            }
            var laminado_Capa = await _context.Laminado_Capa.FindAsync(id);

            if (laminado_Capa == null)
            {
                return NotFound();
            }

            return laminado_Capa;
        }

        [HttpGet("consultaLaminado/{LamCapa1_Nombre}/{LamCapa2_Nombre}/{LamCapa3_Nombre}")]
        public ActionResult GetConsultaLaminado(string LamCapa1_Nombre, string LamCapa2_Nombre, string LamCapa3_Nombre)
        {
            var con = from c1 in _context.Set<Laminado_Capa>()
                      from c2 in _context.Set<Laminado_Capa>()
                      from c3 in _context.Set<Laminado_Capa>()
                      where c1.LamCapa_Nombre == LamCapa1_Nombre
                            && c2.LamCapa_Nombre == LamCapa2_Nombre
                            && c3.LamCapa_Nombre == LamCapa3_Nombre
                      select new
                      {
                          LamCapa1_Id = c1.LamCapa_Id,
                          LamCapa2_Id = c2.LamCapa_Id,
                          LamCapa3_Id = c3.LamCapa_Id
                      };

            return Ok(con);
        }

        // PUT: api/Laminado_Capa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaminado_Capa(int id, Laminado_Capa laminado_Capa)
        {
            if (id != laminado_Capa.LamCapa_Id)
            {
                return BadRequest();
            }

            _context.Entry(laminado_Capa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Laminado_CapaExists(id))
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

        // POST: api/Laminado_Capa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Laminado_Capa>> PostLaminado_Capa(Laminado_Capa laminado_Capa)
        {
            if (_context.Laminado_Capa == null)
            {
                return Problem("Entity set 'dataContext.Laminado_Capa'  is null.");
            }
            _context.Laminado_Capa.Add(laminado_Capa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaminado_Capa", new { id = laminado_Capa.LamCapa_Id }, laminado_Capa);
        }

        // DELETE: api/Laminado_Capa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaminado_Capa(int id)
        {
            if (_context.Laminado_Capa == null)
            {
                return NotFound();
            }
            var laminado_Capa = await _context.Laminado_Capa.FindAsync(id);
            if (laminado_Capa == null)
            {
                return NotFound();
            }

            _context.Laminado_Capa.Remove(laminado_Capa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Laminado_CapaExists(int id)
        {
            return (_context.Laminado_Capa?.Any(e => e.LamCapa_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
