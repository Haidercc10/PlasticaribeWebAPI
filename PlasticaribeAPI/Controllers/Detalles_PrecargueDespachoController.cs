using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detalles_PrecargueDespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_PrecargueDespachoController(dataContext context)
        {
            _context = context;
        }
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_PrecargueDespacho>>> GetDetalles_PrecargueDespacho()
        {
            return await _context.Detalles_PrecargueDespacho.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_PrecargueDespacho>> GetDetalles_PrecargueDespacho(long id)
        {
            var Detalles_PrecargueDespacho = await _context.Detalles_PrecargueDespacho.FindAsync(id);

            if (Detalles_PrecargueDespacho == null)
            {
                return NotFound();
            }

            return Detalles_PrecargueDespacho;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_PrecargueDespacho(long id, Detalles_PrecargueDespacho Detalles_PrecargueDespacho)
        {
            if (id != Detalles_PrecargueDespacho.DtlPcd_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(Detalles_PrecargueDespacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_PrecargueDespachoExists(id))
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
        public async Task<ActionResult<Detalles_PrecargueDespacho>> PostDetalles_PrecargueDespacho(Detalles_PrecargueDespacho Detalles_PrecargueDespacho)
        {
            _context.Detalles_PrecargueDespacho.Add(Detalles_PrecargueDespacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_PrecargueDespacho", new { id = Detalles_PrecargueDespacho.DtlPcd_Codigo }, Detalles_PrecargueDespacho);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_PrecargueDespacho(long id)
        {
            var Detalles_PrecargueDespacho = await _context.Detalles_PrecargueDespacho.FindAsync(id);
            if (Detalles_PrecargueDespacho == null)
            {
                return NotFound();
            }

            _context.Detalles_PrecargueDespacho.Remove(Detalles_PrecargueDespacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Detalles_PrecargueDespachoExists(long id)
        {
            return _context.Detalles_PrecargueDespacho.Any(e => e.DtlPcd_Codigo == id);
        }
    }
}
