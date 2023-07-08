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
    public class DetalleFacturacion_OrdenMaquilaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleFacturacion_OrdenMaquilaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleFacturacion_OrdenMaquila
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFacturacion_OrdenMaquila>>> GetDetalleFacturacion_OrdenMaquila()
        {
          if (_context.DetalleFacturacion_OrdenMaquila == null)
          {
              return NotFound();
          }
            return await _context.DetalleFacturacion_OrdenMaquila.ToListAsync();
        }

        // GET: api/DetalleFacturacion_OrdenMaquila/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleFacturacion_OrdenMaquila>> GetDetalleFacturacion_OrdenMaquila(long id)
        {
          if (_context.DetalleFacturacion_OrdenMaquila == null)
          {
              return NotFound();
          }
            var detalleFacturacion_OrdenMaquila = await _context.DetalleFacturacion_OrdenMaquila.FindAsync(id);

            if (detalleFacturacion_OrdenMaquila == null)
            {
                return NotFound();
            }

            return detalleFacturacion_OrdenMaquila;
        }

        [HttpGet("getConsultarFacturacion/{id}")]
        public ActionResult getConsultarFacturacion(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dt in _context.Set<DetalleFacturacion_OrdenMaquila>()
                      from omFac in _context.Set<OrdenMaquila_Facturacion>()
                      from Emp in _context.Set<Empresa>()
                      where dt.FacOM_Id == id
                            && omFac.FacOM_Id.Equals(id)
                            && Emp.Empresa_Id == 800188732
                      select new
                      {
                          Orden_Maquila = omFac.OM_Id,
                          Codigo_Documento = dt.FacOM.FacOM_Codigo,
                          Fecha = dt.FacOM.FacOM_Fecha,
                          Hora = dt.FacOM.FacOM_Hora,
                          Estado_Id = dt.FacOM.Estado_Id,
                          Estado = dt.FacOM.Estado.Estado_Nombre,
                          tipo_Documento_Id = dt.FacOM.TpDoc_Id,
                          tipo_Documento = dt.FacOM.TipoDoc.TpDoc_Nombre,

                          Tercero_Id = dt.FacOM.Tercero_Id,
                          Tipo_Id = dt.FacOM.Tercero.TipoIdentificacion_Id,
                          Tercero = dt.FacOM.Tercero.Tercero_Nombre,
                          Telefono_Tercero = dt.FacOM.Tercero.Tercero_Telefono,
                          Ciudad_Tercero = dt.FacOM.Tercero.Tercero_Ciudad,
                          Correo_Tercero = dt.FacOM.Tercero.Tercero_Email,

                          Observacion = dt.FacOM.FacOM_Observacion,
                          Usuario_Id = dt.FacOM.Usua_Id,
                          Usuario = dt.FacOM.Usua.Usua_Nombre,
                          Valor_Total = dt.FacOM.FacOM_ValorTotal,

                          Empresa_Id = Emp.Empresa_Id,
                          Empresa_Ciudad = Emp.Empresa_Ciudad,
                          Empresa_Codigo = Emp.Empresa_COdigoPostal,
                          Empresa_Correo = Emp.Empresa_Correo,
                          Empresa_Direccion = Emp.Empresa_Direccion,
                          Empresa_Telefono = Emp.Empresa_Telefono,
                          Empresa = Emp.Empresa_Nombre,

                          MP_Id = dt.MatPri_Id,
                          MP = dt.MatPrima.MatPri_Nombre,
                          Tinta_Id = dt.Tinta_Id,
                          Tinta = dt.Tinta.Tinta_Nombre,
                          Bopp_Id = dt.Bopp_Id,
                          Bopp = dt.BOPP.BOPP_Nombre,
                          Cantidad = dt.DtFacOM_Cantidad,
                          Und_Medida = dt.UndMed_Id,
                          Precio = dt.DtFacOM_ValorUnitario,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(con);
        }

        // PUT: api/DetalleFacturacion_OrdenMaquila/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleFacturacion_OrdenMaquila(long id, DetalleFacturacion_OrdenMaquila detalleFacturacion_OrdenMaquila)
        {
            if (id != detalleFacturacion_OrdenMaquila.DtFacOM_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalleFacturacion_OrdenMaquila).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFacturacion_OrdenMaquilaExists(id))
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

        // POST: api/DetalleFacturacion_OrdenMaquila
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleFacturacion_OrdenMaquila>> PostDetalleFacturacion_OrdenMaquila(DetalleFacturacion_OrdenMaquila detalleFacturacion_OrdenMaquila)
        {
          if (_context.DetalleFacturacion_OrdenMaquila == null)
          {
              return Problem("Entity set 'dataContext.DetalleFacturacion_OrdenMaquila'  is null.");
          }
            _context.DetalleFacturacion_OrdenMaquila.Add(detalleFacturacion_OrdenMaquila);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleFacturacion_OrdenMaquila", new { id = detalleFacturacion_OrdenMaquila.DtFacOM_Codigo }, detalleFacturacion_OrdenMaquila);
        }

        // DELETE: api/DetalleFacturacion_OrdenMaquila/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleFacturacion_OrdenMaquila(long id)
        {
            if (_context.DetalleFacturacion_OrdenMaquila == null)
            {
                return NotFound();
            }
            var detalleFacturacion_OrdenMaquila = await _context.DetalleFacturacion_OrdenMaquila.FindAsync(id);
            if (detalleFacturacion_OrdenMaquila == null)
            {
                return NotFound();
            }

            _context.DetalleFacturacion_OrdenMaquila.Remove(detalleFacturacion_OrdenMaquila);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleFacturacion_OrdenMaquilaExists(long id)
        {
            return (_context.DetalleFacturacion_OrdenMaquila?.Any(e => e.DtFacOM_Codigo == id)).GetValueOrDefault();
        }
    }
}
