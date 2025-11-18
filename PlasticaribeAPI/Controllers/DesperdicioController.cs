#nullable disable
using Intercom.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.Reflection.PortableExecutable;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DesperdiciosController : ControllerBase
    {
        private readonly dataContext _context;

        public DesperdiciosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Desperdicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Desperdicio>>> GetDesperdicios()
        {
            return await _context.Desperdicios.ToListAsync();
        }

        // GET: api/Desperdicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desperdicio>> GetDesperdicio(long id)
        {
            var Desperdicio = await _context.Desperdicios.FindAsync(id);

            if (Desperdicio == null)
            {
                return NotFound();
            }

            return Desperdicio;
        }

        [HttpGet("getUltimoPedido")]
        public ActionResult getUltimoPedido()
        {
            DateTime hora = Convert.ToDateTime("00:00:00");

            var desperdicioFecha = (from des in _context.Set<Desperdicio>()
                                    orderby des.Desp_Id descending
                                    select des.Desp_FechaRegistro).FirstOrDefault();

            var desperdicioHora = (from des in _context.Set<Desperdicio>()
                                   orderby des.Desp_Id descending
                                   select des.Desp_HoraRegistro).FirstOrDefault();

            var desperdicioUsuario = (from des in _context.Set<Desperdicio>()
                                      orderby des.Desp_Id descending
                                      select des.Usua_Id).FirstOrDefault();

            var con = from des in _context.Set<Desperdicio>()
                      from emp in _context.Set<Empresa>()
                      where emp.Empresa_Id == 800188732
                            && des.Desp_FechaRegistro == Convert.ToDateTime(desperdicioFecha).AddHours(hora.Hour).AddMinutes(hora.Minute).AddSeconds(hora.Second)
                            && des.Desp_HoraRegistro == Convert.ToString(desperdicioHora)
                            && des.Usua_Id == Convert.ToInt64(desperdicioUsuario)
                      select new
                      {
                          des.Desp_Id,
                          des.Desp_FechaRegistro,
                          des.Desp_OT,
                          des.Maquina,
                          des.Usuario1.Usua_Nombre,
                          des.Prod_Id,
                          des.Producto.Prod_Nombre,
                          des.Material.Material_Nombre,
                          des.Desp_Impresion,
                          des.Falla.Falla_Nombre,
                          des.Desp_PesoKg,
                          des.Desp_Observacion,
                          des.Desp_Fecha,
                          des.Proceso.Proceso_Nombre,
                          Creador = des.Usua_Id,
                          NombreCreador = des.Usuario2.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };

            return Ok(con);
        }


        /** OT */
        [HttpGet("getConsultaDesperdicioxOT/{OT}")]
        public ActionResult<Desperdicio> GetDesperdicioOT(long OT)
        {
            var Desperdicio = (from d in _context.Set<Desperdicio>()
                               from e in _context.Set<Empresa>()
                               where d.Desp_OT == OT &&
                               e.Empresa_Id == 800188732
                               select new
                               {
                                  Bulto = d.Desp_Id,
                                  OT = d.Desp_OT,
                                  Item = d.Prod_Id,
                                  Cantidad = d.Desp_PesoKg,
                                  Presentacion = Convert.ToString("Kg"),
                                  Referencia = d.Producto.Prod_Nombre,
                                  Id_Proceso = d.Proceso_Id,
                                  Proceso = d.Proceso.Proceso_Nombre,
                                  Id_Material = d.Material_Id,
                                  Material = d.Material.Material_Nombre,
                                  Id_Falla = d.Falla_Id,
                                  Falla = d.Falla.Falla_Nombre,
                                  Impreso = d.Desp_Impresion,
                                  Maquina = d.Maquina,
                                  Id_Operario = d.Usua_Operario,
                                  Operario = d.Usuario1.Usua_Nombre,
                                  Id_Usuario = d.Usua_Id,
                                  Usuario = d.Usuario2.Usua_Nombre,
                                  Fecha = d.Desp_Fecha,
                                  Observacion = d.Desp_Observacion,
                                  Fecha_Registro = d.Desp_FechaRegistro,
                                  Hora_Registro = d.Desp_HoraRegistro,
                                  Nit_Empresa = e.Empresa_Id,
                                  Empresa = e.Empresa_Nombre,
                                  Ciudad_Empresa = e.Empresa_Ciudad,
                                  Direccion_Empresa = e.Empresa_Direccion,
                               }).ToList();

            //if (Desperdicio == null) return NotFound();           
            return Ok(Desperdicio);
        }

        /** OT */
        [HttpGet("GetDesperdicioOt/{Ot}")]
        public ActionResult<Desperdicio> GetDesperdicioOt(long Ot)
        {
            var con = from des in _context.Set<Desperdicio>()
                      from emp in _context.Set<Empresa>()
                      where emp.Empresa_Id == 800188732
                            && des.Desp_OT == Ot
                      select new
                      {
                          des.Desp_Id,
                          des.Desp_FechaRegistro,
                          des.Desp_OT,
                          des.Maquina,
                          des.Usuario1.Usua_Nombre,
                          des.Prod_Id,
                          des.Producto.Prod_Nombre,
                          des.Material.Material_Nombre,
                          des.Desp_Impresion,
                          des.Falla.Falla_Nombre,
                          des.Desp_PesoKg,
                          des.Desp_Observacion,
                          des.Desp_Fecha,
                          des.Proceso.Proceso_Nombre,
                          Creador = des.Usua_Id,
                          NombreCreador = des.Usuario2.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };

            //if (Desperdicio == null) return NotFound();           
            return Ok(con);
        }

        
        [HttpGet("getConsultaDesperdicio2/{fecha1}/{fecha2}")]
#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        public ActionResult<Desperdicio> GetDesperdicios(DateTime fecha1, DateTime fecha2, string? OT = "", string? material = "", string? type = "", string? turn = "", string? process = "", string? machine = "")
#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        {
            string horaInicio = "07:00:00";
            string horaFin = "06:59:59";

            List<string> turnosDia = new List<string>();
            turnosDia.Add("DIA");
            turnosDia.Add("RD");

            List<string> turnosNoche = new List<string>();
            turnosNoche.Add("NOCHE");
            turnosNoche.Add("RN");

            List<int> machines = [35, 37, 38, 39];

            DateTime fechaExtendida = fecha2.AddDays(1);

            var Desperdicio = (from d in _context.Set<Desperdicio>()
                               where
                               (
                                 // Día inicial: desde 7 AM en adelante
                                 (d.Desp_Fecha == fecha1 && String.Compare(d.Desp_HoraRegistro, horaInicio) >= 0)
                                
                                ||
                                 // Día final extendido: hasta antes de 7 AM del día siguiente
                                 (d.Desp_Fecha == fechaExtendida && String.Compare(d.Desp_HoraRegistro, horaFin) < 0)
                                
                                ||
                                  // Días intermedios completos
                                  (d.Desp_Fecha > fecha1 && d.Desp_Fecha < fechaExtendida)
                                ) &&
                                (OT != "" ? (d.Desp_OT.ToString() == OT) : true) &&
                                (material != "" ? (d.Material_Id.ToString() == material) : true) &&
                                (type != "" ? (d.Falla_Id.ToString() == type) : true) &&
                                (turn != "" ? turn == "DIA" ? turnosDia.Contains(d.Turno_Id) : turn == "NOCHE" ? turnosNoche.Contains(d.Turno_Id) : (d.Turno_Id.ToString() == turn) : true) &&
                                (process != "" ? (d.Proceso_Id.ToString() == process) : true) &&
                                (machine != "" ? (d.Maquina.ToString() == machine) : true)
                               select new
                               {
                                   Bulto = d.Desp_Id,
                                   OT = d.Desp_OT,
                                   Item = d.Prod_Id,
                                   Cantidad = d.Desp_PesoKg,
                                   Presentacion = Convert.ToString("Kg"),
                                   Referencia = d.Producto.Prod_Nombre,
                                   Id_Proceso = d.Proceso_Id,
                                   Proceso = d.Proceso.Proceso_Nombre.ToUpper(),
                                   Id_Material = d.Material_Id,
                                   Material = d.Material.Material_Nombre,
                                   Id_Falla = d.Falla_Id,
                                   Falla = d.Falla.Falla_Nombre,
                                   Impreso = d.Desp_Impresion,
                                   Maquina = d.Maquina,
                                   Id_Operario = d.Usua_Operario,
                                   Operario = d.Usuario1.Usua_Nombre,
                                   Id_Usuario = d.Usua_Id,
                                   Usuario = d.Usuario2.Usua_Nombre,
                                   Fecha = d.Desp_Fecha,
                                   Observacion = Convert.ToString(d.Desp_Observacion).Contains("ProcDesperdicio") ? Convert.ToString(d.Desp_Observacion).Replace("Rollo #", "").Replace(" en ProcDesperdicio Bagpro", "") : Convert.ToString(d.Desp_Observacion).Replace("Rollo #", "").Replace(" en ProcExtrusion Bagpro", ""), 
                                   Fecha_Registro = d.Desp_FechaRegistro,
                                   Hora_Registro = d.Desp_HoraRegistro,
                                   Turno = d.Turno_Id,
                               }).ToList();

            if (Desperdicio == null) return NotFound();           
            else return Ok(Desperdicio);
        }

        [HttpGet("GetDesperdiciosMes/{process}")]
