using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaPodaci.Interfejsi;

public interface IDokumentacijaRepo
{
    Task Dodaj(Dokumentacija dokumentacija);

    Task<Dokumentacija?> Izmeni(Dokumentacija dokumentacija, int id);

    Task<bool> Obrisi(int id);

    Task<Dokumentacija?> VratiPoIdu(int id);

    Task<List<Dokumentacija>> VratiSve();

    Task<List<Dokumentacija>> VratiPoZahtevu(int zahtevZaPasosId);
}