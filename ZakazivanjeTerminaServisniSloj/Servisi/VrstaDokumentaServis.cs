using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisniSloj.Servisi
{
    public class VrstaDokumentaServis : IVrstaDokumentaServis
    {
        private readonly IVrstaDokumentaRepo _repo;
        private readonly ICuvanjePromena _cuvanje;

        public VrstaDokumentaServis(
            IVrstaDokumentaRepo repo,
            ICuvanjePromena cuvanje)
        {
            _repo = repo;
            _cuvanje = cuvanje;
        }

        public async Task Dodaj(VrstaDokumentaDTO dto)
        {
            var dokument = new VrstaDokumenta
            {
                Naziv = dto.Naziv,
                Opis = dto.Opis,
                Obavezno = dto.Obavezno
            };

            await _repo.Dodaj(dokument);
            await _cuvanje.Sacuvaj();
        }

        public async Task<VrstaDokumenta?> Izmeni(VrstaDokumentaDTO dto, int id)
        {
            var dokument = new VrstaDokumenta
            {
                Naziv = dto.Naziv,
                Opis = dto.Opis,
                Obavezno = dto.Obavezno
            };

            var izmenjen = await _repo.Izmeni(dokument, id);

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

        public async Task<VrstaDokumenta?> VratiPoIdu(int id)
        {
            return await _repo.VratiPoIdu(id);
        }

        public async Task<List<VrstaDokumenta>> VratiSve()
        {
            return await _repo.VratiSve();
        }
    }
}