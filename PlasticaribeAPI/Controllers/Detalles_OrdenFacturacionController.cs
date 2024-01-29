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
    public class Detalles_OrdenFacturacionController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_OrdenFacturacionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_OrdenFacturacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_OrdenFacturacion>>> GetDetalles_OrdenFacturacion()
        {
            return await _context.Detalles_OrdenFacturacion.ToListAsync();
        }

        // GET: api/Detalles_OrdenFacturacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_OrdenFacturacion>> GetDetalles_OrdenFacturacion(int id)
        {
            var detalles_OrdenFacturacion = await _context.Detalles_OrdenFacturacion.FindAsync(id);

            if (detalles_OrdenFacturacion == null)
            {
                return NotFound();
            }

            return detalles_OrdenFacturacion;
        }

        [HttpGet("getInformationOrderFact/{id}")]
        public ActionResult GetInformacionOrderFact(int id)
        {
            var fact = from order in _context.Set<OrdenFacturacion>()
                       join dtOrder in _context.Set<Detalles_OrdenFacturacion>() on order.Id equals dtOrder.Id_OrdenFacturacion
                       where order.Id == id
                       select new
                       {
                           order,
                           order.Clientes,
                           order.Usuario,
                           dtOrder,
                           dtOrder.Producto
                       };
            return fact.Any() ? Ok(fact) : NotFound();
        }

        [HttpGet("getInformationOrderFactByFactForDevolution/{fact}")]
        public ActionResult GetInformationOrderFactByFactForDevolution(string fact)
        {
            var devolutions = from dev in _context.Set<DetalleDevolucion_ProductoFacturado>() select dev.Rollo_Id;

            var data = from order in _context.Set<OrdenFacturacion>()
                       join dtOrder in _context.Set<Detalles_OrdenFacturacion>() on order.Id equals dtOrder.Id_OrdenFacturacion
                       where order.Factura == fact &&
                             !devolutions.Contains(dtOrder.Numero_Rollo) &&
                             order.Estado_Id == 11
                       select new
                       {
                           order,
                           order.Clientes,
                           order.Usuario,
                           dtOrder,
                           dtOrder.Producto
                       };
            return data.Any() ? Ok(data) : NotFound();
        }

        [HttpGet("getOrders/{startDate}/{endDate}")]
        public ActionResult GetOrderd(DateTime startDate, DateTime endDate, string? order = "")
        {
            var fact = from or in _context.Set<OrdenFacturacion>()
                       where or.Fecha >= startDate &&
                             or.Fecha <= endDate &&
                             (order != "" ? or.Factura == order : true)
                       select new
                       {
                           or,
                           or.Clientes,
                           or.Usuario,
                           Type = "Orden"
                       };
            return fact.Any() ? Ok(fact) : NotFound();
        }

        [HttpGet("getNotAvaibleProduction")]
        public ActionResult GetNorAvaibleProduction()
        {
            return Ok(from of in _context.Set<Detalles_OrdenFacturacion>() select of.Numero_Rollo);
        }

        // PUT: api/Detalles_OrdenFacturacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_OrdenFacturacion(int id, Detalles_OrdenFacturacion detalles_OrdenFacturacion)
        {
            if (id != detalles_OrdenFacturacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(detalles_OrdenFacturacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_OrdenFacturacionExists(id))
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

        // POST: api/Detalles_OrdenFacturacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_OrdenFacturacion>> PostDetalles_OrdenFacturacion(Detalles_OrdenFacturacion detalles_OrdenFacturacion)
        {
            _context.Detalles_OrdenFacturacion.Add(detalles_OrdenFacturacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_OrdenFacturacion", new { id = detalles_OrdenFacturacion.Id }, detalles_OrdenFacturacion);
        }

        // DELETE: api/Detalles_OrdenFacturacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_OrdenFacturacion(int id)
        {
            var detalles_OrdenFacturacion = await _context.Detalles_OrdenFacturacion.FindAsync(id);
            if (detalles_OrdenFacturacion == null)
            {
                return NotFound();
            }

            _context.Detalles_OrdenFacturacion.Remove(detalles_OrdenFacturacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_OrdenFacturacionExists(int id)
        {
            return _context.Detalles_OrdenFacturacion.Any(e => e.Id == id);
        }
    }
}
