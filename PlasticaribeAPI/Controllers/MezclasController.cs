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
    public class MezclasController : ControllerBase
    {
        private readonly dataContext _context;

        public MezclasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Mezclas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mezcla>>> GetMezclas()
        {
          if (_context.Mezclas == null)
          {
              return NotFound();
          }
            return await _context.Mezclas.ToListAsync();
        }

        // GET: api/Mezclas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mezcla>> GetMezcla(long id)
        {
          if (_context.Mezclas == null)
          {
              return NotFound();
          }
            var mezcla = await _context.Mezclas.FindAsync(id);

            if (mezcla == null)
            {
                return NotFound();
            }

            return mezcla;
        }

        [HttpGet("getMezclaNombre/{nombre}")]
        public ActionResult GetMezclaNombre(string nombre)
        {
            var con = (from mez in _context.Set<Mezcla>()
                      where mez.Mezcla_Nombre == nombre
                      select mez).FirstOrDefault();
            if (con == null) return NotFound();
            else return Ok(con);
        }

        [HttpGet("combinacionMezclas/{Mezcla_Nombre}")]
        public ActionResult GetCombinacionMezclas(string Mezcla_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var mezcla = _context.Mezclas
                .Where(m => m.Mezcla_Nombre == Mezcla_Nombre)
                .Include(m => m.MaterialMP)
                .Include(m => m.MezMaterial_MP1C1)
                .Include(m => m.MezMaterial_MP2C1)
                .Include(m => m.MezMaterial_MP3C1)
                .Include(m => m.MezMaterial_MP4C1)
                .Include(m => m.MezPigmento1C1)
                .Include(m => m.MezPigmento2C1)
                .Include(m => m.MezMaterial_MP1C2)
                .Include(m => m.MezMaterial_MP2C2)
                .Include(m => m.MezMaterial_MP3C2)
                .Include(m => m.MezMaterial_MP4C2)
                .Include(m => m.MezPigmento1C2)
                .Include(m => m.MezPigmento2C2)
                .Include(m => m.MezMaterial_MP1C3)
                .Include(m => m.MezMaterial_MP2C3)
                .Include(m => m.MezMaterial_MP3C3)
                .Include(m => m.MezMaterial_MP4C3)
                .Include(m => m.MezPigmento1C3)
                .Include(m => m.MezPigmento2C3)
                .Select(m => new
                {
                    m.Mezcla_Id,
                    m.Mezcla_Nombre,
                    m.Mezcla_NroCapas,
                    m.Material_Id,
                    m.MaterialMP.Material_Nombre,
                    //Porcentaje Capas
                    m.Mezcla_PorcentajeCapa1,
                    m.Mezcla_PorcentajeCapa2,
                    m.Mezcla_PorcentajeCapa3,
                    //Capa 1 - Materiales
                    m.MezMaterial_Id1xCapa1,
                    MezMaterial_Nombre1xCapa1 = m.MezMaterial_MP1C1.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial1_Capa1,
                    m.MezMaterial_Id2xCapa1,
                    MezMaterial_Nombre2xCapa1 = m.MezMaterial_MP2C1.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial2_Capa1,
                    m.MezMaterial_Id3xCapa1,
                    MezMaterial_Nombre3xCapa1 = m.MezMaterial_MP3C1.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial3_Capa1,
                    m.MezMaterial_Id4xCapa1,
                    MezMaterial_Nombre4xCapa1 = m.MezMaterial_MP4C1.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial4_Capa1,
                    //Capa 1 - Pigmentos
                    m.MezPigmto_Id1xCapa1,
                    MezPigmento_Nombre1xCapa1 = m.MezPigmento1C1.MezPigmto_Nombre,
                    m.Mezcla_PorcentajePigmto1_Capa1,
                    m.MezPigmto_Id2xCapa1,
                    MezPigmento_Nombre2xCapa1 = m.MezPigmento2C1.MezPigmto_Nombre,
                    m.Mezcla_PorcentajePigmto2_Capa1,
                    //Capa 2 - Materiales
                    m.MezMaterial_Id1xCapa2,
                    MezMaterial_Nombre1xCapa2 = m.MezMaterial_MP1C2.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial1_Capa2,
                    m.MezMaterial_Id2xCapa2,
                    MezMaterial_Nombre2xCapa2 = m.MezMaterial_MP2C2.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial2_Capa2,
                    m.MezMaterial_Id3xCapa2,
                    MezMaterial_Nombre3xCapa2 = m.MezMaterial_MP3C2.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial3_Capa2,
                    m.MezMaterial_Id4xCapa2,
                    MezMaterial_Nombre4xCapa2 = m.MezMaterial_MP4C2.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial4_Capa2,
                    //Capa 2 - Pigmentos
                    m.MezPigmto_Id1xCapa2,
                    MezPigmento_Nombre1xCapa2 = m.MezPigmento1C2.MezPigmto_Nombre,
                    m.Mezcla_PorcentajePigmto1_Capa2,
                    m.MezPigmto_Id2xCapa2,
                    MezPigmento_Nombre2xCapa2 = m.MezPigmento2C2.MezPigmto_Nombre,
                    m.Mezcla_PorcentajePigmto2_Capa2,
                    //Capa 3 - Materiales
                    m.MezMaterial_Id1xCapa3,
                    MezMaterial_Nombre1xCapa3 = m.MezMaterial_MP1C3.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial1_Capa3,
                    m.MezMaterial_Id2xCapa3,
                    MezMaterial_Nombre2xCapa3 = m.MezMaterial_MP2C3.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial2_Capa3,
                    m.MezMaterial_Id3xCapa3,
                    MezMaterial_Nombre3xCapa3 = m.MezMaterial_MP3C3.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial3_Capa3,
                    m.MezMaterial_Id4xCapa3,
                    MezMaterial_Nombre4xCapa3 = m.MezMaterial_MP4C3.MezMaterial_Nombre,
                    m.Mezcla_PorcentajeMaterial4_Capa3,
                    //Capa 3 - Pigmentos
                    m.MezPigmto_Id1xCapa3,
                    MezPigmento_Nombre1xCapa3 = m.MezPigmento1C3.MezPigmto_Nombre,
                    m.Mezcla_PorcentajePigmto1_Capa3,
                    m.MezPigmto_Id2xCapa3,
                    MezPigmento_Nombre2xCapa3 = m.MezPigmento2C3.MezPigmto_Nombre,
                    m.Mezcla_PorcentajePigmto2_Capa3,
                    m.Usua_Id,
                    m.Usua.Usua_Nombre,
                    m.Mezcla_FechaIngreso
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if ( mezcla != null)
            {
                return Ok(mezcla);
            }else
            {
                return BadRequest();
            }
        }

        // PUT: api/Mezclas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMezcla(long id, Mezcla mezcla)
        {
            if (id != mezcla.Mezcla_Id)
            {
                return BadRequest();
            }

            _context.Entry(mezcla).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MezclaExists(id))
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

        // POST: api/Mezclas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mezcla>> PostMezcla(Mezcla mezcla)
        {
          if (_context.Mezclas == null)
          {
              return Problem("Entity set 'dataContext.Mezclas'  is null.");
          }
            _context.Mezclas.Add(mezcla);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMezcla", new { id = mezcla.Mezcla_Id }, mezcla);
        }

        // DELETE: api/Mezclas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMezcla(long id)
        {
            if (_context.Mezclas == null)
            {
                return NotFound();
            }
            var mezcla = await _context.Mezclas.FindAsync(id);
            if (mezcla == null)
            {
                return NotFound();
            }

            _context.Mezclas.Remove(mezcla);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MezclaExists(long id)
        {
            return (_context.Mezclas?.Any(e => e.Mezcla_Id == id)).GetValueOrDefault();
        }
    }
}
