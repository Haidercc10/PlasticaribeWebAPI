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
    public class Productos_MateriasPrimasController : ControllerBase
    {
        private readonly dataContext _context;

        public Productos_MateriasPrimasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Productos_MateriasPrimas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos_MateriasPrimas>>> GetProductos_MateriasPrimas()
        {
          if (_context.Productos_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.Productos_MateriasPrimas.ToListAsync();
        }

        // GET: api/Productos_MateriasPrimas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos_MateriasPrimas>> GetProductos_MateriasPrimas(int id)
        {
          if (_context.Productos_MateriasPrimas == null)
          {
              return NotFound();
          }
            var productos_MateriasPrimas = await _context.Productos_MateriasPrimas.FindAsync(id);

            if (productos_MateriasPrimas == null)
            {
                return NotFound();
            }

            return productos_MateriasPrimas;
        }
        // Consulta que devolverá la información de las materias primas utilizadas para crear un producto
        [HttpGet("getRecetaProducto/{producto}/{presentacion}")]
        public ActionResult GetRecetaProducto(long producto, string presentacion)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var receta = from rec in _context.Set<Productos_MateriasPrimas>()
                         where producto == rec.Prod_Id &&
                               presentacion == rec.UndMed_Id
                         select new
                         {
                             Id_Existencia = rec.ExProd_Id,
                             Id_Producto = rec.Prod_Id,
                             Nombre_Producto = rec.Producto.Prod_Nombre,
                             Presentacion_Producto = rec.UndMed_Id,
                             Id_MateriaPrima = rec.MatPri_Id,
                             Id_Tinta = rec.Tinta_Id,
                             Id_Bopp = rec.Bopp_Id,
                             Id_Material = rec.MatPri_Id != 84 ? rec.MatPri_Id :
                                           rec.Tinta_Id != 2001 ? rec.Tinta_Id :
                                           rec.Bopp_Id != 1 ? rec.Bopp_Id : 0,
                             Nombre_Material = rec.MatPri_Id != 84 ? rec.Materia_Prima.MatPri_Nombre :
                                               rec.Tinta_Id != 2001 ? rec.Tinta.Tinta_Nombre :
                                               rec.Bopp_Id != 1 ? rec.Bopp_Generico.BoppGen_Nombre : "",
                             Categoria_Material = rec.MatPri_Id != 84 ? rec.Materia_Prima.CatMP_Id :
                                                  rec.Tinta_Id != 2001 ? rec.Tinta.CatMP_Id :
                                                  rec.Bopp_Id != 1 ? 6 : 0,
                             Cantidad = rec.Cantidad_Minima,
                             Presentacion_Material = "Kg",
                             Id_Registro = rec.Id,
                         };

            return receta.Any() ? Ok(receta) : NotFound($"¡No se encontró una receta para el item {producto} con la presentación {presentacion}!");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Consulta que devolverá los datos de una receta con base en un producto, presentación y material que le sean pasado
        [HttpGet("getExistenciaReceta/{prod}/{presentacion}/{material}")]
        public ActionResult GetExistenciaReceta(long prod, string presentacion, long material)
        {
            var receta = from rec in _context.Set<Productos_MateriasPrimas>()
                         where prod == rec.Prod_Id &&
                               presentacion == rec.UndMed_Id &&
                               (rec.MatPri_Id == material || rec.Tinta_Id == material || rec.Bopp_Id == material)
                         select rec;
            return Ok(receta);
        }

        // PUT: api/Productos_MateriasPrimas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductos_MateriasPrimas(int id, Productos_MateriasPrimas productos_MateriasPrimas)
        {
            if (id != productos_MateriasPrimas.Id)
            {
                return BadRequest();
            }

            _context.Entry(productos_MateriasPrimas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Productos_MateriasPrimasExists(id))
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

        // POST: api/Productos_MateriasPrimas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Productos_MateriasPrimas>> PostProductos_MateriasPrimas(Productos_MateriasPrimas productos_MateriasPrimas)
        {
          if (_context.Productos_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.Productos_MateriasPrimas'  is null.");
          }
            _context.Productos_MateriasPrimas.Add(productos_MateriasPrimas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductos_MateriasPrimas", new { id = productos_MateriasPrimas.Id }, productos_MateriasPrimas);
        }

        // DELETE: api/Productos_MateriasPrimas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductos_MateriasPrimas(int id)
        {
            if (_context.Productos_MateriasPrimas == null)
            {
                return NotFound();
            }
            var productos_MateriasPrimas = await _context.Productos_MateriasPrimas.FindAsync(id);
            if (productos_MateriasPrimas == null)
            {
                return NotFound();
            }

            _context.Productos_MateriasPrimas.Remove(productos_MateriasPrimas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Productos_MateriasPrimasExists(int id)
        {
            return (_context.Productos_MateriasPrimas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
