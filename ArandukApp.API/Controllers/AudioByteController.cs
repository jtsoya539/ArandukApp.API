using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;

namespace ArandukApp.API.Controllers
{
    [Route("arandukapp/api/[controller]")]
    [ApiController]
    public class AudioByteController : ControllerBase
    {
        private readonly ArandukAppContext _context;
        private IHostingEnvironment _env;

        public AudioByteController(ArandukAppContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
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
            var filePath = Path.Combine(_env.WebRootPath, "arandukapp", item.UrlAudio);
            byte[] cadena = System.IO.File.ReadAllBytes(filePath);
            return cadena;
        }
    }
}