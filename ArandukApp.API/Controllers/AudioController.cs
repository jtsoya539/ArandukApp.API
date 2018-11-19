using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArandukApp.API.Controllers
{
    [Route("arandukapp/api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly ArandukAppContext _context;

        public AudioController(ArandukAppContext context)
        {
            _context = context;
        }

        // GET arandukapp/api/audio
        [HttpGet]
        [EnableCors]
        public ActionResult<List<Audio>> GetAll()
        {
            return _context.Audios.ToList();
        }

        // GET arandukapp/api/audio/2
        [HttpGet("{id}", Name = "GetAudio")]
        [EnableCors]
        public ActionResult<Audio> GetById(int id)
        {
            var item = _context.Audios.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST arandukapp/api/audio
        [HttpPost]
        [DisableCors]
        public IActionResult Create(Audio item)
        {
            _context.Audios.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetAudio", new { id = item.Id }, item);
        }

        // PUT arandukapp/api/audio/2
        [HttpPut("{id}")]
        [DisableCors]
        public IActionResult Update(int id, Audio texto)
        {
            var item = _context.Audios.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Titulo = texto.Titulo;
            item.Autor = texto.Autor;
            item.UrlAudio = texto.UrlAudio;
            item.CategoriaId = texto.CategoriaId;

            _context.Audios.Update(item);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE arandukapp/api/audio/2
        [HttpDelete("{id}")]
        [DisableCors]
        public IActionResult Delete(int id)
        {
            var item = _context.Audios.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Audios.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }

    }
}