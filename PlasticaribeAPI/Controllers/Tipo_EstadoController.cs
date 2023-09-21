#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Tipo_EstadoController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_EstadoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Estado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Estado>>> GetTipos_Estados()
        {
            return await _context.Tipos_Estados.ToListAsync();
        }

        // GET: api/Tipo_Estado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Estado>> GetTipo_Estado(int id)
        {
            var tipo_Estado = await _context.Tipos_Estados.FindAsync(id);

            if (tipo_Estado == null)
            {
                return NotFound();
            }

            return tipo_Estado;
        }

        // PUT: api/Tipo_Estado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Estado(int id, Tipo_Estado tipo_Estado)
        {
            if (id != tipo_Estado.TpEstado_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_EstadoExists(id))
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

        // POST: api/Tipo_Estado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_Estado>> PostTipo_Estado(Tipo_Estado tipo_Estado)
        {
            _context.Tipos_Estados.Add(tipo_Estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Estado", new { id = tipo_Estado.TpEstado_Id }, tipo_Estado);
        }

        // DELETE: api/Tipo_Estado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Estado(int id)
        {
            var tipo_Estado = await _context.Tipos_Estados.FindAsync(id);
            if (tipo_Estado == null)
            {
                return NotFound();
            }

            _context.Tipos_Estados.Remove(tipo_Estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_EstadoExists(int id)
        {
            return _context.Tipos_Estados.Any(e => e.TpEstado_Id == id);
        }
    }
}
