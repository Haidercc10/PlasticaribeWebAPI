using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Tipo_MantenimientoController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_MantenimientoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Mantenimiento>>> GetTipo_Mantenimiento()
        {
            return await _context.Tipos_Mantenimientos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Mantenimiento>> GetTipo_Mantenimiento(int id)
        {
            var Tipo_Mantenimiento = await _context.Tipos_Mantenimientos.FindAsync(id);

            if (Tipo_Mantenimiento == null)
            {
                return NotFound();
            }

            return Tipo_Mantenimiento;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Mantenimiento(int id, Tipo_Mantenimiento tipo_Mantenimiento)
        {
            if (id != tipo_Mantenimiento.TpMtto_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_MantenimientoExists(id))
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
        public async Task<ActionResult<Tipo_Mantenimiento>> PostTipo_Mantenimiento(Tipo_Mantenimiento tipo_Mantenimiento)
        {
            _context.Tipos_Mantenimientos.Add(tipo_Mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Mantenimiento", new { id = tipo_Mantenimiento.TpMtto_Id }, tipo_Mantenimiento);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Mantenimiento(long id)
        {
            var activo = await _context.Tipos_Mantenimientos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            _context.Tipos_Mantenimientos.Remove(activo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Tipo_MantenimientoExists(long id)
        {
            return _context.Tipos_Mantenimientos.Any(e => e.TpMtto_Id == id);
        }
    }
}
