using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using ServiceReference1;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.ServiceModel;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Produccion_ProcesosController : ControllerBase
    {
        private readonly dataContext _context;

        public Produccion_ProcesosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Produccion_Procesos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produccion_Procesos>>> GetProduccion_Procesos()
        {
            return await _context.Produccion_Procesos.ToListAsync();
        }

        // GET: api/Produccion_Procesos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produccion_Procesos>> GetProduccion_Procesos(int id)
        {
            var produccion_Procesos = await _context.Produccion_Procesos.FindAsync(id);

            if (produccion_Procesos == null)
            {
                return NotFound();
            }

            return produccion_Procesos;
        }

        // Consulta que devolverá toda la información de un rollo
        [HttpGet("getInformationAboutProductionToUpdateZeus/{production}/{process}")]
        public ActionResult GetInformationAboutProductionToUpdateZeus(long production, string process)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.NumeroRollo_BagPro == production &&
                             pp.Envio_Zeus == false &&
                             pp.Estado_Rollo == 19 &&
                             (process != "TODO" ? process == "SELLA" ? (pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE") : (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP") : (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP" || pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE"))
                       select new
                       {
                           pp,
                           pp.Clientes,
                           pp.Proceso,
                           pp.Producto,
                           pp.Turno,
                           pp.Operario1,
                           pp.Operario2,
                           pp.Operario3,
                           pp.Operario4,
                           pp.Cono,
                           pp.Creador,
                           numero_RolloBagPro = 0,
                       };
            return data.Any() ? Ok(data.Take(1)) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Consulta que devolverá toda la información de un rollo
        [HttpGet("getInformationAboutProduction/{production}/{process}")]
        public ActionResult GetInformationAboutProduction(long production, string process)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.NumeroRollo_BagPro == production &&
                            (process != "TODO" ? process == "SELLA" ? (pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE") : (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP") : (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP" || pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE"))
                       select new
                       {
                           pp,
                           pp.Clientes,
                           pp.Proceso,
                           pp.Producto,
                           pp.Turno,
                           pp.Operario1,
                           pp.Operario2,
                           pp.Operario3,
                           pp.Operario4,
                           pp.Cono,
                           pp.Creador,
                           numero_RolloBagPro = 0,
                       };
            return data.Any() ? Ok(data) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Consulta que devolverá la información de la producción pesada dependiendo del numero de rollo de bagpro
        [HttpGet("getInformationAboutProductionBagPro/{production}")]
        public ActionResult GetInformationAboutProductionBagPro(long production)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.NumeroRollo_BagPro == production &&
                             (pp.Estado_Rollo != 23) &&
                             (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP" || pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE")
                       orderby pp.Id descending
                       select new
                       {
                           pp,
                           pp.Clientes,
                           pp.Proceso,
                           pp.Producto,
                           pp.Turno,
                           pp.Operario1,
                           pp.Operario2,
                           pp.Operario3,
                           pp.Operario4,
                           pp.Cono,
                           pp.Creador,
                           numero_RolloBagPro = 0,
                       };
            return data.Any() ? Ok(data.Take(1)) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Consulta que devolverá la información de la producción pesada dependiendo del numero de rollo de bagpro
        [HttpGet("getInformationAboutProductionToSend/{production}/{orderFact}")]
        public ActionResult GetInformationAboutProductionToSend(long production, int orderFact)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var itemsOrder = (from dt in _context.Set<Detalles_OrdenFacturacion>() where dt.Id_OrdenFacturacion == orderFact select dt.Prod_Id).ToArray();

            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.NumeroRollo_BagPro == production &&
                             (pp.Estado_Rollo == 20) &&
                             itemsOrder.Contains(pp.Prod_Id) &&
                             (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP" || pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE")
                       orderby pp.Id descending
                       select new
                       {
                           pp,
                           pp.Clientes,
                           pp.Proceso,
                           pp.Producto,
                           pp.Turno,
                           pp.Operario1,
                           pp.Operario2,
                           pp.Operario3,
                           pp.Operario4,
                           pp.Cono,
                           pp.Creador,
                           numero_RolloBagPro = 0,
                       };
            return data.Any() ? Ok(data.Take(1)) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //consulta que devolverá la informacion de los rollos que está disponibles, es decir, los rollos que no han sido facturados o enviados al cliente
        [HttpGet("getAvaibleProduction/{item}")]
        public ActionResult GetAvaibleProduction(int item)
        {
            var avaibleProduction = from dev in _context.Set<DetalleDevolucion_ProductoFacturado>() where dev.Prod_Id == item select dev.Rollo_Id;

            var notAvaibleProduccion = from order in _context.Set<Detalles_OrdenFacturacion>()
                                       where !avaibleProduction.Contains(order.Numero_Rollo) && order.Prod_Id == item && order.OrdenFacturacion.Estado_Id != 3
                                       select order.Numero_Rollo;

            var production = from pp in _context.Set<Produccion_Procesos>()
                             join prod in _context.Set<Producto>() on pp.Prod_Id equals prod.Prod_Id
                             where pp.Prod_Id == item &&
                                   pp.Estado_Rollo == 19 &&
                                   pp.Envio_Zeus == true &&
                                   !notAvaibleProduccion.Contains(pp.NumeroRollo_BagPro) &&
                                   (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP" || pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE")
                             select new
                             {
                                 pp,
                                 prod,
                                 pp.Clientes
                             };
            return production.Any() ? Ok(production) : NotFound();
        }

        [HttpGet("getInformationAboutProductionByOrderProduction_Process/{process}/{turn}/{start}/{end}")]
        public ActionResult GetInformationAboutProductionByOrderProduction(string process, string turn, DateTime start, DateTime end)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var notAvaible = (from dt in _context.Set<DetallePreEntrega_RolloDespacho>() where dt.PreEntRollo_Id > 2538 select dt.Rollo_Id).ToList();
            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.Proceso_Id == process &&
                             pp.Fecha >= start &&
                             (turn == "DIA" ? pp.Fecha <= end : pp.Fecha <= end.AddDays(1)) &&
                             (turn == "DIA" ? true : pp.Id >= FirstProduction(start, process)) &&
                             (turn == "DIA" ? true : pp.Id <= LastProduction(end.AddDays(1), process)) &&
                             pp.Turno_Id == turn &&
                             pp.Envio_Zeus == false &&
                             pp.Estado_Rollo == 19 &&
                             !notAvaible.Contains(pp.NumeroRollo_BagPro)
                       orderby pp.Id ascending
                       select new
                       {
                           orderProduction = pp.OT,
                           item = pp.Prod_Id,
                           reference = pp.Producto.Prod_Nombre,
                           numberProduction = pp.NumeroRollo_BagPro,
                           quantity = pp.Presentacion == "Kg" ? pp.Peso_Neto : pp.Cantidad,
                           presentation = pp.Presentacion,
                           totalDate = pp.Fecha + " " + pp.Hora,
                           date = pp.Fecha,
                           hour = pp.Hora,
                           process = pp.Proceso_Id,
                           processName = pp.Proceso.Proceso_Nombre,
                       };
            return data.Any() ? Ok(data) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private int FirstProduction(DateTime date, string process)
        {
            return (from pp in _context.Set<Produccion_Procesos>()
                    where pp.Turno_Id == "NOCHE" &&
                          pp.Fecha >= date &&
                          pp.Proceso_Id == process &&
                          !pp.Hora.StartsWith("00") &&
                          !pp.Hora.StartsWith("01") &&
                          !pp.Hora.StartsWith("02") &&
                          !pp.Hora.StartsWith("03") &&
                          !pp.Hora.StartsWith("04") &&
                          !pp.Hora.StartsWith("05") &&
                          !pp.Hora.StartsWith("06") &&
                          !pp.Hora.StartsWith("07") &&
                          !pp.Hora.StartsWith("08")
                    select pp.Id).Min();
        }

        private int LastProduction(DateTime date, string process)
        {
            return (from pp in _context.Set<Produccion_Procesos>()
                    where pp.Turno_Id == "NOCHE" &&
                          pp.Fecha <= date &&
                          pp.Proceso_Id == process &&
                          (pp.Hora.StartsWith("00") ||
                           pp.Hora.StartsWith("01") ||
                           pp.Hora.StartsWith("02") ||
                           pp.Hora.StartsWith("03") ||
                           pp.Hora.StartsWith("04") ||
                           pp.Hora.StartsWith("05") ||
                           pp.Hora.StartsWith("06") ||
                           pp.Hora.StartsWith("07") ||
                           pp.Hora.StartsWith("08"))
                    select pp.Id).Max();
        }

        [HttpGet("EnviarAjuste/{ordenTrabajo}/{articulo}/{presentacion}/{rollo}/{cantidad}/{costo}")]
        public async Task<ActionResult> EnviarAjuste(string ordenTrabajo, string articulo, string presentacion, long rollo, string cantidad, string costo)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            SoapRequestAction request = new SoapRequestAction();
            request.User = "wsZeusInvProd";
            request.Password = "wsZeusInvProd";
            request.Body = $"<Ajuste>" +
                                $"<Op>I</Op>" +
                                $"<Cabecera>" +
                                    $"<Detalle>{ordenTrabajo}</Detalle>" +
                                    "<Concepto>001</Concepto>" +
                                    "<Consecutivo>0</Consecutivo>" +
                                    $"<Fecha>{today}</Fecha>" +
                                    "<Estado></Estado>" +
                                    "<Solicitante>7200000</Solicitante>" +
                                    "<Aprueba></Aprueba>" +
                                    "<Fuente>MA</Fuente>" +
                                    "<Serie>00</Serie>" +
                                    "<Usuario>zeussystem</Usuario>" +
                                    "<Documento></Documento>" +
                                    "<Documentorevertido></Documentorevertido>" +
                                    "<Bodega>003</Bodega>" +
                                    "<Grupo></Grupo>" +
                                    "<Origen>I</Origen>" +
                                    "<ConsecutivoRecosteo>0</ConsecutivoRecosteo>" +
                                    "<TipoDocumentoExterno></TipoDocumentoExterno>" +
                                    "<ConsecutivoExterno></ConsecutivoExterno>" +
                                    "<EsAjustePorDistribucion></EsAjustePorDistribucion>" +
                                    "<ItemsBodegaVirtual></ItemsBodegaVirtual>" +
                                    "<Clasificaciones></Clasificaciones>" +
                                    "<Propiedad1></Propiedad1>" +
                                    "<Propiedad2></Propiedad2>" +
                                    "<Propiedad3></Propiedad3>" +
                                    "<Propiedad4></Propiedad4>" +
                                    "<Propiedad5></Propiedad5>" +
                                    "<EsInicioNIIF></EsInicioNIIF>" +
                                    "<UtilizarZmlSpId></UtilizarZmlSpId>" +
                                    "<DatoExterno1></DatoExterno1>" +
                                    "<DatoExterno2></DatoExterno2>" +
                                    "<DatoExterno3></DatoExterno3>" +
                                    "<Moneda></Moneda>" +
                                    "<TasaCambio>1</TasaCambio>" +
                                    "<BU>Local</BU>" +
                                "</Cabecera>" +
                                "<Productos>" +
                                    $"<CodigoArticulo>{articulo}</CodigoArticulo>" +
                                    $"<Presentacion>{presentacion}</Presentacion>" +
                                    "<CodigoLote>0</CodigoLote>" +
                                    "<CodigoBodega>003</CodigoBodega>" +
                                    "<CodigoUbicacion></CodigoUbicacion>" +
                                    "<CodigoClasificacion>0</CodigoClasificacion>" +
                                    "<CodigoReferencia></CodigoReferencia>" +
                                    "<Serial>0</Serial>" +
                                    $"<Detalle>{rollo}</Detalle>" +
                                    $"<Cantidad>{cantidad}</Cantidad>" +
                                    $"<PrecioUnidad>{costo}</PrecioUnidad>" +
                                    $"<PrecioUnidad2>{costo}</PrecioUnidad2>" +
                                    "<Concepto_Codigo></Concepto_Codigo>" +
                                    "<TemporalItems_ValorAjuste></TemporalItems_ValorAjuste>" +
                                    "<Servicios>" +
                                    "<CodigoServicios>001</CodigoServicios>" +
                                    "<Referencia></Referencia>" +
                                    "<Detalle></Detalle>" +
                                    "<AuxiliarAbierto></AuxiliarAbierto>" +
                                    "<CentroCosto>0202</CentroCosto>" +
                                    "<Tercero>800188732</Tercero>" +
                                    "<Proveedor></Proveedor>" +
                                    "<TipoDocumentoCartera></TipoDocumentoCartera>" +
                                    "<DocumentoCartera></DocumentoCartera>" +
                                    "<Vencimiento></Vencimiento>" +
                                    "<Cliente></Cliente>" +
                                    "<Vendedor></Vendedor>" +
                                    "<ItemsContable></ItemsContable>" +
                                    "<Propiedad1></Propiedad1>" +
                                    "<Propiedad2></Propiedad2>" +
                                    "<Propiedad3></Propiedad3>" +
                                    "<Propiedad4></Propiedad4>" +
                                    "<Propiedad5></Propiedad5>" +
                                    "<CuentaMovimiento></CuentaMovimiento>" +
                                    "<Moneda></Moneda>" +
                                    "<Moneda></Moneda>" +
                                    "</Servicios>" +
                                "</Productos>" +
                            "</Ajuste>";
            request.DynamicProperty = "4";
            request.Action = "Inventario";
            request.TypeSQL = "true";

            var binding = new BasicHttpBinding()
            {
                Name = "BasicHttpBinding_IFakeService",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            var endpoint = new EndpointAddress("http://192.168.0.85/wsGenericoZeus/ServiceWS.asmx");
            WebservicesGenericoZeusSoapClient client = new WebservicesGenericoZeusSoapClient(binding, endpoint);
            SoapResponse response = await client.ExecuteActionSOAPAsync(request);
            if (Convert.ToString(response.Status) == "SUCCESS") PutStatusZeus(rollo, articulo);
            return Convert.ToString(response.Status) == "SUCCESS" ? Ok(response) : BadRequest(response);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        [HttpPut("putStatusZeus/{rollo}")]
        public async Task<IActionResult> PutStatusZeus(long rollo, string item)
        {
            var dataProduction = (from prod in _context.Set<Produccion_Procesos>()
                                  where prod.NumeroRollo_BagPro == rollo && prod.Prod_Id == Convert.ToInt64(item)
                                  select prod).FirstOrDefault();
            dataProduction.Envio_Zeus = true;
            dataProduction.Estado_Rollo = 19;
            _context.Entry(dataProduction).State = EntityState.Modified;
            _context.SaveChanges();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        //funcion que devolverá el primer rollo pesado
        private IQueryable<long> PrimerRolloPesado(DateTime fecha)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return from PS in _context.Set<Produccion_Procesos>()
                   where PS.Fecha >= fecha &&
                         !PS.Hora.StartsWith("00") &&
                         !PS.Hora.StartsWith("01") &&
                         !PS.Hora.StartsWith("02") &&
                         !PS.Hora.StartsWith("03") &&
                         !PS.Hora.StartsWith("04") &&
                         !PS.Hora.StartsWith("05") &&
                         !PS.Hora.StartsWith("06")
                   orderby PS.Numero_Rollo ascending
                   select PS.Numero_Rollo;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Funcion que devolverá todos los rollos pesados en el día que se le pase y los ordenará de manera descendente
        private IQueryable<long> UltimosRolloPesado(DateTime fecha)
        {
            return from PS2 in _context.Set<Produccion_Procesos>()
                   where PS2.Fecha <= fecha
                   orderby PS2.Numero_Rollo descending
                   select PS2.Numero_Rollo;
        }

        // Funcion que devolverá todos los rollos que hayan sido pesados en la fecha que se le pase y que sean solamente de las horas 7, 8 y 9
        private IQueryable<long> RollosPesadosMadrugada(DateTime fecha)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return from PS2 in _context.Set<Produccion_Procesos>()
                   where (PS2.Hora.StartsWith("07") ||
                         PS2.Hora.StartsWith("08") ||
                         PS2.Hora.StartsWith("09") ||
                         !PS2.Hora.StartsWith("0")) &&
                         PS2.Fecha == fecha
                   orderby PS2.Numero_Rollo ascending
                   select PS2.Numero_Rollo;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpGet("getNominaSellado/{inicio}/{fin}/{item}/{persona}")]
        public ActionResult GetNominaSellado(DateTime inicio, DateTime fin)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var nomina = from prod in _context.Set<Produccion_Procesos>()
                         where (prod.Proceso_Id == "SELLA" || prod.Proceso_Id == "WIKE") &&
                               prod.Fecha >= inicio &&
                               prod.Fecha <= fin &&
                               prod.Numero_Rollo >= PrimerRolloPesado(inicio).FirstOrDefault() &&
                               (RollosPesadosMadrugada(inicio.AddDays(1)).Any() ? prod.Numero_Rollo < RollosPesadosMadrugada(fin.AddDays(1)).FirstOrDefault() : prod.Numero_Rollo <= UltimosRolloPesado(fin).FirstOrDefault())
                         select new
                         {
                             prod.Operario1_Id,
                             prod.Operario2_Id,
                             prod.Operario3_Id,
                             prod.Operario4_Id,
                             Operario1 = prod.Operario1.Usua_Nombre,
                             Operario2 = prod.Operario2.Usua_Nombre,
                             Operario3 = prod.Operario3.Usua_Nombre,
                             Operario4 = prod.Operario4.Usua_Nombre,
                             prod.OT,
                             prod.Fecha,
                             prod.Hora,
                             prod.Prod_Id,
                             prod.Producto.Prod_Nombre,
                             prod.Numero_Rollo,
                             Cantidad = (prod.Cantidad / prod.Pesado_Entre),
                             CantidadTotal = prod.Cantidad,
                             prod.Presentacion,
                             prod.Maquina,
                             prod.Peso_Neto,
                             prod.Turno_Id,
                             prod.Proceso_Id,
                             prod.Proceso.Proceso_Nombre,
                             prod.Precio,
                             prod.Pesado_Entre,
                             Total = (prod.Cantidad * prod.Precio) / prod.Pesado_Entre,
                         };

            var result = new List<object>();
            foreach (var res in nomina)
            {
                string data = $"'Fecha': '{res.Fecha} {res.Hora}'," +
                    $"'Ot': '{res.OT}'," +
                    $"'Bulto': '{res.Numero_Rollo}'," +
                    $"'Referencia': '{res.Prod_Id}'," +
                    $"'Nombre_Referencia': '{res.Prod_Nombre.Replace("'", "`").Replace('"', '`')}'," +
                    $"'Cantidad_Total': '{res.CantidadTotal}'," +
                    $"'Cantidad': '{res.Cantidad}'," +
                    $"'Presentacion': '{res.Presentacion}'," +
                    $"'Maquina': '{res.Maquina}'," +
                    $"'Peso': '{res.Peso_Neto}'," +
                    $"'Turno': '{res.Turno_Id}'," +
                    $"'Proceso': '{res.Proceso_Nombre}'," +
                    $"'Precio': '{res.Precio}'," +
                    $"'Valor_Total': '{res.Total}'," +
                    $"'Pesado_Entre': '{res.Pesado_Entre}' ";

                result.Add($"'Cedula': '{res.Operario1_Id}', 'Operario': '{res.Operario1}', {data}");
                if (res.Operario2_Id != 0) result.Add($"'Cedula': '{res.Operario2_Id}', 'Operario': '{res.Operario2}', {data}");
                if (res.Operario3_Id != 0) result.Add($"'Cedula': '{res.Operario3_Id}', 'Operario': '{res.Operario3}', {data}");
                if (res.Operario4_Id != 0) result.Add($"'Cedula': '{res.Operario4_Id}', 'Operario': '{res.Operario4}', {data}");
            }

            return result.Count() > 0 ? Ok(result) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpGet("getInfoProduction/{date1}/{date2}")]
        public ActionResult getInfoProduction(DateTime date1, DateTime date2, string? ot = "", string? process = "", string? roll = "")
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            var data = from pp in _context.Set<Produccion_Procesos>()
                       from epot in _context.Set<Estados_ProcesosOT>()
                       where pp.OT == epot.EstProcOT_OrdenTrabajo &&
                             pp.Fecha >= date1 &&
                             pp.Fecha <= date2 &&
                             Convert.ToString(pp.OT).Contains(ot) &&
                             pp.Proceso_Id.Contains(process) &&
                             Convert.ToString(pp.NumeroRollo_BagPro).Contains(roll) &&
                             pp.Envio_Zeus == false &&
                             pp.Estado_Rollo == 19
                       select new
                       {
                           pp.Id,
                           pp.OT,
                           pp.Cli_Id,
                           pp.Clientes.Cli_Nombre,
                           pp.Prod_Id,
                           pp.Producto.Prod_Nombre,
                           Cantidad_Pedida = pp.Presentacion == "Kg" ? epot.EstProcOT_CantidadPedida : epot.EstProcOT_CantidadPedidaUnd,
                           pp.Numero_Rollo,
                           pp.NumeroRollo_BagPro,
                           pp.Producto.Prod_Ancho,
                           pp.Producto.Prod_Fuelle,
                           pp.Producto.Prod_Largo,
                           pp.Tara_Cono,
                           pp.Cono_Id,
                           pp.Producto.Material_Id,
                           pp.Producto.MaterialMP.Material_Nombre,
                           pp.Producto.Prod_Calibre,
                           pp.Fecha,
                           pp.Hora,
                           pp.Maquina,
                           pp.Operario1.Usua_Nombre,
                           pp.Proceso_Id,
                           pp.Turno_Id,
                           pp.Estado_Rollo,
                           pp.Estado.Estado_Nombre,
                           pp.Peso_Bruto,
                           pp.Peso_Neto,
                           pp.Cantidad,
                           pp.Presentacion,
                           pp.Proceso.Proceso_Nombre,
                       };
            return data.Any() ? Ok(data) : NotFound();
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Consulta que devuelve la información de la producción disponible de empaque y sellado.
        [HttpGet("getInfoProductionAvailable")]
        public ActionResult getInfoProductionAvailable()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            List<string> process = ["EMP", "SELLA"];

            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.Envio_Zeus == false &&
                             pp.Estado_Rollo == 19 &&
                             pp.Fecha >= Convert.ToDateTime("2024-02-04") &&
                             process.Contains(pp.Proceso_Id) &&
                             pp.Proceso_Id != "WIKE"
                       orderby pp.NumeroRollo_BagPro
                       select new
                       {
                           OT = pp.OT,
                           Id = pp.Id,
                           Roll = pp.Numero_Rollo,
                           Roll_BagPro = pp.NumeroRollo_BagPro,
                           Item = pp.Prod_Id,
                           Reference = pp.Producto.Prod_Nombre,
                           RealQty = pp.Presentacion == "Kg" ? pp.Peso_Neto : pp.Cantidad,
                           Qty = pp.Cantidad,
                           Gross_Weight = pp.Peso_Bruto,
                           Net_Weight = pp.Peso_Neto,
                           Presentation = pp.Presentacion,
                           Process_Id = pp.Proceso_Id,
                           Process = pp.Proceso.Proceso_Nombre,
                           Date = pp.Fecha + " " + pp.Hora,
                           Price = pp.PrecioVenta_Producto,
                           Subtotal = (pp.Presentacion == "Kg" ? pp.Peso_Neto : pp.Cantidad) * pp.PrecioVenta_Producto,
                           Client = pp.Clientes.Cli_Nombre,

                       };
            return data.Any() ? Ok(data) : NotFound();
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }  

        //Consulta que devuelve la información de la producción disponible de empaque y sellado.
        [HttpGet("getInfoItemsAvailablesOutPallet/{item}")]
        public ActionResult getInfoItemsAvailablesOutPallet(int item)
        {
            List<long> rollsInPallet = (from pe in _context.Set<PreEntrega_RolloDespacho>()
                                 from pp in _context.Set<Produccion_Procesos>()
                                 from p in _context.Set<Producto>()
                                 join dp in _context.Set<DetallePreEntrega_RolloDespacho>() on pp.NumeroRollo_BagPro equals dp.Rollo_Id
                                 join de in _context.Set<DetalleEntradaRollo_Producto>() on pp.Numero_Rollo equals de.Rollo_Id
                                 where pe.PreEntRollo_Id == dp.PreEntRollo_Id &&
                                 p.Prod_Id == pp.Prod_Id &&
                                 p.Prod_Id == dp.Prod_Id &&
                                 p.Prod_Id == de.Prod_Id &&
                                 pp.Estado_Rollo == 19 &&
                                 pp.Envio_Zeus == true &&
                                 pp.Prod_Id == item
                                 select pp.NumeroRollo_BagPro).ToList();

            var itemsOutPallet = from pp in _context.Set<Produccion_Procesos>()
                                from p in _context.Set<Producto>()
                                where pp.Prod_Id == p.Prod_Id &&
                                pp.Estado_Rollo == 19 &&
                                pp.Envio_Zeus == true &&
                                pp.Prod_Id == item &&
                                !rollsInPallet.Contains(pp.NumeroRollo_BagPro) &&
                                (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP" || pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE")
                                 group pp by new
                                 {
                                     pp.Prod_Id,
                                     pp.Producto.Prod_Nombre,
                                     pp.Cli_Id,
                                     pp.Clientes.Cli_Nombre,
                                     pp.Presentacion,
                                 }
                                 into grouped
                                 select new
                                 {
                                    Pallet = Convert.ToString("FP-") + Convert.ToString(grouped.Key.Prod_Id),
                                    Client_Id = grouped.Key.Cli_Id,
                                    Client = grouped.Key.Cli_Nombre,
                                    Item = grouped.Key.Prod_Id,
                                    Reference = grouped.Key.Prod_Nombre,
                                    Qty = grouped.Sum(x => x.Presentacion == "Kg" ? x.Peso_Neto : x.Cantidad),
                                    Presentation = grouped.Key.Presentacion,
                                    SaleOrder = 0,
                                    QtyRolls = grouped.Count(),
                                    Rolls =
                                    (
                                       from pp in _context.Set<Produccion_Procesos>()
                                       from p in _context.Set<Producto>()
                                       where pp.Prod_Id == p.Prod_Id &&
                                       pp.Estado_Rollo == 19 &&
                                       pp.Envio_Zeus == true &&
                                       pp.Prod_Id == item &&
                                       !rollsInPallet.Contains(pp.NumeroRollo_BagPro) &&
                                       (pp.Proceso_Id == "EXT" || pp.Proceso_Id == "EMP" || pp.Proceso_Id == "SELLA" || pp.Proceso_Id == "WIKE")
                                       select new
                                       {
                                           Pallet = Convert.ToString("FP-") + Convert.ToString(grouped.Key.Prod_Id),
                                           Roll_BagPro = pp.NumeroRollo_BagPro,
                                           Roll = pp.Numero_Rollo,
                                           Client_Id = pp.Cli_Id,
                                           Client = pp.Clientes.Cli_Nombre,
                                           OT = pp.OT,
                                           Item = pp.Prod_Id,
                                           Reference = p.Prod_Nombre,
                                           Qty = pp.Presentacion == "Kg" ? pp.Peso_Neto : pp.Cantidad,
                                           Presentation = pp.Presentacion,
                                           Process_Id = pp.Proceso_Id,
                                           Process = pp.Proceso.Proceso_Nombre,
                                           SaleOrder = 0,
                                       }
                                    ).ToList()
                                     
                                 };
            return Ok(itemsOutPallet);
        }

        //Consulta que devuelve la información de la producción disponible de empaque y sellado.
        [HttpGet("getInfoItemsAvailablesInPallet/{item}")]
        public ActionResult getInfoItemsAvailablesInPallet(int item)
        {
            var itemsInPallet = from pe in _context.Set<PreEntrega_RolloDespacho>()
                                from pp in _context.Set<Produccion_Procesos>()
                                from p in _context.Set<Producto>()
                                join dp in _context.Set<DetallePreEntrega_RolloDespacho>() on pp.NumeroRollo_BagPro equals dp.Rollo_Id
                                join de in _context.Set<DetalleEntradaRollo_Producto>() on pp.Numero_Rollo equals de.Rollo_Id
                                where pe.PreEntRollo_Id == dp.PreEntRollo_Id &&
                                p.Prod_Id == pp.Prod_Id &&
                                p.Prod_Id == dp.Prod_Id &&
                                p.Prod_Id == de.Prod_Id &&
                                pp.Estado_Rollo == 19 &&
                                pp.Envio_Zeus == true &&
                                pp.Prod_Id == item
                                group pp by new
                                {
                                    pe.PreEntRollo_Id,
                                    pp.Prod_Id,
                                    pp.Producto.Prod_Nombre,
                                    pp.Cli_Id,
                                    pp.Clientes.Cli_Nombre,
                                    pp.Presentacion,
                                }
                                  into grouped
                                select new
                                {
                                   Pallet = Convert.ToString("PL-") + Convert.ToString(grouped.Key.PreEntRollo_Id),
                                   Client_Id = grouped.Key.Cli_Id,
                                   Client = grouped.Key.Cli_Nombre,
                                   Item = grouped.Key.Prod_Id,
                                   Reference = grouped.Key.Prod_Nombre,
                                   Qty = grouped.Sum(x => x.Presentacion == "Kg" ? x.Peso_Neto : x.Cantidad),
                                   Presentation = grouped.Key.Presentacion,
                                   SaleOrder = 0,
                                   QtyRolls = grouped.Count(),
                                   Rolls =
                                   (
                                       from pe in _context.Set<PreEntrega_RolloDespacho>()
                                       from pp in _context.Set<Produccion_Procesos>()
                                       from p in _context.Set<Producto>()
                                       join dp in _context.Set<DetallePreEntrega_RolloDespacho>() on pp.NumeroRollo_BagPro equals dp.Rollo_Id
                                       join de in _context.Set<DetalleEntradaRollo_Producto>() on pp.Numero_Rollo equals de.Rollo_Id
                                       where pe.PreEntRollo_Id == dp.PreEntRollo_Id &&
                                       p.Prod_Id == pp.Prod_Id &&
                                       p.Prod_Id == dp.Prod_Id &&
                                       p.Prod_Id == de.Prod_Id &&
                                       pp.Estado_Rollo == 19 &&
                                       pp.Envio_Zeus == true &&
                                       pp.Prod_Id == item &&
                                       pe.PreEntRollo_Id == grouped.Key.PreEntRollo_Id
                                       select new
                                       {
                                           Pallet = Convert.ToString("PL-") + Convert.ToString(grouped.Key.PreEntRollo_Id),
                                           Roll_BagPro = pp.NumeroRollo_BagPro,
                                           Roll = pp.Numero_Rollo,
                                           OT = pp.OT,
                                           Client_Id = pp.Cli_Id,
                                           Client = pp.Clientes.Cli_Nombre,
                                           Item = pp.Prod_Id,
                                           Reference = p.Prod_Nombre,
                                           Qty = pp.Presentacion == "Kg" ? pp.Peso_Neto : pp.Cantidad,
                                           Presentation = pp.Presentacion,
                                           Process_Id = pp.Proceso_Id,
                                           Process = pp.Proceso.Proceso_Nombre,
                                           SaleOrder = 0,
                                       }
                                   ).ToList()
                                };
            return Ok(itemsInPallet);
        }


        [HttpPut("putExistencia/{producto}/{presentacion}/{precio}/{cantidad}")]
        public async Task<IActionResult> PutExistencia(int producto, string presentacion, decimal precio, decimal cantidad)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (presentacion == "KLS") presentacion = "Kg";
            else if (presentacion == "UND") presentacion = "Und";
            else if (presentacion == "PAQ") presentacion = "Paquete";

            var existencia = (from exis in _context.Set<Existencia_Productos>() where exis.Prod_Id == producto && exis.UndMed_Id == presentacion select exis).FirstOrDefault();
            existencia.ExProd_PrecioVenta = precio;
            existencia.ExProd_Cantidad += cantidad;
            existencia.ExProd_PrecioExistencia = precio * existencia.ExProd_Cantidad;
            _context.Entry(existencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpPut("putEnvioZeus/{rollo}")]
        public async Task<IActionResult> PutEnvioZeus(long rollo)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var dataProduction = (from prod in _context.Set<Produccion_Procesos>() where prod.Numero_Rollo == rollo select prod).FirstOrDefault();
            dataProduction.Envio_Zeus = true;
            dataProduction.Estado_Rollo = 19;
            _context.Entry(dataProduction).State = EntityState.Modified;
            _context.SaveChanges();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolloExists(rollo)) return NotFound();
                else throw;
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpPut("putCambiarEstadoRollo/{rollo}")]
        public async Task<IActionResult> PutcambiarEstadoRollo(long rollo)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var dataProduction = (from prod in _context.Set<Produccion_Procesos>() where prod.Numero_Rollo == rollo select prod).FirstOrDefault();
            dataProduction.Estado_Rollo = 23;
            _context.Entry(dataProduction).State = EntityState.Modified;
            _context.SaveChanges();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolloExists(rollo)) return NotFound();
                else throw;
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // PUT: api/Produccion_Procesos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduccion_Procesos(int id, Produccion_Procesos produccion_Procesos)
        {
            if (id != produccion_Procesos.Id)
            {
                return BadRequest();
            }
            _context.Entry(produccion_Procesos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Produccion_ProcesosExists(id))
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

        [HttpPut("putEstadoEntregado_NoIngresado/{preEntrega}")]
        async public Task<IActionResult> PutEstadoEntregado_NoIngresado(int preEntrega)
        {
            var rollos = from pre in _context.Set<DetallePreEntrega_RolloDespacho>()
                         join pp in _context.Set<Produccion_Procesos>() on pre.Rollo_Id equals pp.NumeroRollo_BagPro
                         where pre.PreEntRollo_Id == preEntrega && pre.Prod_Id == pp.Prod_Id
                         select pp;

            int count = 0;
            foreach (var item in rollos)
            {
                item.Estado_Rollo = 31;
                item.Envio_Zeus = false;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == rollos.Count()) return NoContent();
            }
            return NoContent();
        }

        [HttpPut("putEstadoEntregado_Ingresado/{entrada}")]
        async public Task<IActionResult> PutEstadoEntregado_Ingresado(int entrada)
        {
            var rollos = from ent in _context.Set<DetalleEntradaRollo_Producto>()
                         join pp in _context.Set<Produccion_Procesos>() on ent.Rollo_Id equals pp.Numero_Rollo
                         where ent.EntRolloProd_Id == entrada && ent.Prod_Id == pp.Prod_Id
                         select pp;

            int count = 0;
            foreach (var item in rollos)
            {
                item.Estado_Rollo = 19;
                item.Envio_Zeus = true;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == rollos.Count()) return NoContent();
            }
            return NoContent();
        }

        [HttpPut("putEstadoNoDisponible/{orden}")]
        async public Task<IActionResult> PutEstadoNoDisponible(int orden)
        {
            var rollos = from of in _context.Set<Detalles_OrdenFacturacion>()
                         join pp in _context.Set<Produccion_Procesos>() on of.Numero_Rollo equals pp.NumeroRollo_BagPro
                         where of.Id_OrdenFacturacion == orden && of.Prod_Id == pp.Prod_Id
                         select pp;

            int count = 0;
            foreach (var item in rollos)
            {
                item.Estado_Rollo = 23;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == rollos.Count()) return NoContent();
            }
            return NoContent();
        }

        [HttpPut("putEstadoPorDespachar/{orden}")]
        async public Task<IActionResult> PutEstadoPorDespachar(int orden)
        {
            var rollos = from of in _context.Set<Detalles_OrdenFacturacion>()
                         join pp in _context.Set<Produccion_Procesos>() on of.Numero_Rollo equals pp.NumeroRollo_BagPro
                         where of.Id_OrdenFacturacion == orden && of.Prod_Id == pp.Prod_Id
                         select pp;

            int count = 0;
            foreach (var item in rollos)
            {
                item.Estado_Rollo = 20;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == rollos.Count()) return NoContent();
            }
            return NoContent();
        }

        [HttpPut("putEstadoDisponible/{orden}")]
        async public Task<IActionResult> PutEstadoDisponible(int orden)
        {
            var rollos = from of in _context.Set<Detalles_OrdenFacturacion>()
                         join pp in _context.Set<Produccion_Procesos>() on of.Numero_Rollo equals pp.NumeroRollo_BagPro
                         where of.Id_OrdenFacturacion == orden
                         select pp;

            int count = 0;
            foreach (var item in rollos)
            {
                item.Estado_Rollo = 19;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolloExists(item.Numero_Rollo)) return NotFound();
                    else throw;
                }
                count++;
                if (count == rollos.Count()) return NoContent();
            }
            return NoContent();
        } 

        //.Función que recibirá los rollos a los que se les revertirá (actualizará) el Envio Zeus a 0 y el estado del rollo en 19 (Traslado)
        [HttpPost("putReversionEnvioZeus")]
        async public Task<IActionResult> putReversionEnvioZeus([FromBody] List<long> rolls)
        {
            int count = 0;
            foreach (var roll in rolls)
            {
                var dataProduction = (from prod in _context.Set<Produccion_Procesos>() where prod.Numero_Rollo == roll select prod).FirstOrDefault();
                dataProduction.Estado_Rollo = 19;
                dataProduction.Envio_Zeus = false;
                _context.Entry(dataProduction).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolloExists(roll)) return NotFound();
                    else throw;
                }
                count++;
                if (count == rolls.Count()) return NoContent();
            }
            return NoContent();
        }

        //Función que cambiará el estado de los rollos a Eliminados.
        [HttpPut("putStateDeletedRolls")]
        public async Task<IActionResult> putStateDeletedRolls(List<rollsToDelete> rollsToDelete)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var rolls in rollsToDelete)
            {
                var roll = (from pp in _context.Set<Produccion_Procesos>() where pp.NumeroRollo_BagPro == rolls.roll && pp.Proceso_Id == rolls.process select pp).FirstOrDefault();

                roll.Estado_Rollo = 22;
                _context.Entry(roll).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // POST: api/Produccion_Procesos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produccion_Procesos>> PostProduccion_Procesos(Produccion_Procesos produccion_Procesos)
        {
            var numeroUltimoRollo = (from prod in _context.Set<Produccion_Procesos>()
                                     orderby prod.Id descending
                                     select prod.Numero_Rollo).FirstOrDefault();

            var seed = Environment.TickCount;
            var random = new Random(seed);
            var value = random.Next(1, 15);
            var value2 = random.Next(1, 10);

            produccion_Procesos.Numero_Rollo = numeroUltimoRollo + value + value2;
            produccion_Procesos.Estado_Rollo = 19;
            produccion_Procesos.Fecha = DateTime.Now;
            _context.Produccion_Procesos.Add(produccion_Procesos);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduccion_Procesos", new { id = produccion_Procesos.Id }, produccion_Procesos);
        }

        // DELETE: api/Produccion_Procesos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccion_Procesos(int id)
        {
            var produccion_Procesos = await _context.Produccion_Procesos.FindAsync(id);
            if (produccion_Procesos == null)
            {
                return NotFound();
            }

            _context.Produccion_Procesos.Remove(produccion_Procesos);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool Produccion_ProcesosExists(int id)
        {
            return _context.Produccion_Procesos.Any(e => e.Id == id);
        }

        private bool RolloExists(long numeroRollo)
        {
            return _context.Produccion_Procesos.Any(x => x.Numero_Rollo == numeroRollo);
        }
    }
}
public class rollsToDelete
{
    public long roll { get; set; }
    public string process { get; set; }

}