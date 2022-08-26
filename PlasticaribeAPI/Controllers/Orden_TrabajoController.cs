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

        [HttpGet("PedidosSinOT/")]
        public ActionResult<Orden_Trabajo> Get()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var orden_Trabajo = _context.Orden_Trabajo
                .Where(ot => ot.PedidoExterno.PedExt_Id != ot.PedExt_Id)
                .Include(ot => ot.PedidoExterno)
                .Select(ot => ot.PedidoExterno.PedExt_Id)
                .ToList();

            /*var PedidoOt = _context.Orden_Trabajo
                .Where(ot => ot.PedidoExterno.PedExt_Id == ot.PedExt_Id)
                .Include(ot => ot.PedidoExterno)
                .Select(ot => new { ot.PedidoExterno.PedExt_Id })
                .ToList();

            var PedidosSinOt = _context.Pedidos_Externos
                                .Where(pe => PedidoOt.Contains(pe.PedExt_Id))
                                .Tolist();

            return Ok(PedidosSinOt);*/

            /*var ordenTrabajo = (from pe in _context.Pedidos_Externos
                                join ot in _context.Orden_Trabajo
                                on pe.PedExt_Id equals ot.PedExt_Id
                                into pedidos from PedidoExterno in pedidos.DefaultIfEmpty()
                                select new
                                {
                                    IdPedido = (pe.PedExt_Id != PedidoExterno.PedExt_Id)
                                }).ToList();

            return Ok(ordenTrabajo);*/

            var ot = from Pedidos_Externos in _context.Set<PedidoExterno>()
                     join Orden_Trabajo in _context.Set<Orden_Trabajo>()
                     on Pedidos_Externos.PedExt_Id equals Orden_Trabajo.PedExt_Id
                     where Pedidos_Externos.PedExt_Id != Orden_Trabajo.PedExt_Id
                     select Orden_Trabajo.PedExt_Id;

            return Ok(orden_Trabajo);

            /*if (orden_Trabajo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(orden_Trabajo);
            }*/
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
