using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        private readonly dataContext _context;

        public MantenimientoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mantenimiento>>> GetMantenimientos()
        {
            return await _context.Mantenimientos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Mantenimiento>> GetMantenimiento(long id)
        {
            var Mantenimiento = await _context.Mantenimientos.FindAsync(id);

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Mantenimiento;
        }

        [HttpGet("ObtenerUltimoId")]
        public ActionResult GetMttos()
        {
            var Mantenimiento = _context.Mantenimientos.OrderByDescending(ult => ult.Mtto_Id).First();

            return Ok(Mantenimiento);
        }

        //
        [HttpGet("getPedidoMtto/{id}")]
        public ActionResult GetPedidosMttos(long id)
        {
            var Mantenimiento = _context.Mantenimientos.Where(pm => pm.PedMtto_Id == id)
                                                       .Select(u => new
                                                       {
                                                           u.Mtto_Id,
                                                           u.PedMtto_Id,
                                                           u.Mtto_FechaInicio,
                                                           u.Mtto_FechaFin,
                                                           u.Mtto_FechaRegistro,
                                                           u.Mtto_HoraRegistro,
                                                           u.Mtto_PrecioTotal,
                                                           u.Prov_Id,
                                                           u.Proveedor.Prov_Nombre,
                                                           u.Usua_Id,
                                                           u.Usu.Usua_Nombre,
                                                           u.Mtto_Observacion,
                                                           u.Mtto_CantDias,
                                                           u.Estado_Id,
                                                           u.Estado.Estado_Nombre
                                                       }).ToList();

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(Mantenimiento);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMantenimiento(long id, Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.Mtto_Id)
            {
                return BadRequest();
            }

            _context.Entry(mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MantenimientoExists(id))
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

        //
        [HttpPost]
        public async Task<ActionResult<Mantenimiento>> PostMantenimiento(Mantenimiento mantenimiento)
        {
            _context.Mantenimientos.Add(mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMantenimiento", new { id = mantenimiento.Mtto_Id }, mantenimiento);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMantenimientos(long id)
        {
            var Mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (Mantenimiento == null)
            {
                return NotFound();
            }

            _context.Mantenimientos.Remove(Mantenimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool MantenimientoExists(long id)
        {
            return _context.Mantenimientos.Any(e => e.Mtto_Id == id);
        }
    }
}
