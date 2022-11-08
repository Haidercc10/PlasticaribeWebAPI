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
                          ent.Estado_Id,
                          dtAFac.AsigProducto_FV.AsigProdFV_PlacaCamion,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("CrearPdf/{factura}")]
        public ActionResult GetCrearPdf(string factura)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dtAsg in _context.Set<DetallesAsignacionProducto_FacturaVenta>()
                      from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      from emp in _context.Set<Empresa>()
                      where dtAsg.AsigProducto_FV.FacturaVta_Id == factura
                            && rollo.Rollo_Id == dtAsg.Rollo_Id
                      select new
                      {
                          dtAsg.AsigProdFV_Id,
                          dtAsg.Prod_Id,
                          dtAsg.Prod.Prod_Nombre,
                          dtAsg.AsigProducto_FV.Cli_Id,
                          dtAsg.AsigProducto_FV.Cliente.Cli_Nombre,
                          dtAsg.AsigProducto_FV.Cliente.Cli_Email,
                          dtAsg.AsigProducto_FV.Cliente.Cli_Telefono,
                          dtAsg.AsigProducto_FV.Cliente.TPCli.TPCli_Nombre,
                          dtAsg.AsigProducto_FV.Cliente.TipoIdentificacion.TipoIdentificacion_Nombre,
                          dtAsg.Rollo_Id,
                          dtAsg.UndMed_Id,
                          dtAsg.DtAsigProdFV_Cantidad,
                          dtAsg.Prod_CantidadUnidades,
                          dtAsg.AsigProducto_FV.AsigProdFV_Fecha,
                          Creador = dtAsg.AsigProducto_FV.Usua_Id,
                          NombreCreador = dtAsg.AsigProducto_FV.Usua.Usua_Nombre,
                          Conductor = dtAsg.AsigProducto_FV.Usua_Conductor,
                          NombreConductor = dtAsg.AsigProducto_FV.Usuario.Usua_Nombre,
                          dtAsg.AsigProducto_FV.AsigProdFV_Observacion,
                          dtAsg.AsigProducto_FV.AsigProdFV_PlacaCamion,
                          dtAsg.AsigProducto_FV.NotaCredito_Id,
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

        [HttpGet("CrearPdf2/{factura}")]
        public ActionResult GetCrearPdf2(string factura)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetallesAsignacionesProductos_FacturasVentas.Where(x => x.AsigProducto_FV.FacturaVta_Id == factura)
                .GroupBy(x => new
                {
                    x.Prod.Prod_Nombre,
                    x.Prod_Id,
                    x.UndMed_Id,
                })
                .Select(x => new
                {
                    x.Key.Prod_Id,
                    x.Key.Prod_Nombre,
                    x.Key.UndMed_Id,
                    Suma = x.Sum(y => y.DtAsigProdFV_Cantidad),
                    SumaUnd = x.Sum(y => y.Prod_CantidadUnidades),
                    cantRollos = x.Count()
                });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("FiltroFechas/{FechaIni}/{FechaFin}")]
        public ActionResult GetFiltroFechas(DateTime FechaIni, DateTime FechaFin)
        {

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var QueryAsignacion = (from fact in _context.Set<AsignacionProducto_FacturaVenta>()
                                   from detfact in _context.Set<DetallesAsignacionProducto_FacturaVenta>()
                                   from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                                   where fact.AsigProdFV_Fecha >= FechaIni
                                         && fact.AsigProdFV_Fecha <= FechaFin
                                         && fact.AsigProdFV_Id == detfact.AsigProdFV_Id
                                         && rollo.Rollo_Id == detfact.Rollo_Id
                                   group fact by new
                                   {
                                       fact.FacturaVta_Id,
                                       fact.AsigProdFV_Fecha,
                                       fact.Usua.Usua_Nombre
                                   } into fact
                                   select new
                                   {
                                       Documento = Convert.ToString(fact.Key.FacturaVta_Id),
                                       Fecha = fact.Key.AsigProdFV_Fecha,
                                       Usuario = fact.Key.Usua_Nombre,
                                       Tipo = Convert.ToString("ASIGPRODFV"),
                                   });

            var QueryEntrada = (from ent in _context.Set<EntradaRollo_Producto>()
                                from dent in _context.Set<DetalleEntradaRollo_Producto>()
                                where ent.EntRolloProd_Fecha >= FechaIni
                                && ent.EntRolloProd_Fecha <= FechaFin
                                && ent.EntRolloProd_Id == dent.EntRolloProd_Id
                                group dent by new
                                {
                                    dent.EntRolloProd_Id,
                                    ent.EntRolloProd_Fecha,
                                    ent.Usua.Usua_Nombre,
                                } into ent
                                select new
                                {
                                    Documento = Convert.ToString(ent.Key.EntRolloProd_Id),
                                    Fecha = ent.Key.EntRolloProd_Fecha,
                                    Usuario = ent.Key.Usua_Nombre,
                                    Tipo = Convert.ToString("ENTROLLO"),
                                });

            var QueryDevolucion = (from dev in _context.Set<DetalleDevolucion_ProductoFacturado>()
                                   from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                                   where dev.DevolucionProdFact.DevProdFact_Fecha >= FechaIni
                                         && dev.DevolucionProdFact.DevProdFact_Fecha <= FechaFin
                                         && dev.Rollo_Id == rollo.Rollo_Id
                                   group dev by new
                                   {
                                       dev.DevolucionProdFact.FacturaVta_Id,
                                       dev.DevolucionProdFact.DevProdFact_Fecha,
                                       dev.DevolucionProdFact.Usua.Usua_Nombre,
                                   } into dev
                                   select new
                                   {
                                       Documento = Convert.ToString(dev.Key.FacturaVta_Id),
                                       Fecha = dev.Key.DevProdFact_Fecha,
                                       Usuario = dev.Key.Usua_Nombre,
                                       Tipo = Convert.ToString("DEVPRODFAC"),
                                   });

            var QueryPreCargue = (from pre in _context.Set<DetallePreEntrega_RolloDespacho>()
                                  where pre.PreEntregaRollo.PreEntRollo_Fecha >= FechaIni
                                        && pre.PreEntregaRollo.PreEntRollo_Fecha <= FechaFin
                                  group pre by new
                                  {
                                     pre.PreEntRollo_Id,
                                     pre.PreEntregaRollo.PreEntRollo_Fecha,
                                     pre.PreEntregaRollo.Usuario.Usua_Nombre,
                                     pre.Proceso.Proceso_Nombre,
                                  } into pre
                                  select new
                                  {
                                      Documento = Convert.ToString(pre.Key.PreEntRollo_Id),
                                      Fecha = pre.Key.PreEntRollo_Fecha,
                                      Usuario = pre.Key.Usua_Nombre,
                                      Tipo = Convert.ToString(pre.Key.Proceso_Nombre),
                                  });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(QueryAsignacion.Concat(QueryEntrada).Concat(QueryDevolucion).Concat(QueryPreCargue));
        }

        [HttpGet("FiltroFactura/{factura}/{ot}")]
        public ActionResult GetFiltroFactura(string factura, string ot)
        {

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var QueryAsignacion = (from fact in _context.Set<AsignacionProducto_FacturaVenta>()
                                   from detfact in _context.Set<DetallesAsignacionProducto_FacturaVenta>()
                                   from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                                   where fact.FacturaVta_Id == factura
                                         && fact.AsigProdFV_Id == detfact.AsigProdFV_Id
                                         && rollo.Rollo_Id == detfact.Rollo_Id
                                   group fact by new
                                   {
                                       fact.FacturaVta_Id,
                                       fact.AsigProdFV_Fecha,
                                       fact.Usua.Usua_Nombre
                                   } into fact
                                   select new
                                   {
                                       Documento = Convert.ToString(fact.Key.FacturaVta_Id),
                                       Fecha = fact.Key.AsigProdFV_Fecha,
                                       Usuario = fact.Key.Usua_Nombre,
                                       Tipo = Convert.ToString("ASIGPRODFV"),
                                   });

            var QueryEntrada = (from ent in _context.Set<EntradaRollo_Producto>()
                                from dent in _context.Set<DetalleEntradaRollo_Producto>()
                                where Convert.ToString(dent.DtEntRolloProd_OT) == ot
                                && ent.EntRolloProd_Id == dent.EntRolloProd_Id
                                group dent by new
                                {
                                    dent.EntRolloProd_Id,
                                    ent.EntRolloProd_Fecha,
                                    ent.Usua.Usua_Nombre,
                                } into ent
                                select new
                                {
                                    Documento = Convert.ToString(ent.Key.EntRolloProd_Id),
                                    Fecha = ent.Key.EntRolloProd_Fecha,
                                    Usuario = ent.Key.Usua_Nombre,
                                    Tipo = Convert.ToString("ENTROLLO"),
                                });

            var QueryDevolucion = (from dev in _context.Set<DetalleDevolucion_ProductoFacturado>()
                                   from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                                   where dev.DevolucionProdFact.FacturaVta_Id == factura
                                         && dev.Rollo_Id == rollo.Rollo_Id
                                   group dev by new
                                   {
                                       dev.DevolucionProdFact.FacturaVta_Id,
                                       dev.DevolucionProdFact.DevProdFact_Fecha,
                                       dev.DevolucionProdFact.Usua.Usua_Nombre,
                                   } into dev
                                   select new
                                   {
                                       Documento = Convert.ToString(dev.Key.FacturaVta_Id),
                                       Fecha = dev.Key.DevProdFact_Fecha,
                                       Usuario = dev.Key.Usua_Nombre,
                                       Tipo = Convert.ToString("DEVPRODFAC"),
                                   });

            var QueryPreCargue = (from pre in _context.Set<DetallePreEntrega_RolloDespacho>()
                                  where Convert.ToString(pre.DtlPreEntRollo_OT) == ot
                                  group pre by new
                                  {
                                      pre.PreEntRollo_Id,
                                      pre.PreEntregaRollo.PreEntRollo_Fecha,
                                      pre.PreEntregaRollo.Usuario.Usua_Nombre,
                                      pre.Proceso.Proceso_Nombre,
                                  } into pre
                                  select new
                                  {
                                      Documento = Convert.ToString(pre.Key.PreEntRollo_Id),
                                      Fecha = pre.Key.PreEntRollo_Fecha,
                                      Usuario = pre.Key.Usua_Nombre,
                                      Tipo = Convert.ToString(pre.Key.Proceso_Nombre),
                                  });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(QueryAsignacion.Concat(QueryEntrada).Concat(QueryDevolucion).Concat(QueryPreCargue));
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
