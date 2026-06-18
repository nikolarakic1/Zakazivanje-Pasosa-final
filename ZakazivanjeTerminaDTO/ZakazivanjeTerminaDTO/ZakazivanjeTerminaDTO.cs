namespace ZakazivanjeTerminaDTO
{
    public class PrikazVrsteDokumentaDTO
    {
        public int Id { get; set; }

        public string Naziv { get; set; } = string.Empty;

        public string Opis { get; set; } = string.Empty;

        public bool Obavezno { get; set; }
    }
}