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
    public class Orden_CompraController : ControllerBase
    {
        private readonly dataContext _context;

        public Orden_CompraController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Orden_Compra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden_Compra>>> GetOrdenes_Compras()
        {
            return await _context.Ordenes_Compras.ToListAsync();
        }

        //FUNCION QUE CONSULTARÁ Y RETURNARÁ EL ULTIMO ID CREADO 
        [HttpGet("GetUltimoId")]
        public ActionResult GetUltimoId()
        {
            var con = _context.Ordenes_Compras.OrderBy(x => x.Oc_Id).Select(x => x.Oc_Id).Last();
            return Ok(con);
        }

        // GET: api/Orden_Compra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden_Compra>> GetOrden_Compra(long id)
        {
            var orden_Compra = await _context.Ordenes_Compras.FindAsync(id);

            if (orden_Compra == null)
            {
                return NotFound();
            }

            return orden_Compra;
        }

        [HttpGet("getOrdenCompraFacturada/{id}/{mp}")]
        public ActionResult GetOrdenCompraFacturada(long id, long mp)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var factura = from oc in _context.Set<Detalle_OrdenCompra>()
                          from fac in _context.Set<FacturaCompra_MateriaPrima>()
                          from ocfac in _context.Set<OrdenesCompras_FacturasCompras>()
                          where oc.Oc_Id == id
                                && (oc.MatPri_Id == mp || oc.Tinta_Id == mp || oc.BOPP_Id == mp)
                                && oc.Oc_Id == ocfac.Oc_Id
                                && ocfac.Facco_Id == fac.Facco_Id
                                && (fac.MatPri_Id == mp || fac.Tinta_Id == mp || fac.Bopp_Id == mp)
                          group fac by new
                          {
                              Proveedor = oc.Orden_Compra.Proveedor.Prov_Nombre,
                              Proveedor_Id = oc.Orden_Compra.Prov_Id,
                              Observacion = oc.Orden_Compra.Oc_Observacion,
                              MP_Id = oc.MatPri_Id,
                              MP = oc.MatPrima.MatPri_Nombre,
                              Tinta_Id = oc.Tinta_Id,
                              Tinta = oc.Tinta.Tinta_Nombre,
                              Bopp_Id = oc.BOPP_Id,
                              Bopp = oc.BOPP.BoppGen_Nombre,
                              Cantidad_Total = oc.Doc_CantidadPedida,
                              Presentacion = oc.UndMed_Id,
                              Precio = oc.Doc_PrecioUnitario,
                          } into fac
                          select new
                          {
                              fac.Key.Proveedor,
                              fac.Key.Proveedor_Id,
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
                              Cantidad_Ingresada = fac.Sum(x => x.FaccoMatPri_Cantidad),
                              Cantidad_Faltante = (fac.Key.Cantidad_Total - fac.Sum(x => x.FaccoMatPri_Cantidad)),
                              Tipo = "FACCO"
                          };

            var remision = from oc in _context.Set<Detalle_OrdenCompra>()
                           from rem in _context.Set<Remision_MateriaPrima>()
                           from ocrem in _context.Set<Remision_OrdenCompra>()
                           where oc.Oc_Id == id
                                 && (oc.MatPri_Id == mp || oc.Tinta_Id == mp || oc.BOPP_Id == mp)
                                 && oc.Oc_Id == ocrem.Oc_Id
                                 && ocrem.Rem_Id == rem.Rem_Id
                                && (rem.MatPri_Id == mp || rem.Tinta_Id == mp || rem.Bopp_Id == mp)
                           group rem by new
                           {
                               Proveedor = oc.Orden_Compra.Proveedor.Prov_Nombre,
                               Proveedor_Id = oc.Orden_Compra.Prov_Id,
                               Observacion = oc.Orden_Compra.Oc_Observacion,
                               MP_Id = oc.MatPri_Id,
                               MP = oc.MatPrima.MatPri_Nombre,
                               Tinta_Id = oc.Tinta_Id,
                               Tinta = oc.Tinta.Tinta_Nombre,
                               Bopp_Id = oc.BOPP_Id,
                               Bopp = oc.BOPP.BoppGen_Nombre,
                               Cantidad_Total = oc.Doc_CantidadPedida,
                               Presentacion = oc.UndMed_Id,
                               Precio = oc.Doc_PrecioUnitario,
                           } into oc
                           select new
                           {
                               oc.Key.Proveedor,
                               oc.Key.Proveedor_Id,
                               oc.Key.Observacion,
                               oc.Key.MP_Id,
                               oc.Key.MP,
                               oc.Key.Tinta_Id,
                               oc.Key.Tinta,
                               oc.Key.Bopp_Id,
                               oc.Key.Bopp,
                               oc.Key.Cantidad_Total,
                               oc.Key.Presentacion,
                               oc.Key.Precio,
                               Cantidad_Ingresada = oc.Sum(x => x.RemiMatPri_Cantidad),
                               Cantidad_Faltante = (oc.Key.Cantidad_Total - oc.Sum(x => x.RemiMatPri_Cantidad)),
                               Tipo = "REM"
                           };

            if (factura.Count() > 0 || remision.Count() > 0) return Ok(factura.Concat(remision));
            else
            {
                var con = from oc in _context.Set<Detalle_OrdenCompra>()
                          where oc.Oc_Id == id
                                && (oc.MatPri_Id == mp || oc.Tinta_Id == mp || oc.BOPP_Id == mp)
                          group oc by new
                          {
                              Proveedor = oc.Orden_Compra.Proveedor.Prov_Nombre,
                              Proveedor_Id = oc.Orden_Compra.Prov_Id,
                              Observacion = oc.Orden_Compra.Oc_Observacion,
                              MP_Id = oc.MatPri_Id,
                              MP = oc.MatPrima.MatPri_Nombre,
                              Tinta_Id = oc.Tinta_Id,
                              Tinta = oc.Tinta.Tinta_Nombre,
                              Bopp_Id = oc.BOPP_Id,
                              Bopp = oc.BOPP.BoppGen_Nombre,
                              Cantidad_Total = oc.Doc_CantidadPedida,
                              Presentacion = oc.UndMed_Id,
                              Precio = oc.Doc_PrecioUnitario,
                          } into oc
                          select new
                          {
                              oc.Key.Proveedor,
                              oc.Key.Proveedor_Id,
                              oc.Key.Observacion,
                              oc.Key.MP_Id,
                              oc.Key.MP,
                              oc.Key.Tinta_Id,
                              oc.Key.Tinta,
                              oc.Key.Bopp_Id,
                              oc.Key.Bopp,
                              oc.Key.Cantidad_Total,
                              oc.Key.Presentacion,
                              oc.Key.Precio,
                              Cantidad_Ingresada = 0,
                              Cantidad_Faltante = oc.Key.Cantidad_Total,
                              Tipo = "NA"
                          };
                return Ok(con);
            }
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        // PUT: api/Orden_Compra/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrden_Compra(long id, Orden_Compra orden_Compra)
        {
            if (id != orden_Compra.Oc_Id)
            {
                return BadRequest();
            }

            _context.Entry(orden_Compra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Orden_CompraExists(id))
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

        // POST: api/Orden_Compra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orden_Compra>> PostOrden_Compra(Orden_Compra orden_Compra)
        {
            _context.Ordenes_Compras.Add(orden_Compra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrden_Compra", new { id = orden_Compra.Oc_Id }, orden_Compra);
        }

        // DELETE: api/Orden_Compra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden_Compra(long id)
        {
            var orden_Compra = await _context.Ordenes_Compras.FindAsync(id);
            if (orden_Compra == null)
            {
                return NotFound();
            }

            _context.Ordenes_Compras.Remove(orden_Compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Orden_CompraExists(long id)
        {
            return _context.Ordenes_Compras.Any(e => e.Oc_Id == id);
        }
    }
}
