using ZakazivanjeTerminaPodaci.Kontekst;
using ZakazivanjeTerminaServisniSloj.Interfejsi;

namespace ZakazivanjeTerminaServisniSloj.CuvanjePromena
{
    public class CuvanjeSaveChanges : ICuvanjePromena
    {
        private readonly AppDbContext _context;
        public CuvanjeSaveChanges(AppDbContext context)
        {
            _context = context;
        }
        public async Task Sacuvaj()
        {
            await _context.SaveChangesAsync();
        }
    }
}
