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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class OrdenFacturacionController : ControllerBase
    {
        private readonly dataContext _context;

        public OrdenFacturacionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/OrdenFacturacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenFacturacion>>> GetOrdenFacturacion()
        {
            return await _context.OrdenFacturacion.ToListAsync();
        }

        // GET: api/OrdenFacturacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenFacturacion>> GetOrdenFacturacion(int id)
        {
            var ordenFacturacion = await _context.OrdenFacturacion.FindAsync(id);

            if (ordenFacturacion == null)
            {
                return NotFound();
            }

            return ordenFacturacion;
        }

        //Obtener el último id de la orden de facturación
        [HttpGet("getLastOrder")]
        public ActionResult getLastOrder() 
        {
            return Ok((from o in _context.Set<OrdenFacturacion>() orderby o.Id descending select o.Id).FirstOrDefault()); 
        }

        [HttpPut("putStatusOrder/{order}")]
        public async Task<IActionResult> PutStatusOrder(int order)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var ordenFacturacion = (from of in _context.Set<OrdenFacturacion>() where of.Id == order select of).FirstOrDefault();
            ordenFacturacion.Estado_Id = 21;
            _context.Entry(ordenFacturacion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpPut("putStatusOrderAnulled/{order}")]
        public async Task<IActionResult> PutStatusOrderAnulled(int order)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var ordenFacturacion = (from of in _context.Set<OrdenFacturacion>() where of.Id == order select of).FirstOrDefault();
            ordenFacturacion.Estado_Id = 3;
            _context.Entry(ordenFacturacion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpPut("putFactOrder/{order}/{fact}")]
        public async Task<IActionResult> PutFactOrder(int order, string fact)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var ordenFacturacion = (from of in _context.Set<OrdenFacturacion>() where of.Id == order select of).FirstOrDefault();
            ordenFacturacion.Factura = fact;
            _context.Entry(ordenFacturacion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Consulta para traer la información de un bulto. 
        [HttpGet("getMovementsInvoices/{fact}")]
        public ActionResult getMovementsInvoices(string fact)
        {
            var billingOrder = from o in _context.Set<OrdenFacturacion>()
                               where o.Factura == fact
                               select new
                               {
                                   Id = Convert.ToInt32(o.Id),
                                   UserName = Convert.ToString(o.Usuario.Usua_Nombre),
                                   Observation = Convert.ToString(o.Factura),
                                   Date = Convert.ToString(o.Fecha.Value),
                                   Hour = Convert.ToString(o.Hora),
                                   //Status_Id = Convert.ToInt32(dt.Estado_Id),
                                   Status = Convert.ToString(o.Estado.Estado_Nombre),
                                   Client = Convert.ToString(o.Clientes.Cli_Nombre),
                                   Type = Convert.ToString("ORDEN FACTURACIÓN"),
                               };

            var dispatch = from a in _context.Set<AsignacionProducto_FacturaVenta>()
                           where a.FacturaVta_Id == fact
                           select new
                           {
                               Id = Convert.ToInt32("0000" + a.FacturaVta_Id),
                               UserName = Convert.ToString(a.Usua.Usua_Nombre),
                               Observation = Convert.ToString(a.NotaCredito_Id),
                               Date = Convert.ToString(a.AsigProdFV_FechaEnvio),
                               Hour = Convert.ToString(a.AsigProdFV_Hora),
                               //Status_Id = Convert.ToInt32(21),
                               Status = Convert.ToString("ENVIADO"),
                               Client = Convert.ToString(a.Cliente.Cli_Nombre),
                               Type = Convert.ToString("SALIDA DESPACHO"),
                           };

            var devolution = from d in _context.Set<Devolucion_ProductoFacturado>()
                             where d.FacturaVta_Id == fact
                             select new
                             {
                                 Id = Convert.ToInt32(d.DevProdFact_Id),
                                 UserName = Convert.ToString(d.Usua.Usua_Nombre),
                                 Observation = Convert.ToString(d.DevProdFact_Observacion),
                                 Date = Convert.ToString(d.DevProdFact_Fecha),
                                 Hour = Convert.ToString(d.DevProdFact_Hora),
                                 //Status_Id = Convert.ToInt32(dt.DevolucionProdFact.Estado_Id),
                                 Status = Convert.ToString(d.Estados.Estado_Nombre),
                                 Client = Convert.ToString(d.Cliente.Cli_Nombre),
                                 Type = Convert.ToString("DEVOLUCIÓN"),
                             };

            return Ok(billingOrder.Concat(dispatch.Concat(devolution)));
        }

        // PUT: api/OrdenFacturacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenFacturacion(int id, OrdenFacturacion ordenFacturacion)
        {
            if (id != ordenFacturacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordenFacturacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenFacturacionExists(id))
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

        // POST: api/OrdenFacturacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenFacturacion>> PostOrdenFacturacion(OrdenFacturacion ordenFacturacion)
        {
            _context.OrdenFacturacion.Add(ordenFacturacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdenFacturacion", new { id = ordenFacturacion.Id }, ordenFacturacion);
        }

        // DELETE: api/OrdenFacturacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenFacturacion(int id)
        {
            var ordenFacturacion = await _context.OrdenFacturacion.FindAsync(id);
            if (ordenFacturacion == null)
            {
                return NotFound();
            }

            _context.OrdenFacturacion.Remove(ordenFacturacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenFacturacionExists(int id)
        {
            return _context.OrdenFacturacion.Any(e => e.Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
