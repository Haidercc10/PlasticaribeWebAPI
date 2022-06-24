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
    public class Materia_PrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Materia_PrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Materia_Prima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia_Prima>>> GetMaterias_Primas()
        {
          if (_context.Materias_Primas == null)
          {
              return NotFound();
          }
            return await _context.Materias_Primas.ToListAsync();
        }

        // GET: api/Materia_Prima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia_Prima>> GetMateria_Prima(long id)
        {
          if (_context.Materias_Primas == null)
          {
              return NotFound();
          }
            var materia_Prima = await _context.Materias_Primas.FindAsync(id);

            if (materia_Prima == null)
            {
                return NotFound();
            }

            return materia_Prima;
        }

        // PUT: api/Materia_Prima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria_Prima(long id, Materia_Prima materia_Prima)
        {
            if (id != materia_Prima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(materia_Prima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Materia_PrimaExists(id))
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

        // POST: api/Materia_Prima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materia_Prima>> PostMateria_Prima(Materia_Prima materia_Prima)
        {
          if (_context.Materias_Primas == null)
          {
              return Problem("Entity set 'dataContext.Materias_Primas'  is null.");
          }
            _context.Materias_Primas.Add(materia_Prima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateria_Prima", new { id = materia_Prima.MatPri_Id }, materia_Prima);
        }

        // DELETE: api/Materia_Prima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria_Prima(long id)
        {
            if (_context.Materias_Primas == null)
            {
                return NotFound();
            }
            var materia_Prima = await _context.Materias_Primas.FindAsync(id);
            if (materia_Prima == null)
            {
                return NotFound();
            }

            _context.Materias_Primas.Remove(materia_Prima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Materia_PrimaExists(long id)
        {
            return (_context.Materias_Primas?.Any(e => e.MatPri_Id == id)).GetValueOrDefault();
        }
    }
}
