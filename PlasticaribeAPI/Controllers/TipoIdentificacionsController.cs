#nullable disable
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
    public class TipoIdentificacionsController : ControllerBase
    {
        private readonly dataContext _context;

        public TipoIdentificacionsController(dataContext context)
        {
            _context = context;
        }

        // GET: api/TipoIdentificacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoIdentificacion>>> GetTipoIdentificacion()
        {
            return await _context.TipoIdentificaciones.ToListAsync();
        }

        // GET: api/TipoIdentificacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoIdentificacion>> GetTipoIdentificacion(string id)
        {
            var tipoIdentificacion = await _context.TipoIdentificaciones.FindAsync(id);

            if (tipoIdentificacion == null)
            {
                return NotFound();
            }

            return tipoIdentificacion;
        }

        // PUT: api/TipoIdentificacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoIdentificacion(string id, TipoIdentificacion tipoIdentificacion)
        {
            if (id != tipoIdentificacion.TipoIdentificacion_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoIdentificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoIdentificacionExists(id))
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

        // POST: api/TipoIdentificacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoIdentificacion>> PostTipoIdentificacion(TipoIdentificacion tipoIdentificacion)
        {
            _context.TipoIdentificaciones.Add(tipoIdentificacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TipoIdentificacionExists(tipoIdentificacion.TipoIdentificacion_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTipoIdentificacion", new { id = tipoIdentificacion.TipoIdentificacion_Id }, tipoIdentificacion);
        }

        // DELETE: api/TipoIdentificacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoIdentificacion(string id)
        {
            var tipoIdentificacion = await _context.TipoIdentificaciones.FindAsync(id);
            if (tipoIdentificacion == null)
            {
                return NotFound();
            }

            _context.TipoIdentificaciones.Remove(tipoIdentificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoIdentificacionExists(string id)
        {
            return _context.TipoIdentificaciones.Any(e => e.TipoIdentificacion_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
