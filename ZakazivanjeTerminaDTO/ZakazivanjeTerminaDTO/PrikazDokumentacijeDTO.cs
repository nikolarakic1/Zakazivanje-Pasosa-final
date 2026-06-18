namespace ZakazivanjeTerminaDTO
{
    public class PrikazDokumentacijaDTO
    {
        public int Id { get; set; }

        public int ZahtevZaPasosId { get; set; }

        public string BrojZahteva { get; set; } = string.Empty;

        public string Podnosilac { get; set; } = string.Empty;

        public int VrstaDokumentaId { get; set; }

        public string NazivDokumenta { get; set; } = string.Empty;

        public bool Obavezno { get; set; }

        public bool Dostavljeno { get; set; }

        public DateTime? DatumDostavljanja { get; set; }

        public string? Napomena { get; set; }
    }
}