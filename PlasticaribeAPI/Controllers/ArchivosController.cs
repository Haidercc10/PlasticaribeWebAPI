using Aspose.Pdf.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.IO.Compression;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly dataContext _context;

        public ArchivosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Archivos
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Archivos.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Archivos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostArchivo([FromForm] List<IFormFile> archivo, DateTime Fecha, int categoria_Id, long usua_Id, string? filePath)
        {
            List<Archivos> archivos = new();
            if (filePath != null)
            {
                try
                {
                    if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                    if (archivo != null)
                    {
                        if (archivo.Count > 0)
                        {
                            foreach (var item in archivo)
                            {
                                using var stream = System.IO.File.Create(filePath + "\\" + item.FileName, 100000, FileOptions.Asynchronous);
                                archivo[0].CopyToAsync(stream);
                                Archivos archivo2 = new()
                                {
                                    Nombre = item.FileName,
                                    Ubicacion = filePath + "\\" + item.FileName,
                                    Fecha = Fecha,
                                    Categoria_Id = categoria_Id,
                                    Usua_Id = usua_Id
                                };
                                archivos.Add(archivo2);
                            }
                            _context.Archivos.AddRange(archivos);
                            _context.SaveChanges();
                        }
                        else return BadRequest();
                    }
                    else return Ok(filePath);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(archivo);
            }
            else return BadRequest();
        }

        [HttpGet("CrearCarpetas")]
        public ActionResult CrearCarpetas(string? filePath)
        {
            if (filePath != null)
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    return Ok();
                }
                else return Ok();
            }
            else return BadRequest();
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download(string file, string filePath)
        {
            filePath = Path.Combine(filePath, file);
            if (!System.IO.File.Exists(filePath)) return NotFound();

            var memory = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), file);
        }

        [HttpGet("downloadDirectory")]
        public ActionResult DownloadDirectory(string filePath, string directoy)
        {
            var dir = new DirectoryInfo(filePath);
            if (!dir.Exists) throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            string startPath = @$"{filePath}";
            string zipPath = @$"{filePath}.zip";

            if (System.IO.File.Exists(zipPath)) EliminarArchivos(zipPath);

            ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);

            return Ok($"¡La carpeta se ha comprimido, esta tiene el nombre {zipPath}!");
        }

        [HttpGet("Carpetas")]
        public IActionResult Carpetas(string? filePath)
        {
            if (filePath == null) return BadRequest();
            try
            {
                string[] folders = Directory.GetDirectories(filePath);
                return Ok(folders);
            }
            catch (System.IO.IOException e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("Archivos")]
        public IActionResult Archivos(string? filePath)
        {
            if (filePath == null) return BadRequest();
            try
            {
                string[] files = Directory.GetFiles(filePath);
                return Ok(files);
            }
            catch (System.IO.IOException e)
            {
                return NotFound(e);
            }
        }

        [HttpGet("EliminarArchivos")]
        public IActionResult EliminarArchivos(string? filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    return Ok();
                }
                catch (System.IO.IOException e)
                {
                    return (IActionResult)e;
                }
            }
            else return BadRequest();
        }

        [HttpGet("EliminarCarpeta")]
        public IActionResult EliminarCarpeta(string? filePath)
        {
            if (System.IO.Directory.Exists(filePath))
            {
                try
                {
                    System.IO.Directory.Delete(filePath, true);
                    return Ok();
                }
                catch (System.IO.IOException e)
                {
                    return BadRequest(e);
                }
            }
            else return BadRequest();
        }

        [HttpGet("MoverArchivos")]
        public IActionResult MoverArchivos(string filePathInicial, string filePathFinal)
        {
            System.IO.File.Move(filePathInicial, filePathFinal);
            return Ok();
        }

        [HttpGet("MoverCarpeta")]
        public IActionResult MoverCarpeta(string filePathInicial, string filePathFinal)
        {
            System.IO.Directory.Move(filePathInicial, filePathFinal);
            return Ok();
        }

        [HttpGet("CopiarArchivos")]
        public IActionResult CopiarArchivos(string filePathInicial, string filePathFinal)
        {
            System.IO.File.Copy(filePathInicial, filePathFinal, true);
            return Ok();
        }

        [HttpGet("CopiarCarpetas")]
        public void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists) throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        private static string GetContentType(string path)
        {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out string contentType)) contentType = "application/octet-stream";
            return contentType;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
        }
    }
}
