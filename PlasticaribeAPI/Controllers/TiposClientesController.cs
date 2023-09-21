#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TiposClientesController : ControllerBase
    {
        private readonly dataContext _context;

        public TiposClientesController(dataContext context)
        {
            _context = context;
        }

        // GET: api/TiposClientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposClientes>>> GetTipos_Clientes()
        {
            return await _context.Tipos_Clientes.ToListAsync();
        }

        // GET: api/TiposClientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TiposClientes>> GetTiposClientes(int id)
        {
            var tiposClientes = await _context.Tipos_Clientes.FindAsync(id);

            if (tiposClientes == null)
            {
                return NotFound();
            }

            return tiposClientes;
        }

        [HttpGet("nombreTipoCliente/{TPCli_Nombre}")]
        public ActionResult<TiposClientes> GetProductoPedido(string TPCli_Nombre)
        {
            try
            {
                var tipoProducto = _context.Tipos_Clientes.Where(tp => tp.TPCli_Nombre == TPCli_Nombre).ToList();


                if (tipoProducto == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(tipoProducto);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // PUT: api/TiposClientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiposClientes(int id, TiposClientes tiposClientes)
        {
            if (id != tiposClientes.TPCli_Id)
            {
                return BadRequest();
            }

            _context.Entry(tiposClientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiposClientesExists(id))
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

        // POST: api/TiposClientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TiposClientes>> PostTiposClientes(TiposClientes tiposClientes)
        {
            _context.Tipos_Clientes.Add(tiposClientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTiposClientes", new { id = tiposClientes.TPCli_Id }, tiposClientes);
        }

        // DELETE: api/TiposClientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiposClientes(int id)
        {
            var tiposClientes = await _context.Tipos_Clientes.FindAsync(id);
            if (tiposClientes == null)
            {
                return NotFound();
            }

            _context.Tipos_Clientes.Remove(tiposClientes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiposClientesExists(int id)
        {
            return _context.Tipos_Clientes.Any(e => e.TPCli_Id == id);
        }
    }
}
