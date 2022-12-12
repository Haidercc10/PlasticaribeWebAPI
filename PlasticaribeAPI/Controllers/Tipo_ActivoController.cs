using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tipo_ActivoController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_ActivoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Activo>>> GetTipo_Activo()
        {
            return await _context.Tipos_Activos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Activo>> GetTipo_Activo(long id)
        {
            var Tipo_Activo = await _context.Tipos_Activos.FindAsync(id);

            if (Tipo_Activo == null)
            {
                return NotFound();
            }

            return Tipo_Activo;
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Activo(long id, Tipo_Activo tipo_Activo)
        {
            if (id != tipo_Activo.TpActv_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Activo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_ActivoExists(id))
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
        public async Task<ActionResult<Tipo_Activo>> PostTipo_Activo(Tipo_Activo tipo_Activo)
        {
            _context.Tipos_Activos.Add(tipo_Activo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Activo", new { id = tipo_Activo.TpActv_Id }, tipo_Activo);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Activo(long id)
        {
            var activo = await _context.Tipos_Activos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            _context.Tipos_Activos.Remove(activo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Tipo_ActivoExists(long id)
        {
            return _context.Tipos_Activos.Any(e => e.TpActv_Id == id);
        }
    }
}
