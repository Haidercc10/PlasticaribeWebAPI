using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivosController : ControllerBase
    {
        private readonly dataContext _context;

        public ActivosController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activo>>> GetActivos()
        {
            return await _context.Activos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Activo>> GetActivos(long id)
        {
            var Activos = await _context.Activos.FindAsync(id);

            if (Activos == null)
            {
                return NotFound();
            }

            return Activos;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivo(long id, Activo activo)
        {
            if (id != activo.Actv_Id)
            {
                return BadRequest();
            }

            _context.Entry(activo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivoExists(id))
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

        //
        [HttpPost]
        public async Task<ActionResult<Activo>> PostActivos(Activo activo)
        {
            _context.Activos.Add(activo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivos", new { id = activo.Actv_Id }, activo);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivos(long id)
        {
            var activo = await _context.Activos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            _context.Activos.Remove(activo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool ActivoExists(long id)
        {
            return _context.Activos.Any(e => e.Actv_Id == id);
        }
    }
}
