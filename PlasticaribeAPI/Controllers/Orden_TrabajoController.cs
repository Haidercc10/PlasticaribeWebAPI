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
    public class Orden_TrabajoController : ControllerBase
    {
        private readonly dataContext _context;

        public Orden_TrabajoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Orden_Trabajo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden_Trabajo>>> GetOrden_Trabajo()
        {
          if (_context.Orden_Trabajo == null)
          {
              return NotFound();
          }
            return await _context.Orden_Trabajo.ToListAsync();
        }

        // GET: api/Orden_Trabajo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden_Trabajo>> GetOrden_Trabajo(long id)
        {
          if (_context.Orden_Trabajo == null)
          {
              return NotFound();
          }
            var orden_Trabajo = await _context.Orden_Trabajo.FindAsync(id);

            if (orden_Trabajo == null)
            {
                return NotFound();
            }

            return orden_Trabajo;
        }

        [HttpGet("NumeroOt/{Ot_Id}")]
        public ActionResult<Orden_Trabajo> GetNumeroOt(long Ot_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var orden_trabajo = _context.Orden_Trabajo
                .Where(ot => ot.Ot_Id == Ot_Id)
                .Include(ot => ot.PedidoExterno)
                .Include(ot => ot.SedeCli)
                .Include(ot => ot.SedeCli.Cli)
                .Include(ot => ot.Estado)
                .Select(ot => new
                {
                    ot.SedeCli.Cli_Id,
                    ot.SedeCli.Cli.Cli_Nombre,
                    ot.PedExt_Id,
                    ot.PedidoExterno.Usua.Usua_Nombre,
                    ot.Ot_FechaCreacion,
                    ot.PedidoExterno.PedExt_FechaEntrega,
                    ot.SedeCli.SedeCliente_Ciudad,
                    ot.SedeCli.SedeCliente_Direccion,
                    ot.Estado.Estado_Nombre,
                    ot.Ot_Observacion,
                    ot.Ot_MargenAdicional,
                    ot.Ot_Cyrel,
                    ot.Ot_Corte,
                    ot.Prod_Id,
                    ot.Ot_CantidadKilos
                })
                .ToList();
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            return Ok(orden_trabajo);
        }

        [HttpGet("NumeroPedido/{PedExt_Id}")]
        public ActionResult<Orden_Trabajo> GetNumeroPedido(long PedExt_Id)
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var orden_trabajo = _context.Orden_Trabajo
                .Where(ot => ot.PedExt_Id == PedExt_Id)
                .ToList();
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            return Ok(orden_trabajo);
        }

        // PUT: api/Orden_Trabajo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrden_Trabajo(long id, Orden_Trabajo orden_Trabajo)
        {
            if (id != orden_Trabajo.Ot_Id)
            {
                return BadRequest();
            }

            _context.Entry(orden_Trabajo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Orden_TrabajoExists(id))
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

        // POST: api/Orden_Trabajo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orden_Trabajo>> PostOrden_Trabajo(Orden_Trabajo orden_Trabajo)
        {
          if (_context.Orden_Trabajo == null)
          {
              return Problem("Entity set 'dataContext.Orden_Trabajo'  is null.");
          }
            _context.Orden_Trabajo.Add(orden_Trabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrden_Trabajo", new { id = orden_Trabajo.Ot_Id }, orden_Trabajo);
        }

        // DELETE: api/Orden_Trabajo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden_Trabajo(long id)
        {
            if (_context.Orden_Trabajo == null)
            {
                return NotFound();
            }
            var orden_Trabajo = await _context.Orden_Trabajo.FindAsync(id);
            if (orden_Trabajo == null)
            {
                return NotFound();
            }

            _context.Orden_Trabajo.Remove(orden_Trabajo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Orden_TrabajoExists(long id)
        {
            return (_context.Orden_Trabajo?.Any(e => e.Ot_Id == id)).GetValueOrDefault();
        }
    }
}
