#nullable disable
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
    public class DesperdiciosController : ControllerBase
    {
        private readonly dataContext _context;

        public DesperdiciosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Desperdicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Desperdicio>>> GetDesperdicios()
        {
            return await _context.Desperdicios.ToListAsync();
        }      

        // GET: api/Desperdicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desperdicio>> GetDesperdicio(long id)
        {
            var Desperdicio = await _context.Desperdicios.FindAsync(id);

            if (Desperdicio == null)
            {
                return NotFound();
            }

            return Desperdicio;
        }

        [HttpGet("getUltimoPedido")]
        public ActionResult getUltimoPedido()
        {
            DateTime hora = Convert.ToDateTime("00:00:00");

            var desperdicioFecha = (from des in _context.Set<Desperdicio>()
                              orderby des.Desp_Id descending
                              select des.Desp_FechaRegistro).FirstOrDefault();

            var desperdicioHora = (from des in _context.Set<Desperdicio>()
                                   orderby des.Desp_Id descending
                                   select des.Desp_HoraRegistro).FirstOrDefault();

            var desperdicioUsuario = (from des in _context.Set<Desperdicio>()
                                  orderby des.Desp_Id descending
                                  select des.Usua_Id).FirstOrDefault();

            var con = from des in _context.Set<Desperdicio>()
                      from emp in _context.Set<Empresa>()
                      where emp.Empresa_Id == 800188730
                            && des.Desp_FechaRegistro == Convert.ToDateTime(desperdicioFecha).AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second)
                            && des.Desp_HoraRegistro == Convert.ToString(desperdicioHora)
                            && des.Usua_Id == Convert.ToInt64(desperdicioUsuario)
                      select new
                      {
                          des.Desp_Id,
                          des.Desp_FechaRegistro,
                          des.Desp_OT,
                          des.Activo.Actv_Serial,
                          des.Usuario1.Usua_Nombre,
                          des.Prod_Id,
                          des.Producto.Prod_Nombre,
                          des.Material.Material_Nombre,
                          des.Desp_Impresion,
                          des.Falla.Falla_Nombre,
                          des.Desp_PesoKg,
                          des.Desp_Observacion,
                          des.Desp_Fecha,
                          des.Proceso.Proceso_Nombre,
                          Creador = des.Usua_Id,
                          NombreCreador = des.Usuario2.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };

            return Ok(con);
        }

        // PUT: api/Desperdicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesperdicio(long id, Desperdicio Desperdicio)
        {
            if (id != Desperdicio.Desp_Id)
            {
                return BadRequest();
            }

            _context.Entry(Desperdicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesperdicioExists(id))
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

        // POST: api/Desperdicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Desperdicio>> PostDesperdicio(Desperdicio Desperdicio)
        {
            _context.Desperdicios.Add(Desperdicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDesperdicio", new { id = Desperdicio.Desp_Id }, Desperdicio);
        }

        // DELETE: api/Desperdicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesperdicio(long id)
        {
            var Desperdicio = await _context.Desperdicios.FindAsync(id);
            if (Desperdicio == null)
            {
                return NotFound();
            }

            _context.Desperdicios.Remove(Desperdicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DesperdicioExists(long id)
        {
            return _context.Desperdicios.Any(e => e.Desp_Id == id);
        }
    }
}
