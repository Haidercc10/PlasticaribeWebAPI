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
    public class DetalleAsignacion_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_MateriaPrima>>> GetDetallesAsignaciones_MateriasPrimas()
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.DetallesAsignaciones_MateriasPrimas.ToListAsync();
        }

        // GET: api/DetalleAsignacion_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_MateriaPrima>> GetDetalleAsignacion_MateriaPrima(long AsigMp_Id, long MatPri_Id)
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return NotFound();
          }
            var detalleAsignacion_MateriaPrima = await _context.DetallesAsignaciones_MateriasPrimas.FindAsync(AsigMp_Id, MatPri_Id);

            if (detalleAsignacion_MateriaPrima == null)
            {
                return NotFound();
            }

            return detalleAsignacion_MateriaPrima;
        }

        /*Materia Prima agrupada */
        [HttpGet("AsignacionesAgrupadas/{AsigMP_OrdenTrabajo}")] 
        public IActionResult GetAsignacion_MatPrimaAgrupada(long AsigMP_OrdenTrabajo)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var DetAsignacion_MatPrima = _context.DetallesAsignaciones_MateriasPrimas
                /** Consulta OT */
                .Where(da => da.AsigMp.AsigMP_OrdenTrabajo == AsigMP_OrdenTrabajo)
                /** Relación con Materia Prima a través de la Prop. navegación */
                .Include(da => da.MatPri)
                /** Relación con Asignaciones_MatPrima a través de la Prop. navegación */
                .Include(da => da.AsigMp)
                /** Relación con Proceso a través de la Prop. navegación */
                .Include(da => da.Proceso)
                /** Agrupar por ciertos campos */
                .GroupBy(da => new { da.MatPri_Id, da.MatPri.MatPri_Nombre, da.UndMed_Id, da.MatPri.MatPri_Precio, da.Proceso.Proceso_Nombre})
                /** Campos a mostrar */
                .Select(agr => new 
                {
                /** 'Key' hace referencia a los campos que están en el GroupBy */    
                      agr.Key.MatPri_Id,
                      agr.Key.MatPri_Nombre,
                      Sum = agr.Sum(da => da.DtAsigMp_Cantidad),
                      agr.Key.UndMed_Id,
                      agr.Key.MatPri_Precio,
                      agr.Key.Proceso_Nombre
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            if (DetAsignacion_MatPrima == null)
            {
                return NotFound();
            } else
            {
                return Ok(DetAsignacion_MatPrima);
            }

            
        }

        /* Materia Prima agrupada con OT Consultada para movimientos. */
        [HttpGet("AsignacionesAgrupadasXvalores/{AsigMP_OrdenTrabajo}")]
        public IActionResult GetAsignacion_MatPrimaValores(long AsigMP_OrdenTrabajo)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var DetAsignacion_MatPrima = _context.DetallesAsignaciones_MateriasPrimas
                /** Consulta OT */
                .Where(da => da.AsigMp.AsigMP_OrdenTrabajo == AsigMP_OrdenTrabajo)
                /** Relación con Materia Prima a través de la Prop. navegación */
                .Include(da => da.MatPri)
                /** Relación con Asignaciones_MatPrima a través de la Prop. navegación */
                .Include(da => da.AsigMp)
                /** Relación con Proceso a través de la Prop. navegación */
                .Include(da => da.Proceso)
                
                /** Agrupar por ciertos campos */
                .GroupBy(da => new { da.MatPri_Id, da.MatPri.MatPri_Nombre, da.UndMed_Id, 
                                     da.MatPri.MatPri_Precio, da.Proceso.Proceso_Nombre, 
                                     da.AsigMp.AsigMp_FechaEntrega, da.AsigMp.Usua.Usua_Nombre})
                /** Campos a mostrar */
                .Select(agr => new
                {
                    /** 'Key' hace referencia a los campos que están en el GroupBy */
                    agr.Key.MatPri_Id,
                    agr.Key.MatPri_Nombre,
                    agr.Key.AsigMp_FechaEntrega,
                    agr.Key.Usua_Nombre,
                    Sum = agr.Sum(da => da.DtAsigMp_Cantidad),
                    agr.Key.UndMed_Id,
                    agr.Key.MatPri_Precio,
                    agr.Key.Proceso_Nombre
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            if (DetAsignacion_MatPrima == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(DetAsignacion_MatPrima);
            }
        }

        [HttpGet("asignacion/{AsigMp_Id}")]
        public ActionResult<DetalleAsignacion_MateriaPrima> GetIdAsignacion(long AsigMp_Id)
        {
            var detallesAsignacion = _context.DetallesAsignaciones_MateriasPrimas.Where(dtA => dtA.AsigMp_Id == AsigMp_Id).ToList();

            if (detallesAsignacion == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(detallesAsignacion);
            }
        }

        [HttpGet("matPri/{MatPri_Id}")]
        public ActionResult<DetalleAsignacion_MateriaPrima> GetIdMateriaPrima(long MatPri_Id)
        {
            var detallesAsignacion = _context.DetallesAsignaciones_MateriasPrimas.Where (dtA => dtA.MatPri_Id == MatPri_Id).ToList();

            if (detallesAsignacion == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(detallesAsignacion);
            }
        }

        // PUT: api/DetalleAsignacion_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_MateriaPrima(long AsigMp_Id, long MatPri_Id, DetalleAsignacion_MateriaPrima detalleAsignacion_MateriaPrima)
        {
            if (AsigMp_Id != detalleAsignacion_MateriaPrima.AsigMp_Id && MatPri_Id != detalleAsignacion_MateriaPrima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_MateriaPrimaExists(AsigMp_Id) && !DetalleAsignacion_MateriaPrimaExists(MatPri_Id))
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

        // POST: api/DetalleAsignacion_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_MateriaPrima>> PostDetalleAsignacion_MateriaPrima(DetalleAsignacion_MateriaPrima detalleAsignacion_MateriaPrima)
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.DetallesAsignaciones_MateriasPrimas'  is null.");
          }
            _context.DetallesAsignaciones_MateriasPrimas.Add(detalleAsignacion_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_MateriaPrimaExists(detalleAsignacion_MateriaPrima.AsigMp_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_MateriaPrima", new { id = detalleAsignacion_MateriaPrima.AsigMp_Id }, detalleAsignacion_MateriaPrima);
        }

        // DELETE: api/DetalleAsignacion_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_MateriaPrima(long id)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_MateriaPrima = await _context.DetallesAsignaciones_MateriasPrimas.FindAsync(id);
            if (detalleAsignacion_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_MateriasPrimas.Remove(detalleAsignacion_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_MateriaPrimaExists(long id)
        {
            return (_context.DetallesAsignaciones_MateriasPrimas?.Any(e => e.AsigMp_Id == id)).GetValueOrDefault();
        }
    }
}
