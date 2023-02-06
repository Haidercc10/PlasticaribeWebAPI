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
    public class Remision_FacturaCompraController : ControllerBase
    {
        private readonly dataContext _context;

        public Remision_FacturaCompraController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Remision_FacturaCompra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remision_FacturaCompra>>> GetRemisiones_FacturasCompras()
        {
          if (_context.Remisiones_FacturasCompras == null)
          {
              return NotFound();
          }
            return await _context.Remisiones_FacturasCompras.ToListAsync();
        }

        // GET: api/Remision_FacturaCompra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remision_FacturaCompra>> GetRemision_FacturaCompra(long Facco_Id, int Rem_Id)
        {
          if (_context.Remisiones_FacturasCompras == null)
          {
              return NotFound();
          }
            var remision_FacturaCompra = await _context.Remisiones_FacturasCompras.FindAsync(Facco_Id, Rem_Id);

            if (remision_FacturaCompra == null)
            {
                return NotFound();
            }

            return remision_FacturaCompra;
        }

        //Consulta por el Id de la Factura
        [HttpGet("F/{Facco_Id}")]
        public ActionResult<Remision_FacturaCompra> GetRemision_Factura(long Facco_Id)
        {
            var remision_FacturaCompra = _context.Remisiones_FacturasCompras.Where(remFac => remFac.Facco_Id == Facco_Id).ToList();

            if (remision_FacturaCompra == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(remision_FacturaCompra);
            }
        }

        // PUT: api/Remision_FacturaCompra/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemision_FacturaCompra(long id, Remision_FacturaCompra remision_FacturaCompra)
        {
            if (id != remision_FacturaCompra.Facco_Id)
            {
                return BadRequest();
            }

            _context.Entry(remision_FacturaCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Remision_FacturaCompraExists(id))
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

        // POST: api/Remision_FacturaCompra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Remision_FacturaCompra>> PostRemision_FacturaCompra(Remision_FacturaCompra remision_FacturaCompra)
        {
          if (_context.Remisiones_FacturasCompras == null)
          {
              return Problem("Entity set 'dataContext.Remisiones_FacturasCompras'  is null.");
          }
            _context.Remisiones_FacturasCompras.Add(remision_FacturaCompra);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Remision_FacturaCompraExists(remision_FacturaCompra.Facco_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRemision_FacturaCompra", new { id = remision_FacturaCompra.Facco_Id }, remision_FacturaCompra);
        }

        // DELETE: api/Remision_FacturaCompra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemision_FacturaCompra(long id)
        {
            if (_context.Remisiones_FacturasCompras == null)
            {
                return NotFound();
            }
            var remision_FacturaCompra = await _context.Remisiones_FacturasCompras.FindAsync(id);
            if (remision_FacturaCompra == null)
            {
                return NotFound();
            }

            _context.Remisiones_FacturasCompras.Remove(remision_FacturaCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Remision_FacturaCompraExists(long id)
        {
            return (_context.Remisiones_FacturasCompras?.Any(e => e.Facco_Id == id)).GetValueOrDefault();
        }
    }
}
