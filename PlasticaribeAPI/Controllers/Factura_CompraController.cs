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
    public class Factura_CompraController : ControllerBase
    {
        private readonly dataContext _context;

        public Factura_CompraController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Factura_Compra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura_Compra>>> GetFacturas_Compras()
        {
          if (_context.Facturas_Compras == null)
          {
              return NotFound();
          }
            return await _context.Facturas_Compras.ToListAsync();
        }

        // GET: api/Factura_Compra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura_Compra>> GetFactura_Compra(long id)
        {
          if (_context.Facturas_Compras == null)
          {
              return NotFound();
          }
            var factura_Compra = await _context.Facturas_Compras.FindAsync(id);

            if (factura_Compra == null)
            {
                return NotFound();
            }

            return factura_Compra;
        }

        //Consulta por el valor total de la factura
        [HttpGet("api/Factura_Compra/{Facco_ValorTotal}")]
        public ActionResult Nombre(long Facco_ValorTotal)
        {
            var factCompra = _context.Facturas_Compras.Where(f => f.Facco_ValorTotal >= Facco_ValorTotal).ToList();

            return Ok(factCompra);
        }

        //Consulta por el usuario que regitró la factura
        [HttpGet("F/{Usua_Id}")]
        public ActionResult Usuario(long Usua_Id)
        {
            var factCompra = _context.Facturas_Compras.Where(f => f.Usua_Id == Usua_Id).ToList();

            return Ok(factCompra);
        }

        //Consulta por el proveedor de la factura
        [HttpGet("P/{Prov_Id}")]
        public ActionResult Proveedor(long Prov_Id)
        {
            var factCompra = _context.Facturas_Compras.Where(f => f.Prov_Id == Prov_Id).ToList();

            return Ok(factCompra);
        }

        //Conslta por fecha 
        [HttpGet("fecha/{Facco_FechaFactura}")]
        public ActionResult Fecha(DateTime Facco_FechaFactura)
        {
            var factCompra = _context.Facturas_Compras.Where(f => f.Facco_FechaFactura == Facco_FechaFactura).ToList();

            return Ok(factCompra);
        }

        [HttpGet("fechas/")]
        public ActionResult Fechas(DateTime Facco_FechaFactura1, DateTime Facco_FechaFactura2)
        {
            var factCompra = _context.Facturas_Compras.Where(f => f.Facco_FechaFactura >= Facco_FechaFactura1 && f.Facco_FechaFactura <= Facco_FechaFactura2).ToList();

            return Ok(factCompra);
        }

        [HttpGet("codigo/{Facco_Codigo}")]
        public ActionResult codigo(string Facco_Codigo)
        {
            var factCompra = _context.Facturas_Compras.Where(f => f.Facco_Codigo == Facco_Codigo).ToList();

            return Ok(factCompra);
        }

        // PUT: api/Factura_Compra/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura_Compra(long id, Factura_Compra factura_Compra)
        {
            if (id != factura_Compra.Facco_Id)
            {
                return BadRequest();
            }

            _context.Entry(factura_Compra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Factura_CompraExists(id))
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

        // POST: api/Factura_Compra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Factura_Compra>> PostFactura_Compra(Factura_Compra factura_Compra)
        {
          if (_context.Facturas_Compras == null)
          {
              return Problem("Entity set 'dataContext.Facturas_Compras'  is null.");
          }
            _context.Facturas_Compras.Add(factura_Compra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactura_Compra", new { id = factura_Compra.Facco_Id }, factura_Compra);
        }

        // DELETE: api/Factura_Compra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura_Compra(long id)
        {
            if (_context.Facturas_Compras == null)
            {
                return NotFound();
            }
            var factura_Compra = await _context.Facturas_Compras.FindAsync(id);
            if (factura_Compra == null)
            {
                return NotFound();
            }

            _context.Facturas_Compras.Remove(factura_Compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Factura_CompraExists(long id)
        {
            return (_context.Facturas_Compras?.Any(e => e.Facco_Id == id)).GetValueOrDefault();
        }
    }
}
