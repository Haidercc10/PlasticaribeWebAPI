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
    public class Tickets_RevisadosController : ControllerBase
    {
        private readonly dataContext _context;

        public Tickets_RevisadosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tickets_Revisados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tickets_Revisados>>> GetTickets_Revisados()
        {
          if (_context.Tickets_Revisados == null)
          {
              return NotFound();
          }
            return await _context.Tickets_Revisados.ToListAsync();
        }

        // GET: api/Tickets_Revisados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tickets_Revisados>> GetTickets_Revisados(long id)
        {
          if (_context.Tickets_Revisados == null)
          {
              return NotFound();
          }
            var tickets_Revisados = await _context.Tickets_Revisados.FindAsync(id);

            if (tickets_Revisados == null)
            {
                return NotFound();
            }

            return tickets_Revisados;
        }

        // PUT: api/Tickets_Revisados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTickets_Revisados(long id, Tickets_Revisados tickets_Revisados)
        {
            if (id != tickets_Revisados.TicketRev_Id)
            {
                return BadRequest();
            }

            _context.Entry(tickets_Revisados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tickets_RevisadosExists(id))
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

        // POST: api/Tickets_Revisados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tickets_Revisados>> PostTickets_Revisados(Tickets_Revisados tickets_Revisados)
        {
          if (_context.Tickets_Revisados == null)
          {
              return Problem("Entity set 'dataContext.Tickets_Revisados'  is null.");
          }
            _context.Tickets_Revisados.Add(tickets_Revisados);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTickets_Revisados", new { id = tickets_Revisados.TicketRev_Id }, tickets_Revisados);
        }

        // DELETE: api/Tickets_Revisados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTickets_Revisados(long id)
        {
            if (_context.Tickets_Revisados == null)
            {
                return NotFound();
            }
            var tickets_Revisados = await _context.Tickets_Revisados.FindAsync(id);
            if (tickets_Revisados == null)
            {
                return NotFound();
            }

            _context.Tickets_Revisados.Remove(tickets_Revisados);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tickets_RevisadosExists(long id)
        {
            return (_context.Tickets_Revisados?.Any(e => e.TicketRev_Id == id)).GetValueOrDefault();
        }
    }
}
