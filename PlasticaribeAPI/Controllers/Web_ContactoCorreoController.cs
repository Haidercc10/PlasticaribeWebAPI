using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Utils;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
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

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Plasticaribe SAS", web_ContactoCorreo.Correo));
            message.To.Add(new MailboxAddress(web_ContactoCorreo.Nombre, web_ContactoCorreo.Correo));
            message.Subject = web_ContactoCorreo.Asunto;

            var builder = new BodyBuilder();

            string logo = "C:\\Users\\SANDRA\\Desktop\\logo-footer.png"; //Variable que tendrá la ruta del logo de la empresa

            // Set the plain-text version of the message text
            builder.TextBody = web_ContactoCorreo.Mensaje;

            // In order to reference selfie.jpg from the html text, we'll need to add it
            // to builder.LinkedResources and then use its Content-Id value in the img src.
            var image = builder.LinkedResources.Add(@"" + logo);
            image.ContentId = MimeUtils.GenerateMessageId();

            // Mensaje n
            builder.HtmlBody = string.Format(@"
                <center><img src=""cid:{0}""></center>
                    <h2>" + web_ContactoCorreo.Nombre + "</h2> " +
                    "<p>" +
                    web_ContactoCorreo.Mensaje +
                    "</p> ", image.ContentId);

            // Podemos añadir archivos con la linea siguiente linea
            //builder.Attachments.Add(@"C:\\Users\\SANDRA\\Desktop\\logo-footer.png");

            // Cambiamos el cuerpo del correo
            message.Body = builder.ToMessageBody();


            //message.Body = new TextPart("plain") { Text = web_ContactoCorreo.Mensaje };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("prueba20230227@gmail.com", "lestlxcvbrpkmonh");
                client.Send(message);
                client.Disconnect(true);
            }

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

        private bool Web_ContactoCorreoExists(int id)
        {
            return (_context.Web_ContactoCorreo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
