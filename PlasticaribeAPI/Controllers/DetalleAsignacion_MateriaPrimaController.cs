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
    public class DetalleAsignacion_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_MateriaPrima>>> GetDetallesAsignaciones_MateriasPrimas()
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }
            return await _context.DetallesAsignaciones_MateriasPrimas.ToListAsync();
        }

        // GET: api/DetalleAsignacion_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_MateriaPrima>> GetDetalleAsignacion_MateriaPrima(long id)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_MateriaPrima = await _context.DetallesAsignaciones_MateriasPrimas.FindAsync(id);

            if (detalleAsignacion_MateriaPrima == null)
            {
                return NotFound();
            }

            return detalleAsignacion_MateriaPrima;
        }

        [HttpGet("asignacion/{AsigMp_Id}")]
        public ActionResult<DetalleAsignacion_MateriaPrima> GetIdAsignacion(long AsigMp_Id)
        {
            var detallesAsignacion = _context.DetallesAsignaciones_MateriasPrimas.Where(dtA => dtA.AsigMp_Id == AsigMp_Id).ToList();

            if (detallesAsignacion == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(detallesAsignacion);
            }
        }

        [HttpGet("AsignacionesTotales/{ot}")]
        public ActionResult GetAsignacionesTotales(long ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var conMp = (from Asgmp in _context.Set<DetalleAsignacion_MateriaPrima>()
                         where Asgmp.AsigMp.AsigMP_OrdenTrabajo == ot
                         group Asgmp by new
                         {
                             Asgmp.MatPri_Id,
                             Asgmp.MatPri.MatPri_Nombre,
                             Asgmp.UndMed_Id,
                             Asgmp.MatPri.MatPri_Precio,
                             Asgmp.Proceso_Id,
                             Asgmp.Proceso.Proceso_Nombre
                         } into y
                         select new
                         {
                             //Materia Prima
                             MatPri_Id = Convert.ToInt16(y.Key.MatPri_Id),
                             Tinta_Id = Convert.ToInt16(2001),
                             Bopp_Id = Convert.ToInt16(449),
                             MateriaPrima = y.Key.MatPri_Id,
                             NombreMP = y.Key.MatPri_Nombre,
                             CantMP = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad),
                             UndMedida = y.Key.UndMed_Id,
                             Precio = y.Key.MatPri_Precio,
                             SubTotal = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad) * y.Key.MatPri_Precio,
                             Proceso = y.Key.Proceso_Id,
                             NombreProceso = y.Key.Proceso_Nombre
                         });

            var conTinta = (from AsgTinta in _context.Set<DetalleAsignacion_Tinta>()
                            where AsgTinta.AsigMp.AsigMP_OrdenTrabajo == ot
                            group AsgTinta by new
                            {
                                AsgTinta.Tinta_Id,
                                AsgTinta.Tinta.Tinta_Nombre,
                                AsgTinta.UndMed_Id,
                                AsgTinta.Tinta.Tinta_Precio,
                                AsgTinta.Proceso_Id,
                                AsgTinta.Proceso.Proceso_Nombre
                            } into y
                            select new
                            {
                                //Tintas
                                MatPri_Id = Convert.ToInt16(84),
                                Tinta_Id = Convert.ToInt16(y.Key.Tinta_Id),
                                Bopp_Id = Convert.ToInt16(449),
                                MateriaPrima = y.Key.Tinta_Id,
                                NombreMP = y.Key.Tinta_Nombre,
                                CantMP = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad),
                                UndMedida = y.Key.UndMed_Id,
                                Precio = y.Key.Tinta_Precio,
                                SubTotal = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad) * y.Key.Tinta_Precio,
                                Proceso = y.Key.Proceso_Id,
                                NombreProceso = y.Key.Proceso_Nombre
                            });

            var conBopp = (from AsgBopp in _context.Set<DetalleAsignacion_BOPP>()
                           where AsgBopp.DtAsigBOPP_OrdenTrabajo == ot
                           group AsgBopp by new
                           {
                               AsgBopp.BOPP_Id,
                               AsgBopp.BOPP.BOPP_Nombre,
                               AsgBopp.UndMed_Id,
                               AsgBopp.BOPP.BOPP_Precio,
                               AsgBopp.Proceso_Id,
                               AsgBopp.Proceso.Proceso_Nombre
                           } into y
                           select new
                           {
                               //BOPP
                               MatPri_Id = Convert.ToInt16(84),
                               Tinta_Id = Convert.ToInt16(2001),
                               Bopp_Id = Convert.ToInt16(y.Key.BOPP_Id),
                               MateriaPrima = y.Key.BOPP_Id,
                               NombreMP = y.Key.BOPP_Nombre,
                               CantMP = y.Sum(AsgBopp => AsgBopp.DtAsigBOPP_Cantidad),
                               UndMedida = y.Key.UndMed_Id,
                               Precio = y.Key.BOPP_Precio,
                               SubTotal = y.Sum(AsgBopp => AsgBopp.DtAsigBOPP_Cantidad) * y.Key.BOPP_Precio,
                               Proceso = y.Key.Proceso_Id,
                               NombreProceso = y.Key.Proceso_Nombre
                           });
            var con = conMp.Concat(conTinta).Concat(conBopp);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(con);
        }

        //Consulta que devolverá las materias primas asignadas y devueltas a una orden de trabajo
        [HttpGet("getAsginacionDevolucionOrden/{ot}")]
        public ActionResult GetAsginacionDevolucionOrden(long ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var conMp = (from Asgmp in _context.Set<DetalleAsignacion_MateriaPrima>()
                         where Asgmp.AsigMp.AsigMP_OrdenTrabajo == ot
                         group Asgmp by new
                         {
                             Asgmp.MatPri_Id,
                             Asgmp.MatPri.MatPri_Nombre,
                             Asgmp.UndMed_Id,
                             Asgmp.MatPri.MatPri_Precio,
                             Asgmp.Proceso_Id,
                             Asgmp.Proceso.Proceso_Nombre
                         } into y
                         select new
                         {
                             //Materia Prima
                             MatPri_Id = Convert.ToInt16(y.Key.MatPri_Id),
                             Tinta_Id = Convert.ToInt16(2001),
                             Bopp_Id = Convert.ToInt16(449),
                             MateriaPrima = Convert.ToInt16(y.Key.MatPri_Id),
                             NombreMP = Convert.ToString(y.Key.MatPri_Nombre),
                             CantMP = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad),
                             UndMedida = y.Key.UndMed_Id,
                             Precio = Convert.ToDecimal(y.Key.MatPri_Precio),
                             SubTotal = Convert.ToDecimal(y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad) * y.Key.MatPri_Precio),
                             Proceso = y.Key.Proceso_Id,
                             NombreProceso = y.Key.Proceso_Nombre
                         });

            var conTinta = (from AsgTinta in _context.Set<DetalleAsignacion_Tinta>()
                            where AsgTinta.AsigMp.AsigMP_OrdenTrabajo == ot
                            group AsgTinta by new
                            {
                                AsgTinta.Tinta_Id,
                                AsgTinta.Tinta.Tinta_Nombre,
                                AsgTinta.UndMed_Id,
                                AsgTinta.Tinta.Tinta_Precio,
                                AsgTinta.Proceso_Id,
                                AsgTinta.Proceso.Proceso_Nombre
                            } into y
                            select new
                            {
                                //Tintas
                                MatPri_Id = Convert.ToInt16(84),
                                Tinta_Id = Convert.ToInt16(y.Key.Tinta_Id),
                                Bopp_Id = Convert.ToInt16(449),
                                MateriaPrima = Convert.ToInt16(y.Key.Tinta_Id),
                                NombreMP = Convert.ToString(y.Key.Tinta_Nombre),
                                CantMP = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad),
                                UndMedida = y.Key.UndMed_Id,
                                Precio = Convert.ToDecimal(y.Key.Tinta_Precio),
                                SubTotal = Convert.ToDecimal(y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad) * y.Key.Tinta_Precio),
                                Proceso = y.Key.Proceso_Id,
                                NombreProceso = y.Key.Proceso_Nombre
                            });

            var conBopp = (from AsgBopp in _context.Set<DetalleAsignacion_BOPP>()
                           where AsgBopp.DtAsigBOPP_OrdenTrabajo == ot
                           group AsgBopp by new
                           {
                               AsgBopp.BOPP_Id,
                               AsgBopp.BOPP.BOPP_Nombre,
                               AsgBopp.UndMed_Id,
                               AsgBopp.BOPP.BOPP_Precio,
                               AsgBopp.Proceso_Id,
                               AsgBopp.Proceso.Proceso_Nombre
                           } into y
                           select new
                           {
                               //BOPP
                               MatPri_Id = Convert.ToInt16(84),
                               Tinta_Id = Convert.ToInt16(2001),
                               Bopp_Id = Convert.ToInt16(y.Key.BOPP_Id),
                               MateriaPrima = Convert.ToInt16(y.Key.BOPP_Id),
                               NombreMP = Convert.ToString(y.Key.BOPP_Nombre),
                               CantMP = y.Sum(AsgBopp => AsgBopp.DtAsigBOPP_Cantidad),
                               UndMedida = y.Key.UndMed_Id,
                               Precio = Convert.ToDecimal(y.Key.BOPP_Precio),
                               SubTotal = Convert.ToDecimal(y.Sum(AsgBopp => AsgBopp.DtAsigBOPP_Cantidad) * y.Key.BOPP_Precio),
                               Proceso = y.Key.Proceso_Id,
                               NombreProceso = y.Key.Proceso_Nombre
                           });

            var con = conMp.Concat(conTinta).Concat(conBopp);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(con);
        }

        //Consulta que traerá la cantidad de materia prima asignada, teniendo en cuenta la materia prima devuelta
        [HttpGet("getMateriaPrimaAsignada/{ot}")]
        public ActionResult GetMateriaPrimaAsignada(int ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = (from asg in _context.Set<DetalleAsignacion_MateriaPrima>()
                        where asg.AsigMp.AsigMP_OrdenTrabajo == ot
                        select asg.DtAsigMp_Cantidad).Sum();

            var asgBopp = (from asgbopp in _context.Set<DetalleAsignacion_BOPP>()
                           where asgbopp.DtAsigBOPP_OrdenTrabajo == ot
                           select asgbopp.DtAsigBOPP_Cantidad).Sum();

            var devol = (from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                         where dev.DevMatPri.DevMatPri_OrdenTrabajo == ot &&
                               dev.Tinta_Id == 2001
                         select dev.DtDevMatPri_CantidadDevuelta).Sum();

            var asigs = (asig + asgBopp) - devol;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(asigs);
        }

        //Consulta que traerá la cantidad de materia prima asignada, teniendo en cuenta la materia prima devuelta
        [HttpGet("getPolietilenoAsignada/{ot}")]
        public ActionResult GetPolietilenoAsignada(int ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = (from asg in _context.Set<DetalleAsignacion_MateriaPrima>()
                        where asg.AsigMp.AsigMP_OrdenTrabajo == ot
                        select asg.DtAsigMp_Cantidad).Sum();

            var devol = (from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                         where dev.DevMatPri.DevMatPri_OrdenTrabajo == ot &&
                               dev.Tinta_Id == 2001 &&
                               dev.BOPP_Id == 449
                         select dev.DtDevMatPri_CantidadDevuelta).Sum();

            var asigs = asig - devol;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(asigs);
        }

        /** Obtener las materias primas asignadas en base a solicitudes de material */
        [HttpGet("getAsignacionesConSolicitudes/{idSolicitud}")]
        public ActionResult GetAsignacionesConSolicitudes(long idSolicitud)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var conMp = (from Asgmp in _context.Set<DetalleAsignacion_MateriaPrima>()
                         where Asgmp.AsigMp.SolMpExt_Id == idSolicitud
                         group Asgmp by new
                         {
                             Asgmp.MatPri_Id,
                             Asgmp.MatPri.MatPri_Nombre,
                             Asgmp.UndMed_Id,
                             Asgmp.MatPri.MatPri_Precio
                         } into y
                         select new
                         {
                             //Materia Prima
                             MatPri_Id = Convert.ToInt16(y.Key.MatPri_Id),
                             Tinta_Id = Convert.ToInt16(2001),
                             MateriaPrima = y.Key.MatPri_Id,
                             NombreMP = y.Key.MatPri_Nombre,
                             CantMP = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad),
                             UndMedida = y.Key.UndMed_Id,
                         });

            var conTinta = (from AsgTinta in _context.Set<DetalleAsignacion_Tinta>()
                            where AsgTinta.AsigMp.SolMpExt_Id == idSolicitud
                            group AsgTinta by new
                            {
                                AsgTinta.Tinta_Id,
                                AsgTinta.Tinta.Tinta_Nombre,
                                AsgTinta.UndMed_Id,
                            } into y
                            select new
                            {
                                //Tintas
                                MatPri_Id = Convert.ToInt16(84),
                                Tinta_Id = Convert.ToInt16(y.Key.Tinta_Id),
                                MateriaPrima = y.Key.Tinta_Id,
                                NombreMP = y.Key.Tinta_Nombre,
                                CantMP = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad),
                                UndMedida = y.Key.UndMed_Id,
                            });

            var con = conMp.Concat(conTinta);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (con == null) return BadRequest("No se encontraron datos de la solicitud consultada");
            return Ok(con);
        }

        // PUT: api/DetalleAsignacion_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_MateriaPrima(long AsigMp_Id, long MatPri_Id, DetalleAsignacion_MateriaPrima detalleAsignacion_MateriaPrima)
        {
            if (AsigMp_Id != detalleAsignacion_MateriaPrima.AsigMp_Id && MatPri_Id != detalleAsignacion_MateriaPrima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_MateriaPrimaExists(AsigMp_Id) && !DetalleAsignacion_MateriaPrimaExists(MatPri_Id))
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

        // POST: api/DetalleAsignacion_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_MateriaPrima>> PostDetalleAsignacion_MateriaPrima(DetalleAsignacion_MateriaPrima detalleAsignacion_MateriaPrima)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return Problem("Entity set 'dataContext.DetallesAsignaciones_MateriasPrimas'  is null.");
            }
            _context.DetallesAsignaciones_MateriasPrimas.Add(detalleAsignacion_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_MateriaPrimaExists(detalleAsignacion_MateriaPrima.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_MateriaPrima", new { id = detalleAsignacion_MateriaPrima.Codigo }, detalleAsignacion_MateriaPrima);
        }

        // DELETE: api/DetalleAsignacion_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_MateriaPrima(long id)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_MateriaPrima = await _context.DetallesAsignaciones_MateriasPrimas.FindAsync(id);
            if (detalleAsignacion_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_MateriasPrimas.Remove(detalleAsignacion_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_MateriaPrimaExists(long id)
        {
            return (_context.DetallesAsignaciones_MateriasPrimas?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
