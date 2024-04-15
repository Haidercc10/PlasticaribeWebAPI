﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Detalles_OrdenFacturacionController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_OrdenFacturacionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_OrdenFacturacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_OrdenFacturacion>>> GetDetalles_OrdenFacturacion()
        {
            return await _context.Detalles_OrdenFacturacion.ToListAsync();
        }

        // GET: api/Detalles_OrdenFacturacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_OrdenFacturacion>> GetDetalles_OrdenFacturacion(int id)
        {
            var detalles_OrdenFacturacion = await _context.Detalles_OrdenFacturacion.FindAsync(id);

            if (detalles_OrdenFacturacion == null)
            {
                return NotFound();
            }

            return detalles_OrdenFacturacion;
        }

        [HttpGet("getInformationOrderFactToSend/{id}")]
        public ActionResult GetInformacionOrderFactToSend(int id)
        {
            var fact = from order in _context.Set<OrdenFacturacion>()
                       join dtOrder in _context.Set<Detalles_OrdenFacturacion>() on order.Id equals dtOrder.Id_OrdenFacturacion
                       where order.Id == id &&
                             order.Estado_Id == 19
                       select new
                       {
                           order = new
                           {
                               order.Id,
                               order.Factura,
                               order.Fecha,
                               order.Hora,
                               order.Observacion,
                           },
                           Clientes = new
                           {
                               order.Clientes.Cli_Id,
                               order.Clientes.Cli_Nombre,
                               order.Clientes.Cli_Telefono,
                               order.Clientes.Cli_Email,
                               order.Clientes.TipoIdentificacion_Id
                           },
                           Usuario = new
                           {
                               order.Usuario.Usua_Id,
                               order.Usuario.Usua_Nombre
                           },
                           dtOrder = new
                           {
                               dtOrder.Cantidad,
                               dtOrder.Presentacion,
                               dtOrder.Numero_Rollo,
                               dtOrder.Consecutivo_Pedido, 
                               dtOrder.Pallet_Id,
                           },
                           Producto = new
                           {
                               dtOrder.Producto.Prod_Id,
                               dtOrder.Producto.Prod_Nombre
                           },
                           Ubication = (from pp in _context.Set<Produccion_Procesos>()
                                        from dt in _context.Set<DetalleEntradaRollo_Producto>()
                                        join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                                        where pp.NumeroRollo_BagPro == dtOrder.Numero_Rollo &&
                                               (dt.Rollo_Id == pp.Numero_Rollo) &&
                                               e.EntRolloProd_Id >= 28512
                                        orderby e.EntRolloProd_Id descending
                                        select e.EntRolloProd_Observacion).FirstOrDefault(), 
                       };
            return fact.Any() ? Ok(fact) : NotFound();
        }

        [HttpGet("getInformationOrderFact/{id}")]
        public ActionResult GetInformacionOrderFact(int id)
        {
            var dataSend = from asg in _context.Set<AsignacionProducto_FacturaVenta>()
                           where asg.NotaCredito_Id == $"Orden de Facturación #{id}"
                           select new
                           {
                               Conductor = asg.Usuario.Usua_Nombre,
                               Placa = asg.AsigProdFV_PlacaCamion,
                               Observacion = asg.AsigProdFV_Observacion,
                               Fecha = asg.AsigProdFV_Fecha,
                               Hora = asg.AsigProdFV_Hora,
                               CreadoPor = asg.Usua.Usua_Nombre
                           };

            var fact = from order in _context.Set<OrdenFacturacion>()
                       join dtOrder in _context.Set<Detalles_OrdenFacturacion>() on order.Id equals dtOrder.Id_OrdenFacturacion
                       where order.Id == id
                       select new
                       {
                           order = new
                           {
                               order.Id,
                               order.Factura,
                               order.Fecha,
                               order.Hora,
                               order.Observacion,
                           },
                           Clientes = new
                           {
                               order.Clientes.Cli_Id,
                               order.Clientes.Cli_Nombre,
                               order.Clientes.Cli_Telefono,
                               order.Clientes.Cli_Email,
                               order.Clientes.TipoIdentificacion_Id
                           },
                           Usuario = new
                           {
                               order.Usuario.Usua_Id,
                               order.Usuario.Usua_Nombre
                           },
                           dtOrder = new
                           {
                               dtOrder.Cantidad,
                               dtOrder.Presentacion,
                               dtOrder.Numero_Rollo,
                               dtOrder.Consecutivo_Pedido, 
                               dtOrder.Pallet_Id,
                           },
                           Producto = new
                           {
                               dtOrder.Producto.Prod_Id,
                               dtOrder.Producto.Prod_Nombre
                           },
                           Ubication = (from pp in _context.Set<Produccion_Procesos>()
                                        from dt in _context.Set<DetalleEntradaRollo_Producto>()
                                        join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                                        where pp.NumeroRollo_BagPro == dtOrder.Numero_Rollo &&
                                               (dt.Rollo_Id == pp.Numero_Rollo) &&
                                               dt.Estado_Id == 19 &&
                                               e.EntRolloProd_Id >= 28512
                                        orderby e.EntRolloProd_Id descending
                                        select e.EntRolloProd_Observacion).FirstOrDefault(),
                           orderProduction = (from pp in _context.Set<Produccion_Procesos>() where pp.NumeroRollo_BagPro == dtOrder.Numero_Rollo && pp.Prod_Id == dtOrder.Prod_Id select pp.OT).FirstOrDefault(),
                           datosEnvio = dataSend.Any() ? (dataSend).FirstOrDefault() : null,
                           Weight = (from pp in _context.Set<Produccion_Procesos>() where pp.NumeroRollo_BagPro == dtOrder.Numero_Rollo && pp.Prod_Id == dtOrder.Prod_Id select pp.Peso_Bruto).FirstOrDefault(),
                       };
            return fact.Any() ? Ok(fact) : NotFound();
        }

        [HttpGet("getInformationOrderFactByFactForDevolution/{fact}")]
        public ActionResult GetInformationOrderFactByFactForDevolution(int fact)
        {
            var devolutions = from dev in _context.Set<DetalleDevolucion_ProductoFacturado>() select dev.Rollo_Id;

            var data = from order in _context.Set<OrdenFacturacion>()
                       join dtOrder in _context.Set<Detalles_OrdenFacturacion>() on order.Id equals dtOrder.Id_OrdenFacturacion
                       where order.Id == fact &&
                             !devolutions.Contains(dtOrder.Numero_Rollo)
                       select new
                       {
                           order,
                           order.Clientes,
                           order.Usuario,
                           dtOrder,
                           dtOrder.Producto,
                           Type = "Devolucion",
                       };
            return data.Any() ? Ok(data) : NotFound();
        }

        [HttpGet("getOrders/{startDate}/{endDate}")]
        public ActionResult GetOrderd(DateTime startDate, DateTime endDate, string? order = "")
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var fact = from or in _context.Set<OrdenFacturacion>()
                       where or.Fecha >= startDate &&
                             or.Fecha <= endDate &&
                             (order != "" ? (or.Factura.Contains(order) || or.Id == Convert.ToInt32(order)) : true)
                       select new
                       {
                           or,
                           or.Clientes,
                           or.Usuario,
                           or.Factura,
                           Type = "Orden",
                           FechaHora = or.Fecha + " " + or.Hora,
                           FechaDespacho = (from asg in _context.Set<AsignacionProducto_FacturaVenta>() where asg.NotaCredito_Id == "Orden de Facturación #" + or.Id select asg.AsigProdFV_Fecha).FirstOrDefault(),
                           Estado = or.Estado_Id == 19 ? "PENDIENTE" : or.Estado_Id == 21 ? "DESPACHADO" : "ANULADO"
                       };
            return fact.Any() ? Ok(fact) : NotFound();
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [HttpGet("getSendOrders/{startDate}/{endDate}")]
        public ActionResult GetSendOrders(DateTime startDate, DateTime endDate, string? order = "")
        {
            var facts = from or in _context.Set<OrdenFacturacion>()
                        where or.Fecha >= startDate &&
                              or.Fecha <= endDate &&
                              (order != "" ? or.Factura == order : true) &&
                              or.Estado_Id == 21
                        select or.Factura;

            var sendOrders = from asg in _context.Set<AsignacionProducto_FacturaVenta>()
                             join dt in _context.Set<DetallesAsignacionProducto_FacturaVenta>() on asg.AsigProdFV_Id equals dt.AsigProdFV_Id
                             where facts.Contains(asg.FacturaVta_Id)
                             select new
                             {
                                 or = new
                                 {
                                     Id = asg.AsigProdFV_Id,
                                     Factura = asg.FacturaVta_Id,
                                     Fecha = asg.AsigProdFV_Fecha,
                                     Hora = asg.AsigProdFV_Hora,
                                     asg.Cli_Id,
                                     Observacion = asg.AsigProdFV_Observacion,
                                 },
                                 Conductor = asg.Usuario,
                                 Placa = asg.AsigProdFV_PlacaCamion,
                                 Clientes = asg.Cliente,
                                 Usuario = asg.Usua,
                                 Type = "Factura Enviada",
                             };
            return sendOrders.Any() ? Ok(sendOrders) : NotFound();
        }

        [HttpGet("getNotAvaibleProduction")]
        public ActionResult GetNorAvaibleProduction()
        {
            return Ok(from of in _context.Set<Detalles_OrdenFacturacion>() select of.Numero_Rollo);
        }

        // PUT: api/Detalles_OrdenFacturacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_OrdenFacturacion(int id, Detalles_OrdenFacturacion detalles_OrdenFacturacion)
        {
            if (id != detalles_OrdenFacturacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(detalles_OrdenFacturacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_OrdenFacturacionExists(id))
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

        [HttpPost("putStatusProduction/{order}")]
        public async Task<IActionResult> PutStatusProduction([FromBody] List<long> productions, int order)
        {
            var dataOrder = from of in _context.Set<Detalles_OrdenFacturacion>() where of.Id_OrdenFacturacion == order && productions.Contains(of.Numero_Rollo) select of;

            int count = 0;
            foreach (var item in dataOrder)
            {
                item.Estado_Id = 24;
                _context.Entry(item).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == dataOrder.Count()) return NoContent();
            }
            return NoContent();
        }

        // POST: api/Detalles_OrdenFacturacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_OrdenFacturacion>> PostDetalles_OrdenFacturacion(Detalles_OrdenFacturacion detalles_OrdenFacturacion)
        {
            _context.Detalles_OrdenFacturacion.Add(detalles_OrdenFacturacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_OrdenFacturacion", new { id = detalles_OrdenFacturacion.Id }, detalles_OrdenFacturacion);
        }

        // DELETE: api/Detalles_OrdenFacturacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_OrdenFacturacion(int id)
        {
            var detalles_OrdenFacturacion = await _context.Detalles_OrdenFacturacion.FindAsync(id);
            if (detalles_OrdenFacturacion == null)
            {
                return NotFound();
            }

            _context.Detalles_OrdenFacturacion.Remove(detalles_OrdenFacturacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_OrdenFacturacionExists(int id)
        {
            return _context.Detalles_OrdenFacturacion.Any(e => e.Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
