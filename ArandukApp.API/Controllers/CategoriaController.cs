using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArandukApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ArandukAppContext _context;

        public CategoriaController(ArandukAppContext context)
        {
            _context = context;

            // Solo para pruebas
            if (_context.Categorias.Count() == 0)
            {
                _context.Categorias.Add(new Categoria { NombreCastellano = "CATEGORIA 1" });
                _context.Categorias.Add(new Categoria { NombreCastellano = "CATEGORIA 2" });
                _context.SaveChanges();
            }
        }

        // GET api/categoria
        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            List<Categoria> categorias = _context.Categorias.ToList();
            foreach (var categoria in categorias)
            {
                categoria.Audios = _context.Audios.Where(t => t.CategoriaId == categoria.Id).OrderBy(t => t.Id).ToList();
            }
            return categorias;
        }

        // GET api/categoria/5
        [HttpGet("{id}", Name = "GetCategoria")]
        public ActionResult<Categoria> GetById(int id)
        {
            var item = _context.Categorias.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Audios = _context.Audios.Where(t => t.CategoriaId == item.Id).OrderBy(t => t.Id).ToList();
            return item;
        }

        [HttpPost]
        public IActionResult Create(Categoria item)
        {
            _context.Categorias.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCategoria", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Categoria categoria)
        {
            var categ = _context.Categorias.Find(id);
            if (categ == null)
            {
                return NotFound();
            }

            categ.NombreCastellano = categoria.NombreCastellano;
            categ.NombreGuarani = categoria.NombreGuarani;
            categ.UrlImagen = categoria.UrlImagen;
            categ.Audios = categoria.Audios;

            _context.Categorias.Update(categ);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categ = _context.Categorias.Find(id);
            if (categ == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categ);
            _context.SaveChanges();
            return NoContent();
        }
    }
}