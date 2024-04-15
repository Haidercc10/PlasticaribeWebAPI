using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class BOPPController : ControllerBase
    {
        private readonly dataContext _context;

        public BOPPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/BOPP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BOPP>>> GetBOPP()
        {
            if (_context.BOPP == null)
            {
                return NotFound();
            }
            return await _context.BOPP.ToListAsync();
        }

        // GET: api/BOPP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BOPP>> GetBOPP(long id)
        {
            if (_context.BOPP == null)
            {
                return NotFound();
            }
            var bOPP = await _context.BOPP.FindAsync(id);

            if (bOPP == null)
            {
                return NotFound();
            }

            return bOPP;
        }

        [HttpGet("GetBoppConExistencias")]
        public ActionResult GetBoppConExistencias()
        {
            var bopp = from b in _context.Set<BOPP>()
                       where b.BOPP_Stock > 3
                       select b;
            return Ok(bopp);
        }

        /** Obtener BOPP consultado por serial */
        [HttpGet("serial/{BOPP_Serial}")]
        public ActionResult<BOPP> GetSerial(long BOPP_Serial)
        {
            var bOPP = _context.BOPP.Where(bopp => bopp.BOPP_Serial == BOPP_Serial).ToList();

            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }

        /* Get para contar la cantidad de unidades, precio total segun existencias 
        y cantidad en Kilos agrupados BOPP por Nombre */
        [HttpGet("getBoppStockInventario")]
        public ActionResult<BOPP> getBoppStockInventario()
        {

            /* Consulta la tabla de BOPP Agrupa por descripción */
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var bOPP = _context.BOPP.Where(x => x.BOPP_Stock > 0)
                                    .GroupBy(x => new { x.CatMP_Id, x.CatMP.CatMP_Nombre })
                                    .Select(bopp => new
                                    {
                                        bopp.Key.CatMP_Id,
                                        bopp.Key.CatMP_Nombre,
                                        sumaPrecio = bopp.Sum(x => x.BOPP_Precio),
                                        sumaKilosActual = bopp.Sum(x => x.BOPP_Stock),
                                        conteoDescripcion = bopp.Count()
                                    }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }


        [HttpGet("getCantRollosIngresados_Mes/{fecha1}/{fecha2}")]
        public ActionResult getCantRollosIngresados_Mes(DateTime fecha1, DateTime fecha2)
        {
            var con = from r in _context.Set<BOPP>()
                      where r.BOPP_FechaIngreso >= fecha1 && r.BOPP_FechaIngreso <= fecha2
                      group r by new
                      {
                          r.BOPP_Id,
                      }
                      into r
                      select new
                      {
                          cantidad = r.Count(),
                      };
            return Ok(con);

        }


        [HttpGet("getCantRollosUtilizados_Mes/{fecha1}/{fecha2}")]
        public ActionResult getCantRollosUtilizados_Mes(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from r in _context.Set<Asignacion_BOPP>()
                      from r2 in _context.Set<DetalleAsignacion_BOPP>()
                      where r.AsigBOPP_FechaEntrega >= fecha1 && r.AsigBOPP_FechaEntrega <= fecha2
                            && r.AsigBOPP_Id == r2.AsigBOPP_Id
                      group r by new
                      {
                          r2.BOPP.BOPP_Id,
                      }
                      into r
                      select new
                      {
                          cantidad = r.Count(),
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta que traerá las categorias de materia prima de la tabla BOPP
        [HttpGet("getCategoriasBOPP")]
        public ActionResult GetCategoriasBOPP()
        {
            var con = from bp in _context.Set<BOPP>()
                      group bp by new
                      {
                          bp.CatMP_Id
                      } into bp
                      select bp.Key.CatMP_Id;
            return Ok(con);
        }

        [HttpGet("getDescripcion")]
        public ActionResult GetNombresRepetitivos()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var con = _context.BOPP.GroupBy(a => new { a.BOPP_Descripcion, a.BOPP_CantidadMicras })
                                   .Where(b => b.Count() > 10)
                                   .Select(b => new { 
                                       Nombre = b.Key.BOPP_Descripcion, 
                                       Micras = b.Key.BOPP_CantidadMicras, 
                                   }).Distinct().ToList(); 
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL. 
            return Ok(con);
        }

        [HttpGet("getUltimoPrecioBopp")]
        public ActionResult GetUltimoPrecioBopp()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = (from b in _context.Set<BOPP>() orderby b.BOPP_Id descending select b.BOPP_Precio).FirstOrDefault();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("getSeriales")]
        public ActionResult GetSerialesRepetitivos()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.BOPP.GroupBy(a => Convert.ToString(a.BOPP_Serial).Substring(0, 5))
                                   .Where(b => b.Count() > 10)
                                   .Select(b => new { 
                                       Serial = b.Key.Substring(0, 5)    
                                   }).Distinct().ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("getInventarioBoppsGenericos")]
        public ActionResult GetInventarioBoppsGenericos()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from b in _context.Set<BOPP>()
                      from bg in _context.Set<Bopp_Generico>()
                      from cat in _context.Set<Categoria_MatPrima>()
                      where b.BoppGen_Id == bg.BoppGen_Id &&
                      b.BOPP_Stock > 0 &&
                      b.CatMP_Id == cat.CatMP_Id
                      group b by new { b.BoppGen_Id, bg.BoppGen_Nombre, bg.BoppGen_Micra, bg.BoppGen_Ancho, cat.CatMP_Id, cat.CatMP_Nombre, b.UndMed_Kg, b.BOPP_Precio }
                      into b
                      select new
                      {
                          Id = b.Key.BoppGen_Id,
                          Nombre = b.Key.BoppGen_Nombre,
                          Micras = b.Key.BoppGen_Micra,
                          Ancho = b.Key.BoppGen_Ancho,
                          IdCategoria = b.Key.CatMP_Id,
                          NombreCategoria = b.Key.CatMP_Nombre,
                          Rollos = b.Count(),
                          Medida = b.Key.UndMed_Kg,
                          Stock = b.Sum(xx => xx.BOPP_Stock),
                          Precio = b.Key.BOPP_Precio,
                      };

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (con != null) return Ok(con);
            else return BadRequest("No se encontrar Biorientados asociados a Bopps genéricos");
        }

        [HttpGet("getInventarioBopps/{fecha1}/{fecha2}/{id}")]
        public ActionResult GetInventarioBopps(DateTime fecha1, DateTime fecha2, long id)
        {

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var AsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega >= fecha1
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega <= fecha2).Sum(asg => asg.DtAsigBOPP_Cantidad);

            //Entrada de BOPP
            var EntradaBOPP = _context.BOPP
                .Where(x => x.BOPP_FechaIngreso >= fecha1
                       && x.BOPP_FechaIngreso <= fecha2
                       && x.BOPP_Serial == id).Sum(x => x.BOPP_CantidadInicialKg);

            var con = from b in _context.Set<BOPP>()
                      where b.BoppGen_Id == id
                      && (b.BOPP_Stock > 0 || EntradaBOPP > 0)
                      select new
                      {
                          Id = b.BOPP_Id,
                          Serial = b.BOPP_Serial,
                          Nombre = b.BOPP_Nombre,
                          Ancho = b.BOPP_Ancho,
                          Micras = b.BOPP_CantidadMicras,
                          Inicial = b.BOPP_CantidadInicialKg,
                          Entrada = EntradaBOPP,
                          Salida = AsgBopp,
                          Stock = b.BOPP_Stock,
                          Diferencia = (b.BOPP_Stock - b.BOPP_CantidadInicialKg),
                          Medida = b.UndMed_Id,
                          Precio = b.BOPP_Precio,
                          Subtotal = (b.BOPP_Stock * b.BOPP_Precio),
                          CategoriaId = b.CatMP_Id,
                          Categoria = b.CatMP.CatMP_Nombre,
                      };

            if (con != null) return Ok(con);
            else return BadRequest("No se encontraron BOPPs asociados");
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        [HttpGet("getEntryBOPP/{fecha1}/{fecha2}/{hora}")]
        public ActionResult getEntryBOPP(DateTime fecha1, DateTime fecha2, string hora)
        {
            //Entrada de BOPP
            var conEntradaBOPP = from ent in _context.Set<BOPP>()
                                 join bg in _context.Set<Bopp_Generico>() on ent.BoppGen_Id equals bg.BoppGen_Id
                                 join p in _context.Set<Proveedor>() on ent.Prov_Id equals p.Prov_Id
                                 where ent.BOPP_FechaIngreso >= fecha1
                                       && ent.BOPP_FechaIngreso <= fecha2
                                       && ent.BOPP_Hora == hora
                                 select new
                                 {
                                     Id = Convert.ToInt64(ent.BOPP_Id),
                                     Codigo = Convert.ToString(ent.BOPP_CodigoDoc) == null ? Convert.ToString("") : Convert.ToString(ent.BOPP_CodigoDoc),
                                     TipoDoc = Convert.ToString(ent.BOPP_TipoDoc) == null ? Convert.ToString("") : (from tdoc in _context.Set<Tipo_Documento>() where tdoc.TpDoc_Id == Convert.ToString(ent.BOPP_TipoDoc) select tdoc.TpDoc_Nombre).FirstOrDefault(),
                                     Movimiento = Convert.ToString("ENTBIO"),
                                     Tipo_Movimiento = Convert.ToString("Entrada de Biorientado"),
                                     Fecha = ent.BOPP_FechaIngreso,
                                     Hora = ent.BOPP_Hora,
                                     Usuario = ent.Usua.Usua_Nombre,
                                     Bopp_Id = Convert.ToInt64(ent.BOPP_Serial),
                                     Bopp = Convert.ToString(ent.BOPP_Nombre),
                                     BoppGenerico_Id = Convert.ToInt32(bg.BoppGen_Id),
                                     BoppGenerico = Convert.ToString(bg.BoppGen_Nombre),
                                     Descripcion = Convert.ToString(ent.BOPP_Descripcion),
                                     Cantidad = Convert.ToDecimal(ent.BOPP_Stock),
                                     Unidad_Medida = ent.UndMed_Kg,
                                     CantidadInicial = Convert.ToDecimal(ent.BOPP_CantidadInicialKg),
                                     Ancho = Convert.ToDecimal(ent.BOPP_Ancho),
                                     Micras = Convert.ToDecimal(ent.BOPP_CantidadMicras),
                                     Serial = Convert.ToString(ent.BOPP_Serial),
                                     Precio = Convert.ToDecimal(ent.BOPP_Precio),
                                     Id_Proveedor = Convert.ToString(ent.Prov_Id),
                                     Proveedor = Convert.ToString(p.Prov_Nombre),
                                     Telefono_Proveedor = Convert.ToString(p.Prov_Telefono) == null ? Convert.ToString("") : Convert.ToString(p.Prov_Telefono),
                                 };

            return Ok(conEntradaBOPP);
        }

        // PUT: api/BOPP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBOPP(long id, BOPP bOPP)
        {
            if (id != bOPP.BOPP_Id)
            {
                return BadRequest();
            }

            _context.Entry(bOPP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOPPExists(id))
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

        [HttpPut("putInventarioBiorientado/{id}/{cantidad}")]
        public async Task<IActionResult> PutInventarioBiorientado(long id, decimal cantidad)
        {
            var bopp = (from b in _context.Set<BOPP>() where b.BOPP_Id == id select b).FirstOrDefault();
            if (bopp == null) return BadRequest();

            bopp.BOPP_Stock = cantidad;
            _context.Entry(bopp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOPPExists(id)) return NotFound();
                else throw;
            }

            return Ok(bopp);
        }

        // POST: api/BOPP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BOPP>> PostBOPP(BOPP bOPP)
        {
            if (_context.BOPP == null)
            {
                return Problem("Entity set 'dataContext.BOPP'  is null.");
            }
            _context.BOPP.Add(bOPP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBOPP", new { id = bOPP.BOPP_Id }, bOPP);
        }

        // DELETE: api/BOPP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBOPP(long id)
        {
            if (_context.BOPP == null)
            {
                return NotFound();
            }
            var bOPP = await _context.BOPP.FindAsync(id);
            if (bOPP == null)
            {
                return NotFound();
            }

            _context.BOPP.Remove(bOPP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BOPPExists(long id)
        {
            return (_context.BOPP?.Any(e => e.BOPP_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
