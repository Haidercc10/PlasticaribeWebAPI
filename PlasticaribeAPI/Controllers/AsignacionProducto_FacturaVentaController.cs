using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class AsignacionProducto_FacturaVentaController : ControllerBase
    {
        private readonly dataContext _context;

        public AsignacionProducto_FacturaVentaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/AsignacionProducto_FacturaVenta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsignacionProducto_FacturaVenta>>> GetAsignacionesProductos_FacturasVentas()
        {
            return await _context.AsignacionesProductos_FacturasVentas.ToListAsync();
        }

        // GET: api/AsignacionProducto_FacturaVenta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AsignacionProducto_FacturaVenta>> GetAsignacionProducto_FacturaVenta(long id)
        {
            var asignacionProducto_FacturaVenta = await _context.AsignacionesProductos_FacturasVentas.FindAsync(id);

            if (asignacionProducto_FacturaVenta == null)
            {
                return NotFound();
            }

            return asignacionProducto_FacturaVenta;
        }

        [HttpGet("UltimoId")]
        public ActionResult Get()
        {
            var con = _context.AsignacionesProductos_FacturasVentas.OrderByDescending(x => x.AsigProdFV_Id).First();
            return Ok(con);
        }

        [HttpGet("CodigoFactura/{factura}")]
        public ActionResult Get(string factura)
        {
            var con = _context.AsignacionesProductos_FacturasVentas.Where(x => x.FacturaVta_Id == factura).First();
            return Ok(con);
        }

        // PUT: api/AsignacionProducto_FacturaVenta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacionProducto_FacturaVenta(long id, AsignacionProducto_FacturaVenta asignacionProducto_FacturaVenta)
        {
            if (id != asignacionProducto_FacturaVenta.AsigProdFV_Id)
            {
                return BadRequest();
            }

            _context.Entry(asignacionProducto_FacturaVenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionProducto_FacturaVentaExists(id))
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

        [HttpPut("ActualizacionFactura/{FacturaVta_Id}")]
        public IActionResult Put(string FacturaVta_Id, AsignacionProducto_FacturaVenta asignacionProducto_FacturaVenta)
        {

            if (FacturaVta_Id != asignacionProducto_FacturaVenta.FacturaVta_Id)
            {
                return BadRequest();
            }

            try
            {
                var Actualizado = _context.AsignacionesProductos_FacturasVentas
                    .Where(x => x.FacturaVta_Id == FacturaVta_Id)
                    .First<AsignacionProducto_FacturaVenta>();

                Actualizado.AsigProdFV_FechaEnvio = asignacionProducto_FacturaVenta.AsigProdFV_FechaEnvio;
                Actualizado.Usua_Conductor = asignacionProducto_FacturaVenta.Usua_Conductor;
                Actualizado.AsigProdFV_PlacaCamion = asignacionProducto_FacturaVenta.AsigProdFV_PlacaCamion;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/AsignacionProducto_FacturaVenta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AsignacionProducto_FacturaVenta>> PostAsignacionProducto_FacturaVenta(AsignacionProducto_FacturaVenta asignacionProducto_FacturaVenta)
        {
            _context.AsignacionesProductos_FacturasVentas.Add(asignacionProducto_FacturaVenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacionProducto_FacturaVenta", new { id = asignacionProducto_FacturaVenta.AsigProdFV_Id }, asignacionProducto_FacturaVenta);
        }

        // DELETE: api/AsignacionProducto_FacturaVenta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacionProducto_FacturaVenta(long id)
        {
            var asignacionProducto_FacturaVenta = await _context.AsignacionesProductos_FacturasVentas.FindAsync(id);
            if (asignacionProducto_FacturaVenta == null)
            {
                return NotFound();
            }

            _context.AsignacionesProductos_FacturasVentas.Remove(asignacionProducto_FacturaVenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignacionProducto_FacturaVentaExists(long id)
        {
            return _context.AsignacionesProductos_FacturasVentas.Any(e => e.AsigProdFV_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
