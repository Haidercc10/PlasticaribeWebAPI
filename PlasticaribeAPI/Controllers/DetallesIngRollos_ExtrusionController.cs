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
    public class DetallesIngRollos_ExtrusionController : ControllerBase
    {
        private readonly dataContext _context;

        public DetallesIngRollos_ExtrusionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetallesIngRollos_Extrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallesIngRollos_Extrusion>>> GetDetallesIngRollos_Extrusion()
        {
            return await _context.DetallesIngRollos_Extrusion.ToListAsync();
        }

        // GET: api/DetallesIngRollos_Extrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallesIngRollos_Extrusion>> GetDetallesIngRollos_Extrusion(int id)
        {
            var detallesIngRollos_Extrusion = await _context.DetallesIngRollos_Extrusion.FindAsync(id);

            if (detallesIngRollos_Extrusion == null)
            {
                return NotFound();
            }

            return detallesIngRollos_Extrusion;
        }

        [HttpGet("consultaRollos")]
        public ActionResult consultaRollos()
        {
            var con = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                      select ing.Rollo_Id;
            return Ok(con);
        }

        // PUT: api/DetallesIngRollos_Extrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallesIngRollos_Extrusion(int id, DetallesIngRollos_Extrusion detallesIngRollos_Extrusion)
        {
            if (id != detallesIngRollos_Extrusion.DtIngRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(detallesIngRollos_Extrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesIngRollos_ExtrusionExists(id))
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

        // POST: api/DetallesIngRollos_Extrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallesIngRollos_Extrusion>> PostDetallesIngRollos_Extrusion(DetallesIngRollos_Extrusion detallesIngRollos_Extrusion)
        {
            _context.DetallesIngRollos_Extrusion.Add(detallesIngRollos_Extrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallesIngRollos_Extrusion", new { id = detallesIngRollos_Extrusion.DtIngRollo_Id }, detallesIngRollos_Extrusion);
        }

        // DELETE: api/DetallesIngRollos_Extrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallesIngRollos_Extrusion(int id)
        {
            var detallesIngRollos_Extrusion = await _context.DetallesIngRollos_Extrusion.FindAsync(id);
            if (detallesIngRollos_Extrusion == null)
            {
                return NotFound();
            }

            _context.DetallesIngRollos_Extrusion.Remove(detallesIngRollos_Extrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallesIngRollos_ExtrusionExists(int id)
        {
            return _context.DetallesIngRollos_Extrusion.Any(e => e.DtIngRollo_Id == id);
        }
    }
}
