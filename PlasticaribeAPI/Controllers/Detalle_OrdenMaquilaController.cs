using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Detalle_OrdenMaquilaController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalle_OrdenMaquilaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalle_OrdenMaquila
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalle_OrdenMaquila>>> GetDetalle_OrdenMaquila()
        {
            if (_context.Detalle_OrdenMaquila == null)
            {
                return NotFound();
            }
            return await _context.Detalle_OrdenMaquila.ToListAsync();
        }

        // GET: api/Detalle_OrdenMaquila/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalle_OrdenMaquila>> GetDetalle_OrdenMaquila(long id)
        {
            if (_context.Detalle_OrdenMaquila == null)
            {
                return NotFound();
            }
            var detalle_OrdenMaquila = await _context.Detalle_OrdenMaquila.FindAsync(id);

            if (detalle_OrdenMaquila == null)
            {
                return NotFound();
            }

            return detalle_OrdenMaquila;
        }

        [HttpGet("getMateriaPrimaOrdenMaquila/{orden}/{mp}")]
        public ActionResult getMateriaPrimaOrdenMaquila(long orden, int mp)
        {
            var con = from om in _context.Set<Detalle_OrdenMaquila>()
                      where om.OM_Id == orden
                            && (om.BOPP_Id == mp
                                || om.MatPri_Id == mp
                                || om.Tinta_Id == mp)
                      select om.DtOM_Codigo;
            return Ok(con);
        }

        [HttpGet("getInfoOrdenMaquila_Id/{id}")]
        public ActionResult GetInfoOrdenMaquila(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dtOM in _context.Set<Detalle_OrdenMaquila>()
                      from Emp in _context.Set<Empresa>()
                      where dtOM.OM_Id == id
                            && Emp.Empresa_Id == 800188732
                      select new
                      {
                          Orden = dtOM.OM_Id,
                          Fecha = dtOM.Orden_Maquila.OM_Fecha,
                          Hora = dtOM.Orden_Maquila.OM_Hora,
                          Estado_Id = dtOM.Orden_Maquila.Estado_Id,
                          Estado = dtOM.Orden_Maquila.Estado.Estado_Nombre,

                          Tercero_Id = dtOM.Orden_Maquila.Tercero_Id,
                          Tipo_Id = dtOM.Orden_Maquila.Tercero.TipoIdentificacion_Id,
                          Tercero = dtOM.Orden_Maquila.Tercero.Tercero_Nombre,
                          Telefono_Tercero = dtOM.Orden_Maquila.Tercero.Tercero_Telefono,
                          Ciudad_Tercero = dtOM.Orden_Maquila.Tercero.Tercero_Ciudad,
                          Correo_Tercero = dtOM.Orden_Maquila.Tercero.Tercero_Email,

                          Observacion = dtOM.Orden_Maquila.OM_Observacion,
                          Usuario_Id = dtOM.Orden_Maquila.Usua_Id,
                          Usuario = dtOM.Orden_Maquila.Usua.Usua_Nombre,
                          Valor_Total = dtOM.Orden_Maquila.OM_ValorTotal,
                          Peso_Total = dtOM.Orden_Maquila.OM_PesoTotal,

                          Empresa_Id = Emp.Empresa_Id,
                          Empresa_Ciudad = Emp.Empresa_Ciudad,
                          Empresa_Codigo = Emp.Empresa_COdigoPostal,
                          Empresa_Correo = Emp.Empresa_Correo,
                          Empresa_Direccion = Emp.Empresa_Direccion,
                          Empresa_Telefono = Emp.Empresa_Telefono,
                          Empresa = Emp.Empresa_Nombre,

                          MP_Id = dtOM.MatPri_Id,
                          MP = dtOM.MatPrima.MatPri_Nombre,
                          Tinta_Id = dtOM.Tinta_Id,
                          Tinta = dtOM.Tinta.Tinta_Nombre,
                          Bopp_Id = dtOM.BOPP_Id,
                          Bopp = dtOM.BOPP.BOPP_Nombre,
                          Cantidad = dtOM.DtOM_Cantidad,
                          Und_Medida = dtOM.UndMed_Id,
                          Precio = dtOM.DtOM_PrecioUnitario,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // PUT: api/Detalle_OrdenMaquila/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle_OrdenMaquila(long id, Detalle_OrdenMaquila detalle_OrdenMaquila)
        {
            if (id != detalle_OrdenMaquila.DtOM_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalle_OrdenMaquila).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_OrdenMaquilaExists(id))
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

        // POST: api/Detalle_OrdenMaquila
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalle_OrdenMaquila>> PostDetalle_OrdenMaquila(Detalle_OrdenMaquila detalle_OrdenMaquila)
        {
            if (_context.Detalle_OrdenMaquila == null)
            {
                return Problem("Entity set 'dataContext.Detalle_OrdenMaquila'  is null.");
            }
            _context.Detalle_OrdenMaquila.Add(detalle_OrdenMaquila);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalle_OrdenMaquila", new { id = detalle_OrdenMaquila.DtOM_Codigo }, detalle_OrdenMaquila);
        }

        // DELETE: api/Detalle_OrdenMaquila/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalle_OrdenMaquila(long id)
        {
            if (_context.Detalle_OrdenMaquila == null)
            {
                return NotFound();
            }
            var detalle_OrdenMaquila = await _context.Detalle_OrdenMaquila.FindAsync(id);
            if (detalle_OrdenMaquila == null)
            {
                return NotFound();
            }

            _context.Detalle_OrdenMaquila.Remove(detalle_OrdenMaquila);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalle_OrdenMaquilaExists(long id)
        {
            return (_context.Detalle_OrdenMaquila?.Any(e => e.DtOM_Codigo == id)).GetValueOrDefault();
        }
    }
}
