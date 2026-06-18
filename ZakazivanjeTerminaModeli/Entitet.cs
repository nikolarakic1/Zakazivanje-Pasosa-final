namespace ZakazivanjeTerminaModeli
{
    public abstract class Entitet
    {
        public int Id { get; set; }

        public DateTime DatumKreiranja { get; set; } = DateTime.Now;

        public DateTime? DatumIzmene { get; set; }

        public bool Obrisan { get; set; } = false;
    }
}