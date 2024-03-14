using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Detalle_MantenimientoController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalle_MantenimientoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalle_Mantenimiento>>> GetDetalle_Mantenimiento()
        {
            return await _context.Detalles_Mantenimientos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalle_Mantenimiento>> GetDetalle_Mantenimiento(long id)
        {
            var Activos = await _context.Detalles_Mantenimientos.FindAsync(id);

            if (Activos == null)
            {
                return NotFound();
            }

            return Activos;
        }

        [HttpGet("getDetalleMtto/{idPedido}")]
        public ActionResult GetDetalleMttos(long idPedido)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var Mantenimiento = _context.Detalles_Mantenimientos.Where(pm => pm.Mttos.PedMtto_Id == idPedido)
                                                       .Select(u => new
                                                       {
                                                           u.DtMtto_Codigo,
                                                           u.Mtto_Id,
                                                           u.Actv_Id,
                                                           u.Act.Actv_Serial,
                                                           u.Act.Actv_Nombre,
                                                           u.TpMtto_Id,
                                                           u.Tipo_Mtto.TpMtto_Nombre,
                                                           u.Estado_Id,
                                                           u.Estados.Estado_Nombre,
                                                           u.DtMtto_Descripcion,
                                                           u.DtMtto_Precio
                                                       }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(Mantenimiento);
        }

        [HttpGet("getMttoxId/{idMtto}")]
        public ActionResult GetDetalleMttosxId(long idMtto)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var Mantenimiento = _context.Detalles_Mantenimientos.Where(pm => pm.Mtto_Id == idMtto)
                                                       .Select(u => new
                                                       {
                                                           u.DtMtto_Codigo,
                                                           u.Mtto_Id,
                                                           u.Actv_Id,
                                                           u.Act.Actv_Serial,
                                                           u.Act.Actv_Nombre,
                                                           u.TpMtto_Id,
                                                           u.Tipo_Mtto.TpMtto_Nombre,
                                                           u.Estado_Id,
                                                           u.Estados.Estado_Nombre,
                                                           u.DtMtto_Descripcion,
                                                           u.DtMtto_Precio
                                                       }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(Mantenimiento);
        }

        [HttpGet("getCodigoMtto/{codigo}")]
        public ActionResult GetCodigoDetalleMttos(long codigo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var Mantenimiento = _context.Detalles_Mantenimientos.Where(pm => pm.DtMtto_Codigo == codigo)
                                                       .Select(u => new
                                                       {
                                                           u.DtMtto_Codigo,
                                                           u.Mtto_Id,
                                                           u.Actv_Id,
                                                           u.Act.Actv_Serial,
                                                           u.Act.Actv_Nombre,
                                                           u.TpMtto_Id,
                                                           u.Tipo_Mtto.TpMtto_Nombre,
                                                           u.Estado_Id,
                                                           u.Estados.Estado_Nombre,
                                                           u.DtMtto_Descripcion,
                                                           u.DtMtto_Precio
                                                       }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(Mantenimiento);
        }

        //
        [HttpGet("getPDFMantenimiento/{id}")]
        public ActionResult getPDFMantenimiento(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from mtto in _context.Set<Detalle_Mantenimiento>()
                      from emp in _context.Set<Empresa>()
                      where mtto.Mtto_Id == id
                            && emp.Empresa_Id == 800188732
                      select new
                      {
                          mtto.Mtto_Id,
                          TipoMov = "Mantenimiento",
                          mtto.Mttos.Mtto_FechaRegistro,
                          mtto.Mttos.Mtto_FechaInicio,
                          mtto.Mttos.Mtto_FechaFin,
                          mtto.Estado_Id,
                          mtto.Estados.Estado_Nombre,
                          mtto.Actv_Id,
                          mtto.Act.Actv_Serial,
                          mtto.Act.Actv_Nombre,
                          mtto.TpMtto_Id,
                          mtto.Tipo_Mtto.TpMtto_Nombre,
                          mtto.Mttos.Mtto_Observacion,
                          Creador = mtto.Mttos.Usua_Id,
                          NombreCreador = mtto.Mttos.Usu.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre,
                          mtto.Mttos.Prov_Id,
                          mtto.Mttos.Proveedor.TipoIdentificacion_Id,
                          mtto.Mttos.Proveedor.TpProv.TpProv_Nombre,
                          mtto.Mttos.Proveedor.Prov_Nombre,
                          mtto.Mttos.Proveedor.Prov_Telefono,
                          mtto.Mttos.Proveedor.Prov_Ciudad,
                          mtto.Mttos.Proveedor.Prov_Email
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle_Mantenimiento(long id, Detalle_Mantenimiento detalle_Mantenimiento)
        {
            if (id != detalle_Mantenimiento.DtMtto_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalle_Mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_MantenimientoExists(id))
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

        [HttpPut("CambioEstadoDetalleMtto/{codigo}")]
        public ActionResult PutEstado(long codigo, Detalle_Mantenimiento Detalles_Mtto, int Estado)
        {
            if (codigo != Detalles_Mtto.DtMtto_Codigo)
            {
                return BadRequest();
            }

            var Actualizado = _context.Detalles_Mantenimientos.Where(x => x.DtMtto_Codigo == codigo).First<Detalle_Mantenimiento>();

            try
            {
                Actualizado.Estado_Id = Estado;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_MantenimientoExists(codigo))
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

        [HttpPut("CambioPrecioDetalleMtto/{codigo}")]
        public ActionResult PutPrecio(long codigo, Detalle_Mantenimiento Detalles_Mtto, decimal precio)
        {
            if (codigo != Detalles_Mtto.DtMtto_Codigo)
            {
                return BadRequest();
            }

            var Actualizado = _context.Detalles_Mantenimientos.Where(x => x.DtMtto_Codigo == codigo).First<Detalle_Mantenimiento>();

            try
            {
                Actualizado.DtMtto_Precio = precio;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_MantenimientoExists(codigo))
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

        [HttpPost]
        public async Task<ActionResult<Detalle_Mantenimiento>> PostDetalle_Mantenimiento(Detalle_Mantenimiento detalle_Mantenimiento)
        {
            _context.Detalles_Mantenimientos.Add(detalle_Mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalle_Mantenimiento", new { id = detalle_Mantenimiento.DtMtto_Codigo }, detalle_Mantenimiento);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalle_Mantenimiento(long id)
        {
            var activo = await _context.Detalles_Mantenimientos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            _context.Detalles_Mantenimientos.Remove(activo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Detalle_MantenimientoExists(long id)
        {
            return _context.Detalles_Mantenimientos.Any(e => e.DtMtto_Codigo == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
