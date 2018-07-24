using Microsoft.EntityFrameworkCore;

namespace ArandukApp.API.Models
{
    public class ArandukAppContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Texto> Textos { get; set; }

        public ArandukAppContext(DbContextOptions<ArandukAppContext> options) : base(options) { }
    }
}