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
    public class Subcategorias_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Subcategorias_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subcategorias_MatPrima>>> GetSubcategorias_MatPrima()
        {
            return await _context.Subcategorias_MatPrima.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Subcategorias_MatPrima>> GetSubcategorias_MatPrima(long id)
        {
            var Subcategorias_MatPrima = await _context.Subcategorias_MatPrima.FindAsync(id);

            if (Subcategorias_MatPrima == null)
            {
                return NotFound();
            }

            return Subcategorias_MatPrima;
        }

        // Consulta que buscará el nombre de un Subcategorias_MatPrima por medio de los datos que se le vatan pasando, se usuará un Contains() (en sql es un LIKE)
        [HttpGet("getSubcategoriesForCategory/{id}")]
        public ActionResult getSubcategoriesForId(int id)
        {
            
            var Subcategorias_MatPrima = from s in _context.Set<Subcategorias_MatPrima>()
                          where s.CatMP_Id == id
                          select s;

            return Ok(Subcategorias_MatPrima);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcategorias_MatPrima(long id, Subcategorias_MatPrima Subcategorias_MatPrima)
        {
            if (id != Subcategorias_MatPrima.SubCatMP_Id)
            {
                return BadRequest();
            }

            _context.Entry(Subcategorias_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Subcategorias_MatPrimaExists(id))
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
        public async Task<ActionResult<Subcategorias_MatPrima>> PostSubcategorias_MatPrima(Subcategorias_MatPrima Subcategorias_MatPrima)
        {
            _context.Subcategorias_MatPrima.Add(Subcategorias_MatPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubcategorias_MatPrima", new { id = Subcategorias_MatPrima.SubCatMP_Id }, Subcategorias_MatPrima);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategorias_MatPrima(long id)
        {
            var Subcategorias_MatPrima = await _context.Subcategorias_MatPrima.FindAsync(id);
            if (Subcategorias_MatPrima == null)
            {
                return NotFound();
            }

            _context.Subcategorias_MatPrima.Remove(Subcategorias_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Subcategorias_MatPrimaExists(long id)
        {
            return _context.Subcategorias_MatPrima.Any(e => e.SubCatMP_Id == id);
        }
    }
}
