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
    public class Detalles_EntradaTintasController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_EntradaTintasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_EntradaTintas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_EntradaTintas>>> GetDetalles_EntradaTintas()
        {
          if (_context.Detalles_EntradaTintas == null)
          {
              return NotFound();
          }
            return await _context.Detalles_EntradaTintas.ToListAsync();
        }

        // GET: api/Detalles_EntradaTintas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_EntradaTintas>> GetDetalles_EntradaTintas(int id)
        {
          if (_context.Detalles_EntradaTintas == null)
          {
              return NotFound();
          }
            var detalles_EntradaTintas = await _context.Detalles_EntradaTintas.FindAsync(id);

            if (detalles_EntradaTintas == null)
            {
                return NotFound();
            }

            return detalles_EntradaTintas;
        }

        [HttpGet("consultarPorFecha/{entTinta_FechaEntrada}")]
        public ActionResult Get (DateTime entTinta_FechaEntrada)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var tinta = _context.Detalles_EntradaTintas.Where(dtET => dtET.Entrada_Tinta.entTinta_FechaEntrada == entTinta_FechaEntrada).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(tinta);
        }

        [HttpGet("consultarPorFechas/{entTinta_FechaEntrada1}/{entTinta_FechaEntrada2}")]
        public ActionResult Get(DateTime entTinta_FechaEntrada1, DateTime entTinta_FechaEntrada2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var tinta = _context.Detalles_EntradaTintas.Where(dtET => dtET.Entrada_Tinta.entTinta_FechaEntrada >= entTinta_FechaEntrada1 && dtET.Entrada_Tinta.entTinta_FechaEntrada <= entTinta_FechaEntrada2).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(tinta);
        }

        // PUT: api/Detalles_EntradaTintas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_EntradaTintas(int id, Detalles_EntradaTintas detalles_EntradaTintas)
        {
            if (id != detalles_EntradaTintas.EntTinta_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalles_EntradaTintas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_EntradaTintasExists(id))
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

        // POST: api/Detalles_EntradaTintas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_EntradaTintas>> PostDetalles_EntradaTintas(Detalles_EntradaTintas detalles_EntradaTintas)
        {
          if (_context.Detalles_EntradaTintas == null)
          {
              return Problem("Entity set 'dataContext.Detalles_EntradaTintas'  is null.");
          }
            _context.Detalles_EntradaTintas.Add(detalles_EntradaTintas);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Detalles_EntradaTintasExists(detalles_EntradaTintas.EntTinta_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalles_EntradaTintas", new { id = detalles_EntradaTintas.EntTinta_Id }, detalles_EntradaTintas);
        }

        // DELETE: api/Detalles_EntradaTintas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_EntradaTintas(int id)
        {
            if (_context.Detalles_EntradaTintas == null)
            {
                return NotFound();
            }
            var detalles_EntradaTintas = await _context.Detalles_EntradaTintas.FindAsync(id);
            if (detalles_EntradaTintas == null)
            {
                return NotFound();
            }

            _context.Detalles_EntradaTintas.Remove(detalles_EntradaTintas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_EntradaTintasExists(int id)
        {
            return (_context.Detalles_EntradaTintas?.Any(e => e.EntTinta_Id == id)).GetValueOrDefault();
        }
    }
}
