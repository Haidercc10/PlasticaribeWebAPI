using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Migrations;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Rollo_DesechoController : ControllerBase
    {
        private readonly dataContext _context;

        public Rollo_DesechoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Rollo_Desecho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rollo_Desecho>>> GetRollos_Desechos()
        {
            return await _context.Rollos_Desechos.ToListAsync();
        }

        // GET: api/Rollo_Desecho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rollo_Desecho>> GetRollo_Desecho(long id)
        {
            var rollo_Desecho = await _context.Rollos_Desechos.FindAsync(id);

            if (rollo_Desecho == null)
            {
                return NotFound();
            }

            return rollo_Desecho;
        }

        [HttpGet("RollosxOT/{OT}")]
        public ActionResult GetRollo1(long OT)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_OT == OT)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (rollo_Desecho == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(rollo_Desecho);
            }
        }

        //Rollo
        [HttpGet("RollosxRollo/{rollo}")]
        public ActionResult GetRollo2(long rollo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_Id == rollo)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (rollo_Desecho == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(rollo_Desecho);
            }
        }

        //item
        [HttpGet("RollosxItem/{item}")]
        public ActionResult GetRollo6(int item)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Prod_Id == item)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (rollo_Desecho == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(rollo_Desecho);
            }
        }

        //Proceso
        [HttpGet("RollosxProceso/{proceso}")]
        public ActionResult GetRollo7(string proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Proceso_Id == proceso)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (rollo_Desecho == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(rollo_Desecho);
            }
        }

        //Fechas
        [HttpGet("RollosxFechas/{fecha1}/{fecha2}")]
        public ActionResult GetRollo8(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1 && r.Rollo_FechaIngreso <= fecha2)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(rollo_Desecho);

        }

        //Fechas, OT
        [HttpGet("RollosxFechasxOT/{fecha1}/{fecha2}/{OT}")]
        public ActionResult GetRollo9(DateTime fecha1, DateTime fecha2, long OT)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Rollo_OT == OT)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(rollo_Desecho);

        }

        //Fechas, Item
        [HttpGet("RollosxFechasxItem/{fecha1}/{fecha2}/{item}")]
        public ActionResult GetRollo15(DateTime fecha1, DateTime fecha2, int item)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Prod_Id == item)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, rollo, Item
        [HttpGet("RollosxFechasxRolloxItem/{fecha1}/{fecha2}/{rollo}/{item}")]
        public ActionResult GetRollo18(DateTime fecha1, DateTime fecha2, long rollo, int item)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Rollo_Id == rollo
                                                               && r.Prod_Id == item)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, rollo, OT
        [HttpGet("RollosxFechasxRolloxOT/{fecha1}/{fecha2}/{rollo}/{OT}")]
        public ActionResult GetRollo22(DateTime fecha1, DateTime fecha2, long rollo, long OT)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Rollo_Id == rollo
                                                               && r.Rollo_OT == OT)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, Item, OT
        [HttpGet("RollosxFechasxItemxOT/{fecha1}/{fecha2}/{item}/{OT}")]
        public ActionResult GetRollo26(DateTime fecha1, DateTime fecha2, int item, long OT)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Prod_Id == item
                                                               && r.Rollo_OT == OT)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, rollo, Item, OT
        [HttpGet("RollosxFechasxRolloxItemxOT/{fecha1}/{fecha2}/{rollo}/{item}/{OT}")]
        public ActionResult GetRollo42(DateTime fecha1, DateTime fecha2, long rollo, int item, long OT)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Prod_Id == item
                                                               && r.Rollo_OT == OT
                                                               && r.Rollo_Id == rollo)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, rollo, Item, Proceso -- ok
        [HttpGet("RollosxFechasxRolloxItemxProceso/{fecha1}/{fecha2}/{rollo}/{item}/{proceso}")]
        public ActionResult GetRollo36(DateTime fecha1, DateTime fecha2, long rollo, int item, string proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Prod_Id == item
                                                               && r.Rollo_Id == rollo
                                                               && r.Proceso_Id == proceso)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, rollo, OT, Proceso -- ok
        [HttpGet("RollosxFechasxRolloxOTxProceso/{fecha1}/{fecha2}/{rollo}/{OT}/{proceso}")]
        public ActionResult GetRollo43(DateTime fecha1, DateTime fecha2, long rollo, long OT, string proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Rollo_OT == OT
                                                               && r.Rollo_Id == rollo
                                                               && r.Proceso_Id == proceso)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, rollo, Proceso, --ok
        [HttpGet("RollosxFechasxRolloxProceso/{fecha1}/{fecha2}/{rollo}/{proceso}")]
        public ActionResult GetRollo37(DateTime fecha1, DateTime fecha2, long rollo, string proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Rollo_Id == rollo
                                                               && r.Proceso_Id == proceso)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, Item, Proceso, --ok
        [HttpGet("RollosxFechasxItemxProceso/{fecha1}/{fecha2}/{item}/{proceso}")]
        public ActionResult GetRollo41(DateTime fecha1, DateTime fecha2, int item, string proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Prod_Id == item
                                                               && r.Proceso_Id == proceso)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //Fechas, Proceso --ok
        [HttpGet("RollosxFechasxProceso/{fecha}/{fecha2}/{proceso}")]
        public ActionResult GetRollo36(DateTime fecha, DateTime fecha2, int proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Material_Id == proceso &&
                                                               r.Rollo_FechaIngreso >= fecha &&
                                                               r.Rollo_FechaIngreso <= fecha2)
                                                         .Select(r => new
                                                         {
                                                             Rollo_OT = Convert.ToString(r.Rollo_OT),
                                                             Rollo_Id = Convert.ToString(r.Rollo_Id),
                                                             Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                                             Prod_Id = Convert.ToString(r.Prod_Id),
                                                             Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                                             Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                                             Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                                             Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                                             UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                             Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                             Material_Id = Convert.ToString(r.Material_Id),
                                                             Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                             Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                             Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                             Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                             Turno_Id = Convert.ToString(r.Turno_Id),
                                                             Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                                             Proceso_Id = Convert.ToString(r.Proceso_Id),
                                                             Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (rollo_Desecho == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(rollo_Desecho);
            }
        }

        //Fechas
        [HttpGet("getRemovedRolls/{fecha1}/{fecha2}")]
        public ActionResult RollosEliminados(DateTime fecha1, DateTime fecha2, string? ot = "", string? roll = "", string? process = "", string? item = ""  )
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = from r in _context.Set<Rollo_Desecho>()
                                where r.Rollo_FechaEliminacion >= fecha1 &&
                                      r.Rollo_FechaEliminacion <= fecha2 &&
                                      Convert.ToString(r.Rollo_OT).Contains(ot) &&
                                      Convert.ToString(r.Rollo_Id).Contains(roll) &&
                                      Convert.ToString(r.Proceso_Id).Contains(process) &&
                                      Convert.ToString(r.Prod_Id).Contains(item)
                                select new
                                {
                                    Rollo_OT = Convert.ToString(r.Rollo_OT),
                                    Rollo_Id = Convert.ToString(r.Rollo_Id),
                                    Rollo_Cliente = Convert.ToString(r.Rollo_Cliente),
                                    Prod_Id = Convert.ToString(r.Prod_Id),
                                    Prod_Nombre = Convert.ToString(r.Prod.Prod_Nombre),
                                    Rollo_Ancho = Convert.ToString(r.Rollo_Ancho),
                                    Rollo_Largo = Convert.ToString(r.Rollo_Largo),
                                    Rollo_Fuelle = Convert.ToString(r.Rollo_Fuelle),
                                    UndMed_Id = Convert.ToString(r.UndMed_Id),
                                    Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                    Material_Id = Convert.ToString(r.Material_Id),
                                    Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                    Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                    Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                    Rollo_FechaIngreso = r.Rollo_FechaIngreso,
                                    Rollo_HoraIngreso = Convert.ToString(r.Rollo_Hora),
                                    Rollo_FechaEliminacion = r.Rollo_FechaEliminacion,
                                    Rollo_HoraEliminacion = Convert.ToString(r.Rollo_HoraEliminacion),
                                    Turno_Id = Convert.ToString(r.Turno_Id),
                                    Turno_Nombre = Convert.ToString(r.Turno.Turno_Nombre),
                                    Proceso_Id = Convert.ToString(r.Proceso_Id),
                                    Proceso_Nombre = Convert.ToString(r.Proceso.Proceso_Nombre),
                                    Falla_Id = Convert.ToString(r.Falla_Id),
                                    Falla_Nombre = Convert.ToString(r.Falla.Falla_Nombre),
                                    Observacion = Convert.ToString(r.Rollo_Observacion),
                                };                    
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(rollo_Desecho);

        }

        // PUT: api/Rollo_Desecho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRollo_Desecho(long id, Rollo_Desecho rollo_Desecho)
        {
            if (id != rollo_Desecho.Rollo_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(rollo_Desecho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Rollo_DesechoExists(id))
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

        //Función para actualizar el motivo (Falla técnica) por el que se eliminó el rollo
        [HttpPut("putFailRolls/{roll}/{fail}/{observation}")]
        public async Task<IActionResult> putFailRolls(long roll, int fail, string observation)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var rolls = (from rd in _context.Set<Rollo_Desecho>() where rd.Rollo_Id == roll select rd).FirstOrDefault();

            rolls.Falla_Id = fail;
            rolls.Rollo_Observacion = observation;
            _context.Entry(rolls).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // POST: api/Rollo_Desecho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rollo_Desecho>> PostRollo_Desecho(Rollo_Desecho rollo_Desecho)
        {
            _context.Rollos_Desechos.Add(rollo_Desecho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRollo_Desecho", new { id = rollo_Desecho.Rollo_Codigo }, rollo_Desecho);
        }

        // DELETE: api/Rollo_Desecho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRollo_Desecho(long id)
        {
            var rollo_Desecho = await _context.Rollos_Desechos.FindAsync(id);
            if (rollo_Desecho == null)
            {
                return NotFound();
            }

            _context.Rollos_Desechos.Remove(rollo_Desecho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Rollo_DesechoExists(long id)
        {
            return _context.Rollos_Desechos.Any(e => e.Rollo_Codigo == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
