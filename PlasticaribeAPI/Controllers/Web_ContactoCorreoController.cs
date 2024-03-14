using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController]
    public class Web_ContactoCorreoController : ControllerBase
    {
        private readonly dataContext _context;

        public Web_ContactoCorreoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Web_ContactoCorreo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Web_ContactoCorreo>>> GetWeb_ContactoCorreo()
        {
            if (_context.Web_ContactoCorreo == null)
            {
                return NotFound();
            }
            return await _context.Web_ContactoCorreo.ToListAsync();
        }

        // GET: api/Web_ContactoCorreo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Web_ContactoCorreo>> GetWeb_ContactoCorreo(int id)
        {
            if (_context.Web_ContactoCorreo == null)
            {
                return NotFound();
            }
            var web_ContactoCorreo = await _context.Web_ContactoCorreo.FindAsync(id);

            if (web_ContactoCorreo == null)
            {
                return NotFound();
            }

            return web_ContactoCorreo;
        }

        // PUT: api/Web_ContactoCorreo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeb_ContactoCorreo(int id, Web_ContactoCorreo web_ContactoCorreo)
        {
            if (id != web_ContactoCorreo.Id)
            {
                return BadRequest();
            }

            _context.Entry(web_ContactoCorreo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Web_ContactoCorreoExists(id))
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

        // POST: api/Web_ContactoCorreo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Web_ContactoCorreo>> PostWeb_ContactoCorreo(Web_ContactoCorreo web_ContactoCorreo)
        {
            if (_context.Web_ContactoCorreo == null)
            {
                return Problem("Entity set 'dataContext.Web_ContactoCorreo'  is null.");
            }
            _context.Web_ContactoCorreo.Add(web_ContactoCorreo);
            await _context.SaveChangesAsync();
            Envio_Correo(web_ContactoCorreo);
            return CreatedAtAction("GetWeb_ContactoCorreo", new { id = web_ContactoCorreo.Id }, web_ContactoCorreo);
        }

        // DELETE: api/Web_ContactoCorreo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeb_ContactoCorreo(int id)
        {
            if (_context.Web_ContactoCorreo == null)
            {
                return NotFound();
            }
            var web_ContactoCorreo = await _context.Web_ContactoCorreo.FindAsync(id);
            if (web_ContactoCorreo == null)
            {
                return NotFound();
            }

            _context.Web_ContactoCorreo.Remove(web_ContactoCorreo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //ENVIO DE CORREO ELECTRONICO
        [HttpPost("Envio_Correo")]
        public ActionResult<Web_ContactoCorreo> Envio_Correo(Web_ContactoCorreo web_ContactoCorreo)
        {
            // Información de contacto de la empresa
            int idEmpresa = 800188732;
            var correoEmpresa = (from emp in _context.Set<Empresa>() where emp.Empresa_Id == idEmpresa select emp.Empresa_Correo).First();
            var telefonos = (from emp in _context.Set<Empresa>() where emp.Empresa_Id == idEmpresa select emp.Empresa_Telefono).First();
            var direccion = (from emp in _context.Set<Empresa>() where emp.Empresa_Id == idEmpresa select emp.Empresa_Direccion).First();

            //Configuracion de envio de correo
            string destinos = web_ContactoCorreo.Correo + ";prueba20230227@gmail.com";
            string[] destinatarios = destinos.Split(';');
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(web_ContactoCorreo.Nombre, web_ContactoCorreo.Correo));
            foreach (var destinatario in destinatarios)
            {
                message.To.Add(new MailboxAddress("", destinatario));
            }
            message.Subject = web_ContactoCorreo.Asunto;

            //Configuracion del cuerpo del correo
            var builder = new BodyBuilder();
            builder.TextBody = web_ContactoCorreo.Mensaje;

            //Imagenes
            string rutaImg = "C:\\inetpub\\WebApi\\Assets\\";
            string logo = "" + rutaImg + "logo-footer.png";
            string iconoTelefono = "" + rutaImg + "telefono.png";
            string iconoMapa = "" + rutaImg + "mapa.png";
            string iconoCorreo = "" + rutaImg + "correo.png";
            string facebook = "" + rutaImg + "facebook.png";
            string instagram = "" + rutaImg + "instagram.png";
            string whatsapp = "" + rutaImg + "whatsapp.png";

            // Se coloca un ID a cada imagen para llamarla y agregarla en el HTML
            var imagenLogo = builder.LinkedResources.Add(@"" + logo).ContentId = "logo";
            var imageTelefono = builder.LinkedResources.Add(@"" + iconoTelefono).ContentId = "telefono";
            var imageMapa = builder.LinkedResources.Add(@"" + iconoMapa).ContentId = "mapa";
            var imageCorreo = builder.LinkedResources.Add(@"" + iconoCorreo).ContentId = "correo";
            var imagefacebook = builder.LinkedResources.Add(@"" + facebook).ContentId = "facebook";
            var imageInstagram = builder.LinkedResources.Add(@"" + instagram).ContentId = "instagram";
            var imageWhatsApp = builder.LinkedResources.Add(@"" + whatsapp).ContentId = "whatsapp";

            //Estructura HTML del correo
            builder.HtmlBody = string.Format(@"<div style=""font-family: Verdana, Geneva, Tahoma, sans-serif; text-decoration: none; display: flex; font-family: 'Ubuntu', sans-serif; padding-top: 15%; padding-bottom: 25%; display: block;"">");
            builder.HtmlBody += string.Format(@"<center><img src=""cid:logo""></center> ");
            builder.HtmlBody += string.Format(@"<div style=""margin: 5% 10%;"">");
            builder.HtmlBody += string.Format(@"<h2 style=""color: #d83542; text-align: end;"">" + web_ContactoCorreo.Asunto + "</h2>");
            builder.HtmlBody += string.Format(@"<p style=""text-align: justify;""> ");
            builder.HtmlBody += string.Format(@web_ContactoCorreo.Mensaje);
            builder.HtmlBody += string.Format(@"</p>");
            builder.HtmlBody += string.Format(@"<br>");
            builder.HtmlBody += string.Format(@"<br>");
            builder.HtmlBody += string.Format(@"<p>");
            builder.HtmlBody += string.Format(@"Correo enviado por: <b>" + web_ContactoCorreo.Nombre + ".</b><br>Número de contacto: <b>" + web_ContactoCorreo.Telefono + ".</b><br>Correo de contacto: <b>" + web_ContactoCorreo.Correo + "</b>.");
            builder.HtmlBody += string.Format(@"</p>");
            builder.HtmlBody += string.Format(@"</div>");
            builder.HtmlBody += string.Format(@"<section style=""text-align: center;"">");
            builder.HtmlBody += string.Format(@"<h4 style=""color: #d83542;"">Contactanos</h4>");
            builder.HtmlBody += string.Format(@"<div style=""display: block; text-align: center; margin-bottom: 10%;"">");
            builder.HtmlBody += string.Format(@"<span> ");
            builder.HtmlBody += string.Format(@"<a style=""color : #000; text-decoration: none;"" onmouseover=""this.style.color='#d83542"" onmouseout=""this.style.color='#000'"" href=""https://goo.gl/maps/DgChbm9h4mYKgUc99"" target=""_blank""> ");
            builder.HtmlBody += string.Format(@"<img src=""cid:mapa"" style=""width: 15px; margin: 0 5px;"">");
            builder.HtmlBody += string.Format(@"" + direccion + "");
            builder.HtmlBody += string.Format(@"</a>");
            builder.HtmlBody += string.Format(@"</span>");
            builder.HtmlBody += string.Format(@"<br>");
            builder.HtmlBody += string.Format(@"<span>");
            builder.HtmlBody += string.Format(@"<img src=""cid:telefono"" style=""width: 15px; margin: 0 5px;"">");
            builder.HtmlBody += string.Format(@"Números de Contacto<a style=""color : #000; text-decoration: none;"" onmouseover=""this.style.color='#d83542"" onmouseout=""this.style.color='#000'"" href=""tel:""""> " + telefonos + " </a>");
            builder.HtmlBody += string.Format(@"</span>");
            builder.HtmlBody += string.Format(@"<br>");
            builder.HtmlBody += string.Format(@"<span>");
            builder.HtmlBody += string.Format(@"<a style=""color : #000; text-decoration: none;"" onmouseover=""this.style.color='#d83542"" onmouseout=""this.style.color='#000'"" href=""mailto:"">");
            builder.HtmlBody += string.Format(@"<img src=""cid:correo"" style=""width: 15px; margin: 0 5px;"">");
            builder.HtmlBody += string.Format(@"" + correoEmpresa + "");
            builder.HtmlBody += string.Format(@"</a>");
            builder.HtmlBody += string.Format(@"</span>");
            builder.HtmlBody += string.Format(@"</div>");
            builder.HtmlBody += string.Format(@"</section>");
            builder.HtmlBody += string.Format(@"<footer style=""text-align: center;"">");
            builder.HtmlBody += string.Format(@"<a style=""color : #000; text-decoration: none;"" onmouseover=""this.style.color='#d83542"" onmouseout=""this.style.color='#000'"" href=""https://es-la.facebook.com/"" target=""_blank"">");
            builder.HtmlBody += string.Format(@"<img style=""margin: 0 2%; min-width: 20px; max-width: 40px;"" src=""cid:facebook"" alt=""Facebook"">");
            builder.HtmlBody += string.Format(@"</a>");
            builder.HtmlBody += string.Format(@"<a style=""color : #000; text-decoration: none;"" onmouseover=""this.style.color='#d83542"" onmouseout=""this.style.color='#000'"" href=""https://www.instagram.com/"" target=""_blank"">");
            builder.HtmlBody += string.Format(@"<img style=""margin: 0 2%; min-width: 20px; max-width: 40px;"" src=""cid:instagram"" alt=""Instagram"">");
            builder.HtmlBody += string.Format(@"</a>");
            builder.HtmlBody += string.Format(@"<a style=""color : #000; text-decoration: none;"" onmouseover=""this.style.color='#d83542"" onmouseout=""this.style.color='#000'"" href="""" target=""_blank"">");
            builder.HtmlBody += string.Format(@"<img style=""margin: 0 2%; min-width: 20px; max-width: 40px;"" src=""cid:whatsapp"" alt=""WhatsApp"">");
            builder.HtmlBody += string.Format(@"</a>");
            builder.HtmlBody += string.Format(@"</footer>");
            builder.HtmlBody += string.Format(@"</div> ");

            // Podemos añadir archivos con la linea siguiente linea
            //builder.Attachments.Add(@"C:\\Users\\SANDRA\\Desktop\\logo-footer.png");

            // Cambiamos el cuerpo del correo
            message.Body = builder.ToMessageBody();

            //message.Body = new TextPart("plain") { Text = web_ContactoCorreo.Mensaje };

            //Credenciales del correo
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("prueba20230227@gmail.com", "cvspiiikzqxeggjk");
                client.Send(message);
                client.Disconnect(true);
            }
            return Ok("El correo ha sido enviado");
        }

        private bool Web_ContactoCorreoExists(int id)
        {
            return (_context.Web_ContactoCorreo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
