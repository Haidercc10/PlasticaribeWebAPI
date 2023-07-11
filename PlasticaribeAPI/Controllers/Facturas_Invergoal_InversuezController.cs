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
    public class Facturas_Invergoal_InversuezController : ControllerBase
    {
        private readonly dataContext _context;

        public Facturas_Invergoal_InversuezController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Facturas_Invergoal_Inversuez
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facturas_Invergoal_Inversuez>>> GetFacturas_Invergoal_Inversuez()
        {
          if (_context.Facturas_Invergoal_Inversuez == null)
          {
              return NotFound();
          }
            return await _context.Facturas_Invergoal_Inversuez.ToListAsync();
        }

        // GET: api/Facturas_Invergoal_Inversuez/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facturas_Invergoal_Inversuez>> GetFacturas_Invergoal_Inversuez(int id)
        {
          if (_context.Facturas_Invergoal_Inversuez == null)
          {
              return NotFound();
          }
            var facturas_Invergoal_Inversuez = await _context.Facturas_Invergoal_Inversuez.FindAsync(id);

            if (facturas_Invergoal_Inversuez == null)
            {
                return NotFound();
            }

            return facturas_Invergoal_Inversuez;
        }

        // PUT: api/Facturas_Invergoal_Inversuez/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturas_Invergoal_Inversuez(int id, Facturas_Invergoal_Inversuez facturas_Invergoal_Inversuez)
        {
            if (id != facturas_Invergoal_Inversuez.Id)
            {
                return BadRequest();
            }

            _context.Entry(facturas_Invergoal_Inversuez).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Facturas_Invergoal_InversuezExists(id))
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

        // POST: api/Facturas_Invergoal_Inversuez
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facturas_Invergoal_Inversuez>> PostFacturas_Invergoal_Inversuez(Facturas_Invergoal_Inversuez facturas_Invergoal_Inversuez)
        {
          if (_context.Facturas_Invergoal_Inversuez == null)
          {
              return Problem("Entity set 'dataContext.Facturas_Invergoal_Inversuez'  is null.");
          }
            _context.Facturas_Invergoal_Inversuez.Add(facturas_Invergoal_Inversuez);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacturas_Invergoal_Inversuez", new { id = facturas_Invergoal_Inversuez.Id }, facturas_Invergoal_Inversuez);
        }

        // DELETE: api/Facturas_Invergoal_Inversuez/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturas_Invergoal_Inversuez(int id)
        {
            if (_context.Facturas_Invergoal_Inversuez == null)
            {
                return NotFound();
            }
            var facturas_Invergoal_Inversuez = await _context.Facturas_Invergoal_Inversuez.FindAsync(id);
            if (facturas_Invergoal_Inversuez == null)
            {
                return NotFound();
            }

            _context.Facturas_Invergoal_Inversuez.Remove(facturas_Invergoal_Inversuez);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Facturas_Invergoal_InversuezExists(int id)
        {
            return (_context.Facturas_Invergoal_Inversuez?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
