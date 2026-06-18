using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisniSloj.Servisi
{
    public class PodnosilacZahtevaServis : IPodnosilacZahtevaServis
    {
        private readonly IPodnosilacZahtevaRepo _repo;
        private readonly ICuvanjePromena _cuvanje;

        public PodnosilacZahtevaServis(IPodnosilacZahtevaRepo repo, ICuvanjePromena cuvanje)
        {
            _repo = repo;
            _cuvanje = cuvanje;
        }

        public async Task Dodaj(PodnosilacZahtevaDTO dto)
        {
            var podnosilac = new PodnosilacZahteva
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                JMBG = dto.JMBG,
                DatumRodjenja = dto.DatumRodjenja,
                MestoRodjenja = dto.MestoRodjenja,
                Drzavljanstvo = dto.Drzavljanstvo,
                Adresa = dto.Adresa,
                Grad = dto.Grad,
                Telefon = dto.Telefon,
                Email = dto.Email,
                BrojLicneKarte = dto.BrojLicneKarte,
                DatumVazenjaLicneKarte = dto.DatumVazenjaLicneKarte
            };

            await _repo.Dodaj(podnosilac);
            await _cuvanje.Sacuvaj();
        }

        public async Task<PodnosilacZahteva?> Izmeni(PodnosilacZahtevaDTO dto, int id)
        {
            var podnosilac = new PodnosilacZahteva
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                JMBG = dto.JMBG,
                DatumRodjenja = dto.DatumRodjenja,
                MestoRodjenja = dto.MestoRodjenja,
                Drzavljanstvo = dto.Drzavljanstvo,
                Adresa = dto.Adresa,
                Grad = dto.Grad,
                Telefon = dto.Telefon,
                Email = dto.Email,
                BrojLicneKarte = dto.BrojLicneKarte,
                DatumVazenjaLicneKarte = dto.DatumVazenjaLicneKarte
            };

            var izmenjen = await _repo.Izmeni(podnosilac, id);

            if (izmenjen is null)
                return null;

            await _cuvanje.Sacuvaj();
            return izmenjen;
        }

        public async Task<bool> Obrisi(int id)
        {
            var obrisan = await _repo.Obrisi(id);

            if (!obrisan)
                return false;

            await _cuvanje.Sacuvaj();
            return true;
        }

        public async Task<PodnosilacZahteva?> VratiPoIdu(int id)
        {
            return await _repo.VratiPoIdu(id);
        }

        public async Task<List<PodnosilacZahteva>> VratiSve()
        {
            return await _repo.VratiSve();
        }
    }
}