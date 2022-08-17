using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MarcaCDK> MarcasCDK { get ; set; }
        public DbSet<ModeloCDK> ModelosCDK { get ; set; }   
        public DbSet<Comentario> Comentarios { get ; set; }
    }
}
