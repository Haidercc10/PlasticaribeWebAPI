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
    public class MaquinasController : ControllerBase
    {

        private readonly dataContext _context;

        public MaquinasController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquinas>>> GetMaquinas()
        {
            return await _context.Maquinas.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Maquinas>> GetMaquinas(long id)
        {
            var Maquinas = await _context.Maquinas.FindAsync(id);

            if (Maquinas == null)
            {
                return NotFound();
            }

            return Maquinas;
        }

        //Fuón para obtener las máquinas por proceso
        [HttpGet("GetMaquinasPorProceso/{proceso}")]
        public async Task<ActionResult<Maquinas>> GetMaquinasPorProceso(string proceso)
        {
            var Maquinas = await (
                from m in _context.Maquinas.AsNoTracking()
                where m.Proceso_Id == proceso
                select m
            ).ToListAsync();

            if (Maquinas == null)
            {
                return NotFound();
            }

            return Ok(Maquinas);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquinas(string id, Maquinas Maquinas)
        {
            if (id != Maquinas.Maq_Id)
            {
                return BadRequest();
            }

            _context.Entry(Maquinas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinasExists(id))
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
        public async Task<ActionResult<Maquinas>> PostMaquinas(Maquinas Maquinas)
        {
            _context.Maquinas.Add(Maquinas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaquinas", new { id = Maquinas.Maq_Id }, Maquinas);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquinas(string id)
        {
            var Maquinas = await _context.Maquinas.FindAsync(id);
            if (Maquinas == null)
            {
                return NotFound();
            }

            _context.Maquinas.Remove(Maquinas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool MaquinasExists(string id)
        {
            return _context.Maquinas.Any(e => e.Maq_Id == id);
        }
    }
}
