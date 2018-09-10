using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
            byte[] cadena = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), item.UrlAudio.Substring(23)));
            return cadena;
        }

    }
}