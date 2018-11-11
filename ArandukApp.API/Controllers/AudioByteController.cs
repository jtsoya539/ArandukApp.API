using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ArandukApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioByteController : ControllerBase
    {
        private readonly ArandukAppContext _context;

        public AudioByteController(ArandukAppContext context)
        {
            _context = context;
        }

        // GET api/audiobyte/2
        [HttpGet("{id}", Name = "GetAudioByte")]
        public ActionResult<byte[]> GetById(int id)
        {
            var item = _context.Audios.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            string ruta = FirstLetterToUpper(item.UrlAudio.Substring(23));
            byte[] cadena = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), ruta));
            return cadena;
        }

        // PUT api/audiobyte/2
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, IFormFile file)
        {
            long size = file.Length; // files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Archivos");


            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }


            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = size, filePath });
        }


        private string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

    }
}