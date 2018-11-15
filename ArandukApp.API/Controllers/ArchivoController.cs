using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Cors;

namespace ArandukApp.API.Controllers
{
    [Route("arandukapp/api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly ArandukAppContext _context;
        private IHostingEnvironment _env;

        public ArchivoController(ArandukAppContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // POST arandukapp/api/archivo
        [HttpPost]
        [DisableCors]
        public async Task<IActionResult> Upload(string model, int id, IFormFile file)
        {
            string fileName;
            string folder;
            if (model.Equals("categoria"))
            {
                folder = "png";
            }
            else if (model.Equals("audio"))
            {
                folder = "mp3";
            }
            else
            {
                return NoContent();
            }
            fileName = model + "_" + id.ToString() + "." + folder;

            // var filePath = Path.GetTempFileName();
            var filePath = Path.Combine(_env.WebRootPath, "arandukapp", "files", folder);
            filePath = Path.Combine(filePath, fileName);

            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok(new { count = file.Length, filePath });
        }
    }
}