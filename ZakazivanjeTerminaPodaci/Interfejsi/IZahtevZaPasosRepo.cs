using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaPodaci.Interfejsi;

public interface IZahtevZaPasosRepo
{
    Task Dodaj(ZahtevZaPasos zahtev);

    Task<ZahtevZaPasos?> Izmeni(ZahtevZaPasos zahtev, int id);

    Task<bool> Obrisi(int id);

    Task<ZahtevZaPasos?> VratiPoIdu(int id);

    Task<List<ZahtevZaPasos>> VratiSve();

    Task<List<ZahtevZaPasos>> Filtriraj(string? status, string? jmbg);
}