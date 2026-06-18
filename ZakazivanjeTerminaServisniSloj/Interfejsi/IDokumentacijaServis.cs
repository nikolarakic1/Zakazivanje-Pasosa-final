using ZakazivanjeTerminaDTO;

namespace ZakazivanjeTerminaServisniSloj.Interfejsi
{
    public interface IDokumentacijaServis
    {
        Task Dodaj(DokumentacijaDTO dto);

        Task<PrikazDokumentacijaDTO?> Izmeni(DokumentacijaDTO dto, int id);

        Task<bool> Obrisi(int id);

        Task<PrikazDokumentacijaDTO?> VratiPoIdu(int id);

        Task<List<PrikazDokumentacijaDTO>> VratiSve();

        Task<List<PrikazDokumentacijaDTO>> VratiPoZahtevu(int zahtevZaPasosId);
    }
}