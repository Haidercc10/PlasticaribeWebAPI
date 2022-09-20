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
    public class DetallesAsignacionProducto_FacturaVentaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetallesAsignacionProducto_FacturaVentaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetallesAsignacionProducto_FacturaVenta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallesAsignacionProducto_FacturaVenta>>> GetDetallesAsignacionesProductos_FacturasVentas()
        {
            return await _context.DetallesAsignacionesProductos_FacturasVentas.ToListAsync();
        }

        // GET: api/DetallesAsignacionProducto_FacturaVenta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallesAsignacionProducto_FacturaVenta>> GetDetallesAsignacionProducto_FacturaVenta(long id)
        {
            var detallesAsignacionProducto_FacturaVenta = await _context.DetallesAsignacionesProductos_FacturasVentas.FindAsync(id);

            if (detallesAsignacionProducto_FacturaVenta == null)
            {
                return NotFound();
            }

            return detallesAsignacionProducto_FacturaVenta;
        }

        [HttpGet("CodigoFactura/{cod}")]
        public ActionResult Get(string cod)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ent in _context.Set<DetalleEntradaRollo_Producto>()
                      from dtAFac in _context.Set<DetallesAsignacionProducto_FacturaVenta>()
                      where dtAFac.AsigProducto_FV.FacturaVta_Id == cod
                            && ent.Rollo_Id == dtAFac.Rollo_Id
                      select new
                      {
                          dtAFac.AsigProducto_FV.FacturaVta_Id,
                          dtAFac.AsigProdFV_Id,
                          dtAFac.Prod_Id,
                          dtAFac.Prod.Prod_Nombre,
                          dtAFac.DtAsigProdFV_Cantidad,
                          dtAFac.Rollo_Id,
                          dtAFac.UndMed_Id,
                          dtAFac.AsigProducto_FV.Cliente.Cli_Nombre,
                          dtAFac.AsigProducto_FV.Cli_Id,
                          ent.Estado_Id
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // PUT: api/DetallesAsignacionProducto_FacturaVenta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallesAsignacionProducto_FacturaVenta(long id, DetallesAsignacionProducto_FacturaVenta detallesAsignacionProducto_FacturaVenta)
        {
            if (id != detallesAsignacionProducto_FacturaVenta.AsigProdFV_Id)
            {
                return BadRequest();
            }

            _context.Entry(detallesAsignacionProducto_FacturaVenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesAsignacionProducto_FacturaVentaExists(id))
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

        // POST: api/DetallesAsignacionProducto_FacturaVenta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallesAsignacionProducto_FacturaVenta>> PostDetallesAsignacionProducto_FacturaVenta(DetallesAsignacionProducto_FacturaVenta detallesAsignacionProducto_FacturaVenta)
        {
            _context.DetallesAsignacionesProductos_FacturasVentas.Add(detallesAsignacionProducto_FacturaVenta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetallesAsignacionProducto_FacturaVentaExists(detallesAsignacionProducto_FacturaVenta.AsigProdFV_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetallesAsignacionProducto_FacturaVenta", new { id = detallesAsignacionProducto_FacturaVenta.AsigProdFV_Id }, detallesAsignacionProducto_FacturaVenta);
        }

        // DELETE: api/DetallesAsignacionProducto_FacturaVenta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallesAsignacionProducto_FacturaVenta(long id)
        {
            var detallesAsignacionProducto_FacturaVenta = await _context.DetallesAsignacionesProductos_FacturasVentas.FindAsync(id);
            if (detallesAsignacionProducto_FacturaVenta == null)
            {
                return NotFound();
            }

            _context.DetallesAsignacionesProductos_FacturasVentas.Remove(detallesAsignacionProducto_FacturaVenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallesAsignacionProducto_FacturaVentaExists(long id)
        {
            return _context.DetallesAsignacionesProductos_FacturasVentas.Any(e => e.AsigProdFV_Id == id);
        }
    }
}
