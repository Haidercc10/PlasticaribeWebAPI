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
    public class Usabilidad_ModulosController : ControllerBase
    {
        private readonly dataContext _context;

        public Usabilidad_ModulosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Usabilidad_Modulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usabilidad_Modulos>>> GetUsabilidad_Modulos()
        {
            return await _context.Usabilidad_Modulos.ToListAsync();
        }

        // GET: api/Usabilidad_Modulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usabilidad_Modulos>> GetUsabilidad_Modulos(long id)
        {
            var usabilidad = await _context.Usabilidad_Modulos.FindAsync(id);

            if (usabilidad == null)
            {
                return NotFound();
            }

            return usabilidad;
        }

        // PUT: api/Usabilidad_Modulos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsabilidad_Modulos(long id, Usabilidad_Modulos Usabilidad_Modulos)
        {
            if (id != Usabilidad_Modulos.Usm_Id)
            {
                return BadRequest();
            }

            _context.Entry(Usabilidad_Modulos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Usabilidad_ModulosExists(id))
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

        // POST: api/Usabilidad_Modulos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usabilidad_Modulos>> PostUsabilidad_Modulos(Usabilidad_Modulos Usabilidad_Modulos)
        {
            _context.Usabilidad_Modulos.Add(Usabilidad_Modulos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsabilidad_Modulos", new { id = Usabilidad_Modulos.Usm_Id }, Usabilidad_Modulos);
        }

        // DELETE: api/Usabilidad_Modulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsabilidad_Modulos(long id)
        {
            var Usabilidad_Modulos = await _context.Usabilidad_Modulos.FindAsync(id);
            if (Usabilidad_Modulos == null)
            {
                return NotFound();
            }

            _context.Usabilidad_Modulos.Remove(Usabilidad_Modulos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Usabilidad_ModulosExists(long id)
        {
            return _context.Usabilidad_Modulos.Any(e => e.Usm_Id == id);
        }

    }
}
