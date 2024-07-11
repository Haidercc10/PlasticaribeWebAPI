using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detalles_SalidasPeletizadoController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_SalidasPeletizadoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_SalidasPeletizado>>> GetDetalles_SalidasPeletizado()
        {
            return await _context.Detalles_SalidasPeletizado.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_SalidasPeletizado>> GetDetalles_SalidasPeletizado(long id)
        {
            var Detalles_SalidasPeletizado = await _context.Detalles_SalidasPeletizado.FindAsync(id);

            if (Detalles_SalidasPeletizado == null)
            {
                return NotFound();
            }

            return Detalles_SalidasPeletizado;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_SalidasPeletizado(long id, Detalles_SalidasPeletizado Detalles_SalidasPeletizado)
        {
            if (id != Detalles_SalidasPeletizado.DtSalPel_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(Detalles_SalidasPeletizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_SalidasPeletizadoExists(id))
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
        public async Task<ActionResult<Detalles_SalidasPeletizado>> PostDetalles_SalidasPeletizado(Detalles_SalidasPeletizado Detalles_SalidasPeletizado)
        {
            _context.Detalles_SalidasPeletizado.Add(Detalles_SalidasPeletizado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_SalidasPeletizado", new { id = Detalles_SalidasPeletizado.DtSalPel_Codigo }, Detalles_SalidasPeletizado);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_SalidasPeletizado(long id)
        {
            var Detalles_SalidasPeletizado = await _context.Detalles_SalidasPeletizado.FindAsync(id);
            if (Detalles_SalidasPeletizado == null)
            {
                return NotFound();
            }

            _context.Detalles_SalidasPeletizado.Remove(Detalles_SalidasPeletizado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Detalles_SalidasPeletizadoExists(long id)
        {
            return _context.Detalles_SalidasPeletizado.Any(e => e.DtSalPel_Codigo == id);
        }
    }
}
