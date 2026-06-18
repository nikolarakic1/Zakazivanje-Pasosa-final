namespace ZakazivanjeTerminaDTO
{
    public class KreirajZahtevZaPasosDTO
    {
        public string BrojZahteva { get; set; } = string.Empty;

        public DateTime DatumTermina { get; set; }

        public TimeSpan VremeTermina { get; set; }

        public string VrstaPasosa { get; set; } = string.Empty;

        public string RazlogIzdavanja { get; set; } = string.Empty;

        public string MestoPodnosenja { get; set; } = string.Empty;

        public bool ImaVazecuLicnuKartu { get; set; }

        public string? Napomena { get; set; }

        public int PodnosilacZahtevaId { get; set; }

        public int KorisnikId { get; set; }

        public List<DokumentacijaDTO> Dokumentacija { get; set; } = new();
    }
}