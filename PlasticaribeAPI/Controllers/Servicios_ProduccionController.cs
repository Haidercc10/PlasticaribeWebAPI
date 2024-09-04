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
    public class Servicios_ProduccionController : ControllerBase
    {
        private readonly dataContext _context;

        public Servicios_ProduccionController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicios_Produccion>>> GetServicios_Produccion()
        {
            return await _context.Servicios_Produccion.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicios_Produccion>> GetServicios_Produccion(long id)
        {
            var Servicios_Produccion = await _context.Servicios_Produccion.FindAsync(id);

            if (Servicios_Produccion == null)
            {
                return NotFound();
            }

            return Servicios_Produccion;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicios_Produccion(long id, Servicios_Produccion Servicios_Produccion)
        {
            if (id != Servicios_Produccion.SvcProd_Id)
            {
                return BadRequest();
            }

            _context.Entry(Servicios_Produccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Servicios_ProduccionExists(id))
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
        public async Task<ActionResult<Servicios_Produccion>> PostActivos(Servicios_Produccion Servicios_Produccion)
        {
            _context.Servicios_Produccion.Add(Servicios_Produccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServicios_Produccion", new { id = Servicios_Produccion.SvcProd_Id }, Servicios_Produccion);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicios_Produccion(long id)
        {
            var Servicios_Produccion = await _context.Servicios_Produccion.FindAsync(id);
            if (Servicios_Produccion == null)
            {
                return NotFound();
            }

            _context.Servicios_Produccion.Remove(Servicios_Produccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Servicios_ProduccionExists(long id)
        {
            return _context.Servicios_Produccion.Any(e => e.SvcProd_Id == id);
        }
    }
}
