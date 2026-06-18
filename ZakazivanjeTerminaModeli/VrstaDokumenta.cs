namespace ZakazivanjeTerminaModeli
{
    public class VrstaDokumenta : Entitet
    {
        public string Naziv { get; set; } = string.Empty;

        public string Opis { get; set; } = string.Empty;

        public bool Obavezno { get; set; }

        public ICollection<Dokumentacija> Dokumentacija { get; set; } = new List<Dokumentacija>();
    }
}