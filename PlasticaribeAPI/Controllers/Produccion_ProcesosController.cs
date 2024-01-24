using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Migrations;
using PlasticaribeAPI.Models;
using ServiceReference1;

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
        [HttpGet("getInformationAboutProductionToUpdateZeus/{production}")]
        public ActionResult GetInformationAboutProductionToUpdateZeus(long production)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            /*var data = from pp in _context.Set<Produccion_Procesos>()
                       join ot in _context.Set<Orden_Trabajo>() on pp.OT equals ot.Numero_OT
                       join otExt in _context.Set<OT_Extrusion>() on ot.Ot_Id equals otExt.Ot_Id
                       where pp.Numero_Rollo == production && pp.Envio_Zeus == false
                       select new
                       {
                            orderProduction = pp.OT,
                            ot.MotrarEmpresaEtiquetas,
                            product = pp.Producto,
                            client = pp.Clientes,
                            turn = pp.Turno,
                            process = pp.Proceso,
                            material = otExt.Material_MatPrima,
                            dataExtrusion = otExt,
                            dataProduction = pp
                       };*/

            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.Numero_Rollo == production && pp.Envio_Zeus == false
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

        // Consulta que devolverá toda la información de un rollo
        [HttpGet("getInformationAboutProduction/{production}")]
        public ActionResult GetInformationAboutProduction(long production)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            /*var data = from pp in _context.Set<Produccion_Procesos>()
                       join ot in _context.Set<Orden_Trabajo>() on pp.OT equals ot.Numero_OT
                       join otExt in _context.Set<OT_Extrusion>() on ot.Ot_Id equals otExt.Ot_Id
                       where pp.Numero_Rollo == production && pp.Envio_Zeus == false
                       select new
                       {
                            orderProduction = pp.OT,
                            ot.MotrarEmpresaEtiquetas,
                            product = pp.Producto,
                            client = pp.Clientes,
                            turn = pp.Turno,
                            process = pp.Proceso,
                            material = otExt.Material_MatPrima,
                            dataExtrusion = otExt,
                            dataProduction = pp
                       };*/

            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.Numero_Rollo == production
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
                       };
            return data.Any() ? Ok(data) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //consulta que devolverá la informacion de los rollos que está disponibles, es decir, los rollos que no han sido facturados o enviados al cliente
        [HttpGet("getAvaibleProduction/{item}")]
        public ActionResult GetAvaibleProduction(int item)
        {
            var notAvaibleProduccion = from order in _context.Set<Detalles_OrdenFacturacion>()
                                       select order.Numero_Rollo;

            var production = from pp in _context.Set<Produccion_Procesos>()
                             join prod in _context.Set<Producto>() on pp.Prod_Id equals prod.Prod_Id
                             where pp.Prod_Id == item && 
                                   pp.Estado_Rollo == 19 && 
                                   pp.Envio_Zeus == true &&
                                   !notAvaibleProduccion.Contains(pp.NumeroRollo_BagPro)
                             select new
                             {
                                 pp,
                                 prod
                             };
            return production.Any() ? Ok(production) : NotFound();
        }

        [HttpGet("getInformationAboutProductionByOrderProduction_Process/{orderProduction}/{process}")]
        public ActionResult GetInformationAboutProductionByOrderProduction(long orderProduction, string process)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var data = from pp in _context.Set<Produccion_Procesos>()
                       where pp.OT == orderProduction &&
                             pp.Proceso_Id == process &&
                             pp.Envio_Zeus == false &&
                             pp.Estado_Rollo == 19
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
                       };
            return data.Any() ? Ok(data) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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
            //PutExistencia(Convert.ToInt32(articulo), presentacion, Convert.ToDecimal(costo), Convert.ToDecimal(cantidad));
            //PutEnvioZeus(rollo);
            return Convert.ToString(response.Status) == "SUCCESS" ? Ok(response) : BadRequest(response);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
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
                       where pp.Fecha >= date1 &&
                             pp.Fecha <= date2 &&
                             Convert.ToString(pp.OT).Contains(ot) &&
                             pp.Proceso_Id.Contains(process) &&
                             Convert.ToString(pp.NumeroRollo_BagPro).Contains(roll) &&
                             pp.Envio_Zeus == false &&
                             pp.Estado_Rollo == 19
                       select new
                       {
                           pp.OT,
                           pp.Id,
                           pp.Numero_Rollo,
                           pp.NumeroRollo_BagPro,
                           pp.Prod_Id,
                           pp.Producto.Prod_Nombre,
                           pp.Cantidad,
                           pp.Peso_Bruto,
                           pp.Peso_Neto,
                           pp.Presentacion,
                           pp.Proceso_Id,
                           pp.Proceso.Proceso_Nombre,
                           pp.Fecha,
                       };
            return data.Any() ? Ok(data) : NotFound();
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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
                throw;
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

    }
}