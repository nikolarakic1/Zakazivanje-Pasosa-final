using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakazivanjeTerminaModeli;
using ZakazivanjeTerminaPodaci.Interfejsi;
using ZakazivanjeTerminaPodaci.Kontekst;

namespace ZakazivanjeTerminaPodaci.Repozitorijum
{
    public class PodnosilacZahtevaRepo : IPodnosilacZahtevaRepo
    {
        private readonly AppDbContext _context;
        public PodnosilacZahtevaRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Dodaj(PodnosilacZahteva zahtev)
        {
            if (zahtev is null)
                throw new ArgumentException("Nemoguce dodati podnosioca zahteva");
            
            await _context.PodnosilacZahteva.AddAsync(zahtev);
           
        }

        public async Task<PodnosilacZahteva?> Izmeni(PodnosilacZahteva zahtev, int id)
        {
            var nadjiZahtev = await _context.PodnosilacZahteva
                .FirstOrDefaultAsync(x => x.Id == id);

            if (nadjiZahtev is null)
                return null;

            nadjiZahtev.Grad = zahtev.Grad;
            nadjiZahtev.Ime = zahtev.Ime;
            nadjiZahtev.JMBG = zahtev.JMBG;
            nadjiZahtev.MestoRodjenja = zahtev.MestoRodjenja;
            nadjiZahtev.Prezime = zahtev.Prezime;
            nadjiZahtev.Telefon = zahtev.Telefon;

            return nadjiZahtev;
        }

        public async Task<bool> Obrisi(int id)
        {
            var nadjiZahtev = await _context.PodnosilacZahteva.FirstOrDefaultAsync(x => x.Id == id);
            if (nadjiZahtev is null)
                return false;
             _context.Remove(nadjiZahtev);
            return true;
        }

        public async Task<PodnosilacZahteva?> VratiPoIdu(int id)
        {
            return await _context.PodnosilacZahteva
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PodnosilacZahteva>> VratiSve()
        {
            return await _context.PodnosilacZahteva
                .ToListAsync();
        }
    }
}
