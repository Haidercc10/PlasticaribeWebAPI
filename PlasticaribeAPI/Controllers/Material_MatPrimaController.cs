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
    public class Material_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Material_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Material_MatPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material_MatPrima>>> GetMateriales_MatPrima()
        {
          if (_context.Materiales_MatPrima == null)
          {
              return NotFound();
          }
            return await _context.Materiales_MatPrima.ToListAsync();
        }

        // GET: api/Material_MatPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Material_MatPrima>> GetMaterial_MatPrima(int id)
        {
          if (_context.Materiales_MatPrima == null)
          {
              return NotFound();
          }
            var material_MatPrima = await _context.Materiales_MatPrima.FindAsync(id);

            if (material_MatPrima == null)
            {
                return NotFound();
            }

            return material_MatPrima;
        }

        [HttpGet("nombreMaterial/{Material_Nombre}")]
        public ActionResult<Material_MatPrima> GetProductoPedido(string Material_Nombre)
        {
            try
            {
                var material = _context.Materiales_MatPrima.Where(m => m.Material_Nombre == Material_Nombre).ToList();


                if (material == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(material);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // PUT: api/Material_MatPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial_MatPrima(int id, Material_MatPrima material_MatPrima)
        {
            if (id != material_MatPrima.Material_Id)
            {
                return BadRequest();
            }

            _context.Entry(material_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Material_MatPrimaExists(id))
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

        // POST: api/Material_MatPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Material_MatPrima>> PostMaterial_MatPrima(Material_MatPrima material_MatPrima)
        {
          if (_context.Materiales_MatPrima == null)
          {
              return Problem("Entity set 'dataContext.Materiales_MatPrima'  is null.");
          }
            _context.Materiales_MatPrima.Add(material_MatPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterial_MatPrima", new { id = material_MatPrima.Material_Id }, material_MatPrima);
        }

        // DELETE: api/Material_MatPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial_MatPrima(int id)
        {
            if (_context.Materiales_MatPrima == null)
            {
                return NotFound();
            }
            var material_MatPrima = await _context.Materiales_MatPrima.FindAsync(id);
            if (material_MatPrima == null)
            {
                return NotFound();
            }

            _context.Materiales_MatPrima.Remove(material_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Material_MatPrimaExists(int id)
        {
            return (_context.Materiales_MatPrima?.Any(e => e.Material_Id == id)).GetValueOrDefault();
        }
    }
}
