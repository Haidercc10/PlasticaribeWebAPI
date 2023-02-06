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
    public class DetalleDevolucion_ProductoFacturadoController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleDevolucion_ProductoFacturadoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleDevolucion_ProductoFacturado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleDevolucion_ProductoFacturado>>> GetDetallesDevoluciones_ProductosFacturados()
        {
            return await _context.DetallesDevoluciones_ProductosFacturados.ToListAsync();
        }

        // GET: api/DetalleDevolucion_ProductoFacturado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleDevolucion_ProductoFacturado>> GetDetalleDevolucion_ProductoFacturado(long id)
        {
            var detalleDevolucion_ProductoFacturado = await _context.DetallesDevoluciones_ProductosFacturados.FindAsync(id);

            if (detalleDevolucion_ProductoFacturado == null)
            {
                return NotFound();
            }

            return detalleDevolucion_ProductoFacturado;
        }

        [HttpGet("consultarRollo/{Rollo}")]
        public ActionResult GetRollo(long Rollo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            /*var con = _context.DetallesDevoluciones_ProductosFacturados
                .Where(x => x.Rollo_Id == Rollo)
                .Select(x => new
                {
                    x.DevolucionProdFact.FacturaVta_Id,
                    x.DevolucionProdFact.Cli_Id,
                    x.DevolucionProdFact.Cliente.Cli_Nombre,
                    x.Prod.Prod_Nombre,
                    x.Prod_Id,
                    x.Rollo_Id,
                    x.DtDevProdFact_Cantidad,
                    x.UndMed_Id,
                })
                .ToList();*/

            var con = from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      from fac in _context.Set<DetallesAsignacionProducto_FacturaVenta>()
                      where rollo.Rollo_Id == Rollo
                            && fac.Rollo_Id == Rollo
                      select new
                      {
                          fac.AsigProducto_FV.FacturaVta_Id,
                          fac.AsigProducto_FV.Cli_Id,
                          fac.AsigProducto_FV.Cliente.Cli_Nombre,
                          fac.Prod_Id,
                          fac.Prod.Prod_Nombre,
                          fac.Rollo_Id,
                          fac.DtAsigProdFV_Cantidad,
                          fac.UndMed_Id,
                          rollo.Estado_Id
                      }
                      ;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("CrearPdf/{factura}")]
        public ActionResult GetCrearPdf(string factura)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dtDev in _context.Set<DetalleDevolucion_ProductoFacturado>()
                      from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      from emp in _context.Set<Empresa>()
                      where dtDev.DevolucionProdFact.FacturaVta_Id == factura
                            && rollo.Rollo_Id == dtDev.Rollo_Id
                            && emp.Empresa_Id == 800188732
                      select new
                      {
                          dtDev.DevProdFact_Id,
                          dtDev.Prod_Id,
                          dtDev.Prod.Prod_Nombre,
                          dtDev.DevolucionProdFact.Cli_Id,
                          dtDev.DevolucionProdFact.Cliente.Cli_Nombre,
                          dtDev.DevolucionProdFact.Cliente.Cli_Email,
                          dtDev.DevolucionProdFact.Cliente.Cli_Telefono,
                          dtDev.DevolucionProdFact.Cliente.TPCli.TPCli_Nombre,
                          dtDev.DevolucionProdFact.Cliente.TipoIdentificacion.TipoIdentificacion_Nombre,
                          dtDev.Rollo_Id,
                          dtDev.UndMed_Id,
                          dtDev.DtDevProdFact_Cantidad,
                          dtDev.DevolucionProdFact.DevProdFact_Fecha,
                          Creador = dtDev.DevolucionProdFact.Usua_Id,
                          NombreCreador = dtDev.DevolucionProdFact.Usua.Usua_Nombre,
                          dtDev.DevolucionProdFact.DevProdFact_Observacion,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // PUT: api/DetalleDevolucion_ProductoFacturado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleDevolucion_ProductoFacturado(long id, DetalleDevolucion_ProductoFacturado detalleDevolucion_ProductoFacturado)
        {
            if (id != detalleDevolucion_ProductoFacturado.DevProdFact_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleDevolucion_ProductoFacturado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleDevolucion_ProductoFacturadoExists(id))
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

        // POST: api/DetalleDevolucion_ProductoFacturado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleDevolucion_ProductoFacturado>> PostDetalleDevolucion_ProductoFacturado(DetalleDevolucion_ProductoFacturado detalleDevolucion_ProductoFacturado)
        {
            _context.DetallesDevoluciones_ProductosFacturados.Add(detalleDevolucion_ProductoFacturado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleDevolucion_ProductoFacturadoExists(detalleDevolucion_ProductoFacturado.DevProdFact_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleDevolucion_ProductoFacturado", new { id = detalleDevolucion_ProductoFacturado.DevProdFact_Id }, detalleDevolucion_ProductoFacturado);
        }

        // DELETE: api/DetalleDevolucion_ProductoFacturado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleDevolucion_ProductoFacturado(long id)
        {
            var detalleDevolucion_ProductoFacturado = await _context.DetallesDevoluciones_ProductosFacturados.FindAsync(id);
            if (detalleDevolucion_ProductoFacturado == null)
            {
                return NotFound();
            }

            _context.DetallesDevoluciones_ProductosFacturados.Remove(detalleDevolucion_ProductoFacturado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleDevolucion_ProductoFacturadoExists(long id)
        {
            return _context.DetallesDevoluciones_ProductosFacturados.Any(e => e.DevProdFact_Id == id);
        }
    }
}
