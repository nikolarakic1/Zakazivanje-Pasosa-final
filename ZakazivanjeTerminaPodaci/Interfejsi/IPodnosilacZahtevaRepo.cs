using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaPodaci.Interfejsi;

public interface IPodnosilacZahtevaRepo
{
    Task Dodaj(PodnosilacZahteva zahtev);

    Task<PodnosilacZahteva?> Izmeni(PodnosilacZahteva zahtev, int id);

    Task<bool> Obrisi(int id);

    Task<PodnosilacZahteva?> VratiPoIdu(int id);

    Task<List<PodnosilacZahteva>> VratiSve();
}