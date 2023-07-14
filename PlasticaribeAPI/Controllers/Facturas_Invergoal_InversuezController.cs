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
    public class Facturas_Invergoal_InversuezController : ControllerBase
    {
        private readonly dataContext _context;

        public Facturas_Invergoal_InversuezController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Facturas_Invergoal_Inversuez
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facturas_Invergoal_Inversuez>>> GetFacturas_Invergoal_Inversuez()
        {
          if (_context.Facturas_Invergoal_Inversuez == null)
          {
              return NotFound();
          }
            return await _context.Facturas_Invergoal_Inversuez.ToListAsync();
        }

        // GET: api/Facturas_Invergoal_Inversuez/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facturas_Invergoal_Inversuez>> GetFacturas_Invergoal_Inversuez(int id)
        {
          if (_context.Facturas_Invergoal_Inversuez == null)
          {
              return NotFound();
          }
            var facturas_Invergoal_Inversuez = await _context.Facturas_Invergoal_Inversuez.FindAsync(id);

            if (facturas_Invergoal_Inversuez == null)
            {
                return NotFound();
            }

            return facturas_Invergoal_Inversuez;
        }

        //Consulta que va a devolver la informacion de las facturas que sean consultadas
        [HttpGet("getFacturasIngresadas/{empresa}/{fechaInicial}/{fechaFinal}")]
        public ActionResult GetFacturasIngresadas(string empresa, DateTime fechaInicial, DateTime fechaFinal, string? codigo = "", string? proveedor = "", string? cuenta = "", string? estado = "")
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from fac in _context.Set<Facturas_Invergoal_Inversuez>()
                      where fac.Nit_Empresa == empresa &&
                            fac.Fecha_Factura >= fechaInicial &&
                            fac.Fecha_Factura <= fechaFinal &&
                            fac.Codigo_Factura.Contains(codigo) &&
                            Convert.ToString(fac.Nit_Proveedor).Contains(proveedor) &&
                            fac.Cuenta.Contains(cuenta) &&
                            Convert.ToString(fac.Estado_Factura).Contains(estado)
                      select new
                      {
                          fac.Id,
                          fac.Nit_Empresa,
                          fac.Nombre_Empresa,
                          fac.Usua_Id,
                          fac.Fecha_Registro,
                          fac.Hora_Registro,
                          fac.Codigo_Factura,
                          fac.Nit_Proveedor,
                          fac.Proveedor.Prov_Nombre,
                          fac.Fecha_Factura,
                          fac.Fecha_Vencimiento,
                          fac.Valor_Factura,
                          fac.Cuenta,
                          fac.Estado_Factura,
                          fac.Estados.Estado_Nombre,
                          fac.Observacion,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8604 // Posible argumento de referencia nulo
            return con.Any() ? Ok(con) : NotFound();
        }

        // Consulta que devolver de manera agrupada los proveedores a los cuales se les debe pagar
        [HttpGet("getProveedoresFacturas_Pagar")]
        public ActionResult GetProveedoresFActuras_Pagar()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from fac in _context.Set<Facturas_Invergoal_Inversuez>()
                      where fac.Estado_Factura == 2 &&
                            fac.Cuenta == "220505"
                      group fac by new
                      {
                          fac.Nit_Proveedor,
                          fac.Proveedor.Prov_Nombre,
                          fac.Cuenta,
                          fac.Nit_Empresa
                      } into fac
                      select new
                      {
                          fac.Key.Nit_Proveedor,
                          fac.Key.Prov_Nombre,
                          ValorTotal = fac.Sum(x => x.Valor_Factura),
                          fac.Key.Cuenta,
                          fac.Key.Nit_Empresa
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return con.Any() ? Ok(con) : NotFound("¡No se encontraron proveedores a los cuales pagar!");

        }

        //Consulta que va a devolver la informacion de las facturas que se deben
        [HttpGet("getFacturas_Pagar")]
        public ActionResult GetFacturas_Pagar()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from fac in _context.Set<Facturas_Invergoal_Inversuez>()
                      where fac.Estado_Factura == 2
                      select new
                      {
                          Id_Proveedor = fac.Nit_Proveedor,
                          Proveedor = fac.Proveedor.Prov_Nombre,
                          Factura = fac.Codigo_Factura,
                          fac.Fecha_Factura,
                          fac.Fecha_Vencimiento,
                          Saldo_Actual = fac.Valor_Factura,
                          Mora = 0,
                          fac.Cuenta,
                          Empresa = fac.Nit_Empresa,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return con.Any() ? Ok(con) : NotFound("¡No se encontraron facturas por pagar!");
        }

        // PUT: api/Facturas_Invergoal_Inversuez/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturas_Invergoal_Inversuez(int id, Facturas_Invergoal_Inversuez facturas_Invergoal_Inversuez)
        {
            if (id != facturas_Invergoal_Inversuez.Id)
            {
                return BadRequest();
            }

            _context.Entry(facturas_Invergoal_Inversuez).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Facturas_Invergoal_InversuezExists(id))
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

        // POST: api/Facturas_Invergoal_Inversuez
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facturas_Invergoal_Inversuez>> PostFacturas_Invergoal_Inversuez(Facturas_Invergoal_Inversuez facturas_Invergoal_Inversuez)
        {
          if (_context.Facturas_Invergoal_Inversuez == null)
          {
              return Problem("Entity set 'dataContext.Facturas_Invergoal_Inversuez'  is null.");
          }
            _context.Facturas_Invergoal_Inversuez.Add(facturas_Invergoal_Inversuez);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacturas_Invergoal_Inversuez", new { id = facturas_Invergoal_Inversuez.Id }, facturas_Invergoal_Inversuez);
        }

        // DELETE: api/Facturas_Invergoal_Inversuez/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturas_Invergoal_Inversuez(int id)
        {
            if (_context.Facturas_Invergoal_Inversuez == null)
            {
                return NotFound();
            }
            var facturas_Invergoal_Inversuez = await _context.Facturas_Invergoal_Inversuez.FindAsync(id);
            if (facturas_Invergoal_Inversuez == null)
            {
                return NotFound();
            }

            _context.Facturas_Invergoal_Inversuez.Remove(facturas_Invergoal_Inversuez);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Facturas_Invergoal_InversuezExists(int id)
        {
            return (_context.Facturas_Invergoal_Inversuez?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
