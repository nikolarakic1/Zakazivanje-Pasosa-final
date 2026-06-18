using ZakazivanjeTerminaDTO;

namespace ZakazivanjeTerminaServisniSloj.Interfejsi
{
    public interface IZahtevZaPasosServis
    {
        Task Dodaj(ZahtevZaPasosDTO dto);
        Task<PrikazZahtevaZaPasosDTO?> Izmeni(ZahtevZaPasosDTO dto, int id);
        Task<bool> Obrisi(int id);
        Task<PrikazZahtevaZaPasosDTO?> VratiPoIdu(int id);
        Task<List<PrikazZahtevaZaPasosDTO>> VratiSve();
        Task<List<PrikazZahtevaZaPasosDTO>> Filtriraj(string? status, string? jmbg);
    }
}