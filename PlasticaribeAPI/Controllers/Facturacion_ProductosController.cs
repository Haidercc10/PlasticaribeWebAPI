using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using StackExchange.Redis;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Facturacion_ProductosController : ControllerBase
    {
        private readonly dataContext _context;

        public Facturacion_ProductosController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facturacion_Productos>>> GetFacturacion_Productos()
        {
            return await _context.Facturacion_Productos.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Facturacion_Productos>> GetFacturacion_Productos(long id)
        {
            var Facturacion_Productos = await _context.Facturacion_Productos.FindAsync(id);

            if (Facturacion_Productos == null)
            {
                return NotFound();
            }

            return Facturacion_Productos;
        }

        [HttpGet("getInfoOfDirect/{id}")]
        public ActionResult getInfoOfDirect(int id)
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

            var details = from dtOrder in _context.Set<Detalles_OrdenFacturacion>()
                          where dtOrder.Id_OrdenFacturacion == id
                          select new
                          {
                              dtOrder = new
                              {
                                  dtOrder.Id,
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

                              Weight = (from pp in _context.Set<Produccion_Procesos>() where pp.NumeroRollo_BagPro == dtOrder.Numero_Rollo && pp.Prod_Id == dtOrder.Prod_Id select pp.Peso_Bruto).FirstOrDefault(),
                              NetWeight = (from pp in _context.Set<Produccion_Procesos>() where pp.NumeroRollo_BagPro == dtOrder.Numero_Rollo && pp.Prod_Id == dtOrder.Prod_Id select pp.Peso_Neto).FirstOrDefault(),
                          };

            var fact = from order in _context.Set<OrdenFacturacion>()
                       join fp in _context.Set<Facturacion_Productos>() on order.Id equals fp.Of_Id
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
                               order.Estado_Id,
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
                               fp.FactPro_Codigo,
                               fp.FactPro_Cantidad,
                               fp.UndMed_Id,
                               fp.FactPro_Pedido,
                               fp.Peso_Bruto,
                               fp.Peso_Neto,
                               fp.FactPro_Unidades
                           },
                           Producto = new
                           {
                               fp.Producto.Prod_Id,
                               fp.Producto.Prod_Nombre
                           },
                           datosEnvio = dataSend.Any() ? (dataSend).FirstOrDefault() : null,
                           detailsFact = details.Any() ? details.ToList() : null,
                       };
            return fact.Any() ? Ok(fact) : NotFound();
        }


        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturacion_Productos(long id, Facturacion_Productos Facturacion_Productos)
        {
            if (id != Facturacion_Productos.FactPro_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(Facturacion_Productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Facturacion_ProductosExists(id))
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
        [HttpPut("PutOfDirectDispatched/{of}")]
        public async Task<IActionResult> PutOfDirectDispatched(long of, List<rollsConsolidate> rollsConsolidate)
        {
            int count = 0;
            foreach (var item in rollsConsolidate)
            {
                var data = (from fp in _context.Set<Facturacion_Productos>() where fp.Prod_Id == item.item && fp.Of_Id == of select fp).FirstOrDefault();

                data.FactPro_Unidades = item.countProduction;
                data.Peso_Bruto = item.grossWeight;
                data.Peso_Neto = item.presentation == "Kg" ? item.quantity : item.grossWeight;

                _context.Entry(data).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == rollsConsolidate.Count()) return NoContent();
            }
            return NoContent();
        }

        //
        [HttpPost]
        public async Task<ActionResult<Facturacion_Productos>> PostFacturacion_Productos(Facturacion_Productos Facturacion_Productos)
        {
            _context.Facturacion_Productos.Add(Facturacion_Productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacturacion_Productos", new { id = Facturacion_Productos.FactPro_Codigo }, Facturacion_Productos);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturacion_Productos(long id)
        {
            var Facturacion_Productos = await _context.Facturacion_Productos.FindAsync(id);
            if (Facturacion_Productos == null)
            {
                return NotFound();
            }

            _context.Facturacion_Productos.Remove(Facturacion_Productos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Facturacion_ProductosExists(long id)
        {
            return _context.Facturacion_Productos.Any(e => e.FactPro_Codigo == id);
        }
    }
}

public class rollsConsolidate
{
    public int item { get; set; }

    public string reference { get; set; }

    [Precision(18,2)]
    public decimal quantity { get; set; }

    public string presentation { get; set; }

    [Precision(18, 2)]
    public decimal countProduction { get; set; }

    [Precision(18, 2)]
    public decimal grossWeight { get; set; }

    public string unit { get; set; }
}
