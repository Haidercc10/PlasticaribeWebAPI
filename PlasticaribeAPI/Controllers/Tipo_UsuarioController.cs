#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tipo_UsuarioController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_UsuarioController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Usuario>>> GetTipos_Usuarios()
        {
            return await _context.Tipos_Usuarios.ToListAsync();
        }

        // GET: api/Tipo_Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Usuario>> GetTipo_Usuario(int id)
        {
            var tipo_Usuario = await _context.Tipos_Usuarios.FindAsync(id);

            if (tipo_Usuario == null)
            {
                return NotFound();
            }

            return tipo_Usuario;
        }

        // PUT: api/Tipo_Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Usuario(int id, Tipo_Usuario tipo_Usuario)
        {
            if (id != tipo_Usuario.tpUsu_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_UsuarioExists(id))
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

        // POST: api/Tipo_Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_Usuario>> PostTipo_Usuario(Tipo_Usuario tipo_Usuario)
        {
            _context.Tipos_Usuarios.Add(tipo_Usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Usuario", new { id = tipo_Usuario.tpUsu_Id }, tipo_Usuario);
        }

        // DELETE: api/Tipo_Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Usuario(int id)
        {
            var tipo_Usuario = await _context.Tipos_Usuarios.FindAsync(id);
            if (tipo_Usuario == null)
            {
                return NotFound();
            }

            _context.Tipos_Usuarios.Remove(tipo_Usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_UsuarioExists(int id)
        {
            return _context.Tipos_Usuarios.Any(e => e.tpUsu_Id == id);
        }
    }
}
