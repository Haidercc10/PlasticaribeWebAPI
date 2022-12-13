using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivosController : ControllerBase
    {
        private readonly dataContext _context;

        public ActivosController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activo>>> GetActivos()
        {
            return await _context.Activos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Activo>> GetActivos(long id)
        {
            var Activos = await _context.Activos.FindAsync(id);

            if (Activos == null)
            {
                return NotFound();
            }

            return Activos;
        }

        // Consulta que buscará el nombre de un activo por medio de los datos que se le vatan pasando, se usuará un Contains() (en sql es un LIKE)
        [HttpGet("getActivoNombre/{datos}")]
        public ActionResult getActivoNombre(string datos)
        {
            if (datos == null)
            {
                return NoContent();
            }
            var activos = from activo in _context.Set<Activo>()
                          where activo.Actv_Nombre.Contains(datos)
                          select activo;
            return Ok(activos);
        }

        // Consulta que nos servirá para el reporte de activos
        [HttpGet("getInfoActivos/{activo}")]
        public ActionResult get(long activo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var totalMtto = _context.Detalles_Mantenimientos
                            .Where(x => x.Actv_Id == activo)
                            .Sum(x => x.DtMtto_Precio);

            var con = (from act in _context.Set<Activo>()
                      from mtto in _context.Set<Detalle_Mantenimiento>()
                      where act.Actv_Id == activo
                            && act.Actv_Id == mtto.Actv_Id
                      orderby mtto.Mtto_Id descending
                      select new
                      {
                          Activo_Id = act.Actv_Serial,
                          Activo_Nombre = act.Actv_Nombre,
                          Fecha_Compra = act.Actv_FechaCompra,
                          Fecha_UltMtto = mtto.Mttos.Mtto_FechaInicio,
                          Precio_Compra = act.Actv_PrecioCompra,
                          Precio_UltMtto = mtto.DtMtto_Precio,
                          Precio_TotalMtto = totalMtto,
                          Depreciacion = act.Actv_Depreciacion,
                      }).FirstOrDefault();
            return Ok(con);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivo(long id, Activo activo)
        {
            if (id != activo.Actv_Id)
            {
                return BadRequest();
            }

            _context.Entry(activo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivoExists(id))
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
        public async Task<ActionResult<Activo>> PostActivos(Activo activo)
        {
            _context.Activos.Add(activo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivos", new { id = activo.Actv_Id }, activo);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivos(long id)
        {
            var activo = await _context.Activos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            _context.Activos.Remove(activo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool ActivoExists(long id)
        {
            return _context.Activos.Any(e => e.Actv_Id == id);
        }
    }
}
