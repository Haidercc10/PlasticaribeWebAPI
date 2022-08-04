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
    public class DetalleAsignacion_BOPPController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_BOPPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_BOPP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_BOPP>>> GetDetallesAsignaciones_BOPP()
        {
          if (_context.DetallesAsignaciones_BOPP == null)
          {
              return NotFound();
          }
            return await _context.DetallesAsignaciones_BOPP.ToListAsync();
        }

        // GET: api/DetalleAsignacion_BOPP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_BOPP>> GetDetalleAsignacion_BOPP(long id)
        {
          if (_context.DetallesAsignaciones_BOPP == null)
          {
              return NotFound();
          }
            var detalleAsignacion_BOPP = await _context.DetallesAsignaciones_BOPP.FindAsync(id);

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            return detalleAsignacion_BOPP;
        }

        [HttpGet("asignacion/{AsigBOPP_Id}")]
        public ActionResult<DetalleAsignacion_BOPP> GetAsignacion(long AsigBOPP_Id)
        {
            var detalleAsignacion_BOPP = _context.DetallesAsignaciones_BOPP.Where(dtAsg => dtAsg.AsigBOPP_Id == AsigBOPP_Id).ToList();

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(detalleAsignacion_BOPP);
            }
        }

        [HttpGet("BOPP/{BOPP_Id}")]
        public ActionResult<DetalleAsignacion_BOPP> GetBOPP(long BOPP_Id)
        {
            var detalleAsignacion_BOPP = _context.DetallesAsignaciones_BOPP.Where(dtAsg => dtAsg.BOPP_Id == BOPP_Id).ToList();

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(detalleAsignacion_BOPP);
            }
        }

        [HttpGet("OT/{DtAsigBOPP_OrdenTrabajo}")]
        public ActionResult<DetalleAsignacion_BOPP> GetOT(long DtAsigBOPP_OrdenTrabajo)
        {
            var detalleAsignacion_BOPP = _context.DetallesAsignaciones_BOPP.Where(dtAsg => dtAsg.DtAsigBOPP_OrdenTrabajo == DtAsigBOPP_OrdenTrabajo).ToList();

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(detalleAsignacion_BOPP);
            }
        }

        [HttpGet("estadoOT/{Estado_OrdenTrabajo}")]
        public ActionResult<DetalleAsignacion_BOPP> GetEstadoOT(int Estado_OrdenTrabajo)
        {

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var DetalleAsignacion_BOPP = _context.DetallesAsignaciones_BOPP
                /** Consulta Estado de la OT */
                .Where(da => da.Estado_OrdenTrabajo == Estado_OrdenTrabajo)
                /** Relación con BOPP a través de la Prop. navegación */
                .Include(da => da.BOPP)
                /* Relación con Asignaciones de BOPP a través de la Prop. navegación*/
                .Include(da => da.AsigBOPP)
                /** Relación con Proceso a través de la Prop. navegación */
                .Include(da => da.Proceso)
                /** Agrupar por ciertos campos */
                /** Agrupar por ciertos campos */
                .GroupBy(da => new {
                    da.BOPP_Id,
                    da.BOPP.BOPP_Nombre,
                    da.UndMed_Id,
                    da.BOPP.BOPP_Precio,
                    da.Proceso.Proceso_Nombre,
                    da.AsigBOPP.AsigBOPP_FechaEntrega,
                    da.AsigBOPP.Usua.Usua_Nombre,
                    da.AsigBOPP_Id,
                    da.Estado_OrdenTrabajo,
                    da.DtAsigBOPP_OrdenTrabajo
                })
                /** Campos a mostrar */
                .Select(agr => new
                {
                    /** 'Key' hace referencia a los campos que están en el GroupBy */
                    agr.Key.BOPP_Id,
                    agr.Key.BOPP_Nombre,
                    agr.Key.AsigBOPP_FechaEntrega,
                    agr.Key.Usua_Nombre,
                    Sum = agr.Sum(da => da.DtAsigBOPP_Cantidad),
                    agr.Key.UndMed_Id,
                    agr.Key.BOPP_Precio,
                    agr.Key.Proceso_Nombre,
                    agr.Key.AsigBOPP_Id,
                    agr.Key.Estado_OrdenTrabajo,
                    agr.Key.DtAsigBOPP_OrdenTrabajo
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            if (DetalleAsignacion_BOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(DetalleAsignacion_BOPP);
            }
        }


        /** Consulta Serial de BOPP en asignaciones */
        /* [HttpGet("AsignacionesXSerial/{BOPP_Serial}")]
         public ActionResult<DetalleAsignacion_BOPP> GetAsignacionXSerial(string BOPP_Serial)
         {
             if (_context.Asignaciones_BOPP == null)
             {
                 return NotFound();
             }


             var asignacionXSerial = _context.DetallesAsignaciones_BOPP.Where(asgBopp => asgBopp.BOPP.BOPP_Serial == BOPP_Serial &&

                                                                             asgBopp.AsigBOPP.Usua_Id == asgBopp.AsigBOPP.Usua.Usua_Id)

                                                                       .Include(u => u.AsigBOPP.Usua)

                                                                       .Select(x => new
                                                                       {

                                                                           x.AsigBOPP.AsigBOPP_FechaEntrega,
                                                                           x.AsigBOPP.Usua.Usua_Nombre,
                                                                           x.BOPP.BOPP_Nombre,
                                                                           x.DtAsigBOPP_Cantidad
                                                                       });                                                                   
                                                                       .ToList();
             ;
             if (asignacionXSerial == null)
             {
                 return NotFound();
             }
             else
             {
                 return Ok(asignacionXSerial);
             }
         }*/


        // PUT: api/DetalleAsignacion_BOPP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_BOPP(long id, DetalleAsignacion_BOPP detalleAsignacion_BOPP)
        {
            if (id != detalleAsignacion_BOPP.AsigBOPP_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_BOPP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_BOPPExists(id))
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

        // POST: api/DetalleAsignacion_BOPP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_BOPP>> PostDetalleAsignacion_BOPP(DetalleAsignacion_BOPP detalleAsignacion_BOPP)
        {
          if (_context.DetallesAsignaciones_BOPP == null)
          {
              return Problem("Entity set 'dataContext.DetallesAsignaciones_BOPP'  is null.");
          }
            _context.DetallesAsignaciones_BOPP.Add(detalleAsignacion_BOPP);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_BOPPExists(detalleAsignacion_BOPP.AsigBOPP_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_BOPP", new { id = detalleAsignacion_BOPP.AsigBOPP_Id }, detalleAsignacion_BOPP);
        }

        // DELETE: api/DetalleAsignacion_BOPP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_BOPP(long id)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            var detalleAsignacion_BOPP = await _context.DetallesAsignaciones_BOPP.FindAsync(id);
            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_BOPP.Remove(detalleAsignacion_BOPP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: Eliminar Detalle de asignación de BOPP por OT y asignación. 
        [HttpDelete("EliminarXOT_AsignacionBOPP")]
        public ActionResult DeleteIdDetalleAsignacion_BOPP(long AsigBOPP_Id, long DtAsigBOPP_OrdenTrabajo)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            var detalleAsignacion_BOPP =  _context.DetallesAsignaciones_BOPP.Where(x => x.AsigBOPP_Id == AsigBOPP_Id &&
                                                                                   x.DtAsigBOPP_OrdenTrabajo == DtAsigBOPP_OrdenTrabajo
                                                                                   ).ToList();
           
            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_BOPP.RemoveRange(detalleAsignacion_BOPP);
            _context.SaveChangesAsync();

            return NoContent();
        }


        /** Eliminar Detalle de asignación de BOPP por Asignación y BOPP */
        [HttpDelete("EliminarXAsignacion_BOPP")]
        public ActionResult DeleteEnDetalleAsignacion_BOPP(long AsigBOPP_Id, long BOPP_Id)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            var detalleAsignacion_BOPP = _context.DetallesAsignaciones_BOPP.Where(x => x.AsigBOPP_Id == AsigBOPP_Id &&
                                                                                  x.BOPP_Id == BOPP_Id
                                                                                  ).ToList();

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_BOPP.RemoveRange(detalleAsignacion_BOPP);
            _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DetalleAsignacion_BOPPExists(long id)
        {
            return (_context.DetallesAsignaciones_BOPP?.Any(e => e.AsigBOPP_Id == id)).GetValueOrDefault();
        }
    }
}
