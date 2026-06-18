using Microsoft.EntityFrameworkCore;
using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaPodaci.Kontekst
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Korisnik> Korisnici { get; set; }

        public DbSet<PodnosilacZahteva> PodnosilacZahteva{ get; set; }

        public DbSet<ZahtevZaPasos> ZahteviZaPasos { get; set; }

        public DbSet<Dokumentacija> Dokumentacija { get; set; }

        public DbSet<VrstaDokumenta> VrstaDokumenata { get; set; }
    }
}