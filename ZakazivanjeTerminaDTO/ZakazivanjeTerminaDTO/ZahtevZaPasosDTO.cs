namespace ZakazivanjeTerminaDTO
{
    public class ZahtevZaPasosDTO
    {
        public string BrojZahteva { get; set; } = string.Empty;

        public DateTime DatumTermina { get; set; }

        public TimeSpan VremeTermina { get; set; }

        public string StatusZahteva { get; set; } = "Na čekanju";

        public string VrstaPasosa { get; set; } = string.Empty;

        public string RazlogIzdavanja { get; set; } = string.Empty;

        public string MestoPodnosenja { get; set; } = string.Empty;

        public bool ImaVazecuLicnuKartu { get; set; }

        public bool DokumentacijaKompletna { get; set; }

        public string? Napomena { get; set; }

        public string? RazlogOdbijanja { get; set; }

        public int PodnosilacZahtevaId { get; set; }

        public int KorisnikId { get; set; }
        public bool HitanPostupak { get; set; }

        public decimal IznosNaknade { get; set; }
    }
}