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
    [ApiController]
    public class MovientosAplicacionController : ControllerBase
    {
        private readonly dataContext _context;

        public MovientosAplicacionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Rol_Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientosAplicacion>>> GetMovimientosAplicacion()
        {
            return await _context.MovimientosAplicacion.ToListAsync();
        }

        // GET: api/Rol_Usuario/5
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
