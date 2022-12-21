using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;


namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        private readonly dataContext _context;

        public MantenimientoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mantenimiento>>> GetMantenimientos()
        {
            return await _context.Mantenimientos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Mantenimiento>> GetMantenimiento(long id)
        {
            var Mantenimiento = await _context.Mantenimientos.FindAsync(id);

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Mantenimiento;
        }

        [HttpGet("ObtenerUltimoId")]
        public ActionResult GetMttos()
        {
            var Mantenimiento = _context.Mantenimientos.OrderByDescending(ult => ult.Mtto_Id).First();

            return Ok(Mantenimiento);
        }

        //
        [HttpGet("getPedidoMtto/{id}")]
        public ActionResult GetPedidosMttos(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var Mantenimiento = _context.Mantenimientos.Where(pm => pm.PedMtto_Id == id)
                                                       .Select(u => new
                                                       {
                                                           u.Mtto_Id,
                                                           u.PedMtto_Id,
                                                           u.Mtto_FechaInicio,
                                                           u.Mtto_FechaFin,
                                                           u.Mtto_FechaRegistro,
                                                           u.Mtto_HoraRegistro,
                                                           u.Mtto_PrecioTotal,
                                                           u.Prov_Id,
                                                           u.Proveedor.Prov_Nombre,
                                                           u.Usua_Id,
                                                           u.Usu.Usua_Nombre,
                                                           u.Mtto_Observacion,
                                                           u.Mtto_CantDias,
                                                           u.Estado_Id,
                                                           u.Estado.Estado_Nombre
                                                       }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(Mantenimiento);
        }

        [HttpGet("getMttos/{id}")]
        public ActionResult GetEncabezadoMtto(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var Mantenimiento = _context.Mantenimientos.Where(pm => pm.Mtto_Id == id)
                                                       .Select(u => new
                                                       {
                                                           u.Mtto_Id,
                                                           u.PedMtto_Id,
                                                           u.Mtto_FechaInicio,
                                                           u.Mtto_FechaFin,
                                                           u.Mtto_FechaRegistro,
                                                           u.Mtto_HoraRegistro,
                                                           u.Mtto_PrecioTotal,
                                                           u.Prov_Id,
                                                           u.Proveedor.Prov_Nombre,
                                                           u.Usua_Id,
                                                           u.Usu.Usua_Nombre,
                                                           u.Mtto_Observacion,
                                                           u.Mtto_CantDias,
                                                           u.Estado_Id,
                                                           u.Estado.Estado_Nombre
                                                       }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(Mantenimiento);
        }

        /** Consulta para condicionales */
        [HttpGet("getPedido_Mantenimiento/{fecha1}/{fecha2}")]
        public ActionResult getMovimientos(DateTime fecha1, DateTime fecha2, string? consecutivo = "", string? consecutivo2="",  string? tipoMov = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8604 // Posible argumento de referencia nulo

            /*var pedido = _context.Database.ExecuteSql($"SELECT Ped.PedMtto_Id AS Id_Movimiento, 'Pedido de Mantenimiento' AS Tipo_Movimiento, Ped.PedMtto_Fecha AS Fecha, Ped.Estado_Id AS Estado_Id, Est.Estado_Nombre AS Estado_Nombre, Act.Actv_Id AS Activo_Id, Act.Actv_Nombre AS Activo_Nombre, DtPed.DtPedMtto_FechaFalla AS FechaDano, TpMtto.TpMtto_Id AS TipoMantenimiento_Id, TpMtto.TpMtto_Nombre AS TipoMantenimiento_Nombre FROM DetallesPedidos_Mantenimientos DtPed, Pedidos_Mantenimientos Ped, Activos Act, Estados Est, Tipos_Mantenimientos TpMtto WHERE Ped.PedMtto_Id = DtPed.PedMtto_Id AND Ped.Estado_Id = Est.Estado_Id AND DtPed.Actv_Id = Act.Actv_Id AND DtPed.TpMtto_Id = TpMtto.TpMtto_Id AND Ped.PedMtto_Fecha BETWEEN {fecha1} AND {fecha2} AND Ped.PedMtto_Id LIKE '%{consecutivo}%' AND Ped.Estado_Id LIKE '%{estado}%' AND DtPed.DtPedMtto_FechaFalla LIKE '%{fechaDaño}%' AND DtPed.TpMtto_Id LIKE '%{tipoMtto}%'");**/

            var pedido = from ped in _context.Set<Pedido_Mantenimiento>()
                         where Convert.ToString(ped.PedMtto_Id).Contains(consecutivo)
                               && ped.PedMtto_Fecha >= fecha1
                               && ped.PedMtto_Fecha <= fecha2
                               && "1".Contains(tipoMov)
                         select new
                         {
                             Id_Movimiento = ped.PedMtto_Id,
                             Tipo_Movimiento = "Pedido de Mantenimiento",
                             Fecha = ped.PedMtto_Fecha,
                             Hora = ped.PedMtto_Hora,
                             IdUsuario = ped.Usua_Id,   
                             Usuario = ped.Usuario.Usua_Nombre,
                             IdEstado = ped.Estado_Id, 
                             Estado = ped.Estado.Estado_Nombre,
                             Observacion = ped.PedMtto_Observacion
                         };

            var mantenimiento = from mtto in _context.Set<Mantenimiento>()
                                where Convert.ToString(mtto.Mtto_Id).Contains(consecutivo2)
                                      && mtto.Mtto_FechaRegistro >= fecha1
                                      && mtto.Mtto_FechaRegistro <= fecha2
                                       && "2".Contains(tipoMov)
                                select new
                                {
                                    Id_Movimiento = mtto.Mtto_Id,
                                    Tipo_Movimiento = "Mantenimiento",
                                    Fecha = mtto.Mtto_FechaRegistro,
                                    Hora = mtto.Mtto_HoraRegistro,
                                    IdUsuario = mtto.Usua_Id,
                                    Usuario = mtto.Usu.Usua_Nombre,
                                    IdEstado = mtto.Estado_Id,
                                    Estado = mtto.Estado.Estado_Nombre,
                                    Observacion = mtto.Mtto_Observacion
                                };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(pedido.Concat(mantenimiento));
        }


        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMantenimiento(long id, Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.Mtto_Id)
            {
                return BadRequest();
            }

            _context.Entry(mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MantenimientoExists(id))
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
        public async Task<ActionResult<Mantenimiento>> PostMantenimiento(Mantenimiento mantenimiento)
        {
            _context.Mantenimientos.Add(mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMantenimiento", new { id = mantenimiento.Mtto_Id }, mantenimiento);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMantenimientos(long id)
        {
            var Mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (Mantenimiento == null)
            {
                return NotFound();
            }

            _context.Mantenimientos.Remove(Mantenimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool MantenimientoExists(long id)
        {
            return _context.Mantenimientos.Any(e => e.Mtto_Id == id);
        }
    }
}
