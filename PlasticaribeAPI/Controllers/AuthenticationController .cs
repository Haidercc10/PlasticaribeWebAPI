using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConfigurationManager = PlasticaribeAPI.Service.ConfigurationManager;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly dataContext _context;
        public AuthenticationController(dataContext context) { _context = context; }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
            if (user is null) return BadRequest("Usuairo Invalido");
            var con = _context.Usuarios.Where(x => x.Usua_Id == user.Id_Usuario && x.Usua_Contrasena == user.Contrasena && x.Empresa_Id == user.Empresa && x.Estado_Id == 1).First();

#pragma warning disable CS8604 // Posible argumento de referencia nulo
            if (con != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                    audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse
                {
                    Usuario = con.Usua_Nombre,
                    Usua_Id = con.Usua_Id,
                    RolUsu_Id = con.RolUsu_Id,
                    Token = tokenString
                });
            }
            return Unauthorized();
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }
    }
}
