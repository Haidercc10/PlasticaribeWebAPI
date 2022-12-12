using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detalle_MantenimientoController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalle_MantenimientoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalle_Mantenimiento>>> GetDetalle_Mantenimiento()
        {
            return await _context.Detalles_Mantenimientos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalle_Mantenimiento>> GetDetalle_Mantenimiento(long id)
        {
            var Activos = await _context.Detalles_Mantenimientos.FindAsync(id);

            if (Activos == null)
            {
                return NotFound();
            }

            return Activos;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle_Mantenimiento(long id, Detalle_Mantenimiento detalle_Mantenimiento)
        {
            if (id != detalle_Mantenimiento.DtMtto_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalle_Mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_MantenimientoExists(id))
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
        public async Task<ActionResult<Detalle_Mantenimiento>> PostDetalle_Mantenimiento(Detalle_Mantenimiento detalle_Mantenimiento)
        {
            _context.Detalles_Mantenimientos.Add(detalle_Mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalle_Mantenimiento", new { id = detalle_Mantenimiento.DtMtto_Codigo }, detalle_Mantenimiento);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalle_Mantenimiento(long id)
        {
            var activo = await _context.Detalles_Mantenimientos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            _context.Detalles_Mantenimientos.Remove(activo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Detalle_MantenimientoExists(long id)
        {
            return _context.Detalles_Mantenimientos.Any(e => e.DtMtto_Codigo == id);
        }
    }
}