#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        public ActionResult<Desperdicio> GetDesperdiciosMes(string process)
#pragma warning restore CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        {
            string horaInicio = "07:00:00";
            string horaFin = "06:59:59";
            DateTime fecha = DateTime.Now;
            // Primer día del mes
            DateTime primerDia = new DateTime(fecha.Year, fecha.Month, 1);
            // Último día del mes
            DateTime ultimoDia = primerDia.AddMonths(1).AddDays(-1);

            DateTime fechaExtendida = ultimoDia.AddDays(1);
            List<int> machines = [35, 37, 38, 39];

            var Desperdicio = (from d in _context.Set<Desperdicio>()
                               where (
                                         process == "CAMISILLA" ? (d.Proceso_Id == "SELLA" && machines.Contains(Convert.ToInt32(d.Maquina))) :
                                         process == "SELLA" ? (d.Proceso_Id == "SELLA" && !machines.Contains(Convert.ToInt32(d.Maquina))) :
                                         process != "SELLA" ? (d.Proceso_Id == process) : 
                                         d.Proceso_Id == process
                                    )        
                               &&
                               (
                                 // Día inicial: desde 7 AM en adelante
                                 (d.Desp_Fecha == primerDia && String.Compare(d.Desp_HoraRegistro, horaInicio) >= 0)
                                ||
                                 // Día final extendido: hasta antes de 7 AM del día siguiente
                                 (d.Desp_Fecha == fechaExtendida && String.Compare(d.Desp_HoraRegistro, horaFin) < 0)
                                ||
                                  // Días intermedios completos
                                  (d.Desp_Fecha > primerDia && d.Desp_Fecha < fechaExtendida)
                                )
                               select (decimal?)d.Desp_PesoKg).Sum() ?? 0m;

            return Ok(Desperdicio);
        }

            /** Movimientos Desperdicios*/
            [HttpGet("getMovDesperdicios/{fecha1}/{fecha2}")]
