using Microsoft.EntityFrameworkCore;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaPodaci.Kontekst;

namespace ZakazivanjeTerminaPodaci.Repozitorijum
{
    public class DokumentacijaRepo : IDokumentacijaRepo
    {
        private readonly AppDbContext _context;

        public DokumentacijaRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Dodaj(Dokumentacija dokumentacija)
        {
            if (dokumentacija is null)
                throw new ArgumentException("Nemoguce dodati dokumentaciju");

            await _context.Dokumentacija.AddAsync(dokumentacija);
        }

        public async Task<Dokumentacija?> Izmeni(Dokumentacija dokumentacija, int id)
        {
            var nadjiDokumentaciju = await _context.Dokumentacija
                .FirstOrDefaultAsync(x => x.Id == id);

            if (nadjiDokumentaciju is null)
                return null;

            nadjiDokumentaciju.Dostavljeno = dokumentacija.Dostavljeno;
            nadjiDokumentaciju.DatumDostavljanja = dokumentacija.DatumDostavljanja;
            nadjiDokumentaciju.Napomena = dokumentacija.Napomena;
            nadjiDokumentaciju.ZahtevZaPasosId = dokumentacija.ZahtevZaPasosId;
            nadjiDokumentaciju.VrstaDokumentaId = dokumentacija.VrstaDokumentaId;
            nadjiDokumentaciju.DatumIzmene = DateTime.Now;

            return nadjiDokumentaciju;
        }

        public async Task<bool> Obrisi(int id)
        {
            var nadjiDokumentaciju = await _context.Dokumentacija
                .FirstOrDefaultAsync(x => x.Id == id);

            if (nadjiDokumentaciju is null)
                return false;

            _context.Remove(nadjiDokumentaciju);
            return true;
        }

        public async Task<Dokumentacija?> VratiPoIdu(int id)
        {
            return await _context.Dokumentacija
                .Include(x => x.ZahtevZaPasos)
                    .ThenInclude(x => x.PodnosilacZahteva)
                .Include(x => x.VrstaDokumenta)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Dokumentacija>> VratiSve()
        {
            return await _context.Dokumentacija
                .Include(x => x.ZahtevZaPasos)
                    .ThenInclude(x => x.PodnosilacZahteva)
                .Include(x => x.VrstaDokumenta)
                .ToListAsync();
        }

        public async Task<List<Dokumentacija>> VratiPoZahtevu(int zahtevZaPasosId)
        {
            return await _context.Dokumentacija
                .Include(x => x.ZahtevZaPasos)
                    .ThenInclude(x => x.PodnosilacZahteva)
                .Include(x => x.VrstaDokumenta)
                .Where(x => x.ZahtevZaPasosId == zahtevZaPasosId)
                .ToListAsync();
        }
    }
}