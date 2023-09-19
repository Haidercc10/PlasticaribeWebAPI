using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class MovientosAplicacionController : ControllerBase
    {
        private readonly dataContext _context;

        public MovientosAplicacionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/MovientosAplicacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientosAplicacion>>> GetMovimientosAplicacion()
        {
            return await _context.MovimientosAplicacion.ToListAsync();
        }

        // GET: api/MovientosAplicacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientosAplicacion>> GetMovimientosAplicacion(int id)
        {
            var mov = await _context.MovimientosAplicacion.FindAsync(id);

            if (mov == null)
            {
                return NotFound();
            }

            return mov;
        }

        //Consulta para saber que usuario tiene la sesión iniciada y cuanto tiempo lleva en el programa
        [HttpGet("getUsuario_SesionIniciada")]
        public ActionResult GetUsuario_SesionIniciada()
        {
            var con = from mov in _context.Set<MovimientosAplicacion>()
                      join usua in _context.Set<Usuario>() on mov.Usua_Id equals usua.Usua_Id
                      where mov.MovApp_Nombre == "Inicio de sesión" &&
                            mov.MovApp_Fecha >= Convert.ToDateTime("2023-07-24") &&
                            mov.MovApp_Id == (from mov2 in _context.Set<MovimientosAplicacion>()
                                              where mov2.MovApp_Nombre == "Inicio de sesión" &&
                                                    mov.Usua_Id == mov2.Usua_Id &&
                                                    mov2.MovApp_Fecha >= Convert.ToDateTime("2023-07-24")
                                              orderby mov2.MovApp_Id descending
                                              select mov2.MovApp_Id).First() &&
                           mov.MovApp_Id > (from mov2 in _context.Set<MovimientosAplicacion>()
                                            where mov2.MovApp_Nombre == "Cierre de sesión" &&
                                                  mov.Usua_Id == mov2.Usua_Id &&
                                                  mov2.MovApp_Fecha >= Convert.ToDateTime("2023-07-24")
                                            orderby mov2.MovApp_Id descending
                                            select mov2.MovApp_Id).First()
                      select new
                      {
                          usua.Usua_Id,
                          usua.Usua_Nombre,
                          mov.MovApp_Fecha,
                          mov.MovApp_Hora
                      };

            return Ok(con);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorias_Archivos(int id, MovimientosAplicacion movimientosAplicacion)
        {
            if (id != movimientosAplicacion.MovApp_Id)
            {
                return BadRequest();
            }

            _context.Entry(movimientosAplicacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientosAplicacionExists(id))
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

        [HttpPost]
        public async Task<ActionResult<MovimientosAplicacion>> PostRol_Usuario(MovimientosAplicacion movimientosAplicacion)
        {
            _context.MovimientosAplicacion.Add(movimientosAplicacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimientosAplicacion", new { id = movimientosAplicacion.MovApp_Id }, movimientosAplicacion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol_Usuario(int id)
        {
            var rol_Usuario = await _context.Roles_Usuarios.FindAsync(id);
            if (rol_Usuario == null)
            {
                return NotFound();
            }

            _context.Roles_Usuarios.Remove(rol_Usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientosAplicacionExists(int id)
        {
            return _context.MovimientosAplicacion.Any(e => e.MovApp_Id == id);
        }

    }
}
