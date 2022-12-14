using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VersionCDK_ExtraCDK>().HasKey(x => new { x.VersionCDKId, x.ExtraCDKId});
        }

        public DbSet<MarcaCDK> MarcasCDK { get ; set; }
        public DbSet<ModeloCDK> ModelosCDK { get ; set; }   
        public DbSet<Comentario> Comentarios { get ; set; }
        public DbSet<ExtraCDK> ExtraCDK { get; set; }
        public DbSet<VersionCDK> VersionCDK { get; set; }
        public DbSet<VersionCDK_ExtraCDK> versionCDK_ExtraCDK { get ; set; }
        public DbSet<VendedorCDK> VendedorCDK { get; set; }
        public DbSet<ClienteCDK> ClientesCDK { get; set; }
        public DbSet<UbicacionDireccionCDK> UbicacionesDireccionCDK { get; set; }
        public DbSet<DireccionClienteCDK> DireccionClientesCDK { get; set; }
        public DbSet<AutosUsadosCDK> AutosUsadosCDK { get; set; }
    }
}
