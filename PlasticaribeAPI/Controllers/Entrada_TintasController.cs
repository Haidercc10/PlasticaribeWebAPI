using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Entrada_TintasController : ControllerBase
    {
        private readonly dataContext _context;

        public Entrada_TintasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Entrada_Tintas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrada_Tintas>>> GetEntradas_Tintas()
        {
            if (_context.Entradas_Tintas == null)
            {
                return NotFound();
            }
            return await _context.Entradas_Tintas.ToListAsync();
        }


        [HttpGet("ultimoId/")]
        public ActionResult GetUltimoId()
        {
            var asignacion = _context.Entradas_Tintas.OrderBy(asg => asg.EntTinta_Id).Last();

            return Ok(asignacion);
        }

        // GET: api/Entrada_Tintas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entrada_Tintas>> GetEntrada_Tintas(int id)
        {
            if (_context.Entradas_Tintas == null)
            {
                return NotFound();
            }
            var entrada_Tintas = await _context.Entradas_Tintas.FindAsync(id);

            if (entrada_Tintas == null)
            {
                return NotFound();
            }

            return entrada_Tintas;
        }

        // PUT: api/Entrada_Tintas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrada_Tintas(int id, Entrada_Tintas entrada_Tintas)
        {
            if (id != entrada_Tintas.EntTinta_Id)
            {
                return BadRequest();
            }

            _context.Entry(entrada_Tintas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Entrada_TintasExists(id))
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

        // POST: api/Entrada_Tintas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entrada_Tintas>> PostEntrada_Tintas(Entrada_Tintas entrada_Tintas)
        {
            if (_context.Entradas_Tintas == null)
            {
                return Problem("Entity set 'dataContext.Entradas_Tintas'  is null.");
            }
            _context.Entradas_Tintas.Add(entrada_Tintas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrada_Tintas", new { id = entrada_Tintas.EntTinta_Id }, entrada_Tintas);
        }

        // DELETE: api/Entrada_Tintas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrada_Tintas(int id)
        {
            if (_context.Entradas_Tintas == null)
            {
                return NotFound();
            }
            var entrada_Tintas = await _context.Entradas_Tintas.FindAsync(id);
            if (entrada_Tintas == null)
            {
                return NotFound();
            }

            _context.Entradas_Tintas.Remove(entrada_Tintas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Entrada_TintasExists(int id)
        {
            return (_context.Entradas_Tintas?.Any(e => e.EntTinta_Id == id)).GetValueOrDefault();
        }
    }
}
