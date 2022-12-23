#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly dataContext _context;

        public AreasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
        {
            return await _context.Areas.ToListAsync();
        }

        [HttpGet("getNombreArea/{Nombre}")]
        public ActionResult GetAreaNombre(string Nombre)
        {
            var area =  _context.Areas.Where(a => a.Area_Nombre == Nombre).Select(ar => new { ar.Area_Nombre });

            /*if (area == null)
            {
                return NotFound();
            }*/

            return Ok(area);
        }

        [HttpGet("getNombreAreaXLike/{Nombre}")]
        public ActionResult GetNombre(string Nombre)
        {
            var area = from ar in _context.Set<Area>()
                       where ar.Area_Nombre.Contains(Nombre)
                       select new { ar.Area_Nombre }; 

            /*if (area == null)
            {
                return NotFound();
            }*/

            return Ok(area);
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(long id)
        {
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(long id, Area area)
        {
            if (id != area.Area_Id)
            {
                return BadRequest();
            }

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
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

        // POST: api/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            _context.Areas.Add(area);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArea", new { id = area.Area_Id }, area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(long id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaExists(long id)
        {
            return _context.Areas.Any(e => e.Area_Id == id);
        }
    }
}
