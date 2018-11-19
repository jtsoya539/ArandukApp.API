using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace ArandukApp.API.Controllers
{
    [Route("arandukapp/api/[controller]")]
    [ApiController]
    public class AudioByteController : ControllerBase
    {
        private readonly ArandukAppContext _context;

        public AudioByteController(ArandukAppContext context)
        {
            _context = context;
        }

        // GET arandukapp/api/audiobyte/2
        [HttpGet("{id}")]
        [EnableCors]
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