using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaPoslovnaLogika.Pravila;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisniSloj.Servisi
{
    public class ZahtevZaPasosServis : IZahtevZaPasosServis
    {
        private readonly IZahtevZaPasosRepo _repo;
        private readonly IVrstaDokumentaRepo _vrstaDokumentaRepo;
        private readonly ICuvanjePromena _cuvanje;
        private readonly IPraviloObradeZahteva _pravilo;

        public ZahtevZaPasosServis(
            IZahtevZaPasosRepo repo,
            IVrstaDokumentaRepo vrstaDokumentaRepo,
            ICuvanjePromena cuvanje,
            IPraviloObradeZahteva pravilo)
        {
            _repo = repo;
            _vrstaDokumentaRepo = vrstaDokumentaRepo;
            _cuvanje = cuvanje;
            _pravilo = pravilo;
        }

        public async Task Dodaj(ZahtevZaPasosDTO dto)
        {
            if (dto is null)
                throw new ArgumentException("Zahtev za pasoš ne sme biti prazan.");

            var vrsteDokumenata = await _vrstaDokumentaRepo.VratiSve();

            var zahtev = new ZahtevZaPasos
            {
                BrojZahteva = dto.BrojZahteva,
                DatumPodnosenja = DateTime.Now,
                DatumTermina = dto.DatumTermina,
                VremeTermina = dto.VremeTermina,
                StatusZahteva = dto.StatusZahteva,
                VrstaPasosa = dto.VrstaPasosa,
                RazlogIzdavanja = dto.RazlogIzdavanja,
                MestoPodnosenja = dto.MestoPodnosenja,
                ImaVazecuLicnuKartu = dto.ImaVazecuLicnuKartu,
                DokumentacijaKompletna = false,
                Napomena = dto.Napomena,
                RazlogOdbijanja = dto.RazlogOdbijanja,
                PodnosilacZahtevaId = dto.PodnosilacZahtevaId,
                KorisnikId = dto.KorisnikId,
                HitanPostupak = dto.HitanPostupak,
                IznosNaknade = dto.IznosNaknade
            };

            foreach (var vrsta in vrsteDokumenata)
            {
                zahtev.Dokumentacija.Add(new Dokumentacija
                {
                    VrstaDokumentaId = vrsta.Id,
                    Dostavljeno = false,
                    DatumDostavljanja = null,
                    Napomena = vrsta.Obavezno ? "Obavezan dokument" : "Opcioni dokument"
                });
            }

            _pravilo.Primeni(zahtev);

            await _repo.Dodaj(zahtev);
            await _cuvanje.Sacuvaj();
        }

        public async Task<PrikazZahtevaZaPasosDTO?> Izmeni(ZahtevZaPasosDTO dto, int id)
        {
            if (dto is null)
                throw new ArgumentException("Zahtev za pasoš ne sme biti prazan.");

            var zahtev = new ZahtevZaPasos
            {
                BrojZahteva = dto.BrojZahteva,
                DatumTermina = dto.DatumTermina,
                VremeTermina = dto.VremeTermina,
                StatusZahteva = dto.StatusZahteva,
                VrstaPasosa = dto.VrstaPasosa,
                RazlogIzdavanja = dto.RazlogIzdavanja,
                MestoPodnosenja = dto.MestoPodnosenja,
                ImaVazecuLicnuKartu = dto.ImaVazecuLicnuKartu,
                DokumentacijaKompletna = dto.DokumentacijaKompletna,
                Napomena = dto.Napomena,
                RazlogOdbijanja = dto.RazlogOdbijanja,
                PodnosilacZahtevaId = dto.PodnosilacZahtevaId,
                KorisnikId = dto.KorisnikId,
                HitanPostupak = dto.HitanPostupak,
                IznosNaknade = dto.IznosNaknade
            };

            _pravilo.Primeni(zahtev);

            var izmenjen = await _repo.Izmeni(zahtev, id);

            if (izmenjen is null)
                return null;

            await _cuvanje.Sacuvaj();

            return MapirajZaPrikaz(izmenjen);
        }

        public async Task<bool> Obrisi(int id)
        {
            var obrisan = await _repo.Obrisi(id);

            if (!obrisan)
                return false;

            await _cuvanje.Sacuvaj();
            return true;
        }

        public async Task<PrikazZahtevaZaPasosDTO?> VratiPoIdu(int id)
        {
            var zahtev = await _repo.VratiPoIdu(id);

            if (zahtev is null)
                return null;

            return MapirajZaPrikaz(zahtev);
        }

        public async Task<List<PrikazZahtevaZaPasosDTO>> VratiSve()
        {
            var zahtevi = await _repo.VratiSve();

            return zahtevi.Select(MapirajZaPrikaz).ToList();
        }

        public async Task<List<PrikazZahtevaZaPasosDTO>> Filtriraj(string? status, string? jmbg)
        {
            var zahtevi = await _repo.Filtriraj(status, jmbg);

            return zahtevi.Select(MapirajZaPrikaz).ToList();
        }

        private static PrikazZahtevaZaPasosDTO MapirajZaPrikaz(ZahtevZaPasos zahtev)
        {
            return new PrikazZahtevaZaPasosDTO
            {
                Id = zahtev.Id,
                PodnosilacZahtevaId = zahtev.PodnosilacZahtevaId,
                KorisnikId = zahtev.KorisnikId,
                BrojZahteva = zahtev.BrojZahteva,
                DatumPodnosenja = zahtev.DatumPodnosenja,
                DatumTermina = zahtev.DatumTermina,
                VremeTermina = zahtev.VremeTermina,
                StatusZahteva = zahtev.StatusZahteva,
                VrstaPasosa = zahtev.VrstaPasosa,
                RazlogIzdavanja = zahtev.RazlogIzdavanja,
                MestoPodnosenja = zahtev.MestoPodnosenja,
                ImaVazecuLicnuKartu = zahtev.ImaVazecuLicnuKartu,
                DokumentacijaKompletna = zahtev.DokumentacijaKompletna,
                HitanPostupak = zahtev.HitanPostupak,
                IznosNaknade = zahtev.IznosNaknade,
                ImePodnosioca = zahtev.PodnosilacZahteva?.Ime ?? "",
                PrezimePodnosioca = zahtev.PodnosilacZahteva?.Prezime ?? "",
                JMBG = zahtev.PodnosilacZahteva?.JMBG ?? "",
                Napomena = zahtev.Napomena,
                RazlogOdbijanja = zahtev.RazlogOdbijanja
            };
        }
    }
}