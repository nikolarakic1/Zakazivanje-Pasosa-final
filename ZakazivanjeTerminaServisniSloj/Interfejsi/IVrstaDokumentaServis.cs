using ZakazivanjeTerminaDTO;
using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaServisniSloj.Interfejsi
{
    public interface IVrstaDokumentaServis
    {
        Task Dodaj(VrstaDokumentaDTO dto);

        Task<VrstaDokumenta?> Izmeni(VrstaDokumentaDTO dto, int id);

        Task<bool> Obrisi(int id);

        Task<VrstaDokumenta?> VratiPoIdu(int id);

        Task<List<VrstaDokumenta>> VratiSve();
    }
}