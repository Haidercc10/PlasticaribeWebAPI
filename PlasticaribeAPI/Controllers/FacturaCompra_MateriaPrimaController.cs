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
    public class FacturaCompra_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public FacturaCompra_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/FacturaCompra_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaCompra_MateriaPrima>>> GetFacturasCompras_MateriaPrimas()
        {
          if (_context.FacturasCompras_MateriaPrimas == null)
          {
              return NotFound();
          }
            return await _context.FacturasCompras_MateriaPrimas.ToListAsync();
        }

        // GET: api/FacturaCompra_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaCompra_MateriaPrima>> GetFacturaCompra_MateriaPrima(long Facco_Id, long MatPri_Id)
        {
            if (_context.FacturasCompras_MateriaPrimas == null)
            {
                return NotFound();
            }
            var facturaCompra_MateriaPrima = await _context.FacturasCompras_MateriaPrimas.FindAsync(Facco_Id, MatPri_Id);

            if (facturaCompra_MateriaPrima == null)
            {
                return NotFound();
            }

            return facturaCompra_MateriaPrima;
        }

        //Consulta por el Id de la factura
        [HttpGet("facturaCompra/{Facco_Id}")]
        public ActionResult FacturaId(long Facco_Id)
        {
            var factCompra = _context.FacturasCompras_MateriaPrimas.Where(f => f.Facco_Id == Facco_Id).ToList();

            return Ok(factCompra);
        }

        //Consulta por el id de la materia prima
        [HttpGet("MP/{MatPri_Id}")]
        public ActionResult materiaPrimaId(long MatPri_Id)
        {
            var factCompra = _context.FacturasCompras_MateriaPrimas.Where(f => f.MatPri_Id == MatPri_Id).ToList();

            return Ok(factCompra);
        }

        // PUT: api/FacturaCompra_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturaCompra_MateriaPrima(long id, FacturaCompra_MateriaPrima facturaCompra_MateriaPrima)
        {
            if (id != facturaCompra_MateriaPrima.Facco_Id)
            {
                return BadRequest();
            }

            _context.Entry(facturaCompra_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaCompra_MateriaPrimaExists(id))
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

        // POST: api/FacturaCompra_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FacturaCompra_MateriaPrima>> PostFacturaCompra_MateriaPrima(FacturaCompra_MateriaPrima facturaCompra_MateriaPrima)
        {
          if (_context.FacturasCompras_MateriaPrimas == null)
          {
              return Problem("Entity set 'dataContext.FacturasCompras_MateriaPrimas'  is null.");
          }
            _context.FacturasCompras_MateriaPrimas.Add(facturaCompra_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FacturaCompra_MateriaPrimaExists(facturaCompra_MateriaPrima.Facco_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFacturaCompra_MateriaPrima", new { id = facturaCompra_MateriaPrima.Facco_Id }, facturaCompra_MateriaPrima);
        }

        // DELETE: api/FacturaCompra_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturaCompra_MateriaPrima(long id)
        {
            if (_context.FacturasCompras_MateriaPrimas == null)
            {
                return NotFound();
            }
            var facturaCompra_MateriaPrima = await _context.FacturasCompras_MateriaPrimas.FindAsync(id);
            if (facturaCompra_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.FacturasCompras_MateriaPrimas.Remove(facturaCompra_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaCompra_MateriaPrimaExists(long id)
        {
            return (_context.FacturasCompras_MateriaPrimas?.Any(e => e.Facco_Id == id)).GetValueOrDefault();
        }
    }
}
