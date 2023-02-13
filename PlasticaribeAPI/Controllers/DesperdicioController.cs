#nullable disable
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
                      where emp.Empresa_Id == 800188732
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


        /** OT */
        [HttpGet("getConsultaDesperdicioxOT/{OT}")]
        public ActionResult<Desperdicio> GetDesperdicioOT(long OT)
        {
            var Desperdicio = (from d in _context.Set<Desperdicio>()
                               from e in _context.Set<Empresa>()
                               where d.Desp_OT == OT &&
                               e.Empresa_Id == 800188732
                               select new
                               {
                                   d.Desp_Id,
                                   d.Desp_OT,
                                   d.Prod_Id,
                                   d.Desp_PesoKg,
                                   d.Producto.Prod_Nombre,
                                   d.Proceso_Id,
                                   d.Proceso.Proceso_Nombre,
                                   d.Material_Id,
                                   d.Material.Material_Nombre,
                                   d.Falla_Id,
                                   d.Falla.Falla_Nombre,
                                   d.Desp_Impresion,
                                   d.Actv_Id,
                                   d.Activo.Actv_Nombre,
                                   d.Activo.Actv_Serial,
                                   Operario = d.Usua_Operario,
                                   d.Usuario1.Usua_Nombre,
                                   d.Usua_Id,
                                   Usuario = d.Usuario2.Usua_Nombre, 
                                   d.Desp_Fecha, 
                                   d.Desp_Observacion,
                                   d.Desp_FechaRegistro,
                                   d.Desp_HoraRegistro,
                                   e.Empresa_Id,
                                   e.Empresa_Nombre,
                                   e.Empresa_Ciudad,
                                   e.Empresa_Direccion
                               }).ToList();
            
            //if (Desperdicio == null) return NotFound();           
            return Ok(Desperdicio);
        }


        /** OT */
        [HttpGet("GetDesperdicioOt/{Ot}")]
        public ActionResult<Desperdicio> GetDesperdicioOt(long Ot)
        {
            var con = from des in _context.Set<Desperdicio>()
                      from emp in _context.Set<Empresa>()
                      where emp.Empresa_Id == 800188732
                            && des.Desp_OT == Ot
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

            //if (Desperdicio == null) return NotFound();           
            return Ok(con);
        }

        /** OT */
        [HttpGet("getConsultaDesperdicio2/{fecha1}/{fecha2}")]
#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        public ActionResult<Desperdicio> GetDesperdicios(DateTime fecha1, DateTime fecha2, string? OT = "", string? material = "", string? item = "")
#pragma warning restore CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        {
            var Desperdicio = (from d in _context.Set<Desperdicio>()
                               where d.Desp_Fecha >= fecha1 &&
                               d.Desp_Fecha <= fecha2 &&
                               Convert.ToString(d.Desp_OT).Contains(OT) &&
                               Convert.ToString(d.Material_Id).Contains(material) &&
                               Convert.ToString(d.Prod_Id).Contains(item)
                               group d by  new { d.Desp_OT, d.Desp_Impresion, d.Prod_Id, d.Producto.Prod_Nombre, d.Material_Id, d.Material.Material_Nombre } into grupo
                               select new
                               { 
                                  OT = grupo.Key.Desp_OT,
                                  Item = grupo.Key.Prod_Id,
                                  NombreItem = grupo.Key.Prod_Nombre,
                                  Material = grupo.Key.Material_Nombre,
                                  Impreso = grupo.Key.Desp_Impresion,
                                  PesoTotal = grupo.Sum(dd => dd.Desp_PesoKg)
                               }).ToList();
            
            //if (Desperdicio == null) return NotFound();           
            return Ok(Desperdicio);
        }

        /** Movimientos Desperdicios*/
        [HttpGet("getMovDesperdicios/{fecha1}/{fecha2}")]
#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        public ActionResult<Desperdicio> GetDesperdicios(DateTime fecha1, DateTime fecha2, string? OT = "", string? material = "", string? item = "", string? falla = "", string? proceso = "", string? maquina = "")
#pragma warning restore CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        {
            var Desperdicio = (from des in _context.Set<Desperdicio>()
                               where des.Desp_Fecha >= fecha1 &&
                               des.Desp_Fecha <= fecha2 &&
                               Convert.ToString(des.Desp_OT).Contains(OT) &&
                               Convert.ToString(des.Material_Id).Contains(material) &&
                               Convert.ToString(des.Prod_Id).Contains(item) &&
                               Convert.ToString(des.Falla_Id).Contains(falla) &&
                               Convert.ToString(des.Proceso_Id).Contains(proceso) &&
                               Convert.ToString(des.Actv_Id).Contains(maquina)
                               select new
                               {
                                   des.Desp_Id,
                                   des.Desp_OT,
                                   des.Activo.Actv_Serial,
                                   des.Usuario1.Usua_Nombre,
                                   des.Prod_Id,
                                   des.Producto.Prod_Nombre,
                                   des.Material.Material_Nombre,
                                   des.Desp_Impresion,
                                   des.Falla.Falla_Nombre,
                                   des.Desp_PesoKg,
                                   Unidad = "Kg",
                                   des.Desp_Fecha,
                                   des.Proceso.Proceso_Nombre,
                                   UsuarioCreador = des.Usuario2.Usua_Nombre,
                                   des.Desp_FechaRegistro,
                                   des.Desp_Observacion,
                               }).ToList();

            //if (Desperdicio == null) return NotFound();           
            return Ok(Desperdicio);
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
