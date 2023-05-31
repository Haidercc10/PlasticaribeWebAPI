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
    public class SolicitudesMP_OrdenesCompraController : ControllerBase
    {
        private readonly dataContext _context;

        public SolicitudesMP_OrdenesCompraController(dataContext context)
        {
            _context = context;
        }

        // GET: api/SolicitudesMP_OrdenesCompra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudesMP_OrdenesCompra>>> GetSolicitudesMP_OrdenesCompra()
        {
          if (_context.SolicitudesMP_OrdenesCompra == null)
          {
              return NotFound();
          }
            return await _context.SolicitudesMP_OrdenesCompra.ToListAsync();
        }

        // GET: api/SolicitudesMP_OrdenesCompra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudesMP_OrdenesCompra>> GetSolicitudesMP_OrdenesCompra(long id)
        {
          if (_context.SolicitudesMP_OrdenesCompra == null)
          {
              return NotFound();
          }
            var solicitudesMP_OrdenesCompra = await _context.SolicitudesMP_OrdenesCompra.FindAsync(id);

            if (solicitudesMP_OrdenesCompra == null)
            {
                return NotFound();
            }

            return solicitudesMP_OrdenesCompra;
        }

        // PUT: api/SolicitudesMP_OrdenesCompra/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitudesMP_OrdenesCompra(long id, SolicitudesMP_OrdenesCompra solicitudesMP_OrdenesCompra)
        {
            if (id != solicitudesMP_OrdenesCompra.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(solicitudesMP_OrdenesCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudesMP_OrdenesCompraExists(id))
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

        // POST: api/SolicitudesMP_OrdenesCompra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SolicitudesMP_OrdenesCompra>> PostSolicitudesMP_OrdenesCompra(SolicitudesMP_OrdenesCompra solicitudesMP_OrdenesCompra)
        {
          if (_context.SolicitudesMP_OrdenesCompra == null)
          {
              return Problem("Entity set 'dataContext.SolicitudesMP_OrdenesCompra'  is null.");
          }
            _context.SolicitudesMP_OrdenesCompra.Add(solicitudesMP_OrdenesCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitudesMP_OrdenesCompra", new { id = solicitudesMP_OrdenesCompra.Codigo }, solicitudesMP_OrdenesCompra);
        }

        // DELETE: api/SolicitudesMP_OrdenesCompra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitudesMP_OrdenesCompra(long id)
        {
            if (_context.SolicitudesMP_OrdenesCompra == null)
            {
                return NotFound();
            }
            var solicitudesMP_OrdenesCompra = await _context.SolicitudesMP_OrdenesCompra.FindAsync(id);
            if (solicitudesMP_OrdenesCompra == null)
            {
                return NotFound();
            }

            _context.SolicitudesMP_OrdenesCompra.Remove(solicitudesMP_OrdenesCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolicitudesMP_OrdenesCompraExists(long id)
        {
            return (_context.SolicitudesMP_OrdenesCompra?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
