#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class fondoPensionsController : ControllerBase
    {
        private readonly dataContext _context;

        public fondoPensionsController(dataContext context)
        {
            _context = context;
        }

        // GET: api/fondoPensions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<fondoPension>>> GetFondosPensiones()
        {
            return await _context.FondosPensiones.ToListAsync();
        }

        // GET: api/fondoPensions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<fondoPension>> GetfondoPension(long id)
        {
            var fondoPension = await _context.FondosPensiones.FindAsync(id);

            if (fondoPension == null)
            {
                return NotFound();
            }

            return fondoPension;
        }

        // PUT: api/fondoPensions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutfondoPension(long id, fondoPension fondoPension)
        {
            if (id != fondoPension.fPen_Id)
            {
                return BadRequest();
            }

            _context.Entry(fondoPension).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!fondoPensionExists(id))
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

        // POST: api/fondoPensions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<fondoPension>> PostfondoPension(fondoPension fondoPension)
        {
            _context.FondosPensiones.Add(fondoPension);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (fondoPensionExists(fondoPension.fPen_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetfondoPension", new { id = fondoPension.fPen_Id }, fondoPension);
        }

        // DELETE: api/fondoPensions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletefondoPension(long id)
        {
            var fondoPension = await _context.FondosPensiones.FindAsync(id);
            if (fondoPension == null)
            {
                return NotFound();
            }

            _context.FondosPensiones.Remove(fondoPension);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool fondoPensionExists(long id)
        {
            return _context.FondosPensiones.Any(e => e.fPen_Id == id);
        }
    }
}
