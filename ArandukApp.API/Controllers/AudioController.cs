using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArandukApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly ArandukAppContext _context;

        public AudioController(ArandukAppContext context)
        {
            _context = context;
        }

        // GET api/audio
        [HttpGet]
        public ActionResult<List<Audio>> GetAll()
        {
            return _context.Audios.ToList();
        }

        // GET api/audio/2
        [HttpGet("{id}", Name = "GetTexto")]
        public ActionResult<Audio> GetById(int id)
        {
            var item = _context.Audios.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/audio
        [HttpPost]
        public IActionResult Create(Audio item)
        {
            _context.Audios.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTexto", new { id = item.Id }, item);
        }

        // PUT api/audio/2
        [HttpPut("{id}")]
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

        // DELETE api/audio/2
        [HttpDelete("{id}")]
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