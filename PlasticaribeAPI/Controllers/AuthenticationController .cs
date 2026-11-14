using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Service;
using System.Data.Common;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConfigurationManager = PlasticaribeAPI.Service.ConfigurationManager;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly dataContext _context;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(dataContext context, ILogger<AuthenticationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Inicia sesión. Respuestas: 200 con el token; 400 datos incompletos; 401 usuario, contraseña o empresa
        /// incorrectos (sin decir cuál, para no dar pistas); 403 usuario inactivo; 503 base de datos no disponible.
        /// Los errores van en formato ProblemDetails (title, detail, traceId).
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
            if (user is null || user.Id_Usuario is null || user.Empresa is null || string.IsNullOrEmpty(user.Contrasena))
                return Problema(StatusCodes.Status400BadRequest, "Datos incompletos.", "Debe enviar la identificación, la contraseña y la empresa.");

            try
            {
                var con = _context.Usuarios.FirstOrDefault(x => x.Usua_Id == user.Id_Usuario && x.Usua_Contrasena == user.Contrasena && x.Empresa_Id == user.Empresa);

                if (con is null)
                    return Problema(StatusCodes.Status401Unauthorized, "Credenciales incorrectas.", "Usuario, contraseña o empresa incorrectos.");

                if (con.Estado_Id != 1)
                    return Problema(StatusCodes.Status403Forbidden, "Usuario inactivo.", "El usuario está inactivo. Comuníquese con el administrador del sistema.");

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                    audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                    claims: new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, con.Usua_Id.ToString(CultureInfo.InvariantCulture)),
                        new Claim(ClaimTypes.Role, con.RolUsu_Id.ToString(CultureInfo.InvariantCulture))
                    },
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
            catch (Exception ex) when (EsFallaBaseDatos(ex))
            {
                _logger.LogError(ex, "Login: no se pudo consultar la base de datos (traceId {TraceId}).", HttpContext.TraceIdentifier);
                return Problema(StatusCodes.Status503ServiceUnavailable, "Base de datos no disponible.", "No fue posible conectarse a la base de datos de Plasticaribe. Intente de nuevo en unos minutos.");
            }
        }

        /// <summary>Falla de conexión con SQL Server (directa o envuelta por los reintentos de EF).</summary>
        private static bool EsFallaBaseDatos(Exception ex)
        {
            for (var e = ex; e != null; e = e.InnerException)
                if (e is DbException || e is RetryLimitExceededException) return true;
            return false;
        }

        /// <summary>Respuesta de error en formato ProblemDetails con el traceId de la petición.</summary>
        private ObjectResult Problema(int status, string titulo, string detalle)
        {
            var problema = new ProblemDetails { Status = status, Title = titulo, Detail = detalle };
            problema.Extensions["traceId"] = HttpContext.TraceIdentifier;
            return new ObjectResult(problema) { StatusCode = status, ContentTypes = { "application/problem+json" } };
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
