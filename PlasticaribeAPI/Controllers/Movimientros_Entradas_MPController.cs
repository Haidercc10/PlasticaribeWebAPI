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
    public class Movimientros_Entradas_MPController : ControllerBase
    {
        private readonly dataContext _context;

        public Movimientros_Entradas_MPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Movimientros_Entradas_MP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimientros_Entradas_MP>>> GetMovimientros_Entradas_MP()
        {
          if (_context.Movimientros_Entradas_MP == null)
          {
              return NotFound();
          }
            return await _context.Movimientros_Entradas_MP.ToListAsync();
        }

        // GET: api/Movimientros_Entradas_MP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimientros_Entradas_MP>> GetMovimientros_Entradas_MP(int id)
        {
          if (_context.Movimientros_Entradas_MP == null)
          {
              return NotFound();
          }
            var movimientros_Entradas_MP = await _context.Movimientros_Entradas_MP.FindAsync(id);

            if (movimientros_Entradas_MP == null)
            {
                return NotFound();
            }

            return movimientros_Entradas_MP;
        }

        // Consulta que devolverá la información de las compras realizadas de una materia prima en un lapso de tiempo
        [HttpGet("getComprasRealizadas/{fechaInicio}/{fechaFin}/{material}")]
        public ActionResult GetComprasRealizadas(DateTime fechaInicio, DateTime fechaFin, long material)
        {
            var compras = from c in _context.Set<Movimientros_Entradas_MP>()
                          where c.Fecha_Entrada >= fechaInicio &&
                                c.Fecha_Entrada <= fechaFin &&
                                (c.MatPri_Id == material || c.Tinta_Id == material || c.Bopp_Id == material)
                          select new
                          {
                              FechaCompra = c.Fecha_Entrada,
                              HoraCompra = c.Hora_Entrada,
                              CantidadCompra = c.Cantidad_Entrada,
                              PrecioReal = c.Precio_RealUnitario,
                              PrecioEstandar = c.Precio_EstandarUnitario,
                              DiferenciaPrecio = (c.Precio_RealUnitario - c.Precio_EstandarUnitario),
                              CostoReal = (c.Cantidad_Entrada * c.Precio_RealUnitario),
                              CostoEstandar = (c.Cantidad_Entrada * c.Precio_EstandarUnitario),
                              VariacionPrecio = (c.Cantidad_Entrada * c.Precio_RealUnitario) - (c.Cantidad_Entrada * c.Precio_EstandarUnitario)
                          };
            return compras.Any() ? Ok(compras) : NotFound();
        }

        // Materias primas, Tintas, Biorientados
        [HttpGet("getInventarioMateriales")]
        public ActionResult GetInventarioMateriales()
        {
            var materiasPrimas = from mp in _context.Set<Materia_Prima>()
                                 select new
                                 {
                                     Id_Materia_Prima = mp.MatPri_Id,
                                     Nombre_Materia_Prima = mp.MatPri_Nombre,
                                 };

            var bopp = from bp in _context.Set<Bopp_Generico>()
                       select new
                       {
                           Id_Materia_Prima = bp.BoppGen_Id,
                           Nombre_Materia_Prima = bp.BoppGen_Nombre,
                       };

            var tintas = from t in _context.Set<Tinta>()
                         select new
                         {
                             Id_Materia_Prima = t.Tinta_Id,
                             Nombre_Materia_Prima = t.Tinta_Nombre,
                         };

            return Ok(materiasPrimas.Concat(bopp).Concat(tintas));

        }
        // PUT: api/Movimientros_Entradas_MP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientros_Entradas_MP(int id, Movimientros_Entradas_MP movimientros_Entradas_MP)
        {
            if (id != movimientros_Entradas_MP.Id)
            {
                return BadRequest();
            }

            _context.Entry(movimientros_Entradas_MP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Movimientros_Entradas_MPExists(id))
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

        // POST: api/Movimientros_Entradas_MP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimientros_Entradas_MP>> PostMovimientros_Entradas_MP(Movimientros_Entradas_MP movimientros_Entradas_MP)
        {
          if (_context.Movimientros_Entradas_MP == null)
          {
              return Problem("Entity set 'dataContext.Movimientros_Entradas_MP'  is null.");
          }
            _context.Movimientros_Entradas_MP.Add(movimientros_Entradas_MP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimientros_Entradas_MP", new { id = movimientros_Entradas_MP.Id }, movimientros_Entradas_MP);
        }

        // DELETE: api/Movimientros_Entradas_MP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientros_Entradas_MP(int id)
        {
            if (_context.Movimientros_Entradas_MP == null)
            {
                return NotFound();
            }
            var movimientros_Entradas_MP = await _context.Movimientros_Entradas_MP.FindAsync(id);
            if (movimientros_Entradas_MP == null)
            {
                return NotFound();
            }

            _context.Movimientros_Entradas_MP.Remove(movimientros_Entradas_MP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Movimientros_Entradas_MPExists(int id)
        {
            return (_context.Movimientros_Entradas_MP?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
