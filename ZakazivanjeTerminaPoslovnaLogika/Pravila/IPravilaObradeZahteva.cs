using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaPoslovnaLogika.Pravila
{
    public interface IPraviloObradeZahteva
    {
        void Primeni(ZahtevZaPasos zahtev);
    }
}