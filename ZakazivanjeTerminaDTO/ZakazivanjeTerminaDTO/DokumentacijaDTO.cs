namespace ZakazivanjeTerminaDTO
{
    public class DokumentacijaDTO
    {
        public int Id { get; set; }
        public bool Dostavljeno { get; set; }

        public DateTime? DatumDostavljanja { get; set; }

        public string? Napomena { get; set; }

        public int ZahtevZaPasosId { get; set; }

        public int VrstaDokumentaId { get; set; }
    }
}