using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Requerimientos_CalidadController : ControllerBase
    {
        private readonly dataContext _context;

        public Requerimientos_CalidadController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requerimientos_Calidad>>> GetRequerimientos_Calidad()
        {
            return await _context.Requerimientos_Calidad.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Requerimientos_Calidad>> GetRequerimientos_Calidad(long id)
        {
            var Requerimientos_Calidad = await _context.Requerimientos_Calidad.FindAsync(id);

            if (Requerimientos_Calidad == null)
            {
                return NotFound();
            }

            return Requerimientos_Calidad;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequerimientos_Calidad(long id, Requerimientos_Calidad Requerimientos_Calidad)
        {
            if (id != Requerimientos_Calidad.Req_Id)
            {
                return BadRequest();
            }

            _context.Entry(Requerimientos_Calidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Requerimientos_CalidadExists(id))
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
        public async Task<ActionResult<Requerimientos_Calidad>> PostRequerimientos_Calidad(Requerimientos_Calidad Requerimientos_Calidad)
        {
            _context.Requerimientos_Calidad.Add(Requerimientos_Calidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequerimientos_Calidad", new { id = Requerimientos_Calidad.Req_Id }, Requerimientos_Calidad);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequerimientos_Calidad(long id)
        {
            var Requerimientos_Calidad = await _context.Requerimientos_Calidad.FindAsync(id);
            if (Requerimientos_Calidad == null)
            {
                return NotFound();
            }

            _context.Requerimientos_Calidad.Remove(Requerimientos_Calidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Requerimientos_CalidadExists(long id)
        {
            return _context.Requerimientos_Calidad.Any(e => e.Req_Id == id);
        }
    }
}
