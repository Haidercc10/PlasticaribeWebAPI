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
    public class DetalleAsignacion_TintaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_TintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_Tinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_Tinta>>> GetDetalleAsignaciones_Tintas()
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return NotFound();
          }
            return await _context.DetalleAsignaciones_Tintas.ToListAsync();
        }

        // GET: api/DetalleAsignacion_Tinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_Tinta>> GetDetalleAsignacion_Tinta(long id)
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return NotFound();
          }
            var detalleAsignacion_Tinta = await _context.DetalleAsignaciones_Tintas.FindAsync(id);

            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            }

            return detalleAsignacion_Tinta;
        }


        // GET: api/DetalleAsignacion_Tinta/5
        [HttpGet("asignacion/{AsigMp_Id}")]
        public ActionResult<DetalleAsignacion_Tinta> GetDetalleAsignacion(long AsigMp_Id)
        {
            var detalleAsignacion_Tinta = _context.DetalleAsignaciones_Tintas.Where(dtAsg => dtAsg.AsigMp_Id == AsigMp_Id).ToList();

            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(detalleAsignacion_Tinta);
            }

        }


        /** Consulta para traer la información de tintas agrupadas */
        [HttpGet("AsignacionesTintasxOT/{AsigMP_OrdenTrabajo}")]
        public IActionResult GetAsignacion_TintaAgrupada(long AsigMP_OrdenTrabajo)
        {
            if (_context.DetalleAsignaciones_Tintas == null)
            {
                return NotFound();
            }

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var tinta_Asignada = _context.DetalleAsignaciones_Tintas
                
                .Where(da => da.AsigMp.AsigMP_OrdenTrabajo == AsigMP_OrdenTrabajo)               
                .Include(da => da.Tinta)              
                .Include(da => da.AsigMp)                
                .Include(da => da.Proceso)              
                .GroupBy(da => new { da.Tinta_Id, da.Tinta.Tinta_Nombre, da.UndMed_Id, da.Tinta.Tinta_Precio, da.Proceso.Proceso_Nombre }) 
                .Select(agr => new
                {
                    /** 'Key' hace referencia a los campos que están en el GroupBy */
                    agr.Key.Tinta_Id,
                    agr.Key.Tinta_Nombre,
                    Sum = agr.Sum(da => da.DtAsigTinta_Cantidad),
                    agr.Key.UndMed_Id,
                    agr.Key.Tinta_Precio,
                    agr.Key.Proceso_Nombre
                
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            if (tinta_Asignada == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tinta_Asignada);
            }
        }


        // GET: api/DetalleAsignacion_Tinta/5
        [HttpGet("SumaTintas_MatPrimasAsignadas/{OT}")]
        public ActionResult<DetalleAsignacion_Tinta> GetDetalleAsignacion2(long OT)
        {
            var union_Asignaciones = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == OT).Sum(da => da.DtAsigTinta_Cantidad);


            var union_Asignaciones2 = _context.DetallesAsignaciones_MateriasPrimas
               .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == OT).Sum(da => da.DtAsigMp_Cantidad);


            var union_Asignaciones3 = union_Asignaciones + union_Asignaciones2;

            return Ok(union_Asignaciones3);
            

        }


        /** Consulta por fecha de entrega */
        [HttpGet("MovTintasxFechaEntrega/{FechaInicial}")]
        public ActionResult Get(DateTime FechaInicial)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega == FechaInicial)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            
            return Ok(con);
        }


        /** Consulta por Estado */
        [HttpGet("MovTintasxEstado/{estado}")]
        public ActionResult Get(int estado)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.Estado_OrdenTrabajo == estado)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();

            return Ok(con);
        }


        /** Consulta por Tinta y Fecha Inicial */
        [HttpGet("MovTintasxIdxFechaEntrega/{nTinta}/{FechaInicial}")]
        public ActionResult GetMatPri(int nTinta, DateTime FechaInicial)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.Tinta_Id == nTinta
                       && dtAsg.AsigMp.AsigMp_FechaEntrega == FechaInicial)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();

            return Ok(con);
        }


        /** Consulta por OT */
        [HttpGet("MovTintasxOT/{ot}")]
        public ActionResult GetOT(long ot)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == ot)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            
            return Ok(con);
        }


        /** Consulta por Fechas */
        [HttpGet("MovTintasxFechas/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            
            return Ok(con);
        }


        /** Consulta por OT y Fechas */
        [HttpGet("MovTintasxOTxFechas/{Ot}/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(long Ot, DateTime FechaInicial, DateTime FechaFinal)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == Ot
                       && dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }


        /** Consulta por Estado y Fechas */
        [HttpGet("MovTintasxFechasxEstado/{FechaInicial}/{FechaFinal}/{estado}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal, int estado)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       && dtAsg.AsigMp.Estado_OrdenTrabajo == estado)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }


        /** Consulta por Tinta y Fechas */
        [HttpGet("MovTintasxFechasxTinta/{FechaInicial}/{FechaFinal}/{nTinta}")]
        public ActionResult Get8(DateTime FechaInicial, DateTime FechaFinal, int nTinta)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       && dtAsg.Tinta_Id == nTinta)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }


        /** Consulta por Tinta, estado y Fechas */
        [HttpGet("MovTintasxFechasxTintaxEstado/{FechaInicial}/{FechaFinal}/{nTintas}/{estado}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal, int nTintas, int estado)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       && dtAsg.Tinta_Id == nTintas
                       && dtAsg.AsigMp.Estado_OrdenTrabajo == estado)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }


        /** Consulta por OT, Tinta, estado y Fechas */
        [HttpGet("MovTintasxOTxFechasxTintaxEstado/{Ot}/{FechaInicial}/{FechaFinal}/{nTintas}/{estado}")]
        public ActionResult Get(long Ot, DateTime FechaInicial, DateTime FechaFinal, int nTintas, int estado)
        {
            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == Ot
                       && dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       && dtAsg.Tinta_Id == nTintas
                       && dtAsg.AsigMp.Estado_OrdenTrabajo == estado)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }


        /** Consulta por OT para PDF's */
        [HttpGet("pdfMovTintasxOT/{Ot}")]
        public ActionResult Get(long Ot)
        {

            var con = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == Ot)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp.AsigMp_Id,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.AsigMp_Observacion,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_Maquina,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.Tinta_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.UndMed_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.Proceso_Id,
                    dtAsg.Proceso.Proceso_Nombre
                })
                .ToList();

            return Ok(con);
        }

        // PUT: api/DetalleAsignacion_Tinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_Tinta(long id, DetalleAsignacion_Tinta detalleAsignacion_Tinta)
        {
            if (id != detalleAsignacion_Tinta.AsigMp_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_Tinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_TintaExists(id))
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

        // POST: api/DetalleAsignacion_Tinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_Tinta>> PostDetalleAsignacion_Tinta(DetalleAsignacion_Tinta detalleAsignacion_Tinta)
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return Problem("Entity set 'dataContext.DetalleAsignaciones_Tintas'  is null.");
          }
            _context.DetalleAsignaciones_Tintas.Add(detalleAsignacion_Tinta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_TintaExists(detalleAsignacion_Tinta.AsigMp_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_Tinta", new { id = detalleAsignacion_Tinta.AsigMp_Id }, detalleAsignacion_Tinta);
        }

        // DELETE: api/DetalleAsignacion_Tinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_Tinta(long id)
        {
            if (_context.DetalleAsignaciones_Tintas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_Tinta = await _context.DetalleAsignaciones_Tintas.FindAsync(id);
            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            }

            _context.DetalleAsignaciones_Tintas.Remove(detalleAsignacion_Tinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_TintaExists(long id)
        {
            return (_context.DetalleAsignaciones_Tintas?.Any(e => e.AsigMp_Id == id)).GetValueOrDefault();
        }
    }
}
