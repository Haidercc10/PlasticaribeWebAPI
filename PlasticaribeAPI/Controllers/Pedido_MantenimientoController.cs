using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pedido_MantenimientoController : ControllerBase
    {
        private readonly dataContext _context;

        public Pedido_MantenimientoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido_Mantenimiento>>> GetPedido_Mantenimiento()
        {
            return await _context.Pedidos_Mantenimientos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido_Mantenimiento>> GetPedido_Mantenimiento(long id)
        {
            var Activos = await _context.Pedidos_Mantenimientos.FindAsync(id);

            if (Activos == null)
            {
                return NotFound();
            }

            return Activos;
        }

        [HttpGet("getDatosCompletos/{id}")]
        public ActionResult GetPedido_Mtto(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var pedidos =  _context.Pedidos_Mantenimientos.Where(p => p.PedMtto_Id == id)
                                                          .Select(ped => new
                                                          {
                                                              ped.PedMtto_Id,
                                                              ped.PedMtto_Fecha, 
                                                              ped.PedMtto_Hora,
                                                              ped.Usua_Id,
                                                              ped.Usuario.Usua_Nombre,
                                                              ped.Estado_Id,
                                                              ped.Estado.Estado_Nombre,
                                                              ped.PedMtto_Observacion
                                                          }).
                                                          ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (pedidos == null)
            {
                return NotFound();
            }

            return Ok(pedidos);
        }

        [HttpGet("getUltimoIdPedido")]
        public ActionResult<Pedido_Mantenimiento> getUltimoIdPedido()
        {
            var con = (from pm in _context.Set<Pedido_Mantenimiento>()
                      orderby pm.PedMtto_Id descending
                      select pm.PedMtto_Id).FirstOrDefault();
            return Ok(con);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido_Mantenimiento(long id, Pedido_Mantenimiento pedido_Mantenimiento)
        {
            if (id != pedido_Mantenimiento.PedMtto_Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido_Mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Pedido_MantenimientoExists(id))
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
        public async Task<ActionResult<Pedido_Mantenimiento>> PostPedido_Mantenimiento(Pedido_Mantenimiento pedido_Mantenimiento)
        {
            _context.Pedidos_Mantenimientos.Add(pedido_Mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido_Mantenimiento", new { id = pedido_Mantenimiento.PedMtto_Id }, pedido_Mantenimiento);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivos(long id)
        {
            var activo = await _context.Pedidos_Mantenimientos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            _context.Pedidos_Mantenimientos.Remove(activo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Pedido_MantenimientoExists(long id)
        {
            return _context.Pedidos_Mantenimientos.Any(e => e.PedMtto_Id == id);
        }
    }
}
