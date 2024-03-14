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

        /************************************************ CONSULTAS PARA ENTRADAS DE MATERIAS PRIMAS ***************************************************/
        [HttpGet("GetEntradaFacRem_Fechas/{fecha1}/{fecha2}")]
        public ActionResult GetEntradaFacRem_Fechas(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var conFac = _context.Facturas_Compras.Where(x => x.Facco_FechaFactura >= fecha1 && x.Facco_FechaFactura <= fecha2).
                Select(fac => new
                {
                    Id = Convert.ToInt64(fac.Facco_Id),
                    Codigo = fac.Facco_Codigo,
                    Fecha = fac.Facco_FechaFactura,
                    Valor = Convert.ToDecimal(fac.Facco_ValorTotal),
                    Observacion = fac.Facco_Observacion,
                    UsuarioId = fac.Usua_Id,
                    Usuario = fac.Usua.Usua_Nombre,
                    Proveedor_Id = fac.Prov_Id,
                    Proveedor = fac.Prov.Prov_Nombre,
                    Tipo_Doc = "FCO",
                    Nombre_Doc = "Factura",
                }).ToList();

            var conRem = _context.Remisiones.Where(x => x.Rem_Fecha >= fecha1 && x.Rem_Fecha <= fecha2).
                Select(rem => new
                {
                    Id = Convert.ToInt64(rem.Rem_Id),
                    Codigo = rem.Rem_Codigo,
                    Fecha = rem.Rem_Fecha,
                    Valor = Convert.ToDecimal(rem.Rem_PrecioEstimado),
                    Observacion = rem.Rem_Observacion,
                    UsuarioId = rem.Usua_Id,
                    Usuario = rem.Usua.Usua_Nombre,
                    Proveedor_Id = rem.Prov_Id,
                    Proveedor = rem.Prov.Prov_Nombre,
                    Tipo_Doc = "REM",
                    Nombre_Doc = "Remision",
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(conFac.Concat(conRem));
        }

        [HttpGet("GetEntradaFacRem_Codigo/{codigo}")]
        public ActionResult GetEntradaFacRem_Codigo(string codigo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var conFac = _context.Facturas_Compras.Where(x => x.Facco_Codigo == codigo).
                Select(fac => new
                {
                    Id = Convert.ToInt64(fac.Facco_Id),
                    Codigo = fac.Facco_Codigo,
                    Fecha = fac.Facco_FechaFactura,
                    Valor = Convert.ToDecimal(fac.Facco_ValorTotal),
                    Observacion = fac.Facco_Observacion,
                    UsuarioId = fac.Usua_Id,
                    Usuario = fac.Usua.Usua_Nombre,
                    Proveedor_Id = fac.Prov_Id,
                    Proveedor = fac.Prov.Prov_Nombre,
                    Tipo_Doc = "FCO",
                    Nombre_Doc = "Factura",
                }).ToList();

            var conRem = _context.Remisiones.Where(x => x.Rem_Codigo == codigo).
                Select(rem => new
                {
                    Id = Convert.ToInt64(rem.Rem_Id),
                    Codigo = rem.Rem_Codigo,
                    Fecha = rem.Rem_Fecha,
                    Valor = Convert.ToDecimal(rem.Rem_PrecioEstimado),
                    Observacion = rem.Rem_Observacion,
                    UsuarioId = rem.Usua_Id,
                    Usuario = rem.Usua.Usua_Nombre,
                    Proveedor_Id = rem.Prov_Id,
                    Proveedor = rem.Prov.Prov_Nombre,
                    Tipo_Doc = "REM",
                    Nombre_Doc = "Remision",
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(conFac.Concat(conRem));
        }

        [HttpGet("GetEntradaFacRem_Proveedor/{proveedor}")]
        public ActionResult GetEntradaFacRem_Proveedor(long proveedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var conFac = _context.Facturas_Compras.Where(x => x.Prov_Id == proveedor).
                Select(fac => new
                {
                    Id = Convert.ToInt64(fac.Facco_Id),
                    Codigo = fac.Facco_Codigo,
                    Fecha = fac.Facco_FechaFactura,
                    Valor = Convert.ToDecimal(fac.Facco_ValorTotal),
                    Observacion = fac.Facco_Observacion,
                    UsuarioId = fac.Usua_Id,
                    Usuario = fac.Usua.Usua_Nombre,
                    Proveedor_Id = fac.Prov_Id,
                    Proveedor = fac.Prov.Prov_Nombre,
                    Tipo_Doc = "FCO",
                    Nombre_Doc = "Factura",
                }).ToList();

            var conRem = _context.Remisiones.Where(x => x.Prov_Id == proveedor).
                Select(rem => new
                {
                    Id = Convert.ToInt64(rem.Rem_Id),
                    Codigo = rem.Rem_Codigo,
                    Fecha = rem.Rem_Fecha,
                    Valor = Convert.ToDecimal(rem.Rem_PrecioEstimado),
                    Observacion = rem.Rem_Observacion,
                    UsuarioId = rem.Usua_Id,
                    Usuario = rem.Usua.Usua_Nombre,
                    Proveedor_Id = rem.Prov_Id,
                    Proveedor = rem.Prov.Prov_Nombre,
                    Tipo_Doc = "REM",
                    Nombre_Doc = "Remision",
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(conFac.Concat(conRem));
        }

        [HttpGet("GetEntradaFacRem_FechasTipoDocProveedor/{fecha1}/{fecha2}/{tipoDoc}/{proveedor}")]
        public ActionResult GetEntradaFacRem_Proveedor(DateTime fecha1, DateTime fecha2, string tipoDoc, long proveedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var conFac = _context.Facturas_Compras.Where(fac => fac.Prov_Id == proveedor
                               && fac.Facco_FechaFactura >= fecha1
                               && fac.Facco_FechaFactura <= fecha2
                               && tipoDoc == "FCO").
                Select(fac => new
                {
                    Id = Convert.ToInt64(fac.Facco_Id),
                    Codigo = fac.Facco_Codigo,
                    Fecha = fac.Facco_FechaFactura,
                    Valor = Convert.ToDecimal(fac.Facco_ValorTotal),
                    Observacion = fac.Facco_Observacion,
                    UsuarioId = fac.Usua_Id,
                    Usuario = fac.Usua.Usua_Nombre,
                    Proveedor_Id = fac.Prov_Id,
                    Proveedor = fac.Prov.Prov_Nombre,
                    Tipo_Doc = "FCO",
                    Nombre_Doc = "Factura",
                }).ToList();

            var conRem = _context.Remisiones.Where(rem => rem.Prov_Id == proveedor
                               && rem.Rem_Fecha >= fecha1
                               && rem.Rem_Fecha <= fecha2
                               && tipoDoc == "REM").
                Select(rem => new
                {
                    Id = Convert.ToInt64(rem.Rem_Id),
                    Codigo = rem.Rem_Codigo,
                    Fecha = rem.Rem_Fecha,
                    Valor = Convert.ToDecimal(rem.Rem_PrecioEstimado),
                    Observacion = rem.Rem_Observacion,
                    UsuarioId = rem.Usua_Id,
                    Usuario = rem.Usua.Usua_Nombre,
                    Proveedor_Id = rem.Prov_Id,
                    Proveedor = rem.Prov.Prov_Nombre,
                    Tipo_Doc = "REM",
                    Nombre_Doc = "Remision",
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(conFac.Concat(conRem));
        }

        [HttpGet("pdfMovimientos/{codigo}")]
        public ActionResult Get(string codigo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.FacturasCompras_MateriaPrimas
                .Where(fac => fac.Facco.Facco_Codigo == codigo)
                .Include(fac => fac.Facco)
                .Select(fac => new
                {
                    fac.Facco.Facco_Id,
                    fac.Facco.Facco_FechaFactura,
                    fac.Facco.Facco_Observacion,
                    fac.Facco.Facco_ValorTotal,
                    fac.Facco.Prov_Id,
                    fac.Facco.Prov.TipoIdentificacion_Id,
                    fac.Facco.Prov.Prov_Nombre,
                    fac.Facco.Prov.TpProv.TpProv_Nombre,
                    fac.Facco.Prov.Prov_Telefono,
                    fac.Facco.Prov.Prov_Email,
                    fac.Facco.Prov.Prov_Ciudad,
                    fac.Facco.Usua_Id,
                    fac.Facco.Usua.Usua_Nombre,
                    fac.MatPri_Id,
                    fac.MatPri.MatPri_Nombre,
                    fac.UndMed_Id,
                    fac.FaccoMatPri_Cantidad,
                    fac.FaccoMatPri_ValorUnitario
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
