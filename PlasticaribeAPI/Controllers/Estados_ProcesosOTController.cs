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
    public class Estados_ProcesosOTController : ControllerBase
    {
        private readonly dataContext _context;

        public Estados_ProcesosOTController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Estados_ProcesosOT
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estados_ProcesosOT>>> GetEstados_ProcesosOT()
        {
          if (_context.Estados_ProcesosOT == null)
          {
              return NotFound();
          }
            return await _context.Estados_ProcesosOT.ToListAsync();
        }

        // GET: api/Estados_ProcesosOT/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estados_ProcesosOT>> GetEstados_ProcesosOT(long id)
        {
          if (_context.Estados_ProcesosOT == null)
          {
              return NotFound();
          }
            var estados_ProcesosOT = await _context.Estados_ProcesosOT.FindAsync(id);

            if (estados_ProcesosOT == null)
            {
                return NotFound();
            }

            return estados_ProcesosOT;
        }

        // PUT: api/Estados_ProcesosOT/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstados_ProcesosOT(long id, Estados_ProcesosOT estados_ProcesosOT)
        {
            if (id != estados_ProcesosOT.EstProcOT_Id)
            {
                return BadRequest();
            }

            _context.Entry(estados_ProcesosOT).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Estados_ProcesosOTExists(id))
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

        // POST: api/Estados_ProcesosOT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estados_ProcesosOT>> PostEstados_ProcesosOT(Estados_ProcesosOT estados_ProcesosOT)
        {
          if (_context.Estados_ProcesosOT == null)
          {
              return Problem("Entity set 'dataContext.Estados_ProcesosOT'  is null.");
          }
            _context.Estados_ProcesosOT.Add(estados_ProcesosOT);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstados_ProcesosOT", new { id = estados_ProcesosOT.EstProcOT_Id }, estados_ProcesosOT);
        }

        // DELETE: api/Estados_ProcesosOT/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstados_ProcesosOT(long id)
        {
            if (_context.Estados_ProcesosOT == null)
            {
                return NotFound();
            }
            var estados_ProcesosOT = await _context.Estados_ProcesosOT.FindAsync(id);
            if (estados_ProcesosOT == null)
            {
                return NotFound();
            }

            _context.Estados_ProcesosOT.Remove(estados_ProcesosOT);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Estados_ProcesosOTExists(long id)
        {
            return (_context.Estados_ProcesosOT?.Any(e => e.EstProcOT_Id == id)).GetValueOrDefault();
        }
    }
}
