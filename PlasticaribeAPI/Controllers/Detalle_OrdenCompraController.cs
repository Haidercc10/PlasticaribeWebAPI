using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Detalle_OrdenCompraController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalle_OrdenCompraController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalle_OrdenCompra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalle_OrdenCompra>>> GetDetalles_OrdenesCompras()
        {
            return await _context.Detalles_OrdenesCompras.ToListAsync();
        }

        //Funcion que traerá la informacion de la ultima orden de compra creada
        [HttpGet("GetOrdenCompra/{orden}")]
        public ActionResult GetOrdenCompra(int orden)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dt in _context.Set<Detalle_OrdenCompra>()
                      from Oc in _context.Set<Orden_Compra>()
                      from Emp in _context.Set<Empresa>()
                      orderby dt.Oc_Id descending
                      where dt.Oc_Id == orden
                            && dt.Oc_Id == Oc.Oc_Id
                            && Emp.Empresa_Id == 800188732
                      select new
                      {
                          Consecutivo = dt.Oc_Id,
                          Fecha = Oc.Oc_Fecha,
                          Hora = Oc.Oc_Hora,
                          Estado_Id = Oc.Estado_Id,
                          Estado = Oc.Estado.Estado_Nombre,

                          Proveedor_Id = Oc.Prov_Id,
                          Tipo_Id = Oc.Proveedor.TipoIdentificacion_Id,
                          Proveedor = Oc.Proveedor.Prov_Nombre,
                          Tipo_Proveedor = Oc.Proveedor.TpProv.TpProv_Nombre,
                          Telefono_Proveedor = Oc.Proveedor.Prov_Telefono,
                          Ciudad_Proveedor = Oc.Proveedor.Prov_Ciudad,
                          Correo_Proveedor = Oc.Proveedor.Prov_Email,

                          Observacion = Oc.Oc_Observacion,
                          Usuario_Id = Oc.Usua_Id,
                          Usuario = Oc.Usua.Usua_Nombre,
                          Valor_Total = Oc.Oc_ValorTotal,
                          Peso_Total = Oc.Oc_PesoTotal,
                          iva = Oc.IVA,

                          Empresa_Id = Emp.Empresa_Id,
                          Empresa_Ciudad = Emp.Empresa_Ciudad,
                          Empresa_Codigo = Emp.Empresa_COdigoPostal,
                          Empresa_Correo = Emp.Empresa_Correo,
                          Empresa_Direccion = Emp.Empresa_Direccion,
                          Empresa_Telefono = Emp.Empresa_Telefono,
                          Empresa = Emp.Empresa_Nombre,

                          MP_Id = dt.MatPri_Id,
                          MP = dt.MatPrima.MatPri_Nombre,
                          Tinta_Id = dt.Tinta_Id,
                          Tinta = dt.Tinta.Tinta_Nombre,
                          Bopp_Id = dt.BOPP_Id,
                          Bopp = dt.BOPP.BoppGen_Nombre,
                          Cantidad = dt.Doc_CantidadPedida,
                          Unidad_Medida = dt.UndMed_Id,
                          Precio_Unitario = dt.Doc_PrecioUnitario,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Funcion que va a devolver los datos de las ordenes de compra realizadas
        [HttpGet("getOrdenesCompras/{fechaInicial}/{fechaFinal}")]
        public ActionResult GetOrdenesCompras(DateTime fechaInicial, DateTime fechaFinal, string? orden = "", string? estado = "")
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var con = from oc in _context.Set<Detalle_OrdenCompra>()
                      from Emp in _context.Set<Empresa>()
                      where oc.Orden_Compra.Oc_Fecha >= fechaInicial &&
                            oc.Orden_Compra.Oc_Fecha <= fechaFinal &&
                            Emp.Empresa_Id == 800188732 &&
                            Convert.ToString(oc.Oc_Id).Contains(orden) &&
                            Convert.ToString(oc.Orden_Compra.Estado_Id).Contains(estado)
                      select new
                      {
                          Consecutivo = oc.Oc_Id,
                          Fecha = oc.Orden_Compra.Oc_Fecha,
                          Hora = oc.Orden_Compra.Oc_Hora,
                          Estado_Id = oc.Orden_Compra.Estado_Id,
                          Estado = oc.Orden_Compra.Estado.Estado_Nombre,

                          Proveedor_Id = oc.Orden_Compra.Prov_Id,
                          Tipo_Id = oc.Orden_Compra.Proveedor.TipoIdentificacion_Id,
                          Proveedor = oc.Orden_Compra.Proveedor.Prov_Nombre,
                          Tipo_Proveedor = oc.Orden_Compra.Proveedor.TpProv.TpProv_Nombre,
                          Telefono_Proveedor = oc.Orden_Compra.Proveedor.Prov_Telefono,
                          Ciudad_Proveedor = oc.Orden_Compra.Proveedor.Prov_Ciudad,
                          Correo_Proveedor = oc.Orden_Compra.Proveedor.Prov_Email,

                          Observacion = oc.Orden_Compra.Oc_Observacion,
                          Usuario_Id = oc.Orden_Compra.Usua_Id,
                          Usuario = oc.Orden_Compra.Usua.Usua_Nombre,
                          Valor_Total = oc.Orden_Compra.Oc_ValorTotal,
                          Peso_Total = oc.Orden_Compra.Oc_PesoTotal,

                          Empresa_Id = Emp.Empresa_Id,
                          Empresa_Ciudad = Emp.Empresa_Ciudad,
                          Empresa_Codigo = Emp.Empresa_COdigoPostal,
                          Empresa_Correo = Emp.Empresa_Correo,
                          Empresa_Direccion = Emp.Empresa_Direccion,
                          Empresa_Telefono = Emp.Empresa_Telefono,
                          Empresa = Emp.Empresa_Nombre,

                          MP_Id = oc.MatPri_Id,
                          MP = oc.MatPrima.MatPri_Nombre,
                          Tinta_Id = oc.Tinta_Id,
                          Tinta = oc.Tinta.Tinta_Nombre,
                          Bopp_Id = oc.BOPP_Id,
                          Bopp = oc.BOPP.BoppGen_Nombre,
                          Cantidad = oc.Doc_CantidadPedida,
                          Unidad_Medida = oc.UndMed_Id,
                          Precio_Unitario = oc.Doc_PrecioUnitario,
                      };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return con.Any() ? Ok(con) : NotFound("¡No se encontró información!");
        }

        // GET: api/Detalle_OrdenCompra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalle_OrdenCompra>> GetDetalle_OrdenCompra(long id)
        {
            var detalle_OrdenCompra = await _context.Detalles_OrdenesCompras.FindAsync(id);

            if (detalle_OrdenCompra == null)
            {
                return NotFound();
            }

            return detalle_OrdenCompra;
        }

        [HttpGet("InfoOrdenCompraxId/{OC}")]
        public  ActionResult GetDetalle_OrdenCompraxID(long OC)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var detalle_OrdenCompra =  _context.Detalles_OrdenesCompras.Where(d => d.Oc_Id == OC).Select(doc => new
            {
                doc.Oc_Id,
                doc.MatPri_Id,
                doc.MatPrima.MatPri_Nombre,
                doc.Tinta_Id,
                doc.Tinta.Tinta_Nombre,
                doc.BOPP_Id,
                doc.BOPP.BoppGen_Nombre,
                doc.BOPP.BoppGen_Micra,
                doc.BOPP.BoppGen_Ancho,
                doc.Doc_CantidadPedida,
                doc.UndMed_Id,
                doc.Doc_PrecioUnitario,
                doc.Orden_Compra.Prov_Id,
                doc.Orden_Compra.Proveedor.Prov_Nombre,
                
            }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (detalle_OrdenCompra == null)
            {
                return NotFound();
            }

            return Ok(detalle_OrdenCompra);
        }

        [HttpGet("getMateriaPrimaOrdenCompa/{orden}/{mp}")]
        public ActionResult getMateriaPrimaOrdenCompa(long orden, int mp)
        {
            var con = from oc in _context.Set<Detalle_OrdenCompra>()
                      where oc.Oc_Id == orden
                            && (oc.BOPP_Id == mp
                                || oc.MatPri_Id == mp
                                || oc.Tinta_Id == mp)
                      select oc.Doc_Codigo;
            return Ok(con);
        }

        // PUT: api/Detalle_OrdenCompra/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle_OrdenCompra(long id, Detalle_OrdenCompra detalle_OrdenCompra)
        {
            if (id != detalle_OrdenCompra.Doc_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalle_OrdenCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_OrdenCompraExists(id))
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

        // POST: api/Detalle_OrdenCompra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalle_OrdenCompra>> PostDetalle_OrdenCompra(Detalle_OrdenCompra detalle_OrdenCompra)
        {
            _context.Detalles_OrdenesCompras.Add(detalle_OrdenCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalle_OrdenCompra", new { id = detalle_OrdenCompra.Doc_Codigo }, detalle_OrdenCompra);
        }

        // DELETE: api/Detalle_OrdenCompra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalle_OrdenCompra(long id)
        {
            var detalle_OrdenCompra = await _context.Detalles_OrdenesCompras.FindAsync(id);
            if (detalle_OrdenCompra == null)
            {
                return NotFound();
            }

            _context.Detalles_OrdenesCompras.Remove(detalle_OrdenCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }   

        private bool Detalle_OrdenCompraExists(long id)
        {
            return _context.Detalles_OrdenesCompras.Any(e => e.Doc_Codigo == id);
        }
    }
}
