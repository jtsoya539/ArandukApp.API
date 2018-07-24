using System.Collections.Generic;

namespace ArandukApp.API.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string NombreCastellano { get; set; }
        public string NombreGuarani { get; set; }
        public string UrlImagen { get; set; }
        public List<Texto> Textos { get; set; }
    }
}