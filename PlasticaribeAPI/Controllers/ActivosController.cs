using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize] //
    public class ActivosController : ControllerBase
    {
        private readonly dataContext _context;

        public ActivosController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activo>>> GetActivos()
        {
            return await _context.Activos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Activo>> GetActivos(long id)
        {
            var Activos = await _context.Activos.FindAsync(id);

            if (Activos == null)
            {
                return NotFound();
            }

            return Activos;
        }

        // Consulta que buscará el nombre de un activo por medio de los datos que se le vatan pasando, se usuará un Contains() (en sql es un LIKE)
        [HttpGet("getActivoNombre/{datos}")]
        public ActionResult getActivoNombre(string datos)
        {
            if (datos == null)
            {
                return NoContent();
            }
            var activos = from activo in _context.Set<Activo>()
                          where activo.Actv_Nombre.Contains(datos)
                          select activo;
            return Ok(activos);
        }

        // Consulta que nos servirá para el reporte de activos
        [HttpGet("getInfoActivos/{activo}")]
        public ActionResult get(long activo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var totalMtto = _context.Detalles_Mantenimientos
                            .Where(x => x.Actv_Id == activo)
                            .Sum(x => x.DtMtto_Precio);

            var con = (from act in _context.Set<Activo>()
                       from mtto in _context.Set<Detalle_Mantenimiento>()
                       where act.Actv_Id == activo
                             && act.Actv_Id == mtto.Actv_Id
                       orderby mtto.Mtto_Id descending
                       select new
                       {
                           Activo_Id = act.Actv_Serial,
                           Activo_Nombre = act.Actv_Nombre,
                           Tipo_Activo = act.Tp_Activo.TpActv_Nombre,
                           Fecha_Compra = act.Actv_FechaCompra,
                           Fecha_UltMtto = mtto.Mttos.Mtto_FechaInicio,
                           Precio_Compra = act.Actv_PrecioCompra,
                           Precio_UltMtto = mtto.DtMtto_Precio,
                           Precio_TotalMtto = totalMtto,
                           Depreciacion = act.Actv_Depreciacion,
                       }).FirstOrDefault();
            return Ok(con);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        //Consulta que buscará la informacion de un activo mediante su serial
        [HttpGet("getInfoActivoSerial/{serial}")]
        public ActionResult getInfoActivoSerial(string serial)
        {
            var con = from act in _context.Set<Activo>()
                      where act.Actv_Serial == serial
                      select act;
            return Ok(con);
        }

        // Consulta que nos servirá para buscar información de los movimientos de activos
        [HttpGet("getMovimiento/{fecha1}/{fecha2}")]
        public ActionResult getMovimientos(DateTime fecha1, DateTime fecha2, string? consecutivo = "", string? estado = "", string? fechaDaño = "", string? tipoMtto = "", string? activo = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8604 // Posible argumento de referencia nulo

            /*var pedido = _context.Database.ExecuteSql($"SELECT Ped.PedMtto_Id AS Id_Movimiento, 'Pedido de Mantenimiento' AS Tipo_Movimiento, Ped.PedMtto_Fecha AS Fecha, Ped.Estado_Id AS Estado_Id, Est.Estado_Nombre AS Estado_Nombre, Act.Actv_Id AS Activo_Id, Act.Actv_Nombre AS Activo_Nombre, DtPed.DtPedMtto_FechaFalla AS FechaDano, TpMtto.TpMtto_Id AS TipoMantenimiento_Id, TpMtto.TpMtto_Nombre AS TipoMantenimiento_Nombre FROM DetallesPedidos_Mantenimientos DtPed, Pedidos_Mantenimientos Ped, Activos Act, Estados Est, Tipos_Mantenimientos TpMtto WHERE Ped.PedMtto_Id = DtPed.PedMtto_Id AND Ped.Estado_Id = Est.Estado_Id AND DtPed.Actv_Id = Act.Actv_Id AND DtPed.TpMtto_Id = TpMtto.TpMtto_Id AND Ped.PedMtto_Fecha BETWEEN {fecha1} AND {fecha2} AND Ped.PedMtto_Id LIKE '%{consecutivo}%' AND Ped.Estado_Id LIKE '%{estado}%' AND DtPed.DtPedMtto_FechaFalla LIKE '%{fechaDaño}%' AND DtPed.TpMtto_Id LIKE '%{tipoMtto}%'");**/

            var pedido = from ped in _context.Set<DetallePedido_Mantenimiento>()
                         where Convert.ToString(ped.PedMtto_Id).Contains(consecutivo)
                               && ped.PedidoMtto.PedMtto_Fecha >= fecha1
                               && ped.PedidoMtto.PedMtto_Fecha <= fecha2
                               && Convert.ToString(ped.PedidoMtto.Estado_Id).Contains(estado)
                               && Convert.ToString(ped.DtPedMtto_FechaFalla).Contains(fechaDaño)
                               && Convert.ToString(ped.TpMtto_Id).Contains(tipoMtto)
                               && Convert.ToString(ped.Actv_Id).Contains(activo)
                         select new
                         {
                             Id_Movimiento = ped.PedMtto_Id,
                             Tipo_Movimiento = "Pedido de Mantenimiento",
                             Fecha = ped.PedidoMtto.PedMtto_Fecha,
                             ped.PedidoMtto.Estado_Id,
                             ped.PedidoMtto.Estado.Estado_Nombre,
                             Activo_Id = ped.Actv_Id,
                             Activo_Nombre = ped.Act.Actv_Nombre,
                             Fecha_Dano = Convert.ToString(ped.DtPedMtto_FechaFalla),
                             TipoMantenimiento_Id = ped.TpMtto_Id,
                             TipoMantenimiento_Nombre = ped.Tipo_Mtto.TpMtto_Nombre
                         };

            var mantenimiento = from mtto in _context.Set<Detalle_Mantenimiento>()
                                where Convert.ToString(mtto.Mttos.Mtto_Id).Contains(consecutivo)
                                      && mtto.Mttos.Mtto_FechaRegistro >= fecha1
                                      && mtto.Mttos.Mtto_FechaRegistro <= fecha2
                                      && Convert.ToString(mtto.Mttos.Estado_Id).Contains(estado)
                                      && Convert.ToString(mtto.TpMtto_Id).Contains(tipoMtto)
                                      && Convert.ToString(mtto.Actv_Id).Contains(activo)
                                select new
                                {
                                    Id_Movimiento = mtto.Mtto_Id,
                                    Tipo_Movimiento = "Mantenimiento",
                                    Fecha = mtto.Mttos.Mtto_FechaRegistro,
                                    mtto.Mttos.Estado_Id,
                                    mtto.Mttos.Estado.Estado_Nombre,
                                    Activo_Id = mtto.Actv_Id,
                                    Activo_Nombre = mtto.Act.Actv_Nombre,
                                    Fecha_Dano = Convert.ToString(""),
                                    TipoMantenimiento_Id = mtto.TpMtto_Id,
                                    TipoMantenimiento_Nombre = mtto.Tipo_Mtto.TpMtto_Nombre
                                };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(pedido.Concat(mantenimiento));
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivo(long id, Activo activo)
        {
            if (id != activo.Actv_Id)
            {
                return BadRequest();
            }

            _context.Entry(activo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivoExists(id))
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
        public async Task<ActionResult<Activo>> PostActivos(Activo activo)
        {
            _context.Activos.Add(activo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivos", new { id = activo.Actv_Id }, activo);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivos(long id)
        {
            var activo = await _context.Activos.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }

            _context.Activos.Remove(activo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool ActivoExists(long id)
        {
            return _context.Activos.Any(e => e.Actv_Id == id);
        }
    }
}
