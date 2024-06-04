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

        // Consulta que devolverá la informacaión una devolución
        [HttpGet("getInformationDevById/{id}")]
        public ActionResult GetInformationDevById(long id)
        {
            var infoDev = from dev in _context.Set<Devolucion_ProductoFacturado>()
                          join dtDev in _context.Set<DetalleDevolucion_ProductoFacturado>() on dev.DevProdFact_Id equals dtDev.DevProdFact_Id
                          where dev.DevProdFact_Id == id
                          //&& dev.Estado_Id != 18 
                          select new
                          {
                              dev,
                              Cliente = new
                              {
                                  dev.Cliente.TipoIdentificacion_Id,
                                  dev.Cli_Id,
                                  dev.Cliente.Cli_Nombre,
                                  dev.Cliente.Cli_Telefono,
                                  dev.Cliente.Cli_Email,
                              },
                              Usua = new
                              {
                                  dev.Usua_Id,
                                  dev.Usua.Usua_Nombre, 
                                  dev.UsuaModifica_Id, 
                                  Usua_Modifica = dev.UsuaModificaDv.Usua_Nombre,
                              },
                              dtDev = new
                              {
                                  dtDev.DtDevProdFact_Id,
                                  dtDev.DevProdFact_Id,
                                  dtDev.Prod_Id,
                                  Numero_Rollo = dtDev.Rollo_Id,
                                  Cantidad = dtDev.DtDevProdFact_Cantidad,
                                  Presentacion = dtDev.UndMed_Id, 
                                  Falla_Id = dtDev.Falla_Id,
                                  Falla = dtDev.Fallas.Falla_Nombre,
                              },
                              Prod = new
                              {
                                  dtDev.Prod_Id,
                                  dtDev.Prod.Prod_Nombre
                              },
                              EstadoDv = new { 
                                   dev.Estado_Id,
                                   dev.Estados.Estado_Nombre,
                              },
                              EstadoOF = (from dof in _context.Set<Detalles_OrdenFacturacion>() where dof.Id_OrdenFacturacion == dev.Id_OrdenFact && dof.Numero_Rollo == dtDev.Rollo_Id select dof.Estados.Estado_Nombre).FirstOrDefault(),
                          };
            return infoDev.Any() ? Ok(infoDev) : NotFound();
        }

        [HttpGet("getDevolutions/{startDate}/{endDate}")]
        public ActionResult GetOrderd(DateTime startDate, DateTime endDate, string? order = "")
        {

            var infoDev = from dev in _context.Set<Devolucion_ProductoFacturado>()
                          where dev.DevProdFact_Fecha >= startDate &&
                                dev.DevProdFact_Fecha <= endDate &&
                                (order != "" ? Convert.ToString(dev.DevProdFact_Id) == order : true)
                          select new
                          {
                              or = new
                              {
                                  Id = dev.DevProdFact_Id,
                                  Factura = dev.FacturaVta_Id,
                                  Fecha = dev.DevProdFact_Fecha,
                                  Hora = dev.DevProdFact_Hora,
                                  dev.Cli_Id,
                                  Observacion = dev.DevProdFact_Observacion,
                                  Reposicion = dev.DevProdFact_Reposicion,
                                  Estado_Id = dev.Estado_Id,
                              },
                              Clientes = dev.Cliente,
                              Usuario = dev.Usua,
                              FechaHora = dev.DevProdFact_Fecha + " " + dev.DevProdFact_Hora,
                              Type = "Devolucion",
                              Estado = dev.Estado_Id == 11 ? "PENDIENTE" : dev.Estado_Id == 29 ? "EN REVISIÓN" : dev.Estado_Id == 38 ? "POR REPONER" : dev.Estado_Id == 39 ? "EN REPOSICION" : dev.Estado_Id == 18 ? "CERRADA" : "",
                              
                          };
            return infoDev.Any() ? Ok(infoDev) : NotFound();
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
