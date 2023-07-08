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
    public class Detalles_SolicitudMateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_SolicitudMateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_SolicitudMateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_SolicitudMateriaPrima>>> GetDetalles_SolicitudMateriaPrima()
        {
          if (_context.Detalles_SolicitudMateriaPrima == null)
          {
              return NotFound();
          }
            return await _context.Detalles_SolicitudMateriaPrima.ToListAsync();
        }

        // GET: api/Detalles_SolicitudMateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_SolicitudMateriaPrima>> GetDetalles_SolicitudMateriaPrima(long id)
        {
          if (_context.Detalles_SolicitudMateriaPrima == null)
          {
              return NotFound();
          }
            var detalles_SolicitudMateriaPrima = await _context.Detalles_SolicitudMateriaPrima.FindAsync(id);

            if (detalles_SolicitudMateriaPrima == null)
            {
                return NotFound();
            }

            return detalles_SolicitudMateriaPrima;
        }

        //Consulta que devolverá toda la información de una solicitud
        [HttpGet("getInfoSolicitud/{solicitud_Id}")]
        public ActionResult GetInfoSolicitud (long solicitud_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dtSol in _context.Set<Detalles_SolicitudMateriaPrima>()
                      from Emp in _context.Set<Empresa>()
                      where dtSol.Solicitud_Id == solicitud_Id
                            && Emp.Empresa_Id == 800188732
                      select new
                      {
                          Codigo = dtSol.DtSolicitud_Id,
                          Consecutivo = dtSol.Solicitud_Id,
                          Fecha = dtSol.Solicitud_MateriaPrima.Solicitud_Fecha,
                          Hora = dtSol.Solicitud_MateriaPrima.Solicitud_Hora,
                          Estado_Solicitud_Id = dtSol.Solicitud_MateriaPrima.Estado_Id,
                          Estado_Solicitud = dtSol.Solicitud_MateriaPrima.Estado.Estado_Nombre,

                          Observacion = dtSol.Solicitud_MateriaPrima.Solicitud_Observacion,
                          Usuario_Id = dtSol.Solicitud_MateriaPrima.Usua_Id,
                          Usuario = dtSol.Solicitud_MateriaPrima.Usuario.Usua_Nombre,

                          Empresa_Id = Emp.Empresa_Id,
                          Empresa_Ciudad = Emp.Empresa_Ciudad,
                          Empresa_Codigo = Emp.Empresa_COdigoPostal,
                          Empresa_Correo = Emp.Empresa_Correo,
                          Empresa_Direccion = Emp.Empresa_Direccion,
                          Empresa_Telefono = Emp.Empresa_Telefono,
                          Empresa = Emp.Empresa_Nombre,

                          MP_Id = dtSol.MatPri_Id,
                          MP = dtSol.Materia_Prima.MatPri_Nombre,
                          Precio_MP = dtSol.Materia_Prima.MatPri_Precio,
                          Tinta_Id = dtSol.Tinta_Id,
                          Tinta = dtSol.Tinta.Tinta_Nombre,
                          Precio_Tinta = dtSol.Tinta.Tinta_Precio,
                          Bopp_Id = dtSol.Bopp_Id,
                          Bopp = dtSol.Bopp.BoppGen_Nombre,
                          Precio_Bopp = 0,
                          Cantidad = dtSol.DtSolicitud_Cantidad,
                          Unidad_Medida = dtSol.UndMed_Id,
                          Estado_MP_Id = dtSol.Estado_Id,
                          Estado_MP = dtSol.Estado.Estado_Nombre,

                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (con.Count() > 0) return Ok(con);
            else return BadRequest("No se encontró información sobre una solicitud con el número '" + solicitud_Id +"'");
        }

        // Consulta que devolverá la infomación de una de las materias primas que tiene la solicitud
        [HttpGet("getMateriaPrimaSolicitud/{mp}/{solicitud}")]
        public ActionResult GetMateriaPrimaSolicitud(long mp, long solicitud)
        {
            var con = from dtSol in _context.Set<Detalles_SolicitudMateriaPrima>()
                      where dtSol.Solicitud_Id == solicitud && (dtSol.MatPri_Id == mp || dtSol.Tinta_Id == mp || dtSol.Bopp_Id == mp)
                      select dtSol;

            if (con.Count() > 0) return Ok(con);
            else return BadRequest("No se ha encontrada la materia prima en la solicitud");
        }

        [HttpGet("getEstadosMateriasPrimas/{solicitud}")]
        public ActionResult GetEstadosMateriasPrimas(long solicitud)
        {
            var pendientes = (from dt in _context.Set<Detalles_SolicitudMateriaPrima>() where dt.Solicitud_Id == solicitud && dt.Estado_Id == 11 select dt.Estado_Id).Count();
            var parciales = (from dt in _context.Set<Detalles_SolicitudMateriaPrima>() where dt.Solicitud_Id == solicitud && dt.Estado_Id == 12 select dt.Estado_Id).Count();
            var totales = (from dt in _context.Set<Detalles_SolicitudMateriaPrima>() where dt.Solicitud_Id == solicitud && dt.Estado_Id == 5 select dt.Estado_Id).Count();
            var CantMp = (from dt in _context.Set<Detalles_SolicitudMateriaPrima>() where dt.Solicitud_Id == solicitud select dt.Estado_Id).Count();

            long[] datos = { pendientes, parciales, totales, CantMp };
            return Ok(datos);
        }

        [HttpGet("getRelacionSolicitudesMp_Oc/{solicitud}")]
        public ActionResult getRelacionSolicitudesMp_Oc(long solicitud)

        {
            var relacion = (from smpOc in _context.Set<SolicitudesMP_OrdenesCompra>() where smpOc.Solicitud_Id == solicitud select smpOc.Oc_Id).ToList();

            if (relacion.Count() == 0) return BadRequest("No se encontraron ordenes de compra asociadas a la solicitud N° " + solicitud);
            else
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                var ordenCompra = from Oc in _context.Set<Detalle_OrdenCompra>()
                                  where relacion.Contains(Oc.Oc_Id)
                                  group Oc by new {
                                    Oc.MatPri_Id,
                                    Oc.MatPrima.MatPri_Nombre,
                                    Oc.Tinta_Id,
                                    Oc.Tinta.Tinta_Nombre,
                                    Oc.BOPP_Id,
                                    Oc.BOPP.BoppGen_Nombre,
                                    Oc.UndMed_Id,
                                  } 
                                  into Oc
                                  select new
                                  {
                                    IdMatPrima = Oc.Key.MatPri_Id,
                                    MatPrima = Oc.Key.MatPri_Nombre,
                                    IdTinta = Oc.Key.Tinta_Id,
                                    Tinta = Oc.Key.Tinta_Nombre,
                                    IdBopp = Oc.Key.BOPP_Id,
                                    Bopp = Oc.Key.BoppGen_Nombre,
                                    Cantidad = Oc.Sum(oc => oc.Doc_CantidadPedida),
                                    Unidad = Oc.Key.UndMed_Id,
                                  };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                return Ok(ordenCompra);
            }
        }


            // PUT: api/Detalles_SolicitudMateriaPrima/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_SolicitudMateriaPrima(long id, Detalles_SolicitudMateriaPrima detalles_SolicitudMateriaPrima)
        {
            if (id != detalles_SolicitudMateriaPrima.DtSolicitud_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalles_SolicitudMateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_SolicitudMateriaPrimaExists(id))
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

        // POST: api/Detalles_SolicitudMateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_SolicitudMateriaPrima>> PostDetalles_SolicitudMateriaPrima(Detalles_SolicitudMateriaPrima detalles_SolicitudMateriaPrima)
        {
          if (_context.Detalles_SolicitudMateriaPrima == null)
          {
              return Problem("Entity set 'dataContext.Detalles_SolicitudMateriaPrima'  is null.");
          }
            _context.Detalles_SolicitudMateriaPrima.Add(detalles_SolicitudMateriaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_SolicitudMateriaPrima", new { id = detalles_SolicitudMateriaPrima.DtSolicitud_Id }, detalles_SolicitudMateriaPrima);
        }

        // DELETE: api/Detalles_SolicitudMateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_SolicitudMateriaPrima(long id)
        {
            if (_context.Detalles_SolicitudMateriaPrima == null)
            {
                return NotFound();
            }
            var detalles_SolicitudMateriaPrima = await _context.Detalles_SolicitudMateriaPrima.FindAsync(id);
            if (detalles_SolicitudMateriaPrima == null)
            {
                return NotFound();
            }

            _context.Detalles_SolicitudMateriaPrima.Remove(detalles_SolicitudMateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_SolicitudMateriaPrimaExists(long id)
        {
            return (_context.Detalles_SolicitudMateriaPrima?.Any(e => e.DtSolicitud_Id == id)).GetValueOrDefault();
        }
    }
}
