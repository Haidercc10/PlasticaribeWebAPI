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

            var FacCompras = _context.OrdenesCompras_FacturasCompras.Where(o => o.Oc_Id == OC).Select(of => new { of.Facco_Id }).ToList();


            if (FacCompras == null)
            {
                return NotFound();
            }

            return Ok(FacCompras);

        }

        /* Consulta facturas asociadas a OC, Luego carga las Mat. Primas que están en dichas facturas. Forma 1 */
        [HttpGet("FacturasAsociadasAOC/{OC}")]
        public ActionResult GetFactura(long OC)
        {
            /* Selecciona las facturas de OrdenesCompras_FacturasCompras. */
            var Facturas = _context.OrdenesCompras_FacturasCompras.Where(o => o.Oc_Id == OC).Select(of => of.Facco_Id);
            var Ordenes = _context.Detalles_OrdenesCompras.Where(o => o.Oc_Id == OC).Select(of => of.MatPri_Id);
            var Ordenes2 = _context.Detalles_OrdenesCompras.Where(o => o.Oc_Id == OC).Select(of => of.Tinta_Id);
            var Ordenes3 = _context.Detalles_OrdenesCompras.Where(o => o.Oc_Id == OC).Select(of => of.BOPP_Id);

            /* Selecciona las mat. primas y tintas de facturas compras materias primas. */
            var FacCompras = _context.FacturasCompras_MateriaPrimas.Where(f => Facturas.Contains(f.Facco_Id) &&
                                                                          Ordenes.Contains(f.MatPri_Id) &&
                                                                          Ordenes2.Contains(f.Tinta_Id) &&
                                                                          Ordenes3.Contains(f.Bopp_Id)
                                                                          ).GroupBy(agr => new
                                                                          {
                                                                              agr.MatPri_Id,
                                                                              agr.Tinta_Id,
                                                                              agr.UndMed_Id,
                                                                              agr.Bopp_Id
                                                                          }).Select(fco => new
                                                                          {
                                                                              fco.Key.MatPri_Id,
                                                                              fco.Key.Tinta_Id,
                                                                              fco.Key.Bopp_Id,
                                                                              Suma = fco.Sum(a => a.FaccoMatPri_Cantidad),
                                                                              fco.Key.UndMed_Id
                                                                          }).ToList();
            if (FacCompras == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(FacCompras);
            }
        }

        /* Consulta facturas asociadas a OC, Luego carga las Mat. Primas que están en dichas facturas. Forma 2  */
        [HttpGet("FacturasComprasAsociadasAOC/{OC}")]
        public ActionResult GetFactura2(long OC)
        {
            var facxOrden = from ocfc2 in _context.Set<OrdenesCompras_FacturasCompras>()
                            where ocfc2.Oc_Id == OC
                            select ocfc2.Facco_Id;

            var FacCompras = from ocfc in _context.Set<OrdenesCompras_FacturasCompras>()
                             from fac in _context.Set<FacturaCompra_MateriaPrima>()
                             from doc in _context.Set<Detalle_OrdenCompra>()
                             where facxOrden.Contains(ocfc.Facco_Id) &&
                             doc.MatPri_Id == fac.MatPri_Id &&
                             doc.Tinta_Id == fac.Tinta_Id &&
                             doc.BOPP_Id == fac.Bopp_Id &&
                             ocfc.Facco_Id == fac.Facco_Id &&
                             ocfc.Oc_Id == doc.Oc_Id
                             group fac by new { fac.MatPri_Id, fac.Tinta_Id, fac.UndMed_Id, fac.Bopp_Id } into fc
                             select new
                             {
                                 fc.Key.MatPri_Id,
                                 fc.Key.Tinta_Id,
                                 fc.Key.Bopp_Id,
                                 Suma = fc.Sum(a => a.FaccoMatPri_Cantidad),
                                 fc.Key.UndMed_Id
                             };


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
                if (OrdenesCompras_FacturasComprasExists(ordenesCompras_FacturasCompras.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrdenesCompras_FacturasCompras", new { id = ordenesCompras_FacturasCompras.Codigo }, ordenesCompras_FacturasCompras);
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
            return _context.OrdenesCompras_FacturasCompras.Any(e => e.Codigo == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
