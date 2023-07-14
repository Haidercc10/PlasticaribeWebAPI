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
    public class Vistas_PermisosController : ControllerBase
    {
        private readonly dataContext _context;

        public Vistas_PermisosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Vistas_Permisos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vistas_Permisos>>> GetVistas_Permisos()
        {
            if (_context.Vistas_Permisos == null)
            {
                return NotFound();
            }
            return await _context.Vistas_Permisos.ToListAsync();
        }

        // GET: api/Vistas_Permisos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vistas_Permisos>> GetVistas_Permisos(int id)
        {
            if (_context.Vistas_Permisos == null)
            {
                return NotFound();
            }
            var vistas_Permisos = await _context.Vistas_Permisos.FindAsync(id);

            if (vistas_Permisos == null)
            {
                return NotFound();
            }

            return vistas_Permisos;
        }

        //
        [HttpGet("get_categorias_menu/{rol}")]
        public ActionResult GetCategorias(string rol)
        {
            var con = from vp in _context.Set<Vistas_Permisos>()
                      where vp.Vp_Id_Roles.Contains($"|{rol}|")
                      select vp.Vp_Categoria;
            return Ok(con);
        }

        //
        [HttpGet("get_vistas_rol/{rol}/{categoria}")]
        public ActionResult Get_Vistas_Rol(string rol, string categoria)
        {
            var con = from vp in _context.Set<Vistas_Permisos>()
                      where vp.Vp_Id_Roles.Contains($"|{rol}|") &&
                            vp.Vp_Categoria.Contains($"|{categoria}|")
                      select vp;
            return Ok(con);
        }

        //
        [HttpGet("get_permisos/{rol}/{ruta}")]
        public ActionResult Get_Permisos(string rol, string ruta)
        {
            var con = from vp in _context.Set<Vistas_Permisos>()
                      where vp.Vp_Id_Roles.Contains($"|{rol}|") &&
                            vp.Vp_Nombre == ruta
                      select vp.Vp_Nombre;
            return con.Any() ? Ok(con) : NotFound();
        }

        //
        [HttpGet("get_By_Rol/{rol}")]
        public ActionResult Get_By_Rol(string rol)
        {
            var con = from vp in _context.Set<Vistas_Permisos>()
                      where vp.Vp_Id_Roles.Contains($"|{rol}|")
                      select new
                      {
                          Id = vp.Vp_Id,
                          Nombre = vp.Vp_Nombre,
                          Icono = vp.Vp_Icono_Dock,
                          Categoria = vp.Vp_Categoria,
                          Ruta = vp.Vp_Ruta,
                      };
            return con.Any() ? Ok(con) : NotFound();
        }

        // PUT: api/Vistas_Permisos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVistas_Permisos(int id, Vistas_Permisos vistas_Permisos)
        {
            if (id != vistas_Permisos.Vp_Id)
            {
                return BadRequest();
            }

            _context.Entry(vistas_Permisos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Vistas_PermisosExists(id))
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

        // POST: api/Vistas_Permisos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vistas_Permisos>> PostVistas_Permisos(Vistas_Permisos vistas_Permisos)
        {
          if (_context.Vistas_Permisos == null)
          {
              return Problem("Entity set 'dataContext.Vistas_Permisos'  is null.");
          }
            _context.Vistas_Permisos.Add(vistas_Permisos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVistas_Permisos", new { id = vistas_Permisos.Vp_Id }, vistas_Permisos);
        }

        // DELETE: api/Vistas_Permisos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVistas_Permisos(int id)
        {
            if (_context.Vistas_Permisos == null)
            {
                return NotFound();
            }
            var vistas_Permisos = await _context.Vistas_Permisos.FindAsync(id);
            if (vistas_Permisos == null)
            {
                return NotFound();
            }

            _context.Vistas_Permisos.Remove(vistas_Permisos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Vistas_PermisosExists(int id)
        {
            return (_context.Vistas_Permisos?.Any(e => e.Vp_Id == id)).GetValueOrDefault();
        }
    }
}