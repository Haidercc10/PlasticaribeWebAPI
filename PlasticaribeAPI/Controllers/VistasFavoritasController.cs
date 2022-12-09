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
    public class VistasFavoritasController : ControllerBase
    {
        private readonly dataContext _context;

        public VistasFavoritasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/VistasFavoritas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistasFavoritas>>> GetVistasFavoritas()
        {
            return await _context.VistasFavoritas.ToListAsync();
        }

        // GET: api/VistasFavoritas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VistasFavoritas>> GetVistasFavoritas(int id)
        {
            var vistasFavoritas = await _context.VistasFavoritas.FindAsync(id);

            if (vistasFavoritas == null)
            {
                return NotFound();
            }

            return vistasFavoritas;
        }

        [HttpGet("getVistasFavUsuario/{usuario}")]
        public ActionResult getVistasFavUsuario (long usuario)
        {
            var con = from vf in _context.Set<VistasFavoritas>()
                      where vf.Usua_Id == usuario
                      select vf;
            return Ok(con);
        }

        // PUT: api/VistasFavoritas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVistasFavoritas(int id, VistasFavoritas vistasFavoritas)
        {
            if (id != vistasFavoritas.VistasFav_Id)
            {
                return BadRequest();
            }

            _context.Entry(vistasFavoritas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VistasFavoritasExists(id))
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

        // POST: api/VistasFavoritas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VistasFavoritas>> PostVistasFavoritas(VistasFavoritas vistasFavoritas)
        {
            _context.VistasFavoritas.Add(vistasFavoritas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVistasFavoritas", new { id = vistasFavoritas.VistasFav_Id }, vistasFavoritas);
        }

        // DELETE: api/VistasFavoritas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVistasFavoritas(int id)
        {
            var vistasFavoritas = await _context.VistasFavoritas.FindAsync(id);
            if (vistasFavoritas == null)
            {
                return NotFound();
            }

            _context.VistasFavoritas.Remove(vistasFavoritas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VistasFavoritasExists(int id)
        {
            return _context.VistasFavoritas.Any(e => e.VistasFav_Id == id);
        }
    }
}
