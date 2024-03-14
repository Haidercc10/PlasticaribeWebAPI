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
    public class FormatosController : ControllerBase
    {
        private readonly dataContext _context;

        public FormatosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Formatos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formato>>> GetFormato()
        {
            if (_context.Formato == null)
            {
                return NotFound();
            }
            return await _context.Formato.ToListAsync();
        }

        // GET: api/Formatos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Formato>> GetFormato(long id)
        {
            if (_context.Formato == null)
            {
                return NotFound();
            }
            var formato = await _context.Formato.FindAsync(id);

            if (formato == null)
            {
                return NotFound();
            }

            return formato;
        }

        [HttpGet("consultaExtrusion/{Formato_Nombre}/{Material_Nombre}/{Pigmt_Nombre}/{Tratado_Nombre}")]
        public ActionResult GetCaonsultaExtrusion(string Formato_Nombre, string Material_Nombre, string Pigmt_Nombre, string Tratado_Nombre)
        {

            var con = from f in _context.Set<Formato>()
                      from m in _context.Set<Material_MatPrima>()
                      from p in _context.Set<Pigmento>()
                      from t in _context.Set<Tratado>()
                      where f.Formato_Nombre == Formato_Nombre
                            && m.Material_Nombre == Material_Nombre
                            && p.Pigmt_Nombre == Pigmt_Nombre
                            && t.Tratado_Nombre == Tratado_Nombre
                      select new
                      {
                          f.Formato_Id,
                          m.Material_Id,
                          p.Pigmt_Id,
                          t.Tratado_Id
                      };

            return Ok(con);
        }

        // PUT: api/Formatos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormato(long id, Formato formato)
        {
            if (id != formato.Formato_Id)
            {
                return BadRequest();
            }

            _context.Entry(formato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormatoExists(id))
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

        // POST: api/Formatos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Formato>> PostFormato(Formato formato)
        {
            if (_context.Formato == null)
            {
                return Problem("Entity set 'dataContext.Formato'  is null.");
            }
            _context.Formato.Add(formato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormato", new { id = formato.Formato_Id }, formato);
        }

        // DELETE: api/Formatos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormato(long id)
        {
            if (_context.Formato == null)
            {
                return NotFound();
            }
            var formato = await _context.Formato.FindAsync(id);
            if (formato == null)
            {
                return NotFound();
            }

            _context.Formato.Remove(formato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormatoExists(long id)
        {
            return (_context.Formato?.Any(e => e.Formato_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
