﻿#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
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
                                                   usu.Usua_Hora
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
                usu.Usua_Hora
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
                            where op.RolUsu_Id == 59
                            select new
                            {
                                op.Usua_Id,
                                op.Usua_Nombre,
                                op.Area_Id
                            };
            return operarios.Any() ? Ok(operarios) : NotFound();
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
}
