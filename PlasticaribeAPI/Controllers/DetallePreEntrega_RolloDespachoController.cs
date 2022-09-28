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
    public class DetallePreEntrega_RolloDespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public DetallePreEntrega_RolloDespachoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetallePreEntrega_RolloDespacho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePreEntrega_RolloDespacho>>> GetDetallesPreEntrega_RollosDespacho()
        {
            return await _context.DetallesPreEntrega_RollosDespacho.ToListAsync();
        }

        // GET: api/DetallePreEntrega_RolloDespacho/5
        [HttpGet("{id}")]

        [HttpGet("VerificarRollo/{id}")]
        public ActionResult GetVerificarRollo(long id)
        {
            var con = _context.DetallesPreEntrega_RollosDespacho.Where(x => x.Rollo_Id == id).ToList();
            return Ok(con);
        }
        public async Task<ActionResult<DetallePreEntrega_RolloDespacho>> GetDetallePreEntrega_RolloDespacho(long id)
        {
            var detallePreEntrega_RolloDespacho = await _context.DetallesPreEntrega_RollosDespacho.FindAsync(id);

            if (detallePreEntrega_RolloDespacho == null)
            {
                return NotFound();
            }

            return detallePreEntrega_RolloDespacho;
        }

        [HttpGet("CrearPDFUltimoID/{id}")]
        public ActionResult Get(long id )
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dt in _context.Set<DetallePreEntrega_RolloDespacho>()
                      from emp in _context.Set<Empresa>()
                      where dt.PreEntregaRollo.PreEntRollo_Id == id
                      orderby dt.PreEntregaRollo.PreEntRollo_Id
                      select new {
                          dt.PreEntRollo_Id,
                          dt.PreEntregaRollo.PreEntRollo_Fecha,
                          dt.PreEntregaRollo.PreEntRollo_Observacion,
                          dt.PreEntregaRollo.Usua_Id,
                          dt.PreEntregaRollo.Usuario.Usua_Nombre,
                          dt.Rollo_Id,
                          dt.Prod_Id,
                          dt.Prod.Prod_Nombre,
                          dt.UndMed_Rollo,
                          dt.DtlPreEntRollo_Cantidad,
                          dt.DtlPreEntRollo_OT,
                          dt.Cli_Id,
                          dt.Cliente.Cli_Nombre,
                          dt.Proceso_Id,
                          dt.Proceso.Proceso_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // PUT: api/DetallePreEntrega_RolloDespacho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallePreEntrega_RolloDespacho(long id, DetallePreEntrega_RolloDespacho detallePreEntrega_RolloDespacho)
        {
            if (id != detallePreEntrega_RolloDespacho.DtlPreEntRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(detallePreEntrega_RolloDespacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePreEntrega_RolloDespachoExists(id))
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

        // POST: api/DetallePreEntrega_RolloDespacho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallePreEntrega_RolloDespacho>> PostDetallePreEntrega_RolloDespacho(DetallePreEntrega_RolloDespacho detallePreEntrega_RolloDespacho)
        {
            _context.DetallesPreEntrega_RollosDespacho.Add(detallePreEntrega_RolloDespacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallePreEntrega_RolloDespacho", new { id = detallePreEntrega_RolloDespacho.DtlPreEntRollo_Id }, detallePreEntrega_RolloDespacho);
        }

        // DELETE: api/DetallePreEntrega_RolloDespacho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallePreEntrega_RolloDespacho(long id)
        {
            var detallePreEntrega_RolloDespacho = await _context.DetallesPreEntrega_RollosDespacho.FindAsync(id);
            if (detallePreEntrega_RolloDespacho == null)
            {
                return NotFound();
            }

            _context.DetallesPreEntrega_RollosDespacho.Remove(detallePreEntrega_RolloDespacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallePreEntrega_RolloDespachoExists(long id)
        {
            return _context.DetallesPreEntrega_RollosDespacho.Any(e => e.DtlPreEntRollo_Id == id);
        }
    }
}
