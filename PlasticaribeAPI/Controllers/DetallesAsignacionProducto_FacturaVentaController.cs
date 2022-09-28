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

        [HttpGet("CrearPdf/{factura}")]
        public ActionResult GetCrearPdf (string factura)
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
                    Suma = x.Sum(y => y.DtAsigProdFV_Cantidad)
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
                                   select new
                                   {
                                       Documento = Convert.ToString(fact.FacturaVta_Id),
                                       Fecha = fact.AsigProdFV_Fecha,
                                       Cli_Id = Convert.ToString(fact.Cli_Id),
                                       Cli_Nombre = Convert.ToString(fact.Cliente.Cli_Nombre),
                                       Prod_Id = detfact.Prod_Id,
                                       Prod_Nombre = detfact.Prod.Prod_Nombre,
                                       Rollo = detfact.Rollo_Id,
                                       Cantidad = detfact.DtAsigProdFV_Cantidad,
                                       Presentacion = detfact.UndMed_Id,
                                       Estado_Rollo = rollo.Estado.Estado_Nombre,
                                       Tipo = "ASIGPRODFV",
                                   });

            var QueryEntrada = (from ent in _context.Set<EntradaRollo_Producto>()
                                from dent in _context.Set<DetalleEntradaRollo_Producto>()
                                where ent.EntRolloProd_Fecha >= FechaIni
                                && ent.EntRolloProd_Fecha <= FechaFin
                                && ent.EntRolloProd_Id == dent.EntRolloProd_Id
                                select new
                                {
                                    Documento = Convert.ToString(dent.DtEntRolloProd_OT),
                                    Fecha = ent.EntRolloProd_Fecha,
                                    Cli_Id = Convert.ToString(""),
                                    Cli_Nombre = Convert.ToString(""),
                                    Prod_Id = dent.Prod_Id,
                                    Prod_Nombre = dent.Prod.Prod_Nombre,
                                    Rollo = dent.Rollo_Id,
                                    Cantidad = dent.DtEntRolloProd_Cantidad,
                                    Presentacion = dent.UndMed_Rollo,
                                    Estado_Rollo = dent.Estado.Estado_Nombre,
                                    Tipo = "ENTROLLO",
                                });

            var QueryDevolucion = (from dev in _context.Set<DetalleDevolucion_ProductoFacturado>()
                      from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      where dev.DevolucionProdFact.DevProdFact_Fecha >= FechaIni
                            && dev.DevolucionProdFact.DevProdFact_Fecha <= FechaFin
                            && dev.Rollo_Id == rollo.Rollo_Id
                      select new
                      {
                          Documento = Convert.ToString(dev.DevolucionProdFact.FacturaVta_Id),
                          Fecha = dev.DevolucionProdFact.DevProdFact_Fecha,
                          Cli_Id = Convert.ToString(dev.DevolucionProdFact.Cli_Id),
                          Cli_Nombre = Convert.ToString(dev.DevolucionProdFact.Cliente.Cli_Nombre),
                          Prod_Id = dev.Prod_Id,
                          Prod_Nombre = dev.Prod.Prod_Nombre,
                          Rollo =  dev.Rollo_Id,
                          Cantidad = dev.DtDevProdFact_Cantidad,
                          Presentacion = dev.UndMed_Id,
                          Estado_Rollo = rollo.Estado.Estado_Nombre,
                          Tipo = "DEVPRODFAC",
                      });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
          
            return Ok(QueryAsignacion.Concat(QueryEntrada).Concat(QueryDevolucion));                    
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
                                   select new
                                   {
                                       Documento = Convert.ToString(fact.FacturaVta_Id),
                                       Fecha = fact.AsigProdFV_Fecha,
                                       Cli_Id = Convert.ToString(fact.Cli_Id),
                                       Cli_Nombre = Convert.ToString(fact.Cliente.Cli_Nombre),
                                       Prod_Id = detfact.Prod_Id,
                                       Prod_Nombre = detfact.Prod.Prod_Nombre,
                                       Rollo = detfact.Rollo_Id,
                                       Cantidad = detfact.DtAsigProdFV_Cantidad,
                                       Presentacion = detfact.UndMed_Id,
                                       Estado_Rollo = rollo.Estado.Estado_Nombre,
                                       Tipo = "ASIGPRODFV",
                                   });

            var QueryEntrada = (from ent in _context.Set<EntradaRollo_Producto>()
                                from dent in _context.Set<DetalleEntradaRollo_Producto>()
                                where Convert.ToString(dent.DtEntRolloProd_OT) == ot
                                && ent.EntRolloProd_Id == dent.EntRolloProd_Id
                                select new
                                {
                                    Documento = Convert.ToString(dent.DtEntRolloProd_OT),
                                    Fecha = ent.EntRolloProd_Fecha,
                                    Cli_Id = Convert.ToString(""),
                                    Cli_Nombre = Convert.ToString(""),
                                    Prod_Id = dent.Prod_Id,
                                    Prod_Nombre = dent.Prod.Prod_Nombre,
                                    Rollo = dent.Rollo_Id,
                                    Cantidad = dent.DtEntRolloProd_Cantidad,
                                    Presentacion = dent.UndMed_Rollo,
                                    Estado_Rollo = dent.Estado.Estado_Nombre,
                                    Tipo = "ENTROLLO",
                                });

            var QueryDevolucion = (from dev in _context.Set<DetalleDevolucion_ProductoFacturado>()
                                   from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                                   where dev.DevolucionProdFact.FacturaVta_Id == factura
                                         && dev.Rollo_Id == rollo.Rollo_Id
                                   select new
                                   {
                                       Documento = Convert.ToString(dev.DevolucionProdFact.FacturaVta_Id),
                                       Fecha = dev.DevolucionProdFact.DevProdFact_Fecha,
                                       Cli_Id = Convert.ToString(dev.DevolucionProdFact.Cli_Id),
                                       Cli_Nombre = Convert.ToString(dev.DevolucionProdFact.Cliente.Cli_Nombre),
                                       Prod_Id = dev.Prod_Id,
                                       Prod_Nombre = dev.Prod.Prod_Nombre,
                                       Rollo = dev.Rollo_Id,
                                       Cantidad = dev.DtDevProdFact_Cantidad,
                                       Presentacion = dev.UndMed_Id,
                                       Estado_Rollo = rollo.Estado.Estado_Nombre,
                                       Tipo = "DEVPRODFAC",
                                   });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(QueryAsignacion.Concat(QueryEntrada).Concat(QueryDevolucion));
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
