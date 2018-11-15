using System.Collections.Generic;
using System.Linq;
using ArandukApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArandukApp.API.Controllers
{
    [Route("arandukapp/api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ArandukAppContext _context;

        public CategoriaController(ArandukAppContext context)
        {
            _context = context;
        }

        // GET arandukapp/api/categoria
        [HttpGet]
        [EnableCors]
        public ActionResult<List<Categoria>> GetAll()
        {
            List<Categoria> categorias = _context.Categorias.ToList();
            foreach (var categoria in categorias)
            {
                categoria.Audios = _context.Audios.Where(t => t.CategoriaId == categoria.Id).OrderBy(t => t.Id).ToList();
            }
            return categorias;
        }

        // GET arandukapp/api/categoria/5
        [HttpGet("{id}", Name = "GetCategoria")]
        [EnableCors]
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

        // POST arandukapp/api/categoria
        [HttpPost]
        [DisableCors]
        //[Authorize]
        public IActionResult Create(Categoria item)
        {
            _context.Categorias.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCategoria", new { id = item.Id }, item);
        }

        // PUT arandukapp/api/categoria/5
        [HttpPut("{id}")]
        [DisableCors]
        //[Authorize]
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

        // DELETE arandukapp/api/categoria/5
        [HttpDelete("{id}")]
        [DisableCors]
        //[Authorize]
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