#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        public ActionResult<Desperdicio> GetMovDesperdicios(DateTime fecha1, DateTime fecha2, string? OT = "", string? material = "", string? item = "", string? falla = "", string? proceso = "", string? maquina = "")
#pragma warning restore CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        {
            var Desperdicio = (from des in _context.Set<Desperdicio>()
                               where des.Desp_Fecha >= fecha1 &&
                               des.Desp_Fecha <= fecha2 &&
                               Convert.ToString(des.Desp_OT).Contains(OT) &&
                               Convert.ToString(des.Material_Id).Contains(material) &&
                               Convert.ToString(des.Prod_Id).Contains(item) &&
                               Convert.ToString(des.Falla_Id).Contains(falla) &&
                               Convert.ToString(des.Proceso_Id).Contains(proceso) &&
                               Convert.ToString(des.Maquina).Contains(maquina)
                               select new
                               {
                                   des.Desp_Id,
                                   des.Desp_OT,
                                   des.Maquina,
                                   des.Usuario1.Usua_Nombre,
                                   des.Prod_Id,
                                   des.Producto.Prod_Nombre,
                                   des.Material.Material_Nombre,
                                   des.Desp_Impresion,
                                   des.Falla.Falla_Nombre,
                                   des.Desp_PesoKg,
                                   Unidad = "Kg",
                                   des.Desp_Fecha,
                                   des.Proceso.Proceso_Nombre,
                                   UsuarioCreador = des.Usuario2.Usua_Nombre,
                                   des.Desp_FechaRegistro,
                                   des.Desp_Observacion,
                               }).ToList();

            //if (Desperdicio == null) return NotFound();           
            return Ok(Desperdicio);
        }


        // PUT: api/Desperdicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesperdicio(long id, Desperdicio Desperdicio)
        {
            if (id != Desperdicio.Desp_Id)
            {
                return BadRequest();
            }

            _context.Entry(Desperdicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesperdicioExists(id))
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

        // POST: api/Desperdicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Desperdicio>> PostDesperdicio(Desperdicio Desperdicio)
        {
            _context.Desperdicios.Add(Desperdicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDesperdicio", new { id = Desperdicio.Desp_Id }, Desperdicio);
        }

        // DELETE: api/Desperdicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesperdicio(long id)
        {
            var Desperdicio = await _context.Desperdicios.FindAsync(id);
            if (Desperdicio == null)
            {
                return NotFound();
            }

            _context.Desperdicios.Remove(Desperdicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DesperdicioExists(long id)
        {
            return _context.Desperdicios.Any(e => e.Desp_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
