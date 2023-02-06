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
    public class TratadosController : ControllerBase
    {
        private readonly dataContext _context;

        public TratadosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tratadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tratado>>> GetTratado()
        {
          if (_context.Tratado == null)
          {
              return NotFound();
          }
            return await _context.Tratado.ToListAsync();
        }

        // GET: api/Tratadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tratado>> GetTratado(int id)
        {
          if (_context.Tratado == null)
          {
              return NotFound();
          }
            var tratado = await _context.Tratado.FindAsync(id);

            if (tratado == null)
            {
                return NotFound();
            }

            return tratado;
        }

        // PUT: api/Tratadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratado(int id, Tratado tratado)
        {
            if (id != tratado.Tratado_Id)
            {
                return BadRequest();
            }

            _context.Entry(tratado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TratadoExists(id))
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

        // POST: api/Tratadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tratado>> PostTratado(Tratado tratado)
        {
          if (_context.Tratado == null)
          {
              return Problem("Entity set 'dataContext.Tratado'  is null.");
          }
            _context.Tratado.Add(tratado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTratado", new { id = tratado.Tratado_Id }, tratado);
        }

        // DELETE: api/Tratadoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratado(int id)
        {
            if (_context.Tratado == null)
            {
                return NotFound();
            }
            var tratado = await _context.Tratado.FindAsync(id);
            if (tratado == null)
            {
                return NotFound();
            }

            _context.Tratado.Remove(tratado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TratadoExists(int id)
        {
            return (_context.Tratado?.Any(e => e.Tratado_Id == id)).GetValueOrDefault();
        }
    }
}
