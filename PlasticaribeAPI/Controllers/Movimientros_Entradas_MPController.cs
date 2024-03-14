using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or members
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Movimientros_Entradas_MPController : ControllerBase
    {
        private readonly dataContext _context;

        public Movimientros_Entradas_MPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Movimientros_Entradas_MP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimientros_Entradas_MP>>> GetMovimientros_Entradas_MP()
        {
            if (_context.Movimientros_Entradas_MP == null)
            {
                return NotFound();
            }
            return await _context.Movimientros_Entradas_MP.ToListAsync();
        }

        // GET: api/Movimientros_Entradas_MP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimientros_Entradas_MP>> GetMovimientros_Entradas_MP(int id)
        {
            if (_context.Movimientros_Entradas_MP == null)
            {
                return NotFound();
            }
            var movimientros_Entradas_MP = await _context.Movimientros_Entradas_MP.FindAsync(id);

            if (movimientros_Entradas_MP == null)
            {
                return NotFound();
            }

            return movimientros_Entradas_MP;
        }

        // Consulta que devolverá la información de las compras realizadas de una materia prima en un lapso de tiempo
        [HttpGet("getComprasRealizadas/{fechaInicio}/{fechaFin}/{material}")]
        public ActionResult GetComprasRealizadas(DateTime fechaInicio, DateTime fechaFin, long material)
        {
            var compras = from c in _context.Set<Movimientros_Entradas_MP>()
                          where c.Fecha_Entrada >= fechaInicio &&
                                c.Fecha_Entrada <= fechaFin &&
                                (c.MatPri_Id == material || c.Tinta_Id == material || c.Bopp_Id == material)
                          select new
                          {
                              FechaCompra = c.Fecha_Entrada,
                              HoraCompra = c.Hora_Entrada,
                              CantidadCompra = c.Cantidad_Entrada,
                              PrecioReal = c.Precio_RealUnitario,
                              PrecioEstandar = c.Precio_EstandarUnitario,
                              DiferenciaPrecio = (c.Precio_EstandarUnitario - c.Precio_RealUnitario),
                              CostoReal = (c.Cantidad_Entrada * c.Precio_RealUnitario),
                              CostoEstandar = (c.Cantidad_Entrada * c.Precio_EstandarUnitario),
                              VariacionPrecio = (c.Cantidad_Entrada * c.Precio_EstandarUnitario) - (c.Cantidad_Entrada * c.Precio_RealUnitario)
                          };
            return compras.Any() ? Ok(compras) : NotFound();
        }

        // Consulta que devolverá la información de las cantidades disponibles que hay de compras realizados antes de un lapso de tiempo
        [HttpGet("getComprasAntiguas/{fecha}/{material}")]
        public ActionResult GetComprasAntiguas(DateTime fecha, long material)
        {
            var compras = from c in _context.Set<Movimientros_Entradas_MP>()
                          where c.Fecha_Entrada < fecha &&
                                (c.MatPri_Id == material || c.Tinta_Id == material || c.Bopp_Id == material) &&
                                c.Cantidad_Disponible > 0
                          select new
                          {
                              FechaCompra = c.Fecha_Entrada,
                              CantidadCompra = c.Cantidad_Disponible,
                              PrecioReal = c.Precio_RealUnitario,
                              CostoReal = (c.Cantidad_Disponible * c.Precio_RealUnitario)
                          };
            return compras.Any() ? Ok(compras) : NotFound();
        }

        // Materias primas, Tintas, Biorientados
        [HttpGet("getInventarioMateriales")]
        public ActionResult GetInventarioMateriales()
        {
            long[] noMp = { 84, 88 };
            long[] noBopp = { 1 };
            long[] noTinta = { 2001, 2072 };

            var materiasPrimas = from mp in _context.Set<Materia_Prima>()
                                 where !noMp.Contains(mp.MatPri_Id)
                                 select new
                                 {
                                     Id_Materia_Prima = mp.MatPri_Id,
                                     Nombre_Materia_Prima = mp.MatPri_Nombre,
                                 };

            var bopp = from bp in _context.Set<Bopp_Generico>()
                       where !noBopp.Contains(bp.BoppGen_Id)
                       select new
                       {
                           Id_Materia_Prima = bp.BoppGen_Id,
                           Nombre_Materia_Prima = bp.BoppGen_Nombre,
                       };

            var tintas = from t in _context.Set<Tinta>()
                         where !noTinta.Contains(t.Tinta_Id)
                         select new
                         {
                             Id_Materia_Prima = t.Tinta_Id,
                             Nombre_Materia_Prima = t.Tinta_Nombre,
                         };

            return Ok(materiasPrimas.Concat(bopp).Concat(tintas));
        }

        // Cargar inventario de la materia prima asignar de manera detallada
        [HttpGet("getInventarioxMaterial/{material}")]
        public ActionResult GetInventarioxMaterial(long material)
        {
            var materiasPrimas = from mp in _context.Set<Movimientros_Entradas_MP>()
                                 where (mp.MatPri_Id == material || mp.Tinta_Id == material || (mp.Codigo_Entrada == material && mp.Bopp_Id != 1))
                                 && mp.Cantidad_Disponible > 0
                                 && mp.Estado_Id == 19
                                 orderby mp.Id ascending
                                 select new
                                 {
                                     Id = mp.Id,
                                     MatPri_Id = mp.MatPri_Id,
                                     Tinta_Id = mp.Tinta_Id,
                                     Bopp_Id = mp.Bopp_Id,
                                     Cantidad_Entrada = mp.Cantidad_Entrada,
                                     UndMed_Id = mp.UndMed_Id,
                                     Precio_RealUnitario = mp.Precio_RealUnitario,
                                     Tipo_Entrada = mp.Tipo_Entrada,
                                     Codigo_Entrada = mp.Codigo_Entrada,
                                     Estado_Id = mp.Estado_Id,
                                     Cantidad_Asignada = mp.Cantidad_Asignada,
                                     Cantidad_Disponible = mp.Cantidad_Disponible,
                                     Observacion = mp.Observacion,
                                     Fecha_Entrada = Convert.ToString(mp.Fecha_Entrada).Substring(0, 10),
                                     Hora_Entrada = mp.Hora_Entrada,
                                     Precio_EstandarUnitario = mp.Precio_EstandarUnitario
                                 };

            if (materiasPrimas == null) return BadRequest("No se encontró el material seleccionado!");
            return Ok(materiasPrimas);
        }

        //
        [HttpGet("getEntradasMP/{ot}/{mp}/{tinta}/{bopp}")]
        public ActionResult getEntradasMP(long ot, long mp, long tinta, long bopp)
        {
            var salidas = (from e in _context.Set<Entradas_Salidas_MP>()
                           where e.Orden_Trabajo == ot &&
                           e.MatPri_Id == mp &&
                           e.Tinta_Id == tinta &&
                           e.Bopp_Id == bopp
                           orderby e.Id_Entrada descending
                           select e.Id_Entrada).ToList();

            var entradas = from me in _context.Set<Movimientros_Entradas_MP>()
                           orderby me.Id descending
                           where salidas.Contains(me.Id) &&
                           me.Cantidad_Asignada > 0
                           select me;

            if (entradas == null) return BadRequest("No se encontraron entradas de material");
            else return Ok(entradas);

        }

        /* Obtendrá los movimientos de entrada asociados a la salida de material que se está consultando
        y luego le adicionará la cantidad de salida a la cantidad disponible y restará la cantidad de salida 
        a la cantidad asignada.*/
        [HttpGet("getEntradasxMaquilas/{om}/{mp}/{tinta}/{bopp}")]
        public ActionResult getEntradasxMaquilas(long om, long mp, long tinta, long bopp)
        {
            var entradas_salidas = from s in _context.Set<Entradas_Salidas_MP>()
                                   from me in _context.Set<Movimientros_Entradas_MP>()
                                   where s.MatPri_Id == mp &&
                                         s.Tinta_Id == tinta &&
                                         s.Bopp_Id == bopp &&
                                         s.Tipo_Salida == "OM" &&
                                         s.Codigo_Salida == om &&
                                         s.Codigo_Entrada == me.Codigo_Entrada &&
                                         s.Id_Entrada == me.Id &&
                                         s.Tipo_Entrada == me.Tipo_Entrada &&
                                         me.MatPri_Id == s.MatPri_Id &&
                                         me.Tinta_Id == s.Tinta_Id &&
                                         me.Bopp_Id == s.Bopp_Id
                                   select new
                                   {
                                       Id = me.Id,
                                       MatPri_Id = me.MatPri_Id,
                                       Tinta_Id = me.Tinta_Id,
                                       Bopp_Id = me.Bopp_Id,
                                       Cantidad_Entrada = me.Cantidad_Entrada,
                                       UndMed_Id = me.UndMed_Id,
                                       Precio_RealUnitario = me.Precio_RealUnitario,
                                       Tipo_Entrada = me.Tipo_Entrada,
                                       Codigo_Entrada = me.Codigo_Entrada,
                                       Estado_Id = 19,
                                       Cantidad_Asignada = me.Cantidad_Asignada - s.Cantidad_Salida,
                                       Cantidad_Disponible = me.Cantidad_Disponible + s.Cantidad_Salida, 
                                       Observacion = me.Observacion,
                                       Fecha_Entrada = me.Fecha_Entrada,
                                       Hora_Entrada = me.Hora_Entrada,
                                       Precio_EstandarUnitario = me.Precio_EstandarUnitario
                                   };

            if (entradas_salidas == null) return BadRequest("No se encontraron entradas de material");
            else return Ok(entradas_salidas);
        }


        // PUT: api/Movimientros_Entradas_MP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientros_Entradas_MP(int id, Movimientros_Entradas_MP movimientros_Entradas_MP)
        {
            if (id != movimientros_Entradas_MP.Id)
            {
                return BadRequest();
            }

            _context.Entry(movimientros_Entradas_MP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Movimientros_Entradas_MPExists(id))
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

        // POST: api/Movimientros_Entradas_MP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimientros_Entradas_MP>> PostMovimientros_Entradas_MP(Movimientros_Entradas_MP movimientros_Entradas_MP)
        {
            if (_context.Movimientros_Entradas_MP == null)
            {
                return Problem("Entity set 'dataContext.Movimientros_Entradas_MP'  is null.");
            }
            _context.Movimientros_Entradas_MP.Add(movimientros_Entradas_MP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimientros_Entradas_MP", new { id = movimientros_Entradas_MP.Id }, movimientros_Entradas_MP);
        }

        // DELETE: api/Movimientros_Entradas_MP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientros_Entradas_MP(int id)
        {
            if (_context.Movimientros_Entradas_MP == null)
            {
                return NotFound();
            }
            var movimientros_Entradas_MP = await _context.Movimientros_Entradas_MP.FindAsync(id);
            if (movimientros_Entradas_MP == null)
            {
                return NotFound();
            }

            _context.Movimientros_Entradas_MP.Remove(movimientros_Entradas_MP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Movimientros_Entradas_MPExists(int id)
        {
            return (_context.Movimientros_Entradas_MP?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or members
}
