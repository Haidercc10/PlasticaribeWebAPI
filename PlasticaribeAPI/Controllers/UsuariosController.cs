using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using StackExchange.Redis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UsuariosController : ControllerBase
    {
        private readonly dataContext _context;

        public UsuariosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet("nombreUsuario/{Usua_Nombre}")]
        public ActionResult<Usuario> GetProductoPedido(string Usua_Nombre)
        {
            try
            {
                var tipoProducto = _context.Usuarios.Where(tp => tp.Usua_Nombre == Usua_Nombre).ToList();


                if (tipoProducto == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(tipoProducto);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("UsuarioConductor/{ID}")]
        public ActionResult<Usuario> GetConductor(long ID)
        {
            try
            {
                var conductor = _context.Usuarios.Where(tp => tp.Usua_Id == ID && tp.tpUsu_Id == 7).ToList();


                if (conductor == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(conductor);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("getConductores")]
        public ActionResult GetCondutores()
        {
            var con = from usua in _context.Set<Usuario>()
                      where usua.RolUsu_Id == 11 && usua.Estado_Id == 1
                      select new
                      {
                          Nombre = usua.Usua_Nombre,
                          Id = usua.Usua_Id
                      };
            return con.Any() ? Ok(con) : NotFound();
        }

        [HttpGet("UsuariosxId/{ID}")]
        public ActionResult<Usuario> GetUsuariosxId(long ID)
        {
            try
            {
                var usuario = _context.Usuarios.Where(tp => tp.Usua_Id == ID)
                                               .Select(usu => new
                                               {
                                                   usu.Usua_Id,
                                                   usu.Usua_Nombre,
                                                   usu.tpUsu_Id,
                                                   usu.tpUsu.tpUsu_Nombre,
                                                   usu.Area_Id,
                                                   usu.Area.Area_Nombre,
                                                   usu.RolUsu_Id,
                                                   usu.RolUsu.RolUsu_Nombre,
                                                   usu.Estado_Id,
                                                   usu.Estado.Estado_Nombre,
                                                   usu.Usua_Telefono,
                                                   usu.fPen_Id,
                                                   usu.fPen.fPen_Nombre,
                                                   usu.Usua_Email,
                                                   usu.cajComp_Id,
                                                   usu.cajComp.cajComp_Nombre,
                                                   usu.eps_Id,
                                                   usu.EPS.eps_Nombre,
                                                   usu.Usua_Contrasena,
                                                   usu.Empresa_Id,
                                                   usu.Empresa.Empresa_Nombre,
                                                   usu.TipoIdentificacion_Id,
                                                   usu.Usua_Fecha,
                                                   usu.Usua_Hora, 
                                                   usu.Usua_Cedula,
                                               }).ToList();
                if (usuario == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(usuario);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("UsuariosSinParametros")]
        public ActionResult<Usuario> GetUsuarios2()
        {

            var usuario = _context.Usuarios.Select(usu => new
            {
                usu.Usua_Id,
                usu.Usua_Nombre,
                usu.tpUsu_Id,
                usu.tpUsu.tpUsu_Nombre,
                usu.Area_Id,
                usu.Area.Area_Nombre,
                usu.RolUsu_Id,
                usu.RolUsu.RolUsu_Nombre,
                usu.Estado_Id,
                usu.Estado.Estado_Nombre,
                usu.Usua_Telefono,
                usu.fPen_Id,
                usu.fPen.fPen_Nombre,
                usu.Usua_Email,
                usu.cajComp_Id,
                usu.cajComp.cajComp_Nombre,
                usu.eps_Id,
                usu.EPS.eps_Nombre,
                usu.Usua_Contrasena,
                usu.Empresa_Id,
                usu.Empresa.Empresa_Nombre,
                usu.TipoIdentificacion_Id,
                usu.Usua_Fecha,
                usu.Usua_Hora, 
                usu.Usua_Cedula,
            }).ToList();
            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(usuario);
            }


        }

        [HttpGet("getVendedores")]
        public ActionResult GetVendedores()
        {
            var usuario = from usu in _context.Set<Usuario>()
                          where usu.RolUsu_Id == 2 && usu.Estado_Id == 1
                          select new
                          {
                              usu.Usua_Nombre,
                              usu.Usua_Id
                          };
            return Ok(usuario);
        }

        [HttpGet("getOperariosProduccion")]
        public ActionResult GetOperariosProduccion()
        {
            var operarios = from op in _context.Set<Usuario>()
                            where op.RolUsu_Id == 59 && op.Estado_Id == 1
                            orderby op.Usua_Nombre ascending
                            select new
                            {
                                op.Usua_Id,
                                op.Usua_Nombre,
                                op.Area_Id
                            };
            return operarios.Any() ? Ok(operarios) : NotFound();
        }

        [HttpGet("getTrabajadores/{startDate}/{endDate}/{area}")]
        public ActionResult GetTrabajadores(DateTime startDate, DateTime endDate, string area)
        {
            string[] areas = area.Split("|");

            var notAvaibleWorkers = from p in _context.Set<NominaDetallada_Plasticaribe>()
                                    where (p.PeriodoInicio >= startDate && p.PeriodoInicio <= endDate) ||
                                          (p.PeriodoFin >= startDate && p.PeriodoFin <= endDate) || 
                                          (p.PeriodoInicio < startDate && p.PeriodoFin >= startDate)
                                    select p.Id_Trabajador;

            var workers = from u in _context.Set<Usuario>()
                          join a in _context.Set<Area>() on u.Area_Id equals a.Area_Id
                          join st in _context.Set<SalariosTrabajadores>() on u.Usua_Id equals st.Id_Trabajador
                          where areas.Contains(Convert.ToString(u.Area_Id)) &&
                                u.Estado_Id == 1 &&
                                !notAvaibleWorkers.Contains(u.Usua_Id)
                          select new
                          {
                              Identification = u.Usua_Id,
                              Name = u.Usua_Nombre,
                              a.Area_Id,
                              a.Area_Nombre,
                              BaseSalary = st.SalarioBase,
                              TransportAsistance = st.AuxilioTransp,
                              Eps = st.EPSMensual,
                              Afp = st.AFPMensual,
                              Saving = st.AhorroMensual,
                              DetailsMoneySave = (from pr in _context.Set<NominaDetallada_Plasticaribe>()
                                                  where pr.Id_Trabajador == st.Id_Trabajador &&
                                                        pr.Ahorro > 0 &&
                                                        u.Estado_Id == 1 &&
                                                        pr.Estado_Nomina == 13
                                                  select new
                                                  {
                                                      IdSaveMoney = st.Id,
                                                      Worker = u.Usua_Nombre,
                                                      SubTotalSaveMoney = st.AhorroTotal,
                                                      StartPayroll = pr.PeriodoInicio,
                                                      EndPayroll = pr.PeriodoFin,
                                                      TotalSaveInPayroll = pr.Ahorro
                                                  }).ToList(),
                              Loan = (from l in _context.Set<Prestamos>()
                                      where l.Usua_Id == u.Usua_Id &&
                                            l.Estado_Id == 11
                                      select new
                                      {
                                          IdLoan = l.Ptm_Id,
                                          Worker = u.Usua_Nombre,
                                          Date = l.Ptm_FechaRegistro + " " + l.Ptm_HoraRegistro,
                                          TotalLoan = l.Ptm_Valor,
                                          DebtValue = l.Ptm_ValorDeuda,
                                          TotalPay = l.Ptm_ValorCancelado,
                                          ValueQuota = l.Ptm_ValorCuota,
                                          PercentageQuota = l.Ptm_PctjeCuota,
                                          FinalDate = l.Ptm_FechaPlazo,
                                          LastPay = l.Ptm_FechaUltCuota,
                                      }).ToList(),
                              Disability = (from d in _context.Set<Incapacidades>()
                                            join td in _context.Set<TipoIncapacidad>() on d.Id_TipoIncapacidad equals td.Id
                                            where ((d.FechaInicio < startDate && d.FechaFin >= startDate) || 
                                                    d.FechaInicio >= startDate && d.FechaInicio <= endDate) &&
                                                  d.Id_Trabajador == u.Usua_Id
                                            select new
                                            {
                                                IdDisability = d.Id,
                                                Worker = u.Usua_Nombre,
                                                BaseSalary = st.SalarioBase,
                                                ValueDay = st.SalarioBase / 30,
                                                ValueHour = (st.SalarioBase / 30) / 8,
                                                IdTypeDisability = d.Id_TipoIncapacidad,
                                                TypeDisability = td.Nombre,
                                                StartDate = d.FechaInicio,
                                                EndDate = d.FechaFin,
                                                TotalDays = d.CantDias,
                                                TotalToPayThisPayroll = 0,
                                                TotalToPayPreviusPayrolls = (from m in _context.Set<Movimientos_Nomina>()
                                                                             where m.CodigoMovimento == d.Id &&
                                                                                   m.NombreMovimento == "INCAPACIDAD" &&
                                                                                   m.Trabajador_Id == d.Id_Trabajador
                                                                             orderby m.Id descending
                                                                             select m.ValorPagado).FirstOrDefault(),
                                                TotalToPayNextPayroll = d.TotalPagar - (from m in _context.Set<Movimientos_Nomina>()
                                                                                        where m.CodigoMovimento == d.Id &&
                                                                                              m.NombreMovimento == "INCAPACIDAD" &&
                                                                                              m.Trabajador_Id == d.Id_Trabajador
                                                                                        orderby m.Id descending
                                                                                        select m.ValorPagado).FirstOrDefault(),
                                                TotalToPay = d.TotalPagar,
                                                Observation = d.Observacion,
                                            }).ToList(),
                              Advance = (from dtP in _context.Set<NominaDetallada_Plasticaribe>()
                                         where dtP.Id_Trabajador == st.Id_Trabajador &&
                                               dtP.TipoNomina == 4 &&
                                               dtP.Estado_Nomina == 11
                                         select new
                                         {
                                             IdAdvance = dtP.Id,
                                             Worker = u.Usua_Nombre,
                                             StartDate = dtP.PeriodoInicio,
                                             EndDate = dtP.PeriodoFin,
                                             ValueAdvance = dtP.TotalPagar,
                                         }).ToList(),
                          };
            return workers.Any() ? Ok(workers) : NotFound();
        }

        [HttpGet("getNotAvaibleWorkers/{startDate}/{endDate}")]
        public ActionResult GetNotAvaibleWorkers(DateTime startDate, DateTime endDate)
        {
            return Ok(from p in _context.Set<NominaDetallada_Plasticaribe>()
                      where (p.PeriodoInicio >= startDate && p.PeriodoFin <= endDate)
                      select p.Id_Trabajador);
        }

        [HttpGet("getEmployees/{data}")]
        public ActionResult getEmployees(string data)
        {
            var employees = from emp in _context.Set<Usuario>()
                            where emp.Estado_Id == 1
                            && (emp.Usua_Nombre.Contains(data) || Convert.ToString(emp.Usua_Id) == data)
                            select new { emp.Usua_Id, emp.Usua_Cedula, emp.Usua_Nombre, emp.Area_Id, emp.RolUsu_Id };

            if (employees != null) return Ok(employees);
            else return NotFound();
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, Usuario usuario)
        {
            if (id != usuario.Usua_Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.Usua_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.Usua_Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.Usua_Id == id);
        }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
