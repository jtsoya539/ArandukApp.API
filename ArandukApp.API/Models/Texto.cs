namespace ArandukApp.API.Models
{
    public class Texto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string UrlAudio { get; set; }
        public int IdCategoria { get; set; }
    }
}