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
    public class Remision_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Remision_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Remision_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remision_MateriaPrima>>> GetRemisiones_MateriasPrimas()
        {
          if (_context.Remisiones_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.Remisiones_MateriasPrimas.ToListAsync();
        }

        // GET: api/Remision_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remision_MateriaPrima>> GetRemision_MateriaPrima(int id)
        {
          if (_context.Remisiones_MateriasPrimas == null)
          {
              return NotFound();
          }
            var remision_MateriaPrima = await _context.Remisiones_MateriasPrimas.FindAsync(id);

            if (remision_MateriaPrima == null)
            {
                return NotFound();
            }

            return remision_MateriaPrima;
        }

        //Consulta por el id de la remision
        [HttpGet("remision/{Rem_Id}")]
        public ActionResult RemisionId(long Rem_Id)
        {
            var remCompra = _context.Remisiones_MateriasPrimas.Where(f => f.Rem_Id == Rem_Id).ToList();

            return Ok(remCompra);
        }

        //consulta por el Id de la materia prima
        [HttpGet("MP/{MatPri_Id}")]
        public ActionResult MateriaPrimaId(long MatPri_Id)
        {
            var remCompra = _context.Remisiones_MateriasPrimas.Where(f => f.MatPri_Id == MatPri_Id).ToList();

            return Ok(remCompra);
        }

        // PUT: api/Remision_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemision_MateriaPrima(int id, Remision_MateriaPrima remision_MateriaPrima)
        {
            if (id != remision_MateriaPrima.Rem_Id)
            {
                return BadRequest();
            }

            _context.Entry(remision_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Remision_MateriaPrimaExists(id))
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

        // POST: api/Remision_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Remision_MateriaPrima>> PostRemision_MateriaPrima(Remision_MateriaPrima remision_MateriaPrima)
        {
          if (_context.Remisiones_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.Remisiones_MateriasPrimas'  is null.");
          }
            _context.Remisiones_MateriasPrimas.Add(remision_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Remision_MateriaPrimaExists(remision_MateriaPrima.Rem_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRemision_MateriaPrima", new { id = remision_MateriaPrima.Rem_Id }, remision_MateriaPrima);
        }

        // DELETE: api/Remision_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemision_MateriaPrima(int id)
        {
            if (_context.Remisiones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var remision_MateriaPrima = await _context.Remisiones_MateriasPrimas.FindAsync(id);
            if (remision_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.Remisiones_MateriasPrimas.Remove(remision_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Remision_MateriaPrimaExists(int id)
        {
            return (_context.Remisiones_MateriasPrimas?.Any(e => e.Rem_Id == id)).GetValueOrDefault();
        }
    }
}
