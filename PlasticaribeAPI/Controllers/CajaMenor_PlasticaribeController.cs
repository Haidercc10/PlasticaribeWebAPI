using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController]
    public class CajaMenor_PlasticaribeController : ControllerBase
    {
        private readonly dataContext _context;

        public CajaMenor_PlasticaribeController(dataContext context)
        {
            _context = context;
        }

        // GET: api/CajaMenor_Plasticaribe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CajaMenor_Plasticaribe>>> GetCajaMenor_Plasticaribe()
        {
            if (_context.CajaMenor_Plasticaribe == null)
            {
                return NotFound();
            }
            return await _context.CajaMenor_Plasticaribe.ToListAsync();
        }

        // GET: api/CajaMenor_Plasticaribe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CajaMenor_Plasticaribe>> GetCajaMenor_Plasticaribe(int id)
        {
            if (_context.CajaMenor_Plasticaribe == null)
            {
                return NotFound();
            }
            var cajaMenor_Plasticaribe = await _context.CajaMenor_Plasticaribe.FindAsync(id);

            if (cajaMenor_Plasticaribe == null)
            {
                return NotFound();
            }

            return cajaMenor_Plasticaribe;
        }

        //Consulta que devolverá la información de los registros de caja menor segun la busqueda con los parametros que le sean pasados
        [HttpGet("getRegistrosCajaMenor/{fechaInicial}/{fechaFinal}")]
        public ActionResult GetRegistrosCajaMenor(DateTime fechaInicial, DateTime fechaFinal, string? area = "", string? tipo = "")
        {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var caja = from c in _context.Set<CajaMenor_Plasticaribe>()
                       where c.CajaMenor_FechaSalida >= fechaInicial &&
                             c.CajaMenor_FechaSalida <= fechaFinal &&
                             (area != "" ? Convert.ToString(c.Area_Id) == area : Convert.ToString(c.Area_Id).Contains(area)) &&
                             (tipo != "" ? Convert.ToString(c.TpSal_Id) == tipo : Convert.ToString(c.TpSal_Id).Contains(tipo))
                       select new
                       {
                           FechaRegistro = c.CajaMenor_FechaRegistro,
                           HoraRegistro = c.CajaMenor_HoraRegistro,
                           RegistradoPor = c.Usuario.Usua_Nombre,
                           FechaSalida = c.CajaMenor_FechaSalida,
                           TipoSalida = c.TpSalida.TpSal_Nombre,
                           Area = c.Areas.Area_Nombre,
                           Valor = c.CajaMenor_ValorSalida,
                           Descripcion = c.CajaMenor_Observacion,
                       };

            return caja.Any() ? Ok(caja) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
        }

        // PUT: api/CajaMenor_Plasticaribe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCajaMenor_Plasticaribe(int id, CajaMenor_Plasticaribe cajaMenor_Plasticaribe)
        {
            if (id != cajaMenor_Plasticaribe.CajaMenor_Id)
            {
                return BadRequest();
            }

            _context.Entry(cajaMenor_Plasticaribe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CajaMenor_PlasticaribeExists(id))
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

        // POST: api/CajaMenor_Plasticaribe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CajaMenor_Plasticaribe>> PostCajaMenor_Plasticaribe(CajaMenor_Plasticaribe cajaMenor_Plasticaribe)
        {
            if (_context.CajaMenor_Plasticaribe == null)
            {
                return Problem("Entity set 'dataContext.CajaMenor_Plasticaribe'  is null.");
            }
            _context.CajaMenor_Plasticaribe.Add(cajaMenor_Plasticaribe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCajaMenor_Plasticaribe", new { id = cajaMenor_Plasticaribe.CajaMenor_Id }, cajaMenor_Plasticaribe);
        }

        // DELETE: api/CajaMenor_Plasticaribe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCajaMenor_Plasticaribe(int id)
        {
            if (_context.CajaMenor_Plasticaribe == null)
            {
                return NotFound();
            }
            var cajaMenor_Plasticaribe = await _context.CajaMenor_Plasticaribe.FindAsync(id);
            if (cajaMenor_Plasticaribe == null)
            {
                return NotFound();
            }

            _context.CajaMenor_Plasticaribe.Remove(cajaMenor_Plasticaribe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CajaMenor_PlasticaribeExists(int id)
        {
            return (_context.CajaMenor_Plasticaribe?.Any(e => e.CajaMenor_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
