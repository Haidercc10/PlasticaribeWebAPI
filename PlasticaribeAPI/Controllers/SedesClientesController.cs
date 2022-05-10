#nullable disable
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
    public class SedesClientesController : ControllerBase
    {
        private readonly dataContext _context;

        public SedesClientesController(dataContext context)
        {
            _context = context;
        }

        public class ClientesController : ControllerBase
        {
            private readonly dataContext _context;
            public ClientesController(dataContext context)
            {
                _context = context;
            }
        }

        // GET: api/SedesClientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SedesClientes>>> GetSedes_Clientes()
        {
            return await _context.Sedes_Clientes.ToListAsync();
        }

        // GET: api/SedesClientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SedesClientes>> GetSedesClientes(long id)
        {
            var sedesClientes = await _context.Sedes_Clientes.FindAsync(id);

            if (sedesClientes == null)
            {
                return NotFound();
            }

            return sedesClientes;
        }

        // PUT: api/SedesClientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSedesClientes(long id, SedesClientes sedesClientes)
        {
            if (id != sedesClientes.SedeCli_Id)
            {
                return BadRequest();
            }

            _context.Entry(sedesClientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SedesClientesExists(id))
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

        // POST: api/SedesClientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SedesClientes>> PostSedesClientes(SedesClientes sedesClientes)
        {
            _context.Sedes_Clientes.Add(sedesClientes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SedesClientesExists(sedesClientes.SedeCli_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSedesClientes", new { id = sedesClientes.SedeCli_Id }, sedesClientes);
        }

        // DELETE: api/SedesClientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSedesClientes(long id)
        {
            var sedesClientes = await _context.Sedes_Clientes.FindAsync(id);
            if (sedesClientes == null)
            {
                return NotFound();
            }

            _context.Sedes_Clientes.Remove(sedesClientes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SedesClientesExists(long id)
        {
            return _context.Sedes_Clientes.Any(e => e.SedeCli_Id == id);
        }
    }
}
