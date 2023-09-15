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
    public class Entradas_Salidas_MPController : ControllerBase
    {
        private readonly dataContext _context;

        public Entradas_Salidas_MPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Entradas_Salidas_MP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entradas_Salidas_MP>>> GetEntradas_Salidas_MP()
        {
          if (_context.Entradas_Salidas_MP == null)
          {
              return NotFound();
          }
            return await _context.Entradas_Salidas_MP.ToListAsync();
        }

        // GET: api/Entradas_Salidas_MP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entradas_Salidas_MP>> GetEntradas_Salidas_MP(int id)
        {
          if (_context.Entradas_Salidas_MP == null)
          {
              return NotFound();
          }
            var entradas_Salidas_MP = await _context.Entradas_Salidas_MP.FindAsync(id);

            if (entradas_Salidas_MP == null)
            {
                return NotFound();
            }

            return entradas_Salidas_MP;
        }

        // Consulta que devolverá la información de las salidas realizadas de una materia prima en un lapso de tiempo
        [HttpGet("getSalidasRealizadas/{fechaInicio}/{fechaFin}/{material}/{producto}")]
        public ActionResult GetSalidasRealizadas(DateTime fechaInicio, DateTime fechaFin, long material, long producto)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var compras = from c in _context.Set<Entradas_Salidas_MP>()
                          join r in _context.Set<Productos_MateriasPrimas>() on c.Prod_Id equals r.Prod_Id
                          where c.Fecha_Registro >= fechaInicio &&
                                c.Fecha_Registro <= fechaFin &&
                                (c.MatPri_Id == material || c.Tinta_Id == material || c.Bopp_Id == material) &&
                                r.MatPri_Id == c.MatPri_Id &&
                                r.Tinta_Id == c.Tinta_Id &&
                                r.Bopp_Id == c.Bopp_Id &&
                                r.Prod_Id == producto &&
                                r.UndMed_Id == c.UndMed_Id
                          select new
                          {
                              Fecha = c.Fecha_Registro,
                              Hora = c.Hora_Registro,
                              Orden = c.Orden_Trabajo,
                              CantidadEstandar = r.Cantidad_Minima,
                              CantidadTotalEstandar = r.Cantidad_Minima * c.Cant_PedidaOT,
                              CantidadSalida = c.Cantidad_Salida,
                              PrecioReal = c.Movimientros.Precio_RealUnitario,
                              PrecioEstandar = c.Movimientros.Precio_EstandarUnitario,
                              DiferenciaPrecio = (c.Movimientros.Precio_EstandarUnitario - c.Movimientros.Precio_RealUnitario),
                              CostoReal = (c.Cantidad_Salida * c.Movimientros.Precio_RealUnitario),
                              CostoEstandar = ((r.Cantidad_Minima * c.Cant_PedidaOT) * c.Movimientros.Precio_EstandarUnitario),
                              VariacionPrecio = (c.Cantidad_Salida * c.Movimientros.Precio_EstandarUnitario) - (c.Cantidad_Salida * c.Movimientros.Precio_RealUnitario)
                          };
            return compras.Any() ? Ok(compras) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Consulta que devolverá la información de las salidas de material
        [HttpGet("getSalidasMaterial/{fecha}/{material}")]
        public ActionResult GetSalidasMaterial(DateTime fecha, long material)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var salidas = from s in _context.Set<Entradas_Salidas_MP>()
                          where s.Fecha_Registro <= fecha &&
                                (s.MatPri_Id == material || s.Tinta_Id == material || s.Bopp_Id == material)
                          select new
                          {
                              CantidadSalida = s.Cantidad_Salida,
                              PrecioReal = s.Movimientros.Precio_RealUnitario,
                              CostoReal = (s.Cantidad_Salida * s.Movimientros.Precio_RealUnitario)
                          };

            return salidas.Any() ? Ok(salidas) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // GET: Obtener registros de salidas
        [HttpGet("getConsumos/{fecha1}/{fecha2}/{material}/{item}")]
        public ActionResult GetConsumos(DateTime fecha1, DateTime fecha2, long material, int item)
        {

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var consumos = from s in _context.Set<Entradas_Salidas_MP>()
                           from me in _context.Set<Movimientros_Entradas_MP>()
                           where s.Fecha_Registro >= fecha1 &&
                           s.Fecha_Registro <= fecha2 &&
                           (s.MatPri_Id == material || s.Tinta_Id == material || s.Bopp_Id == material) &&
                           s.Codigo_Entrada == me.Codigo_Entrada &&
                           s.Id_Entrada == me.Id &&
                           s.Tipo_Entrada == me.Tipo_Entrada &&
                           me.MatPri_Id == s.MatPri_Id &&
                           s.Prod_Id == item
                           select new
                           {
                               Fecha = s.Fecha_Registro,
                               Hora = s.Hora_Registro,
                               OT = s.Orden_Trabajo,
                               Item = s.Prod_Id, 
                               Referencia = s.Producto.Prod_Nombre,
                               Cantidad_PedidaOT = s.Cant_PedidaOT, 
                               Medida = s.UndMed_Id,
                               MatPrimaId = s.MatPri_Id,
                               TintaId = s.Tinta_Id,
                               BoppId = s.Bopp_Id,
                               MaterialRealId = (s.MatPri_Id != 84 && s.Tinta_Id == 2001 && s.Bopp_Id == 1 ? s.MatPri_Id :
                                                 s.MatPri_Id == 84 && s.Tinta_Id != 2001 && s.Bopp_Id == 1 ? s.Tinta_Id :
                                                 s.MatPri_Id == 84 && s.Tinta_Id == 2001 && s.Bopp_Id != 1 ? s.Bopp_Id : 1),
                               NombreMaterial = (s.MatPri_Id != 84 && s.Tinta_Id == 2001 && s.Bopp_Id == 1 ? s.Materia_Prima.MatPri_Nombre :
                                                 s.MatPri_Id == 84 && s.Tinta_Id != 2001 && s.Bopp_Id == 1 ? s.Tinta.Tinta_Nombre :
                                                 s.MatPri_Id == 84 && s.Tinta_Id == 2001 && s.Bopp_Id != 1 ? s.Bopp.BoppGen_Nombre : ""),
                               Cantidad_Requerida = s.Cantidad_Salida,
                               Cantidad_Estandar = 0,
                               Diferencial_Cantidad = (s.Cantidad_Salida - 0),
                               Precio_Real = me.Precio_RealUnitario,
                               ValoracionDCxPR = (me.Precio_RealUnitario * (s.Cantidad_Salida - 0)),
                               Costo_Real = (me.Precio_RealUnitario * s.Cantidad_Salida),
                               Costo_Estandar = (me.Precio_EstandarUnitario),
                               Valoracion_Cantidad = ((me.Precio_RealUnitario * s.Cantidad_Salida) - (me.Precio_RealUnitario * 0)),
                           };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (consumos == null) return BadRequest("No se encontraron movimientos del material en las fechas consultadas!");
            return Ok(consumos);
        }

        // PUT: api/Entradas_Salidas_MP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntradas_Salidas_MP(int id, Entradas_Salidas_MP entradas_Salidas_MP)
        {
            if (id != entradas_Salidas_MP.Id)
            {
                return BadRequest();
            }

            _context.Entry(entradas_Salidas_MP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Entradas_Salidas_MPExists(id))
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

        // POST: api/Entradas_Salidas_MP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entradas_Salidas_MP>> PostEntradas_Salidas_MP(Entradas_Salidas_MP entradas_Salidas_MP)
        {
          if (_context.Entradas_Salidas_MP == null)
          {
              return Problem("Entity set 'dataContext.Entradas_Salidas_MP'  is null.");
          }
            _context.Entradas_Salidas_MP.Add(entradas_Salidas_MP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntradas_Salidas_MP", new { id = entradas_Salidas_MP.Id }, entradas_Salidas_MP);
        }

        // DELETE: api/Entradas_Salidas_MP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntradas_Salidas_MP(int id)
        {
            if (_context.Entradas_Salidas_MP == null)
            {
                return NotFound();
            }
            var entradas_Salidas_MP = await _context.Entradas_Salidas_MP.FindAsync(id);
            if (entradas_Salidas_MP == null)
            {
                return NotFound();
            }

            _context.Entradas_Salidas_MP.Remove(entradas_Salidas_MP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Entradas_Salidas_MPExists(int id)
        {
            return (_context.Entradas_Salidas_MP?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
