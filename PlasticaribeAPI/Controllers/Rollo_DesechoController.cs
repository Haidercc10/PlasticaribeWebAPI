﻿using System;
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

        // GET: api/Rollo_Desecho/5
        [HttpGet("Und")]
        public ActionResult GetRollo_Desecho2()
        {
            var rollo_Desecho = _context.Rollos_Desechos.Where(und => und.Rollo_OT == 121388).Select(c => new { c.Proceso_Id });

            if (rollo_Desecho == null)
            {
                return NotFound();
            }

            return Ok(rollo_Desecho);
        }

        /** Consultas para Reporte de Rollos Eliminados */
        //Rollos Eliminados Hoy
        //Fechas, material, OT
        [HttpGet("RollosxHoy")]
        public ActionResult GetRollo31()
        {
            DateTime Hoy = DateTime.Today;
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso == Hoy)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

        //OT
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
                                                                 Rollo_Fuelle =Convert.ToString(r.Rollo_Fuelle),
                                                                 UndMed_Id = Convert.ToString(r.UndMed_Id),
                                                                 Rollo_PesoNeto = Convert.ToString(r.Rollo_PesoNeto),
                                                                 Material_Id = Convert.ToString(r.Material_Id),
                                                                 Material_Nombre = Convert.ToString(r.Material.Material_Nombre),
                                                                 Rollo_Calibre = Convert.ToString(r.Rollo_Calibre),
                                                                 Rollo_Operario = Convert.ToString(r.Rollo_Operario),
                                                                 Rollo_FechaIngreso = Convert.ToString(r.Rollo_FechaIngreso),
                                                                 Turno_Id = Convert.ToString(r.Turno_Id)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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

        //Fecha
        [HttpGet("RollosxFecha/{fecha}")]
        public ActionResult GetRollo3(DateTime fecha)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso == fecha)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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

        //Material
        [HttpGet("RollosxMaterial/{material}")]
        public ActionResult GetRollo7(int material)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Material_Id == material)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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

       

        //Fecha, OT
        [HttpGet("RollosxFechaxOT/{fecha}/{OT}")]
        public ActionResult GetRollo32(DateTime fecha, long OT)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_OT == OT && 
                                                          r.Rollo_FechaIngreso == fecha)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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

        //Fecha, Rollo
        [HttpGet("RollosxFechaxRollo/{fecha}/{rollo}")]
        public ActionResult GetRollo33(DateTime fecha, long rollo )
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_Id == rollo &&
                                                               r.Rollo_FechaIngreso == fecha)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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


        //Fecha, item
        [HttpGet("RollosxFechaxItem/{fecha}/{item}")]
        public ActionResult GetRollo35(DateTime fecha, int item)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Prod_Id == item &&
                                                               r.Rollo_FechaIngreso == fecha)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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

        //Fecha, Material
        [HttpGet("RollosxMaterial/{fecha}/{material}")]
        public ActionResult GetRollo36(DateTime fecha, int material)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Material_Id == material &&
                                                               r.Rollo_FechaIngreso == fecha)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                return Ok(rollo_Desecho);
            
        }

        //Fechas, Rollo
        [HttpGet("RollosxFechasxRollo/{fecha1}/{fecha2}/{rollo}")]
        public ActionResult GetRollo10(DateTime fecha1, DateTime fecha2, long rollo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
                return Ok(rollo_Desecho);
            
        }

        //Fechas, Material
        [HttpGet("RollosxFechasxMaterial/{fecha1}/{fecha2}/{material}")]
        public ActionResult GetRollo16(DateTime fecha1, DateTime fecha2, int material)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Material_Id == material)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }

     
        //Fechas, rollo, material
        [HttpGet("RollosxFechasxRolloxMaterial/{fecha1}/{fecha2}/{rollo}/{material}")]
        public ActionResult GetRollo20(DateTime fecha1, DateTime fecha2, long rollo, int material)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Rollo_Id == rollo
                                                               && r.Material_Id == material)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
                                                         }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.           
            return Ok(rollo_Desecho);

        }



        //Fechas, item, material
        [HttpGet("RollosxFechasxItemxMaterial/{fecha1}/{fecha2}/{item}/{material}")]
        public ActionResult GetRollo24(DateTime fecha1, DateTime fecha2, int item, int material)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var rollo_Desecho = _context.Rollos_Desechos.Where(r => r.Rollo_FechaIngreso >= fecha1
                                                               && r.Rollo_FechaIngreso <= fecha2
                                                               && r.Prod_Id == item
                                                               && r.Material_Id == material)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
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
                                                             Turno_Id = Convert.ToString(r.Turno_Id)
                                                         }).ToList();
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
}