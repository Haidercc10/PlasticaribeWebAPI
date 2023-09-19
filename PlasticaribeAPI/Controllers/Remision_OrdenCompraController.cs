using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Remision_OrdenCompraController : ControllerBase
    {
        private readonly dataContext _context;

        public Remision_OrdenCompraController(dataContext context)
        {
            _context = context;
        }

        // GET: api/OrdenesCompras_FacturasCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remision_OrdenCompra>>> GetRemision_OrdenCompra()
        {
            return await _context.Remision_OrdenCompra.ToListAsync();
        }

        // GET: api/Remision_OrdenCompra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remision_OrdenCompra>> GetRemision_OrdenCompra(long id)
        {
            var Remision_OrdenCompra = await _context.Remision_OrdenCompra.FindAsync(id);

            if (Remision_OrdenCompra == null)
            {
                return NotFound();
            }

            return Remision_OrdenCompra;
        }


        [HttpGet("InfoRemision_OrdenCompra/{OC}")]
        public ActionResult InfoRemision_OrdenCompra(long OC)
        {

            var FacCompras = _context.Remision_OrdenCompra.Where(o => o.Oc_Id == OC).Select(of => new { of.Rem_Id }).ToList();


            if (FacCompras == null)
            {
                return NotFound();
            }

            return Ok(FacCompras);

        }

        /** Consulta facturas asociadas a OC, Luego carga las Mat. Primas que están en dichas facturas. Forma 1 */
        [HttpGet("RemisionesAsociadasAOC/{OC}")]
        public ActionResult GetFactura(long OC)
        {
            /** Selecciona las facturas de OrdenesCompras_FacturasCompras. */
            var Remisiones = _context.Remision_OrdenCompra.Where(o => o.Oc_Id == OC).Select(of => of.Rem_Id);
            var Ordenes = _context.Detalles_OrdenesCompras.Where(o => o.Oc_Id == OC).Select(of => of.MatPri_Id);
            var Ordenes2 = _context.Detalles_OrdenesCompras.Where(o => o.Oc_Id == OC).Select(of => of.Tinta_Id);
            var Ordenes3 = _context.Detalles_OrdenesCompras.Where(o => o.Oc_Id == OC).Select(of => of.BOPP_Id);

            /** Selecciona las mat. primas y tintas de facturas compras materias primas. */
            var FacCompras = _context.Remisiones_MateriasPrimas.Where(f => Remisiones.Contains(f.Rem_Id) &&
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
                                                                              Suma = fco.Sum(a => a.RemiMatPri_Cantidad),
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

        /** Consulta facturas asociadas a OC, Luego carga las Mat. Primas que están en dichas facturas. Forma 2  */
        [HttpGet("RemisionesComprasAsociadasAOC/{OC}")]
        public ActionResult GetFactura2(long OC)
        {
            var facxOrden = from ocfc2 in _context.Set<Remision_OrdenCompra>()
                            where ocfc2.Oc_Id == OC
                            select ocfc2.Rem_Id;

            var FacCompras = from ocfc in _context.Set<Remision_OrdenCompra>()
                             from fac in _context.Set<Remision_MateriaPrima>()
                             from doc in _context.Set<Detalle_OrdenCompra>()
                             where facxOrden.Contains(ocfc.Rem_Id) &&
                             doc.MatPri_Id == fac.MatPri_Id &&
                             doc.Tinta_Id == fac.Tinta_Id &&
                             doc.BOPP_Id == fac.Bopp_Id &&
                             ocfc.Rem_Id == fac.Rem_Id &&
                             ocfc.Oc_Id == doc.Oc_Id
                             group fac by new { fac.MatPri_Id, fac.Tinta_Id, fac.UndMed_Id, fac.Bopp_Id } into fc
                             select new
                             {
                                 fc.Key.MatPri_Id,
                                 fc.Key.Tinta_Id,
                                 fc.Key.Bopp_Id,
                                 Suma = fc.Sum(a => a.RemiMatPri_Cantidad),
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
        public async Task<IActionResult> PutRemision_OrdenCompra(long id, Remision_OrdenCompra Remision_OrdenCompra)
        {
            if (id != Remision_OrdenCompra.Oc_Id)
            {
                return BadRequest();
            }

            _context.Entry(Remision_OrdenCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Remision_OrdenCompraExists(id))
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
        public async Task<ActionResult<Remision_OrdenCompra>> PostOrdenesCompras_FacturasCompras(Remision_OrdenCompra Remision_OrdenCompra)
        {
            _context.Remision_OrdenCompra.Add(Remision_OrdenCompra);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Remision_OrdenCompraExists(Remision_OrdenCompra.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRemision_OrdenCompra", new { id = Remision_OrdenCompra.Codigo }, Remision_OrdenCompra);
        }

        // DELETE: api/OrdenesCompras_FacturasCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemision_OrdenCompra(long id)
        {
            var Remision_OrdenCompra = await _context.Remision_OrdenCompra.FindAsync(id);
            if (Remision_OrdenCompra == null)
            {
                return NotFound();
            }

            _context.Remision_OrdenCompra.Remove(Remision_OrdenCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Remision_OrdenCompraExists(long id)
        {
            return _context.Remision_OrdenCompra.Any(e => e.Codigo == id);
        }
    }
}
