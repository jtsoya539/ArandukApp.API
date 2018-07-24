using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArandukApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextoController : ControllerBase
    {
        private readonly ArandukAppContext _context;

        public TextoController(ArandukAppContext context)
        {
            _context = context;
        }

        // GET api/texto
        [HttpGet]
        public ActionResult<List<Texto>> GetAll()
        {
            return _context.Textos.ToList();
        }

        // GET api/texto/2
        [HttpGet("{id}", Name = "GetTexto")]
        public ActionResult<Texto> GetById(int id)
        {
            var item = _context.Textos.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/texto
        [HttpPost]
        public IActionResult Create(Texto item)
        {
            _context.Textos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTexto", new { id = item.Id }, item);
        }

        // PUT api/texto/2
        [HttpPut("{id}")]
        public IActionResult Update(int id, Texto texto)
        {
            var item = _context.Textos.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Titulo = texto.Titulo;
            item.Autor = texto.Autor;
            item.UrlAudio = texto.UrlAudio;
            item.IdCategoria = texto.IdCategoria;

            _context.Textos.Update(item);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/texto/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Textos.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Textos.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }

    }
}