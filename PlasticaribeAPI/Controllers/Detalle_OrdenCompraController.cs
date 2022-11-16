using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("GetUltimaOrdenCompra")]
        public ActionResult GetUltimaOrdenCompra()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var consecutivo = _context.Ordenes_Compras.OrderByDescending(x => x.Oc_Id).Select(x => x.Oc_Id).First();

            var con = from dt in _context.Set<Detalle_OrdenCompra>()
                      from Oc in _context.Set<Orden_Compra>()
                      from Emp in _context.Set<Empresa>()
                      orderby dt.Oc_Id descending
                      where dt.Oc_Id == consecutivo 
                            && dt.Oc_Id == Oc.Oc_Id 
                            && Emp.Empresa_Id == 800188730
                      select new
                      {
                          Consecutivo = dt.Oc_Id,
                          Fecha = Oc.Oc_Fecha,
                          Hora = Oc.Oc_Hora,

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
                doc.Doc_CantidadPedida,
                doc.UndMed_Id,
                doc.Doc_PrecioUnitario
                
            }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (detalle_OrdenCompra == null)
            {
                return NotFound();
            }

            return Ok(detalle_OrdenCompra);
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
