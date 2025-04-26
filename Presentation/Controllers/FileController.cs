using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            //Check if file is null
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //folder
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "media");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            //path

            var path = Path.Combine(folder, file.FileName);

            //stream
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //Response body
            return Ok(new
            {
                FilePath = path,
                FileName = file.FileName,
                FileSize = file.Length,
            });
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download(string fileName)
        {
            //Check if file is null
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //filePath
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Media", fileName);

            //ContentType = (MİME)
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
                contentType = "application/octet-stream";
            //Read
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(fileName));
        }
    }
}
