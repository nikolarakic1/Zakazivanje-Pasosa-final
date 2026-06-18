using Microsoft.EntityFrameworkCore;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaPodaci.Kontekst;

namespace ZakazivanjeTerminaPodaci.Repozitorijum
{
    public class ZahtevZaPasosRepo : IZahtevZaPasosRepo
    {
        private readonly AppDbContext _context;

        public ZahtevZaPasosRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Dodaj(ZahtevZaPasos zahtev)
        {
            if (zahtev is null)
                throw new ArgumentException("Nemoguće dodati zahtev za pasoš.");

            await _context.ZahteviZaPasos.AddAsync(zahtev);
        }

        public async Task<ZahtevZaPasos?> Izmeni(ZahtevZaPasos zahtev, int id)
        {
            var nadjiZahtev = await _context.ZahteviZaPasos
                .Include(x => x.PodnosilacZahteva)
                .Include(x => x.Dokumentacija)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (nadjiZahtev is null)
                return null;

            nadjiZahtev.BrojZahteva = zahtev.BrojZahteva;
            nadjiZahtev.DatumTermina = zahtev.DatumTermina;
            nadjiZahtev.VremeTermina = zahtev.VremeTermina;
            nadjiZahtev.StatusZahteva = zahtev.StatusZahteva;
            nadjiZahtev.VrstaPasosa = zahtev.VrstaPasosa;
            nadjiZahtev.RazlogIzdavanja = zahtev.RazlogIzdavanja;
            nadjiZahtev.MestoPodnosenja = zahtev.MestoPodnosenja;
            nadjiZahtev.ImaVazecuLicnuKartu = zahtev.ImaVazecuLicnuKartu;
            nadjiZahtev.DokumentacijaKompletna = zahtev.DokumentacijaKompletna;
            nadjiZahtev.Napomena = zahtev.Napomena;
            nadjiZahtev.RazlogOdbijanja = zahtev.RazlogOdbijanja;
            nadjiZahtev.PodnosilacZahtevaId = zahtev.PodnosilacZahtevaId;
            nadjiZahtev.KorisnikId = zahtev.KorisnikId;
            nadjiZahtev.HitanPostupak = zahtev.HitanPostupak;
            nadjiZahtev.IznosNaknade = zahtev.IznosNaknade;
            nadjiZahtev.DatumIzmene = DateTime.Now;

            return nadjiZahtev;
        }

        public async Task<bool> Obrisi(int id)
        {
            var nadjiZahtev = await _context.ZahteviZaPasos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (nadjiZahtev is null)
                return false;

            _context.Remove(nadjiZahtev);
            return true;
        }

        public async Task<ZahtevZaPasos?> VratiPoIdu(int id)
        {
            return await _context.ZahteviZaPasos
                .Include(x => x.PodnosilacZahteva)
                .Include(x => x.Korisnik)
                .Include(x => x.Dokumentacija)
                    .ThenInclude(x => x.VrstaDokumenta)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ZahtevZaPasos>> VratiSve()
        {
            return await _context.ZahteviZaPasos
                .Include(x => x.PodnosilacZahteva)
                .Include(x => x.Korisnik)
                .Include(x => x.Dokumentacija)
                    .ThenInclude(x => x.VrstaDokumenta)
                .ToListAsync();
        }

        public async Task<List<ZahtevZaPasos>> Filtriraj(string? status, string? jmbg)
        {
            var upit = _context.ZahteviZaPasos
                .Include(x => x.PodnosilacZahteva)
                .Include(x => x.Korisnik)
                .Include(x => x.Dokumentacija)
                    .ThenInclude(x => x.VrstaDokumenta)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                upit = upit.Where(x => x.StatusZahteva.Contains(status));
            }

            if (!string.IsNullOrWhiteSpace(jmbg))
            {
                upit = upit.Where(x => x.PodnosilacZahteva.JMBG.Contains(jmbg));
            }

            return await upit.ToListAsync();
        }
    }
}