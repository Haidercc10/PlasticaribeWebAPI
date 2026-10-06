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
        public async Task<ActionResult> getInfoOfDirect(int id)
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
                              Ubication = "",
                              dataProduction = (from pp in _context.Set<Produccion_Procesos>()
                                                where pp.NumeroRollo_BagPro == dtOrder.Numero_Rollo && pp.Prod_Id == dtOrder.Prod_Id
                                                select new
                                                {
                                                    OrdenProduction = pp.OT,
                                                    Weight = pp.Peso_Bruto,
                                                    NetWeight = pp.Peso_Neto,
                                                    Item = pp.Prod_Id,
                                                    Etiqueta = pp.NumeroRollo_BagPro
                                                }).FirstOrDefault(),
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
                               Of_Directa = order.Of_Directa == true ? "Si" : "No",
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
                           Asesor = new
                           {
                               order.Asesor_Id,
                               order.Asesor_Comercial.Usua_Nombre
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
                           Sede = (from sedes in _context.Set<SedesClientes>() 
                                   where sedes.Cli_Id == order.Cli_Id 
                                   select new {
                                       City = sedes.SedeCliente_Ciudad,
                                       Direction = sedes.SedeCliente_Direccion
                                   }).FirstOrDefault(),
                       };

            var result = await fact.ToListAsync();

            return fact.Any() ? Ok(fact) : NotFound();
        }

        [HttpGet("getInfoOfDirectAsync/{id}")]
        public async Task<ActionResult> GetInfoOfDirectAsync(int id)
        {
            // ============================================================
            // 1. INFORMACIÓN PRINCIPAL DE LA ORDEN
            // ============================================================

            var order = await _context.Set<OrdenFacturacion>()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.Factura,
                    x.Fecha,
                    x.Hora,
                    x.Observacion,
                    x.Estado_Id,

                    Of_Directa = x.Of_Directa == true ? "Si" : "No",

                    Clientes = new
                    {
                        x.Clientes.Cli_Id,
                        x.Clientes.Cli_Nombre,
                        x.Clientes.Cli_Telefono,
                        x.Clientes.Cli_Email,
                        x.Clientes.TipoIdentificacion_Id
                    },

                    Usuario = new
                    {
                        x.Usuario.Usua_Id,
                        x.Usuario.Usua_Nombre
                    },

                    Asesor = new
                    {
                        x.Asesor_Id,
                        Nombre = x.Asesor_Comercial.Usua_Nombre
                    },

                    Cli_Id = x.Cli_Id
                })
                .FirstOrDefaultAsync();


            // ============================================================
            // 2. VALIDAR ORDEN
            // ============================================================

            if (order == null)
            {
                return NotFound();
            }


            // ============================================================
            // 3. PRODUCTOS FACTURADOS
            // ============================================================

            var factProducts = await _context.Set<Facturacion_Productos>()
                .AsNoTracking()
                .Where(fp => fp.Of_Id == id)
                .Select(fp => new
                {
                    FactPro_Codigo = fp.FactPro_Codigo,
                    FactPro_Cantidad = fp.FactPro_Cantidad,
                    UndMed_Id = fp.UndMed_Id,
                    FactPro_Pedido = fp.FactPro_Pedido,
                    Peso_Bruto = fp.Peso_Bruto,
                    Peso_Neto = fp.Peso_Neto,
                    FactPro_Unidades = fp.FactPro_Unidades,

                    Producto = new
                    {
                        fp.Producto.Prod_Id,
                        fp.Producto.Prod_Nombre
                    }
                })
                .ToListAsync();


            // ============================================================
            // 4. VALIDAR PRODUCTOS FACTURADOS
            // ============================================================

            if (factProducts.Count == 0)
            {
                return NotFound();
            }


            // ============================================================
            // 5. DETALLES DE LA ORDEN
            // ============================================================

            var details = await _context.Set<Detalles_OrdenFacturacion>()
                .AsNoTracking()
                .Where(d => d.Id_OrdenFacturacion == id)
                .Select(d => new
                {
                    dtOrder = new
                    {
                        d.Id,
                        d.Cantidad,
                        d.Presentacion,
                        d.Numero_Rollo,
                        d.Consecutivo_Pedido,
                        d.Pallet_Id
                    },

                    Producto = new
                    {
                        d.Producto.Prod_Id,
                        d.Producto.Prod_Nombre
                    }
                })
                .ToListAsync();


            // ============================================================
            // 6. ROLLOS Y PRODUCTOS NECESARIOS PARA PRODUCCIÓN
            // ============================================================

            var rolls = details
                .Where(x => x.dtOrder.Numero_Rollo != null)
                .Select(x => x.dtOrder.Numero_Rollo)
                .Distinct()
                .ToList();

            var products = details
                .Select(x => x.Producto.Prod_Id)
                .Distinct()
                .ToList();


            // ============================================================
            // 7. PRODUCCIÓN
            // ============================================================

            var production = await _context.Set<Produccion_Procesos>()
                .AsNoTracking()
                .Where(pp =>
                    rolls.Contains(pp.NumeroRollo_BagPro) &&
                    products.Contains(pp.Prod_Id))
                .Select(pp => new
                {
                    OrdenProduction = pp.OT,
                    Weight = pp.Peso_Bruto,
                    NetWeight = pp.Peso_Neto,
                    Item = pp.Prod_Id,
                    Etiqueta = pp.NumeroRollo_BagPro
                })
                .ToListAsync();


            // ============================================================
            // 8. DICCIONARIO DE PRODUCCIÓN
            // ============================================================

            var productionDict = production
                .GroupBy(x => new
                {
                    x.Etiqueta,
                    x.Item
                })
                .ToDictionary(
                    g => (g.Key.Etiqueta, g.Key.Item),
                    g => g.First()
                );


            // ============================================================
            // 9. DATOS DE ENVÍO
            // ============================================================

            var dataSend = await _context.Set<AsignacionProducto_FacturaVenta>()
                .AsNoTracking()
                .Where(asg =>
                    asg.NotaCredito_Id == $"Orden de Facturación #{id}")
                .Select(asg => new
                {
                    Conductor = asg.Usuario.Usua_Nombre,
                    Placa = asg.AsigProdFV_PlacaCamion,
                    Observacion = asg.AsigProdFV_Observacion,
                    Fecha = asg.AsigProdFV_Fecha,
                    Hora = asg.AsigProdFV_Hora,
                    CreadoPor = asg.Usua.Usua_Nombre
                })
                .FirstOrDefaultAsync();


            // ============================================================
            // 10. SEDE DEL CLIENTE
            // ============================================================

            var sede = await _context.Set<SedesClientes>()
                .AsNoTracking()
                .Where(s => s.Cli_Id == order.Cli_Id)
                .Select(s => new
                {
                    City = s.SedeCliente_Ciudad,
                    Direction = s.SedeCliente_Direccion
                })
                .FirstOrDefaultAsync();


            // ============================================================
            // 11. ARMAR DETAILS
            // ============================================================

            var detailsFact = details
                .Select(detail =>
                {
                    productionDict.TryGetValue(
                        (
                            detail.dtOrder.Numero_Rollo,
                            detail.Producto.Prod_Id
                        ),
                        out var productionData);

                    return new
                    {
                        detail.dtOrder,
                        detail.Producto,

                        Ubication = "",

                        dataProduction = productionData
                    };
                })
                .ToList();


            // ============================================================
            // 12. ARMAR RESPUESTA FINAL
            // ============================================================

            var result = factProducts
                .Select(fp => new
                {
                    order = new
                    {
                        order.Id,
                        order.Factura,
                        order.Fecha,
                        order.Hora,
                        order.Observacion,
                        order.Estado_Id,
                        order.Of_Directa
                    },

                    order.Clientes,

                    order.Usuario,

                    order.Asesor,

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

                    fp.Producto,

                    datosEnvio = dataSend,

                    detailsFact,

                    Sede = sede
                })
                .ToList();

            return Ok(result);
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
