using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Certificados_CalidadController : ControllerBase
    {
        private readonly dataContext _context;

        public Certificados_CalidadController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Certificados_Calidad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificados_Calidad>>> GetCertificados_Calidad()
        {
          if (_context.Certificados_Calidad == null)
          {
              return NotFound();
          }
            return await _context.Certificados_Calidad.ToListAsync();
        }

        // GET: api/Certificados_Calidad/5
        [HttpGet("{id}")]
        public ActionResult GetCertificados_Calidad(long id)
        {
            var certificados_Calidad = (from cc in _context.Set<Certificados_Calidad>() where cc.Consecutivo == id select cc).FirstOrDefault();
            return Ok(certificados_Calidad);
        }


        //Consulta que devolverá los diferentes materiales utilizados en los certificados
        [HttpGet("getMateriales")]
        public ActionResult GetMateriales()
        {
            return Ok((from cc in _context.Set<Certificados_Calidad>() select cc.Material).Distinct());
        }

        // Funcion que retornará la información del ultimo certificado realizado a un Item
        [HttpGet("getUltCertificadoItem/{item}")]
        public ActionResult GetUltCertificadoItem(long item)
        {
            return Ok((from cc in _context.Set<Certificados_Calidad>() where cc.Item == item orderby cc.Consecutivo descending select cc).FirstOrDefault());
        }

        // Get Certificados
        [HttpGet("GetCertificados/{fecha1}/{fecha2}")]
        public ActionResult GetCertificados(DateTime fecha1, DateTime fecha2, string? consec = "", string? ot = "", string? cliente = "", string? referencia = "")
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var certificados_Calidad = from c in _context.Set<Certificados_Calidad>()
                                       where c.Fecha_Registro >= fecha1 &&
                                             c.Fecha_Registro <= fecha2 &&
                                             Convert.ToString(c.Consecutivo).Contains(consec) &&
                                             Convert.ToString(c.Orden_Trabajo).Contains(ot) &&
                                             Convert.ToString(c.Cliente).Contains(cliente) &&
                                             Convert.ToString(c.Referencia).Contains(referencia)
                                       select c;
#pragma warning restore CS8604 // Possible null reference argument.

            if (certificados_Calidad == null) return BadRequest("No se encontraron certificados con los datos consultados!");
            return Ok(certificados_Calidad);
        }

        // Get Clientes
        [HttpGet("getClientes/{cliente}")]
        public ActionResult GetClientes(string cliente)
        {
            var clientes = (from c in _context.Set<Certificados_Calidad>()
                           where c.Cliente.Contains(cliente)
                           select c.Cliente).Distinct();

            if (clientes == null) return BadRequest("El cliente solicitado no tiene certificados creados!");
            return Ok(clientes);

        }

        // Get Items
        [HttpGet("getItems/{item}")]
        public ActionResult GetItems(string item)
        {
            var items = (from c in _context.Set<Certificados_Calidad>()
                        where c.Referencia.Contains(item)
                        select new { c.Item, c.Referencia }).Distinct();

            if (items == null) return BadRequest("El item solicitado no tiene certificados creados!");
            return Ok(items);
        }

        // Get Parametros cualitativos
        [HttpGet("getParametrosCualitativos/{consecutivo}")]
        public ActionResult GetParametrosCualitativos(long consecutivo)
        {
            var cualitativos = _context.Certificados_Calidad.Where(c => c.Consecutivo == consecutivo).Select(c => new
            {
                Calibre = new { 
                   Parametro = "Calibre",
                   Unidad = c.Unidad_Calibre,
                   Nominal = c.Nominal_Calibre, 
                   Tolerancia = c.Tolerancia_Calibre, 
                   Minimo = c.Minimo_Calibre, 
                   Maximo = c.Maximo_Calibre,
                },
                AnchoFrente = new
                {
                    Parametro = "Ancho Frente",
                    Unidad = c.Unidad_AnchoFrente,
                    Nominal = c.Nominal_AnchoFrente,
                    Tolerancia = c.Tolerancia_AnchoFrente,
                    Minimo = c.Minimo_AnchoFrente,
                    Maximo = c.Maximo_AnchoFrente,
                },
                AnchoFuelle = new
                {
                    Parametro = "Ancho Fuelle",
                    Unidad = c.Unidad_AnchoFuelle,
                    Nominal = c.Nominal_AnchoFuelle,
                    Tolerancia = c.Tolerancia_AnchoFuelle,
                    Minimo = c.Minimo_AnchoFuelle,
                    Maximo = c.Maximo_AnchoFuelle,
                }, 
                LargoRepeticion = new
                {
                    Parametro = "Largo Repetición",
                    Unidad = c.Unidad_LargoRepeticion,
                    Nominal = c.Nominal_LargoRepeticion,
                    Tolerancia = c.Tolerancia_LargoRepeticion,
                    Minimo = c.Minimo_LargoRepeticion,
                    Maximo = c.Maximo_LargoRepeticion,
                },
                Cof = new
                {
                    Parametro = "COF",
                    Unidad = c.Unidad_Cof,
                    Nominal = c.Nominal_Cof,
                    Tolerancia = c.Tolerancia_Cof,
                    Minimo = c.Minimo_Cof,
                    Maximo = c.Maximo_Cof,
                }
            });

            var result = cualitativos.Select(pc => pc.Calibre)
                .Concat(cualitativos.Select(pc => pc.AnchoFrente)
                .Concat(cualitativos.Select(pc => pc.AnchoFuelle)
                .Concat(cualitativos.Select(pc => pc.LargoRepeticion)
                .Concat(cualitativos.Select(pc => pc.Cof)))));

            if (result == null) return BadRequest("No se encontraron registros del consecutivo ingresado!");
            else return Ok(result);        

        }

        // PUT: api/Certificados_Calidad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificados_Calidad(long id, Certificados_Calidad certificados_Calidad)
        {
            if (id != certificados_Calidad.Consecutivo)
            {
                return BadRequest();
            }

            _context.Entry(certificados_Calidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Certificados_CalidadExists(id))
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

        // POST: api/Certificados_Calidad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Certificados_Calidad>> PostCertificados_Calidad(Certificados_Calidad certificados_Calidad)
        {
          if (_context.Certificados_Calidad == null)
          {
              return Problem("Entity set 'dataContext.Certificados_Calidad'  is null.");
          }
            _context.Certificados_Calidad.Add(certificados_Calidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificados_Calidad", new { id = certificados_Calidad.Consecutivo }, certificados_Calidad);
        }

        // DELETE: api/Certificados_Calidad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificados_Calidad(long id)
        {
            if (_context.Certificados_Calidad == null)
            {
                return NotFound();
            }
            var certificados_Calidad = await _context.Certificados_Calidad.FindAsync(id);
            if (certificados_Calidad == null)
            {
                return NotFound();
            }

            _context.Certificados_Calidad.Remove(certificados_Calidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Certificados_CalidadExists(long id)
        {
            return (_context.Certificados_Calidad?.Any(e => e.Consecutivo == id)).GetValueOrDefault();
        }
    }
}
