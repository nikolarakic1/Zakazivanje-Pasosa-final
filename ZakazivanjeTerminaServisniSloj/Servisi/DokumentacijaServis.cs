using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisniSloj.Servisi
{
    public class DokumentacijaServis : IDokumentacijaServis
    {
        private readonly IDokumentacijaRepo _repo;
        private readonly ICuvanjePromena _cuvanje;

        public DokumentacijaServis(IDokumentacijaRepo repo, ICuvanjePromena cuvanje)
        {
            _repo = repo;
            _cuvanje = cuvanje;
        }

        public async Task Dodaj(DokumentacijaDTO dto)
        {
            var dokumentacija = new Dokumentacija
            {
                Dostavljeno = dto.Dostavljeno,
                DatumDostavljanja = dto.Dostavljeno ? dto.DatumDostavljanja : null,
                Napomena = dto.Napomena,
                ZahtevZaPasosId = dto.ZahtevZaPasosId,
                VrstaDokumentaId = dto.VrstaDokumentaId
            };

            await _repo.Dodaj(dokumentacija);
            await _cuvanje.Sacuvaj();
        }

        public async Task<PrikazDokumentacijaDTO?> Izmeni(DokumentacijaDTO dto, int id)
        {
            var dokumentacija = new Dokumentacija
            {
                Dostavljeno = dto.Dostavljeno,
                DatumDostavljanja = dto.Dostavljeno ? dto.DatumDostavljanja : null,
                Napomena = dto.Napomena,
                ZahtevZaPasosId = dto.ZahtevZaPasosId,
                VrstaDokumentaId = dto.VrstaDokumentaId
            };

            var izmenjena = await _repo.Izmeni(dokumentacija, id);

            if (izmenjena is null)
                return null;

            await _cuvanje.Sacuvaj();

            var ponovoUcitaj = await _repo.VratiPoIdu(id);

            return ponovoUcitaj is null ? null : Mapiraj(ponovoUcitaj);
        }

        public async Task<bool> Obrisi(int id)
        {
            var obrisana = await _repo.Obrisi(id);

            if (!obrisana)
                return false;

            await _cuvanje.Sacuvaj();
            return true;
        }

        public async Task<PrikazDokumentacijaDTO?> VratiPoIdu(int id)
        {
            var dokumentacija = await _repo.VratiPoIdu(id);

            return dokumentacija is null ? null : Mapiraj(dokumentacija);
        }

        public async Task<List<PrikazDokumentacijaDTO>> VratiSve()
        {
            var lista = await _repo.VratiSve();

            return lista.Select(Mapiraj).ToList();
        }

        public async Task<List<PrikazDokumentacijaDTO>> VratiPoZahtevu(int zahtevZaPasosId)
        {
            var lista = await _repo.VratiPoZahtevu(zahtevZaPasosId);

            return lista.Select(Mapiraj).ToList();
        }

        private static PrikazDokumentacijaDTO Mapiraj(Dokumentacija d)
        {
            return new PrikazDokumentacijaDTO
            {
                Id = d.Id,
                ZahtevZaPasosId = d.ZahtevZaPasosId,
                BrojZahteva = d.ZahtevZaPasos?.BrojZahteva ?? "",
                Podnosilac = d.ZahtevZaPasos?.PodnosilacZahteva is null
                    ? ""
                    : $"{d.ZahtevZaPasos.PodnosilacZahteva.Ime} {d.ZahtevZaPasos.PodnosilacZahteva.Prezime}",
                VrstaDokumentaId = d.VrstaDokumentaId,
                NazivDokumenta = d.VrstaDokumenta?.Naziv ?? "",
                Obavezno = d.VrstaDokumenta?.Obavezno ?? false,
                Dostavljeno = d.Dostavljeno,
                DatumDostavljanja = d.DatumDostavljanja,
                Napomena = d.Napomena
            };
        }
    }
}