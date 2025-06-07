using clase_dos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace clase_dos.Persistencia
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
