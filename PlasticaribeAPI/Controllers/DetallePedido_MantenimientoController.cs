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
    public class DetallePedido_MantenimientoController : ControllerBase
    {
        private readonly dataContext _context;

        public DetallePedido_MantenimientoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePedido_Mantenimiento>>> GetDetallePedido_Mantenimiento()
        {
            return await _context.DetallesPedidos_Mantenimientos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido_Mantenimiento>> GetDetallePedido_Mantenimiento(long id)
        {
            var Activos = await _context.DetallesPedidos_Mantenimientos.FindAsync(id);

            if (Activos == null)
            {
                return NotFound();
            }

            return Activos;
        }

        [HttpGet("Detalle_PedidoMtto/{id}")]
        public ActionResult GetDetallePedido_Mtto(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var detPedidos = _context.DetallesPedidos_Mantenimientos.Where(d => d.PedMtto_Id == id)
                                                                    .Select(det => new
                                                                    {
                                                                        det.PedMtto_Id,
                                                                        det.DtPedMtto_Codigo,
                                                                        det.Actv_Id,
                                                                        det.Act.Actv_Nombre,
                                                                        det.Act.Actv_Serial,
                                                                        det.TpMtto_Id,
                                                                        det.Tipo_Mtto.TpMtto_Nombre,
                                                                        det.DtPedMtto_FechaFalla
                                                                    })
                                                                    .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (detPedidos == null)
            {
                return NotFound();
            }

            return Ok(detPedidos);

        }

        [HttpGet("getPDFPedido/{id}")]
        public ActionResult getPDFPedido(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ped in _context.Set<DetallePedido_Mantenimiento>()
                      from emp in _context.Set<Empresa>()
                      where ped.PedMtto_Id == id
                            && emp.Empresa_Id == 800188732
                      select new
                      {
                          ped.PedMtto_Id,
                          TipoMov = "Pedido de Mantenimiento",
                          ped.PedidoMtto.PedMtto_Fecha,
                          ped.PedidoMtto.Estado_Id,
                          ped.PedidoMtto.Estado.Estado_Nombre,
                          ped.Actv_Id,
                          ped.Act.Actv_Serial,
                          ped.Act.Actv_Nombre,
                          ped.DtPedMtto_FechaFalla,
                          ped.TpMtto_Id,
                          ped.Tipo_Mtto.TpMtto_Nombre,
                          ped.PedidoMtto.PedMtto_Observacion,
                          Creador = ped.PedidoMtto.Usua_Id,
                          NombreCreador = ped.PedidoMtto.Usuario.Usua_Nombre,
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

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallePedido_Mantenimiento(long id, DetallePedido_Mantenimiento detallePedido_Mantenimiento)
        {
            if (id != detallePedido_Mantenimiento.DtPedMtto_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detallePedido_Mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePedido_MantenimientoExists(id))
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

        //
        [HttpPost]
        public async Task<ActionResult<DetallePedido_Mantenimiento>> PostDetallePedido_Mantenimiento(DetallePedido_Mantenimiento detallePedido_Mantenimiento)
        {
            _context.DetallesPedidos_Mantenimientos.Add(detallePedido_Mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallePedido_Mantenimiento", new { id = detallePedido_Mantenimiento.DtPedMtto_Codigo }, detallePedido_Mantenimiento);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivos(long id)
        {
            var DetallesPedidos_Mantenimientos = await _context.DetallesPedidos_Mantenimientos.FindAsync(id);
            if (DetallesPedidos_Mantenimientos == null)
            {
                return NotFound();
            }

            _context.DetallesPedidos_Mantenimientos.Remove(DetallesPedidos_Mantenimientos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool DetallePedido_MantenimientoExists(long id)
        {
            return _context.DetallesPedidos_Mantenimientos.Any(e => e.DtPedMtto_Codigo == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
