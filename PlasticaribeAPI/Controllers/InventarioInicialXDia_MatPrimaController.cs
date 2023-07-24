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
    public class InventarioInicialXDia_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public InventarioInicialXDia_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/InventarioInicialXDia_MatPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioInicialXDia_MatPrima>>> GetInventarioInicialXDias_MatPrima()
        {
          if (_context.InventarioInicialXDias_MatPrima == null)
          {
              return NotFound();
          }
            return await _context.InventarioInicialXDias_MatPrima.ToListAsync();
        }

        // GET: api/InventarioInicialXDia_MatPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioInicialXDia_MatPrima>> GetInventarioInicialXDia_MatPrima(long id)
        {
          if (_context.InventarioInicialXDias_MatPrima == null)
          {
              return NotFound();
          }
            var inventarioInicialXDia_MatPrima = await _context.InventarioInicialXDias_MatPrima.FindAsync(id);

            if (inventarioInicialXDia_MatPrima == null)
            {
                return NotFound();
            }

            return inventarioInicialXDia_MatPrima;
        }

        [HttpGet("get_Cantidad_Material_Meses")]
        public ActionResult Get_Cantidad_Material_Meses()
        {
            var enero = (from inv in _context.Set<InventarioInicialXDia_MatPrima>() 
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Enero * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Enero * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 1 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var febrero = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                           join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                           select inv.Febrero * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                         join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                         select inv.Febrero * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                        where bp.BOPP_FechaIngreso.Month == 2 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                        select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var marzo = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Marzo * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Marzo * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 3 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var abril = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Abril * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Abril * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 4 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var mayo = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                        join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                        select inv.Mayo * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                      join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                      select inv.Mayo * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                  where bp.BOPP_FechaIngreso.Month == 5 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                  select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var junio = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Junio * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Junio * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 6 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var julio = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Julio * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Julio * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 7 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var agosto = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                          join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                          select inv.Agosto * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                        join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                        select inv.Agosto * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                      where bp.BOPP_FechaIngreso.Month == 8 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                      select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var septiembre = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                              join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                              select inv.Septiembre * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                            join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                            select inv.Septiembre * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                              where bp.BOPP_FechaIngreso.Month == 9 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                              select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var octubre = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                           join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                           select inv.Octubre * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                         join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                         select inv.Octubre * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                        where bp.BOPP_FechaIngreso.Month == 10 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                        select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var novimebre = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                             join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                             select inv.Noviembre * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                           join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                           select inv.Noviembre * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                            where bp.BOPP_FechaIngreso.Month == 11 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                            select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var diciembre = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                             join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                             select inv.Diciembre * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                           join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                           select inv.Diciembre * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                            where bp.BOPP_FechaIngreso.Month == 12 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                            select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var result = new List<object>();
            result.Add($"'Enero': '{enero}'," +
                      $"'Febrero': '{febrero}'," +
                      $"'Marzo': '{marzo}'," +
                      $"'Abril': '{abril}'," +
                      $"'Mayo': '{mayo}'," +
                      $"'Junio': '{junio}'," +
                      $"'Julio': '{julio}'," +
                      $"'Agosto': '{agosto}'," +
                      $"'Septiembre': '{septiembre}'," +
                      $"'Octubre': '{octubre}'," +
                      $"'Noviembre': '{novimebre}'," +
                      $"'Diciembre': '{diciembre}'");

            return Ok(result);
        }

        // PUT: api/InventarioInicialXDia_MatPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventarioInicialXDia_MatPrima(long id, InventarioInicialXDia_MatPrima inventarioInicialXDia_MatPrima)
        {
            if (id != inventarioInicialXDia_MatPrima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(inventarioInicialXDia_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioInicialXDia_MatPrimaExists(id))
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

        // POST: api/InventarioInicialXDia_MatPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventarioInicialXDia_MatPrima>> PostInventarioInicialXDia_MatPrima(InventarioInicialXDia_MatPrima inventarioInicialXDia_MatPrima)
        {
          if (_context.InventarioInicialXDias_MatPrima == null)
          {
              return Problem("Entity set 'dataContext.InventarioInicialXDias_MatPrima'  is null.");
          }
            _context.InventarioInicialXDias_MatPrima.Add(inventarioInicialXDia_MatPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InventarioInicialXDia_MatPrimaExists(inventarioInicialXDia_MatPrima.MatPri_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInventarioInicialXDia_MatPrima", new { id = inventarioInicialXDia_MatPrima.MatPri_Id }, inventarioInicialXDia_MatPrima);
        }

        // DELETE: api/InventarioInicialXDia_MatPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventarioInicialXDia_MatPrima(long id)
        {
            if (_context.InventarioInicialXDias_MatPrima == null)
            {
                return NotFound();
            }
            var inventarioInicialXDia_MatPrima = await _context.InventarioInicialXDias_MatPrima.FindAsync(id);
            if (inventarioInicialXDia_MatPrima == null)
            {
                return NotFound();
            }

            _context.InventarioInicialXDias_MatPrima.Remove(inventarioInicialXDia_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioInicialXDia_MatPrimaExists(long id)
        {
            return (_context.InventarioInicialXDias_MatPrima?.Any(e => e.MatPri_Id == id)).GetValueOrDefault();
        }
    }
}
