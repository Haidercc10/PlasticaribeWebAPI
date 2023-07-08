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
    public class OrdenMaquila_FacturacionController : ControllerBase
    {
        private readonly dataContext _context;

        public OrdenMaquila_FacturacionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/OrdenMaquila_Facturacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenMaquila_Facturacion>>> GetOrdenMaquila_Facturacion()
        {
          if (_context.OrdenMaquila_Facturacion == null)
          {
              return NotFound();
          }
            return await _context.OrdenMaquila_Facturacion.ToListAsync();
        }

        // GET: api/OrdenMaquila_Facturacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenMaquila_Facturacion>> GetOrdenMaquila_Facturacion(long id)
        {
          if (_context.OrdenMaquila_Facturacion == null)
          {
              return NotFound();
          }
            var ordenMaquila_Facturacion = await _context.OrdenMaquila_Facturacion.FindAsync(id);

            if (ordenMaquila_Facturacion == null)
            {
                return NotFound();
            }

            return ordenMaquila_Facturacion;
        }

        [HttpGet("getOrdenMaquilaFacturada/{id}/{mp}")]
        public ActionResult GetOrdenMaquilaFacturada(long id, int mp)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from fac in _context.Set<DetalleFacturacion_OrdenMaquila>()
                      from dtOm in _context.Set<Detalle_OrdenMaquila>()
                      from omf in _context.Set<OrdenMaquila_Facturacion>()
                      where fac.FacOM_Id == omf.FacOM_Id
                            && dtOm.Orden_Maquila.OM_Id == id
                            && omf.OM_Id == id
                            && (fac.MatPri_Id == mp || fac.Tinta_Id == mp || fac.Bopp_Id == mp)
                            && (dtOm.MatPri_Id == fac.MatPri_Id && dtOm.Tinta_Id == fac.Tinta_Id && dtOm.BOPP_Id == fac.Bopp_Id)
                      group fac by new
                      {
                          // INFORMACION ORDEN DE MAQUILA
                          Tercero = dtOm.Orden_Maquila.Tercero.Tercero_Nombre,
                          Tercero_Id = dtOm.Orden_Maquila.Tercero_Id,
                          Observacion = dtOm.Orden_Maquila.OM_Observacion,
                          MP_Id = dtOm.MatPri_Id,
                          MP = dtOm.MatPrima.MatPri_Nombre,
                          Tinta_Id = dtOm.Tinta_Id,
                          Tinta = dtOm.Tinta.Tinta_Nombre,
                          Bopp_Id = dtOm.BOPP_Id,
                          Bopp = dtOm.BOPP.BOPP_Nombre,
                          Cantidad_Total = dtOm.DtOM_Cantidad,
                          Presentacion = dtOm.UndMed_Id,
                          Precio = dtOm.DtOM_PrecioUnitario,
                      } into fac
                      select new
                      {
                          fac.Key.Tercero,
                          fac.Key.Tercero_Id,
                          fac.Key.Observacion,
                          fac.Key.MP_Id,
                          fac.Key.MP,
                          fac.Key.Tinta_Id,
                          fac.Key.Tinta,
                          fac.Key.Bopp_Id,
                          fac.Key.Bopp,
                          fac.Key.Cantidad_Total,
                          fac.Key.Presentacion,
                          fac.Key.Precio,

                          Cantidad_Facturada = fac.Sum(x => x.DtFacOM_Cantidad),
                          Cantidad_Faltante = (fac.Key.Cantidad_Total - fac.Sum(x => x.DtFacOM_Cantidad))
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (con.Count() == 0)
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                var con2 = from dtOm in _context.Set<Detalle_OrdenMaquila>()
                           where dtOm.Orden_Maquila.OM_Id == id
                                 && (dtOm.MatPri_Id == mp || dtOm.Tinta_Id == mp || dtOm.BOPP_Id == mp)
                           group dtOm by new
                           {
                               // INFORMACION ORDEN DE MAQUILA
                               Tercero = dtOm.Orden_Maquila.Tercero.Tercero_Nombre,
                               Tercero_Id = dtOm.Orden_Maquila.Tercero_Id,
                               Observacion = dtOm.Orden_Maquila.OM_Observacion,
                               MP_Id = dtOm.MatPri_Id,
                               MP = dtOm.MatPrima.MatPri_Nombre,
                               Tinta_Id = dtOm.Tinta_Id,
                               Tinta = dtOm.Tinta.Tinta_Nombre,
                               Bopp_Id = dtOm.BOPP_Id,
                               Bopp = dtOm.BOPP.BOPP_Nombre,
                               Cantidad_Total = dtOm.DtOM_Cantidad,
                               Presentacion = dtOm.UndMed_Id,
                               Precio = dtOm.DtOM_PrecioUnitario,
                           } into fac
                           select new
                           {
                               fac.Key.Tercero,
                               fac.Key.Tercero_Id,
                               fac.Key.Observacion,
                               fac.Key.MP_Id,
                               fac.Key.MP,
                               fac.Key.Tinta_Id,
                               fac.Key.Tinta,
                               fac.Key.Bopp_Id,
                               fac.Key.Bopp,
                               fac.Key.Cantidad_Total,
                               fac.Key.Presentacion,
                               fac.Key.Precio,

                               Cantidad_Facturada = 0,
                               Cantidad_Faltante = (fac.Key.Cantidad_Total),
                               con = con.Count(),
                           };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                return Ok(con2);
            } else return Ok(con);
        }

        // PUT: api/OrdenMaquila_Facturacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenMaquila_Facturacion(long id, OrdenMaquila_Facturacion ordenMaquila_Facturacion)
        {
            if (id != ordenMaquila_Facturacion.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(ordenMaquila_Facturacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenMaquila_FacturacionExists(id))
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

        // POST: api/OrdenMaquila_Facturacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenMaquila_Facturacion>> PostOrdenMaquila_Facturacion(OrdenMaquila_Facturacion ordenMaquila_Facturacion)
        {
          if (_context.OrdenMaquila_Facturacion == null)
          {
              return Problem("Entity set 'dataContext.OrdenMaquila_Facturacion'  is null.");
          }
            _context.OrdenMaquila_Facturacion.Add(ordenMaquila_Facturacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdenMaquila_Facturacion", new { id = ordenMaquila_Facturacion.Codigo }, ordenMaquila_Facturacion);
        }

        // DELETE: api/OrdenMaquila_Facturacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenMaquila_Facturacion(long id)
        {
            if (_context.OrdenMaquila_Facturacion == null)
            {
                return NotFound();
            }
            var ordenMaquila_Facturacion = await _context.OrdenMaquila_Facturacion.FindAsync(id);
            if (ordenMaquila_Facturacion == null)
            {
                return NotFound();
            }

            _context.OrdenMaquila_Facturacion.Remove(ordenMaquila_Facturacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenMaquila_FacturacionExists(long id)
        {
            return (_context.OrdenMaquila_Facturacion?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
