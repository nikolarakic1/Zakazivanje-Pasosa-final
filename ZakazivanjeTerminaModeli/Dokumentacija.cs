namespace ZakazivanjeTerminaModeli
{
    public class Dokumentacija : Entitet
    {
        public bool Dostavljeno { get; set; }

        public DateTime? DatumDostavljanja { get; set; }

        public string? Napomena { get; set; }

        public int ZahtevZaPasosId { get; set; }

        public ZahtevZaPasos ZahtevZaPasos { get; set; } = null!;

        public int VrstaDokumentaId { get; set; }

        public VrstaDokumenta VrstaDokumenta { get; set; } = null!;
    }
}