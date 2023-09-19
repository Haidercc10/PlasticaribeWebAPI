using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Remision_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Remision_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Remision_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remision_MateriaPrima>>> GetRemisiones_MateriasPrimas()
        {
            if (_context.Remisiones_MateriasPrimas == null)
            {
                return NotFound();
            }
            return await _context.Remisiones_MateriasPrimas.ToListAsync();
        }

        // GET: api/Remision_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remision_MateriaPrima>> GetRemision_MateriaPrima(int id)
        {
            if (_context.Remisiones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var remision_MateriaPrima = await _context.Remisiones_MateriasPrimas.FindAsync(id);

            if (remision_MateriaPrima == null)
            {
                return NotFound();
            }

            return remision_MateriaPrima;
        }

        [HttpGet("pdfMovimientos/{codigo}")]
        public ActionResult Get(string codigo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Codigo == codigo)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem.Rem_Id,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Rem_Observacion,
                    rem.Rem.Prov_Id,
                    rem.Rem.Prov.TipoIdentificacion_Id,
                    rem.Rem.Prov.Prov_Nombre,
                    rem.Rem.Prov.TpProv.TpProv_Nombre,
                    rem.Rem.Prov.Prov_Telefono,
                    rem.Rem.Prov.Prov_Email,
                    rem.Rem.Prov.Prov_Ciudad,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.UndMed_Id,
                    rem.RemiMatPri_Cantidad,
                    rem.RemiMatPri_ValorUnitario
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("GetRemisionSinFactura/{codigo}")]
        public ActionResult GetRemisionSinFactura(string codigo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.Remisiones_FacturasCompras.Where(x => x.Remi.Rem_Codigo == codigo).Select(x => x.Rem_Id).ToList();
            var remision = from rem in _context.Set<Remision>()
                           where !con.Contains(rem.Rem_Id) && rem.Rem_Codigo == codigo
                           select new
                           {
                               rem.Rem_Id,
                               rem.Rem_Codigo,
                               rem.Rem_Fecha,
                               rem.Prov_Id,
                               rem.Prov.Prov_Nombre,
                               rem.Usua_Id,
                               rem.Usua.Usua_Nombre,
                               rem.TpDoc_Id,
                               rem.TpDoc.TpDoc_Nombre,
                               rem.Rem_PrecioEstimado,
                           };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(remision);
        }

        // PUT: api/Remision_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemision_MateriaPrima(int id, Remision_MateriaPrima remision_MateriaPrima)
        {
            if (id != remision_MateriaPrima.Rem_Id)
            {
                return BadRequest();
            }

            _context.Entry(remision_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Remision_MateriaPrimaExists(id))
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

        // POST: api/Remision_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Remision_MateriaPrima>> PostRemision_MateriaPrima(Remision_MateriaPrima remision_MateriaPrima)
        {
            if (_context.Remisiones_MateriasPrimas == null)
            {
                return Problem("Entity set 'dataContext.Remisiones_MateriasPrimas'  is null.");
            }
            _context.Remisiones_MateriasPrimas.Add(remision_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Remision_MateriaPrimaExists(remision_MateriaPrima.Rem_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRemision_MateriaPrima", new { id = remision_MateriaPrima.Rem_Id }, remision_MateriaPrima);
        }

        // DELETE: api/Remision_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemision_MateriaPrima(int id)
        {
            if (_context.Remisiones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var remision_MateriaPrima = await _context.Remisiones_MateriasPrimas.FindAsync(id);
            if (remision_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.Remisiones_MateriasPrimas.Remove(remision_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Remision_MateriaPrimaExists(int id)
        {
            return (_context.Remisiones_MateriasPrimas?.Any(e => e.Rem_Id == id)).GetValueOrDefault();
        }
    }
}
