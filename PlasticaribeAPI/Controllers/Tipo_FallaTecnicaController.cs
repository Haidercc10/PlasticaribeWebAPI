using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Tipo_FallaTecnicaController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_FallaTecnicaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_FallaTecnica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_FallaTecnica>>> GetTipos_FallasTecnicas()
        {
            if (_context.Tipos_FallasTecnicas == null)
            {
                return NotFound();
            }
            return await _context.Tipos_FallasTecnicas.ToListAsync();
        }

        // GET: api/Tipo_FallaTecnica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_FallaTecnica>> GetTipo_FallaTecnica(int id)
        {
            if (_context.Tipos_FallasTecnicas == null)
            {
                return NotFound();
            }
            var tipo_FallaTecnica = await _context.Tipos_FallasTecnicas.FindAsync(id);

            if (tipo_FallaTecnica == null)
            {
                return NotFound();
            }

            return tipo_FallaTecnica;
        }

        // PUT: api/Tipo_FallaTecnica/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_FallaTecnica(int id, Tipo_FallaTecnica tipo_FallaTecnica)
        {
            if (id != tipo_FallaTecnica.TipoFalla_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_FallaTecnica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_FallaTecnicaExists(id))
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

        // POST: api/Tipo_FallaTecnica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_FallaTecnica>> PostTipo_FallaTecnica(Tipo_FallaTecnica tipo_FallaTecnica)
        {
            if (_context.Tipos_FallasTecnicas == null)
            {
                return Problem("Entity set 'dataContext.Tipos_FallasTecnicas'  is null.");
            }
            _context.Tipos_FallasTecnicas.Add(tipo_FallaTecnica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_FallaTecnica", new { id = tipo_FallaTecnica.TipoFalla_Id }, tipo_FallaTecnica);
        }

        // DELETE: api/Tipo_FallaTecnica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_FallaTecnica(int id)
        {
            if (_context.Tipos_FallasTecnicas == null)
            {
                return NotFound();
            }
            var tipo_FallaTecnica = await _context.Tipos_FallasTecnicas.FindAsync(id);
            if (tipo_FallaTecnica == null)
            {
                return NotFound();
            }

            _context.Tipos_FallasTecnicas.Remove(tipo_FallaTecnica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_FallaTecnicaExists(int id)
        {
            return (_context.Tipos_FallasTecnicas?.Any(e => e.TipoFalla_Id == id)).GetValueOrDefault();
        }
    }
}
