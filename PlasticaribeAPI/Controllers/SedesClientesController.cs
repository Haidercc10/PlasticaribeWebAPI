﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class SedesClientesController : ControllerBase
    {
        private readonly dataContext _context;

        public SedesClientesController(dataContext context)
        {
            _context = context;
        }

        // GET: api/SedesClientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SedesClientes>>> GetSedes_Clientes()
        {
            if (_context.Sedes_Clientes == null)
            {
                return NotFound();
            }
            return await _context.Sedes_Clientes.ToListAsync();
        }

        // GET: api/SedesClientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SedesClientes>> GetSedesClientes(long id)
        {
            if (_context.Sedes_Clientes == null)
            {
                return NotFound();
            }
            var sedesClientes = await _context.Sedes_Clientes.FindAsync(id);

            if (sedesClientes == null)
            {
                return NotFound();
            }

            return sedesClientes;
        }

        [HttpGet("cliente/{Cli_Id}")]
        public ActionResult<SedesClientes> Get(long Cli_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var clientes = _context.Sedes_Clientes
                .Where(sc => sc.Cli_Id == Cli_Id)
                .Include(sc => sc.Cli.Usua)
                .Select(sc => new
                {
                    sc.SedeCli_Id,
                    sc.SedeCliente_Ciudad,
                    sc.SedeCliente_Direccion,
                    sc.SedeCli_CodPostal,
                    sc.Cli_Id,
                    sc.Cli.Cli_Nombre,
                    sc.Cli.usua_Id,
                    sc.Cli.Usua.Usua_Nombre,
                    sc.SedeCli_CodBagPro,
                }).ToList();

            if (clientes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(clientes);
            }
        }

        [HttpGet("clienteNombre/{Cli_Nombre}")]
        public ActionResult<SedesClientes> GetClienteSede(string Cli_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var clientes = _context.Sedes_Clientes
                .Where(sc => sc.Cli.Cli_Nombre == Cli_Nombre)
                .Include(sc => sc.Cli.Usua)
                .Include(sc => sc.Cli.TPCli)
                .Select(sc => new
                {
                    sc.SedeCli_Id,
                    sc.SedeCliente_Ciudad,
                    sc.SedeCliente_Direccion,
                    sc.SedeCli_CodPostal,
                    sc.Cli_Id,
                    sc.Cli.Cli_Nombre,
                    sc.Cli.usua_Id,
                    sc.Cli.Usua.Usua_Nombre,
                    sc.Cli.TPCli.TPCli_Nombre,
                    sc.Cli.TPCli.TPCli_Id,
                    sc.Cli.TipoIdentificacion_Id,
                    sc.Cli.Cli_Telefono,
                    sc.Cli.Cli_Email,
                    sc.SedeCli_CodBagPro,
                }).ToList();

            if (clientes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(clientes);
            }
        }

        [HttpGet("clienteSede/{Cli_Nombre}/{ciudad}/{direccion}")]
        public ActionResult<SedesClientes> GetClienteSede(string Cli_Nombre, string ciudad, string direccion)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var clientes = _context.Sedes_Clientes
                .Where(sc => sc.Cli.Cli_Nombre == Cli_Nombre && sc.SedeCliente_Ciudad == ciudad && sc.SedeCliente_Direccion == direccion)
                .Include(sc => sc.Cli.Usua)
                .Select(sc => new
                {
                    sc.SedeCli_Id,
                    sc.SedeCliente_Ciudad,
                    sc.SedeCliente_Direccion,
                    sc.SedeCli_CodPostal,
                    sc.Cli_Id,
                    sc.Cli.Cli_Nombre,
                    sc.Cli.usua_Id,
                    sc.Cli.Usua.Usua_Nombre,
                    sc.SedeCli_CodBagPro,
                }).ToList();

            return clientes == null ? BadRequest($"¡No se encontró información del cliente {Cli_Nombre} con la ciudad {ciudad} y dirección {direccion}!") : Ok(clientes);
        }

        //Funcion que consultará la informacion de una sede de cliente basandose en el Codigo de BagPro, Ciudad y Dirección
        [HttpGet("getSedeCliente/{cod}/{ciudad}/{direccion}")]
        public ActionResult GetSedeCliente(string cod, string ciudad, string direccion)
        {
            var con = from sd in _context.Set<SedesClientes>()
                      where sd.SedeCli_CodBagPro == cod
                            && sd.SedeCliente_Ciudad == ciudad
                            && sd.SedeCliente_Direccion == direccion
                      select sd;
            return Ok(con);
        }

        [HttpGet("getDireccionesCliente/{cliente}/{ciudad}")]
        public ActionResult GetDireccionesCliente(long cliente, string ciudad)
        {
            var con = from sc in _context.Set<SedesClientes>()
                      where sc.Cli_Id == cliente
                            && sc.SedeCliente_Ciudad == ciudad
                      select sc;
            return Ok(con);
        }

        // Funcion que consultará las sedes que tenga un cliente, el cliente se le pasará como parametro
        [HttpGet("getSedesCliente/{id}")]
        public ActionResult GetSedes_Clientes(long id)
        {
            var con = (from sc in _context.Set<SedesClientes>()
                       where sc.Cli_Id == id
                       orderby sc.SedeCli_Id descending
                       select sc.SedeCli_Id).FirstOrDefault();
            return Ok(con);
        }

        //Funcion que consultará la informacion de una sede de cliente basandose en el Codigo de BagPro, Ciudad y Dirección
        [HttpGet("getSedeClientexNitBagPro/{cod}")]
        public ActionResult getSedeClientexNitBagPro(string cod)
        {
            var con = from sd in _context.Set<SedesClientes>()
                      from cl in _context.Set<Clientes>()
                      where cl.Cli_Id == sd.Cli_Id &&
                      sd.SedeCli_CodBagPro == cod
                      select new
                      {
                          idSede = sd.SedeCli_Id,       
                          id_Cliente = sd.Cli_Id,
                          Cliente = cl.Cli_Nombre,
                          id_Vendedor = Convert.ToString(cl.usua_Id),
                          Vendedor = cl.Usua.Usua_Nombre,
                      }; 
            
            return Ok(con);
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
            if (_context.Sedes_Clientes == null)
            {
                return Problem("Entity set 'dataContext.Sedes_Clientes'  is null.");
            }
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
            if (_context.Sedes_Clientes == null)
            {
                return NotFound();
            }
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
            return (_context.Sedes_Clientes?.Any(e => e.SedeCli_Id == id)).GetValueOrDefault();
        }
    }
}
