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
    public class BOPPController : ControllerBase
    {
        private readonly dataContext _context;

        public BOPPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/BOPP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BOPP>>> GetBOPP()
        {
          if (_context.BOPP == null)
          {
              return NotFound();
          }
            return await _context.BOPP.ToListAsync();
        }

        // GET: api/BOPP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BOPP>> GetBOPP(long id)
        {
          if (_context.BOPP == null)
          {
              return NotFound();
          }
            var bOPP = await _context.BOPP.FindAsync(id);

            if (bOPP == null)
            {
                return NotFound();
            }

            return bOPP;
        }

        /** Obtener BOPP consultado por serial */
        [HttpGet("serial/{BOPP_Serial}")]
        public ActionResult<BOPP> GetSerial(long BOPP_Serial)
        {
            var bOPP = _context.BOPP.Where(bopp => bopp.BOPP_Serial == BOPP_Serial).ToList();

            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }

        [HttpGet("fecha/{BOPP_FechaIngreso}")]
        public ActionResult<BOPP> GetFecha(DateTime BOPP_FechaIngreso)
        {
            var bOPP = _context.BOPP.Where(bopp => bopp.BOPP_FechaIngreso == BOPP_FechaIngreso).ToList();

            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }

        /** Get para contar la cantidad de unidades, precio total segun existencias 
y cantidad en Kilos agrupados BOPP por Nombre */
        [HttpGet("BoppAgrupado")]
        public ActionResult<BOPP> GetBoppAgrupado()
        {

            /** Consulta la tabla de BOPP Agrupa por descripción */
            var bOPP = _context.BOPP.GroupBy(x => new {x.BOPP_Descripcion, x.BOPP_CantidadMicras, x.BOPP_Ancho})
            /** Selecciona los campos Descripción, Cantidad Micras, Suma el Precio total, Suma los Kilos, Cuenta cantidad de cada BOPP */
                                    .Select(bopp => new
                                    {
                                        bopp.Key.BOPP_Descripcion,
                                        bopp.Key.BOPP_CantidadMicras,
                                        bopp.Key.BOPP_Ancho,
                                        sumaPrecio = bopp.Sum(x => x.BOPP_Precio),
                                        sumaKilosIngresados = bopp.Sum(x => x.BOPP_CantidadInicialKg),
                                        sumaKilosActual = bopp.Sum(x => x.BOPP_Stock),
                                        conteoDescripcion = bopp.Count() 
                                    })
                                    .ToList();
            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }

        [HttpGet("fechas/")]
        public ActionResult<BOPP> GetFechas(DateTime BOPP_FechaIngreso1, DateTime BOPP_FechaIngreso2)
        {
            var bOPP = _context.BOPP.Where(bopp => bopp.BOPP_FechaIngreso >= BOPP_FechaIngreso1 && bopp.BOPP_FechaIngreso <= BOPP_FechaIngreso2).ToList();

            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }

        [HttpGet("consultaMovimientos0/{FechaInicial}")]
        public ActionResult Get(DateTime FechaInicial)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.BOPP
                .Where(BOPP => BOPP.BOPP_FechaIngreso == FechaInicial)
                .Select(BOPP => new
                {
                    BOPP.BOPP_Id,
                    BOPP.BOPP_Serial,
                    BOPP.BOPP_FechaIngreso,
                    BOPP.BOPP_Nombre,
                    BOPP.Usua.Usua_Nombre,
                    BOPP.Usua_Id,
                    BOPP.BOPP_CantidadInicialKg
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("consultaMovimientos1/{Bopp}/{FechaInicial}")]
        public ActionResult GetMatPri(long Bopp, DateTime FechaInicial)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.BOPP
                .Where(dtAsg => dtAsg.BOPP_Serial == Bopp
                       && dtAsg.BOPP_FechaIngreso == FechaInicial)
                .Select(BOPP => new
                {
                    BOPP.BOPP_Id,
                    BOPP.BOPP_Serial,
                    BOPP.BOPP_FechaIngreso,
                    BOPP.BOPP_Nombre,
                    BOPP.Usua.Usua_Nombre,
                    BOPP.Usua_Id,
                    BOPP.BOPP_CantidadInicialKg
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("consultaMovimientos2/{Bopp}")]
        public ActionResult Get(long Bopp)
        {
            var con = _context.BOPP
                .Where(dtAsg => dtAsg.BOPP_Serial == Bopp)
                .Select(BOPP => new
                {
                    BOPP.BOPP_Id,
                    BOPP.BOPP_Serial,
                    BOPP.BOPP_FechaIngreso,
                    BOPP.BOPP_Nombre,
                    BOPP.Usua.Usua_Nombre,
                    BOPP.Usua_Id,
                    BOPP.BOPP_CantidadInicialKg
                }).ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos3/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.BOPP
                .Where(dtAsg => dtAsg.BOPP_FechaIngreso >= FechaInicial
                       && dtAsg.BOPP_FechaIngreso <= FechaFinal)
                .Select(BOPP => new
                {
                    BOPP.BOPP_Id,
                    BOPP.BOPP_Serial,
                    BOPP.BOPP_FechaIngreso,
                    BOPP.BOPP_Nombre,
                    BOPP.Usua.Usua_Nombre,
                    BOPP.Usua_Id,
                    BOPP.BOPP_CantidadInicialKg
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("consultaMovimientos4/{Bopp}/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(long Bopp, DateTime FechaInicial, DateTime FechaFinal)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.BOPP
                .Where(dtAsg => dtAsg.BOPP_Serial == Bopp
                       && dtAsg.BOPP_FechaIngreso >= FechaInicial
                       && dtAsg.BOPP_FechaIngreso <= FechaFinal)
                .Select(BOPP => new
                {
                    BOPP.BOPP_Id,
                    BOPP.BOPP_Serial,
                    BOPP.BOPP_FechaIngreso,
                    BOPP.BOPP_Nombre,
                    BOPP.Usua.Usua_Nombre,
                    BOPP.Usua_Id,
                    BOPP.BOPP_CantidadInicialKg
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("consultaMovimientos5/{FechaInicial}/{FechaFinal}/{Bopp}")]
        public ActionResult Get8(DateTime FechaInicial, DateTime FechaFinal, int Bopp)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.BOPP
                .Where(dtAsg => dtAsg.BOPP_FechaIngreso >= FechaInicial
                       && dtAsg.BOPP_FechaIngreso <= FechaFinal
                       && dtAsg.BOPP_Id == Bopp)
                .Select(BOPP => new
                {
                    BOPP.BOPP_Id,
                    BOPP.BOPP_Serial,
                    BOPP.BOPP_FechaIngreso,
                    BOPP.BOPP_Nombre,
                    BOPP.Usua.Usua_Nombre,
                    BOPP.Usua_Id,
                    BOPP.BOPP_CantidadInicialKg
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }


        [HttpGet("consultaMovimientos6/{Bopp}/{FechaInicial}")]
        public ActionResult Get9(int Bopp, DateTime FechaInicial)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.BOPP
                .Where(dtAsg => dtAsg.BOPP_Id == Bopp
                       && dtAsg.BOPP_FechaIngreso == FechaInicial)
                .Select(BOPP => new
                {
                    BOPP.BOPP_Id,
                    BOPP.BOPP_Serial,
                    BOPP.BOPP_FechaIngreso,
                    BOPP.BOPP_Nombre,
                    BOPP.Usua.Usua_Nombre,
                    BOPP.Usua_Id,
                    BOPP.BOPP_CantidadInicialKg
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        /*[HttpGet("pdfMovimientos/{Ot}")]
        public ActionResult Get(long Ot)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
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
                    dtAsg.MatPri_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.UndMed_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.Proceso_Id,
                    dtAsg.Proceso.Proceso_Nombre
                })
                .ToList();
            return Ok(con);
        }
        */

        // PUT: api/BOPP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBOPP(long id, BOPP bOPP)
        {
            if (id != bOPP.BOPP_Id)
            {
                return BadRequest();
            }

            _context.Entry(bOPP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOPPExists(id))
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

        // POST: api/BOPP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BOPP>> PostBOPP(BOPP bOPP)
        {
          if (_context.BOPP == null)
          {
              return Problem("Entity set 'dataContext.BOPP'  is null.");
          }
            _context.BOPP.Add(bOPP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBOPP", new { id = bOPP.BOPP_Id }, bOPP);
        }

        // DELETE: api/BOPP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBOPP(long id)
        {
            if (_context.BOPP == null)
            {
                return NotFound();
            }
            var bOPP = await _context.BOPP.FindAsync(id);
            if (bOPP == null)
            {
                return NotFound();
            }

            _context.BOPP.Remove(bOPP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BOPPExists(long id)
        {
            return (_context.BOPP?.Any(e => e.BOPP_Id == id)).GetValueOrDefault();
        }
    }
}
