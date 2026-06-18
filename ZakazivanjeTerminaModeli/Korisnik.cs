namespace ZakazivanjeTerminaModeli
{
    public class Korisnik : Entitet
    {
        public string Ime { get; set; } = string.Empty;

        public string Prezime { get; set; } = string.Empty;

        public string KorisnickoIme { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string LozinkaHash { get; set; } = string.Empty;

        public string Uloga { get; set; } = "Referent";

        public ICollection<ZahtevZaPasos> ZahteviZaPasos { get; set; } = new List<ZahtevZaPasos>();
    }
}