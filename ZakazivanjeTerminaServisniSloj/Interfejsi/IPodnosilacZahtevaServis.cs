using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaServisniSloj.Interfejsi
{
    public interface IPodnosilacZahtevaServis
    {
        Task Dodaj(PodnosilacZahtevaDTO dto);

        Task<PodnosilacZahteva?> Izmeni(PodnosilacZahtevaDTO dto, int id);

        Task<bool> Obrisi(int id);

        Task<PodnosilacZahteva?> VratiPoIdu(int id);

        Task<List<PodnosilacZahteva>> VratiSve();
    }
}