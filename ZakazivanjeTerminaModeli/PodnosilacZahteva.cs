namespace ZakazivanjeTerminaModeli
{
    public class PodnosilacZahteva : Entitet
    {
        public string Ime { get; set; } = string.Empty;

        public string Prezime { get; set; } = string.Empty;

        public string JMBG { get; set; } = string.Empty;

        public DateTime DatumRodjenja { get; set; }

        public string MestoRodjenja { get; set; } = string.Empty;

        public string Drzavljanstvo { get; set; } = "Republika Srbija";

        public string Adresa { get; set; } = string.Empty;

        public string Grad { get; set; } = string.Empty;

        public string Telefon { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string BrojLicneKarte { get; set; } = string.Empty;

        public DateTime DatumVazenjaLicneKarte { get; set; }

        public ICollection<ZahtevZaPasos> ZahteviZaPasos { get; set; } = new List<ZahtevZaPasos>();
    }
}