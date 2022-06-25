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
    public class Proveedor_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Proveedor_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Provedor_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provedor_MateriaPrima>>> GetProveedores_MateriasPrimas()
        {
          if (_context.Proveedores_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.Proveedores_MateriasPrimas.ToListAsync();
        }

        // GET: api/Provedor_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provedor_MateriaPrima>> GetProvedor_MateriaPrima(long Prov_Id, long MatPri_Id)
        {
          if (_context.Proveedores_MateriasPrimas == null)
          {
              return NotFound();
          }
            var provedor_MateriaPrima = await _context.Proveedores_MateriasPrimas.FindAsync(Prov_Id, MatPri_Id);

            if (provedor_MateriaPrima == null)
            {
                return NotFound();
            }

            return provedor_MateriaPrima;
        }

        // PUT: api/Provedor_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvedor_MateriaPrima(long id, Provedor_MateriaPrima provedor_MateriaPrima)
        {
            if (id != provedor_MateriaPrima.Prov_Id)
            {
                return BadRequest();
            }

            _context.Entry(provedor_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Provedor_MateriaPrimaExists(id))
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

        // POST: api/Provedor_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Provedor_MateriaPrima>> PostProvedor_MateriaPrima(Provedor_MateriaPrima provedor_MateriaPrima)
        {
          if (_context.Proveedores_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.Proveedores_MateriasPrimas'  is null.");
          }
            _context.Proveedores_MateriasPrimas.Add(provedor_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Provedor_MateriaPrimaExists(provedor_MateriaPrima.Prov_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProvedor_MateriaPrima", new { id = provedor_MateriaPrima.Prov_Id }, provedor_MateriaPrima);
        }

        // DELETE: api/Provedor_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvedor_MateriaPrima(long id)
        {
            if (_context.Proveedores_MateriasPrimas == null)
            {
                return NotFound();
            }
            var provedor_MateriaPrima = await _context.Proveedores_MateriasPrimas.FindAsync(id);
            if (provedor_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.Proveedores_MateriasPrimas.Remove(provedor_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Provedor_MateriaPrimaExists(long id)
        {
            return (_context.Proveedores_MateriasPrimas?.Any(e => e.Prov_Id == id)).GetValueOrDefault();
        }
    }
}
