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
    public class Asignacion_MatPrimaXTintaController : ControllerBase
    {
        private readonly dataContext _context;

        public Asignacion_MatPrimaXTintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Asignacion_MatPrimaXTinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion_MatPrimaXTinta>>> GetAsignaciones_MatPrimasXTintas()
        {
          if (_context.Asignaciones_MatPrimasXTintas == null)
          {
              return NotFound();
          }
            return await _context.Asignaciones_MatPrimasXTintas.ToListAsync();
        }

        // GET: api/Asignacion_MatPrimaXTinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion_MatPrimaXTinta>> GetAsignacion_MatPrimaXTinta(long id)
        {
          if (_context.Asignaciones_MatPrimasXTintas == null)
          {
              return NotFound();
          }
            var asignacion_MatPrimaXTinta = await _context.Asignaciones_MatPrimasXTintas.FindAsync(id);

            if (asignacion_MatPrimaXTinta == null)
            {
                return NotFound();
            }

            return asignacion_MatPrimaXTinta;
        }


        [HttpGet("ultimoId/")]
        public ActionResult<Asignacion_MatPrimaXTinta> GetUltimoId()
        {
            var asignacion = _context.Asignaciones_MatPrimasXTintas.OrderBy(asg => asg.AsigMPxTinta_Id).Select(agr => new
            {
                agr.AsigMPxTinta_Id
            }).Last();

            return Ok(asignacion);
        }

        /** Cargar Tintas y Materias Primas */
        [HttpGet("CargarTintas_MatPrimas/")]
        public ActionResult GetTinta_MaPrima()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var matPrima = (from mp in _context.Set<Materia_Prima>()
                            select new
                            {
                                MatPrima = mp.MatPri_Id,
                                NombreMP = mp.MatPri_Nombre,
                                Stock = mp.MatPri_Stock,
                                Unidad = mp.UndMed_Id,
                                IDCategoria = mp.CatMP_Id,
                                NombreCategoria = mp.CatMP.CatMP_Nombre,
                                PrecioMP = mp.MatPri_Precio
                            });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var tinta = (from tnt in _context.Set<Tinta>()
                         select new
                         {
                             MatPrima = tnt.Tinta_Id,
                             NombreMP = tnt.Tinta_Nombre,
                             Stock = tnt.Tinta_Stock,
                             Unidad = tnt.UndMed_Id,
                             IDCategoria = tnt.CatMP_Id,
                             NombreCategoria = tnt.CatMP.CatMP_Nombre, 
                             PrecioMP = tnt.Tinta_Precio
                         });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            var Query = tinta.Concat(matPrima);

            return Ok(Query);
        }

        /**/
        [HttpGet("CargarMatPrimasXId/{Id_MatPrima}")]
        public ActionResult GetTinta_MaPrima(long Id_MatPrima)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var matPrima = (from mp in _context.Set<Materia_Prima>()
                            where mp.MatPri_Id == Id_MatPrima
                            select new
                            {
                                MatPrima = mp.MatPri_Id,
                                NombreMP = mp.MatPri_Nombre,
                                Stock = mp.MatPri_Stock,
                                Unidad = mp.UndMed_Id,
                                IDCategoria = mp.CatMP_Id,
                                NombreCategoria = mp.CatMP.CatMP_Nombre,
                                PrecioMP = mp.MatPri_Precio

                            });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var tinta = (from tnt in _context.Set<Tinta>()
                         where tnt.Tinta_Id == Id_MatPrima
                         select new
                         {
                             MatPrima = tnt.Tinta_Id,
                             NombreMP = tnt.Tinta_Nombre,
                             Stock = tnt.Tinta_Stock,
                             Unidad = tnt.UndMed_Id,
                             IDCategoria = tnt.CatMP_Id,
                             NombreCategoria = tnt.CatMP.CatMP_Nombre,
                             PrecioMP = tnt.Tinta_Precio
                         });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            var Query = tinta.Concat(matPrima);

            return Ok(Query);
        }


        // PUT: api/Asignacion_MatPrimaXTinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion_MatPrimaXTinta(long id, Asignacion_MatPrimaXTinta asignacion_MatPrimaXTinta)
        {
            if (id != asignacion_MatPrimaXTinta.AsigMPxTinta_Id)
            {
                return BadRequest();
            }

            _context.Entry(asignacion_MatPrimaXTinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asignacion_MatPrimaXTintaExists(id))
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


        // POST: api/Asignacion_MatPrimaXTinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignacion_MatPrimaXTinta>> PostAsignacion_MatPrimaXTinta(Asignacion_MatPrimaXTinta asignacion_MatPrimaXTinta)
        {
          if (_context.Asignaciones_MatPrimasXTintas == null)
          {
              return Problem("Entity set 'dataContext.Asignaciones_MatPrimasXTintas'  is null.");
          }
            _context.Asignaciones_MatPrimasXTintas.Add(asignacion_MatPrimaXTinta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion_MatPrimaXTinta", new { id = asignacion_MatPrimaXTinta.AsigMPxTinta_Id }, asignacion_MatPrimaXTinta);
        }

        // DELETE: api/Asignacion_MatPrimaXTinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion_MatPrimaXTinta(long id)
        {
            if (_context.Asignaciones_MatPrimasXTintas == null)
            {
                return NotFound();
            }
            var asignacion_MatPrimaXTinta = await _context.Asignaciones_MatPrimasXTintas.FindAsync(id);
            if (asignacion_MatPrimaXTinta == null)
            {
                return NotFound();
            }

            _context.Asignaciones_MatPrimasXTintas.Remove(asignacion_MatPrimaXTinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Asignacion_MatPrimaXTintaExists(long id)
        {
            return (_context.Asignaciones_MatPrimasXTintas?.Any(e => e.AsigMPxTinta_Id == id)).GetValueOrDefault();
        }
    }
}
