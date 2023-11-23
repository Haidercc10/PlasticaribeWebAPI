using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using ServiceReference1;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("EnviarAjuste/{ordenTrabajo}/{articulo}/{presentacion}/{rollo}/{cantidad}/{costo}")]
        public async Task<ActionResult> EnviarAjuste(string ordenTrabajo, string articulo, string presentacion, long rollo, decimal cantidad, decimal costo)
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
            PutEnvioZeus(rollo);
            return Convert.ToString(response.Status) == "SUCCESS" ? Ok(response) : BadRequest(response);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        [HttpPut("putEnvioZeus{rollo}")]
        public async Task<IActionResult> PutEnvioZeus(long rollo)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var dataProduction = (from prod in _context.Set<Produccion_Procesos>() where prod.Numero_Rollo == rollo select prod).FirstOrDefault();
            dataProduction.Envio_Zeus = true;
            _context.Entry(dataProduction).State = EntityState.Modified;
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
                                     orderby prod.Numero_Rollo descending
                                     select prod.Numero_Rollo).FirstOrDefault();

            produccion_Procesos.Numero_Rollo = numeroUltimoRollo + 1;
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