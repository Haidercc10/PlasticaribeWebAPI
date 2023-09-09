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
    public class ClientesController : ControllerBase
    {
        private readonly dataContext _context;

        public ClientesController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClientes(long id)
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
            var clientes = await _context.Clientes.FindAsync(id);

            if (clientes == null)
            {
                return NotFound();
            }

            return clientes;
        }

        [HttpGet("estadoId/{Estado_Id}")]
        public ActionResult<Clientes> GetEstado(int Estado_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var clientes = _context.Clientes.Where(pp => pp.Estado_Id == Estado_Id).ToList();

            if (clientes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(clientes);
            }
        }

        [HttpGet("nombreCliente/{Cli_Nombre}")]
        public ActionResult<Clientes> GetNombreCliente(string Cli_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var clientes = _context.Clientes.Where(pp => pp.Cli_Nombre == Cli_Nombre).ToList();

            if (clientes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(clientes);
            }
        }

        [HttpGet("getClientesVendedores_LikeNombre/{vendedor}/{nombre}")]
        public ActionResult GetClientes_LikeNombre(long vendedor, string nombre)
        {
            var cliente = from cli in _context.Set<Clientes>()
                          where cli.Estado_Id == 1
                                && cli.usua_Id == vendedor
                                && cli.Cli_Nombre.Contains(nombre)
                          select cli;

            if (cliente == null || cliente.Count() == 0) return BadRequest("No se encontró ningún cliente");
            else return Ok(cliente);
        }

        [HttpGet("LikeGetClientes/{Cli_Nombre}")]
        public ActionResult<Clientes> LikeGetClientes(string Cli_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var clientes = _context.Clientes.Where(pp => pp.Cli_Nombre.Contains(Cli_Nombre)).Select(cl => new { cl.Cli_Id, cl.Cli_Nombre }).ToList();

            if (clientes == null) return NotFound();
            else return Ok(clientes);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(long id, Clientes clientes)
        {
            if (id != clientes.Cli_Id)
            {
                return BadRequest();
            }

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
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

        //Funcion que actualizará el estado del cliente
        [HttpPut("putEstadoCliente/{id}")]
        public ActionResult PutEstadoCliente(long id, int estado)
        {
            try
            {
                var con = _context.Clientes.Where(x => x.Cli_Id == id).First();
                con.Estado_Id = estado;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
          if (_context.Clientes == null)
          {
              return Problem("Entity set 'dataContext.Clientes'  is null.");
          }
            _context.Clientes.Add(clientes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientesExists(clientes.Cli_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClientes", new { id = clientes.Cli_Id }, clientes);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(long id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesExists(long id)
        {
            return (_context.Clientes?.Any(e => e.Cli_Id == id)).GetValueOrDefault();
        }
    }
}
