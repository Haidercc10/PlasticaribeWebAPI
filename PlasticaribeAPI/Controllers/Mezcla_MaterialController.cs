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
    public class Mezcla_MaterialController : ControllerBase
    {
        private readonly dataContext _context;

        public Mezcla_MaterialController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Mezcla_Material
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mezcla_Material>>> GetMezclas_Materiales()
        {
          if (_context.Mezclas_Materiales == null)
          {
              return NotFound();
          }
            return await _context.Mezclas_Materiales.ToListAsync();
        }

        // GET: api/Mezcla_Material/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mezcla_Material>> GetMezcla_Material(int id)
        {
          if (_context.Mezclas_Materiales == null)
          {
              return NotFound();
          }
            var mezcla_Material = await _context.Mezclas_Materiales.FindAsync(id);

            if (mezcla_Material == null)
            {
                return NotFound();
            }

            return mezcla_Material;
        }

        [HttpGet("Nombres_Materiales/{mezclaMaterial}")]
        public ActionResult GetMezcla_MaterialxNombre(string mezclaMaterial)
        {
            if (_context.Mezclas_Materiales == null)
            {
                return NotFound();
            }
            var mezcla_Material = _context.Mezclas_Materiales.Where(p => p.MezMaterial_Nombre == mezclaMaterial).ToList();

            return Ok(mezcla_Material);
        }

        // PUT: api/Mezcla_Material/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMezcla_Material(int id, Mezcla_Material mezcla_Material)
        {
            if (id != mezcla_Material.MezMaterial_Id)
            {
                return BadRequest();
            }

            _context.Entry(mezcla_Material).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Mezcla_MaterialExists(id))
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

        // POST: api/Mezcla_Material
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mezcla_Material>> PostMezcla_Material(Mezcla_Material mezcla_Material)
        {
          if (_context.Mezclas_Materiales == null)
          {
              return Problem("Entity set 'dataContext.Mezclas_Materiales'  is null.");
          }
            _context.Mezclas_Materiales.Add(mezcla_Material);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMezcla_Material", new { id = mezcla_Material.MezMaterial_Id }, mezcla_Material);
        }

        // DELETE: api/Mezcla_Material/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMezcla_Material(int id)
        {
            if (_context.Mezclas_Materiales == null)
            {
                return NotFound();
            }
            var mezcla_Material = await _context.Mezclas_Materiales.FindAsync(id);
            if (mezcla_Material == null)
            {
                return NotFound();
            }

            _context.Mezclas_Materiales.Remove(mezcla_Material);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Mezcla_MaterialExists(int id)
        {
            return (_context.Mezclas_Materiales?.Any(e => e.MezMaterial_Id == id)).GetValueOrDefault();
        }
    }
}
