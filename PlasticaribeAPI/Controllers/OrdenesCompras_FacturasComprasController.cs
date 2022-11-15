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
    public class OrdenesCompras_FacturasComprasController : ControllerBase
    {
        private readonly dataContext _context;

        public OrdenesCompras_FacturasComprasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/OrdenesCompras_FacturasCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenesCompras_FacturasCompras>>> GetOrdenesCompras_FacturasCompras()
        {
            return await _context.OrdenesCompras_FacturasCompras.ToListAsync();
        }

        // GET: api/OrdenesCompras_FacturasCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenesCompras_FacturasCompras>> GetOrdenesCompras_FacturasCompras(long id)
        {
            var ordenesCompras_FacturasCompras = await _context.OrdenesCompras_FacturasCompras.FindAsync(id);

            if (ordenesCompras_FacturasCompras == null)
            {
                return NotFound();
            }

            return ordenesCompras_FacturasCompras;
        }


        [HttpGet("InfoOrdenCompraxFactura/{OC}")]
        public ActionResult GetFacturaxOC(long OC)
        {

            var FacCompras = _context.OrdenesCompras_FacturasCompras.Where(o => o.Oc_Id == OC).Select(of => new { of.Facco_Id });


            if (FacCompras == null)
            {
                return NotFound();
            }

            return Ok(FacCompras);

        }

        // PUT: api/OrdenesCompras_FacturasCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenesCompras_FacturasCompras(long id, OrdenesCompras_FacturasCompras ordenesCompras_FacturasCompras)
        {
            if (id != ordenesCompras_FacturasCompras.Oc_Id)
            {
                return BadRequest();
            }

            _context.Entry(ordenesCompras_FacturasCompras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenesCompras_FacturasComprasExists(id))
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

        // POST: api/OrdenesCompras_FacturasCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenesCompras_FacturasCompras>> PostOrdenesCompras_FacturasCompras(OrdenesCompras_FacturasCompras ordenesCompras_FacturasCompras)
        {
            _context.OrdenesCompras_FacturasCompras.Add(ordenesCompras_FacturasCompras);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrdenesCompras_FacturasComprasExists(ordenesCompras_FacturasCompras.Oc_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrdenesCompras_FacturasCompras", new { id = ordenesCompras_FacturasCompras.Oc_Id }, ordenesCompras_FacturasCompras);
        }

        // DELETE: api/OrdenesCompras_FacturasCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenesCompras_FacturasCompras(long id)
        {
            var ordenesCompras_FacturasCompras = await _context.OrdenesCompras_FacturasCompras.FindAsync(id);
            if (ordenesCompras_FacturasCompras == null)
            {
                return NotFound();
            }

            _context.OrdenesCompras_FacturasCompras.Remove(ordenesCompras_FacturasCompras);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenesCompras_FacturasComprasExists(long id)
        {
            return _context.OrdenesCompras_FacturasCompras.Any(e => e.Oc_Id == id);
        }
    }
}
