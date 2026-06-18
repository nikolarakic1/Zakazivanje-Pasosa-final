using Microsoft.Extensions.Configuration;
using ZakazivanjeTerminaModeli;

namespace ZakazivanjeTerminaPoslovnaLogika.Pravila
{
    public class PraviloObradeZahteva : IPraviloObradeZahteva
    {
        private readonly IConfiguration _configuration;

        public PraviloObradeZahteva(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Primeni(ZahtevZaPasos zahtev)
        {
            var naknadaTekst = _configuration["PoslovnaPravila:NaknadaZaHitanPostupak"] ?? "3500";

            decimal naknadaZaHitanPostupak = decimal.Parse(naknadaTekst);

            zahtev.IznosNaknade = 0;

            if (zahtev.HitanPostupak)
            {
                zahtev.IznosNaknade = naknadaZaHitanPostupak;

                if (string.IsNullOrWhiteSpace(zahtev.Napomena))
                {
                    zahtev.Napomena = $"Hitan postupak: obračunata dodatna naknada {naknadaZaHitanPostupak} RSD.";
                }
                else if (!zahtev.Napomena.Contains("Hitan postupak"))
                {
                    zahtev.Napomena += $" Hitan postupak: obračunata dodatna naknada {naknadaZaHitanPostupak} RSD.";
                }
            }
        }
    }
}