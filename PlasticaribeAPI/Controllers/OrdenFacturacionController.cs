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
}
