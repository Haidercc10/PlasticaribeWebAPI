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
    public class Trazabilidad_ProduccionController : ControllerBase
    {
        private readonly dataContext _context;

        public Trazabilidad_ProduccionController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Trazabilidad_Produccion>>> GetTrazabilidad_Produccion()
        {
            return await _context.Trazabilidad_Produccion.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Trazabilidad_Produccion>> GetTrazabilidad_Produccion(int id)
        {
            var Trazabilidad_Produccion = await _context.Trazabilidad_Produccion.FindAsync(id);

            if (Trazabilidad_Produccion == null)
            {
                return NotFound();
            }

            return Trazabilidad_Produccion;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrazabilidad_Produccion(int id, Models.Trazabilidad_Produccion Trazabilidad_Produccion)
        {
            if (id != Trazabilidad_Produccion.Trz_Id)
            {
                return BadRequest();
            }

            _context.Entry(Trazabilidad_Produccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Trazabilidad_ProduccionExists(id))
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
        public async Task<ActionResult<Trazabilidad_Produccion>> PostTrazabilidad_Produccion(Models.Trazabilidad_Produccion Trazabilidad_Produccion)
        {
            _context.Trazabilidad_Produccion.Add(Trazabilidad_Produccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrazabilidad_Produccion", new { id = Trazabilidad_Produccion.Trz_Id }, Trazabilidad_Produccion);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrazabilidad_Produccion(int id)
        {
            var Trazabilidad_Produccion = await _context.Trazabilidad_Produccion.FindAsync(id);
            if (Trazabilidad_Produccion == null)
            {
                return NotFound();
            }

            _context.Trazabilidad_Produccion.Remove(Trazabilidad_Produccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Trazabilidad_ProduccionExists(int id)
        {
            return _context.Trazabilidad_Produccion.Any(e => e.Trz_Id == id);
        }
    }
}
