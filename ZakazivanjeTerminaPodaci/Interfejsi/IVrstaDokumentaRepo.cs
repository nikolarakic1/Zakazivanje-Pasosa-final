using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaPodaci.Interfejsi;

public interface IVrstaDokumentaRepo
{
    Task<VrstaDokumenta?> VratiPoIdu(int id);

    Task<List<VrstaDokumenta>> VratiSve();

    Task<bool> Obrisi(int id);

    Task<VrstaDokumenta?> Izmeni(VrstaDokumenta dokument, int id);

    Task Dodaj(VrstaDokumenta dokument);
}